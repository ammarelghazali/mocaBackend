using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MOCA.Core.Entities.BaseEntities;
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
