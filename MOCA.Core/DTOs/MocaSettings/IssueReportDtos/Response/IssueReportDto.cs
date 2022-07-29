namespace MOCA.Core.DTOs.MocaSettings.IssueReportDtos.Response
{
    public class IssueReportDto
    {
        public long Id { get; set; }
        public long? LobSpaceTypeId { get; set; }
        public string SubmissionDate { get; set; }
        public string ReportedBy { get; set; }
        public Guid OwnerId { get; set; }
        public string ClosureDuration { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public long LocationId { get; set; }
        public string CaseType { get; set; }
        public string CaseDescription { get; set; }
        public string Severity { get; set; }
        public string Priority { get; set; }
    }
}
