using Microsoft.EntityFrameworkCore;
using MOCA.Core.Interfaces.MocaSettings.Repositories;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.Base;
using MOCA.Core.Interfaces.LocationManagment.Repositories;

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
        #endregion

        void Save();
        Task<int> SaveAsync();
        DateTime? GetServerDate();
        DateTime ConvertToLocalDate(DateTime dateInEasternTimeZone);

        DbContext contextForTransaction { get; }
    }
}
