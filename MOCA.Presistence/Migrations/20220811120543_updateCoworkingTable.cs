using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class updateCoworkingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CoworkingWorkSpaceMarketingImageLocationId",
                table: "Coworking",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CoworkingWorkSpaceMarketingImageMarketingImagesId",
                table: "Coworking",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CoworkingWorkspaceFurnishingFurnishingId",
                table: "Coworking",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CoworkingWorkspaceFurnishingLocationId",
                table: "Coworking",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "CoworkingWorkspaceFurnishing",
                columns: table => new
                {
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    FurnishingId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingWorkspaceFurnishing", x => new { x.LocationId, x.FurnishingId });
                    table.ForeignKey(
                        name: "FK_CoworkingWorkspaceFurnishing_Furnishing_FurnishingId",
                        column: x => x.FurnishingId,
                        principalTable: "Furnishing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingWorkspaceFurnishing_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoworkingWorkSpaceMarketingImage",
                columns: table => new
                {
                    LocationId = table.Column<long>(type: "bigint", nullable: false),
                    MarketingImagesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingWorkSpaceMarketingImage", x => new { x.LocationId, x.MarketingImagesId });
                    table.ForeignKey(
                        name: "FK_CoworkingWorkSpaceMarketingImage_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoworkingWorkSpaceMarketingImage_MarketingImages_MarketingImagesId",
                        column: x => x.MarketingImagesId,
                        principalTable: "MarketingImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coworking_CoworkingWorkspaceFurnishingLocationId_CoworkingWorkspaceFurnishingFurnishingId",
                table: "Coworking",
                columns: new[] { "CoworkingWorkspaceFurnishingLocationId", "CoworkingWorkspaceFurnishingFurnishingId" });

            migrationBuilder.CreateIndex(
                name: "IX_Coworking_CoworkingWorkSpaceMarketingImageLocationId_CoworkingWorkSpaceMarketingImageMarketingImagesId",
                table: "Coworking",
                columns: new[] { "CoworkingWorkSpaceMarketingImageLocationId", "CoworkingWorkSpaceMarketingImageMarketingImagesId" });

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingWorkspaceFurnishing_FurnishingId",
                table: "CoworkingWorkspaceFurnishing",
                column: "FurnishingId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingWorkSpaceMarketingImage_MarketingImagesId",
                table: "CoworkingWorkSpaceMarketingImage",
                column: "MarketingImagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coworking_CoworkingWorkspaceFurnishing_CoworkingWorkspaceFurnishingLocationId_CoworkingWorkspaceFurnishingFurnishingId",
                table: "Coworking",
                columns: new[] { "CoworkingWorkspaceFurnishingLocationId", "CoworkingWorkspaceFurnishingFurnishingId" },
                principalTable: "CoworkingWorkspaceFurnishing",
                principalColumns: new[] { "LocationId", "FurnishingId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Coworking_CoworkingWorkSpaceMarketingImage_CoworkingWorkSpaceMarketingImageLocationId_CoworkingWorkSpaceMarketingImageMarket~",
                table: "Coworking",
                columns: new[] { "CoworkingWorkSpaceMarketingImageLocationId", "CoworkingWorkSpaceMarketingImageMarketingImagesId" },
                principalTable: "CoworkingWorkSpaceMarketingImage",
                principalColumns: new[] { "LocationId", "MarketingImagesId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coworking_CoworkingWorkspaceFurnishing_CoworkingWorkspaceFurnishingLocationId_CoworkingWorkspaceFurnishingFurnishingId",
                table: "Coworking");

            migrationBuilder.DropForeignKey(
                name: "FK_Coworking_CoworkingWorkSpaceMarketingImage_CoworkingWorkSpaceMarketingImageLocationId_CoworkingWorkSpaceMarketingImageMarket~",
                table: "Coworking");

            migrationBuilder.DropTable(
                name: "CoworkingWorkspaceFurnishing");

            migrationBuilder.DropTable(
                name: "CoworkingWorkSpaceMarketingImage");

            migrationBuilder.DropIndex(
                name: "IX_Coworking_CoworkingWorkspaceFurnishingLocationId_CoworkingWorkspaceFurnishingFurnishingId",
                table: "Coworking");

            migrationBuilder.DropIndex(
                name: "IX_Coworking_CoworkingWorkSpaceMarketingImageLocationId_CoworkingWorkSpaceMarketingImageMarketingImagesId",
                table: "Coworking");

            migrationBuilder.DropColumn(
                name: "CoworkingWorkSpaceMarketingImageLocationId",
                table: "Coworking");

            migrationBuilder.DropColumn(
                name: "CoworkingWorkSpaceMarketingImageMarketingImagesId",
                table: "Coworking");

            migrationBuilder.DropColumn(
                name: "CoworkingWorkspaceFurnishingFurnishingId",
                table: "Coworking");

            migrationBuilder.DropColumn(
                name: "CoworkingWorkspaceFurnishingLocationId",
                table: "Coworking");
        }
    }
}
