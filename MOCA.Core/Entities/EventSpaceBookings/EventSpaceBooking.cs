using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class EventSpaceBooking : BaseEntity
    {
        public long? LocationNameId { get; set; }
        public long? EventRequesterId { get; set; }
        public string? CompanyCommericalName { get; set; }
        public int? IndustryNameId { get; set; }
        public string? OtherIndustryName { get; set; }
        public string? CompanyWebsite { get; set; }
        public string? CompanyFacebook { get; set; }
        public string? CompanyLinkedin { get; set; }
        public string? CompanyInstgram { get; set; }
        public string? ContactFullName1 { get; set; }
        public string? ContactMobile1 { get; set; }
        public string? ContactEmail1 { get; set; }
        public string? ContactFullName2 { get; set; }
        public string? ContactMobile2 { get; set; }
        public string? ContactEmail2 { get; set; }
        public string? EventName { get; set; }
        public long? EventCategoryId { get; set; }
        public string? EventDescription { get; set; }
        public long? EventReccuranceId { get; set; }
        public int? ExpectedNoAttend { get; set; }
        public long? EventTypeId { get; set; }
        public long? EventAttendanceId { get; set; }
        public bool? DoesYourEventSupportStartup { get; set; }
        public bool? IsThereThirdPartyOrganizer { get; set; }
        public string? OrgnizingCompany { get; set; }
        public bool? NeedConsultancy { get; set; }
        public string? Platform { get; set; }
        public string? OtherEventCategory { get; set; }
        public long? InitiatedId { get; set; }
        public string? IdentityUserId { get; set; }
        public long? OpportunityStageId { get; set; }
        public long? Revenue { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public int? EventOpportunityStatusId { get; set; }
        public int? LocationTypeId { get; set; }

        [ForeignKey("InitiatedId")]
        public Initiated Initiated { get; set; }

        [ForeignKey("EventOpportunityStatusId")]
        public EventOpportunityStatus EventOpportunityStatus { get; set; }

        [ForeignKey("EventCategoryId")]
        public EventCategory EventCategory { get; set; }

        [ForeignKey("EventAttendanceId")]
        public EventAttendance EventAttendance { get; set; }

        [ForeignKey("EventReccuranceId")]
        public EventReccurance EventReccurance { get; set; }

        [ForeignKey("EventRequesterId")]
        public EventRequester EventRequester { get; set; }

        [ForeignKey("OpportunityStageId")]
        public OpportunityStage OpportunityStage { get; set; }

        [ForeignKey("EventTypeId")]
        public EventType EventType { get; set; }
        public ICollection<EventSpaceTime> EventSpaceTimes { get; set; }
        public ICollection<EventSpaceVenues> EventSpaceVenues { get; set; }
        public ICollection<SendEmail> SendEmails { get; set; }
    }
}
