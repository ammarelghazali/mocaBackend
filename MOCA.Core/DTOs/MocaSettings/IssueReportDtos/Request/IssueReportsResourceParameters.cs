using MOCA.Core.DTOs.Shared;
using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.MocaSettings.IssueReportDtos.Request
{
    [SubmissionDateStringMustBeParsedToValidDate
    (ErrorMessage = "Submission Date is not well formated")]
    public class IssueReportsResourceParameters : RequestParameter
    {
        public long? Id { get; set; }
        public String SubmissionDate { get; set; }
        public string? ReportedBy { get; set; }
        public long? Location { get; set; }
        public string? Owner { get; set; }
        public long? CaseType { get; set; }
        public long? LevelOfSeverity { get; set; }
        public long? Priority { get; set; }
        public long? Status { get; set; }
        public long? ClosureDuration { get; set; }
    }

    public class SubmissionDateStringMustBeParsedToValidDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var objectInstance = validationContext.ObjectInstance;
            var resourceParameters = (IssueReportsResourceParameters)validationContext.ObjectInstance;

            try
            {
                if (resourceParameters.SubmissionDate is not null)
                {
                    var submissionDate = resourceParameters.SubmissionDate.Trim();
                    var date = DateTime.Parse(submissionDate);
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(ErrorMessage,
                        new[] { "SubmissionDate" });
            }

            return ValidationResult.Success;
        }
    }
}
