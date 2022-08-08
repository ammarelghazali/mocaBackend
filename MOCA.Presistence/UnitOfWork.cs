using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MOCA.Core;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Interfaces.Base;
using MOCA.Core.Interfaces.Events;
using MOCA.Core.Interfaces.Events.Repositories;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Core.Interfaces.MocaSettings.Repositories;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Presistence.Contexts;
using MOCA.Presistence.Repositories.Base;
using MOCA.Presistence.Repositories.Events;
using MOCA.Presistence.Repositories.LocationManagment;
using MOCA.Presistence.Repositories.MocaSettings;
using MOCA.Core.Interfaces.WorkSpaceReservations.Repositories;
using MOCA.Presistence.Repositories.WorkSpaceReservations;
using MOCA.Core.Interfaces.MeetingSpaceReservations.Repositories;
using MOCA.Presistence.Repositories.MeetingSpaceReservations;
using MOCA.Core.Interfaces.SSO.Repositories;
using MOCA.Core.Interfaces.MeetingSpaceReservations.Repositories;
using MOCA.Presistence.Repositories.MeetingSpaceReservations;

namespace MOCA.Presistence
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        public ApplicationDbContext _context { get; }
        public IConfiguration _configuration { get; }
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IDateTimeService _dateTimeService;
        private readonly IReservationsStatusService _reservationsStatusService;

        DbContext IUnitOfWork.contextForTransaction
        {
            get
            {
                return _context;
            }
        }

        public UnitOfWork(ApplicationDbContext context,
            IConfiguration configuration,
            IAuthenticatedUserService authenticatedUser,
            IDateTimeService dateTimeService, IReservationsStatusService reservationsStatusService)
        {
            _context = context;
            _configuration = configuration;
            _authenticatedUser = authenticatedUser;
            _dateTimeService = dateTimeService;
            _reservationsStatusService = reservationsStatusService;
        }

        #region Moca Settings
        private ICategoriesRepository _categories;
        public ICategoriesRepository Categories
        {
            get
            {
                return _categories ?? new CategoriesRepository(_context);
            }
        }

        private IFaqsRepository _faqs;
        public IFaqsRepository Faqs
        {
            get
            {
                return _faqs ?? new FaqsRepository(_context);
            }
        }

        private IPlansRepository _plans;
        public IPlansRepository Plans
        {
            get
            {
                return _plans ?? new PlansRepository(_context);
            }
        }

        private IPlanTypesRepository _planTypes;
        public IPlanTypesRepository PlanTypes
        {
            get
            {
                return _planTypes ?? new PlanTypesRepository(_context);
            }
        }

        private ITopUpsRespository _topUps;
        public ITopUpsRespository TopUps
        {
            get
            {
                return _topUps ?? new TopUpsRepository(_context);
            }
        }

        private ITopUpTypesRepository _topUpTypes;
        public ITopUpTypesRepository TopUpTypes
        {
            get
            {
                return _topUpTypes ?? new TopUpTypesRepository(_context);
            }
        }

        private IPolicyTypesRepository _policyTypes;
        public IPolicyTypesRepository PolicyTypes
        {
            get
            {
                return _policyTypes ?? new PolicyTypesRepository(_context);
            }
        }

        private IPolicyRepository _policies;
        public IPolicyRepository Policies
        {
            get
            {
                return _policies ?? new PolicyRepository(_context);
            }
        }

        //public ILobSpaceTypesRepository LobSpaceTypes => throw new NotImplementedException();

        private IWifisRepository _wifis;
        public IWifisRepository Wifis
        {
            get
            {
                return _wifis ?? new WifisRepository(_context);
            }
        }

        private IStatusesRepository _statuses;
        public IStatusesRepository Statuses
        {
            get
            {
                return _statuses ?? new StatusesRepository(_context);
            }
        }

        private ISeveritiesRepository _severities;
        public ISeveritiesRepository Severities
        {
            get
            {
                return _severities ?? new SeveritiesRepository(_context);
            }
        }

        private IPrioritiesRepository _priorities;
        public IPrioritiesRepository Priorities
        {
            get
            {
                return _priorities ?? new PrioritiesRepository(_context);
            }
        }

        private ICaseTypesReository _caseTypes;
        public ICaseTypesReository CaseTypes
        {
            get
            {
                return _caseTypes ?? new CaseTypesRepository(_context);
            }
        }

        private IIssueReportsRepository _issueReports;
        public IIssueReportsRepository IssueReports
        {
            get
            {
                return _issueReports ?? new IssueReportsRepository(_context);
            }
        }

        //public IIdentityUserRepository Users => throw new NotImplementedException();
        #endregion


        #region Events

        private IGenericRepository<EventType> _eventTypeRepo;
        private IGenericRepository<EventCategory> _eventCategoryRepo;
        private IGenericRepository<EventRequester> _eventRequesterRepo;
        private IGenericRepository<EventAttendance> _eventAttendanceRepo;
        private IGenericRepository<EventReccurance> _eventReccuranceRepo;
        private ILocationsMemberShipsRepository _locationsMemberShipsRepo;
        private IEventOpportunityStatusRepository _eventOpportunityStatusRepo;
        private IOpportunityStageReportRepository _opportunityStageReportRepo;
        private IEventSpaceBookingRepository _eventSpaceBookingRepository;
        private IOpportunityStageRepository _opportunityStageRepo;
        private IEventSpaceTimesRepository _eventSpaceTimesRepo;
        private IEventSpaceVenueRepository _eventSpaceVenuesRepo;
        private IContactDetailsRepository _contactDetailsRepo;
        private IInitiatedRepository _initiatedRepo;
        private ILoungeClientRepository _loungeClientRepo;
        private ISendEmailRepository _sendEmailRepo;
        private IUserService _userService;
        private IAccountService _accountService;
        private IEmailTemplateRepository _emailTemplateRepo;

        public IEmailTemplateRepository EmailTemplateRepository
        {
            get
            {
                return this._emailTemplateRepo = this._emailTemplateRepo ?? new EmailTemplateRepository(_context);
            }
        }
        public IGenericRepository<EventRequester> EventRequesterRepo
        {
            get
            {
                return this._eventRequesterRepo = this._eventRequesterRepo ?? new GenericRepository<EventRequester>(_context);
            }
        }
        public IGenericRepository<EventAttendance> EventAttendanceRepo
        {
            get
            {
                return this._eventAttendanceRepo = this._eventAttendanceRepo ?? new GenericRepository<EventAttendance>(_context);
            }
        }
        public IGenericRepository<EventType> EventTypeRepo
        {
            get
            {
                return this._eventTypeRepo = this._eventTypeRepo ?? new GenericRepository<EventType>(_context);
            }
        }
        public IGenericRepository<EventReccurance> EventReccuranceRepo
        {
            get
            {
                return this._eventReccuranceRepo = this._eventReccuranceRepo ?? new GenericRepository<EventReccurance>(_context);
            }
        }

        public IGenericRepository<EventCategory> EventCategoryRepo
        {
            get
            {
                return this._eventCategoryRepo = this._eventCategoryRepo ?? new GenericRepository<EventCategory>(_context);
            }
        }
        public IContactDetailsRepository ContactDetailsRepo
        {
            get
            {
                return this._contactDetailsRepo = this._contactDetailsRepo ?? new ContactDetailsRepository(_context);
            }
        }
        public IEventSpaceBookingRepository EventSpaceBookingRepo
        {
            get
            {
                return this._eventSpaceBookingRepository = this._eventSpaceBookingRepository ??
                                                                  new EventSpaceBookingRepository(_context);
            }
        }
        public IEventSpaceTimesRepository EventSpaceTimesRepo
        {
            get
            {
                return this._eventSpaceTimesRepo = this._eventSpaceTimesRepo ?? new EventSpaceTimesRepository(_context);
            }
        }
        public IEventSpaceVenueRepository EventSpaceVenuesRepo
        {
            get
            {
                return this._eventSpaceVenuesRepo = this._eventSpaceVenuesRepo ?? new EventSpaceVenueRepository(_context);
            }
        }
        public ISendEmailRepository SendEmailRepo
        {
            get
            {
                return this._sendEmailRepo = this._sendEmailRepo ?? new SendEmailRepository(_context);
            }
        }
        public IInitiatedRepository InitiatedRepo
        {
            get
            {
                return this._initiatedRepo = this._initiatedRepo ?? new InitiatedRepository(_context);
            }
        }
        public IOpportunityStageReportRepository OpportunityStageReportRepo
        {
            get
            {
                return this._opportunityStageReportRepo = this._opportunityStageReportRepo ?? new OpportunityStageReportRepository(_context);
            }
        }
        public IOpportunityStageRepository OpportunityStageRepo
        {
            get
            {
                return this._opportunityStageRepo = this._opportunityStageRepo ?? new OpportunityStageRepository(_context);
            }
        }
        public IEventOpportunityStatusRepository EventOpportunityStatusRepo
        {
            get
            {
                return this._eventOpportunityStatusRepo = this._eventOpportunityStatusRepo ?? new EventOpportunityStatusRepository(_context);
            }
        }
        public ILocationsMemberShipsRepository LocationsMemberShipsRepo
        {
            get
            {
                return this._locationsMemberShipsRepo = this._locationsMemberShipsRepo ?? new LocationsMemberShipsRepository(_configuration);
            }
        }


        #endregion

        #region WorkSpaceReservations
        private IWorkSpaceReservationsRepositoryCRM _workSpaceReservationCRM;
        public IWorkSpaceReservationsRepositoryCRM WorkSpaceReservationsRepositoryCRM 
        {
            get
            {
                return _workSpaceReservationCRM ?? new WorkSpaceReservationsRepositoryCRM(_context);
            }
        }

        private IWorkSpaceReservationBundleRepo _workSpaceReservationBundleRepo;

        public IWorkSpaceReservationBundleRepo WorkSpaceReservationBundleRepo
        {
            get 
            {
                return _workSpaceReservationBundleRepo ?? new WorkSpaceReservationBundleRepo(_context, _reservationsStatusService);

            }
        }

        private IWorkSpaceReservationHourlyRepo _workSpaceReservationHourlyRepo;
        public IWorkSpaceReservationHourlyRepo WorkSpaceReservationHourlyRepo
        {
            get
            {
                return _workSpaceReservationHourlyRepo ?? new WorkSpaceReservationHourlyRepo(_context, _reservationsStatusService);

            }
        }

        private IWorkSpaceReservationTailoredRepo _workSpaceReservationTailoredRepo;
        public IWorkSpaceReservationTailoredRepo WorkSpaceReservationTailoredRepo
        {
            get
            {
                return _workSpaceReservationTailoredRepo ?? new WorkSpaceReservationTailoredRepo(_context, _reservationsStatusService);
            }
        }

        private IWorkSpaceHourlyTopUpRepo _workSpaceHourlyTopUpRepo;
        public IWorkSpaceHourlyTopUpRepo WorkSpaceHourlyTopUpRepo
        {
            get
            {
                return _workSpaceHourlyTopUpRepo ?? new WorkSpaceHourlyTopUpRepo(_context);
            }
        }

        private IWorkSpaceTailoredTopUpRepo _workSpaceTailoredTopUpRepo;
        public IWorkSpaceTailoredTopUpRepo WorkSpaceTailoredTopUpRepo
        {
            get
            {
                return _workSpaceTailoredTopUpRepo ?? new WorkSpaceTailoredTopUpRepo(_context);
            }
        }
        #endregion

        #region Location Managment

        private IGenericRepository<Country> _countryRepo;
        public IGenericRepository<Country> CountryRepo
        {
            get
            {
                return _countryRepo = _countryRepo ?? new GenericRepository<Country>(_context);
            }
        }

        private ICountryRepository _countryRepoEF;
        public ICountryRepository CountryRepoEF
        {
            get
            {
                return _countryRepoEF = _countryRepoEF ?? new CountryRepository(_context);
            }
        }

        private IGenericRepository<City> _cityRepo;
        public IGenericRepository<City> CityRepo
        {
            get
            {
                return _cityRepo = _cityRepo ?? new GenericRepository<City>(_context);
            }
        }

        private ICityRepository _cityRepoEF;
        public ICityRepository CityRepoEF
        {
            get
            {
                return _cityRepoEF = _cityRepoEF ?? new CityRepository(_context);
            }
        }

        private IGenericRepository<District> _districtRepo;
        public IGenericRepository<District> DistrictRepo
        {
            get
            {
                return _districtRepo = _districtRepo ?? new GenericRepository<District>(_context);
            }
        }

        private IDistrictRepository _districtRepoEF;
        public IDistrictRepository DistrictRepoEF
        {
            get
            {
                return _districtRepoEF = _districtRepoEF ?? new DistrictRepository(_context);
            }
        }

        IGenericRepository<Currency> _currencyRepo;
        public IGenericRepository<Currency> CurrencyRepo
        {
            get
            {
                return _currencyRepo = _currencyRepo ?? new GenericRepository<Currency>(_context);
            }
        }

        ICurrencyRepository _currencyRepoEF;
        public ICurrencyRepository CurrencyRepoEF
        {
            get
            {
                return _currencyRepoEF = _currencyRepoEF ?? new CurrencyRepository(_context);
            }
        }

        IGenericRepository<LocationType> _locationTypeRepo;
        public IGenericRepository<LocationType> LocationTypeRepo
        {
            get
            {
                return _locationTypeRepo = _locationTypeRepo ?? new GenericRepository<LocationType>(_context);
            }
        }

        ILocationTypeRepository _locationTypeRepoEF;
        public ILocationTypeRepository LocationTypeRepoEF
        {
            get
            {
                return _locationTypeRepoEF = _locationTypeRepoEF ?? new LocationTypeRepository(_context);
            }
        }

        IGenericRepository<Feature> _featureRepo;
        public IGenericRepository<Feature> FeatureRepo
        {
            get
            {
                return _featureRepo = _featureRepo ?? new GenericRepository<Feature>(_context);
            }
        }

        IFeatureRepository _featureRepoEF;
        public IFeatureRepository FeatureRepoEF
        {
            get
            {
                return _featureRepoEF = _featureRepoEF ?? new FeatureRepository(_context);
            }
        }

        IGenericRepository<Inclusion> _inclusionRepo;
        public IGenericRepository<Inclusion> InclusionRepo
        {
            get
            {
                return _inclusionRepo = _inclusionRepo ?? new GenericRepository<Inclusion>(_context);
            }
        }

        IInclusionRepository _inclusionRepoEF;
        public IInclusionRepository InclusionRepoEF
        {
            get
            {
                return _inclusionRepoEF = _inclusionRepoEF ?? new InclusionRepository(_context);
            }
        }

        IGenericRepository<Industry> _industryRepo;
        public IGenericRepository<Industry> IndustryRepo
        {
            get
            {
                return _industryRepo = _industryRepo ?? new GenericRepository<Industry>(_context);
            }
        }

        IIndustryRepository _industryRepoEF;
        public IIndustryRepository IndustryRepoEF
        {
            get
            {
                return _industryRepoEF = _industryRepoEF ?? new IndustryRepository(_context);
            }
        }

        IGenericRepository<LocationBankAccount> _locationBankAccountRepo;
        public IGenericRepository<LocationBankAccount> LocationBankAccountRepo
        {
            get
            {
                return _locationBankAccountRepo = _locationBankAccountRepo ?? new GenericRepository<LocationBankAccount>(_context);
            }
        }

        ILocationBankAccountRepository _locationBankAccountRepoEF;
        public ILocationBankAccountRepository LocationBankAccountRepoEF
        {
            get
            {
                return _locationBankAccountRepoEF = _locationBankAccountRepoEF ?? new LocationBankAccountRepository(_context);
            }
        }

        IGenericRepository<LocationContact> _locationContactRepo;
        public IGenericRepository<LocationContact> LocationContactRepo
        {
            get
            {
                return _locationContactRepo = _locationContactRepo ?? new GenericRepository<LocationContact>(_context);
            }
        }

        ILocationContactRepository _locationContactRepoEF;
        public ILocationContactRepository LocationContactRepoEF
        {
            get
            {
                return _locationContactRepoEF = _locationContactRepoEF ?? new LocationContactRepository(_context);
            }
        }

        IGenericRepository<LocationCurrency> _locationCurrencyRepo;
        public IGenericRepository<LocationCurrency> LocationCurrencyRepo
        {
            get
            {
                return _locationCurrencyRepo = _locationCurrencyRepo ?? new GenericRepository<LocationCurrency>(_context);
            }
        }

        ILocationCurrencyRepository _locationCurrencyRepoEF;
        public ILocationCurrencyRepository LocationCurrencyRepoEF
        {
            get
            {
                return _locationCurrencyRepoEF = _locationCurrencyRepoEF ?? new LocationCurrencyRepository(_context);
            }
        }

        IGenericRepository<LocationFile> _locationFileRepo;
        public IGenericRepository<LocationFile> LocationFileRepo
        {
            get
            {
                return _locationFileRepo = _locationFileRepo ?? new GenericRepository<LocationFile>(_context);
            }
        }

        ILocationFileRepository _locationFileRepoEF;
        public ILocationFileRepository LocationFileRepoEF
        {
            get
            {
                return _locationFileRepoEF = _locationFileRepoEF ?? new LocationFileRepository(_context);
            }
        }

        IGenericRepository<LocationImage> _locationImageRepo;
        public IGenericRepository<LocationImage> LocationImageRepo
        {
            get
            {
                return _locationImageRepo = _locationImageRepo ?? new GenericRepository<LocationImage>(_context);
            }
        }

        ILocationImageRepository _locationImageRepoEF;
        public ILocationImageRepository LocationImageRepoEF
        {
            get
            {
                return _locationImageRepoEF = _locationImageRepoEF ?? new LocationImageRepository(_context);
            }
        }

        IGenericRepository<LocationInclusion> _locationInclusionRepo;
        public IGenericRepository<LocationInclusion> LocationInclusionRepo
        {
            get
            {
                return _locationInclusionRepo = _locationInclusionRepo ?? new GenericRepository<LocationInclusion>(_context);
            }
        }

        ILocationInclusionRepository _locationInclusionRepoEF;
        public ILocationInclusionRepository LocationInclusionRepoEF
        {
            get
            {
                return _locationInclusionRepoEF = _locationInclusionRepoEF ?? new LocationInclusionRepository(_context);
            }
        }

        IGenericRepository<LocationWorkingHour> _locationWorkingHourRepo;
        public IGenericRepository<LocationWorkingHour> LocationWorkingHourRepo
        {
            get
            {
                return _locationWorkingHourRepo = _locationWorkingHourRepo ?? new GenericRepository<LocationWorkingHour>(_context);
            }
        }

        ILocationWorkingHourRepository _locationWorkingHourRepoEF;
        public ILocationWorkingHourRepository LocationWorkingHourRepoEF
        {
            get
            {
                return _locationWorkingHourRepoEF = _locationWorkingHourRepoEF ?? new LocationWorkingHourRepository(_context);
            }
        }

        IGenericRepository<ServiceFeePaymentsDueDate> _serviceFeePaymentsDueDateRepo;
        public IGenericRepository<ServiceFeePaymentsDueDate> ServiceFeePaymentsDueDateRepo
        {
            get
            {
                return _serviceFeePaymentsDueDateRepo = _serviceFeePaymentsDueDateRepo ?? new GenericRepository<ServiceFeePaymentsDueDate>(_context);
            }
        }

        IServiceFeePaymentsDueDateRepository _serviceFeePaymentsDueDateRepoEF;
        public IServiceFeePaymentsDueDateRepository ServiceFeePaymentsDueDateRepoEF
        {
            get
            {
                return _serviceFeePaymentsDueDateRepoEF = _serviceFeePaymentsDueDateRepoEF ?? new ServiceFeePaymentsDueDateRepository(_context);
            }
        }

        IGenericRepository<Location> _locationRepo;
        public IGenericRepository<Location> LocationRepo
        {
            get
            {
                return _locationRepo = _locationRepo ?? new GenericRepository<Location>(_context);
            }
        }

        ILocationRepository _locationRepoEF;
        public ILocationRepository LocationRepoEF
        {
            get
            {
                return _locationRepoEF = _locationRepoEF ?? new LocationRepository(_context);
            }
        }

        IGenericRepository<FavouriteLocation> _favouriteLocationRepo;
        public IGenericRepository<FavouriteLocation> FavouriteLocationRepo
        {
            get
            {
                return _favouriteLocationRepo = _favouriteLocationRepo ?? new GenericRepository<FavouriteLocation>(_context);
            }
        }

        IFavouriteLocationRepository _favouriteLocationRepoEF;
        public IFavouriteLocationRepository FavouriteLocationRepoEF
        {
            get
            {
                return _favouriteLocationRepoEF = _favouriteLocationRepoEF ?? new FavouriteLocationRepository(_context);
            }
        }

        IGenericRepository<Building> _buildingRepo;
        public IGenericRepository<Building> BuildingRepo
        {
            get
            {
                return _buildingRepo = _buildingRepo ?? new GenericRepository<Building>(_context);
            }
        }

        IBuildingRepository _buildingRepoEF;
        public IBuildingRepository BuildingRepoEF
        {
            get
            {
                return _buildingRepoEF = _buildingRepoEF ?? new BuildingRepository(_context);
            }
        }

        IGenericRepository<BuildingFloor> _buildingFloorRepo;
        public IGenericRepository<BuildingFloor> BuildingFloorRepo
        {
            get
            {
                return _buildingFloorRepo = _buildingFloorRepo ?? new GenericRepository<BuildingFloor>(_context);
            }
        }

        IBuildingFloorRepository _buildingFloorRepoEF;
        public IBuildingFloorRepository BuildingFloorRepoEF
        {
            get
            {
                return _buildingFloorRepoEF = _buildingFloorRepoEF ?? new BuildingFloorRepository(_context);
            }
        }
        #endregion


        #region MeetingSpaceReservations
        private IMeetingSpaceReservationRepository _meetingSpaceReservationRepository;

        public IMeetingSpaceReservationRepository MeetingSpaceReservationRepository
        {
            get 
            { 
                return _meetingSpaceReservationRepository = _meetingSpaceReservationRepository ?? new MeetingSpaceReservationsRepository(_context); 
            }
        }


        #endregion


        #region SSO

        public IBasicUserRepository BasicUserRepository { get; }

        public IBasicUserStatusHistoryRepository BasicUserStatusHistoryRepository { get; }

        public IClientDeviceRepository ClientDeviceRepository { get; }

        public IMemberShipTypesRepository MemberShipTypesRepository { get; }

        public IMemberShipMainCategoriesRepository MemberShipMainCategoriesRepository { get; }

        public IMemberShipCategoriesRepository MemberShipCategoriesRepository { get; }

        public IMemberShipBenefitsTypesRepository MemberShipBenefitsTypesRepository { get; }

        public IGenderRepository GenderRepository { get; }

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
                    _context.Dispose();
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
                _context.SaveChanges();
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
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
