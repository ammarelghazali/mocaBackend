using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MOCA.Core.Entities.BaseEntities;

namespace MOCA.Core.Entities.MocaSetting
{
    public class IssueCaseStage : BaseEntity
    {
        [Key, Column(Order = 1)]
        public override long Id { get; set; }

        [Key, Column(Order = 2)]
        public override DateTime? LastModifiedAt { get; set; }

        [ForeignKey("Id,UpdatedAt")]
        public IssueReport IssueReport { get; set; }
    }
}
