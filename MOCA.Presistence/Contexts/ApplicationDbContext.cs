using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.DynamicLists;
using MOCA.Core.Entities.EventSpaceBookings;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Entities.MeetingSpaceReservation;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Entities.Shared;
using MOCA.Core.Entities.Shared.Reservations;
using MOCA.Core.Entities.SSO;
using MOCA.Core.Entities.SSO.Identity;
using MOCA.Core.Entities.WorkSpaceReservations;
using MOCA.Core.Interfaces.Shared.Services;
using System.Data;

namespace MOCA.Presistence.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<Admin, AdminRole, string, AdminUserClaim, IdentityUserRole<string>, IdentityUserLogin<string>, AdminRoleClaim, IdentityUserToken<string>>, IApplicationDbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                                    IDateTimeService dateTime,
                                    IAuthenticatedUserService authenticatedUser) : base(options)
        {
           ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }
        public IDbConnection Connection => Database.GetDbConnection();

        #region Moca Settings
        public DbSet<CaseType> CaseTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<IssueCaseStage> IssueCaseStages { get; set; }
        public DbSet<IssueReport> IssueReports { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanType> PlanTypes { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<PolicyType> PolicyTypes { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Severity> Severities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<TopUp> TopUps { get; set; }
        public DbSet<TopUpType> TopUpTypes { get; set; }
        public DbSet<Wifi> Wifis { get; set; }
        #endregion

        #region LocationManagment
        public DbSet<Building> Buildings { get; set; }
        public DbSet<BuildingFloor> BuildingFloors { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Inclusion> Inclusions { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationBankAccount> LocationBankAccounts { get; set; }
        public DbSet<LocationContact> LocationContacts { get; set; }
        public DbSet<LocationCurrency> LocationCurrencies { get; set; }
        public DbSet<LocationFeature> LocationFeatures { get; set; }
        public DbSet<LocationFile> LocationFiles { get; set; }
        public DbSet<LocationImage> LocationImages { get; set; }
        public DbSet<LocationInclusion> LocationInclusions { get; set; }
        public DbSet<LocationIndustry> LocationIndustries { get; set; }
        public DbSet<LocationType> LocationTypes { get; set; }
        public DbSet<LocationWorkingHour> LocationWorkingHours { get; set; }
        public DbSet<ServiceFeePaymentsDueDate> ServiceFeePaymentsDueDates { get; set; }
        public DbSet<FavouriteLocation> FavouriteLocations { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<SpaceAmenity> SpaceAmenities { get; set; }
        public DbSet<EventSpace> EventSpaces { get; set; }
        public DbSet<EventSpaceHourlyPricing> EventSpaceHourlyPricings { get; set; }
        public DbSet<FurnishingType> FurnishingTypes { get; set; }
        public DbSet<Furnishing> Furnishings { get; set; }
        public DbSet<MeetingSpace> MeetingSpaces { get; set; }
        public DbSet<MeetingSpaceHourlyPricing> MeetingSpaceHourlyPricings { get; set; }
        public DbSet<WorkSpace> WorkSpaces { get; set; }
        public DbSet<MarketingImages> MarketingImages { get; set; }
        public DbSet<VenueSetup> VenueSetups { get; set; }
        public DbSet<EventSpaceOccupancy> EventSpaceOccupancies { get; set; }
        #endregion

        #region Shared
        public DbSet<MemberType> MemberTypes { get; set; }
        #endregion

        #region Dynamic Lists
        public DbSet<WorkSpaceCategory> WorkSpaceCategories { get; set; }
        public DbSet<WorkSpaceType> WorkSpaceTypes { get; set; }

        #endregion

        #region EventSpaceBookings
        public DbSet<ContactDetails> ContactDetails { get; set; }
        public DbSet<EventAttendance> EventAttendances { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<EventOpportunityStatus> EventOpportunityStatuses { get; set; }
        public DbSet<EventRequester> EventRequesters { get; set; }
        public DbSet<EventReccurance> EventReccurances { get; set; }
        public DbSet<EventSpaceBooking> EventSpaceBookings { get; set; }
        public DbSet<EventSpaceTime> EventSpaceTimes { get; set; }
        public DbSet<EventSpaceVenues> EventSpaceVenues { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Initiated> Initiateds { get; set; }
        public DbSet<OpportunityStage> OpportunityStages { get; set; }
        public DbSet<OpportunityStageReport> OpportunityStageReports { get; set; }
        public DbSet<SendEmail> SendEmails { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<EmailTemplateType> EmailTemplateTypes { get; set; }

        #endregion

        #region SSO
        public DbSet<Admin> Admins { get; set; }
            public DbSet<BasicUser> BasicUsers { get; set; }
            public DbSet<ClientDevice> ClientDevices { get; set; }
            public DbSet<MemberShipMainCategories> MemberShipMainCategories { get; set; }
            public DbSet<MemberShipBenefitsTypes> MemberShipBenefitsTypes { get; set; }
            public DbSet<MemberShipTypes> MemberShipTypes { get; set; }
            public DbSet<MemberShipCategories> MemberShipCategories { get; set; }
        #endregion

        #region ReservationShared
        public DbSet<CancelReservation> CancelReservations { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<ReservationDetail> ReservationDetails { get; set; }
        public DbSet<ReservationTransaction> ReservationTransactions { get; set; }
        public DbSet<ReservationType> ReservationTypes { get; set; }
        #endregion

        #region WorkSpaceReservations
        public DbSet<WorkSpaceReservationHourly> WorkSpaceReservationHourly { get; set; }
        public DbSet<WorkSpaceReservationTailored> WorkSpaceReservationTailored { get; set; }
        public DbSet<WorkSpaceReservationBundle> WorkSpaceReservationBundle { get; set; }
        public DbSet<WorkSpaceHourlyTopUp> WorkSpaceHourlyTopUps { get; set; }
        public DbSet<WorkSpaceTailoredTopUp> WorkSpaceTailoredTopUps { get; set; }
        public DbSet<WorkSpaceHourlyTransactions> WorkSpaceHourlyTransactions { get; set; }
        public DbSet<WorkSpaceTailoredTransactions> WorkSpaceTailoredTransactions { get; set; }
        public DbSet<WorkSpaceBundleTransactions> WorkSpaceBundleTransactions { get; set; }
        public DbSet<WorkSpaceHourlyCancellation> WorkSpaceHourlyCancellations { get; set; }
        public DbSet<WorkSpaceTailoredCancellation> WorkSpaceTailoredCancellations { get; set; }
        public DbSet<WorkSpaceBundleCancellation> WorkSpaceBundleCancellations { get; set; }
        #endregion

        #region MeetingSpaceReservations
        public DbSet<MeetingReservation> MeetingSpaceReservations { get; set; }
        public DbSet<MeetingAttendee> MeetingAttendees { get; set; }
        public DbSet<MeetingReservationTopUp> MeetingReservationTopUps { get; set; }
        #endregion

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                }

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = _dateTime.NowUtc;
                    entry.Entity.CreatedBy = _authenticatedUser.UserId;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedAt = _dateTime.NowUtc;
                    entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //All Decimals will have 18,3 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,3)");
            }
            base.OnModelCreating(builder); // test

            builder.Entity<Admin>().ToTable("Admin");
            builder.Entity<BasicUser>().ToTable("BasicUser");

            builder.Entity<AdminRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<AdminUserClaim>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<AdminRoleClaim>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            #region Register Moca Settings
            builder.Entity<Policy>()
                    .HasIndex(p => p.PolicyTypeId).IsUnique(false);

            builder.Entity<Plan>()
                        .HasIndex(p => p.LobSpaceTypeId).IsUnique(false);

            builder.Entity<Wifi>()
                        .HasIndex(w => w.LobSpaceTypeId).IsUnique(false);

            builder.Entity<IssueReport>()
                .HasKey(p => new { p.Id, p.LastModifiedAt });

            builder.Entity<IssueCaseStage>()
                .HasKey(p => new { p.Id, p.LastModifiedAt });

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                // equivalent of modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                entityType.SetTableName(entityType.DisplayName());

                // equivalent of modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }
            #endregion

            #region Workspace Reservation
            builder.Entity<WorkSpaceBundleCancellation>()
                .HasKey(p => new { p.WorkSpaceBundleReservationId, p.CancellationId });

            builder.Entity<WorkSpaceBundleTransactions>()
                .HasKey(p => new { p.WorkSpaceReservationBundleId, p.ReservationTransactionId });

            builder.Entity<WorkSpaceHourlyCancellation>()
                .HasKey(p => new { p.WorkSpaceHourlyReservationId, p.CancellationId });
            
            builder.Entity<WorkSpaceHourlyTransactions>()
                .HasKey(p => new { p.WorkSpaceReservationHourlyId, p.ReservationTransactionId });

            builder.Entity<WorkSpaceTailoredCancellation>()
                .HasKey(p => new { p.WorkSpaceTailoredReservationId, p.CancellationId });
            
            builder.Entity<WorkSpaceTailoredTransactions>()
                .HasKey(p => new { p.WorkSpaceReservationTailoredId, p.ReservationTransactionId });
            #endregion

            #region Meetingspace Reservation
            builder.Entity<MeetingReservationTransaction>()
                .HasKey(p => new { p.MeetingReservationId, p.ReservationTransactionId });
            #endregion
        }
    }
}
