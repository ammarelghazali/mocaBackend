using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class updateBuildingFloorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Number",
                table: "BuildingFloor");

            migrationBuilder.AddColumn<long>(
                name: "FloorNumber",
                table: "BuildingFloor",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "InstallAccessPoint",
                table: "BuildingFloor",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                name: "WorkSpaceReservationBundle",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageId = table.Column<long>(type: "bigint", nullable: false),
                    PackagePrice = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    PackageStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PackageEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PackageDiscount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    BasicUserId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: false),
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
                    Price = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    HourlyDiscount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    BasicUserId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: false),
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
                    TailoredPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    TailoredDiscount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    BasicUserId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PaymentMethodId = table.Column<long>(type: "bigint", nullable: false),
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
                name: "WorkSpaceHourlyTopUp",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HourId = table.Column<long>(type: "bigint", nullable: false),
                    HourlyTotalPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
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
                        name: "FK_WorkSpaceHourlyTopUp_WorkSpaceReservationHourly_WorkSpaceReservationHourlyId",
                        column: x => x.WorkSpaceReservationHourlyId,
                        principalTable: "WorkSpaceReservationHourly",
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
                    TailoredPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
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
                name: "IX_WorkSpaceHourlyTopUp_BasicUserId",
                table: "WorkSpaceHourlyTopUp",
                column: "BasicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyTopUp_PaymentMethodId",
                table: "WorkSpaceHourlyTopUp",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyTopUp_WorkSpaceReservationHourlyId",
                table: "WorkSpaceHourlyTopUp",
                column: "WorkSpaceReservationHourlyId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationBundle_BasicUserId",
                table: "WorkSpaceReservationBundle",
                column: "BasicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationBundle_LocationId",
                table: "WorkSpaceReservationBundle",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationBundle_PaymentMethodId",
                table: "WorkSpaceReservationBundle",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationHourly_BasicUserId",
                table: "WorkSpaceReservationHourly",
                column: "BasicUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationHourly_LocationId",
                table: "WorkSpaceReservationHourly",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceReservationHourly_PaymentMethodId",
                table: "WorkSpaceReservationHourly",
                column: "PaymentMethodId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CancelReservation");

            migrationBuilder.DropTable(
                name: "ReservationDetail");

            migrationBuilder.DropTable(
                name: "WorkSpaceHourlyTopUp");

            migrationBuilder.DropTable(
                name: "WorkSpaceReservationBundle");

            migrationBuilder.DropTable(
                name: "WorkSpaceTailoredTopUp");

            migrationBuilder.DropTable(
                name: "ReservationTransaction");

            migrationBuilder.DropTable(
                name: "WorkSpaceReservationHourly");

            migrationBuilder.DropTable(
                name: "WorkSpaceReservationTailored");

            migrationBuilder.DropTable(
                name: "ReservationType");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "FloorNumber",
                table: "BuildingFloor");

            migrationBuilder.DropColumn(
                name: "InstallAccessPoint",
                table: "BuildingFloor");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "BuildingFloor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
