using AutoMapper;
using MOCA.Core;
using MOCA.Core.DTOs.Events.BookEventSpaceDtos.Request;
using MOCA.Core.DTOs.Events.BookEventSpaceDtos.Response;
using MOCA.Core.DTOs.Events.EventAttendanceDtos.Response;
using MOCA.Core.DTOs.Events.EventCategoryDtos.Response;
using MOCA.Core.DTOs.Events.EventOpportunityDtos.Response;
using MOCA.Core.DTOs.Events.EventReccuranceDtos.Response;
using MOCA.Core.DTOs.Events.EventTypeDtos.Response;
using MOCA.Core.DTOs.Events.Response;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.Shared.Exceptions;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Events.Services;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Interfaces.Shared.Services.ThirdParty.Email;
using MOCA.Services.Implementation.Events.Helpers;

namespace MOCA.Services.Implementation.Events
{
    public class BookEventSpaceService : IBookEventSpaceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IPasswordEncoderDecoder _decoder;
        private readonly IDateTimeService _dateTimeService;
        private readonly IGetEmailBody _getEmailBody;
        public BookEventSpaceService(IUnitOfWork unitOfWork,
                                     IAuthenticatedUserService authenticatedUser,
                                     IMapper mapper,
                                     IEmailService emailService,
                                     IPasswordEncoderDecoder decoder,
                                     IDateTimeService dateTimeService,
                                     IGetEmailBody getEmailBody)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _authenticatedUser = authenticatedUser ?? throw new ArgumentNullException(nameof(authenticatedUser));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService;
            _decoder = decoder ?? throw new ArgumentNullException(nameof(decoder));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _getEmailBody = getEmailBody ?? throw new ArgumentNullException(nameof(getEmailBody));
        }

        public async Task<Response<long>> EventSpaceBooking(BooEventSpaceDto request)
        {
            if (string.IsNullOrWhiteSpace(request.CreatedBy))
            {
                if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
                {
                    throw new UnauthorizedAccessException("User is not authorized");
                }
                else
                {
                    request.CreatedBy = _authenticatedUser.UserId;
                    bool isNumeric = int.TryParse(_authenticatedUser.UserId, out _);
                    if (!isNumeric)
                    {
                        request.IdentityUserId = _authenticatedUser.UserId;
                    }
                }
            }

            if (request.CreatedAt == null || request.CreatedAt == default)
            {
                request.CreatedAt = _dateTimeService.NowUtc;
            }

            request.SubmissionDate = _dateTimeService.NowUtc;

            if (request.IndustryNameId == 0)
            {
                request.IndustryNameId = null;
            }

            if (request.EventCategoryId == 0)
            {
                request.EventCategoryId = null;
            }

            var location = await _unitOfWork.LocationsMemberShipsRepo.GetLocationByID(request.LocationNameId.GetValueOrDefault());
            if (request.LobLocationTypeId == 0 || request.LobLocationTypeId == null)
            {
                if (location != null)
                    request.LobLocationTypeId = await _unitOfWork.LocationsMemberShipsRepo.GetLocationTypeByID(request.LocationNameId.GetValueOrDefault());
                else
                    return new Response<long>("location not found.");
            }

            if (request.ContactMobile1 == request.ContactMobile2)
                return new Response<long>("contact moblie should be different.");

            if (request.ContactEmail1 == request.ContactEmail2)
                return new Response<long>("Contact email should be different.");

            if (request.ExpectedNoAttend < 1)
                return new Response<long>("number of attendence must be greater than 0.");

            if (await _unitOfWork.EventAttendanceRepo.GetByIdAsync(request.EventAttendanceId) == null)
                return new Response<long>("Event attendence must be added.");
            
            var eventRequester = _unitOfWork.EventRequesterRepo.GetByID(request.EventRequesterId);
            if (eventRequester == null)
              return new Response<long>("Event requester must be added.");

            if (await _unitOfWork.EventReccuranceRepo.GetByIdAsync(request.EventReccuranceId) == null)
               return new Response<long>("Event reccurance must be added.");

            var eventType = _unitOfWork.EventTypeRepo.GetByID(request.EventTypeId);
            if (eventType == null)
              return new Response<long>("Event type must be added.");

            if (await _unitOfWork.InitiatedRepo.GetByIdAsync(request.InitiatedId) == null)
            return new Response<long>("Intiated must be added.");

            var eventCategory = _unitOfWork.EventCategoryRepo.GetByID(request.EventCategoryId.GetValueOrDefault());
            if (eventCategory == null)
                return new Response<long>("Event category must be added.");

            var eventIndustry = await _unitOfWork.IndustryRepo.GetByIdAsync(request.IndustryNameId.GetValueOrDefault());
            if (_unitOfWork.EventRequesterRepo.GetByIdAsync(request.EventRequesterId).Result.Name == "Company")
            {
                if (string.IsNullOrWhiteSpace(request.CompanyCommericalName))
                    return new Response<long>("Company name must be added.");

                if (request.IndustryNameId == null || eventIndustry == null)
                    return new Response<long>("Industry must be added.");
            }

            try
            {
                var eventSpace = _mapper.Map<EventSpaceBooking>(request);
                await _unitOfWork.EventSpaceBookingRepo.AddAsync(eventSpace);

                if (request.EventSpaceVenues.Count() > 0)
                {
                    var EventSpaceVenus = new List<EventSpaceVenues>();
                    foreach (var item in request.EventSpaceVenues)
                    {
                        var EventSpaceVenu = new EventSpaceVenues();
                        EventSpaceVenu.EventSpaceBookingId = eventSpace.Id;
                        EventSpaceVenu.VenueName = item.VenueName;
                        EventSpaceVenus.Add(EventSpaceVenu);
                    }
                    await _unitOfWork.EventSpaceVenuesRepo.AddEventSpaceVenues(EventSpaceVenus);
                }

                if (request.EventSpaceTimes.Count() > 0)
                {
                    var times = new List<EventSpaceTime>();
                    foreach (var item in request.EventSpaceTimes)
                    {
                        if (item.RecurrenceStartDate == null)
                        {
                            return new Response<long>(0, "you must assign start date for this event space booking");
                        }

                        var EventSpaceTime = new EventSpaceTime();
                        EventSpaceTime.EventSpaceBookingId = eventSpace.Id;
                        EventSpaceTime.RecurrenceStartDate = item.RecurrenceStartDate;
                        EventSpaceTime.RecurrenceEndDate = item.RecurrenceEndDate;
                        EventSpaceTime.RecurrenceStartTime = item.RecurrenceStartTime.ToString("HH:mm:ss");
                        EventSpaceTime.RecurrenceEndTime = item.RecurrenceEndTime.ToString("HH:mm:ss");
                        EventSpaceTime.RecurrenceDay = item.RecurrenceDay;
                        times.Add(EventSpaceTime);
                    }
                    await _unitOfWork.EventSpaceTimesRepo.BookingEventSpaceTime(times);
                }

                var eventTimes = await _unitOfWork.EventSpaceTimesRepo.GetBookedEventSpaceTimeById(eventSpace.Id);
                var venueName = await _unitOfWork.EventSpaceVenuesRepo.GetEventSpaceVenuesById(eventSpace.Id);

                var emailRequest = await _getEmailBody.GetEmailRequest(location, eventRequester, request, eventIndustry,
                                                                         eventCategory, eventTimes, eventType, venueName);
                await _emailService.SendAsync(emailRequest, true);

                return new Response<long>(1000);
            }
            catch (Exception ex)
            {
                return new Response<long>(1, ex.Message);
                throw new ExistsBeforeException("Event space was added before.");
            }

        }




        public async Task<PagedResponse<IReadOnlyList<GetAllBookedEventSpaceResponseDto>>> GetAllBookedEventSpaceTypeAsync(GetAllBookedEventSpaceByType_Query request)
        {
            List<GetAllBookedEventSpaceResponseDto> GetAllBookedEventSpaceResponseDtos = new List<GetAllBookedEventSpaceResponseDto>();

            var eventlocation = await _unitOfWork.EventSpaceBookingRepo.GetBookedEventSpaceByIdAsync(request.TypeLocation, request.pageNumber, request.pageSize);
            int count = await _unitOfWork.EventSpaceBookingRepo.CountGetBookedEventSpaceById(request.TypeLocation);

            foreach (var eventSpace in eventlocation)
            {
                GetAllBookedEventSpaceResponseDto GetAllBookedEventSpaceResponseDto = new GetAllBookedEventSpaceResponseDto();

                GetAllBookedEventSpaceResponseDto = _mapper.Map<GetAllBookedEventSpaceResponseDto>(eventSpace);
                GetAllBookedEventSpaceResponseDto.EventRequester = null;
                GetAllBookedEventSpaceResponseDto.EventOpportunityStatus = null;
                GetAllBookedEventSpaceResponseDto.EventReccurance = null;
                GetAllBookedEventSpaceResponseDto.EventCategory = null;
                GetAllBookedEventSpaceResponseDto.EventAttendance = null;
                GetAllBookedEventSpaceResponseDto.OpportunityStage = null;
                GetAllBookedEventSpaceResponseDto.Initiated = null;
                GetAllBookedEventSpaceResponseDto.EventOpportunityStatus = null;
                GetAllBookedEventSpaceResponseDto.EventType = null;


                if (eventSpace.EventSpaceTimes.Count > 0)
                {
                    GetAllBookedEventSpaceResponseDto.eventSpaceTimes = _mapper.Map<List<EventSpace_TimeDto>>(eventSpace.EventSpaceTimes);
                }


                if (eventSpace.EventSpaceVenues.Count > 0)
                {
                    GetAllBookedEventSpaceResponseDto.eventSpaceVenues = _mapper.Map<List<EventSpace_VenuesDto>>(eventSpace.EventSpaceVenues);
                }

                GetAllBookedEventSpaceResponseDto.EventRequester = _mapper.Map<EventRequesterDto>(eventSpace.EventRequester);
                GetAllBookedEventSpaceResponseDto.EventCategory = _mapper.Map<EventCategoryDto>(eventSpace.EventCategory);
                GetAllBookedEventSpaceResponseDto.EventType = _mapper.Map<EventTypeDto>(eventSpace.EventType);

                var eventIndustry = await _unitOfWork.IndustryRepo.GetByIdAsync(eventSpace.IndustryNameId);
                if (eventIndustry != null)
                {
                    GetAllBookedEventSpaceResponseDto.Industry = new IndustryViewModel { Id = eventIndustry.Id, IndustryName = eventIndustry.Name };
                }

                GetAllBookedEventSpaceResponseDto.EventReccurance = _mapper.Map<EventRecurrenceDto>(eventSpace.EventReccurance);
                GetAllBookedEventSpaceResponseDto.EventAttendance = _mapper.Map<EventAttendanceDto>(eventSpace.EventAttendance);
                GetAllBookedEventSpaceResponseDto.Initiated = _mapper.Map<InitiatedDto>(eventSpace.Initiated);
                GetAllBookedEventSpaceResponseDto.OpportunityStage = _mapper.Map<OpportunityStageDto>(eventSpace.OpportunityStage);
                GetAllBookedEventSpaceResponseDto.EventOpportunityStatus = _mapper.Map<EventOpportunityStatusDto>(eventSpace.EventOpportunityStatus);

                Location locationData = new Location();
                if (eventSpace.LocationNameId != null)
                {
                    locationData = await _unitOfWork.LocationsMemberShipsRepo.GetLocationByID(eventSpace.LocationNameId.Value);
                    GetAllBookedEventSpaceResponseDto.Location = new LocationViewModel { Id = locationData.Id, LocationName = locationData.Name };
                }

                GetAllBookedEventSpaceResponseDto.OtherEventCategory = eventSpace.OtherEventCategory;

                GetAllBookedEventSpaceResponseDtos.Add(GetAllBookedEventSpaceResponseDto);
            }
            if (GetAllBookedEventSpaceResponseDtos.Count > 0)
            {

                for (int i = 0; i < GetAllBookedEventSpaceResponseDtos.Count; i++)
                {
                    if (GetAllBookedEventSpaceResponseDtos[i].IdentityUserId != null)
                    {
                        //var admin = await _unitOfWork.UserService.
                        //                             .GetUserByID(GetAllBookedEventSpaceResponseDtos[i].IdentityUserId);
                        //GetAllBookedEventSpaceResponseDtos[i].IdentityUserId = admin.Data.FirstName + " " + admin.Data.LastName;
                    }
                }
                return new PagedResponse<IReadOnlyList<GetAllBookedEventSpaceResponseDto>>(GetAllBookedEventSpaceResponseDtos, request.pageNumber, request.pageSize, count);
            }


            return new PagedResponse<IReadOnlyList<GetAllBookedEventSpaceResponseDto>>(null, request.pageNumber, request.pageSize);

        }

        public async Task<Response<DropDownsResponseDto>> GetAllDataForDropDowns(GetAllBookedEventSpaceDropDownsDto dto)
        {/*
            if (string.IsNullOrWhiteSpace(_authenticatedUser.UserId))
            {
                throw new UnauthorizedAccessException("User is not authorized");
            }
            */
            var allDropDowns = new DropDownsResponseDto();



            var locationIDs = await _unitOfWork.EventSpaceBookingRepo.GetAllDistinctLocation();
            var loc_data = new List<DropdownViewModel>();
            allDropDowns.EventVenues = new List<DropdownViewModel>();
            allDropDowns.EventAttendances = new List<DropdownViewModel>();
            allDropDowns.EventReccurances = new List<DropdownViewModel>();
            allDropDowns.Locations = new List<DropdownViewModel>();
            allDropDowns.EventIndusties = new List<DropdownViewModel>();
            allDropDowns.EventCategories = new List<DropdownViewModel>();
            allDropDowns.EventRequesters = new List<DropdownViewModel>();
            allDropDowns.EventTypes = new List<DropdownViewModel>();
            allDropDowns.Initiated = new List<DropdownViewModel>();

            foreach (var id in locationIDs)
            {
                var location = await _unitOfWork.LocationsMemberShipsRepo.GetLocationByIDAndLocType(id, dto.LocTypeId);
                if (location != null)
                {
                    loc_data.Add(new DropdownViewModel { Id = id, Name = location.Name });
                    foreach (var item in loc_data)
                    {
                        //// hna htb3t location id item.Id get all booking eventspace
                        var eventSpaces = await _unitOfWork.EventSpaceBookingRepo.GetAllBookedEventSpaceByLocationId(item.Id);
                        ///get array of eventspace 
                        ///loop {id bta3 booking eventspace send}
                        foreach (var eventSpace in eventSpaces)
                        {
                            var dataVenu = await _unitOfWork.EventSpaceVenuesRepo.GetAllDistinctVenue(eventSpace.Id);
                            if (dataVenu != null)
                            {
                                allDropDowns.EventVenues.AddRange(dataVenu);
                            }

                        }

                    }
                    // location.eventspaces.include(venues, industry, category, recurrence, typeId, requester, attendence, intiated)
                    var IndustyIDs = await _unitOfWork.EventSpaceBookingRepo.GetAllDistinctIndusty(id);
                    var ind_data = new List<DropdownViewModel>();
                    foreach (var ids in IndustyIDs)
                    {
                        var industy = await _unitOfWork.IndustryRepo.GetByIdAsync(ids);
                        ind_data.Add(new DropdownViewModel { Id = industy.Id, Name = industy.Name });
                    }
                    allDropDowns.EventIndusties.AddRange(ind_data);

                    var CategoryIDs = await _unitOfWork.EventSpaceBookingRepo.GetAllDistinctCategory(id);
                    if (CategoryIDs != null)
                    {
                        var cat_data = new List<DropdownViewModel>();
                        foreach (var ids in CategoryIDs)
                        {
                            var category = _unitOfWork.EventCategoryRepo.GetByID(ids);
                            cat_data.Add(new DropdownViewModel { Id = category.Id, Name = category.Name });
                        }
                        allDropDowns.EventCategories.AddRange(cat_data);
                    }

                    var ReccuranceIDs = await _unitOfWork.EventSpaceBookingRepo.GetAllDistinctReccurance(id);
                    var rec_data = new List<DropdownViewModel>();
                    foreach (var ids in ReccuranceIDs)
                    {
                        var reccurance = _unitOfWork.EventReccuranceRepo.GetByID(ids);
                        rec_data.Add(new DropdownViewModel { Id = reccurance.Id, Name = reccurance.Name });
                    }
                    allDropDowns.EventReccurances.AddRange(rec_data);

                    var TypeIDs = await _unitOfWork.EventSpaceBookingRepo.GetAllDistinctType(id);
                    var typ_data = new List<DropdownViewModel>();
                    foreach (var ids in TypeIDs)
                    {
                        var type = _unitOfWork.EventTypeRepo.GetByID(ids);
                        typ_data.Add(new DropdownViewModel { Id = type.Id, Name = type.Name });
                    }
                    allDropDowns.EventTypes.AddRange(typ_data);

                    var AttendanceIDs = await _unitOfWork.EventSpaceBookingRepo.GetAllDistinctAttendance(id);
                    var att_data = new List<DropdownViewModel>();
                    foreach (var ids in AttendanceIDs)
                    {
                        var attendance = _unitOfWork.EventAttendanceRepo.GetByID(ids);
                        att_data.Add(new DropdownViewModel { Id = attendance.Id, Name = attendance.Name });
                    }
                    allDropDowns.EventAttendances.AddRange(att_data);
                }
            }
            var venus = allDropDowns.EventVenues.Select(x => x.Name).Distinct();
            allDropDowns.EventVenues = new List<DropdownViewModel>();
            foreach (var venue in venus)
            {
                DropdownViewModel dropdownViewModel = new DropdownViewModel();
                dropdownViewModel.Name = venue;
                allDropDowns.EventVenues.Add(dropdownViewModel);
            }
            allDropDowns.Locations = loc_data;

            var RequesterIDs = await _unitOfWork.EventSpaceBookingRepo.GetAllDistinctRequester(dto.LocTypeId);
            var req_data = new List<DropdownViewModel>();
            foreach (var ids in RequesterIDs)
            {
                var requester = _unitOfWork.EventRequesterRepo.GetByID(ids);
                req_data.Add(new DropdownViewModel { Id = requester.Id, Name = requester.Name });
            }
            allDropDowns.EventRequesters.AddRange(req_data);

            var InitiatedIDs = await _unitOfWork.EventSpaceBookingRepo.GetAllDistinctInitiated();
            var ini_data = new List<DropdownViewModel>();
            foreach (var ids in InitiatedIDs)
            {
                var initiated = await _unitOfWork.InitiatedRepo.GetByIdAsync(ids);
                ini_data.Add(new DropdownViewModel { Id = initiated.Id, Name = initiated.Name });
            }
            allDropDowns.Initiated.AddRange(ini_data);

            return new Response<DropDownsResponseDto>(allDropDowns);
        }

    }
}
