using Microsoft.EntityFrameworkCore;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Entities.WorkSpaceReservations.CoWorkSpace;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Interfaces.WorkSpaceReservations.CoworkSpace.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.WorkSpaceReservations.CoworkSpace
{
    public class CoworkSpaceReservationBundleRepo : GenericRepository<CoworkingSpaceReservationBundle>, ICoworkSpaceReservationBundleRepo 
    {
        private readonly ApplicationDbContext _context;
        private readonly IReservationsStatusService _reservationsStatusService;

        public CoworkSpaceReservationBundleRepo(ApplicationDbContext context, IReservationsStatusService reservationsStatusService) : base(context)
        {
            _context = context;
            _reservationsStatusService = reservationsStatusService;
        }

        public IQueryable<GetAllWorkSpaceReservationsResponse> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request)
        {
            var reservations = _context.CoworkingSpaceReservationBundles.Where(r => r.IsDeleted != true).OrderByDescending(r => r.CreatedAt)
                                                                 .Include(r => r.BasicUser)
                                                                 .Include(r => r.Location)
                                                                 .Include(r => r.CoworkingSpaceBundleTransaction)
                                                                 .ThenInclude(r => r.ReservationTransaction)
                                                                 .ThenInclude(r => r.ReservationDetails)
                                                                 .Include(r => r.CoworkingSpaceBundleCancellation)
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

                                                                     CreditHours = r.CoworkingSpaceBundleTransaction.ReservationTransaction
                                                                                                                .RemainingHours,

                                                                     EndDate = r.CoworkingSpaceBundleTransaction.ReservationTransaction
                                                                                                            .ExtendExpiryDate,



                                                                     EntryScanTime = r.CoworkingSpaceBundleTransaction
                                                                                      .ReservationTransaction.ReservationDetails
                                                                                      .OrderByDescending(r => r.CreatedAt)
                                                                                      .FirstOrDefault().StartDateTime,

                                                                     Scanin = r.CoworkingSpaceBundleTransaction.ReservationTransaction
                                                                               .ReservationDetails.Select(r => r.StartDateTime).FirstOrDefault(),

                                                                     ScanOut = r.CoworkingSpaceBundleTransaction.ReservationTransaction
                                                                                .ReservationDetails.OrderByDescending(r => r.Id)
                                                                                .Select(r => r.EndDateTime).FirstOrDefault(),

                                                                     Status = _reservationsStatusService.GetStatus(r.CoworkingSpaceBundleTransaction
                                                                                                                    .ReservationTransaction,
                                                                                                                    r.CoworkingSpaceBundleCancellation
                                                                                                                     .CancelReservation)
                                                                 });

            return reservations;
        }

        public async Task<CoworkingSpaceReservationBundle> GetReservationInfo(long id)
        {
            return await _context.CoworkingSpaceReservationBundles.Where(r => r.Id == id && r.IsDeleted != true)
                                                                      .Include(r => r.Location)
                                                                      .ThenInclude(r => r.LocationType)
                                                                      .Include(r => r.CoworkingSpaceBundleTransaction)
                                                                      .ThenInclude(r => r.ReservationTransaction)
                                                                      .ThenInclude(r => r.ReservationDetails)
                                                                      .Include(r => r.BasicUser)
                                                                      .Include(r => r.CoworkingSpaceBundleCancellation)
                                                                      .ThenInclude(r => r.CancelReservation).FirstOrDefaultAsync();
        }
    }
}
