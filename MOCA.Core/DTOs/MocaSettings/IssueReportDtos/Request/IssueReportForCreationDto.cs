using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.IssueReportDtos.Request
{
    public class IssueReportForCreationDto
    {
        //[Required]
        //[Range(1, long.MaxValue, ErrorMessage = "Report Issue Id Cannot Be Zero")]
        //public long Id { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Lob Space Type Id cannot be zero or null")]
        public long LobSpaceTypeId { get; set; }

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
