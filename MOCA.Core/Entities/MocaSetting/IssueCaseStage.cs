using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Enums.LocationManagment;

namespace MOCA.Core.Entities.MocaSetting
{
    public class IssueCaseStage : BaseEntity
    {
        [Key, Column(Order = 1)]
        public override long Id { get; set; }

        [Key, Column(Order = 2)]
        public override DateTime? LastModifiedAt { get; set; }

        [ForeignKey("Id,LastModifiedAt")]
        public IssueReport IssueReport { get; set; }
        public long? LobSpaceTypeId { get; set; }
        [ForeignKey("LobSpaceTypeId")]
        public LocationType LobSpaceType { get; set; }


    }
}
