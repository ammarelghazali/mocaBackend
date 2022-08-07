using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request
{
    public class GetAllWorkSpaceReservationsParam
    {
        public long? Id { get; set; }
        public long? LocationId { get; set; }
        public long? ClientId { get; set; } = null;
        //public string Platform { get; set; }
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
        public string? Name { get; set; }
        public string? CountryCode { get; set; }
        public string? MobileNumber { get; set; }
        public int? ReservationTypeId { get; set; }
        public DateTime? FromEndDate { get; set; }
        public DateTime? ToEndDate { get; set; }
    }
}
