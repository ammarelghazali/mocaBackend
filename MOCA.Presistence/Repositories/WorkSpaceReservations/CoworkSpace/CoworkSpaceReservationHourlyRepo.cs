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
    public class CoworkSpaceReservationHourlyRepo : GenericRepository<CoworkingSpaceReservationHourly>, ICoworkSpaceReservationHourlyRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IReservationsStatusService _reservationsStatusService;

        public CoworkSpaceReservationHourlyRepo(ApplicationDbContext context, IReservationsStatusService reservationsStatusService) : base(context)
        {
            _context = context;
            _reservationsStatusService = reservationsStatusService;
        }

        public IQueryable<GetAllWorkSpaceReservationsResponse> GetAllWorkSpaceSubmissions(GetAllWorkSpaceReservationsDto request)
        {
            var reservations = _context.CoworkingSpaceReservationHourlies.Where(r => r.IsDeleted != true).OrderByDescending(r => r.CreatedAt)
                                                                              .Include(r => r.BasicUser)
                                                                              .Include(r => r.Location)
                                                                              .Include(r => r.TopUps)
                                                                              .Include(r => r.CoworkingSpaceHourlyTransaction)
                                                                              .ThenInclude(r => r.ReservationTransaction)
                                                                              .ThenInclude(r => r.ReservationDetails)
                                                                              .Include(r => r.CoworkingSpaceHourlyCancellation)
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
                                                                                  StartDate = r.Date,
                                                                                  Amount = r.Price,
                                                                                  ReservationTypeId = 1,
                                                                                  Mode = r.TopUps.Count > 0 ? "TopUp" : "Basic",
                                                                                  CreditHours = r.CoworkingSpaceHourlyTransaction.ReservationTransaction
                                                                                                                             .RemainingHours,

                                                                                  EndDate = r.CoworkingSpaceHourlyTransaction.ReservationTransaction
                                                                                                                         .ExtendExpiryDate,

                                                                                  TopUpsLink = r.TopUps.Count > 0 ? "resources/templates/check.png" :
                                                                                                                    "resources/templates/unchecked.png",

                                                                                  EntryScanTime = r.CoworkingSpaceHourlyTransaction
                                                                                                   .ReservationTransaction.ReservationDetails
                                                                                                   .OrderByDescending(r => r.CreatedAt)
                                                                                                   .FirstOrDefault().StartDateTime,

                                                                                  Scanin = r.CoworkingSpaceHourlyTransaction.ReservationTransaction
                                                                                            .ReservationDetails.Select(r => r.StartDateTime).FirstOrDefault(),

                                                                                  ScanOut = r.CoworkingSpaceHourlyTransaction.ReservationTransaction
                                                                                             .ReservationDetails.OrderByDescending(r => r.Id)
                                                                                             .Select(r => r.EndDateTime).FirstOrDefault(),

                                                                                  Status = _reservationsStatusService.GetStatus(r.CoworkingSpaceHourlyTransaction
                                                                                                                                 .ReservationTransaction,
                                                                                                                                 r.CoworkingSpaceHourlyCancellation
                                                                                                                                  .CancelReservation)

                                                                              });

            return reservations;
        }

        public async Task<CoworkingSpaceReservationHourly> GetReservationById(long id)
        {
            return await _context.CoworkingSpaceReservationHourlies.Where(r => r.Id == id && r.IsDeleted != true &&
                                                                          r.CoworkingSpaceHourlyCancellation.CancelReservation == null)
                                                                            .Include(r => r.Location)
                                                                            .ThenInclude(r => r.LocationWorkingHours)
                                                                            .Include(r => r.CoworkingSpaceHourlyTransaction)
                                                                            .ThenInclude(r => r.ReservationTransaction)
                                                                            .Include(r => r.CoworkingSpaceHourlyCancellation)
                                                                            .ThenInclude(r => r.CancelReservation)
                                                                            .FirstOrDefaultAsync();
        }

        public async Task<CoworkingSpaceReservationHourly> GetReservationInfo(long id)
        {
            return await _context.CoworkingSpaceReservationHourlies.Where(r => r.Id == id && r.IsDeleted != true)
                                                                       .Include(r => r.Location)
                                                                       .ThenInclude(r => r.LocationType)
                                                                       .Include(r => r.TopUps)
                                                                       .ThenInclude(r => r.PaymentMethod)
                                                                       .Include(r => r.CoworkingSpaceHourlyTransaction)
                                                                       .ThenInclude(r => r.ReservationTransaction)
                                                                       .ThenInclude(r => r.ReservationDetails)
                                                                       .Include(r => r.BasicUser)
                                                                       .Include(r => r.CoworkingSpaceHourlyCancellation)
                                                                       .ThenInclude(r => r.CancelReservation).FirstOrDefaultAsync();
        }
    }
}
