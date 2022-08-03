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
    public class WorkSpaceReservationBundleRepo : GenericRepository<WorkSpaceReservationBundle>, IWorkSpaceReservationBundleRepo
    {
        private readonly ApplicationDbContext _context;

        public WorkSpaceReservationBundleRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<GetAllWorkSpaceReservationsResponse>> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request)
        {
            var reservations = _context.WorkSpaceReservationBundle.OrderByDescending(r => r.CreatedAt)
                                                                  .Include(r => r.BasicUser)
                                                                  .Include(r => r.Location)
                                                                  .Select(r => new GetAllWorkSpaceReservationsResponse
                                                                  {
                                                                     Id = r.Id,
                                                                     BasicUserId = r.BasicUserId,
                                                                     OpportunityStartDate = r.CreatedAt,
                                                                     FirstName = r.BasicUser.FirstName,
                                                                     LastName = r.BasicUser.LastName,
                                                                     MobileNumber = r.BasicUser.MobileNumber,
                                                                     LocationName = r.Location.Name,
                                                                     ReservationType = "Bundle",
                                                                     DateTime = r.PackageStartDate,
                                                                     Amount = r.PackagePrice,
                                                                     ReservationTypeId = 3,
                                                                     Mode = "basic",
                                                                     TopUpsLink = "resources/templates/unchecked.png",
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

        public async Task<WorkSpaceReservationBundle> GetReservationInfo(long id)
        {
            return await _context.WorkSpaceReservationBundle.Where(r => r.Id == id)
                                                            .Include(r => r.Location)
                                                            .ThenInclude(r => r.LocationType)
                                                            .Include(r => r.BasicUser).FirstOrDefaultAsync();
        }
    }
}
