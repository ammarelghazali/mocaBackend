using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MOCA.Core;
using MOCA.Core.DTOs.MeetingReservations.Request;
using MOCA.Core.DTOs.MeetingReservations.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.DTOs.Shared.ThirdParty.Email;
using MOCA.Core.Entities.MeetingSpaceReservation;
using MOCA.Core.Entities.Shared.Reservations;
using MOCA.Core.Entities.SSO;
using MOCA.Core.Interfaces.MeetingSpaceReservations.Services;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Interfaces.Shared.Services.ThirdParty.Email;
using MOCA.Core.Settings;
using MOCA.Services.Implementation.MeetingSpaceReservations.Helpers;

namespace MOCA.Services.Implementation.MeetingSpaceReservations
{
    public class MeetingSpaceREservationsServices : IMeetingSpaceReservationsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IMapper _mapper;
        private readonly IGetEmailBodyForBookingMeetingSpace _getEmailBodyForBookingMeetingSpace;
        private readonly MailSettings _mailSettings;
        private readonly IEmailService _emailService;
        public MeetingSpaceREservationsServices(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUserService,
            IMapper mapper, IGetEmailBodyForBookingMeetingSpace getEmailBodyForBookingMeetingSpace,
            IOptions<MailSettings> mailSettings, IEmailService emailService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _authenticatedUserService = authenticatedUserService ?? throw new ArgumentNullException(nameof(authenticatedUserService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _getEmailBodyForBookingMeetingSpace = getEmailBodyForBookingMeetingSpace ?? throw new ArgumentNullException(nameof(getEmailBodyForBookingMeetingSpace));
            _mailSettings = mailSettings.Value;
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        #region CRM
        public async Task<PagedResponse<List<MeetingReservationResponseDto>>> GetAllSubmissionsWithPagination(int pageNumber, int pageSize)
        {
            pageSize = pageSize > 0 ? pageSize : 10;
            pageNumber = pageNumber > 0 ? pageNumber : 1;
            var allSubmissions = await _unitOfWork.MeetingSpaceReservationRepository.GetAllSubmissions();
            var submissions = await allSubmissions.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedResponse<List<MeetingReservationResponseDto>>(submissions, pageNumber, pageSize, submissions.Count);

        }

        public async Task<Response<List<MeetingReservationResponseDto>>> GetAllSubmissionsWithoutPagination()
        {
            var allSubmissions = await _unitOfWork.MeetingSpaceReservationRepository.GetAllSubmissions();
            var submissions = await allSubmissions.ToListAsync();
            return new Response<List<MeetingReservationResponseDto>>(submissions);
        }

        public async Task<Response<List<MeetingReservationLocationsDto>>> GetAllMeetingReservationLocations()
        {
            var locations = await _unitOfWork.MeetingSpaceReservationRepository.GetAllDistinctLocations();
            return new Response<List<MeetingReservationLocationsDto>>(locations);
        }

        public async Task<Response<MeetingReservationResponseDto>> GetMeetingReservationById(long id)
        {
            var meetingReservation = await _unitOfWork.MeetingSpaceReservationRepository.GetMeetingReservationById(id);
            return new Response<MeetingReservationResponseDto>(meetingReservation);
        }

        public async Task<PagedResponse<List<MeetingReservationResponseDto>>> GetAllMeetingReservationsWithFilter(
            GetAllMeetingReservationsWithFilterRequestDto dto)
        {
            dto.PageSize = dto.PageSize <= 0 ? 10 : dto.PageSize;
            var meetingReservation = await _unitOfWork.MeetingSpaceReservationRepository.GetAllSubmissionsWithFilter(dto);
            var allFilterdData = await meetingReservation.Take(dto.PageSize).ToListAsync();
            return new PagedResponse<List<MeetingReservationResponseDto>>(allFilterdData, 1, dto.PageSize, allFilterdData.Count);
        }
        #endregion


        #region Mobile
        
        public async Task<Response<bool>> BookMeetingReservation(BookMeetingReservationRequestDto dto)
        {

            #region notes:
            /*
                needed validations :
                1. user is authorized
                2. userId !null in DB
                2. num Of Attendees > 0
                3. MeetingPriceId !null in DB
                4. MeetingSpaceId !null in DB
                5. locationId !null in DB
                6. Date is not weekend for this MeetingSpace
                7. Time is between working hours of MeetingSpace
                8. NumOfHourse in MeetingPrice <= closing hour of this meetingSpace
                9. Occupancy of this MeetingSpace > Num Of Attendees
                10. No other meeting reservations at the same time
                11. validate meetingSpaceId in the right LocationId ?? 
             */
            #endregion

            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                return new Response<bool>("User is not authorized");
            }

            var user = await _unitOfWork.BasicUserRepository.GetByIdAsync(long.Parse(_authenticatedUserService.UserId));
            if (user == null)
            {
                return new Response<bool>("user is not found!");
            }

            var meetingPrice = await _unitOfWork.MeetingSpaceHourlyPricingRepository.GetByIdAsync(dto.MeetingSpaceHourlyPricingId);
            if(meetingPrice == null)
            {
                return new Response<bool>("Meeting price is not found!");
            }

            var meetingSpace = await _unitOfWork.MeetingSpaceRepository.GetByIdAsync(dto.MeetingSpaceId);
            if (meetingSpace == null)
            {
                return new Response<bool>("Meeting space is not found!");
            }

            if (dto.NumOfAttendees > meetingSpace.MaximumOccupancy)
            {
                return new Response<bool>("Number of attendees is greater than meeting space occupancy!");
            }

            var location = await _unitOfWork.LocationRepo.GetByIdAsync(dto.LocationId);
            if (location == null)
            {
                return new Response<bool>("Location is not found!");
            }

            // validate meetings at the ame time for the the same meeting space 
            var meetingReservations = await _unitOfWork.MeetingSpaceReservationRepository.GetMeetingsWithinPeriodOfTime(
                dto.DateAndTime, dto.DateAndTime.AddHours(meetingPrice.Hours), dto.MeetingSpaceId); // parse to double?
            if(meetingReservations > 0)
            {
                return new Response<bool>("Meeting space is already occupied at the same time!");
            }

            // validate working hours of location
            var locationWorkingHours = await _unitOfWork.LocationWorkingHourRepoEF.GetAllLocationWorkingHourByLocationID(location.Id);
            if(locationWorkingHours == null)
            {
                return new Response<bool>("This Location doesn't have working hours!");
            }

            /*
                validate working hours
            */

            try
            {
                var meetingReservation = new MeetingReservation
                {
                    BasicUserId = user.Id,
                    DateAndTime = dto.DateAndTime,
                    NumOfAttendees = dto.NumOfAttendees,
                    MeetingSpaceId = dto.MeetingSpaceId,
                    LocationId = dto.LocationId,
                    MeetingSpaceHourlyPricingId = dto.MeetingSpaceHourlyPricingId
                };

                var addedMeetingReservation = await _unitOfWork.MeetingSpaceReservationRepository.AddAsync(meetingReservation);

                var reservationType = await _unitOfWork.ReservationTypesRepository.GetAll().Where(x => x.Name == "MeetingSpace").ToListAsync();
                if (reservationType == null)
                {
                    return new Response<bool>("Meeting space type reservation not found in reservation types!");
                }

                var reservationTransacttion = new ReservationTransaction
                {
                    ReservationTargetId = addedMeetingReservation.Id,
                    BasicUserId = user.Id,
                    LocationId = location.Id,
                    ReservationTypeId = reservationType[0].Id,
                    TotalHours = meetingPrice.Hours,
                    RemainingHours = meetingPrice.Hours,
                    ExtendExpiryDate = addedMeetingReservation.DateAndTime.AddHours(meetingPrice.Hours)
                };

                var addedReservationTransaction = await _unitOfWork.ReservationTransactionRepository.AddAsync(reservationTransacttion);

                var meetingReservationTransaction = new MeetingReservationTransaction
                {
                    MeetingReservationId = addedMeetingReservation.Id,
                    ReservationTransactionId = addedReservationTransaction.Id
                };

                var addedMeetingReservationTransaction = await _unitOfWork.MeetingReservationTransactionRepository.AddAsync(meetingReservationTransaction);

                // Send Email
                var emailRequest = new EmailRequest();
                emailRequest = new EmailRequest()
                {
                    Body = _getEmailBodyForBookingMeetingSpace.GetEmailBody(location, 
                    addedMeetingReservation, meetingSpace, user, meetingPrice),
                    To = _mailSettings.ContactUsMoca,
                    Subject = $"New moca Meeting Room Booking @ {location.Name}",
                };
                await _emailService.SendAsync(emailRequest, true);

                return new Response<bool>(true, "Meeting is reserved successfully :D");
            }
            catch(Exception ex)
            {
                return new Response<bool>("UnExpected error happened");
            }
        }

        public async Task<Response<bool>> AddAttendees(List<MeetingAttendeeDto> dto)
        {
            try
            {
                var attendees = _mapper.Map<List<MeetingAttendee>>(dto);
                var addedAttendees = await _unitOfWork.MeetingAttendeesRepository.AddRangeAsync(attendees);
                return new Response<bool>(true, "Attendees added successfully :D");
            }
            catch(Exception ex)
            {
                return new Response<bool>("Unexpected error happened!");
            }

        }

        public async Task<Response<bool>> UpdatePaymentMethod(long meetingReservationId, long paymentMethodId)
        {
            if (await _unitOfWork.PaymentMethodRepository.GetByIdAsync(paymentMethodId) == null)
            {
                return new Response<bool>("Payment method not found");
            }

            var meetingReservation = await _unitOfWork.MeetingSpaceReservationRepository.GetByIdAsync(meetingReservationId);
            if(meetingReservation == null)
            {
                return new Response<bool>("Meeting reservation not found");
            }


            try
            {
                meetingReservation.PaymentMethodId = paymentMethodId;
                _unitOfWork.MeetingSpaceReservationRepository.Update(meetingReservation);
                return new Response<bool>(true, "Updated Successfully :D");
            }
            catch(Exception ex)
            {
                return new Response<bool>("Unexpected error happened!");
            }

        }

        public async Task<Response<List<OccupiedTimesDto>>> GetAllOccupiedTimeInDay(DateTime Day, long meetingSpaceId)
        {
            var meetingSpace = await _unitOfWork.MeetingSpaceRepository.GetByIdAsync(meetingSpaceId);
            if (meetingSpace == null)
            {
                return new Response<List<OccupiedTimesDto>>("meeting space not found!");
            }
            // validate string day
            var allMeetingTimesInDay = await _unitOfWork.MeetingSpaceReservationRepository.GetMeetingsInDay(Day, meetingSpaceId);
            return new Response<List<OccupiedTimesDto>>(allMeetingTimesInDay);
        }

        #endregion
    }
}
