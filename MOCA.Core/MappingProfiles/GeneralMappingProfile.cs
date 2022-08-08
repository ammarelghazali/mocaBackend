using AutoMapper;
using MMOCA.Core.DTOs.MocaSettings.CategoryDtos.Response;
using MOCA.Core.DTOs.MocaSettings.CaseTypesDtos.Response;
using MOCA.Core.DTOs.MocaSettings.CategoryDtos.Request;
using MOCA.Core.DTOs.MocaSettings.CategoryDtos.Response;
using MOCA.Core.DTOs.MocaSettings.FaqDtos.Request;
using MOCA.Core.DTOs.MocaSettings.FaqDtos.Response;
using MOCA.Core.DTOs.MocaSettings.IssueReportDtos.Request;
using MOCA.Core.DTOs.MocaSettings.IssueReportDtos.Response;
using MOCA.Core.DTOs.MocaSettings.PlanDtos.Request;
using MOCA.Core.DTOs.MocaSettings.PlanDtos.Response;
using MOCA.Core.DTOs.MocaSettings.PlanTypesDto.Request;
using MOCA.Core.DTOs.MocaSettings.PlanTypesDto.Response;
using MOCA.Core.DTOs.MocaSettings.PoliciesDtos.Response;
using MOCA.Core.DTOs.MocaSettings.PolicyTypesDtos.Response;
using MOCA.Core.DTOs.MocaSettings.PriorityDtos.Response;
using MOCA.Core.DTOs.MocaSettings.SeverityDtos.Request;
using MOCA.Core.DTOs.MocaSettings.SeverityDtos.Response;
using MOCA.Core.DTOs.MocaSettings.StatusDto.Request;
using MOCA.Core.DTOs.MocaSettings.StatusDto.Response;
using MOCA.Core.DTOs.MocaSettings.TopUpDtos.Request;
using MOCA.Core.DTOs.MocaSettings.TopUpDtos.Response;
using MOCA.Core.DTOs.MocaSettings.TopUpTypeDtos.Request;
using MOCA.Core.DTOs.MocaSettings.TopUpTypeDtos.Response;
using MOCA.Core.DTOs.MocaSettings.WifiDtos.Response;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.DTOs.LocationManagment.City;
using MOCA.Core.DTOs.LocationManagment.Country;
using MOCA.Core.DTOs.LocationManagment.Currency;
using MOCA.Core.DTOs.LocationManagment.District;
using MOCA.Core.DTOs.LocationManagment.LocationType;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.DTOs.LocationManagment.Feature;
using MOCA.Core.DTOs.LocationManagment.Inclusion;
using MOCA.Core.DTOs.LocationManagment.Industry;
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
using MOCA.Core.DTOs.MocaSettings.PolicyTypesDtos.Request;
using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.LocationManagment.FavouriteLocation;
using MOCA.Core.DTOs.LocationManagment.Building;
using MOCA.Core.DTOs.LocationManagment.BuildingFloor;
using MOCA.Core.DTOs.DynamicLists.WorkSpaceCategory;
using MOCA.Core.Entities.DynamicLists;


