using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Enums.LocationManagment;

namespace MOCA.Core.Entities.MocaSetting
{
    public class Wifi : BaseEntity
    {
        [Required]
        public string Description { get; set; }

        public long? LobSpaceTypeId { get; set; }
        [ForeignKey("LobSpaceTypeId")]
        public LocationType LobSpaceType { get; set; }

    }
}
