using MOCA.Core.Entities.DynamicLists;
using MOCA.Core.Entities.LocationManagment.Base;
using MOCA.Core.Entities.WorkSpaceReservations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class WorkSpace : BaseSpaceEntity
    {
        public decimal? GrossArea { get; set; }

        public decimal? NetArea { get; set; }

        [Required]
        public long WorkSpaceTypeId { get; set; }

        [ForeignKey("WorkSpaceTypeId")]
        public WorkSpaceType WorkSpaceType { get; set; }

        [Required]
        public int MaximumOccupancy { get; set; }
            
        [Required]
        public bool IsFurnishing { get; set; }

        public ICollection<WorkSpaceReservationHourly> WorkSpaceReservationHourlies{ get; set; }
        public ICollection<WorkSpaceReservationTailored> WorkSpaceReservationTailoreds { get; set; }
        public ICollection<WorkSpaceReservationBundle> WorkSpaceReservationBundles { get; set; }
    }
}
