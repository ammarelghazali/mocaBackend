namespace MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response
{
    public class GetFilteredWorkSpaceReservationResponse
    {
        public long Id { get; set; }
        //public string Platform { get; set; }
        public string LocationName { get; set; }
        public DateTime? DateTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public int? ReservationTypeId { get; set; }
        public DateTime? EndDate { get; set; }
        public string Mode { get; set; }
        public decimal? CreditHours { get; set; }
        public decimal? Amount { get; set; }
        public string CartCurrency { get; set; }
        public int? PaymentMethodId { get; set; }
        public DateTime? EntryScanTime { get; set; }
        public DateTime? OpportunityStartDate { get; set; }
        public string Status { get; set; }
        public long BasicUserId { get; set; }
        public string ReservationType { get; set; }
        public string PaymentMethodName { get; set; }

        public DateTime? Scanin { get; set; }
        public DateTime? ScanOut { get; set; }
        public string TopUpsLink { get; set; }

        public long LocationId { get; set; }

        public int pg_total { get; set; }
    }
}
