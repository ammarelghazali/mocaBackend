using MOCA.Core.Entities.WorkSpaceReservations;

namespace MOCA.Core.Entities.LocationManagment
{
    public class WorkSpace : BaseWorkSpace
    {
        public ICollection<WorkSpaceReservationHourly> WorkSpaceReservationHourlies { get; set; }
        public ICollection<WorkSpaceReservationTailored> WorkSpaceReservationTailoreds { get; set; }
        public ICollection<WorkSpaceReservationBundle> WorkSpaceReservationBundles { get; set; }
        public WorkSpaceBundlePricing WorkSpaceBundlePricing { get; set; }
        public WorkSpaceHourlyPricing WorkSpaceHourlyPricing { get; set; }
        public WorkSpaceTailoredPricing WorkSpaceTailoredPricing { get; set; }
    }
}
