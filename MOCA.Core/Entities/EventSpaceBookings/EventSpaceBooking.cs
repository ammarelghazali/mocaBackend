using MOCA.Core.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class EventSpaceBooking : BaseEntity
    {
        public long? LocationName_ID { get; set; }
        public long? EventRequester_ID { get; set; }
        public string? CompanyCommericalName { get; set; }
        public int? IndustryName_ID { get; set; }
        public string? Other_IndustryName { get; set; }
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
        public long? EventCategory_ID { get; set; }
        public string? EventDescription { get; set; }
        public long? EventReccurance_ID { get; set; }
        public int? ExpectedNoAttend { get; set; }
        public long? EventType_ID { get; set; }
        public long? EventAttendance_ID { get; set; }
        public bool? DoesYourEventSupportStartup { get; set; }
        public bool? IsThereThirdPartyOrganizer { get; set; }
        public string? OrgnizingCompany { get; set; }
        public bool? NeedConsultancy { get; set; }
        public string? Platform { get; set; }
        public string? OtherEventCategory { get; set; }
        public long? Initiated_ID { get; set; }
        public string? IdentityUser_ID { get; set; }
        public long? OpportunityStage_ID { get; set; }
        public long? Revenue { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public int? EventOpportunityStatus_ID { get; set; }
        public int? LocationType_ID { get; set; }

        [ForeignKey("Initiated_ID")]
        public Initiated Initiated { get; set; }

        [ForeignKey("EventOpportunityStatus_ID")]
        public EventOpportunityStatus EventOpportunityStatus { get; set; }

        [ForeignKey("EventCategory_ID")]
        public EventCategory EventCategory { get; set; }

        [ForeignKey("EventAttendance_ID")]
        public EventAttendance EventAttendance { get; set; }

        [ForeignKey("EventReccurance_ID")]
        public EventReccurance EventReccurance { get; set; }

        [ForeignKey("EventRequester_ID")]
        public EventRequester EventRequester { get; set; }

        [ForeignKey("OpportunityStage_ID")]
        public OpportunityStage OpportunityStage { get; set; }

        [ForeignKey("EventType_ID")]
        public EventType EventType { get; set; }
        public ICollection<EventSpaceTime> EventSpace_Times { get; set; }
        public ICollection<EventSpaceVenues> EventSpace_Venues { get; set; }
        public ICollection<SendEmail> SendEmails { get; set; }
    }
}
