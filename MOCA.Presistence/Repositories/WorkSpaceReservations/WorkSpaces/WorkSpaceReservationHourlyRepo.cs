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
    public class WorkSpaceReservationHourlyRepo : GenericRepository<WorkSpaceReservationHourly>, IWorkSpaceReservationHourlyRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IReservationsStatusService _reservationsStatusService;

        public WorkSpaceReservationHourlyRepo(ApplicationDbContext context, IReservationsStatusService reservationsStatusService) : base(context)
        {
            _context = context;
            _reservationsStatusService = reservationsStatusService;
        }

        public async Task<IQueryable<GetAllWorkSpaceReservationsResponse>> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request)
        {
            var reservations = _context.WorkSpaceReservationHourly.Where(r => r.IsDeleted != true).OrderByDescending(r => r.CreatedAt)
                                                                  .Include(r => r.BasicUser)
                                                                  .Include(r => r.Location)
                                                                  .Include(r => r.TopUps)
                                                                  .Include(r => r.WorkSpaceHourlyTransactions)
                                                                  .ThenInclude(r => r.ReservationTransaction)
                                                                  .ThenInclude(r => r.ReservationDetails)
                                                                  .Include(r => r.WorkSpaceHourlyCancellation)
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
                                                                      ReservationType = "Hourly",
                                                                      DateTime = r.Date,
                                                                      Amount = r.Price,
                                                                      ReservationTypeId = 1,
                                                                      Mode = r.TopUps.Count > 0 ? "TopUp" : "Basic",
                                                                      CreditHours = r.WorkSpaceHourlyTransactions.ReservationTransaction
                                                                                                                 .RemainingHours,

                                                                      EndDate = r.WorkSpaceHourlyTransactions.ReservationTransaction
                                                                                                             .ExtendExpiryDate,

                                                                      TopUpsLink = r.TopUps.Count > 0 ? "resources/templates/check.png" :
                                                                                                        "resources/templates/unchecked.png",

                                                                      EntryScanTime = r.WorkSpaceHourlyTransactions
                                                                                       .ReservationTransaction.ReservationDetails
                                                                                       .OrderByDescending(r => r.CreatedAt)
                                                                                       .FirstOrDefault().StartDateTime,

                                                                      Scanin = r.WorkSpaceHourlyTransactions.ReservationTransaction
                                                                                .ReservationDetails.Select(r => r.StartDateTime).FirstOrDefault(),

                                                                      ScanOut = r.WorkSpaceHourlyTransactions.ReservationTransaction
                                                                                 .ReservationDetails.OrderByDescending(r => r.Id)
                                                                                 .Select(r => r.EndDateTime).FirstOrDefault(),

                                                                      Status = _reservationsStatusService.GetStatus(r.WorkSpaceHourlyTransactions
                                                                                                                     .ReservationTransaction,
                                                                                                                     r.WorkSpaceHourlyCancellation
                                                                                                                      .CancelReservation)

                                                                  });

            return reservations;
        }

        public async Task<WorkSpaceReservationHourly> GetReservationById(long id)
        {
            return await _context.WorkSpaceReservationHourly.Where(r => r.Id == id && r.IsDeleted != true &&
                                                                        r.WorkSpaceHourlyCancellation.CancelReservation == null)
                                                                .Include(r => r.Location)
                                                                .ThenInclude(r => r.LocationWorkingHours)
                                                                .Include(r => r.WorkSpaceHourlyTransactions)
                                                                .ThenInclude(r => r.ReservationTransaction)
                                                                .Include(r => r.WorkSpaceHourlyCancellation)
                                                                .ThenInclude(r => r.CancelReservation)
                                                                .FirstOrDefaultAsync();
        }

        public async Task<WorkSpaceReservationHourly> GetReservationInfo(long id)
        {
            return await _context.WorkSpaceReservationHourly.Where(r => r.Id == id && r.IsDeleted != true)
                                                            .Include(r => r.Location)
                                                            .ThenInclude(r => r.LocationType)
                                                            .Include(r => r.TopUps)
                                                            .ThenInclude(r => r.PaymentMethod)
                                                            .Include(r => r.WorkSpaceHourlyTransactions)
                                                            .ThenInclude(r => r.ReservationTransaction)
                                                            .ThenInclude(r => r.ReservationDetails)
                                                            .Include(r => r.BasicUser)
                                                            .Include(r => r.WorkSpaceHourlyCancellation)
                                                            .ThenInclude(r => r.CancelReservation).FirstOrDefaultAsync();
        }

    }
}
