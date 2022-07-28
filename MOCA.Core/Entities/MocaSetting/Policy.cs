using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Enums.LocationManagment;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.MocaSetting
{
    public class Policy : BaseEntity
    {
        [Required]
        public long PolicyTypeId { get; set; }
        
        [ForeignKey("PolicyTypeId")]
        public PolicyType PolicyType { get; set; }
       
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        public long? LobSpaceTypeId { get; set; }
        [ForeignKey("LobSpaceTypeId")]
        public LocationType LobSpaceType { get; set; }

    }
}
