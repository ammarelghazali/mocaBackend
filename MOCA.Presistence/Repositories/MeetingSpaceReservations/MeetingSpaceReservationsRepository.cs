using Microsoft.EntityFrameworkCore;
using MOCA.Core.DTOs.MeetingReservations.Response;
using MOCA.Core.Entities.MeetingSpaceReservation;
using MOCA.Core.Interfaces.MeetingSpaceReservations.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.MeetingSpaceReservations
{
    public class MeetingSpaceReservationsRepository : GenericRepository<MeetingReservation>, IMeetingSpaceReservationRepository
    {
        private readonly ApplicationDbContext _context;
        public MeetingSpaceReservationsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IQueryable<GetAllMeetingSubmissionsResponseDto>> GetAllSubmissions()
        {

            var allSubmissions = _context.MeetingSpaceReservations.OrderByDescending(x => x.CreatedAt)
                .Include(x => x.Location)
                .Include(x => x.BasicUser)
                .Include(x => x.MeetingReservationTopUps)
                .Include(x => x.MeetingReservationTransaction)
                .ThenInclude(x => x.ReservationTransaction)
                .ThenInclude(x => x.ReservationDetails)
                .Select(x => new GetAllMeetingSubmissionsResponseDto
                {
                    Id = x.Id,
                    LocationId = x.LocationId,
                    LocationName = x.Location.Name,
                    UserId = x.BasicUser.Id,
                    FirstName = x.BasicUser.FirstName,
                    LastName = x.BasicUser.LastName,
                    MobileNumber = x.BasicUser.MobileNumber,
                    Date = x.Date,
                    Time = x.Time,
                    SubmissionDate = x.CreatedAt,
                    // Amount = x.Amount // TimePriceTable,
                    // Hourse = x.Hours // TimePriceTable,
                    // VenueName = x.VenueName // MeetingsTable,
                    NumOfGuests = x.NumOfAttendees,
                    HasTopUp = x.MeetingReservationTopUps.Count > 0,
                    ScanInDate = x.MeetingReservationTransaction
                                  .ReservationTransaction
                                  .ReservationDetails
                                  .Select(d => d.StartDateTime)
                                  .FirstOrDefault(),

                    ScanOutDate = x.MeetingReservationTransaction
                                  .ReservationTransaction
                                  .ReservationDetails
                                  .Select(d => d.EndDateTime)
                                  .FirstOrDefault(),
                    //Status = get
                });

            return allSubmissions;
        }


    }
}
