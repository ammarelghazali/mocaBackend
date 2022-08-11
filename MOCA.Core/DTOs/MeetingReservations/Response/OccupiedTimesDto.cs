using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.DTOs.MeetingReservations.Response
{
    public class OccupiedTimesDto
    {
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }
}
