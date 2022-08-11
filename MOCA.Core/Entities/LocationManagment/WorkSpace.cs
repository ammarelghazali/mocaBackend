using MOCA.Core.Entities.DynamicLists;
using MOCA.Core.Entities.LocationManagment.Base;
using MOCA.Core.Entities.WorkSpaceReservations.WorkSpaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class WorkSpace : BaseWorkSpace
    {
        public ICollection<WorkSpaceReservationHourly> WorkSpaceReservationHourlies { get; set; }
        public ICollection<WorkSpaceReservationTailored> WorkSpaceReservationTailoreds { get; set; }
        public ICollection<WorkSpaceReservationBundle> WorkSpaceReservationBundles { get; set; }

        public ICollection<WorkSpaceBundlePricing> WorkSpaceBundlePricing { get; set; }
        public ICollection<WorkSpaceHourlyPricing> WorkSpaceHourlyPricing { get; set; }
        public ICollection<WorkSpaceTailoredPricing> WorkSpaceTailoredPricing { get; set; }
    }
}
