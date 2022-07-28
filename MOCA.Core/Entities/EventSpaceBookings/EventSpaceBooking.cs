using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.LocationManagment;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOCA.Core.Entities.EventSpaceBookings
{
    public class EventSpaceBooking : BaseEntity
    {
        public long? LocationNameId { get; set; }
        public long? EventRequesterId { get; set; }

        [MaxLength(800)]
        public string? CompanyCommericalName { get; set; }
        public long? IndustryNameId { get; set; }

        [MaxLength(800)]
        public string? OtherIndustryName { get; set; }

        [MaxLength(800)]
        public string? CompanyWebsite { get; set; }

        [MaxLength(800)]
        public string? CompanyFacebook { get; set; }

        [MaxLength(800)]
        public string? CompanyLinkedin { get; set; }

        [MaxLength(800)]
        public string? CompanyInstgram { get; set; }

        [MaxLength(800)]
        public string? ContactFullName1 { get; set; }

        [MaxLength(800)]
        public string? ContactMobile1 { get; set; }

        [MaxLength(800)]
        public string? ContactEmail1 { get; set; }

        [MaxLength(800)]
        public string? ContactFullName2 { get; set; }

        [MaxLength(800)]
        public string? ContactMobile2 { get; set; }

        [MaxLength(800)]
        public string? ContactEmail2 { get; set; }

        [MaxLength(800)]
        public string? EventName { get; set; }
        public long? EventCategoryId { get; set; }

        [MaxLength(2000)]
        public string? EventDescription { get; set; }
        public long? EventReccuranceId { get; set; }
        public int? ExpectedNoAttend { get; set; }
        public long? EventTypeId { get; set; }
        public long? EventAttendanceId { get; set; }
        public bool? DoesYourEventSupportStartup { get; set; }
        public bool? IsThereThirdPartyOrganizer { get; set; }

        [MaxLength(800)]
        public string? OrgnizingCompany { get; set; }
        public bool? NeedConsultancy { get; set; }

        [MaxLength(800)]
        public string? Platform { get; set; }

        [MaxLength(800)]
        public string? OtherEventCategory { get; set; }
        public long? InitiatedId { get; set; }
        public string? IdentityUserId { get; set; }
        public long? OpportunityStageId { get; set; }
        public long? Revenue { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public long? EventOpportunityStatusId { get; set; }
        public long? LobLocationTypeId { get; set; }

        [ForeignKey("LobLocationTypeId")]
        public LocationType LocationType { get; set; }

        [ForeignKey("IndustryNameId")]
        public Industry Industry { get; set; }

        [ForeignKey("LocationNameId")]
        public Location Location{ get; set; }

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
