using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request
{
    public class GetAllWorkSpaceReservationNotPaginated
    {
        [Range(1, long.MaxValue, ErrorMessage = "Id cannot be zero")]
        public long? Id { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "LocationId cannot be zero")]
        public long? LocationId { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "ClientId cannot be zero")]
        public long? ClientId { get; set; } = null;
        //public string Platform { get; set; }
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
        public string? Name { get; set; }
        public string? MobileNumber { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "ReservationTypeId cannot be zero")]
        public int? ReservationTypeId { get; set; }
        public DateTime? FromEndDate { get; set; }
        public DateTime? ToEndDate { get; set; }
        public string? Mode { get; set; }
        public string? ReservationType { get; set; }
        public decimal? CreditHours { get; set; }
        public decimal? MaxCreditHours { get; set; }
        public decimal? Amount { get; set; }
        public decimal? MaxAmount { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "PaymentMethodId cannot be zero")]
        public int? PaymentMethodId { get; set; }
        public DateTime? EntryScanTime { get; set; }
        public DateTime? OpportunityStartDate { get; set; }
        public string? Status { get; set; }
        public string? FilterBy { get; set; } = null;
        public string? FilterValue { get; set; } = null;
        public string? SearchValue { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public string? SortDirection { get; set; } = null;
    }
}
