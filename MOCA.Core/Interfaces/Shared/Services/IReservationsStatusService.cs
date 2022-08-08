using MOCA.Core.Entities.Shared.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Interfaces.Shared.Services
{
    public interface IReservationsStatusService
    {
        string GetStatus(ReservationTransaction reservationTransaction, CancelReservation cancelReservation);
    }
}
