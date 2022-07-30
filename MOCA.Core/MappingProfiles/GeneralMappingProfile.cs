using AutoMapper;
using MOCA.Core.DTOs.Events.BookEventSpaceDtos.Request;
using MOCA.Core.DTOs.Events.BookEventSpaceDtos.Response;
using MOCA.Core.DTOs.Events.EventAttendanceDtos.Request;
using MOCA.Core.DTOs.Events.EventAttendanceDtos.Response;
using MOCA.Core.DTOs.Events.EventCategoryDtos.Response;
using MOCA.Core.DTOs.Events.EventOpportunityDtos.Request;
using MOCA.Core.DTOs.Events.EventOpportunityDtos.Response;
using MOCA.Core.DTOs.Events.EventReccuranceDtos.Response;
using MOCA.Core.DTOs.Events.EventRequesterDtos.Response;
using MOCA.Core.DTOs.Events.EventsOpportunitiesDtos.Response;
using MOCA.Core.DTOs.Events.EventTypeDtos.Response;
using MOCA.Core.DTOs.Events.Response;
using MOCA.Core.Entities.EventSpaceBookings;

namespace MOCA.Core.MappingProfiles
{

    public class GeneralMappingProfile : Profile
    {
        public GeneralMappingProfile()
        {
            #region Moca Settings

            #endregion

            #region Events
            //EventSpaceBookings

            CreateMap<EventSpaceBooking, BooEventSpaceDto>()
                .ForMember(x => x.eventSpace_Times, opt => opt.Ignore())
                .ForMember(x => x.eventSpace_Venues, opt => opt.Ignore());
            CreateMap<EventSpaceBooking, BooEventSpaceDto>()
                .ReverseMap()
                .ForMember(x => x.EventSpaceTimes, opt => opt.Ignore())
                .ForMember(x => x.EventSpaceVenues, opt => opt.Ignore());

            CreateMap<GetAllBookedEventSpaceResponseDto, EventSpaceBooking>();
            CreateMap<GetAllBookedEventSpaceResponseDto, EventSpaceBooking>().ReverseMap();

            // Event Attendance

            CreateMap<EventAttendance, get_AllEventAttendance_ViewModel>();
            CreateMap<EventAttendance, get_AllEventAttendance_ViewModel>().ReverseMap();
            CreateMap<EventAttendanceForCreationDto, EventAttendance>();
            CreateMap<EventAttendance, EventAttendanceDto>();

            // Event Category

            CreateMap<EventCategory, get_AllEventCategory_ViewModel>();
            CreateMap<EventCategory, get_AllEventCategory_ViewModel>().ReverseMap();
            CreateMap<EventCategory, EventCategoryDto>();

            // Event Type

            CreateMap<EventType, AllEventTypesDto>();
            CreateMap<EventType, AllEventTypesDto>().ReverseMap();
            CreateMap<EventType, EventTypeDto>();

            // Event Reccurance

            CreateMap<EventReccurance, get_AllEventReccurance_ViewModel>();
            CreateMap<EventReccurance, get_AllEventReccurance_ViewModel>().ReverseMap();
            CreateMap<EventReccurance, EventRecurrenceDto>();

            // Event Requester

            CreateMap<EventRequester, GetAllEventRequesterResponseDto>();
            CreateMap<EventRequester, GetAllEventRequesterResponseDto>().ReverseMap();
            CreateMap<EventRequester, EventRequesterDto>();

            // Event Opportunity
            CreateMap<EventSpaceBooking, cmd_Create_NewEventOpportunity_Parameter>().ReverseMap()
                .ForMember(s => s.CompanyCommericalName, d => d.MapFrom(di => di.CompanyName));

            CreateMap<ContactDetails, EventOpportunityContactDetail_ViewModel>().ReverseMap();
            CreateMap<ContactDetails, EventOpportunityContactDetails_ViewModel>();
            CreateMap<OpportunityStage, OpportunityStageDto>();
            CreateMap<EventOpportunityStatus, EventOpportunityStatusDto>();
            CreateMap<Initiated, InitiatedDto>();
            CreateMap<EventSpaceTime, EventSpace_TimeDto>();
            CreateMap<EventSpaceVenues, EventSpace_VenuesDto>();


            // Email Templete
            CreateMap<EmailTemplate, GetEmailTempleteEventOpportunitylViewModelDto>().ReverseMap();

            #endregion

        }
    }

}
