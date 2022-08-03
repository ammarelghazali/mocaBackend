using Microsoft.EntityFrameworkCore;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Entities.Shared.Reservations;
using MOCA.Core.Entities.WorkSpaceReservations;
using MOCA.Core.Interfaces.WorkSpaceReservations.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.WorkSpaceReservations
{
    public class WorkSpaceReservationHourlyRepo : GenericRepository<WorkSpaceReservationHourly>, IWorkSpaceReservationHourlyRepo
    {
        private readonly ApplicationDbContext _context;

        public WorkSpaceReservationHourlyRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<GetAllWorkSpaceReservationsResponse>> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request)
        {
            var reservations = _context.WorkSpaceReservationHourly.OrderByDescending(r => r.CreatedAt)
                                                                  .Include(r => r.BasicUser)
                                                                  .Include(r => r.Location)
                                                                  .Include(r => r.TopUps)
                                                                  .Select(r => new GetAllWorkSpaceReservationsResponse
                                                                  {
                                                                      Id = r.Id,
                                                                      BasicUserId = r.BasicUserId,
                                                                      OpportunityStartDate = r.CreatedAt,
                                                                      FirstName = r.BasicUser.FirstName,
                                                                      LastName = r.BasicUser.LastName,
                                                                      MobileNumber = r.BasicUser.MobileNumber,
                                                                      LocationName = r.Location.Name,
                                                                      ReservationType = "Hourly",
                                                                      DateTime = r.Date,
                                                                      Amount = r.Price,
                                                                      ReservationTypeId = 1,
                                                                      Mode = r.TopUps.Count > 0 ? "TopUp" : "Basic"
                                                                  });

            return await reservations.Skip(request.pageSize * (request.pageNumber - 1))
                                     .Take(request.pageSize)
                                     .ToListAsync();

        }

        public async Task<ReservationTransaction> GetRelatedReservationTransaction(long Reservationid, long reservationTypeId)
        {
            return await _context.ReservationTransactions.Where(r => r.ReservationTypeId == reservationTypeId &&
                                                                     r.ReservationTargetId == Reservationid)
                                                         .Include(r => r.ReservationDetails)
                                                         .Include(r => r.ReservationType)
                                                         .FirstOrDefaultAsync();
        }

        public async Task<WorkSpaceReservationHourly> GetReservationInfo(long id)
        {
            return await _context.WorkSpaceReservationHourly.Where(r => r.Id == id)
                                                            .Include(r => r.Location)
                                                            .ThenInclude(r => r.LocationType)
                                                            .Include(r => r.TopUps)
                                                            .ThenInclude(r => r.PaymentMethod)
                                                            .Include(r => r.BasicUser).FirstOrDefaultAsync();
        }
    }
}
