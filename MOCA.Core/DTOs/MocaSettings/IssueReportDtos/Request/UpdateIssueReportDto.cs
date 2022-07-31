using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.IssueReportDtos.Request
{
    public class UpdateIssueReportDto
    {
        [Required]
        public string ReportedById { get; set; }
        [Required]
        public string OwnerId { get; set; }
        [Required]
        public long StatusId { get; set; }
        [Required]
        [MaxLength(700)]
        public string Comment { get; set; }
        [Required]
        public long LocationId { get; set; }
        [Required]
        public long CaseTypeId { get; set; }
        [Required]
        [MaxLength(700)]
        public string CaseDescription { get; set; }
        [Required]
        public long LevelSeverityId { get; set; }
        [Required]
        public long PriorityId { get; set; }

    }
}
