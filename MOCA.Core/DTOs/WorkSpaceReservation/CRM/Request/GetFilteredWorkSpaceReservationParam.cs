namespace MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request
{
    public class GetFilteredWorkSpaceReservationParam
    {
        public long? Id { get; set; }
        public long? LocationId { get; set; }
        public long? ClientId { get; set; } = null;
        //public string Platform { get; set; }
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string MobileNumber { get; set; }
        public int? ReservationTypeId { get; set; }
        public DateTime? FromEndDate { get; set; }
        public DateTime? ToEndDate { get; set; }
        public string Mode { get; set; }
        public string ReservationType { get; set; }
        public decimal? CreditHours { get; set; }
        public decimal? MaxCreditHours { get; set; }
        public decimal? Amount { get; set; }
        public decimal? MaxAmount { get; set; }
        public int? PaymentMethodId { get; set; }
        public DateTime? EntryScanTime { get; set; }
        public DateTime? OpportunityStartDate { get; set; }
        public string Status { get; set; }
        public string FilterBy { get; set; } = null;
        public string FilterValue { get; set; } = null;
        public string SearchValue { get; set; } = null;
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public string SortBy { get; set; } = null;
        public string SortDirection { get; set; } = null;
    }
}