namespace MOCA.Core.MappingProfiles
{
    public class GeneralMappingProfile : Profile
    {
        public GeneralMappingProfile()
        {
            #region Moca Settings
            // Case Type 
            CreateMap<CaseType, CaseTypeDto>();

            // Category 
            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryWithFaqDto>();
            CreateMap<Category, Category>()
                .ForMember(c1 => c1.Id, c2 => c2.Ignore())
                .ForMember(c1 => c1.LastModifiedAt, c2 => c2.Ignore())
                .ForMember(c1 => c1.LastModifiedBy, c2 => c2.Ignore())
                .ForMember(c1 => c1.Name, c2 => c2.Ignore());

            // Faq 
            CreateMap<FaqForCreationDto, Faq>();
            CreateMap<Faq, FaqDto>();
            CreateMap<Faq, FaqBaseDto>();
            CreateMap<FaqForUpdateDto, Faq>();

            // Issue Report 
            CreateMap<IssueReportForCreationDto, IssueReport>();

            CreateMap<IssueReport, IssueReportDto>()
                .ForMember(i => i.CaseType, i2 => i2.MapFrom(i3 => i3.CaseType.Name))
                .ForMember(i => i.Priority, i2 => i2.MapFrom(i3 => i3.Priority.Name))
                .ForMember(i => i.Status, i2 => i2.MapFrom(i3 => i3.Status.Name))
                .ForMember(i => i.Severity, i2 => i2.MapFrom(i3 => i3.Severity.Name))
                .ForMember(i => i.SubmissionDate, i2 => i2.MapFrom(i3 => i3.SubmissionDate.ToShortDateString()))
                .ForMember(i => i.ClosureDuration,
                                i2 => i2.MapFrom(i3 => i3.ClosureDate == null ? "Not Closed Yet" :
                               ((i3.ClosureDate.Value.Date - i3.SubmissionDate.Date).TotalDays.ToString() == "1") ?
                                (i3.ClosureDate.Value.Date - i3.SubmissionDate.Date).TotalDays.ToString() + " Day" :
                                (i3.ClosureDate.Value.Date - i3.SubmissionDate.Date).TotalDays.ToString() + " Days"));

            CreateMap<IssueCaseStage, IssueCaseStagesDto>()
                .ForMember(i => i.Date, i2 => i2.MapFrom(i3 => ((DateTime)i3.LastModifiedAt).ToShortDateString()))
                .ForMember(i => i.Stage, i2 => i2.MapFrom(i3 => i3.IssueReport.Status.Name))
                .ForMember(i => i.Comment, i2 => i2.MapFrom(i3 => i3.IssueReport.Comment));


            CreateMap<UpdateIssueReportDto, IssueReport>().ReverseMap();

            // Plan 
            CreateMap<Plan, PlanDtoBase>()
               .ForMember(x => x.LobSpaceTypeId, opt => opt.Ignore())
               .ReverseMap();

            CreateMap<PlanDtoBase, CreationPlanDto>()
                .ReverseMap();

            CreateMap<UpdatePlanDto, PlanDtoBase>()
                .ReverseMap();

            // Plan Type 
            CreateMap<PlanType, PlanTypeDto>().ReverseMap();
            CreateMap<PlanType, PlanTypeForCreationDto>().ReverseMap();

            // Policies 
            CreateMap<Policy, Policy>()
                .ForMember(p1 => p1.Id, p2 => p2.Ignore())
                .ForMember(p1 => p1.LastModifiedAt, p2 => p2.Ignore())
                .ForMember(p1 => p1.LastModifiedBy, p2 => p2.Ignore())
                .ForMember(p1 => p1.Description, p2 => p2.Ignore())
                .ForMember(p1 => p1.IsDeleted, p2 => p2.Ignore());

            CreateMap<Policy, PolicyDto>();

            CreateMap<Policy, PolicyExtendedDto>()
                .ForMember(p1 => p1.PolicyTypeName, p2 => p2.MapFrom(p => p.PolicyType.Name));

            CreateMap<Policy, PolicyDtoMinimized>();

            // Policy Type 
            CreateMap<PolicyType, PolicyTypeDto>();
            CreateMap<PolicyType, PolicyTypeWithDescriptionDto>();
            CreateMap<PolicyTypeForCreationDto, PolicyType>();

            // Priority
            CreateMap<Priority, PriorityDto>();

            // Severity
            CreateMap<Severity, SeverityDto>().ReverseMap();
            CreateMap<Severity, SeverityForCreationDto>().ReverseMap();

            // Status
            CreateMap<Status, StatusDto>().ReverseMap();
            CreateMap<Status, StatusForCreationDto>().ReverseMap();

            // TopUp
            CreateMap<TopUp, TopUpDto>()
           .ForMember(dest => dest.TopUpType, opt => opt.MapFrom(src => src.TopUpType.Name));

            CreateMap<TopUp, TopUpCreateionDto>().ReverseMap();
            CreateMap<TopUp, UpdateTopUpDto>().ReverseMap();

            // TopUpType
            CreateMap<TopUpType, TopUpTypeDto>().ReverseMap();
            CreateMap<TopUpType, AddTopUpTypeDto>().ReverseMap();

            // Wifi
            CreateMap<Wifi, Wifi>().ForMember(p1 => p1.Id, p2 => p2.Ignore())
               .ForMember(p1 => p1.LastModifiedAt, p2 => p2.Ignore())
               .ForMember(p1 => p1.LastModifiedBy, p2 => p2.Ignore())
               .ForMember(p1 => p1.Description, p2 => p2.Ignore())
               .ForMember(p1 => p1.IsDeleted, p2 => p2.Ignore());

            CreateMap<Wifi, WifiDto>();
            #endregion


            #region Location Managment
            CreateMap<CountryModel, Country>();
            CreateMap<Country, CountryModel>();

            CreateMap<CityModel, City>();
            CreateMap<City, CityModel>();

            CreateMap<DistrictModel, District>();
            CreateMap<District, DistrictModel>();

            CreateMap<CurrencyModel, Currency>();
            CreateMap<Currency, CurrencyModel>();

            CreateMap<LocationTypeModel, LocationType>();
            CreateMap<LocationType, LocationTypeModel>();

            CreateMap<FeatureModel, Feature>();
            CreateMap<Feature, FeatureModel>();

            CreateMap<InclusionModel, Inclusion>();
            CreateMap<Inclusion, InclusionModel>();

            CreateMap<IndustryModel, Industry>();
            CreateMap<Industry, IndustryModel>();

            CreateMap<LocationBankAccountModel, LocationBankAccount>();
            CreateMap<LocationBankAccount, LocationBankAccountModel>();

            CreateMap<LocationContactModel, LocationContact>();
            CreateMap<LocationContact, LocationContactModel>();

            CreateMap<LocationCurrencyModel, LocationCurrency>();
            CreateMap<LocationCurrency, LocationCurrencyModel>();

            CreateMap<LocationFileModel, LocationFile>();
            CreateMap<LocationFile, LocationFileModel>();

            CreateMap<LocationImageModel, LocationImage>();
            CreateMap<LocationImage, LocationImageModel>();

            CreateMap<LocationInclusionModel, LocationInclusion>();
            CreateMap<LocationInclusion, LocationInclusionModel>();

            CreateMap<LocationWorkingHourModel, LocationWorkingHour>();
            CreateMap<LocationWorkingHour, LocationWorkingHourModel>();

            CreateMap<ServiceFeePaymentsDueDateModel, ServiceFeePaymentsDueDate>();
            CreateMap<ServiceFeePaymentsDueDate, ServiceFeePaymentsDueDateModel>();

            CreateMap<LocationModel, Location>();
            CreateMap<Location, LocationModel>();

            CreateMap<LocationModel, LocationDetailsModel>();
            CreateMap<LocationDetailsModel, LocationModel>();

            CreateMap<FavouriteLocationModel, FavouriteLocation>();
            CreateMap<FavouriteLocation, FavouriteLocationModel>();

            CreateMap<BuildingModel, Building>();
            CreateMap<Building, BuildingModel>();

            CreateMap<BuildingFloorModel, BuildingFloor>();
            CreateMap<BuildingFloor, BuildingFloorModel>();

            CreateMap<BuildingModelByLocationId, Building>();
            CreateMap<Building, BuildingModelByLocationId>();
            #endregion

            #region Dynamic Lists
            CreateMap<WorkSpaceCategoryModel, WorkSpaceCategory>();
            CreateMap<WorkSpaceCategory, WorkSpaceCategoryModel>();
            #endregion

            #region Events
            //EventSpaceBookings

            CreateMap<EventSpaceBooking, BooEventSpaceDto>()
                .ForMember(x => x.EventSpaceTimes, opt => opt.Ignore())
                .ForMember(x => x.EventSpaceVenues, opt => opt.Ignore());
            CreateMap<EventSpaceBooking, BooEventSpaceDto>()
                .ReverseMap()
                .ForMember(x => x.EventSpaceTimes, opt => opt.Ignore())
                .ForMember(x => x.EventSpaceVenues, opt => opt.Ignore());

            CreateMap<GetAllBookedEventSpaceResponseDto, EventSpaceBooking>();
            CreateMap<GetAllBookedEventSpaceResponseDto, EventSpaceBooking>().ReverseMap();

            // Event Attendance

            CreateMap<EventAttendance, GetAllEventAttendanceViewModel>();
            CreateMap<EventAttendance, GetAllEventAttendanceViewModel>().ReverseMap();
            CreateMap<EventAttendanceForCreationDto, EventAttendance>();
            CreateMap<EventAttendance, EventAttendanceDto>();

            // Event Category

            CreateMap<EventCategory, GetAllEventCategoryViewModel>();
            CreateMap<EventCategory, GetAllEventCategoryViewModel>().ReverseMap();
            CreateMap<EventCategory, EventCategoryDto>();

            // Event Type

            CreateMap<EventType, AllEventTypesDto>();
            CreateMap<EventType, AllEventTypesDto>().ReverseMap();
            CreateMap<EventType, EventTypeDto>();

            // Event Reccurance

            CreateMap<EventReccurance, GetAllEventReccuranceViewModel>();
            CreateMap<EventReccurance, GetAllEventReccuranceViewModel>().ReverseMap();
            CreateMap<EventReccurance, EventRecurrenceDto>();

            // Event Requester

            CreateMap<EventRequester, GetAllEventRequesterResponseDto>();
            CreateMap<EventRequester, GetAllEventRequesterResponseDto>().ReverseMap();
            CreateMap<EventRequester, EventRequesterDto>();

            // Event Opportunity
            CreateMap<EventSpaceBooking, cmdCreateNewEventOpportunityParameter>().ReverseMap()
                .ForMember(s => s.CompanyCommericalName, d => d.MapFrom(di => di.CompanyName));

            CreateMap<ContactDetails, EventOpportunityContactDetailViewModel>().ReverseMap();
            CreateMap<ContactDetails, EventOpportunityContactDetailsViewModel>();
            CreateMap<OpportunityStage, OpportunityStageDto>();
            CreateMap<EventOpportunityStatus, EventOpportunityStatusDto>();
            CreateMap<Initiated, InitiatedDto>();
            CreateMap<EventSpaceTime, DTOs.Events.EventOpportunityDtos.Response.EventSpaceTimeDto>();
            CreateMap<EventSpaceVenues, DTOs.Events.EventOpportunityDtos.Response.EventSpaceVenuesDto>();


            // Email Templete
            CreateMap<EmailTemplate, GetEmailTempleteEventOpportunitylViewModelDto>().ReverseMap();
            #endregion

        }
    }

}
