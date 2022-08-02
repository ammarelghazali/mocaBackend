using MOCA.Core.Entities.Shared.Reservations;

namespace MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response
{
    public class WorkSpaceReservationHistoryResponse
    {
        public long Id { get; set; }
        public string Platform { get; set; }
        public DateTime? DateTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryCode { get; set; }
        public string MobileNumber { get; set; }
        public int ReservationTypeId { get; set; }
        public DateTime? EndDate { get; set; }
        public string Mode { get; set; }
        public long CreditHours { get; set; }
        public decimal? Amount { get; set; }
        public int? PaymentMethod { get; set; }
        public DateTime? EntryScanTime { get; set; }
        public DateTime? OpportunityStartDate { get; set; }
        public string Status { get; set; }
        public string ReservationType { get; set; }
        public long LocationId { get; set; }
        public string LocationName { get; set; }
        public long BasicUserId { get; set; }
        public long LocationTypeId { get; set; }
        public string LocationTypeName { get; set; }
        public List<ReservationDetail> lstReservation_Details { get; set; }
        //public List<FoodicsOrderViewModel> lstFoodics_Details { get; set; }
        public List<WorkSpaceTopupHistoryResponse> lstTopupHistory { get; set; }
        public List<GiftedHours> lstGiftedHours { get; set; }
    }
}
