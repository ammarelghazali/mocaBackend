using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Base;
using MOCA.Core.Interfaces.Events;
using MOCA.Core.Interfaces.Events.Repositories;
using MOCA.Core.Interfaces.MocaSettings.Repositories;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Core.Interfaces.WorkSpaceReservations.Repositories;

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

        #region WorkSpaceReservations
        public IWorkSpaceReservationsRepositoryCRM WorkSpaceReservationsRepositoryCRM { get; }
        #endregion

        void Save();
        Task<int> SaveAsync();
        DateTime? GetServerDate();
        DateTime ConvertToLocalDate(DateTime dateInEasternTimeZone);

        DbContext contextForTransaction { get; }
    }
}
