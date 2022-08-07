using MOCA.Core.Entities.Shared.Reservations;
using MOCA.Core.Interfaces.Shared.Services;

namespace MOCA.Services.Implementation.Shared
{
    public class ReservationsStatusService : IReservationsStatusService
    {
        private readonly IDateTimeService _dateTimeService;

        public ReservationsStatusService(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
        }

        public string GetStatus(ReservationTransaction reservationTransaction, CancelReservation cancelReservation)
        {
            if (cancelReservation != null)
                return "Cancelled";

            string status = string.Empty;

            var expiryDate = reservationTransaction.ExtendExpiryDate ?? null;

            if (expiryDate is null)
            {
                return status;
            }

            var isExpired = DateTime.Compare(_dateTimeService.NowUtc, expiryDate.Value);

            if (isExpired > 0 || isExpired == 0)
                status = "Expired";

            else
            {
                var isScannedIn = reservationTransaction.ReservationDetails.Count > 0;

                status = isScannedIn ? "Open" : "New";
            }

            return status;
        }
    }
}
