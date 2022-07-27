using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.MocaSetting
{
    public class IssueReport : BaseEntity
    {
        [Key, Column(Order = 1)]
        public override long Id { get; set; }

        public long? LobSpaceTypeId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public long LocationId { get; set; }

        [Required]
        public long StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }

        [Required]
        public long LevelSeverityId { get; set; }

        [ForeignKey("LevelSeverityId")]
        public Severity Severity { get; set; }

        [Required]
        public long PriorityId { get; set; }

        [ForeignKey("PriorityId")]
        public Priority Priority { get; set; }

        [Required]
        public long CaseTypeId { get; set; }

        [ForeignKey("CaseTypeId")]
        public CaseType CaseType { get; set; }

        [Required]
        public DateTime SubmissionDate { get; set; }

        public DateTime? ClosureDate { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public string CaseDescription { get; set; }

        [Key, Column(Order = 2)]
        [Required]
        public override DateTime? LastModifiedAt { get; set; }

    }
}
