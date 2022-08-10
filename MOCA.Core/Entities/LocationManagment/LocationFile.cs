using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MOCA.Core.Entities.LocationManagment
{
    public class LocationFile : BaseEntity
    {
        public long LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
        //[Required]
        public string LocationContractFilePath { get; set; }
    }
}
