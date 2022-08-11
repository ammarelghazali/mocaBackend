using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class latestUpdateOnTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LocationId",
                table: "WorkSpace",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "WorkSpaceCategoryId",
                table: "WorkSpace",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "LocationId",
                table: "MeetingSpace",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "FullRampUpRevenue",
                table: "Location",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LandlordAdditionalRevenue",
                table: "Location",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerMeter",
                table: "Location",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LocationId",
                table: "EventSpace",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpace_LocationId",
                table: "WorkSpace",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpace_WorkSpaceCategoryId",
                table: "WorkSpace",
                column: "WorkSpaceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingSpace_LocationId",
                table: "MeetingSpace",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSpace_LocationId",
                table: "EventSpace",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Coworking_LocationId",
                table: "Coworking",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceBundleMemberType_MemberTypeId",
                table: "CoworkingSpaceBundleMemberType",
                column: "MemberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceBundlePricing_CoworkingId",
                table: "CoworkingSpaceBundlePricing",
                column: "CoworkingId");

            migrationBuilder.CreateIndex(
                name: "IX_CoWorkingSpaceHourlyPricing_CoworkingId",
                table: "CoWorkingSpaceHourlyPricing",
                column: "CoworkingId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceTailoredPricing_CoworkingId",
                table: "CoworkingSpaceTailoredPricing",
                column: "CoworkingId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceBundleMemberType_MemberTypeId",
                table: "WorkSpaceBundleMemberType",
                column: "MemberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceBundlePricing_WorkSpaceId",
                table: "WorkSpaceBundlePricing",
                column: "WorkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceHourlyPricing_WorkSpaceId",
                table: "WorkSpaceHourlyPricing",
                column: "WorkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSpaceTailoredPricing_WorkSpaceId",
                table: "WorkSpaceTailoredPricing",
                column: "WorkSpaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpace_Location_LocationId",
                table: "EventSpace",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingSpace_Location_LocationId",
                table: "MeetingSpace",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpace_Location_LocationId",
                table: "WorkSpace",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpace_WorkSpaceCategory_WorkSpaceCategoryId",
                table: "WorkSpace",
                column: "WorkSpaceCategoryId",
                principalTable: "WorkSpaceCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSpace_Location_LocationId",
                table: "EventSpace");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingSpace_Location_LocationId",
                table: "MeetingSpace");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpace_Location_LocationId",
                table: "WorkSpace");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpace_WorkSpaceCategory_WorkSpaceCategoryId",
                table: "WorkSpace");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceBundleMemberType");

            migrationBuilder.DropTable(
                name: "CoWorkingSpaceHourlyPricing");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceTailoredPricing");

            migrationBuilder.DropTable(
                name: "WorkSpaceBundleMemberType");

            migrationBuilder.DropTable(
                name: "WorkSpaceHourlyPricing");

            migrationBuilder.DropTable(
                name: "WorkSpaceTailoredPricing");

            migrationBuilder.DropTable(
                name: "CoworkingSpaceBundlePricing");

            migrationBuilder.DropTable(
                name: "WorkSpaceBundlePricing");

            migrationBuilder.DropTable(
                name: "Coworking");

            migrationBuilder.DropIndex(
                name: "IX_WorkSpace_LocationId",
                table: "WorkSpace");

            migrationBuilder.DropIndex(
                name: "IX_WorkSpace_WorkSpaceCategoryId",
                table: "WorkSpace");

            migrationBuilder.DropIndex(
                name: "IX_MeetingSpace_LocationId",
                table: "MeetingSpace");

            migrationBuilder.DropIndex(
                name: "IX_EventSpace_LocationId",
                table: "EventSpace");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "WorkSpace");

            migrationBuilder.DropColumn(
                name: "WorkSpaceCategoryId",
                table: "WorkSpace");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "MeetingSpace");

            migrationBuilder.DropColumn(
                name: "FullRampUpRevenue",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "LandlordAdditionalRevenue",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "PricePerMeter",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "EventSpace");
        }
    }
}
