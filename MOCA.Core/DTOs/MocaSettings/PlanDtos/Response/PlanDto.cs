using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.DTOs.MocaSettings.PlanDtos.Response
{
    public class PlanDto
    {
        public long Id { get; set; }
        public PlanDtoBase Plan { get; set; }
    }
}
