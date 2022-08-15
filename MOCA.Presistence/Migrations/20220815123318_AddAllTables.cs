using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class AddAllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Amenity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CaseType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientDevice",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniqulyIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OTP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OTPDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BasicUserId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientDevice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCodeString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlagCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplateType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplateType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventAttendance",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventAttendance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventCategory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventOpportunityStatus",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventOpportunityStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventReccurance",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventReccurance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventRequester",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRequester", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FurnishingType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnishingType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inclusion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inclusion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Industry",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Initiated",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Initiated", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberShipBenefitsTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberShipBenefitsTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberShipMainCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberShipMainCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberShipTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberShipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityStage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityStage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    URL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PolicyType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    URL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Priority",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReservationType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Severity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Severity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TopUpType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    URL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopUpType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VenueSetup",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueSetup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkSpaceCategory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminUserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Selected = table.Column<bool>(type: "bit", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminUserClaim_Admin_UserId",
                        column: x => x.UserId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserLogin<string>",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserLogin<string>", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IdentityUserLogin<string>_Admin_UserId",
                        column: x => x.UserId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserToken<string>",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserToken<string>", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_IdentityUserToken<string>_Admin_UserId",
                        column: x => x.UserId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdminRoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Selected = table.Column<bool>(type: "bit", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminRoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminRoleClaim_AdminRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AdminRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserRole<string>",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<string>", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_Admin_UserId",
                        column: x => x.UserId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdentityUserRole<string>_AdminRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AdminRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<long>(type: "bigint", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailTemplateTypeID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailTemplate_EmailTemplateType_EmailTemplateTypeID",
                        column: x => x.EmailTemplateTypeID,
                        principalTable: "EmailTemplateType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarketingImages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpaceId = table.Column<long>(type: "bigint", nullable: false),
                    FeatureId = table.Column<long>(type: "bigint", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketingImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketingImages_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpaceAmenity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpaceId = table.Column<long>(type: "bigint", nullable: false),
                    AmenityId = table.Column<long>(type: "bigint", nullable: false),
                    FeatureId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpaceAmenity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpaceAmenity_Amenity_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "Amenity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SpaceAmenity_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Furnishing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FurnishingTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Vendor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dimensions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpaceId = table.Column<long>(type: "bigint", nullable: false),
                    FeatureId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furnishing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Furnishing_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Furnishing_FurnishingType_FurnishingTypeId",
                        column: x => x.FurnishingTypeId,
                        principalTable: "FurnishingType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    LobSpaceTypeId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_LocationType_LobSpaceTypeId",
                        column: x => x.LobSpaceTypeId,
                        principalTable: "LocationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wifi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LobSpaceTypeId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wifi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wifi_LocationType_LobSpaceTypeId",
                        column: x => x.LobSpaceTypeId,
                        principalTable: "LocationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MemberShipCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    BenefitTypeId = table.Column<long>(type: "bigint", nullable: false),
                    IsStatic = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberShipCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberShipCategories_MemberShipBenefitsTypes_BenefitTypeId",
                        column: x => x.BenefitTypeId,
                        principalTable: "MemberShipBenefitsTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberShipCategories_MemberShipMainCategories_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "MemberShipMainCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    LobSpaceTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Points = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhatYouGet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TermsOfUse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plan_LocationType_LobSpaceTypeId",
                        column: x => x.LobSpaceTypeId,
                        principalTable: "LocationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plan_PlanType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PlanType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Policy",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LobSpaceTypeId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Policy_LocationType_LobSpaceTypeId",
                        column: x => x.LobSpaceTypeId,
                        principalTable: "LocationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policy_PolicyType_PolicyTypeId",
                        column: x => x.PolicyTypeId,
                        principalTable: "PolicyType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TopUp",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopUpTypeId = table.Column<long>(type: "bigint", nullable: false),
                    LobSpaceTypeId = table.Column<long>(type: "bigint", nullable: false),
                    TermsOfUse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopUp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopUp_LocationType_LobSpaceTypeId",
                        column: x => x.LobSpaceTypeId,
                        principalTable: "LocationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopUp_TopUpType_TopUpTypeId",
                        column: x => x.TopUpTypeId,
                        principalTable: "TopUpType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkSpaceType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkSpaceCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSpaceType_WorkSpaceCategory_WorkSpaceCategoryId",
                        column: x => x.WorkSpaceCategoryId,
                        principalTable: "WorkSpaceCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<long>(type: "bigint", nullable: false),
                    DistrictName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                    table.ForeignKey(
                        name: "FK_District_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Faq",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<long>(type: "bigint", nullable: true),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    LobSpaceTypeId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faq", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faq_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Faq_LocationType_LobSpaceTypeId",
                        column: x => x.LobSpaceTypeId,
                        principalTable: "LocationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BasicUser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<long>(type: "bigint", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePhoto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MembershipCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    MembershipTypeId = table.Column<long>(type: "bigint", nullable: true),
                    MemberShipTypesId = table.Column<long>(type: "bigint", nullable: false),
                    IsQRVerifiedByAdmin = table.Column<bool>(type: "bit", nullable: true),
                    IsQRVerifiedByClient = table.Column<bool>(type: "bit", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    Accept = table.Column<bool>(type: "bit", nullable: false),
                    StatusUser = table.Column<bool>(type: "bit", nullable: false),
                    ActivationToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WalletBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MemberID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MembershipActivationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenderId = table.Column<long>(type: "bigint", nullable: false),
                    UserDeviceId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasicUser_ClientDevice_UserDeviceId",
                        column: x => x.UserDeviceId,
                        principalTable: "ClientDevice",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BasicUser_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BasicUser_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BasicUser_MemberShipCategories_MembershipCategoryId",
                        column: x => x.MembershipCategoryId,
                        principalTable: "MemberShipCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BasicUser_MemberShipTypes_MemberShipTypesId",
                        column: x => x.MemberShipTypesId,
                        principalTable: "MemberShipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuildYear = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: false),
                    CityId = table.Column<long>(type: "bigint", nullable: false),
                    DistrictId = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MapAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    GrossArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LaunchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LandlordCommercialName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LandlordLegalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractLength = table.Column<int>(type: "int", nullable: false),
                    PartentershipType = table.Column<int>(type: "int", nullable: false),
                    PreOperationFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GracePeriod = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RampUpPeriod = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UtilizationPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinPaymentPerMonth = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinPaymentPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EstimatedRampUpAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FullRampUpRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FullOccupancyMonthlyPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EstimatedAnnualizedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EstimatedContractAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LandlordShares = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CopolitanShares = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentMethod = table.Column<int>(type: "int", nullable: true),
                    PaymentTerm = table.Column<int>(type: "int", nullable: true),
                    MonthlyRentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PricePerMeter = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AnnualIncrease = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LandlordAdditionalRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MonthlyRevenue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DirectCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Overhead = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalAfterDeductions = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ServiceFeesPriceSqm = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ServiceFeesTotalFees = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ServiceFeesAnnualIncrease = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EventspaceLeaseContract = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommercialRegisterFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommercialRegisterNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxIdFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Terms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublish = table.Column<bool>(type: "bit", nullable: false),
                    InstallAccessPoint = table.Column<bool>(type: "bit", nullable: false),
                    LocationBankAccountType = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Location_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Location_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Location_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Location_LocationType_LocationTypeId",
                        column: x => x.LocationTypeId,
                        principalTable: "LocationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CancelReservation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationTargetId = table.Column<long>(type: "bigint", nullable: false),
                    ReservationTypeId = table.Column<long>(type: "bigint", nullable: false),
                    RefundReservationType = table.Column<int>(type: "int", nullable: false),
                    CancelDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BasicUserId = table.Column<long>(type: "bigint", nullable: false),
                    AdminId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelReservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CancelReservation_Admin_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelReservation_BasicUser_BasicUserId",
                        column: x => x.BasicUserId,
                        principalTable: "BasicUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelReservation_ReservationType_ReservationTypeId",
                        column: x => x.ReservationTypeId,
                        principalTable: "ReservationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrossArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaleRestroomCount = table.Column<int>(type: "int", nullable: false),
                    FemaleRestroomCount = table.Column<int>(type: "int", nullable: false),
                    InstallAccessPoint = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Building_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Coworking",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    Occupancy = table.Column<int>(type: "int", nullable: false),
                    RemainingOccupancy = table.Column<int>(type: "int", nullable: false),
                    TailoredPercentage = table.Column<int>(type: "int", nullable: false),
                    InstallAccessPoint = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coworking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coworking_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventSpaceBooking",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationNameId = table.Column<long>(type: "bigint", nullable: true),
                    EventRequesterId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyCommericalName = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    IndustryNameId = table.Column<long>(type: "bigint", nullable: true),
                    OtherIndustryName = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    CompanyWebsite = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    CompanyFacebook = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    CompanyLinkedin = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    CompanyInstgram = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    ContactFullName1 = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    ContactMobile1 = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    ContactEmail1 = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    ContactFullName2 = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    ContactMobile2 = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    ContactEmail2 = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    EventName = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    EventCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    EventDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    EventReccuranceId = table.Column<long>(type: "bigint", nullable: true),
                    ExpectedNoAttend = table.Column<int>(type: "int", nullable: true),
                    EventTypeId = table.Column<long>(type: "bigint", nullable: true),
                    EventAttendanceId = table.Column<long>(type: "bigint", nullable: true),
                    DoesYourEventSupportStartup = table.Column<bool>(type: "bit", nullable: true),
                    IsThereThirdPartyOrganizer = table.Column<bool>(type: "bit", nullable: true),
                    OrgnizingCompany = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    NeedConsultancy = table.Column<bool>(type: "bit", nullable: true),
                    Platform = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    OtherEventCategory = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    InitiatedId = table.Column<long>(type: "bigint", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpportunityStageId = table.Column<long>(type: "bigint", nullable: true),
                    Revenue = table.Column<long>(type: "bigint", nullable: true),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EventOpportunityStatusId = table.Column<long>(type: "bigint", nullable: true),
                    LobLocationTypeId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSpaceBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventSpaceBooking_EventAttendance_EventAttendanceId",
                        column: x => x.EventAttendanceId,
                        principalTable: "EventAttendance",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventSpaceBooking_EventCategory_EventCategoryId",
                        column: x => x.EventCategoryId,
                        principalTable: "EventCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventSpaceBooking_EventOpportunityStatus_EventOpportunityStatusId",
                        column: x => x.EventOpportunityStatusId,
                        principalTable: "EventOpportunityStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventSpaceBooking_EventReccurance_EventReccuranceId",
                        column: x => x.EventReccuranceId,
                        principalTable: "EventReccurance",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventSpaceBooking_EventRequester_EventRequesterId",
                        column: x => x.EventRequesterId,
                        principalTable: "EventRequester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventSpaceBooking_EventType_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventSpaceBooking_Industry_IndustryNameId",
                        column: x => x.IndustryNameId,
                        principalTable: "Industry",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventSpaceBooking_Initiated_InitiatedId",
                        column: x => x.InitiatedId,
                        principalTable: "Initiated",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventSpaceBooking_Location_LocationNameId",
                        column: x => x.LocationNameId,
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventSpaceBooking_LocationType_LobLocationTypeId",
                        column: x => x.LobLocationTypeId,
                        principalTable: "LocationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventSpaceBooking_OpportunityStage_OpportunityStageId",
                        column: x => x.OpportunityStageId,
                        principalTable: "OpportunityStage",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FavouriteLocation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    BasicUserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavouriteLocation_BasicUser_BasicUserId",
                        column: x => x.BasicUserId,
                        principalTable: "BasicUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavouriteLocation_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IssueReport",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LobSpaceTypeId = table.Column<long>(type: "bigint", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false),
                    LevelSeverityId = table.Column<long>(type: "bigint", nullable: false),
                    PriorityId = table.Column<long>(type: "bigint", nullable: false),
                    CaseTypeId = table.Column<long>(type: "bigint", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CaseDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueReport", x => new { x.Id, x.LastModifiedAt });
                    table.ForeignKey(
                        name: "FK_IssueReport_Admin_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueReport_Admin_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueReport_CaseType_CaseTypeId",
                        column: x => x.CaseTypeId,
                        principalTable: "CaseType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueReport_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueReport_LocationType_LobSpaceTypeId",
                        column: x => x.LobSpaceTypeId,
                        principalTable: "LocationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueReport_Priority_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueReport_Severity_LevelSeverityId",
                        column: x => x.LevelSeverityId,
                        principalTable: "Severity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueReport_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationBankAccount",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    LandlordBankAccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LandlordBankAccountNumber = table.Column<long>(type: "bigint", nullable: true),
                    LandlordBankAccountSwift = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LandlordBankAccountIBAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MocaBankAccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MocaBankAccountNumber = table.Column<long>(type: "bigint", nullable: true),
                    MocaBankAccountSwift = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MocaBankAccountIBAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SharedBankAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SharedBankAccountNumber = table.Column<long>(type: "bigint", nullable: true),
                    SharedBankAccountSwift = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SharedBankAccountIBAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationBankAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationBankAccount_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationContact",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationContact_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationCurrency",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationCurrency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationCurrency_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocationCurrency_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationFeature",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    FeatureId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationFeature_Feature_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocationFeature_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationFile",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    LocationContractFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationFile_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationImage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    LocationImageFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationImage_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationInclusion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    InclusionId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationInclusion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationInclusion_Inclusion_InclusionId",
                        column: x => x.InclusionId,
                        principalTable: "Inclusion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocationInclusion_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationIndustry",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    IndustryId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationIndustry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationIndustry_Industry_IndustryId",
                        column: x => x.IndustryId,
                        principalTable: "Industry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocationIndustry_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationWorkingHour",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartWorkingHour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndWorkingHour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationWorkingHour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationWorkingHour_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationTransaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationTypeId = table.Column<long>(type: "bigint", nullable: false),
                    BasicUserId = table.Column<long>(type: "bigint", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    ReservationTargetId = table.Column<long>(type: "bigint", nullable: false),
                    RemainingHours = table.Column<long>(type: "bigint", nullable: false),
                    TotalHours = table.Column<int>(type: "int", nullable: true),
                    ExtendExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationTransaction_BasicUser_BasicUserId",
                        column: x => x.BasicUserId,
                        principalTable: "BasicUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationTransaction_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationTransaction_ReservationType_ReservationTypeId",
                        column: x => x.ReservationTypeId,
                        principalTable: "ReservationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceFeePaymentsDueDate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceFeePaymentsDueDate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceFeePaymentsDueDate_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BuildingFloor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorNumber = table.Column<long>(type: "bigint", nullable: false),
                    BuildingId = table.Column<long>(type: "bigint", nullable: false),
                    GrossArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaleRestroomCount = table.Column<int>(type: "int", nullable: false),
                    FemaleRestroomCount = table.Column<int>(type: "int", nullable: false),
                    InstallAccessPoint = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingFloor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildingFloor_Building_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoworkingSpaceBundlePricing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoworkingId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BundleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfUsers = table.Column<int>(type: "int", nullable: false),
                    DurationInDays = table.Column<int>(type: "int", nullable: false),
                    NumberOfHours = table.Column<int>(type: "int", nullable: false),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deactivation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingSpaceBundlePricing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceBundlePricing_Coworking_CoworkingId",
                        column: x => x.CoworkingId,
                        principalTable: "Coworking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoWorkingSpaceHourlyPricing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoworkingId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VoucherPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VoucherAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoWorkingSpaceHourlyPricing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoWorkingSpaceHourlyPricing_Coworking_CoworkingId",
                        column: x => x.CoworkingId,
                        principalTable: "Coworking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoworkingSpaceTailoredPricing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoworkingId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    HoursFrom = table.Column<int>(type: "int", nullable: false),
                    HoursTo = table.Column<int>(type: "int", nullable: false),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VoucherPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VoucherAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingSpaceTailoredPricing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceTailoredPricing_Coworking_CoworkingId",
                        column: x => x.CoworkingId,
                        principalTable: "Coworking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookATourId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    EventSpaceBookingId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactDetails_EventSpaceBooking_EventSpaceBookingId",
                        column: x => x.EventSpaceBookingId,
                        principalTable: "EventSpaceBooking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventSpaceTime",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventSpaceBookingId = table.Column<long>(type: "bigint", nullable: false),
                    RecurrenceStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecurrenceEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecurrenceStartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecurrenceEndTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecurrenceDay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSpaceTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventSpaceTime_EventSpaceBooking_EventSpaceBookingId",
                        column: x => x.EventSpaceBookingId,
                        principalTable: "EventSpaceBooking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventSpaceVenues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventSpaceBookingId = table.Column<long>(type: "bigint", nullable: false),
                    VenueName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSpaceVenues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventSpaceVenues_EventSpaceBooking_EventSpaceBookingId",
                        column: x => x.EventSpaceBookingId,
                        principalTable: "EventSpaceBooking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityStageReport",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventSpaceBookingId = table.Column<long>(type: "bigint", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Reminder = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OpportunityStageId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityStageReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpportunityStageReport_EventSpaceBooking_EventSpaceBookingId",
                        column: x => x.EventSpaceBookingId,
                        principalTable: "EventSpaceBooking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpportunityStageReport_OpportunityStage_OpportunityStageId",
                        column: x => x.OpportunityStageId,
                        principalTable: "OpportunityStage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IssueCaseStage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LobSpaceTypeId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueCaseStage", x => new { x.Id, x.LastModifiedAt });
                    table.ForeignKey(
                        name: "FK_IssueCaseStage_IssueReport_Id_LastModifiedAt",
                        columns: x => new { x.Id, x.LastModifiedAt },
                        principalTable: "IssueReport",
                        principalColumns: new[] { "Id", "LastModifiedAt" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueCaseStage_LocationType_LobSpaceTypeId",
                        column: x => x.LobSpaceTypeId,
                        principalTable: "LocationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationTransactionId = table.Column<long>(type: "bigint", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BasicUserId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationDetail_BasicUser_BasicUserId",
                        column: x => x.BasicUserId,
                        principalTable: "BasicUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReservationDetail_ReservationTransaction_ReservationTransactionId",
                        column: x => x.ReservationTransactionId,
                        principalTable: "ReservationTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoworkingWorkSpace",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoworkingId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UnitNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstallAccessPoint = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingFloorId = table.Column<long>(type: "bigint", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    GrossArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WorkSpaceCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    WorkSpaceTypeId = table.Column<long>(type: "bigint", nullable: false),
                    MaximumOccupancy = table.Column<int>(type: "int", nullable: false),
                    IsFurnishing = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingWorkSpace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoworkingWorkSpace_BuildingFloor_BuildingFloorId",
                        column: x => x.BuildingFloorId,
                        principalTable: "BuildingFloor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingWorkSpace_Coworking_CoworkingId",
                        column: x => x.CoworkingId,
                        principalTable: "Coworking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingWorkSpace_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingWorkSpace_WorkSpaceCategory_WorkSpaceCategoryId",
                        column: x => x.WorkSpaceCategoryId,
                        principalTable: "WorkSpaceCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingWorkSpace_WorkSpaceType_WorkSpaceTypeId",
                        column: x => x.WorkSpaceTypeId,
                        principalTable: "WorkSpaceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventSpace",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrossArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TermsOfUse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url360Tour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitEBrochure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestRoomMaleOccupancy = table.Column<int>(type: "int", nullable: true),
                    RestRoomFemaleOccupancy = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UnitNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstallAccessPoint = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingFloorId = table.Column<long>(type: "bigint", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSpace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventSpace_BuildingFloor_BuildingFloorId",
                        column: x => x.BuildingFloorId,
                        principalTable: "BuildingFloor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventSpace_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeetingSpace",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrossArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetArea = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VenueName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TermsOfUse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFurnishing = table.Column<bool>(type: "bit", nullable: false),
                    MaximumOccupancy = table.Column<int>(type: "int", nullable: false),
                    CovidOccupancy = table.Column<int>(type: "int", nullable: true),
                    Url360Tour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitEBrochure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UnitNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstallAccessPoint = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingFloorId = table.Column<long>(type: "bigint", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingSpace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingSpace_BuildingFloor_BuildingFloorId",
                        column: x => x.BuildingFloorId,
                        principalTable: "BuildingFloor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeetingSpace_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkSpace",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UnitNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstallAccessPoint = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingFloorId = table.Column<long>(type: "bigint", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    GrossArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WorkSpaceCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    WorkSpaceTypeId = table.Column<long>(type: "bigint", nullable: false),
                    MaximumOccupancy = table.Column<int>(type: "int", nullable: false),
                    IsFurnishing = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSpace_BuildingFloor_BuildingFloorId",
                        column: x => x.BuildingFloorId,
                        principalTable: "BuildingFloor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpace_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpace_WorkSpaceCategory_WorkSpaceCategoryId",
                        column: x => x.WorkSpaceCategoryId,
                        principalTable: "WorkSpaceCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpace_WorkSpaceType_WorkSpaceTypeId",
                        column: x => x.WorkSpaceTypeId,
                        principalTable: "WorkSpaceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoworkingSpaceBundleMemberType",
                columns: table => new
                {
                    CoworkSpaceBundleId = table.Column<long>(type: "bigint", nullable: false),
                    MemberTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingSpaceBundleMemberType", x => new { x.CoworkSpaceBundleId, x.MemberTypeId });
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceBundleMemberType_CoworkingSpaceBundlePricing_CoworkSpaceBundleId",
                        column: x => x.CoworkSpaceBundleId,
                        principalTable: "CoworkingSpaceBundlePricing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceBundleMemberType_MemberType_MemberTypeId",
                        column: x => x.MemberTypeId,
                        principalTable: "MemberType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SendEmail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactDetailId = table.Column<long>(type: "bigint", nullable: false),
                    BookATourId = table.Column<long>(type: "bigint", nullable: true),
                    EventSpaceBookingId = table.Column<long>(type: "bigint", nullable: true),
                    EmailTemplateId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendEmail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SendEmail_ContactDetails_ContactDetailId",
                        column: x => x.ContactDetailId,
                        principalTable: "ContactDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SendEmail_EmailTemplate_EmailTemplateId",
                        column: x => x.EmailTemplateId,
                        principalTable: "EmailTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SendEmail_EventSpaceBooking_EventSpaceBookingId",
                        column: x => x.EventSpaceBookingId,
                        principalTable: "EventSpaceBooking",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CoworkingSpaceReservationBundle",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BundleId = table.Column<long>(type: "bigint", nullable: false),
                    BundlePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BundleStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BundleEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoworkingId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    BasicUserId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: true),
                    CoworkSpaceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingSpaceReservationBundle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceReservationBundle_BasicUser_BasicUserId",
                        column: x => x.BasicUserId,
                        principalTable: "BasicUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceReservationBundle_Coworking_CoworkingId",
                        column: x => x.CoworkingId,
                        principalTable: "Coworking",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceReservationBundle_CoworkingSpaceBundlePricing_BundleId",
                        column: x => x.BundleId,
                        principalTable: "CoworkingSpaceBundlePricing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceReservationBundle_CoworkingWorkSpace_CoworkSpaceId",
                        column: x => x.CoworkSpaceId,
                        principalTable: "CoworkingWorkSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceReservationBundle_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceReservationBundle_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CoworkingSpaceReservationHourly",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HourId = table.Column<long>(type: "bigint", nullable: false),
                    IsDay = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HourlyDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CoworkingId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    BasicUserId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: true),
                    CoworkSpaceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingSpaceReservationHourly", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceReservationHourly_BasicUser_BasicUserId",
                        column: x => x.BasicUserId,
                        principalTable: "BasicUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceReservationHourly_Coworking_CoworkingId",
                        column: x => x.CoworkingId,
                        principalTable: "Coworking",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceReservationHourly_CoWorkingSpaceHourlyPricing_HourId",
                        column: x => x.HourId,
                        principalTable: "CoWorkingSpaceHourlyPricing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceReservationHourly_CoworkingWorkSpace_CoworkSpaceId",
                        column: x => x.CoworkSpaceId,
                        principalTable: "CoworkingWorkSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceReservationHourly_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceReservationHourly_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CoworkingSpaceReservationTailored",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TailoredStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TailoredEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TailoredHours = table.Column<int>(type: "int", nullable: false),
                    TailoredPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TailoredDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CoworkingId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    BasicUserId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: true),
                    CoworkSpaceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingSpaceReservationTailored", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceReservationTailored_BasicUser_BasicUserId",
                        column: x => x.BasicUserId,
                        principalTable: "BasicUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceReservationTailored_Coworking_CoworkingId",
                        column: x => x.CoworkingId,
                        principalTable: "Coworking",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceReservationTailored_CoworkingWorkSpace_CoworkSpaceId",
                        column: x => x.CoworkSpaceId,
                        principalTable: "CoworkingWorkSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceReservationTailored_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceReservationTailored_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CoworkingWorkspaceFurnishing",
                columns: table => new
                {
                    CoworkingWorkSpaceId = table.Column<long>(type: "bigint", nullable: false),
                    FurnishingId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingWorkspaceFurnishing", x => new { x.CoworkingWorkSpaceId, x.FurnishingId });
                    table.ForeignKey(
                        name: "FK_CoworkingWorkspaceFurnishing_CoworkingWorkSpace_CoworkingWorkSpaceId",
                        column: x => x.CoworkingWorkSpaceId,
                        principalTable: "CoworkingWorkSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingWorkspaceFurnishing_Furnishing_FurnishingId",
                        column: x => x.FurnishingId,
                        principalTable: "Furnishing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoworkingWorkSpaceMarketingImage",
                columns: table => new
                {
                    CoworkingWorkSpaceId = table.Column<long>(type: "bigint", nullable: false),
                    MarketingImagesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingWorkSpaceMarketingImage", x => new { x.CoworkingWorkSpaceId, x.MarketingImagesId });
                    table.ForeignKey(
                        name: "FK_CoworkingWorkSpaceMarketingImage_CoworkingWorkSpace_CoworkingWorkSpaceId",
                        column: x => x.CoworkingWorkSpaceId,
                        principalTable: "CoworkingWorkSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingWorkSpaceMarketingImage_MarketingImages_MarketingImagesId",
                        column: x => x.MarketingImagesId,
                        principalTable: "MarketingImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventSpaceHourlyPricing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventSpaceId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MemberTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSpaceHourlyPricing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventSpaceHourlyPricing_EventSpace_EventSpaceId",
                        column: x => x.EventSpaceId,
                        principalTable: "EventSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventSpaceHourlyPricing_MemberType_MemberTypeId",
                        column: x => x.MemberTypeId,
                        principalTable: "MemberType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventSpaceOccupancy",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventSpaceId = table.Column<long>(type: "bigint", nullable: false),
                    VenueSetupId = table.Column<long>(type: "bigint", nullable: false),
                    MaximumOccupancy = table.Column<int>(type: "int", nullable: false),
                    CovidOccupancy = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSpaceOccupancy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventSpaceOccupancy_EventSpace_EventSpaceId",
                        column: x => x.EventSpaceId,
                        principalTable: "EventSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventSpaceOccupancy_VenueSetup_VenueSetupId",
                        column: x => x.VenueSetupId,
                        principalTable: "VenueSetup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeetingSpaceHourlyPricing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeetingSpaceId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MemberTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingSpaceHourlyPricing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingSpaceHourlyPricing_MeetingSpace_MeetingSpaceId",
                        column: x => x.MeetingSpaceId,
                        principalTable: "MeetingSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeetingSpaceHourlyPricing_MemberType_MemberTypeId",
                        column: x => x.MemberTypeId,
                        principalTable: "MemberType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkSpaceBundlePricing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkSpaceId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BundleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfUsers = table.Column<int>(type: "int", nullable: false),
                    DurationInDays = table.Column<int>(type: "int", nullable: false),
                    NumberOfHours = table.Column<int>(type: "int", nullable: false),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deactivation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceBundlePricing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSpaceBundlePricing_WorkSpace_WorkSpaceId",
                        column: x => x.WorkSpaceId,
                        principalTable: "WorkSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkSpaceHourlyPricing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkSpaceId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Hour = table.Column<int>(type: "int", nullable: false),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VoucherPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VoucherAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceHourlyPricing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSpaceHourlyPricing_WorkSpace_WorkSpaceId",
                        column: x => x.WorkSpaceId,
                        principalTable: "WorkSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkSpaceReservationTailored",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TailoredStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TailoredEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TailoredHours = table.Column<int>(type: "int", nullable: false),
                    TailoredPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TailoredDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    BasicUserId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: true),
                    WorkSpaceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceReservationTailored", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSpaceReservationTailored_BasicUser_BasicUserId",
                        column: x => x.BasicUserId,
                        principalTable: "BasicUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceReservationTailored_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceReservationTailored_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkSpaceReservationTailored_WorkSpace_WorkSpaceId",
                        column: x => x.WorkSpaceId,
                        principalTable: "WorkSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkSpaceTailoredPricing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkSpaceId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    HoursFrom = table.Column<int>(type: "int", nullable: false),
                    HoursTo = table.Column<int>(type: "int", nullable: false),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VoucherPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VoucherAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceTailoredPricing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSpaceTailoredPricing_WorkSpace_WorkSpaceId",
                        column: x => x.WorkSpaceId,
                        principalTable: "WorkSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoworkingSpaceBundleCancellation",
                columns: table => new
                {
                    CoworkingSpaceReservationBundleId = table.Column<long>(type: "bigint", nullable: false),
                    CancellationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingSpaceBundleCancellation", x => new { x.CoworkingSpaceReservationBundleId, x.CancellationId });
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceBundleCancellation_CancelReservation_CancellationId",
                        column: x => x.CancellationId,
                        principalTable: "CancelReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceBundleCancellation_CoworkingSpaceReservationBundle_CoworkingSpaceReservationBundleId",
                        column: x => x.CoworkingSpaceReservationBundleId,
                        principalTable: "CoworkingSpaceReservationBundle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoworkingSpaceBundleTransaction",
                columns: table => new
                {
                    CoworkingSpaceReservationBundleId = table.Column<long>(type: "bigint", nullable: false),
                    ReservationTransactionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingSpaceBundleTransaction", x => new { x.CoworkingSpaceReservationBundleId, x.ReservationTransactionId });
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceBundleTransaction_CoworkingSpaceReservationBundle_CoworkingSpaceReservationBundleId",
                        column: x => x.CoworkingSpaceReservationBundleId,
                        principalTable: "CoworkingSpaceReservationBundle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceBundleTransaction_ReservationTransaction_ReservationTransactionId",
                        column: x => x.ReservationTransactionId,
                        principalTable: "ReservationTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoworkingSpaceHourlyCancellation",
                columns: table => new
                {
                    CoworkingSpaceReservationHourlyId = table.Column<long>(type: "bigint", nullable: false),
                    CancellationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingSpaceHourlyCancellation", x => new { x.CoworkingSpaceReservationHourlyId, x.CancellationId });
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceHourlyCancellation_CancelReservation_CancellationId",
                        column: x => x.CancellationId,
                        principalTable: "CancelReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceHourlyCancellation_CoworkingSpaceReservationHourly_CoworkingSpaceReservationHourlyId",
                        column: x => x.CoworkingSpaceReservationHourlyId,
                        principalTable: "CoworkingSpaceReservationHourly",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoworkingSpaceHourlyTop",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HourId = table.Column<long>(type: "bigint", nullable: false),
                    HourlyTotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoworkingSpaceReservationHourlyId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingSpaceHourlyTop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceHourlyTop_CoWorkingSpaceHourlyPricing_HourId",
                        column: x => x.HourId,
                        principalTable: "CoWorkingSpaceHourlyPricing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceHourlyTop_CoworkingSpaceReservationHourly_CoworkingSpaceReservationHourlyId",
                        column: x => x.CoworkingSpaceReservationHourlyId,
                        principalTable: "CoworkingSpaceReservationHourly",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceHourlyTop_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoworkingSpaceHourlyTransaction",
                columns: table => new
                {
                    CoworkingSpaceReservationHourlyId = table.Column<long>(type: "bigint", nullable: false),
                    ReservationTransactionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingSpaceHourlyTransaction", x => new { x.CoworkingSpaceReservationHourlyId, x.ReservationTransactionId });
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceHourlyTransaction_CoworkingSpaceReservationHourly_CoworkingSpaceReservationHourlyId",
                        column: x => x.CoworkingSpaceReservationHourlyId,
                        principalTable: "CoworkingSpaceReservationHourly",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceHourlyTransaction_ReservationTransaction_ReservationTransactionId",
                        column: x => x.ReservationTransactionId,
                        principalTable: "ReservationTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoworkingSpaceTailoredCancellation",
                columns: table => new
                {
                    CoworkingSpaceReservationTailoredId = table.Column<long>(type: "bigint", nullable: false),
                    CancellationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingSpaceTailoredCancellation", x => new { x.CoworkingSpaceReservationTailoredId, x.CancellationId });
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceTailoredCancellation_CancelReservation_CancellationId",
                        column: x => x.CancellationId,
                        principalTable: "CancelReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceTailoredCancellation_CoworkingSpaceReservationTailored_CoworkingSpaceReservationTailoredId",
                        column: x => x.CoworkingSpaceReservationTailoredId,
                        principalTable: "CoworkingSpaceReservationTailored",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoworkingSpaceTailoredTopUp",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TailoredHours = table.Column<int>(type: "int", nullable: false),
                    TailoredPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoworkingSpaceReservationTailoredId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingSpaceTailoredTopUp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceTailoredTopUp_CoworkingSpaceReservationTailored_CoworkingSpaceReservationTailoredId",
                        column: x => x.CoworkingSpaceReservationTailoredId,
                        principalTable: "CoworkingSpaceReservationTailored",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceTailoredTopUp_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoworkingSpaceTailoredTransaction",
                columns: table => new
                {
                    CoworkingSpaceReservationTailoredId = table.Column<long>(type: "bigint", nullable: false),
                    ReservationTransactionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingSpaceTailoredTransaction", x => new { x.CoworkingSpaceReservationTailoredId, x.ReservationTransactionId });
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceTailoredTransaction_CoworkingSpaceReservationTailored_CoworkingSpaceReservationTailoredId",
                        column: x => x.CoworkingSpaceReservationTailoredId,
                        principalTable: "CoworkingSpaceReservationTailored",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingSpaceTailoredTransaction_ReservationTransaction_ReservationTransactionId",
                        column: x => x.ReservationTransactionId,
                        principalTable: "ReservationTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeetingReservation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumOfAttendees = table.Column<int>(type: "int", nullable: false),
                    MeetingSpaceId = table.Column<long>(type: "bigint", nullable: false),
                    MeetingSpaceHourlyPricingId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    BasicUserId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingReservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingReservation_BasicUser_BasicUserId",
                        column: x => x.BasicUserId,
                        principalTable: "BasicUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeetingReservation_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeetingReservation_MeetingSpace_MeetingSpaceId",
                        column: x => x.MeetingSpaceId,
                        principalTable: "MeetingSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeetingReservation_MeetingSpaceHourlyPricing_MeetingSpaceHourlyPricingId",
                        column: x => x.MeetingSpaceHourlyPricingId,
                        principalTable: "MeetingSpaceHourlyPricing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeetingReservation_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkSpaceBundleMemberType",
                columns: table => new
                {
                    WorkSpaceBundleId = table.Column<long>(type: "bigint", nullable: false),
                    MemberTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceBundleMemberType", x => new { x.WorkSpaceBundleId, x.MemberTypeId });
                    table.ForeignKey(
                        name: "FK_WorkSpaceBundleMemberType_MemberType_MemberTypeId",
                        column: x => x.MemberTypeId,
                        principalTable: "MemberType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceBundleMemberType_WorkSpaceBundlePricing_WorkSpaceBundleId",
                        column: x => x.WorkSpaceBundleId,
                        principalTable: "WorkSpaceBundlePricing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkSpaceReservationBundle",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BundleId = table.Column<long>(type: "bigint", nullable: false),
                    BundlePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BundleStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BundleEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    BasicUserId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: true),
                    WorkSpaceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceReservationBundle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSpaceReservationBundle_BasicUser_BasicUserId",
                        column: x => x.BasicUserId,
                        principalTable: "BasicUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceReservationBundle_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceReservationBundle_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkSpaceReservationBundle_WorkSpace_WorkSpaceId",
                        column: x => x.WorkSpaceId,
                        principalTable: "WorkSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceReservationBundle_WorkSpaceBundlePricing_BundleId",
                        column: x => x.BundleId,
                        principalTable: "WorkSpaceBundlePricing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkSpaceReservationHourly",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HourId = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HourlyDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsDay = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    BasicUserId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: true),
                    WorkSpaceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceReservationHourly", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSpaceReservationHourly_BasicUser_BasicUserId",
                        column: x => x.BasicUserId,
                        principalTable: "BasicUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceReservationHourly_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceReservationHourly_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkSpaceReservationHourly_WorkSpace_WorkSpaceId",
                        column: x => x.WorkSpaceId,
                        principalTable: "WorkSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceReservationHourly_WorkSpaceHourlyPricing_HourId",
                        column: x => x.HourId,
                        principalTable: "WorkSpaceHourlyPricing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkSpaceTailoredCancellation",
                columns: table => new
                {
                    WorkSpaceTailoredReservationId = table.Column<long>(type: "bigint", nullable: false),
                    CancellationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceTailoredCancellation", x => new { x.WorkSpaceTailoredReservationId, x.CancellationId });
                    table.ForeignKey(
                        name: "FK_WorkSpaceTailoredCancellation_CancelReservation_CancellationId",
                        column: x => x.CancellationId,
                        principalTable: "CancelReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceTailoredCancellation_WorkSpaceReservationTailored_WorkSpaceTailoredReservationId",
                        column: x => x.WorkSpaceTailoredReservationId,
                        principalTable: "WorkSpaceReservationTailored",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkSpaceTailoredTopUp",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TailoredHours = table.Column<int>(type: "int", nullable: false),
                    TailoredPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WorkSpaceReservationTailoredId = table.Column<long>(type: "bigint", nullable: false),
                    BasicUserId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceTailoredTopUp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSpaceTailoredTopUp_BasicUser_BasicUserId",
                        column: x => x.BasicUserId,
                        principalTable: "BasicUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkSpaceTailoredTopUp_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceTailoredTopUp_WorkSpaceReservationTailored_WorkSpaceReservationTailoredId",
                        column: x => x.WorkSpaceReservationTailoredId,
                        principalTable: "WorkSpaceReservationTailored",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkSpaceTailoredTransaction",
                columns: table => new
                {
                    WorkSpaceReservationTailoredId = table.Column<long>(type: "bigint", nullable: false),
                    ReservationTransactionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceTailoredTransaction", x => new { x.WorkSpaceReservationTailoredId, x.ReservationTransactionId });
                    table.ForeignKey(
                        name: "FK_WorkSpaceTailoredTransaction_ReservationTransaction_ReservationTransactionId",
                        column: x => x.ReservationTransactionId,
                        principalTable: "ReservationTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceTailoredTransaction_WorkSpaceReservationTailored_WorkSpaceReservationTailoredId",
                        column: x => x.WorkSpaceReservationTailoredId,
                        principalTable: "WorkSpaceReservationTailored",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeetingAttendee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeetingReservationId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingAttendee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingAttendee_MeetingReservation_MeetingReservationId",
                        column: x => x.MeetingReservationId,
                        principalTable: "MeetingReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeetingReservationCancellation",
                columns: table => new
                {
                    MeetingReservationId = table.Column<long>(type: "bigint", nullable: false),
                    CancellationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingReservationCancellation", x => new { x.MeetingReservationId, x.CancellationId });
                    table.ForeignKey(
                        name: "FK_MeetingReservationCancellation_CancelReservation_CancellationId",
                        column: x => x.CancellationId,
                        principalTable: "CancelReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeetingReservationCancellation_MeetingReservation_MeetingReservationId",
                        column: x => x.MeetingReservationId,
                        principalTable: "MeetingReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeetingReservationTopUp",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeetingReservationId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: false),
                    MeetingSpaceHourlyPricingId = table.Column<long>(type: "bigint", nullable: false),
                    BasicUserId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingReservationTopUp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingReservationTopUp_BasicUser_BasicUserId",
                        column: x => x.BasicUserId,
                        principalTable: "BasicUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeetingReservationTopUp_MeetingReservation_MeetingReservationId",
                        column: x => x.MeetingReservationId,
                        principalTable: "MeetingReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeetingReservationTopUp_MeetingSpaceHourlyPricing_MeetingSpaceHourlyPricingId",
                        column: x => x.MeetingSpaceHourlyPricingId,
                        principalTable: "MeetingSpaceHourlyPricing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeetingReservationTopUp_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeetingReservationTransaction",
                columns: table => new
                {
                    MeetingReservationId = table.Column<long>(type: "bigint", nullable: false),
                    ReservationTransactionId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingReservationTransaction", x => new { x.MeetingReservationId, x.ReservationTransactionId });
                    table.ForeignKey(
                        name: "FK_MeetingReservationTransaction_MeetingReservation_MeetingReservationId",
                        column: x => x.MeetingReservationId,
                        principalTable: "MeetingReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeetingReservationTransaction_ReservationTransaction_ReservationTransactionId",
                        column: x => x.ReservationTransactionId,
                        principalTable: "ReservationTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkSpaceBundleCancellation",
                columns: table => new
                {
                    WorkSpaceBundleReservationId = table.Column<long>(type: "bigint", nullable: false),
                    CancellationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceBundleCancellation", x => new { x.WorkSpaceBundleReservationId, x.CancellationId });
                    table.ForeignKey(
                        name: "FK_WorkSpaceBundleCancellation_CancelReservation_CancellationId",
                        column: x => x.CancellationId,
                        principalTable: "CancelReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceBundleCancellation_WorkSpaceReservationBundle_WorkSpaceBundleReservationId",
                        column: x => x.WorkSpaceBundleReservationId,
                        principalTable: "WorkSpaceReservationBundle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkSpaceBundleTransaction",
                columns: table => new
                {
                    WorkSpaceReservationBundleId = table.Column<long>(type: "bigint", nullable: false),
                    ReservationTransactionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceBundleTransaction", x => new { x.WorkSpaceReservationBundleId, x.ReservationTransactionId });
                    table.ForeignKey(
                        name: "FK_WorkSpaceBundleTransaction_ReservationTransaction_ReservationTransactionId",
                        column: x => x.ReservationTransactionId,
                        principalTable: "ReservationTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceBundleTransaction_WorkSpaceReservationBundle_WorkSpaceReservationBundleId",
                        column: x => x.WorkSpaceReservationBundleId,
                        principalTable: "WorkSpaceReservationBundle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkSpaceHourlyCancellation",
                columns: table => new
                {
                    WorkSpaceHourlyReservationId = table.Column<long>(type: "bigint", nullable: false),
                    CancellationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceHourlyCancellation", x => new { x.WorkSpaceHourlyReservationId, x.CancellationId });
                    table.ForeignKey(
                        name: "FK_WorkSpaceHourlyCancellation_CancelReservation_CancellationId",
                        column: x => x.CancellationId,
                        principalTable: "CancelReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceHourlyCancellation_WorkSpaceReservationHourly_WorkSpaceHourlyReservationId",
                        column: x => x.WorkSpaceHourlyReservationId,
                        principalTable: "WorkSpaceReservationHourly",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkSpaceHourlyTopUp",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HourId = table.Column<long>(type: "bigint", nullable: false),
                    HourlyTotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WorkSpaceReservationHourlyId = table.Column<long>(type: "bigint", nullable: false),
                    BasicUserId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceHourlyTopUp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSpaceHourlyTopUp_BasicUser_BasicUserId",
                        column: x => x.BasicUserId,
                        principalTable: "BasicUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkSpaceHourlyTopUp_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceHourlyTopUp_WorkSpaceHourlyPricing_HourId",
                        column: x => x.HourId,
                        principalTable: "WorkSpaceHourlyPricing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceHourlyTopUp_WorkSpaceReservationHourly_WorkSpaceReservationHourlyId",
                        column: x => x.WorkSpaceReservationHourlyId,
                        principalTable: "WorkSpaceReservationHourly",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkSpaceHourlyTransaction",
                columns: table => new
                {
                    WorkSpaceReservationHourlyId = table.Column<long>(type: "bigint", nullable: false),
                    ReservationTransactionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceHourlyTransaction", x => new { x.WorkSpaceReservationHourlyId, x.ReservationTransactionId });
                    table.ForeignKey(
                        name: "FK_WorkSpaceHourlyTransaction_ReservationTransaction_ReservationTransactionId",
                        column: x => x.ReservationTransactionId,
                        principalTable: "ReservationTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceHourlyTransaction_WorkSpaceReservationHourly_WorkSpaceReservationHourlyId",
                        column: x => x.WorkSpaceReservationHourlyId,
                        principalTable: "WorkSpaceReservationHourly",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Admin",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Admin",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AdminRole",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AdminRoleClaim_RoleId",
                table: "AdminRoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUserClaim_UserId",
                table: "AdminUserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicUser_CountryId",
                table: "BasicUser",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicUser_GenderId",
                table: "BasicUser",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicUser_MembershipCategoryId",
                table: "BasicUser",
                column: "MembershipCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicUser_MemberShipTypesId",
                table: "BasicUser",
                column: "MemberShipTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicUser_UserDeviceId",
                table: "BasicUser",
                column: "UserDeviceId",
                unique: true,
                filter: "[UserDeviceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Building_LocationId",
                table: "Building",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingFloor_BuildingId",
                table: "BuildingFloor",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_CancelReservation_AdminId",
                table: "CancelReservation",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_CancelReservation_BasicUserId",
                table: "CancelReservation",
                column: "BasicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CancelReservation_ReservationTypeId",
                table: "CancelReservation",
                column: "ReservationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_LobSpaceTypeId",
                table: "Category",
                column: "LobSpaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryId",
                table: "City",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_EventSpaceBookingId",
                table: "ContactDetails",
                column: "EventSpaceBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Coworking_LocationId",
                table: "Coworking",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceBundleCancellation_CancellationId",
                table: "CoworkingSpaceBundleCancellation",
                column: "CancellationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceBundleCancellation_CoworkingSpaceReservationBundleId",
                table: "CoworkingSpaceBundleCancellation",
                column: "CoworkingSpaceReservationBundleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceBundleMemberType_MemberTypeId",
                table: "CoworkingSpaceBundleMemberType",
                column: "MemberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceBundlePricing_CoworkingId",
                table: "CoworkingSpaceBundlePricing",
                column: "CoworkingId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceBundleTransaction_CoworkingSpaceReservationBundleId",
                table: "CoworkingSpaceBundleTransaction",
                column: "CoworkingSpaceReservationBundleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceBundleTransaction_ReservationTransactionId",
                table: "CoworkingSpaceBundleTransaction",
                column: "ReservationTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceHourlyCancellation_CancellationId",
                table: "CoworkingSpaceHourlyCancellation",
                column: "CancellationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceHourlyCancellation_CoworkingSpaceReservationHourlyId",
                table: "CoworkingSpaceHourlyCancellation",
                column: "CoworkingSpaceReservationHourlyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoWorkingSpaceHourlyPricing_CoworkingId",
                table: "CoWorkingSpaceHourlyPricing",
                column: "CoworkingId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceHourlyTop_CoworkingSpaceReservationHourlyId",
                table: "CoworkingSpaceHourlyTop",
                column: "CoworkingSpaceReservationHourlyId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceHourlyTop_HourId",
                table: "CoworkingSpaceHourlyTop",
                column: "HourId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceHourlyTop_PaymentMethodId",
                table: "CoworkingSpaceHourlyTop",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceHourlyTransaction_CoworkingSpaceReservationHourlyId",
                table: "CoworkingSpaceHourlyTransaction",
                column: "CoworkingSpaceReservationHourlyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceHourlyTransaction_ReservationTransactionId",
                table: "CoworkingSpaceHourlyTransaction",
                column: "ReservationTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationBundle_BasicUserId",
                table: "CoworkingSpaceReservationBundle",
                column: "BasicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationBundle_BundleId",
                table: "CoworkingSpaceReservationBundle",
                column: "BundleId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationBundle_CoworkingId",
                table: "CoworkingSpaceReservationBundle",
                column: "CoworkingId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationBundle_CoworkSpaceId",
                table: "CoworkingSpaceReservationBundle",
                column: "CoworkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationBundle_LocationId",
                table: "CoworkingSpaceReservationBundle",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationBundle_PaymentMethodId",
                table: "CoworkingSpaceReservationBundle",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationHourly_BasicUserId",
                table: "CoworkingSpaceReservationHourly",
                column: "BasicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationHourly_CoworkingId",
                table: "CoworkingSpaceReservationHourly",
                column: "CoworkingId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationHourly_CoworkSpaceId",
                table: "CoworkingSpaceReservationHourly",
                column: "CoworkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationHourly_HourId",
                table: "CoworkingSpaceReservationHourly",
                column: "HourId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationHourly_LocationId",
                table: "CoworkingSpaceReservationHourly",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationHourly_PaymentMethodId",
                table: "CoworkingSpaceReservationHourly",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationTailored_BasicUserId",
                table: "CoworkingSpaceReservationTailored",
                column: "BasicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationTailored_CoworkingId",
                table: "CoworkingSpaceReservationTailored",
                column: "CoworkingId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationTailored_CoworkSpaceId",
                table: "CoworkingSpaceReservationTailored",
                column: "CoworkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationTailored_LocationId",
                table: "CoworkingSpaceReservationTailored",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationTailored_PaymentMethodId",
                table: "CoworkingSpaceReservationTailored",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceTailoredCancellation_CancellationId",
                table: "CoworkingSpaceTailoredCancellation",
                column: "CancellationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceTailoredCancellation_CoworkingSpaceReservationTailoredId",
                table: "CoworkingSpaceTailoredCancellation",
                column: "CoworkingSpaceReservationTailoredId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceTailoredPricing_CoworkingId",
                table: "CoworkingSpaceTailoredPricing",
                column: "CoworkingId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceTailoredTopUp_CoworkingSpaceReservationTailoredId",
                table: "CoworkingSpaceTailoredTopUp",
                column: "CoworkingSpaceReservationTailoredId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceTailoredTopUp_PaymentMethodId",
                table: "CoworkingSpaceTailoredTopUp",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceTailoredTransaction_CoworkingSpaceReservationTailoredId",
                table: "CoworkingSpaceTailoredTransaction",
                column: "CoworkingSpaceReservationTailoredId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceTailoredTransaction_ReservationTransactionId",
                table: "CoworkingSpaceTailoredTransaction",
                column: "ReservationTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingWorkSpace_BuildingFloorId",
                table: "CoworkingWorkSpace",
                column: "BuildingFloorId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingWorkSpace_CoworkingId",
                table: "CoworkingWorkSpace",
                column: "CoworkingId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingWorkSpace_LocationId",
                table: "CoworkingWorkSpace",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingWorkSpace_WorkSpaceCategoryId",
                table: "CoworkingWorkSpace",
                column: "WorkSpaceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingWorkSpace_WorkSpaceTypeId",
                table: "CoworkingWorkSpace",
                column: "WorkSpaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingWorkspaceFurnishing_CoworkingWorkSpaceId",
                table: "CoworkingWorkspaceFurnishing",
                column: "CoworkingWorkSpaceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingWorkspaceFurnishing_FurnishingId",
                table: "CoworkingWorkspaceFurnishing",
                column: "FurnishingId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingWorkSpaceMarketingImage_CoworkingWorkSpaceId",
                table: "CoworkingWorkSpaceMarketingImage",
                column: "CoworkingWorkSpaceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingWorkSpaceMarketingImage_MarketingImagesId",
                table: "CoworkingWorkSpaceMarketingImage",
                column: "MarketingImagesId");

            migrationBuilder.CreateIndex(
                name: "IX_District_CityId",
                table: "District",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplate_EmailTemplateTypeID",
                table: "EmailTemplate",
                column: "EmailTemplateTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpace_BuildingFloorId",
                table: "EventSpace",
                column: "BuildingFloorId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpace_LocationId",
                table: "EventSpace",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpaceBooking_EventAttendanceId",
                table: "EventSpaceBooking",
                column: "EventAttendanceId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpaceBooking_EventCategoryId",
                table: "EventSpaceBooking",
                column: "EventCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpaceBooking_EventOpportunityStatusId",
                table: "EventSpaceBooking",
                column: "EventOpportunityStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpaceBooking_EventReccuranceId",
                table: "EventSpaceBooking",
                column: "EventReccuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpaceBooking_EventRequesterId",
                table: "EventSpaceBooking",
                column: "EventRequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpaceBooking_EventTypeId",
                table: "EventSpaceBooking",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpaceBooking_IndustryNameId",
                table: "EventSpaceBooking",
                column: "IndustryNameId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpaceBooking_InitiatedId",
                table: "EventSpaceBooking",
                column: "InitiatedId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpaceBooking_LobLocationTypeId",
                table: "EventSpaceBooking",
                column: "LobLocationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpaceBooking_LocationNameId",
                table: "EventSpaceBooking",
                column: "LocationNameId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpaceBooking_OpportunityStageId",
                table: "EventSpaceBooking",
                column: "OpportunityStageId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpaceHourlyPricing_EventSpaceId",
                table: "EventSpaceHourlyPricing",
                column: "EventSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpaceHourlyPricing_MemberTypeId",
                table: "EventSpaceHourlyPricing",
                column: "MemberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpaceOccupancy_EventSpaceId",
                table: "EventSpaceOccupancy",
                column: "EventSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpaceOccupancy_VenueSetupId",
                table: "EventSpaceOccupancy",
                column: "VenueSetupId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpaceTime_EventSpaceBookingId",
                table: "EventSpaceTime",
                column: "EventSpaceBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpaceVenues_EventSpaceBookingId",
                table: "EventSpaceVenues",
                column: "EventSpaceBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Faq_CategoryId",
                table: "Faq",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Faq_LobSpaceTypeId",
                table: "Faq",
                column: "LobSpaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteLocation_BasicUserId",
                table: "FavouriteLocation",
                column: "BasicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteLocation_LocationId",
                table: "FavouriteLocation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Furnishing_FeatureId",
                table: "Furnishing",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Furnishing_FurnishingTypeId",
                table: "Furnishing",
                column: "FurnishingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserLogin<string>_UserId",
                table: "IdentityUserLogin<string>",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserRole<string>_RoleId",
                table: "IdentityUserRole<string>",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueCaseStage_LobSpaceTypeId",
                table: "IssueCaseStage",
                column: "LobSpaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueReport_CaseTypeId",
                table: "IssueReport",
                column: "CaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueReport_CreatedBy",
                table: "IssueReport",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IssueReport_LevelSeverityId",
                table: "IssueReport",
                column: "LevelSeverityId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueReport_LobSpaceTypeId",
                table: "IssueReport",
                column: "LobSpaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueReport_LocationId",
                table: "IssueReport",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueReport_OwnerId",
                table: "IssueReport",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueReport_PriorityId",
                table: "IssueReport",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueReport_StatusId",
                table: "IssueReport",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_CityId",
                table: "Location",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_CountryId",
                table: "Location",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_CurrencyId",
                table: "Location",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_DistrictId",
                table: "Location",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_LocationTypeId",
                table: "Location",
                column: "LocationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationBankAccount_LocationId",
                table: "LocationBankAccount",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationContact_LocationId",
                table: "LocationContact",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationCurrency_CurrencyId",
                table: "LocationCurrency",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationCurrency_LocationId",
                table: "LocationCurrency",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationFeature_FeatureId",
                table: "LocationFeature",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationFeature_LocationId",
                table: "LocationFeature",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationFile_LocationId",
                table: "LocationFile",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationImage_LocationId",
                table: "LocationImage",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationInclusion_InclusionId",
                table: "LocationInclusion",
                column: "InclusionId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationInclusion_LocationId",
                table: "LocationInclusion",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationIndustry_IndustryId",
                table: "LocationIndustry",
                column: "IndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationIndustry_LocationId",
                table: "LocationIndustry",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationWorkingHour_LocationId",
                table: "LocationWorkingHour",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketingImages_FeatureId",
                table: "MarketingImages",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingAttendee_MeetingReservationId",
                table: "MeetingAttendee",
                column: "MeetingReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservation_BasicUserId",
                table: "MeetingReservation",
                column: "BasicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservation_LocationId",
                table: "MeetingReservation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservation_MeetingSpaceHourlyPricingId",
                table: "MeetingReservation",
                column: "MeetingSpaceHourlyPricingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservation_MeetingSpaceId",
                table: "MeetingReservation",
                column: "MeetingSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservation_PaymentMethodId",
                table: "MeetingReservation",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservationCancellation_CancellationId",
                table: "MeetingReservationCancellation",
                column: "CancellationId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservationCancellation_MeetingReservationId",
                table: "MeetingReservationCancellation",
                column: "MeetingReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservationTopUp_BasicUserId",
                table: "MeetingReservationTopUp",
                column: "BasicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservationTopUp_MeetingReservationId",
                table: "MeetingReservationTopUp",
                column: "MeetingReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservationTopUp_MeetingSpaceHourlyPricingId",
                table: "MeetingReservationTopUp",
                column: "MeetingSpaceHourlyPricingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservationTopUp_PaymentMethodId",
                table: "MeetingReservationTopUp",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservationTransaction_MeetingReservationId",
                table: "MeetingReservationTransaction",
                column: "MeetingReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservationTransaction_ReservationTransactionId",
                table: "MeetingReservationTransaction",
                column: "ReservationTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingSpace_BuildingFloorId",
                table: "MeetingSpace",
                column: "BuildingFloorId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingSpace_LocationId",
                table: "MeetingSpace",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingSpaceHourlyPricing_MeetingSpaceId",
                table: "MeetingSpaceHourlyPricing",
                column: "MeetingSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingSpaceHourlyPricing_MemberTypeId",
                table: "MeetingSpaceHourlyPricing",
                column: "MemberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberShipCategories_BenefitTypeId",
                table: "MemberShipCategories",
                column: "BenefitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberShipCategories_MainCategoryId",
                table: "MemberShipCategories",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityStageReport_EventSpaceBookingId",
                table: "OpportunityStageReport",
                column: "EventSpaceBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityStageReport_OpportunityStageId",
                table: "OpportunityStageReport",
                column: "OpportunityStageId");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_LobSpaceTypeId",
                table: "Plan",
                column: "LobSpaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_TypeId",
                table: "Plan",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_LobSpaceTypeId",
                table: "Policy",
                column: "LobSpaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Policy_PolicyTypeId",
                table: "Policy",
                column: "PolicyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDetail_BasicUserId",
                table: "ReservationDetail",
                column: "BasicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDetail_ReservationTransactionId",
                table: "ReservationDetail",
                column: "ReservationTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTransaction_BasicUserId",
                table: "ReservationTransaction",
                column: "BasicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTransaction_LocationId",
                table: "ReservationTransaction",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTransaction_ReservationTypeId",
                table: "ReservationTransaction",
                column: "ReservationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SendEmail_ContactDetailId",
                table: "SendEmail",
                column: "ContactDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_SendEmail_EmailTemplateId",
                table: "SendEmail",
                column: "EmailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SendEmail_EventSpaceBookingId",
                table: "SendEmail",
                column: "EventSpaceBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceFeePaymentsDueDate_LocationId",
                table: "ServiceFeePaymentsDueDate",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceAmenity_AmenityId",
                table: "SpaceAmenity",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceAmenity_FeatureId",
                table: "SpaceAmenity",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_TopUp_LobSpaceTypeId",
                table: "TopUp",
                column: "LobSpaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TopUp_TopUpTypeId",
                table: "TopUp",
                column: "TopUpTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Wifi_LobSpaceTypeId",
                table: "Wifi",
                column: "LobSpaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpace_BuildingFloorId",
                table: "WorkSpace",
                column: "BuildingFloorId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpace_LocationId",
                table: "WorkSpace",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpace_WorkSpaceCategoryId",
                table: "WorkSpace",
                column: "WorkSpaceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpace_WorkSpaceTypeId",
                table: "WorkSpace",
                column: "WorkSpaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceBundleCancellation_CancellationId",
                table: "WorkSpaceBundleCancellation",
                column: "CancellationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceBundleCancellation_WorkSpaceBundleReservationId",
                table: "WorkSpaceBundleCancellation",
                column: "WorkSpaceBundleReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceBundleMemberType_MemberTypeId",
                table: "WorkSpaceBundleMemberType",
                column: "MemberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceBundlePricing_WorkSpaceId",
                table: "WorkSpaceBundlePricing",
                column: "WorkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceBundleTransaction_ReservationTransactionId",
                table: "WorkSpaceBundleTransaction",
                column: "ReservationTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceBundleTransaction_WorkSpaceReservationBundleId",
                table: "WorkSpaceBundleTransaction",
                column: "WorkSpaceReservationBundleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyCancellation_CancellationId",
                table: "WorkSpaceHourlyCancellation",
                column: "CancellationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyCancellation_WorkSpaceHourlyReservationId",
                table: "WorkSpaceHourlyCancellation",
                column: "WorkSpaceHourlyReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyPricing_WorkSpaceId",
                table: "WorkSpaceHourlyPricing",
                column: "WorkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyTopUp_BasicUserId",
                table: "WorkSpaceHourlyTopUp",
                column: "BasicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyTopUp_HourId",
                table: "WorkSpaceHourlyTopUp",
                column: "HourId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyTopUp_PaymentMethodId",
                table: "WorkSpaceHourlyTopUp",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyTopUp_WorkSpaceReservationHourlyId",
                table: "WorkSpaceHourlyTopUp",
                column: "WorkSpaceReservationHourlyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyTransaction_ReservationTransactionId",
                table: "WorkSpaceHourlyTransaction",
                column: "ReservationTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyTransaction_WorkSpaceReservationHourlyId",
                table: "WorkSpaceHourlyTransaction",
                column: "WorkSpaceReservationHourlyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationBundle_BasicUserId",
                table: "WorkSpaceReservationBundle",
                column: "BasicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationBundle_BundleId",
                table: "WorkSpaceReservationBundle",
                column: "BundleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationBundle_LocationId",
                table: "WorkSpaceReservationBundle",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationBundle_PaymentMethodId",
                table: "WorkSpaceReservationBundle",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationBundle_WorkSpaceId",
                table: "WorkSpaceReservationBundle",
                column: "WorkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationHourly_BasicUserId",
                table: "WorkSpaceReservationHourly",
                column: "BasicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationHourly_HourId",
                table: "WorkSpaceReservationHourly",
                column: "HourId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationHourly_LocationId",
                table: "WorkSpaceReservationHourly",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationHourly_PaymentMethodId",
                table: "WorkSpaceReservationHourly",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationHourly_WorkSpaceId",
                table: "WorkSpaceReservationHourly",
                column: "WorkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationTailored_BasicUserId",
                table: "WorkSpaceReservationTailored",
                column: "BasicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationTailored_LocationId",
                table: "WorkSpaceReservationTailored",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationTailored_PaymentMethodId",
                table: "WorkSpaceReservationTailored",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationTailored_WorkSpaceId",
                table: "WorkSpaceReservationTailored",
                column: "WorkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceTailoredCancellation_CancellationId",
                table: "WorkSpaceTailoredCancellation",
                column: "CancellationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceTailoredCancellation_WorkSpaceTailoredReservationId",
                table: "WorkSpaceTailoredCancellation",
                column: "WorkSpaceTailoredReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceTailoredPricing_WorkSpaceId",
                table: "WorkSpaceTailoredPricing",
                column: "WorkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceTailoredTopUp_BasicUserId",
                table: "WorkSpaceTailoredTopUp",
                column: "BasicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceTailoredTopUp_PaymentMethodId",
                table: "WorkSpaceTailoredTopUp",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceTailoredTopUp_WorkSpaceReservationTailoredId",
                table: "WorkSpaceTailoredTopUp",
                column: "WorkSpaceReservationTailoredId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceTailoredTransaction_ReservationTransactionId",
                table: "WorkSpaceTailoredTransaction",
                column: "ReservationTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceTailoredTransaction_WorkSpaceReservationTailoredId",
                table: "WorkSpaceTailoredTransaction",
                column: "WorkSpaceReservationTailoredId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceType_WorkSpaceCategoryId",
                table: "WorkSpaceType",
                column: "WorkSpaceCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminRoleClaim");

            migrationBuilder.DropTable(
                name: "AdminUserClaim");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceBundleCancellation");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceBundleMemberType");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceBundleTransaction");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceHourlyCancellation");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceHourlyTop");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceHourlyTransaction");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceTailoredCancellation");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceTailoredPricing");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceTailoredTopUp");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceTailoredTransaction");

            migrationBuilder.DropTable(
                name: "CoworkingWorkspaceFurnishing");

            migrationBuilder.DropTable(
                name: "CoworkingWorkSpaceMarketingImage");

            migrationBuilder.DropTable(
                name: "EventSpaceHourlyPricing");

            migrationBuilder.DropTable(
                name: "EventSpaceOccupancy");

            migrationBuilder.DropTable(
                name: "EventSpaceTime");

            migrationBuilder.DropTable(
                name: "EventSpaceVenues");

            migrationBuilder.DropTable(
                name: "Faq");

            migrationBuilder.DropTable(
                name: "FavouriteLocation");

            migrationBuilder.DropTable(
                name: "IdentityUserLogin<string>");

            migrationBuilder.DropTable(
                name: "IdentityUserRole<string>");

            migrationBuilder.DropTable(
                name: "IdentityUserToken<string>");

            migrationBuilder.DropTable(
                name: "IssueCaseStage");

            migrationBuilder.DropTable(
                name: "LocationBankAccount");

            migrationBuilder.DropTable(
                name: "LocationContact");

            migrationBuilder.DropTable(
                name: "LocationCurrency");

            migrationBuilder.DropTable(
                name: "LocationFeature");

            migrationBuilder.DropTable(
                name: "LocationFile");

            migrationBuilder.DropTable(
                name: "LocationImage");

            migrationBuilder.DropTable(
                name: "LocationInclusion");

            migrationBuilder.DropTable(
                name: "LocationIndustry");

            migrationBuilder.DropTable(
                name: "LocationWorkingHour");

            migrationBuilder.DropTable(
                name: "MeetingAttendee");

            migrationBuilder.DropTable(
                name: "MeetingReservationCancellation");

            migrationBuilder.DropTable(
                name: "MeetingReservationTopUp");

            migrationBuilder.DropTable(
                name: "MeetingReservationTransaction");

            migrationBuilder.DropTable(
                name: "OpportunityStageReport");

            migrationBuilder.DropTable(
                name: "Plan");

            migrationBuilder.DropTable(
                name: "Policy");

            migrationBuilder.DropTable(
                name: "ReservationDetail");

            migrationBuilder.DropTable(
                name: "SendEmail");

            migrationBuilder.DropTable(
                name: "ServiceFeePaymentsDueDate");

            migrationBuilder.DropTable(
                name: "SpaceAmenity");

            migrationBuilder.DropTable(
                name: "TopUp");

            migrationBuilder.DropTable(
                name: "Wifi");

            migrationBuilder.DropTable(
                name: "WorkSpaceBundleCancellation");

            migrationBuilder.DropTable(
                name: "WorkSpaceBundleMemberType");

            migrationBuilder.DropTable(
                name: "WorkSpaceBundleTransaction");

            migrationBuilder.DropTable(
                name: "WorkSpaceHourlyCancellation");

            migrationBuilder.DropTable(
                name: "WorkSpaceHourlyTopUp");

            migrationBuilder.DropTable(
                name: "WorkSpaceHourlyTransaction");

            migrationBuilder.DropTable(
                name: "WorkSpaceTailoredCancellation");

            migrationBuilder.DropTable(
                name: "WorkSpaceTailoredPricing");

            migrationBuilder.DropTable(
                name: "WorkSpaceTailoredTopUp");

            migrationBuilder.DropTable(
                name: "WorkSpaceTailoredTransaction");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceReservationBundle");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceReservationHourly");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceReservationTailored");

            migrationBuilder.DropTable(
                name: "Furnishing");

            migrationBuilder.DropTable(
                name: "MarketingImages");

            migrationBuilder.DropTable(
                name: "EventSpace");

            migrationBuilder.DropTable(
                name: "VenueSetup");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "AdminRole");

            migrationBuilder.DropTable(
                name: "IssueReport");

            migrationBuilder.DropTable(
                name: "Inclusion");

            migrationBuilder.DropTable(
                name: "MeetingReservation");

            migrationBuilder.DropTable(
                name: "PlanType");

            migrationBuilder.DropTable(
                name: "PolicyType");

            migrationBuilder.DropTable(
                name: "ContactDetails");

            migrationBuilder.DropTable(
                name: "EmailTemplate");

            migrationBuilder.DropTable(
                name: "Amenity");

            migrationBuilder.DropTable(
                name: "TopUpType");

            migrationBuilder.DropTable(
                name: "WorkSpaceReservationBundle");

            migrationBuilder.DropTable(
                name: "WorkSpaceReservationHourly");

            migrationBuilder.DropTable(
                name: "CancelReservation");

            migrationBuilder.DropTable(
                name: "ReservationTransaction");

            migrationBuilder.DropTable(
                name: "WorkSpaceReservationTailored");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceBundlePricing");

            migrationBuilder.DropTable(
                name: "CoWorkingSpaceHourlyPricing");

            migrationBuilder.DropTable(
                name: "CoworkingWorkSpace");

            migrationBuilder.DropTable(
                name: "FurnishingType");

            migrationBuilder.DropTable(
                name: "Feature");

            migrationBuilder.DropTable(
                name: "CaseType");

            migrationBuilder.DropTable(
                name: "Priority");

            migrationBuilder.DropTable(
                name: "Severity");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "MeetingSpaceHourlyPricing");

            migrationBuilder.DropTable(
                name: "EventSpaceBooking");

            migrationBuilder.DropTable(
                name: "EmailTemplateType");

            migrationBuilder.DropTable(
                name: "WorkSpaceBundlePricing");

            migrationBuilder.DropTable(
                name: "WorkSpaceHourlyPricing");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "ReservationType");

            migrationBuilder.DropTable(
                name: "BasicUser");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "Coworking");

            migrationBuilder.DropTable(
                name: "MeetingSpace");

            migrationBuilder.DropTable(
                name: "MemberType");

            migrationBuilder.DropTable(
                name: "EventAttendance");

            migrationBuilder.DropTable(
                name: "EventCategory");

            migrationBuilder.DropTable(
                name: "EventOpportunityStatus");

            migrationBuilder.DropTable(
                name: "EventReccurance");

            migrationBuilder.DropTable(
                name: "EventRequester");

            migrationBuilder.DropTable(
                name: "EventType");

            migrationBuilder.DropTable(
                name: "Industry");

            migrationBuilder.DropTable(
                name: "Initiated");

            migrationBuilder.DropTable(
                name: "OpportunityStage");

            migrationBuilder.DropTable(
                name: "WorkSpace");

            migrationBuilder.DropTable(
                name: "ClientDevice");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "MemberShipCategories");

            migrationBuilder.DropTable(
                name: "MemberShipTypes");

            migrationBuilder.DropTable(
                name: "BuildingFloor");

            migrationBuilder.DropTable(
                name: "WorkSpaceType");

            migrationBuilder.DropTable(
                name: "MemberShipBenefitsTypes");

            migrationBuilder.DropTable(
                name: "MemberShipMainCategories");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "WorkSpaceCategory");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "LocationType");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
