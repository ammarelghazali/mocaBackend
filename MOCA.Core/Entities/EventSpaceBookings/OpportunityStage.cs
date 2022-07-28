using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class OpportunityStage : BaseEntity
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        public ICollection<OpportunityStageReport> OpportunityStageReports { get; set; }
    }
}
