using MOCA.Core.Entities.WorkSpaceReservations;

namespace MOCA.Core.Entities.LocationManagment
{
    public class WorkSpace : BaseWorkSpace
    {
        public ICollection<WorkSpaceReservationHourly> WorkSpaceReservationHourlies { get; set; }
        public ICollection<WorkSpaceReservationTailored> WorkSpaceReservationTailoreds { get; set; }
        public ICollection<WorkSpaceReservationBundle> WorkSpaceReservationBundles { get; set; }
    }
}
