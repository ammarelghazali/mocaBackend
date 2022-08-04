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
    public class WorkSpaceReservationTailoredRepo : GenericRepository<WorkSpaceReservationTailored>, IWorkSpaceReservationTailoredRepo
    {
        private readonly ApplicationDbContext _context;
        public WorkSpaceReservationTailoredRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IQueryable<GetAllWorkSpaceReservationsResponse>> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request)
        {
            var reservations = _context.WorkSpaceReservationTailored.OrderByDescending(r => r.CreatedAt)
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
                                                                                  ReservationType = "Tailored",
                                                                                  DateTime = r.TailoredStartDate,
                                                                                  Amount = r.TailoredPrice,
                                                                                  ReservationTypeId = 2,
                                                                                  Mode = r.TopUps.Count > 0 ? "TopUp" : "Basic"
                                                                              });

            return reservations;
        }

        public async Task<ReservationTransaction> GetRelatedReservationTransaction(long Reservationid, long reservationTypeId)
        {
            return await _context.ReservationTransactions.Where(r => r.ReservationTypeId == reservationTypeId &&
                                                                                r.ReservationTargetId == Reservationid)
                                                                    .Include(r => r.ReservationDetails)
                                                                    .Include(r => r.ReservationType)
                                                                    .FirstOrDefaultAsync();
        }

        public async Task<WorkSpaceReservationTailored> GetReservationInfo(long id)
        {
            return await _context.WorkSpaceReservationTailored.Where(r => r.Id == id)
                                                            .Include(r => r.Location)
                                                            .ThenInclude(r => r.LocationType)
                                                            .Include(r => r.TopUps)
                                                            .Include(r => r.BasicUser).FirstOrDefaultAsync();
        }
    }
}
