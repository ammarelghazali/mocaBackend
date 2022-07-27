using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.MocaSetting
{
    public class TopUp : BaseEntity
    {
        [Required]
        public long TopUpTypeId { get; set; }

        [ForeignKey("TopUpTypeId")]
        public TopUpType TopUpType { get; set; }

        public long? LobSpaceTypeId { get; set; }

        //[ForeignKey("LobSpaceTypeId")]
        //public LobSpaceType LobSpaceType { get; set; }

        [Required]
        public string TermsOfUse { get; set; }

    }
}