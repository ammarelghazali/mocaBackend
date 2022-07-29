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
        public ICategoriesRepository Categories
        {
            get
            {
                return this.Categories ?? new CategoriesRepository(context);
            }
        }

        public IFaqsRepository Faqs
        {
            get
            {
                return this.Faqs ?? new FaqsRepository(context);
            }
        }

        public IPlansRepository Plans
        {
            get
            {
                return this.Plans ?? new PlansRepository(context);
            }
        }

        public IPlanTypesRepository PlanTypes
        {
            get
            {
                return this.PlanTypes ?? new PlanTypesRepository(context);
            }
        }

        public ITopUpsRespository TopUps
        {
            get
            {
                return this.TopUps ?? new TopUpsRepository(context);
            }
        }

        public ITopUpTypesRepository TopUpTypes
        {
            get
            {
                return this.TopUpTypes ?? new TopUpTypesRepository(context);
            }
        }

        public IPolicyTypesRepository PolicyTypes
        {
            get
            {
                return this.PolicyTypes ?? new PolicyTypesRepository(context);
            }
        }

        public IPolicyRepository Policies
        {
            get
            {
                return this.Policies ?? new PolicyRepository(context);
            }
        }

        //public ILobSpaceTypesRepository LobSpaceTypes => throw new NotImplementedException();

        public IWifisRepository Wifis
        {
            get
            {
                return this.Wifis ?? new WifisRepository(context);
            }
        }

        public IStatusesRepository Statuses
        {
            get
            {
                return this.Statuses ?? new StatusesRepository(context);
            }
        }

        public ISeveritiesRepository Severities
        {
            get
            {
                return this.Severities ?? new SeveritiesRepository(context);
            }
        }

        public IPrioritiesRepository Priorities
        {
            get
            {
                return this.Priorities ?? new PrioritiesRepository(context);
            }
        }

        public ICaseTypesReository CaseTypes
        {
            get
            {
                return this.CaseTypes ?? new CaseTypesRepository(context);
            }
        }

        public IIssueReportsRepository IssueReports
        {
            get
            {
                return this.IssueReports ?? new IssueReportsRepository(context);
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
