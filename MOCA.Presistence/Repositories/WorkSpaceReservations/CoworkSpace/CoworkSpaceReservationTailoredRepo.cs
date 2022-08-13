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
    public class CoworkSpaceReservationTailoredRepo : GenericRepository<CoworkingSpaceReservationTailored>, ICoworkSpaceReservationTailoredRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IReservationsStatusService _reservationsStatusService;

        public CoworkSpaceReservationTailoredRepo(ApplicationDbContext context, IReservationsStatusService reservationsStatusService) : base(context)
        {
            _context = context;
            _reservationsStatusService = reservationsStatusService;
        }

        public IQueryable<GetAllWorkSpaceReservationsResponse> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request)
        {
            var reservations = _context.CoworkingSpaceReservationTailoreds.Where(r => r.IsDeleted != true).OrderByDescending(r => r.CreatedAt)
                                                                                         .Include(r => r.BasicUser)
                                                                                         .Include(r => r.Location)
                                                                                         .Include(r => r.CoworkingSpaceTailoredTransaction)
                                                                                         .ThenInclude(r => r.ReservationTransaction)
                                                                                         .ThenInclude(r => r.ReservationDetails)
                                                                                         .Include(r => r.TopUps)
                                                                                         .Include(r => r.CoworkingSpaceTailoredCancellation)
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
                                                                                             ReservationType = "Tailored",
                                                                                             StartDate = r.TailoredStartDate,
                                                                                             Amount = r.TailoredPrice,
                                                                                             ReservationTypeId = 2,
                                                                                             Mode = r.TopUps.Count > 0 ? "TopUp" : "Basic",
                                                                                             CreditHours = r.CoworkingSpaceTailoredTransaction.ReservationTransaction
                                                                                                                            .RemainingHours,

                                                                                             EndDate = r.CoworkingSpaceTailoredTransaction.ReservationTransaction
                                                                                                                        .ExtendExpiryDate,

                                                                                             TopUpsLink = r.TopUps.Count > 0 ? "resources/templates/check.png" :
                                                                                                                   "resources/templates/unchecked.png",

                                                                                             EntryScanTime = r.CoworkingSpaceTailoredTransaction
                                                                                                  .ReservationTransaction.ReservationDetails
                                                                                                  .OrderByDescending(r => r.CreatedAt)
                                                                                                  .FirstOrDefault().StartDateTime,

                                                                                             Scanin = r.CoworkingSpaceTailoredTransaction.ReservationTransaction
                                                                                           .ReservationDetails.Select(r => r.StartDateTime).FirstOrDefault(),

                                                                                             ScanOut = r.CoworkingSpaceTailoredTransaction.ReservationTransaction
                                                                                            .ReservationDetails.OrderByDescending(r => r.Id)
                                                                                            .Select(r => r.EndDateTime).FirstOrDefault(),

                                                                                             Status = _reservationsStatusService.GetStatus(r.CoworkingSpaceTailoredTransaction
                                                                                                                                .ReservationTransaction,
                                                                                                                                r.CoworkingSpaceTailoredCancellation
                                                                                                                                 .CancelReservation)
                                                                                         });

            return reservations;
        }

        public async Task<CoworkingSpaceReservationTailored> GetReservationById(long id)
        {
            return await _context.CoworkingSpaceReservationTailoreds.Where(r => r.Id == id && r.IsDeleted != true &&
                                                                           r.CoworkingSpaceTailoredCancellation.CancelReservation == null)
                                                                            .Include(r => r.Location)
                                                                            .ThenInclude(r => r.LocationWorkingHours)
                                                                            .Include(r => r.CoworkingSpaceTailoredTransaction)
                                                                            .ThenInclude(r => r.ReservationTransaction)
                                                                            .Include(r => r.CoworkingSpaceTailoredCancellation)
                                                                            .ThenInclude(r => r.CancelReservation)
                                                                            .FirstOrDefaultAsync();
        }

        public async Task<CoworkingSpaceReservationTailored> GetReservationInfo(long id)
        {
            return await _context.CoworkingSpaceReservationTailoreds.Where(r => r.Id == id && r.IsDeleted != true)
                                                                       .Include(r => r.Location)
                                                                       .ThenInclude(r => r.LocationType)
                                                                       .Include(r => r.TopUps)
                                                                       .Include(r => r.CoworkingSpaceTailoredTransaction)
                                                                       .ThenInclude(r => r.ReservationTransaction)
                                                                       .ThenInclude(r => r.ReservationDetails)
                                                                       .Include(r => r.BasicUser)
                                                                       .Include(r => r.CoworkingSpaceTailoredCancellation)
                                                                       .ThenInclude(r => r.CancelReservation).FirstOrDefaultAsync();
        }
    }
}
