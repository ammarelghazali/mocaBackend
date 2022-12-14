using AutoMapper;
using Dapper;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.City;
using MOCA.Core.DTOs.LocationManagment.Country;
using MOCA.Core.DTOs.LocationManagment.District;
using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.LocationManagment
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        public LocationService(
            IAuthenticatedUserService authenticatedUserService, 
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _authenticatedUserService = authenticatedUserService ?? throw new ArgumentNullException(nameof(authenticatedUserService));
        }

        public async Task<Response<long>> AddLocation(LocationModel request)
        {
            try
            {
                var location = _mapper.Map<Location>(request);
                if (string.IsNullOrWhiteSpace(location.CreatedBy))
                {
                    if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                    {
                        throw new UnauthorizedAccessException("User is not authorized");
                    }
                    else
                    { location.CreatedBy = _authenticatedUserService.UserId; }
                }
                if (location.CreatedAt == null || location.CreatedAt == default)
                {
                    location.CreatedAt = _dateTimeService.NowUtc;
                }

                #region Validation
                if (request.LocationWorkingHours.Count == 0)
                {
                    return new Response<long>("You Must enter Working Hours for location.");
                }

                bool NameChecker = await _unitOfWork.LocationRepoEF.CheckLocationNameIsUinque(request.Name);
                if (NameChecker == false)
                {
                    return new Response<long>("Location Name is not unique.");
                }

                var DistrictChecker = await _unitOfWork.DistrictRepo.GetByIdAsync(request.DistrictId);
                if (DistrictChecker == null)
                {
                    return new Response<long>("Location District is not found.");
                }

                var CurrencyChecker = await _unitOfWork.CurrencyRepo.GetByIdAsync(request.CurrencyId);
                if (CurrencyChecker == null)
                {
                    return new Response<long>("Currency is not found.");
                }

                var LocationTypeChecker = await _unitOfWork.LocationTypeRepo.GetByIdAsync(request.LocationTypeId);
                if (LocationTypeChecker == null)
                {
                    return new Response<long>("Location Type is not found.");
                }
                #endregion

                _unitOfWork.LocationRepo.Insert(location);
                if (await _unitOfWork.SaveAsync() < 1)
                {
                    return new Response<long>("Cannot Add Location right now");
                }

                #region Add Location Bank Account
                var locationBankAccount = _mapper.Map<LocationBankAccount>(request.LocationBankAccount);
                locationBankAccount.LocationId = location.Id;
                _unitOfWork.LocationBankAccountRepo.Insert(locationBankAccount);
                if (await _unitOfWork.SaveAsync() < 1)
                {
                    return new Response<long>("Cannot Add LocationBankAccount right now");
                }
                #endregion
                return new Response<long>(location.Id, "Location Added Successfully.");
            }
            catch (Exception ex)
            {
                return new Response<long>($"error while add location. ERROR : {ex}.");
            }
            
        }

        public async Task<Response<long>> UpdateLocation(LocationModel request)
        {
            try
            {
                var location = _mapper.Map<Location>(request);
                if (string.IsNullOrWhiteSpace(location.CreatedBy))
                {
                    if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                    {
                        throw new UnauthorizedAccessException("User is not authorized");
                    }
                    else
                    { location.CreatedBy = _authenticatedUserService.UserId; }
                }
                if (location.CreatedAt == null || location.CreatedAt == default)
                {
                    location.CreatedAt = _dateTimeService.NowUtc;
                }

                #region Validation
                if (request.LocationWorkingHours.Count == 0)
                {
                    return new Response<long>("You Must enter Working Hours for location.");
                }

                bool NameChecker = await _unitOfWork.LocationRepoEF.CheckLocationNameIsUinque(request.Name, request.Id);
                if (NameChecker == false)
                {
                    return new Response<long>("Location Name is not unique.");
                }

                var DistrictChecker = await _unitOfWork.DistrictRepo.GetByIdAsync(request.DistrictId);
                if (DistrictChecker == null)
                {
                    return new Response<long>("Location District is not found.");
                }

                var CurrencyChecker = await _unitOfWork.CurrencyRepo.GetByIdAsync(request.CurrencyId);
                if (CurrencyChecker == null)
                {
                    return new Response<long>("Currency is not found.");
                }

                var LocationTypeChecker = await _unitOfWork.LocationTypeRepo.GetByIdAsync(request.LocationTypeId);
                if (LocationTypeChecker == null)
                {
                    return new Response<long>("Location Type is not found.");
                }
                #endregion

                #region Delete Old Service Fee Payments Due Date And Add New One
                var serviceFeePaymentsDueDate = _mapper.Map<List<ServiceFeePaymentsDueDate>>(request.ServiceFeePaymentsDueDates);
                for (int i = 0; i < serviceFeePaymentsDueDate.Count; i++)
                {
                    serviceFeePaymentsDueDate[i].Id = 0;
                    serviceFeePaymentsDueDate[i].CreatedBy = _authenticatedUserService.UserId;
                    serviceFeePaymentsDueDate[i].CreatedAt = _dateTimeService.NowUtc;
                }
                _unitOfWork.ServiceFeePaymentsDueDateRepoEF.DeleteAllServiceFeePaymentsDueDateByLocationID(request.Id);
                /*_unitOfWork.ServiceFeePaymentsDueDateRepo.InsertRang(serviceFeePaymentsDueDate);
                if (await _unitOfWork.SaveAsync() < 1)
                {
                    return new Response<long>("Cannot Update ServiceFeePaymentsDueDate right now");
                }*/
                location.ServiceFeePaymentsDueDates = new List<ServiceFeePaymentsDueDate>(serviceFeePaymentsDueDate);
                #endregion

                #region Delete Old Location Contact And Add New One
                var locationContact = _mapper.Map<List<LocationContact>>(request.LocationContacts);
                for (int i = 0; i < locationContact.Count; i++)
                {
                    locationContact[i].Id = 0;
                    locationContact[i].CreatedBy = _authenticatedUserService.UserId;
                    locationContact[i].CreatedAt = _dateTimeService.NowUtc;
                }
                _unitOfWork.LocationContactRepoEF.DeleteAllLocationContactByLocationID(request.Id);
                /*_unitOfWork.LocationContactRepo.InsertRang(locationContact);
                if (await _unitOfWork.SaveAsync() < 1)
                {
                    return new Response<long>("Cannot Update LocationContact right now");
                }*/
                location.LocationContacts = new List<LocationContact>(locationContact);
                #endregion

                #region Delete Old Location Image And Add New One
                var locationImage = _mapper.Map<List<LocationImage>>(request.LocationImages);
                for (int i = 0; i < locationImage.Count; i++)
                {
                    locationImage[i].Id = 0;
                    locationImage[i].CreatedBy = _authenticatedUserService.UserId;
                    locationImage[i].CreatedAt = _dateTimeService.NowUtc;
                }
                _unitOfWork.LocationImageRepoEF.DeleteAllLocationImageByLocationID(request.Id);
                /*_unitOfWork.LocationImageRepo.InsertRang(locationImage);
                if (await _unitOfWork.SaveAsync() < 1)
                {
                    return new Response<long>("Cannot Update LocationImage right now");
                }*/
                location.LocationImages = new List<LocationImage>(locationImage);
                #endregion

                #region Delete Old Location Currency Add New One
                var locationCurrency = _mapper.Map<List<LocationCurrency>>(request.LocationCurrencies);
                for (int i = 0; i < locationCurrency.Count; i++)
                {
                    locationCurrency[i].Id = 0;
                    locationCurrency[i].CreatedBy = _authenticatedUserService.UserId;
                    locationCurrency[i].CreatedAt = _dateTimeService.NowUtc;
                }
                _unitOfWork.LocationCurrencyRepoEF.DeleteByLocationID(request.Id);
                /*_unitOfWork.LocationCurrencyRepo.InsertRang(locationCurrency);
                if (await _unitOfWork.SaveAsync() < 1)
                {
                    return new Response<long>("Cannot Update LocationCurrency right now");
                }*/
                location.LocationCurrencies = new List<LocationCurrency>(locationCurrency);
                #endregion

                #region Delete Old Location File Add New One
                if (request.LocationFiles.Count != 0)
                {
                    var locationFile = _mapper.Map<List<LocationFile>>(request.LocationFiles);
                    for (int i = 0; i < locationFile.Count; i++)
                    {
                        locationFile[i].Id = 0;
                        locationFile[i].CreatedBy = _authenticatedUserService.UserId;
                        locationFile[i].CreatedAt = _dateTimeService.NowUtc;
                    }
                    _unitOfWork.LocationFileRepoEF.DeleteAllLocationFileByLocationID(request.Id);
                    /*_unitOfWork.LocationFileRepo.InsertRang(locationFile);
                    if (await _unitOfWork.SaveAsync() < 1)
                    {
                        return new Response<long>("Cannot Update LocationFile right now");
                    }*/
                    location.LocationFiles = new List<LocationFile>(locationFile);
                }
                #endregion

                #region Delete Old Location Working Hour Add New One
                var locationWorkingHour = _mapper.Map<List<LocationWorkingHour>>(request.LocationWorkingHours);
                for (int i = 0; i < locationWorkingHour.Count; i++)
                {
                    locationWorkingHour[i].Id = 0;
                    locationWorkingHour[i].CreatedBy = _authenticatedUserService.UserId;
                    locationWorkingHour[i].CreatedAt = _dateTimeService.NowUtc;
                }
                _unitOfWork.LocationWorkingHourRepoEF.DeleteAllLocationWorkingHourByLocationID(request.Id);
                /*_unitOfWork.LocationWorkingHourRepo.InsertRang(locationWorkingHour);
                if (await _unitOfWork.SaveAsync() < 1)
                {
                    return new Response<long>("Cannot Update LocationWorkingHour right now");
                }*/
                location.LocationWorkingHours = new List<LocationWorkingHour>(locationWorkingHour);
                #endregion

                #region Delete Old Location Bank Account Add New One
                var locationBankAccount = _mapper.Map<LocationBankAccount>(request.LocationBankAccount);
                locationBankAccount.Id = 0;
                locationBankAccount.LocationId = request.Id;
                locationBankAccount.CreatedBy = _authenticatedUserService.UserId;
                locationBankAccount.CreatedAt = _dateTimeService.NowUtc;
                _unitOfWork.LocationBankAccountRepoEF.DeleteByLocationID(request.Id);
                _unitOfWork.LocationBankAccountRepo.Insert(locationBankAccount);
                #endregion

                #region Delete Old Location Inclusion Add New One
                var locationInclusion = _mapper.Map<List<LocationInclusion>>(request.LocationInclusions);
                for (int i = 0; i < locationInclusion.Count; i++)
                {
                    locationInclusion[i].Id = 0;
                    locationInclusion[i].CreatedBy = _authenticatedUserService.UserId;
                    locationInclusion[i].CreatedAt = _dateTimeService.NowUtc;
                }
                _unitOfWork.LocationInclusionRepoEF.DeleteAllLocationInclusionByLocationID(request.Id);
                /*_unitOfWork.LocationInclusionRepo.InsertRang(locationInclusion);
                if (await _unitOfWork.SaveAsync() < 1)
                {
                    return new Response<long>("Cannot Update LocationInclusion right now");
                }*/
                location.LocationInclusions = new List<LocationInclusion>(locationInclusion);
                #endregion

                _unitOfWork.LocationRepo.Update(location);
                if (await _unitOfWork.SaveAsync() < 1)
                {
                    return new Response<long>("Cannot Updated Location right now");
                }
                return new Response<long>(location.Id, "Location updated Successfully.");
            }
            catch (Exception ex)
            {
                return new Response<long>($"error while update location. ERROR : {ex}.");
            }
        }

        public async Task<Response<bool>> DeleteLocation(long LocationId)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            #region Delete Service Fee Payments Due Date
            _unitOfWork.ServiceFeePaymentsDueDateRepoEF.DeleteAllServiceFeePaymentsDueDateByLocationID(LocationId);
            #endregion

            #region Delete Location Contact
            _unitOfWork.LocationContactRepoEF.DeleteAllLocationContactByLocationID(LocationId);
            #endregion

            #region Delete Location Image
            _unitOfWork.LocationImageRepoEF.DeleteAllLocationImageByLocationID(LocationId);
            #endregion

            #region Delete Location Currency
            _unitOfWork.LocationCurrencyRepoEF.DeleteByLocationID(LocationId);
            #endregion

            #region Delete Location File
            var files = await _unitOfWork.LocationFileRepoEF.GetAllLocationFileByLocationID(LocationId);
            if (files.Count > 0)
            {
                _unitOfWork.LocationFileRepoEF.DeleteAllLocationFileByLocationID(LocationId);
            }
            #endregion

            #region Delete Location Working Hour
            _unitOfWork.LocationWorkingHourRepoEF.DeleteAllLocationWorkingHourByLocationID(LocationId);
            #endregion

            #region Delete Location Bank Account
            _unitOfWork.LocationBankAccountRepoEF.DeleteByLocationID(LocationId);
            #endregion

            #region Delete Location Inclusion
            _unitOfWork.LocationInclusionRepoEF.DeleteAllLocationInclusionByLocationID(LocationId);
            #endregion

            var location = await _unitOfWork.LocationRepoEF.DeleteLocation(LocationId);
            if (location == false)
                return new Response<bool>("Location With This ID didn't exist.");

            if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<bool>("Cannot Delete Location right now");
            }
            return new Response<bool>(true, "Location Deleted Successfully.");
        }

        public async Task<Response<LocationDropDown>> GetAllForDropDown()
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }

            var districtIDs = await _unitOfWork.LocationRepoEF.GetAllDistinictDistrict();

            List<DistrictModel> Districts = new List<DistrictModel>();
            foreach (var id in districtIDs)
            {
                var district = await _unitOfWork.DistrictRepo.GetByIdAsync(id);
                Districts.Add(_mapper.Map<DistrictModel>(district));
            }

            IEnumerable<long> cityIDs = Districts.AsQueryable().Select(c => c.CityId).Distinct();
            List<CityModel> Cities = new List<CityModel>();
            foreach (var id in cityIDs)
            {
                var city = await _unitOfWork.CityRepo.GetByIdAsync(id);
                Cities.Add(_mapper.Map<CityModel>(city));
            }

            var location = await _unitOfWork.LocationRepoEF.GetAllDistinictLocation();

            LocationDropDown locationRegion = new LocationDropDown
            {
                Districts = Districts,
                Cities = Cities,
                Locations = location
            };

            return new Response<LocationDropDown>(locationRegion);
        }

        public async Task<Response<LocationDetailsModel>> GetLocationByID(long Id)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            if (Id <= 0)
            {
                return new Response<LocationDetailsModel>("ID must be greater than zero.");
            }
            LocationModel locationModel = new LocationModel();
            var location = await _unitOfWork.LocationRepo.GetByIdAsync(Id);
            if (location == null)
            {
                return new Response<LocationDetailsModel>(null, "No Location Found With This ID.");
            }
            locationModel = _mapper.Map<LocationModel>(location);

            LocationDetailsModel locationDetails = new LocationDetailsModel();
            locationDetails = _mapper.Map<LocationDetailsModel>(locationModel);

            #region serviceFee
            var serviceFee = await _unitOfWork.ServiceFeePaymentsDueDateRepoEF.GetAllServiceFeePaymentsDueDateByLocationID(Id);
            locationDetails.ServiceFeePaymentsDueDates = _mapper.Map<List<ServiceFeePaymentsDueDateModel>>(serviceFee);
            #endregion

            #region LocationImage
            var locationImage = await _unitOfWork.LocationImageRepoEF.GetAllLocationImageByLocationID(Id);
            locationDetails.LocationImages = _mapper.Map<List<LocationImageModel>>(locationImage);
            #endregion

            #region LocationContact
            var locationContact = await _unitOfWork.LocationContactRepoEF.GetAllLocationContactByLocationID(Id);
            locationDetails.LocationContacts = _mapper.Map<List<LocationContactModel>>(locationContact);
            #endregion

            #region LocationCurrency
            var locationCurrency = await _unitOfWork.LocationCurrencyRepoEF.GetByLocationID(Id);
            locationDetails.LocationCurrencies = _mapper.Map<List<LocationCurrencyModelGetMultiSelect>>(locationCurrency);
            #endregion

            #region LocationFile
            var locationFile = await _unitOfWork.LocationFileRepoEF.GetAllLocationFileByLocationID(Id);
            locationDetails.LocationFiles = _mapper.Map<List<LocationFileModel>>(locationFile);
            #endregion

            #region LocationWorkingHour
            var locationWorkingHour = await _unitOfWork.LocationWorkingHourRepoEF.GetAllLocationWorkingHourByLocationID(Id);
            locationDetails.LocationWorkingHours = _mapper.Map<List<LocationWorkingHourModel>>(locationWorkingHour);
            #endregion

            #region LocationBankAccount
            var locationBankAccount = await _unitOfWork.LocationBankAccountRepoEF.GetByLocationID(Id);
            locationDetails.LocationBankAccount = _mapper.Map<LocationBankAccountModel>(locationBankAccount);
            #endregion

            #region locationInclusion
            var locationInclusion = await _unitOfWork.LocationInclusionRepoEF.GetAllLocationInclusionByLocationID(Id);
            locationDetails.LocationInclusions = _mapper.Map<List<LocationInclusionModelGetMultiSelect>>(locationInclusion);
            #endregion

            #region District
            var District = await _unitOfWork.DistrictRepo.GetByIdAsync(locationModel.DistrictId);
            locationDetails.District = new DropdownViewModel
            {
                Id = District.Id,
                Name = District.DistrictName
            };
            #endregion

            #region City
            var City = await _unitOfWork.CityRepo.GetByIdAsync(District.CityId);
            locationDetails.City = new DropdownViewModel
            {
                Id = City.Id,
                Name = City.CityName
            };
            #endregion

            #region Country
            var Country = await _unitOfWork.CountryRepo.GetByIdAsync(City.CountryId);
            locationDetails.Country = new DropdownViewModel
            {
                Id = Country.Id,
                Name = Country.CountryName
            };
            #endregion

            #region Currency
            var Currency = await _unitOfWork.CurrencyRepo.GetByIdAsync(locationModel.CurrencyId);
            locationDetails.Currency = new DropdownViewModel
            {
                Id = Currency.Id,
                Name = Currency.Name
            };
            #endregion

            #region LocationType
            var LocationType = await _unitOfWork.LocationTypeRepo.GetByIdAsync(locationModel.LocationTypeId);
            locationDetails.LocationType = new DropdownViewModel
            {
                Id = LocationType.Id,
                Name = LocationType.Name
            };
            #endregion

            return new Response<LocationDetailsModel>(locationDetails);
        }

        public async Task<PagedResponse<List<LocationModel>>> GetAllLocationWithPagination(RequestParameter filter)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            int pg_total = await _unitOfWork.LocationRepo.GetCountAsync(x => x.IsDeleted == false);
            var data = _unitOfWork.LocationRepo.GetPaged(filter.PageNumber,
                filter.PageSize,
                f => f.IsDeleted == false,
                q => q.OrderBy(o => o.Name));

            if (data.Count() == 0)
            {
                return new PagedResponse<List<LocationModel>>(null, filter.PageNumber, filter.PageSize);
            }

            List<LocationModel> locationDetails = new List<LocationModel>();
            locationDetails = _mapper.Map<List<LocationModel>>(data);

            for (int i = 0; i < locationDetails.Count; i++)
            {
                #region serviceFee
                var serviceFee = await _unitOfWork.ServiceFeePaymentsDueDateRepoEF.GetAllServiceFeePaymentsDueDateByLocationID(locationDetails[i].Id);
                locationDetails[i].ServiceFeePaymentsDueDates = _mapper.Map<List<ServiceFeePaymentsDueDateModel>>(serviceFee);
                #endregion

                #region LocationImage
                var locationImage = await _unitOfWork.LocationImageRepoEF.GetAllLocationImageByLocationID(locationDetails[i].Id);
                locationDetails[i].LocationImages = _mapper.Map<List<LocationImageModel>>(locationImage);
                #endregion

                #region LocationContact
                var locationContact = await _unitOfWork.LocationContactRepoEF.GetAllLocationContactByLocationID(locationDetails[i].Id);
                locationDetails[i].LocationContacts = _mapper.Map<List<LocationContactModel>>(locationContact);
                #endregion

                #region LocationCurrency
                var locationCurrency = await _unitOfWork.LocationCurrencyRepoEF.GetByLocationID(locationDetails[i].Id);
                locationDetails[i].LocationCurrencies = _mapper.Map<List<LocationCurrencyModel>>(locationCurrency);
                #endregion

                #region LocationFile
                var locationFile = await _unitOfWork.LocationFileRepoEF.GetAllLocationFileByLocationID(locationDetails[i].Id);
                locationDetails[i].LocationFiles = _mapper.Map<List<LocationFileModel>>(locationFile);
                #endregion

                #region LocationWorkingHour
                var locationWorkingHour = await _unitOfWork.LocationWorkingHourRepoEF.GetAllLocationWorkingHourByLocationID(locationDetails[i].Id);
                locationDetails[i].LocationWorkingHours = _mapper.Map<List<LocationWorkingHourModel>>(locationWorkingHour);
                #endregion

                #region LocationBankAccount
                var locationBankAccount = await _unitOfWork.LocationBankAccountRepoEF.GetByLocationID(locationDetails[i].Id);
                locationDetails[i].LocationBankAccount = _mapper.Map<LocationBankAccountModel>(locationBankAccount);
                #endregion

                #region locationInclusion
                var locationInclusion = await _unitOfWork.LocationInclusionRepoEF.GetAllLocationInclusionByLocationID(locationDetails[i].Id);
                locationDetails[i].LocationInclusions = _mapper.Map<List<LocationInclusionModel>>(locationInclusion);
                #endregion
            }

            return new PagedResponse<List<LocationModel>>(locationDetails, filter.PageNumber, filter.PageSize, pg_total);
        }

        public async Task<Response<List<LocationModel>>> GetAllLocationWithoutPagination()
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var data = _unitOfWork.LocationRepo.GetAll();

            if (data.Count() == 0)
            {
                return new Response<List<LocationModel>>(null, "Not Data Found");
            }

            List<LocationModel> locationDetails = new List<LocationModel>();
            locationDetails = _mapper.Map<List<LocationModel>>(data);

            for (int i = 0; i < locationDetails.Count; i++)
            {
                #region serviceFee
                var serviceFee = await _unitOfWork.ServiceFeePaymentsDueDateRepoEF.GetAllServiceFeePaymentsDueDateByLocationID(locationDetails[i].Id);
                locationDetails[i].ServiceFeePaymentsDueDates = _mapper.Map<List<ServiceFeePaymentsDueDateModel>>(serviceFee);
                #endregion

                #region LocationImage
                var locationImage = await _unitOfWork.LocationImageRepoEF.GetAllLocationImageByLocationID(locationDetails[i].Id);
                locationDetails[i].LocationImages = _mapper.Map<List<LocationImageModel>>(locationImage);
                #endregion

                #region LocationContact
                var locationContact = await _unitOfWork.LocationContactRepoEF.GetAllLocationContactByLocationID(locationDetails[i].Id);
                locationDetails[i].LocationContacts = _mapper.Map<List<LocationContactModel>>(locationContact);
                #endregion

                #region LocationCurrency
                var locationCurrency = await _unitOfWork.LocationCurrencyRepoEF.GetByLocationID(locationDetails[i].Id);
                locationDetails[i].LocationCurrencies = _mapper.Map<List<LocationCurrencyModel>>(locationCurrency);
                #endregion

                #region LocationFile
                var locationFile = await _unitOfWork.LocationFileRepoEF.GetAllLocationFileByLocationID(locationDetails[i].Id);
                locationDetails[i].LocationFiles = _mapper.Map<List<LocationFileModel>>(locationFile);
                #endregion

                #region LocationWorkingHour
                var locationWorkingHour = await _unitOfWork.LocationWorkingHourRepoEF.GetAllLocationWorkingHourByLocationID(locationDetails[i].Id);
                locationDetails[i].LocationWorkingHours = _mapper.Map<List<LocationWorkingHourModel>>(locationWorkingHour);
                #endregion

                #region LocationBankAccount
                var locationBankAccount = await _unitOfWork.LocationBankAccountRepoEF.GetByLocationID(locationDetails[i].Id);
                locationDetails[i].LocationBankAccount = _mapper.Map<LocationBankAccountModel>(locationBankAccount);
                #endregion

                #region locationInclusion
                var locationInclusion = await _unitOfWork.LocationInclusionRepoEF.GetAllLocationInclusionByLocationID(locationDetails[i].Id);
                locationDetails[i].LocationInclusions = _mapper.Map<List<LocationInclusionModel>>(locationInclusion);
                #endregion
            }

            return new Response<List<LocationModel>>(locationDetails);
        }

        public async Task<Response<bool>> UpdateLocationPublishStatus(long LocationId)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var location = await _unitOfWork.LocationRepo.GetByIdAsync(LocationId);
            if (location != null)
            {
                if (location.IsPublish == true)
                {
                    location.IsPublish = false;
                }
                else if (location.IsPublish == false)
                {
                    location.IsPublish = true;
                }
                _unitOfWork.LocationRepo.Update(location);
                if (await _unitOfWork.SaveAsync() < 1)
                {
                    return new Response<bool>("Cannot Updated Location right now");
                }

                if (location.IsPublish == true)
                {
                    return new Response<bool>(true, "Location Published Successfully.");
                }
                else if (location.IsPublish == false)
                {
                    return new Response<bool>(true, "Location UnPublished Successfully.");
                }
            }
            return new Response<bool>("Error while updateting location Publish state.");
        }

        public async Task<Response<List<LocationGetAllModel>>> GetAllUnpublishedLocation()
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var data = await _unitOfWork.LocationRepoEF.GetAllUnpublishedLocation();
            for (int i = 0; i < data.Count; i++)
            {
                #region District
                var District = await _unitOfWork.DistrictRepo.GetByIdAsync(data[i].District.Id);
                data[i].District = new DropdownViewModel
                {
                    Id = District.Id,
                    Name = District.DistrictName
                };
                #endregion

                #region City
                var City = await _unitOfWork.CityRepo.GetByIdAsync(District.CityId);
                data[i].City = new DropdownViewModel
                {
                    Id = City.Id,
                    Name = City.CityName
                };
                #endregion

                #region LocationType
                var LocationType = await _unitOfWork.LocationTypeRepo.GetByIdAsync(data[i].LocationType.Id);
                data[i].LocationType = new DropdownViewModel
                {
                    Id = LocationType.Id,
                    Name = LocationType.Name
                };
                #endregion
            }

            if (data.Count == 0)
            {
                return new Response<List<LocationGetAllModel>>(null, "Not Data Found");
            }
            return new Response<List<LocationGetAllModel>>(data);
        }

        public async Task<PagedResponse<List<LocationGetAllModel>>> GetAllPublishedAndUnpublishedLocation(RequestParameter filter)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var data = await _unitOfWork.LocationRepoEF.GetAllPublishedAndUnpublishedLocation(filter);
            int pg_total = await _unitOfWork.LocationRepo.GetCountAsync(x => x.IsDeleted == false);
            for (int i = 0; i < data.Count; i++)
            {
                #region District
                var District = await _unitOfWork.DistrictRepo.GetByIdAsync(data[i].District.Id);
                data[i].District = new DropdownViewModel
                {
                    Id = District.Id,
                    Name = District.DistrictName
                };
                #endregion

                #region City
                var City = await _unitOfWork.CityRepo.GetByIdAsync(District.CityId);
                data[i].City = new DropdownViewModel
                {
                    Id = City.Id,
                    Name = City.CityName
                };
                #endregion

                #region LocationType
                var LocationType = await _unitOfWork.LocationTypeRepo.GetByIdAsync(data[i].LocationType.Id);
                data[i].LocationType = new DropdownViewModel
                {
                    Id = LocationType.Id,
                    Name = LocationType.Name
                };
                #endregion
            }

            if (data.Count == 0)
            {
                return new PagedResponse<List<LocationGetAllModel>>(null, filter.PageNumber, filter.PageSize, pg_total);
            }
            return new PagedResponse<List<LocationGetAllModel>>(data, filter.PageNumber, filter.PageSize, pg_total);
        }

        public async Task<Response<List<LocationGetAllModel>>> GetAllPublishedAndUnpublishedLocationWithoutPagination()
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            var data = await _unitOfWork.LocationRepoEF.GetAllPublishedAndUnpublishedLocationWithoutPagination();
            for (int i = 0; i < data.Count; i++)
            {
                #region District
                var District = await _unitOfWork.DistrictRepo.GetByIdAsync(data[i].District.Id);
                data[i].District = new DropdownViewModel
                {
                    Id = District.Id,
                    Name = District.DistrictName
                };
                #endregion

                #region City
                var City = await _unitOfWork.CityRepo.GetByIdAsync(District.CityId);
                data[i].City = new DropdownViewModel
                {
                    Id = City.Id,
                    Name = City.CityName
                };
                #endregion

                #region LocationType
                var LocationType = await _unitOfWork.LocationTypeRepo.GetByIdAsync(data[i].LocationType.Id);
                data[i].LocationType = new DropdownViewModel
                {
                    Id = LocationType.Id,
                    Name = LocationType.Name
                };
                #endregion
            }

            if (data.Count == 0)
            {
                return new Response<List<LocationGetAllModel>>(null, "No Data Found.");
            }
            return new Response<List<LocationGetAllModel>>(data);
        }

        public async Task<PagedResponse<List<LocationGetAllModel>>> GetAllPublishedAndUnpublishedLocationFilter(RequestGetAllLocationParameter filter)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@Id", filter.Id);
            parms.Add("@CityId", filter.CityId);
            parms.Add("@DistrictId", filter.DistrictId);
            parms.Add("@LocationTypeId", filter.LocationTypeId);
            parms.Add("@ContractLength", filter.ContractLength);
            parms.Add("@FromContractStartDate", filter.FromContractStartDate);
            parms.Add("@ToContractStartDate", filter.ToContractStartDate);
            parms.Add("@FromLaunchDate", filter.FromLaunchDate);
            parms.Add("@ToLaunchDate", filter.ToLaunchDate);
            parms.Add("@FromNetArea", filter.FromNetArea);
            parms.Add("@ToNetArea", filter.ToNetArea);
            parms.Add("@pageNumber", filter.PageNumber);
            parms.Add("@pageSize", filter.PageSize);
            var data = await _unitOfWork.LocationRepo.QueryAsync<LocationGetAllFilterModel>("[dbo].[SP_Location_GetAll_Filter_Pagination]", parms, System.Data.CommandType.StoredProcedure);

            var resultData = new List<LocationGetAllModel>();
            foreach (var item in data)
            {
                resultData.Add(new LocationGetAllModel
                {
                    City = new DropdownViewModel
                    {
                        Id = item.CityId,
                        Name = item.CityName
                    },
                    Name = item.LocationName,
                    Id = item.Id,
                    ContractLength = item.ContractLength,
                    ContractStartDate = item.ContractStartDate,
                    GrossArea = item.GrossArea,
                    InstallAccessPoint = item.InstallAccessPoint,
                    LaunchDate = item.LaunchDate,
                    IsPublish = item.IsPublish,
                    NetArea = item.NetArea,
                    District = new DropdownViewModel
                    {
                        Id = item.DistrictId,
                        Name = item.DistrictName
                    },
                    LocationType = new DropdownViewModel
                    {
                        Id = item.LocationTypeId,
                        Name = item.LocationTypeName
                    }
                });
            }
            if (resultData.Count == 0)
            {
                return new PagedResponse<List<LocationGetAllModel>>(null, filter.PageNumber, filter.PageSize, data[0].pgTotal);
            }
            return new PagedResponse<List<LocationGetAllModel>>(resultData, filter.PageNumber, filter.PageSize, data[0].pgTotal);
        }

        public async Task<Response<List<LocationGetAllModel>>> GetAllPublishedAndUnpublishedLocationFilterWithoutPagination(RequestGetAllLocationWithoutPaginationParameter filter)
        {
            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            DynamicParameters parms = new DynamicParameters();
            parms.Add("@Id", filter.Id);
            parms.Add("@CityId", filter.CityId);
            parms.Add("@DistrictId", filter.DistrictId);
            parms.Add("@LocationTypeId", filter.LocationTypeId);
            parms.Add("@ContractLength", filter.ContractLength);
            parms.Add("@FromContractStartDate", filter.FromContractStartDate);
            parms.Add("@ToContractStartDate", filter.ToContractStartDate);
            parms.Add("@FromLaunchDate", filter.FromLaunchDate);
            parms.Add("@ToLaunchDate", filter.ToLaunchDate);
            parms.Add("@FromNetArea", filter.FromNetArea);
            parms.Add("@ToNetArea", filter.ToNetArea);

            var data = await _unitOfWork.LocationRepo.QueryAsync<LocationGetAllFilterModel>("[dbo].[SP_Location_GetAll_Filter_WithoutPagination]", parms, System.Data.CommandType.StoredProcedure);
            
            var resultData = new List<LocationGetAllModel>();
            foreach (var item in data)
            {
                resultData.Add(new LocationGetAllModel
                {
                    City = new DropdownViewModel
                    {
                        Id = item.CityId,
                        Name = item.CityName
                    },
                    Name = item.LocationName,
                    Id = item.Id,
                    ContractLength = item.ContractLength,
                    ContractStartDate = item.ContractStartDate,
                    GrossArea = item.GrossArea,
                    InstallAccessPoint = item.InstallAccessPoint,
                    LaunchDate = item.LaunchDate,
                    IsPublish = item.IsPublish,
                    NetArea = item.NetArea,
                    District = new DropdownViewModel
                    {
                        Id = item.DistrictId,
                        Name = item.DistrictName
                    },
                    LocationType = new DropdownViewModel
                    {
                        Id = item.LocationTypeId,
                        Name = item.LocationTypeName
                    }
                });
            }
            if (resultData.Count == 0)
            {
                return new Response<List<LocationGetAllModel>>(null, "Not Data Found");
            }
            return new Response<List<LocationGetAllModel>>(resultData);
        }
    }
}
