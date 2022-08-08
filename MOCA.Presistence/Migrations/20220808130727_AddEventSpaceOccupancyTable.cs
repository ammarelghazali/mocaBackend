using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class AddEventSpaceOccupancyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FemaleRestroomCount",
                table: "Building",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaleRestroomCount",
                table: "Building",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Amenity",
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
                    table.PrimaryKey("PK_Amenity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventSpace",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrossArea = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    NetArea = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
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
                    BuildingFloorId = table.Column<long>(type: "bigint", nullable: false)
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
                name: "MeetingReservation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    NumOfAttendees = table.Column<int>(type: "int", nullable: false),
                    MeetingroomId = table.Column<long>(type: "bigint", nullable: true),
                    MeetingRoomTimePriceId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    BasicUserId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: false)
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
                        name: "FK_MeetingReservation_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeetingSpace",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrossArea = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    NetArea = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
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
                    BuildingFloorId = table.Column<long>(type: "bigint", nullable: false)
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
                name: "WorkSpaceBundleTransactions",
                columns: table => new
                {
                    WorkSpaceReservationBundleId = table.Column<long>(type: "bigint", nullable: false),
                    ReservationTransactionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceBundleTransactions", x => new { x.WorkSpaceReservationBundleId, x.ReservationTransactionId });
                    table.ForeignKey(
                        name: "FK_WorkSpaceBundleTransactions_ReservationTransaction_ReservationTransactionId",
                        column: x => x.ReservationTransactionId,
                        principalTable: "ReservationTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceBundleTransactions_WorkSpaceReservationBundle_WorkSpaceReservationBundleId",
                        column: x => x.WorkSpaceReservationBundleId,
                        principalTable: "WorkSpaceReservationBundle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "WorkSpaceHourlyTransactions",
                columns: table => new
                {
                    WorkSpaceReservationHourlyId = table.Column<long>(type: "bigint", nullable: false),
                    ReservationTransactionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceHourlyTransactions", x => new { x.WorkSpaceReservationHourlyId, x.ReservationTransactionId });
                    table.ForeignKey(
                        name: "FK_WorkSpaceHourlyTransactions_ReservationTransaction_ReservationTransactionId",
                        column: x => x.ReservationTransactionId,
                        principalTable: "ReservationTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceHourlyTransactions_WorkSpaceReservationHourly_WorkSpaceReservationHourlyId",
                        column: x => x.WorkSpaceReservationHourlyId,
                        principalTable: "WorkSpaceReservationHourly",
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
                name: "WorkSpaceTailoredTransactions",
                columns: table => new
                {
                    WorkSpaceReservationTailoredId = table.Column<long>(type: "bigint", nullable: false),
                    ReservationTransactionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSpaceTailoredTransactions", x => new { x.WorkSpaceReservationTailoredId, x.ReservationTransactionId });
                    table.ForeignKey(
                        name: "FK_WorkSpaceTailoredTransactions_ReservationTransaction_ReservationTransactionId",
                        column: x => x.ReservationTransactionId,
                        principalTable: "ReservationTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkSpaceTailoredTransactions_WorkSpaceReservationTailored_WorkSpaceReservationTailoredId",
                        column: x => x.WorkSpaceReservationTailoredId,
                        principalTable: "WorkSpaceReservationTailored",
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
                    FeatureId = table.Column<long>(type: "bigint", nullable: false),
                    FurnishingTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Vendor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dimensions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpaceId = table.Column<long>(type: "bigint", nullable: false),
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
                name: "MeetingAttendee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeetingSpaceReservationId = table.Column<long>(type: "bigint", nullable: false),
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
                        name: "FK_MeetingAttendee_MeetingReservation_MeetingSpaceReservationId",
                        column: x => x.MeetingSpaceReservationId,
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
                    MeetingSpaceReservationId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: false),
                    MeetingRoomTimePriceId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        name: "FK_MeetingReservationTopUp_MeetingReservation_MeetingSpaceReservationId",
                        column: x => x.MeetingSpaceReservationId,
                        principalTable: "MeetingReservation",
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
                    ReservationTransactionId = table.Column<long>(type: "bigint", nullable: false)
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
                    PricePerHour = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
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
                    PricePerHour = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
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
                name: "WorkSpace",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrossArea = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    NetArea = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    WorkSpaceTypeId = table.Column<long>(type: "bigint", nullable: false),
                    MaximumOccupancy = table.Column<int>(type: "int", nullable: false),
                    IsFurnishing = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UnitNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstallAccessPoint = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingFloorId = table.Column<long>(type: "bigint", nullable: false)
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
                        name: "FK_WorkSpace_WorkSpaceType_WorkSpaceTypeId",
                        column: x => x.WorkSpaceTypeId,
                        principalTable: "WorkSpaceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationTailored_WorkSpaceId",
                table: "WorkSpaceReservationTailored",
                column: "WorkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationHourly_WorkSpaceId",
                table: "WorkSpaceReservationHourly",
                column: "WorkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationBundle_WorkSpaceId",
                table: "WorkSpaceReservationBundle",
                column: "WorkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpace_BuildingFloorId",
                table: "EventSpace",
                column: "BuildingFloorId");

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
                name: "IX_Furnishing_FeatureId",
                table: "Furnishing",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Furnishing_FurnishingTypeId",
                table: "Furnishing",
                column: "FurnishingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketingImages_FeatureId",
                table: "MarketingImages",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingAttendee_MeetingSpaceReservationId",
                table: "MeetingAttendee",
                column: "MeetingSpaceReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservation_BasicUserId",
                table: "MeetingReservation",
                column: "BasicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservation_LocationId",
                table: "MeetingReservation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservation_PaymentMethodId",
                table: "MeetingReservation",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservationTopUp_BasicUserId",
                table: "MeetingReservationTopUp",
                column: "BasicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservationTopUp_MeetingSpaceReservationId",
                table: "MeetingReservationTopUp",
                column: "MeetingSpaceReservationId");

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
                name: "IX_MeetingSpaceHourlyPricing_MeetingSpaceId",
                table: "MeetingSpaceHourlyPricing",
                column: "MeetingSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingSpaceHourlyPricing_MemberTypeId",
                table: "MeetingSpaceHourlyPricing",
                column: "MemberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceAmenity_AmenityId",
                table: "SpaceAmenity",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_SpaceAmenity_FeatureId",
                table: "SpaceAmenity",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpace_BuildingFloorId",
                table: "WorkSpace",
                column: "BuildingFloorId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpace_WorkSpaceTypeId",
                table: "WorkSpace",
                column: "WorkSpaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceBundleCancellation_CancellationId",
                table: "WorkSpaceBundleCancellation",
                column: "CancellationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceBundleCancellation_WorkSpaceBundleReservationId",
                table: "WorkSpaceBundleCancellation",
                column: "WorkSpaceBundleReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceBundleTransactions_ReservationTransactionId",
                table: "WorkSpaceBundleTransactions",
                column: "ReservationTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceBundleTransactions_WorkSpaceReservationBundleId",
                table: "WorkSpaceBundleTransactions",
                column: "WorkSpaceReservationBundleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyCancellation_CancellationId",
                table: "WorkSpaceHourlyCancellation",
                column: "CancellationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyCancellation_WorkSpaceHourlyReservationId",
                table: "WorkSpaceHourlyCancellation",
                column: "WorkSpaceHourlyReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyTransactions_ReservationTransactionId",
                table: "WorkSpaceHourlyTransactions",
                column: "ReservationTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyTransactions_WorkSpaceReservationHourlyId",
                table: "WorkSpaceHourlyTransactions",
                column: "WorkSpaceReservationHourlyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceTailoredCancellation_CancellationId",
                table: "WorkSpaceTailoredCancellation",
                column: "CancellationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceTailoredCancellation_WorkSpaceTailoredReservationId",
                table: "WorkSpaceTailoredCancellation",
                column: "WorkSpaceTailoredReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceTailoredTransactions_ReservationTransactionId",
                table: "WorkSpaceTailoredTransactions",
                column: "ReservationTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceTailoredTransactions_WorkSpaceReservationTailoredId",
                table: "WorkSpaceTailoredTransactions",
                column: "WorkSpaceReservationTailoredId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceType_WorkSpaceCategoryId",
                table: "WorkSpaceType",
                column: "WorkSpaceCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpaceReservationBundle_WorkSpace_WorkSpaceId",
                table: "WorkSpaceReservationBundle",
                column: "WorkSpaceId",
                principalTable: "WorkSpace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpaceReservationHourly_WorkSpace_WorkSpaceId",
                table: "WorkSpaceReservationHourly",
                column: "WorkSpaceId",
                principalTable: "WorkSpace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpaceReservationTailored_WorkSpace_WorkSpaceId",
                table: "WorkSpaceReservationTailored",
                column: "WorkSpaceId",
                principalTable: "WorkSpace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpaceReservationBundle_WorkSpace_WorkSpaceId",
                table: "WorkSpaceReservationBundle");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpaceReservationHourly_WorkSpace_WorkSpaceId",
                table: "WorkSpaceReservationHourly");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpaceReservationTailored_WorkSpace_WorkSpaceId",
                table: "WorkSpaceReservationTailored");

            migrationBuilder.DropTable(
                name: "EventSpaceHourlyPricing");

            migrationBuilder.DropTable(
                name: "EventSpaceOccupancy");

            migrationBuilder.DropTable(
                name: "Furnishing");

            migrationBuilder.DropTable(
                name: "MarketingImages");

            migrationBuilder.DropTable(
                name: "MeetingAttendee");

            migrationBuilder.DropTable(
                name: "MeetingReservationTopUp");

            migrationBuilder.DropTable(
                name: "MeetingReservationTransaction");

            migrationBuilder.DropTable(
                name: "MeetingSpaceHourlyPricing");

            migrationBuilder.DropTable(
                name: "SpaceAmenity");

            migrationBuilder.DropTable(
                name: "WorkSpace");

            migrationBuilder.DropTable(
                name: "WorkSpaceBundleCancellation");

            migrationBuilder.DropTable(
                name: "WorkSpaceBundleTransactions");

            migrationBuilder.DropTable(
                name: "WorkSpaceHourlyCancellation");

            migrationBuilder.DropTable(
                name: "WorkSpaceHourlyTransactions");

            migrationBuilder.DropTable(
                name: "WorkSpaceTailoredCancellation");

            migrationBuilder.DropTable(
                name: "WorkSpaceTailoredTransactions");

            migrationBuilder.DropTable(
                name: "EventSpace");

            migrationBuilder.DropTable(
                name: "VenueSetup");

            migrationBuilder.DropTable(
                name: "FurnishingType");

            migrationBuilder.DropTable(
                name: "MeetingReservation");

            migrationBuilder.DropTable(
                name: "MeetingSpace");

            migrationBuilder.DropTable(
                name: "MemberType");

            migrationBuilder.DropTable(
                name: "Amenity");

            migrationBuilder.DropTable(
                name: "WorkSpaceType");

            migrationBuilder.DropTable(
                name: "WorkSpaceCategory");

            migrationBuilder.DropIndex(
                name: "IX_WorkSpaceReservationTailored_WorkSpaceId",
                table: "WorkSpaceReservationTailored");

            migrationBuilder.DropIndex(
                name: "IX_WorkSpaceReservationHourly_WorkSpaceId",
                table: "WorkSpaceReservationHourly");

            migrationBuilder.DropIndex(
                name: "IX_WorkSpaceReservationBundle_WorkSpaceId",
                table: "WorkSpaceReservationBundle");

            migrationBuilder.DropColumn(
                name: "FemaleRestroomCount",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "MaleRestroomCount",
                table: "Building");
        }
    }
}
