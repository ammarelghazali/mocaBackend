using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.LocationManagment.City;
using MOCA.Core.DTOs.LocationManagment.Country;
using MOCA.Core.DTOs.LocationManagment.District;
using MOCA.Core.DTOs.LocationManagment.Location;
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
        public LocationService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IDateTimeService dateTimeService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
        }

        public async Task<Response<long>> AddLocation(LocationModel request)
        {
            var location = _mapper.Map<Location>(request);
            location.CreatedBy = "System";
            /*if (string.IsNullOrWhiteSpace(city.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { city.CreatedBy = authenticatedUserService.UserId; }
            }*/
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
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add Location right now");
            }*/

            #region Add Service Fee Payments Due Date
            var serviceFeePaymentsDueDate = _mapper.Map<List<ServiceFeePaymentsDueDate>>(request.ServiceFeePaymentsDueDates);
            serviceFeePaymentsDueDate.ForEach(c => { c.LocationId = location.Id; });
            _unitOfWork.ServiceFeePaymentsDueDateRepo.InsertRang(serviceFeePaymentsDueDate);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add ServiceFeePaymentsDueDate right now");
            }*/
            #endregion

            #region Add Location Contact
            var locationContact = _mapper.Map<List<LocationContact>>(request.LocationContacts);
            locationContact.ForEach(c => { c.LocationId = location.Id; });
            _unitOfWork.LocationContactRepo.InsertRang(locationContact);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add LocationContact right now");
            }*/
            #endregion

            #region Add Location Image
            var locationImage = _mapper.Map<List<LocationImage>>(request.LocationImages);
            locationImage.ForEach(c => { c.LocationId = location.Id; });
            _unitOfWork.LocationImageRepo.InsertRang(locationImage);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add LocationImage right now");
            }*/
            #endregion

            #region Add Location Currency
            var locationCurrency = _mapper.Map<List<LocationCurrency>>(request.LocationCurrencies);
            locationCurrency.ForEach(c => { c.LocationId = location.Id; });
            _unitOfWork.LocationCurrencyRepo.InsertRang(locationCurrency);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add LocationCurrency right now");
            }*/
            #endregion

            #region Add Location File
            var locationFile = _mapper.Map<List<LocationFile>>(request.LocationFiles);
            locationFile.ForEach(c => { c.LocationId = location.Id; });
            _unitOfWork.LocationFileRepo.InsertRang(locationFile);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add LocationFile right now");
            }*/
            #endregion

            #region Add Location Working Hour
            var locationWorkingHour = _mapper.Map<List<LocationWorkingHour>>(request.LocationWorkingHours);
            locationWorkingHour.ForEach(c => { c.LocationId = location.Id; });
            _unitOfWork.LocationWorkingHourRepo.InsertRang(locationWorkingHour);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add LocationWorkingHour right now");
            }*/
            #endregion

            #region Add Location Bank Account
            var locationBankAccount = _mapper.Map<List<LocationBankAccount>>(request.LocationBankAccount);
            locationBankAccount.ForEach(c => { c.LocationId = location.Id; });
            _unitOfWork.LocationBankAccountRepo.InsertRang(locationBankAccount);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add LocationBankAccount right now");
            }*/
            #endregion

            #region Add Location Inclusion
            var locationInclusion = _mapper.Map<List<LocationInclusion>>(request.LocationInclusions);
            locationInclusion.ForEach(c => { c.LocationId = location.Id; });
            _unitOfWork.LocationInclusionRepo.InsertRang(locationInclusion);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Add LocationInclusion right now");
            }*/
            #endregion

            return new Response<long>(location.Id, "Location Added Successfully.");
        }

        public async Task<Response<long>> UpdateLocation(LocationModel request)
        {
            var location = _mapper.Map<Location>(request);
            location.CreatedBy = "System";
            /*if (string.IsNullOrWhiteSpace(city.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                { city.CreatedBy = authenticatedUserService.UserId; }
            }*/
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

            _unitOfWork.LocationRepo.Update(location);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Updated Location right now");
            }*/

            #region Delete Old Service Fee Payments Due Date And Add New One
            var serviceFeePaymentsDueDate = _mapper.Map<List<ServiceFeePaymentsDueDate>>(request.ServiceFeePaymentsDueDates);
            _unitOfWork.ServiceFeePaymentsDueDateRepoEF.DeleteAllServiceFeePaymentsDueDateByLocationID(request.Id);
            _unitOfWork.ServiceFeePaymentsDueDateRepo.InsertRang(serviceFeePaymentsDueDate);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Update ServiceFeePaymentsDueDate right now");
            }*/
            #endregion

            #region Delete Old Location Contact And Add New One
            var locationContact = _mapper.Map<List<LocationContact>>(request.LocationContacts);
            _unitOfWork.LocationContactRepoEF.DeleteAllLocationContactByLocationID(request.Id);
            _unitOfWork.LocationContactRepo.InsertRang(locationContact);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Update LocationContact right now");
            }*/
            #endregion

            #region Delete Old Location Image And Add New One
            var locationImage = _mapper.Map<List<LocationImage>>(request.LocationImages);
            _unitOfWork.LocationImageRepoEF.DeleteAllLocationImageByLocationID(request.Id);
            _unitOfWork.LocationImageRepo.InsertRang(locationImage);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Update LocationImage right now");
            }*/
            #endregion

            #region Delete Old Location Currency Add New One
            var locationCurrency = _mapper.Map<List<LocationCurrency>>(request.LocationCurrencies);
            _unitOfWork.LocationCurrencyRepoEF.DeleteByLocationID(request.Id);
            _unitOfWork.LocationCurrencyRepo.InsertRang(locationCurrency);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Update LocationCurrency right now");
            }*/
            #endregion

            #region Delete Old Location File Add New One
            var locationFile = _mapper.Map<List<LocationFile>>(request.LocationFiles);
            _unitOfWork.LocationFileRepoEF.DeleteAllLocationFileByLocationID(request.Id);
            _unitOfWork.LocationFileRepo.InsertRang(locationFile);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Update LocationFile right now");
            }*/
            #endregion

            #region Delete Old Location Working Hour Add New One
            var locationWorkingHour = _mapper.Map<List<LocationWorkingHour>>(request.LocationWorkingHours);
            _unitOfWork.LocationWorkingHourRepoEF.DeleteAllLocationWorkingHourByLocationID(request.Id);
            _unitOfWork.LocationWorkingHourRepo.InsertRang(locationWorkingHour);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Update LocationWorkingHour right now");
            }*/
            #endregion

            #region Delete Old Location Bank Account Add New One
            var locationBankAccount = _mapper.Map<List<LocationBankAccount>>(request.LocationBankAccount);
            _unitOfWork.LocationBankAccountRepoEF.DeleteByLocationID(request.Id);
            _unitOfWork.LocationBankAccountRepo.InsertRang(locationBankAccount);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Update LocationBankAccount right now");
            }*/
            #endregion

            #region Delete Old Location Inclusion Add New One
            var locationInclusion = _mapper.Map<List<LocationInclusion>>(request.LocationInclusions);
            _unitOfWork.LocationInclusionRepoEF.DeleteAllLocationInclusionByLocationID(request.Id);
            _unitOfWork.LocationInclusionRepo.InsertRang(locationInclusion);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Update LocationInclusion right now");
            }*/
            #endregion

            return new Response<long>(location.Id, "Location updated Successfully.");
        }

        public async Task<Response<bool>> DeleteLocation(long LocationId)
        {
            /*
            if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
               {
                   throw new UnauthorizedAccessException("User is not authorized");
               }
           */

            #region Delete Service Fee Payments Due Date
            _unitOfWork.ServiceFeePaymentsDueDateRepoEF.DeleteAllServiceFeePaymentsDueDateByLocationID(LocationId);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Delete ServiceFeePaymentsDueDate right now");
            }*/
            #endregion

            #region Delete Location Contact
            _unitOfWork.LocationContactRepoEF.DeleteAllLocationContactByLocationID(LocationId);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Delete LocationContact right now");
            }*/
            #endregion

            #region Delete Location Image
            _unitOfWork.LocationImageRepoEF.DeleteAllLocationImageByLocationID(LocationId);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Delete LocationImage right now");
            }*/
            #endregion

            #region Delete Location Currency
            _unitOfWork.LocationCurrencyRepoEF.DeleteByLocationID(LocationId);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Delete LocationCurrency right now");
            }*/
            #endregion

            #region Delete Location File
            _unitOfWork.LocationFileRepoEF.DeleteAllLocationFileByLocationID(LocationId);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Delete LocationFile right now");
            }*/
            #endregion

            #region Delete Location Working Hour
            _unitOfWork.LocationWorkingHourRepoEF.DeleteAllLocationWorkingHourByLocationID(LocationId);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Delete LocationWorkingHour right now");
            }*/
            #endregion

            #region Delete Location Bank Account
            _unitOfWork.LocationBankAccountRepoEF.DeleteByLocationID(LocationId);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Delete LocationBankAccount right now");
            }*/
            #endregion

            #region Delete Location Inclusion
            _unitOfWork.LocationInclusionRepoEF.DeleteAllLocationInclusionByLocationID(LocationId);
            _unitOfWork.Save();
            /*if (await _unitOfWork.SaveAsync() < 1)
            {
                return new Response<long>("Cannot Delete LocationInclusion right now");
            }*/
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

        public async Task<Response<LocationRegion>> GetRegionsDropDown()
        {
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

            var country = await _unitOfWork.CountryRepo.GetByIdAsync(Cities[0].CountryId);
            CountryModel Country = _mapper.Map<CountryModel>(country);

            LocationRegion locationRegion = new LocationRegion
            {
                Districts = Districts,
                Cities = Cities,
                Country = Country
            };

            return new Response<LocationRegion>(locationRegion);
        }
    }
}
