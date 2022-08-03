using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.LocationManagment
{
    public class BuildingFloor : BaseEntity
    {
        public string Number { get; set; }
        public long BuildingId { get; set; }
        [ForeignKey("BuildingId")]
        public virtual Building Building { get; set; }
        public decimal GrossArea { get; set; }
        public decimal NetArea { get; set; }
        public int MaleRestroomCount { get; set; }
        public int FemaleRestroomCount { get; set; }
    }
}
