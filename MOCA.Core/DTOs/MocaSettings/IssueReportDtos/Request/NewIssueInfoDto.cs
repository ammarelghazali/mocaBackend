using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.IssueReportDtos.Request
{
    public class NewIssueInfoDto
    {
        [Required]
        public string AdminId { get; set; }
    }
}
