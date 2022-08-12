using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MOCA.Core;
using MOCA.Core.DTOs.MeetingReservations.Request;
using MOCA.Core.DTOs.MeetingReservations.Response;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.MeetingSpaceReservation;
using MOCA.Core.Entities.Shared.Reservations;
using MOCA.Core.Interfaces.MeetingSpaceReservations.Services;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.MeetingSpaceReservations
{
    public class MeetingSpaceREservationsServices : IMeetingSpaceReservationsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IMapper _mapper;
        public MeetingSpaceREservationsServices(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUserService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _authenticatedUserService = authenticatedUserService ?? throw new ArgumentNullException(nameof(authenticatedUserService));
            _mapper = mapper;
        }

        #region CRM
        public async Task<PagedResponse<List<MeetingReservationResponseDto>>> GetAllSubmissionsWithPagination(int pageNumber, int pageSize)
        {
            pageSize = pageSize > 0 ? pageSize : 10;
            pageNumber = pageNumber > 0 ? pageNumber : 1;
            var allSubmissions =await _unitOfWork.MeetingSpaceReservationRepository.GetAllSubmissions();
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
                -- TODO: Add booking in Transactions Table
             */
            #endregion

            if (string.IsNullOrWhiteSpace(_authenticatedUserService.UserId))
            {
                return new Response<bool>("User is not authorized");
            }

            var user = await _unitOfWork.BasicUserRepository.getFirstBasicUserById(long.Parse(_authenticatedUserService.UserId));
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

            var meetingReservations = await _unitOfWork.MeetingSpaceReservationRepository.GetMeetingsWithinPeriodOfTime(
                dto.DateAndTime, dto.DateAndTime.AddHours(meetingPrice.Hours), dto.MeetingSpaceId); // parse to double?

            if(meetingReservations == false)
            {
                return new Response<bool>("Meeting space is already occupied at the same time!");
            }
            
            // validate working hours of location.

            var meetingReservation = new MeetingReservation
            {
                BasicUserId = user.Id,
                DateAndTime = dto.DateAndTime,
                NumOfAttendees = dto.NumOfAttendees,
                MeetingSpaceId = dto.MeetingSpaceId,
                LocationId = dto.LocationId,
                MeetingSpaceHourlyPricingId = dto.MeetingSpaceHourlyPricingId
            };

            try
            {
                var addedMeetingReservation = await _unitOfWork.MeetingSpaceReservationRepository.AddAsync(meetingReservation);
                
                var reservationTransacttion = new ReservationTransaction
                {
                    ReservationTargetId = addedMeetingReservation.Id,
                    BasicUserId = user.Id,
                    LocationId = location.Id,
                    ReservationTypeId = 1, // must be changed according to the new DB or _uniteOfWork.ReservationTypesRepo.where(x => x.name == "MeetingSpace").select(x => x.Id);
                    TotalHours = meetingPrice.Hours,
                    RemainingHours = meetingPrice.Hours,
                    ExtendExpiryDate = addedMeetingReservation.DateAndTime.AddHours(meetingPrice.Hours)
                };

                var addedReservationTransaction = await _unitOfWork.ReservationTransactionRepository.AddAsync(reservationTransacttion);
                
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

        public async Task<Response<List<OccupiedTimesDto>>> GetAllOccupiedTimeInDay(string Day, long meetingSpaceId)
        {
            if(await _unitOfWork.MeetingSpaceRepository.GetByIdAsync(meetingSpaceId) == null)
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
