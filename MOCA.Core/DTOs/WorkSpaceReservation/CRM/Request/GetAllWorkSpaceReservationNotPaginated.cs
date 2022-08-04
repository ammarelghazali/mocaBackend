using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request
{
    public class GetAllWorkSpaceReservationNotPaginated
    {
        public DateTime? Date{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Mode { get; set; }
        public decimal? CreditHours { get; set; }
        public decimal? Amount { get; set; }
        public int? PaymentMethodId { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? EntryScanTime { get; set; }
        public DateTime? OpportunityStartDate { get; set; }
        public string Status { get; set; }
        public int? PlanDayType { get; set; }
        public long? ClientId { get; set; }
    }
}
