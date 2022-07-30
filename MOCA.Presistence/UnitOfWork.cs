using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MOCA.Core;
using MOCA.Core.Interfaces.MocaSettings.Repositories;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.MocaSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Presistence
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        public ApplicationDbContext context { get; }
        public IConfiguration Configuration { get; }
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IDateTimeService _dateTimeService;

        DbContext IUnitOfWork.contextForTransaction
        {
            get
            {
                return context;
            }
        }

        public UnitOfWork(ApplicationDbContext _context, IConfiguration configuration,
                          IAuthenticatedUserService authenticatedUser, IDateTimeService dateTimeService)
        {
            context = _context;
            Configuration = configuration;
            _authenticatedUser = authenticatedUser;
            _dateTimeService = dateTimeService;
        }

        #region Moca Settings
        private ICategoriesRepository _categories;
        public ICategoriesRepository Categories
        {
            get
            {
                return _categories ?? new CategoriesRepository(context);
            }
        }

        private IFaqsRepository _faqs;
        public IFaqsRepository Faqs
        {
            get
            {
                return _faqs ?? new FaqsRepository(context);
            }
        }

        private IPlansRepository _plans;
        public IPlansRepository Plans
        {
            get
            {
                return _plans ?? new PlansRepository(context);
            }
        }

        private IPlanTypesRepository _planTypes;
        public IPlanTypesRepository PlanTypes
        {
            get
            {
                return _planTypes ?? new PlanTypesRepository(context);
            }
        }

        private ITopUpsRespository _topUps;
        public ITopUpsRespository TopUps
        {
            get
            {
                return _topUps ?? new TopUpsRepository(context);
            }
        }

        private ITopUpTypesRepository _topUpTypes;
        public ITopUpTypesRepository TopUpTypes
        {
            get
            {
                return _topUpTypes ?? new TopUpTypesRepository(context);
            }
        }

        private IPolicyTypesRepository _policyTypes;
        public IPolicyTypesRepository PolicyTypes
        {
            get
            {
                return _policyTypes ?? new PolicyTypesRepository(context);
            }
        }

        private IPolicyRepository _policies;
        public IPolicyRepository Policies
        {
            get
            {
                return _policies ?? new PolicyRepository(context);
            }
        }

        //public ILobSpaceTypesRepository LobSpaceTypes => throw new NotImplementedException();

        private IWifisRepository _wifis;
        public IWifisRepository Wifis
        {
            get
            {
                return _wifis ?? new WifisRepository(context);
            }
        }

        private IStatusesRepository _statuses;
        public IStatusesRepository Statuses
        {
            get
            {
                return _statuses ?? new StatusesRepository(context);
            }
        }

        private ISeveritiesRepository _severities;
        public ISeveritiesRepository Severities
        {
            get
            {
                return _severities ?? new SeveritiesRepository(context);
            }
        }

        private IPrioritiesRepository _priorities;
        public IPrioritiesRepository Priorities
        {
            get
            {
                return _priorities ?? new PrioritiesRepository(context);
            }
        }

        private ICaseTypesReository _caseTypes;
        public ICaseTypesReository CaseTypes
        {
            get
            {
                return _caseTypes ?? new CaseTypesRepository(context);
            }
        }

        private IIssueReportsRepository _issueReports;
        public IIssueReportsRepository IssueReports
        {
            get
            {
                return _issueReports ?? new IssueReportsRepository(context);
            }
        }

        //public IIdentityUserRepository Users => throw new NotImplementedException();
        #endregion

        public DateTime ConvertToLocalDate(DateTime dateInEasternTimeZone)
        {
            #region convert american time to utc time
            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime UTCdateTime = TimeZoneInfo.ConvertTimeToUtc(dateInEasternTimeZone, easternZone);
            #endregion

            #region convert utc time to egyptian time
            TimeZoneInfo egyZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            DateTime EgyptdateTime = TimeZoneInfo.ConvertTimeFromUtc(UTCdateTime, egyZone);
            #endregion

            return EgyptdateTime;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public DateTime? GetServerDate()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
