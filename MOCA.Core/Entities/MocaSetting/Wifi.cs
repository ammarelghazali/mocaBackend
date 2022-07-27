using System.ComponentModel.DataAnnotations;
using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.MocaSetting
{
    public class Wifi : BaseEntity
    {
        [Required]
        public string Description { get; set; }
        
        public long? LobSpaceTypeId  { get; set; }

        //[ForeignKey("LobSpaceTypeId")]
        //public LobSpaceType LobSpaceType { get; set; }
    }
}
