using Microsoft.EntityFrameworkCore;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Interfaces.WorkSpaceReservations.WorkSpaces.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.WorkSpaceReservations.WorkSpaces
{
    public class WorkSpaceReservationBundleRepo : GenericRepository<WorkSpaceReservationBundle>, IWorkSpaceReservationBundleRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IReservationsStatusService _reservationsStatusService;

        public WorkSpaceReservationBundleRepo(ApplicationDbContext context, IReservationsStatusService reservationsStatusService) : base(context)
        {
            _context = context;
            _reservationsStatusService = reservationsStatusService;
        }

        public IQueryable<GetAllWorkSpaceReservationsResponse> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request)
        {
            var reservations = _context.WorkSpaceReservationBundle.Where(r => r.IsDeleted != true).OrderByDescending(r => r.CreatedAt)
                                                                  .Include(r => r.BasicUser)
                                                                  .Include(r => r.Location)
                                                                  .Include(r => r.WorkSpaceBundleTransactions)
                                                                  .ThenInclude(r => r.ReservationTransaction)
                                                                  .ThenInclude(r => r.ReservationDetails)
                                                                  .Include(r => r.WorkSpaceBundleCancellation)
                                                                  .ThenInclude(r => r.CancelReservation)
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
                                                                      StartDate = r.BundleStartDate,
                                                                      Amount = r.BundlePrice,
                                                                      ReservationTypeId = 3,
                                                                      Mode = "basic",
                                                                      TopUpsLink = "resources/templates/unchecked.png",

                                                                      CreditHours = r.WorkSpaceBundleTransactions.ReservationTransaction
                                                                                                                 .RemainingHours,

                                                                      EndDate = r.WorkSpaceBundleTransactions.ReservationTransaction
                                                                                                             .ExtendExpiryDate,



                                                                      EntryScanTime = r.WorkSpaceBundleTransactions
                                                                                       .ReservationTransaction.ReservationDetails
                                                                                       .OrderByDescending(r => r.CreatedAt)
                                                                                       .FirstOrDefault().StartDateTime,

                                                                      Scanin = r.WorkSpaceBundleTransactions.ReservationTransaction
                                                                                .ReservationDetails.Select(r => r.StartDateTime).FirstOrDefault(),

                                                                      ScanOut = r.WorkSpaceBundleTransactions.ReservationTransaction
                                                                                 .ReservationDetails.OrderByDescending(r => r.Id)
                                                                                 .Select(r => r.EndDateTime).FirstOrDefault(),

                                                                      Status = _reservationsStatusService.GetStatus(r.WorkSpaceBundleTransactions
                                                                                                                     .ReservationTransaction,
                                                                                                                     r.WorkSpaceBundleCancellation
                                                                                                                      .CancelReservation)
                                                                  });

            return reservations;
        }

        public async Task<WorkSpaceReservationBundle> GetReservationInfo(long id)
        {
            return await _context.WorkSpaceReservationBundle.Where(r => r.Id == id && r.IsDeleted != true)
                                                            .Include(r => r.Location)
                                                            .ThenInclude(r => r.LocationType)
                                                            .Include(r => r.WorkSpaceBundleTransactions)
                                                            .ThenInclude(r => r.ReservationTransaction)
                                                            .ThenInclude(r => r.ReservationDetails)
                                                            .Include(r => r.BasicUser)
                                                            .Include(r => r.WorkSpaceBundleCancellation)
                                                            .ThenInclude(r => r.CancelReservation).FirstOrDefaultAsync();
        }
    }
}
