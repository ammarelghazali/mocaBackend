using MOCA.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.Entities.LocationManagment
{
    public class Currency : BaseEntity
    {
        public string Name { get; set; }
        public string FlagCode { get; set; }
    }
}
