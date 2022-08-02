using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.DTOs.WorkSpaceReservation.Mobile.Request
{
    public class WorkspaceReservationHomePageDto
    {
        [Required]
        public long ClientId { get; set; }
    }
}
