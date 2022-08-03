namespace MOCA.Core.DTOs.WorkSpaceReservation.CRM.Response
{
    public class WorkSpaceTopupHistoryResponse
    {
        public long Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public int Hours { get; set; }
        public string Type { get; set; }
        public string Payment_Method { get; set; }
        public string Reason { get; set; }
        public decimal Amount { get; set; }
    }
}
