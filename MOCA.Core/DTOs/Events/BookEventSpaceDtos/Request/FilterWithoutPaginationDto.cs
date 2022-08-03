using System.ComponentModel.DataAnnotations;

namespace MOCA.Core.DTOs.Events.BookEventSpaceDtos.Request
{
    public class FilterWithoutPaginationDto
    {
        public long? Id { get; set; }
        public long? LocationNameId { get; set; }
        public long? EventRequesterId { get; set; }

        [MaxLength(800)]
        public string? CompanyCommericalName { get; set; }
        public long? IndustryNameId { get; set; }

        [MaxLength(800)]
        public string? OtherIndustryName { get; set; }

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
        public long InitiatedId { get; set; }
        public string? IdentityUserId { get; set; }
        public long? OpportunityStageId { get; set; }
        public long? Revenue { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public long? EventOpportunityStatusId { get; set; }
        public long LobLocationTypeId { get; set; }

    }
}
