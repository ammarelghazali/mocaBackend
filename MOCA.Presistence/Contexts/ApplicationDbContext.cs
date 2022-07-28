using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.BaseEntities;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Entities.MocaSetting;
using MOCA.Core.Entities.SSO;
using MOCA.Core.Entities.SSO.Identity;
using MOCA.Core.Interfaces.Shared.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #endregion
        public DbSet<CaseType> CaseTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<IssueCaseStage> IssueCaseStages { get; set; }
        public DbSet<IssueReport> IssueReports { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanType> PlanTypes{ get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<PolicyType> PolicyTypes { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Severity> Severities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<TopUp> TopUps { get; set; }
        public DbSet<TopUpType> TopUpTypes { get; set; }
        public DbSet<Wifi> Wifis { get; set; }
        #region LocationManagment
        public DbSet<City> Citys { get; set; }
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

            #endregion
        }
    }
}
