using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MOCA.Core;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Base;
using MOCA.Core.Interfaces.Events;
using MOCA.Core.Interfaces.Events.Repositories;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;
using MOCA.Presistence.Repositories.Events;

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

        #endregion


        #region Events

        private IGenericRepository<EventType> _eventTypeRepo;
        private IGenericRepository<EventCategory> _eventCategoryRepo;
        private IGenericRepository<EventRequester> _eventRequesterRepo;
        private IGenericRepository<EventAttendance> _eventAttendanceRepo;
        private IGenericRepository<EventReccurance> _eventReccuranceRepo;
        private ILocationsMemberShipsRepository _locationsMemberShipsRepo;
        private IEmailTemplateRepository _emailTemplateRepository;
        private IEventOpportunityStatusRepository _eventOpportunityStatusRepo;
        private IOpportunityStageReportRepository _opportunityStageReportRepo;
        private IEventSpaceBookingRepository _eventSpaceBookingRepository;
        private IOpportunityStageRepository _opportunityStageRepo;
        private IEventSpaceTimesRepository _eventSpaceTimesRepo;
        private IEventSpaceVenueRepository _eventSpaceVenuesRepo;
        private IContactDetailsRepository _contactDetailsRepo;
        private ILocationRepositoty _locationRepo;
        private IIndustryRepository _industryRepo;
        private IInitiatedRepository _initiatedRepo;
        private ILoungeClientRepository _loungeClientRepo;
        private ISendEmailRepository _sendEmailRepo;
        private IUserService _userService;
        private IAccountService _accountService;
        public IGenericRepository<EventRequester> EventRequesterRepo
        {
            get
            {
                return this._eventRequesterRepo = this._eventRequesterRepo ?? new GenericRepository<EventRequester>(context);
            }
        }
        public IGenericRepository<EventAttendance> EventAttendanceRepo
        {
            get
            {
                return this._eventAttendanceRepo = this._eventAttendanceRepo ?? new GenericRepository<EventAttendance>(context);
            }
        }
        public IGenericRepository<EventType> EventTypeRepo
        {
            get
            {
                return this._eventTypeRepo = this._eventTypeRepo ?? new GenericRepository<EventType>(context);
            }
        }
        public IGenericRepository<EventReccurance> EventReccuranceRepo
        {
            get
            {
                return this._eventReccuranceRepo = this._eventReccuranceRepo ?? new GenericRepository<EventReccurance>(context);
            }
        }

        public IGenericRepository<EventCategory> EventCategoryRepo
        {
            get
            {
                return this._eventCategoryRepo = this._eventCategoryRepo ?? new GenericRepository<EventCategory>(context);
            }
        }
        public IContactDetailsRepository ContactDetailsRepo
        {
            get
            {
                return this._contactDetailsRepo = this._contactDetailsRepo ?? new ContactDetailsRepository(context);
            }
        }
        public IEventSpaceBookingRepository EventSpaceBookingRepo
        {
            get
            {
                return this._eventSpaceBookingRepository = this._eventSpaceBookingRepository ??
                                                                  new EventSpaceBookingRepository(context);
            }
        }
        public IEventSpaceTimesRepository EventSpaceTimesRepo
        {
            get
            {
                return this._eventSpaceTimesRepo = this._eventSpaceTimesRepo ?? new EventSpaceTimesRepository(context);
            }
        }
        public IEventSpaceVenueRepository EventSpaceVenuesRepo
        {
            get
            {
                return this._eventSpaceVenuesRepo = this._eventSpaceVenuesRepo ?? new EventSpaceVenueRepository(context);
            }
        }
        public ISendEmailRepository SendEmailRepo
        {
            get
            {
                return this._sendEmailRepo = this._sendEmailRepo ?? new SendEmailRepository(context);
            }
        }
        public IInitiatedRepository InitiatedRepo
        {
            get
            {
                return this._initiatedRepo = this._initiatedRepo ?? new InitiatedRepository(context);
            }
        }
        public IOpportunityStageReportRepository OpportunityStageReportRepo
        {
            get
            {
                return this._opportunityStageReportRepo = this._opportunityStageReportRepo ?? new OpportunityStageReportRepository(context);
            }
        }
        public IOpportunityStageRepository OpportunityStageRepo
        {
            get
            {
                return this._opportunityStageRepo = this._opportunityStageRepo ?? new OpportunityStageRepository(context);
            }
        }
        public IEventOpportunityStatusRepository EventOpportunityStatusRepo
        {
            get
            {
                return this._eventOpportunityStatusRepo = this._eventOpportunityStatusRepo ?? new EventOpportunityStatusRepository(context);
            }
        }
        public ILocationRepositoty LocationRepo
        {
            get
            {////////////////// All Configurations must be replaced with context after using DB
                return this._locationRepo = this._locationRepo ?? new LocationRepositoty(Configuration);
            }
        }
        public IIndustryRepository IndustryRepo
        {
            get
            {
                return this._industryRepo = this._industryRepo ?? new IndustryRepository(Configuration);
            }
        }
        public IAccountService AccountService
        {
            get
            {
                return this._accountService = this._accountService ?? new AccountService(Configuration);
            }
        }
        public IUserService UserService
        {
            get
            {
                return this._userService = this._userService ?? new UserService(Configuration);
            }
        }
        public ILoungeClientRepository LoungeClientRepo
        {
            get
            {
                return this._loungeClientRepo = this._loungeClientRepo ?? new LoungeClientRepository(Configuration);
            }
        }
        public ILocationsMemberShipsRepository LocationsMemberShipsRepo
        {
            get
            {
                return this._locationsMemberShipsRepo = this._locationsMemberShipsRepo ?? new LocationsMemberShipsRepository(Configuration);
            }
        }
        public IEmailTemplateRepository EmailTemplateRepository
        {
            get
            {
                return this._emailTemplateRepository = this._emailTemplateRepository ?? new EmailTemplateRepository(Configuration, _authenticatedUser, _dateTimeService);
            }
        }


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
