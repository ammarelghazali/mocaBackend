using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Enums.Shared
{
    public enum RefundReservationType
    {
        AddBalanceToWallet = 0,
        RefundBookingToWallet = 1,
        BookingFromWallet = 2,
        RefundBookingFromAdmin = 3,
        BookingByPaymentTap = 4,
        BookingByPaymentTapAndWallet = 5,
        RefundBookingAgainstCancellationPolicy0 = 6,
        RefundBookingAgainstCancellationPolicy50 = 7,
        RefundBookingAgainstCancellationPolicy75 = 8,
        RefundBookingAgainstCancellationPolicy100 = 9,
        Soon = 10
    }
}
