using MOCA.Core.Entities.BaseEntities;
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
        public string Description { get; set; }
        public long? LobSpaceTypeId { get; set; }

        //[ForeignKey("LobSpaceTypeId")]
        //public LobSpaceType LobSpaceType { get; set; }
    }
}
