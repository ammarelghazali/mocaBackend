using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Entities.SSO.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.MocaSetting
{
    public class IssueReport : BaseEntity
    {
        [Key, Column(Order = 1)]
        public override long Id { get; set; }

        [Key, Column(Order = 2)]
        [Required]
        public override DateTime? LastModifiedAt { get; set; }

        public long? LobSpaceTypeId { get; set; }
        [ForeignKey("LobSpaceTypeId")]
        public LocationType LobSpaceType { get; set; }
        

        [ForeignKey("CreatedBy")]
        public Admin ReportedBy { get; set; }

        [Required]
        public Guid OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public Admin Owner { get; set; }
        
        public int MyProperty { get; set; }

        [Required]
        public long LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Location Location { get; set; } 

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
        [MaxLength(1000)]
        public string Comment { get; set; }

        [Required]
        [MaxLength(1000)]
        public string CaseDescription { get; set; }


    }
}
