using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class addInstallAccessPointToLocationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkSpaceBundleTransactions");

            migrationBuilder.DropTable(
                name: "WorkSpaceHourlyTransactions");

            migrationBuilder.DropTable(
                name: "WorkSpaceTailoredTransactions");

            migrationBuilder.DropIndex(
                name: "IX_WorkSpaceTailoredCancellation_CancellationId",
                table: "WorkSpaceTailoredCancellation");

            migrationBuilder.DropIndex(
                name: "IX_WorkSpaceHourlyCancellation_CancellationId",
                table: "WorkSpaceHourlyCancellation");

            migrationBuilder.DropIndex(
                name: "IX_WorkSpaceBundleCancellation_CancellationId",
                table: "WorkSpaceBundleCancellation");

            migrationBuilder.DropColumn(
                name: "PackageDiscount",
                table: "WorkSpaceReservationBundle");

            migrationBuilder.RenameColumn(
                name: "PackageStartDate",
                table: "WorkSpaceReservationBundle",
                newName: "BundleStartDate");

            migrationBuilder.RenameColumn(
                name: "PackagePrice",
                table: "WorkSpaceReservationBundle",
                newName: "BundlePrice");

            migrationBuilder.RenameColumn(
                name: "PackageId",
                table: "WorkSpaceReservationBundle",
                newName: "BundleId");

            migrationBuilder.RenameColumn(
                name: "PackageEndDate",
                table: "WorkSpaceReservationBundle",
                newName: "BundleEndDate");

            migrationBuilder.AddColumn<bool>(
                name: "IsDay",
                table: "WorkSpaceReservationHourly",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InstallAccessPoint",
                table: "Location",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceTailoredCancellation_CancellationId",
                table: "WorkSpaceTailoredCancellation",
                column: "CancellationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationHourly_HourId",
                table: "WorkSpaceReservationHourly",
                column: "HourId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationBundle_BundleId",
                table: "WorkSpaceReservationBundle",
                column: "BundleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyTopUp_HourId",
                table: "WorkSpaceHourlyTopUp",
                column: "HourId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyCancellation_CancellationId",
                table: "WorkSpaceHourlyCancellation",
                column: "CancellationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceBundleCancellation_CancellationId",
                table: "WorkSpaceBundleCancellation",
                column: "CancellationId",
                unique: true);

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
                name: "IX_WorkSpaceTailoredTransaction_ReservationTransactionId",
                table: "WorkSpaceTailoredTransaction",
                column: "ReservationTransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceTailoredTransaction_WorkSpaceReservationTailoredId",
                table: "WorkSpaceTailoredTransaction",
                column: "WorkSpaceReservationTailoredId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpaceHourlyTopUp_WorkSpaceHourlyPricing_HourId",
                table: "WorkSpaceHourlyTopUp",
                column: "HourId",
                principalTable: "WorkSpaceHourlyPricing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpaceReservationBundle_WorkSpaceBundlePricing_BundleId",
                table: "WorkSpaceReservationBundle",
                column: "BundleId",
                principalTable: "WorkSpaceBundlePricing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpaceReservationHourly_WorkSpaceHourlyPricing_HourId",
                table: "WorkSpaceReservationHourly",
                column: "HourId",
                principalTable: "WorkSpaceHourlyPricing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpaceHourlyTopUp_WorkSpaceHourlyPricing_HourId",
                table: "WorkSpaceHourlyTopUp");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpaceReservationBundle_WorkSpaceBundlePricing_BundleId",
                table: "WorkSpaceReservationBundle");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpaceReservationHourly_WorkSpaceHourlyPricing_HourId",
                table: "WorkSpaceReservationHourly");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceBundleCancellation");

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
                name: "CoworkingSpaceTailoredTopUp");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceTailoredTransaction");

            migrationBuilder.DropTable(
                name: "WorkSpaceBundleTransaction");

            migrationBuilder.DropTable(
                name: "WorkSpaceHourlyTransaction");

            migrationBuilder.DropTable(
                name: "WorkSpaceTailoredTransaction");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceReservationBundle");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceReservationHourly");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceReservationTailored");

            migrationBuilder.DropTable(
                name: "CoworkingWorkSpace");

            migrationBuilder.DropIndex(
                name: "IX_WorkSpaceTailoredCancellation_CancellationId",
                table: "WorkSpaceTailoredCancellation");

            migrationBuilder.DropIndex(
                name: "IX_WorkSpaceReservationHourly_HourId",
                table: "WorkSpaceReservationHourly");

            migrationBuilder.DropIndex(
                name: "IX_WorkSpaceReservationBundle_BundleId",
                table: "WorkSpaceReservationBundle");

            migrationBuilder.DropIndex(
                name: "IX_WorkSpaceHourlyTopUp_HourId",
                table: "WorkSpaceHourlyTopUp");

            migrationBuilder.DropIndex(
                name: "IX_WorkSpaceHourlyCancellation_CancellationId",
                table: "WorkSpaceHourlyCancellation");

            migrationBuilder.DropIndex(
                name: "IX_WorkSpaceBundleCancellation_CancellationId",
                table: "WorkSpaceBundleCancellation");

            migrationBuilder.DropColumn(
                name: "IsDay",
                table: "WorkSpaceReservationHourly");

            migrationBuilder.DropColumn(
                name: "InstallAccessPoint",
                table: "Location");

            migrationBuilder.RenameColumn(
                name: "BundleStartDate",
                table: "WorkSpaceReservationBundle",
                newName: "PackageStartDate");

            migrationBuilder.RenameColumn(
                name: "BundlePrice",
                table: "WorkSpaceReservationBundle",
                newName: "PackagePrice");

            migrationBuilder.RenameColumn(
                name: "BundleId",
                table: "WorkSpaceReservationBundle",
                newName: "PackageId");

            migrationBuilder.RenameColumn(
                name: "BundleEndDate",
                table: "WorkSpaceReservationBundle",
                newName: "PackageEndDate");

            migrationBuilder.AddColumn<decimal>(
                name: "PackageDiscount",
                table: "WorkSpaceReservationBundle",
                type: "decimal(18,2)",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceTailoredCancellation_CancellationId",
                table: "WorkSpaceTailoredCancellation",
                column: "CancellationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyCancellation_CancellationId",
                table: "WorkSpaceHourlyCancellation",
                column: "CancellationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceBundleCancellation_CancellationId",
                table: "WorkSpaceBundleCancellation",
                column: "CancellationId");

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
                name: "IX_WorkSpaceHourlyTransactions_ReservationTransactionId",
                table: "WorkSpaceHourlyTransactions",
                column: "ReservationTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyTransactions_WorkSpaceReservationHourlyId",
                table: "WorkSpaceHourlyTransactions",
                column: "WorkSpaceReservationHourlyId",
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
        }
    }
}
