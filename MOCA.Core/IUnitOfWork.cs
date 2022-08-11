using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Base;
using MOCA.Core.Interfaces.Events;
using MOCA.Core.Interfaces.Events.Repositories;
using MOCA.Core.Interfaces.MocaSettings.Repositories;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Core.Interfaces.MeetingSpaceReservations.Repositories;
using MOCA.Core.Interfaces.SSO.Repositories;
using MOCA.Core.Interfaces.MeetingSpaceReservations.Repositories;
using MOCA.Core.Entities.DynamicLists;
using MOCA.Core.Interfaces.DynamicLists.Repositories;
using MOCA.Core.Interfaces.WorkSpaceReservations.WorkSpaces.Repositories;
using MOCA.Core.Interfaces.WorkSpaceReservations.CoworkSpace.Repositories;

namespace MOCA.Core
{
    public interface IUnitOfWork : IDisposable
    {
        #region Moca Settings
        ICategoriesRepository Categories { get; }

        IFaqsRepository Faqs { get; }

        IPlansRepository Plans { get; }

        IPlanTypesRepository PlanTypes { get; }

        ITopUpsRespository TopUps { get; }

        ITopUpTypesRepository TopUpTypes { get; }

        IPolicyTypesRepository PolicyTypes { get; }

        IPolicyRepository Policies { get; }

        //ILobSpaceTypesRepository LobSpaceTypes { get; }

        IWifisRepository Wifis { get; }

        IStatusesRepository Statuses { get; }

        ISeveritiesRepository Severities { get; }

        IPrioritiesRepository Priorities { get; }

        ICaseTypesReository CaseTypes { get; }

        IIssueReportsRepository IssueReports { get; }

        //IIdentityUserRepository Users { get; }
        #endregion

        #region Location Managment

        IGenericRepository<Country> CountryRepo { get; }
        ICountryRepository CountryRepoEF { get; }
        IGenericRepository<City> CityRepo { get; }
        ICityRepository CityRepoEF { get; }
        IGenericRepository<District> DistrictRepo { get; }
        IDistrictRepository DistrictRepoEF { get; }
        IGenericRepository<Currency> CurrencyRepo { get; }
        ICurrencyRepository CurrencyRepoEF { get; }
        IGenericRepository<LocationType> LocationTypeRepo { get; }
        ILocationTypeRepository LocationTypeRepoEF { get; }
        IGenericRepository<Feature> FeatureRepo { get; }
        IFeatureRepository FeatureRepoEF { get; }
        IGenericRepository<Inclusion> InclusionRepo { get; }
        IInclusionRepository InclusionRepoEF { get; }
        IGenericRepository<Industry> IndustryRepo { get; }
        IIndustryRepository IndustryRepoEF { get; }
        IGenericRepository<LocationBankAccount> LocationBankAccountRepo { get; }
        ILocationBankAccountRepository LocationBankAccountRepoEF { get; }
        IGenericRepository<LocationContact> LocationContactRepo { get; }
        ILocationContactRepository LocationContactRepoEF { get; }
        IGenericRepository<LocationCurrency> LocationCurrencyRepo { get; }
        ILocationCurrencyRepository LocationCurrencyRepoEF { get; }
        IGenericRepository<LocationFile> LocationFileRepo { get; }
        ILocationFileRepository LocationFileRepoEF { get; }
        IGenericRepository<LocationImage> LocationImageRepo { get; }
        ILocationImageRepository LocationImageRepoEF { get; }
        IGenericRepository<LocationInclusion> LocationInclusionRepo { get; }
        ILocationInclusionRepository LocationInclusionRepoEF { get; }
        IGenericRepository<LocationWorkingHour> LocationWorkingHourRepo { get; }
        ILocationWorkingHourRepository LocationWorkingHourRepoEF { get; }
        IGenericRepository<ServiceFeePaymentsDueDate> ServiceFeePaymentsDueDateRepo { get; }
        IServiceFeePaymentsDueDateRepository ServiceFeePaymentsDueDateRepoEF { get; }
        IGenericRepository<Location> LocationRepo { get; }
        ILocationRepository LocationRepoEF { get; }
        IGenericRepository<FavouriteLocation> FavouriteLocationRepo { get; }
        IFavouriteLocationRepository FavouriteLocationRepoEF { get; }
        IGenericRepository<Building> BuildingRepo { get; }
        IBuildingRepository BuildingRepoEF { get; }
        IGenericRepository<BuildingFloor> BuildingFloorRepo { get; }
        IBuildingFloorRepository BuildingFloorRepoEF { get; }

