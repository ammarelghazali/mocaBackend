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

        public async Task<IQueryable<GetAllWorkSpaceReservationsResponse>> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request)
        {
            var reservations = _context.WorkSpaceReservationHourly.OrderByDescending(r => r.CreatedAt)
                                                                  .Include(r => r.BasicUser)
                                                                  .Include(r => r.Location)
                                                                  .Include(r => r.TopUps)
                                                                  .Include(r => r.WorkSpaceHourlyTransactions)
                                                                  .ThenInclude(r => r.ReservationTransaction)
                                                                  .ThenInclude(r => r.ReservationDetails)
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
                                                                      Status = getStatus(r.WorkSpaceHourlyTransactions.ReservationTransaction)

                                                                  });

            return reservations;
        }


        public async Task<WorkSpaceReservationHourly> GetReservationInfo(long id)
        {
            return await _context.WorkSpaceReservationHourly.Where(r => r.Id == id)
                                                            .Include(r => r.Location)
                                                            .ThenInclude(r => r.LocationType)
                                                            .Include(r => r.TopUps)
                                                            .ThenInclude(r => r.PaymentMethod)
                                                            .Include(r => r.WorkSpaceHourlyTransactions)
                                                            .ThenInclude(r => r.ReservationTransaction)
                                                            .ThenInclude(r => r.ReservationDetails)
                                                            .Include(r => r.BasicUser).FirstOrDefaultAsync();
        }

        private string getStatus(ReservationTransaction reservation)
        {
            string status = string.Empty;

            var expiryDate = reservation.ExtendExpiryDate ?? null;

            if (expiryDate is null)
            {
                return status;
            }

            var isExpired = DateTime.Compare(DateTime.UtcNow, expiryDate.Value);

            if (isExpired > 0 || isExpired == 0)
                status = "Closed";

            else
            {
                var isScannedIn = reservation.ReservationDetails.Count > 0;

                status = isScannedIn ? "Open" : "New";
            }

            return status;
        }
    }
}
