using Microsoft.EntityFrameworkCore;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response;
using MOCA.Core.Entities.WorkSpaceReservations;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Interfaces.WorkSpaceReservations.Repositories;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;

namespace MOCA.Presistence.Repositories.WorkSpaceReservations
{
    public class WorkSpaceReservationTailoredRepo : GenericRepository<WorkSpaceReservationTailored>, IWorkSpaceReservationTailoredRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IReservationsStatusService _reservationsStatusService;

        public WorkSpaceReservationTailoredRepo(ApplicationDbContext context, IReservationsStatusService reservationsStatusService) : base(context)
        {
            _context = context;
            _reservationsStatusService = reservationsStatusService;
        }

        public async Task<IQueryable<GetAllWorkSpaceReservationsResponse>> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request)
        {
            var reservations = _context.WorkSpaceReservationTailored.Where(r => r.IsDeleted != true).OrderByDescending(r => r.CreatedAt)
                                                                              .Include(r => r.BasicUser)
                                                                              .Include(r => r.Location)
                                                                              .Include(r => r.WorkSpaceTailoredTransactions)
                                                                              .ThenInclude(r => r.ReservationTransaction)
                                                                              .ThenInclude(r => r.ReservationDetails)
                                                                              .Include(r => r.TopUps)
                                                                              .Include(r => r.WorkSpaceTailoredCancellation)
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
                                                                                  DateTime = r.TailoredStartDate,
                                                                                  Amount = r.TailoredPrice,
                                                                                  ReservationTypeId = 2,
                                                                                  Mode = r.TopUps.Count > 0 ? "TopUp" : "Basic",
                                                                                  CreditHours = r.WorkSpaceTailoredTransactions.ReservationTransaction
                                                                                                                 .RemainingHours,

                                                                                  EndDate = r.WorkSpaceTailoredTransactions.ReservationTransaction
                                                                                                             .ExtendExpiryDate,

                                                                                  TopUpsLink = r.TopUps.Count > 0 ? "resources/templates/check.png" :
                                                                                                        "resources/templates/unchecked.png",

                                                                                  EntryScanTime = r.WorkSpaceTailoredTransactions
                                                                                       .ReservationTransaction.ReservationDetails
                                                                                       .OrderByDescending(r => r.CreatedAt)
                                                                                       .FirstOrDefault().StartDateTime,

                                                                                  Scanin = r.WorkSpaceTailoredTransactions.ReservationTransaction
                                                                                .ReservationDetails.Select(r => r.StartDateTime).FirstOrDefault(),

                                                                                  ScanOut = r.WorkSpaceTailoredTransactions.ReservationTransaction
                                                                                 .ReservationDetails.OrderByDescending(r => r.Id)
                                                                                 .Select(r => r.EndDateTime).FirstOrDefault(),

                                                                                  Status = _reservationsStatusService.GetStatus(r.WorkSpaceTailoredTransactions
                                                                                                                     .ReservationTransaction,
                                                                                                                     r.WorkSpaceTailoredCancellation
                                                                                                                      .CancelReservation)
                                                                              });

            return reservations;
        }

        public async Task<WorkSpaceReservationTailored> GetReservationById(long id)
        {
            return await _context.WorkSpaceReservationTailored.Where(r => r.Id == id && r.IsDeleted != true)
                                                                .Include(r => r.Location)
                                                                .ThenInclude(r => r.LocationWorkingHours)
                                                                .Include(r => r.WorkSpaceTailoredTransactions)
                                                                .ThenInclude(r => r.ReservationTransaction)
                                                                .FirstOrDefaultAsync();
        }

        public async Task<WorkSpaceReservationTailored> GetReservationInfo(long id)
        {
            return await _context.WorkSpaceReservationTailored.Where(r => r.Id == id && r.IsDeleted != true)
                                                            .Include(r => r.Location)
                                                            .ThenInclude(r => r.LocationType)
                                                            .Include(r => r.TopUps)
                                                            .Include(r => r.WorkSpaceTailoredTransactions)
                                                            .ThenInclude(r => r.ReservationTransaction)
                                                            .ThenInclude(r => r.ReservationDetails)
                                                            .Include(r => r.BasicUser)
                                                            .Include(r => r.WorkSpaceTailoredCancellation)
                                                            .ThenInclude(r => r.CancelReservation).FirstOrDefaultAsync();
        }
    }
}