        #endregion

        #region Events
        IGenericRepository<EventRequester> EventRequesterRepo { get; }
        IGenericRepository<EventCategory> EventCategoryRepo { get; }
        IGenericRepository<EventReccurance> EventReccuranceRepo { get; }
        IGenericRepository<EventAttendance> EventAttendanceRepo { get; }
        IGenericRepository<EventType> EventTypeRepo { get; }
        IContactDetailsRepository ContactDetailsRepo { get; }
        IEventSpaceBookingRepository EventSpaceBookingRepo { get; }
        IEventSpaceTimesRepository EventSpaceTimesRepo { get; }
        IEventSpaceVenueRepository EventSpaceVenuesRepo { get; }
        ISendEmailRepository SendEmailRepo { get; }
        IInitiatedRepository InitiatedRepo { get; }
        IOpportunityStageReportRepository OpportunityStageReportRepo { get; }
        IOpportunityStageRepository OpportunityStageRepo { get; }
        IEventOpportunityStatusRepository EventOpportunityStatusRepo { get; }
        ILocationsMemberShipsRepository LocationsMemberShipsRepo { get; }
        IEmailTemplateRepository EmailTemplateRepository { get; }

        #endregion

        #region SSO

        IBasicUserRepository BasicUserRepository { get; }

        IBasicUserStatusHistoryRepository BasicUserStatusHistoryRepository { get; }

        IClientDeviceRepository ClientDeviceRepository { get; }

        IMemberShipTypesRepository MemberShipTypesRepository { get; }

        IMemberShipMainCategoriesRepository  MemberShipMainCategoriesRepository { get; }

        IMemberShipCategoriesRepository MemberShipCategoriesRepository { get; }

        IMemberShipBenefitsTypesRepository MemberShipBenefitsTypesRepository { get; }

        IGenderRepository GenderRepository { get; }

        #endregion 

        #region WorkSpaceReservations
        public IWorkSpaceReservationsRepositoryCRM WorkSpaceReservationsRepositoryCRM { get; }
        public IWorkSpaceReservationBundleRepo WorkSpaceReservationBundleRepo { get; }
        public IWorkSpaceReservationHourlyRepo WorkSpaceReservationHourlyRepo { get; }
        public IWorkSpaceReservationTailoredRepo WorkSpaceReservationTailoredRepo { get; }
        public IWorkSpaceHourlyTopUpRepo WorkSpaceHourlyTopUpRepo { get; }
        public IWorkSpaceTailoredTopUpRepo WorkSpaceTailoredTopUpRepo { get; }
        #endregion

        #region CoworkingSpaceReservations
        public ICoworkSpaceReservationsRepositoryCRM CoworkSpaceReservationsRepositoryCRM { get; }
        public ICoworkSpaceReservationBundleRepo CoworkSpaceReservationBundleRepo { get; }
        public ICoworkSpaceReservationHourlyRepo CoworkSpaceReservationHourlyRepo { get; }
        public ICoworkSpaceReservationTailoredRepo CoworkSpaceReservationTailoredRepo { get; }
        public ICoworkSpaceHourlyTopUpRepo CoworkSpaceHourlyTopUpRepo { get; }
        public ICoworkSpaceTailoredTopUpRepo CoworkSpaceTailoredTopUpRepo { get; }
        #endregion

        #region MeetingSpaceReservations
        public IMeetingSpaceReservationRepository MeetingSpaceReservationRepository { get; }
        #endregion


        #region Dynamic Lists
        IGenericRepository<WorkSpaceCategory> WorkSpaceCategoryRepo { get; }
        IWorkSpaceCategoryRepository WorkSpaceCategoryRepoEF { get; }

        IGenericRepository<WorkSpaceType> WorkSpaceTypeRepo { get; }
        IWorkSpaceTypeRepository WorkSpaceTypeRepoEF { get; }

        #endregion
        void Save();
        Task<int> SaveAsync();
        DateTime? GetServerDate();
        DateTime ConvertToLocalDate(DateTime dateInEasternTimeZone);

        DbContext contextForTransaction { get; }
    }
}
