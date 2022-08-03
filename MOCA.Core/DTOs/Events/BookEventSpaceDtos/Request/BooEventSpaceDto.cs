using System.ComponentModel.DataAnnotations;
namespace MOCA.Core.DTOs.Events.BookEventSpaceDtos.Request
{
    public class BooEventSpaceDto
    {
        public long? Id { get; set; }
        public long? LocationNameId { get; set; }
        public long? EventRequesterId { get; set; }
        public string? CompanyCommericalName { get; set; }
        public int? IndustryNameId { get; set; }
        public string? OtherIndustryName { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]+[.]+[a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]+)$", ErrorMessage = "Please enter correct website format.")]
        public string? CompanyWebsite { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]+[.]+[a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]+)$", ErrorMessage = "Please enter correct website format.")]
        public string? CompanyFacebook { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]+[.]+[a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]+)$", ErrorMessage = "Please enter correct website format.")]
        public string? CompanyLinkedin { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]+[.]+[a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]+)$", ErrorMessage = "Please enter correct website format.")]
        public string? CompanyInstgram { get; set; }
        [RegularExpression(@"^[a-zA-Z- ]+$", ErrorMessage = "Name should contain characters only.")]
        public string? ContactFullName1 { get; set; }
        [RegularExpression(@"^(\+[0-9]+)$", ErrorMessage = "Please enter correct mobile number format.")]
        public string? ContactMobile1 { get; set; }
        [RegularExpression(@"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Please enter correct email format.")]
        public string? ContactEmail1 { get; set; }
        [RegularExpression(@"^[a-zA-Z- ]+$", ErrorMessage = "Name should contain characters only.")]
        public string? ContactFullName2 { get; set; }
        [RegularExpression(@"^(\+[0-9]+)$", ErrorMessage = "Please enter correct mobile number format.")]
        public string? ContactMobile2 { get; set; }
        [RegularExpression(@"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Please enter correct email format.")]
        public string? ContactEmail2 { get; set; }
        public string? EventName { get; set; }
        public long? EventCategoryId { get; set; }
        public string? OtherEventCategory { get; set; }
        public string? EventDescription { get; set; }
        public long? EventReccuranceId { get; set; }
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Please enter correct number.")]
        public int? ExpectedNoAttend { get; set; }
        public long? EventTypeId { get; set; }
        public long? EventAttendanceId { get; set; }
        public bool? DoesYourEventSupportStartup { get; set; }
        public bool? IsThereThirdPartyOrganizer { get; set; }
        public string? OrgnizingCompany { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public bool? NeedConsultancy { get; set; }
        public string? Platform { get; set; }
        public long InitiatedId { get; set; }
        public string? IdentityUserId { get; set; }
        public long? OpportunityStageId { get; set; }
        public long? Revenue { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public long? EventOpportunityStatusId { get; set; }
        public long? LobLocationTypeId { get; set; }
        public List<EventSpaceTimeDto>? EventSpaceTimes { get; set; }
        public List<EventSpaceVenuesDto>? EventSpaceVenues { get; set; }
    }
}
