using MOCA.Core.DTOs.Events.EventAttendanceDtos.Response;
using MOCA.Core.DTOs.Events.EventCategoryDtos.Response;
using MOCA.Core.DTOs.Events.EventOpportunityDtos.Response;
using MOCA.Core.DTOs.Events.EventReccuranceDtos.Response;
using MOCA.Core.DTOs.Events.EventTypeDtos.Response;
using MOCA.Core.DTOs.Events.Response;
namespace MOCA.Core.DTOs.Events.BookEventSpaceDtos.Response
{
    public class GetAllBookedEventSpaceResponseDto
    {
        public long Id { get; set; }
        //public long LocationName_ID { get; set; }
        public LocationViewModel? Location { get; set; }
        //public long EventRequester_ID { get; set; }
        public EventRequesterDto EventRequester { get; set; }
        public string CompanyCommericalName { get; set; }
        //public int IndustryName_ID { get; set; }
        public IndustryViewModel? Industry { get; set; }
        public string OtherIndustryName { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyFacebook { get; set; }
        public string CompanyLinkedin { get; set; }
        public string CompanyInstgram { get; set; }
        public string ContactFullName1 { get; set; }
        public string ContactMobile1 { get; set; }
        public string ContactEmail1 { get; set; }
        public string ContactFullName2 { get; set; }
        public string ContactMobile2 { get; set; }
        public string ContactEmail2 { get; set; }
        public string EventName { get; set; }
        //public long EventCategory_ID { get; set; }
        public EventCategoryDto EventCategory { get; set; }
        public string OtherEventCategory { get; set; }
        public string EventDescription { get; set; }
        //public long EventReccurance_ID { get; set; }
        public EventRecurrenceDto EventReccurance { get; set; }
        public int? ExpectedNoAttend { get; set; }
        //public long EventType_ID { get; set; }
        public EventTypeDto EventType { get; set; }
        //public long EventAttendance_ID { get; set; }
        public EventAttendanceDto EventAttendance { get; set; }
        public bool? DoesYourEventSupportStartup { get; set; }
        public bool? IsThereThirdPartyOrganizer { get; set; }
        public string OrgnizingCompany { get; set; }
        public string IdentityUserId { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public string LastModified { get; set; }
        //public DateTime? LastModifiedAt { get; set; }
        public bool? NeedConsultancy { get; set; }
        public string Platform { get; set; }
        public List<EventSpace_TimeDto> eventSpaceTimes { get; set; }
        public List<EventSpace_VenuesDto> eventSpaceVenues { get; set; }
        public InitiatedDto? Initiated { get; set; }
        public OpportunityStageDto? OpportunityStage { get; set; }
        public long? Revenue { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public EventOpportunityStatusDto? EventOpportunityStatus { get; set; }
        public int? LobLocationTypeId { get; set; }
        //public int pg_total { get; set; }
    }
}
