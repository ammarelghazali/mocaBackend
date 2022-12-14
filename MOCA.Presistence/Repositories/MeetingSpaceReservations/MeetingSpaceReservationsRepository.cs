using Microsoft.EntityFrameworkCore;
using MOCA.Core.DTOs.MeetingReservations.Request;
using MOCA.Core.DTOs.MeetingReservations.Response;
using MOCA.Core.Entities.MeetingSpaceReservation;
using MOCA.Core.Interfaces.MeetingSpaceReservations.Repositories;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.MeetingSpaceReservations
{
    public class MeetingSpaceReservationsRepository : GenericRepository<MeetingReservation>, IMeetingSpaceReservationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IReservationsStatusService _reservationsStatusService;
        public MeetingSpaceReservationsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IQueryable<MeetingReservationResponseDto>> GetAllSubmissions()
        {
            var allMeetingReservations = await getAll();
            return allMeetingReservations;
        }

        public async Task<MeetingReservationResponseDto> GetMeetingReservationById(long id)
        {
            var allMeetingReservation = await getAll();
            var meetingReservation = await allMeetingReservation.Where(x => x.Id == id).ToListAsync();
            return meetingReservation[0];
        }


        public async Task<IQueryable<MeetingReservationResponseDto>> GetAllSubmissionsWithFilter(GetAllMeetingReservationsWithFilterRequestDto dto)
        {
            var allMeetingReservation = await getAll();

            if (dto.Id != null)
                allMeetingReservation = allMeetingReservation.Where(x => x.Id == dto.Id);
            if (dto.MobileNumber != null)
                allMeetingReservation = allMeetingReservation.Where(x => x.MobileNumber == dto.MobileNumber);
            if (dto.VenueName != null)
                allMeetingReservation = allMeetingReservation.Where(x => x.VenueName == dto.VenueName);
            if (dto.FirstName != null)
                allMeetingReservation = allMeetingReservation.Where(x => x.FirstName == dto.FirstName);
            if (dto.LastName != null)
                allMeetingReservation = allMeetingReservation.Where(x => x.LastName == dto.LastName);
            if (dto.LocationName != null)
                allMeetingReservation = allMeetingReservation.Where(x => x.LocationName == dto.LocationName);
            if (dto.Status != null)
                allMeetingReservation = allMeetingReservation.Where(x => x.Status == dto.Status);

            /*
            if (dto.FromHours != null )
                allMeetingReservation = allMeetingReservation.Where(x => x.FromHours == dto.FromHours);
            
            /
            if (dto.Id != null)
                allMeetingReservation = allMeetingReservation.Where(x => x.Id == dto.Id);
            if (dto.Id != null)
                allMeetingReservation = allMeetingReservation.Where(x => x.Id == dto.Id);
            dto.MaxAmount
                dto.ToTime*/

            return allMeetingReservation;
        }



        public async Task<List<MeetingReservationLocationsDto>> GetAllDistinctLocations()
        {
            var locations = await _context.MeetingSpaceReservations
                .Where(x => x.IsDeleted != true)
                .Include(x => x.Location)
                .Select(x => new MeetingReservationLocationsDto
                {
                    LocationId = x.LocationId,
                    LocationName = x.Location.Name,
                })
                .Distinct()
                .ToListAsync();

            return locations;
        }



        private async Task<IQueryable<MeetingReservationResponseDto>> getAll()
        {
            var allSubmissions = _context.MeetingSpaceReservations
                .Where(x => x.IsDeleted != true)
                .OrderByDescending(x => x.CreatedAt)
                .Include(x => x.Location)
                .Include(x => x.BasicUser)
                .Include(x => x.MeetingSpace)
                .Include(x => x.MeetingSpaceHourlyPricing)
                .Include(x => x.MeetingReservationTopUps)
                .Include(x => x.MeetingReservationTransaction)
                .ThenInclude(x => x.ReservationTransaction)
                .ThenInclude(x => x.ReservationDetails)
                .Include(x => x.MeetingReservationCancellation)
                .ThenInclude(x => x.CancelReservation)
                .Include(x => x.MeetingAttendees)
                .Select(x => new MeetingReservationResponseDto
                {
                    Id = x.Id,
                    Date = x.DateAndTime.ToShortDateString(),
                    Time = x.DateAndTime.ToShortTimeString(),
                    UserId = x.BasicUser.Id,
                    LocationId = x.LocationId,
                    SubmissionDate = x.CreatedAt,
                    LocationName = x.Location.Name,
                    FirstName = x.BasicUser.FirstName,
                    LastName = x.BasicUser.LastName,
                    MobileNumber = x.BasicUser.MobileNumber,
                    Amount = x.MeetingSpaceHourlyPricing.TotalPrice,
                    Hourse = x.MeetingSpaceHourlyPricing.Hours,
                    VenueName = x.MeetingSpace.VenueName,
                    NumOfGuests = x.NumOfAttendees,
                    HasTopUp = x.MeetingReservationTopUps.Count > 0,
                    TopUps = x.MeetingReservationTopUps,
                    Attendees = x.MeetingAttendees,
                    
                    RemainingHours = x.MeetingReservationTransaction
                                      .ReservationTransaction
                                      .RemainingHours,

                    ScanInDate = x.MeetingReservationTransaction
                                  .ReservationTransaction
                                  .ReservationDetails
                                  .Select(d => d.StartDateTime)
                                  .FirstOrDefault(),

                    ScanOutDate = x.MeetingReservationTransaction
                                  .ReservationTransaction
                                  .ReservationDetails
                                  .OrderByDescending(x => x.Id)
                                  .Select(d => d.EndDateTime)
                                  .FirstOrDefault(),

                    Status = _reservationsStatusService
                             .GetStatus(x.MeetingReservationTransaction.ReservationTransaction,
                              x.MeetingReservationCancellation.CancelReservation)


                });

            return allSubmissions;
        }

        public async Task<int> GetMeetingsWithinPeriodOfTime(DateTime fromDate, DateTime toDate, long meetingSpaceId)
        {
            var meetingReservations =  _context.MeetingSpaceReservations.Where(x => x.IsDeleted != true
                                                                                && x.MeetingSpaceId == meetingSpaceId
                                                                                && x.DateAndTime >= fromDate 
                                                                                && x.DateAndTime <= toDate
                                                                               ).Count();
            return meetingReservations;
        }



        public async Task<List<OccupiedTimesDto>> GetMeetingsInDay(DateTime Day, long meetingSpaceId)
        {
            var meetingReservations = await _context.MeetingSpaceReservations
                .Where(x => x.IsDeleted != true
                    && x.MeetingSpaceId == meetingSpaceId
                    && x.DateAndTime.Day == Day.Day
                    && x.DateAndTime.Month == Day.Month
                    && x.DateAndTime.Year == Day.Year
                    )
                    .Include(x => x.MeetingSpaceHourlyPricing)
                    .Select(x => new OccupiedTimesDto
                    {
                        fromDate = x.DateAndTime,
                        toDate = x.DateAndTime.AddHours(x.MeetingSpaceHourlyPricing.Hours)
                    }).ToListAsync();

            return meetingReservations;
        }
    }
}
