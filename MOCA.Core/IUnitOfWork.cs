using Microsoft.EntityFrameworkCore;
using MOCA.Core.Interfaces.MocaSettings.Repositories;

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

        void Save();
        Task<int> SaveAsync();
        DateTime? GetServerDate();
        DateTime ConvertToLocalDate(DateTime dateInEasternTimeZone);

        DbContext contextForTransaction { get; }
    }
}
