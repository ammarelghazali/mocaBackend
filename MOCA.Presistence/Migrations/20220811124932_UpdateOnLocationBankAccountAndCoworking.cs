using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class UpdateOnLocationBankAccountAndCoworking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coworking_CoworkingWorkspaceFurnishing_CoworkingWorkspaceFurnishingLocationId_CoworkingWorkspaceFurnishingFurnishingId",
                table: "Coworking");

            migrationBuilder.DropForeignKey(
                name: "FK_Coworking_CoworkingWorkSpaceMarketingImage_CoworkingWorkSpaceMarketingImageLocationId_CoworkingWorkSpaceMarketingImageMarket~",
                table: "Coworking");

            migrationBuilder.DropForeignKey(
                name: "FK_CoworkingWorkspaceFurnishing_Location_LocationId",
                table: "CoworkingWorkspaceFurnishing");

            migrationBuilder.DropForeignKey(
                name: "FK_Furnishing_Feature_FeatureId",
                table: "Furnishing");

            migrationBuilder.DropIndex(
                name: "IX_Coworking_CoworkingWorkspaceFurnishingLocationId_CoworkingWorkspaceFurnishingFurnishingId",
                table: "Coworking");

            migrationBuilder.DropIndex(
                name: "IX_Coworking_CoworkingWorkSpaceMarketingImageLocationId_CoworkingWorkSpaceMarketingImageMarketingImagesId",
                table: "Coworking");

            migrationBuilder.DropColumn(
                name: "SpaceId",
                table: "Furnishing");

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

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "CoworkingWorkspaceFurnishing",
                newName: "CoworkingWorkSpaceId");

            migrationBuilder.AlterColumn<string>(
                name: "SharedBankAccountSwift",
                table: "LocationBankAccount",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SharedBankAccountName",
                table: "LocationBankAccount",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SharedBankAccountIBAN",
                table: "LocationBankAccount",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<long>(
                name: "FeatureId",
                table: "Furnishing",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "CoworkingWorkSpaceMarketingImageLocationId",
                table: "CoworkingWorkSpace",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CoworkingWorkSpaceMarketingImageMarketingImagesId",
                table: "CoworkingWorkSpace",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingWorkspaceFurnishing_CoworkingWorkSpaceId",
                table: "CoworkingWorkspaceFurnishing",
                column: "CoworkingWorkSpaceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingWorkSpace_CoworkingWorkSpaceMarketingImageLocationId_CoworkingWorkSpaceMarketingImageMarketingImagesId",
                table: "CoworkingWorkSpace",
                columns: new[] { "CoworkingWorkSpaceMarketingImageLocationId", "CoworkingWorkSpaceMarketingImageMarketingImagesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CoworkingWorkSpace_CoworkingWorkSpaceMarketingImage_CoworkingWorkSpaceMarketingImageLocationId_CoworkingWorkSpaceMarketingIm~",
                table: "CoworkingWorkSpace",
                columns: new[] { "CoworkingWorkSpaceMarketingImageLocationId", "CoworkingWorkSpaceMarketingImageMarketingImagesId" },
                principalTable: "CoworkingWorkSpaceMarketingImage",
                principalColumns: new[] { "LocationId", "MarketingImagesId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoworkingWorkspaceFurnishing_CoworkingWorkSpace_CoworkingWorkSpaceId",
                table: "CoworkingWorkspaceFurnishing",
                column: "CoworkingWorkSpaceId",
                principalTable: "CoworkingWorkSpace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Furnishing_Feature_FeatureId",
                table: "Furnishing",
                column: "FeatureId",
                principalTable: "Feature",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoworkingWorkSpace_CoworkingWorkSpaceMarketingImage_CoworkingWorkSpaceMarketingImageLocationId_CoworkingWorkSpaceMarketingIm~",
                table: "CoworkingWorkSpace");

            migrationBuilder.DropForeignKey(
                name: "FK_CoworkingWorkspaceFurnishing_CoworkingWorkSpace_CoworkingWorkSpaceId",
                table: "CoworkingWorkspaceFurnishing");

            migrationBuilder.DropForeignKey(
                name: "FK_Furnishing_Feature_FeatureId",
                table: "Furnishing");

            migrationBuilder.DropIndex(
                name: "IX_CoworkingWorkspaceFurnishing_CoworkingWorkSpaceId",
                table: "CoworkingWorkspaceFurnishing");

            migrationBuilder.DropIndex(
                name: "IX_CoworkingWorkSpace_CoworkingWorkSpaceMarketingImageLocationId_CoworkingWorkSpaceMarketingImageMarketingImagesId",
                table: "CoworkingWorkSpace");

            migrationBuilder.DropColumn(
                name: "CoworkingWorkSpaceMarketingImageLocationId",
                table: "CoworkingWorkSpace");

            migrationBuilder.DropColumn(
                name: "CoworkingWorkSpaceMarketingImageMarketingImagesId",
                table: "CoworkingWorkSpace");

            migrationBuilder.RenameColumn(
                name: "CoworkingWorkSpaceId",
                table: "CoworkingWorkspaceFurnishing",
                newName: "LocationId");

            migrationBuilder.AlterColumn<string>(
                name: "SharedBankAccountSwift",
                table: "LocationBankAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SharedBankAccountName",
                table: "LocationBankAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SharedBankAccountIBAN",
                table: "LocationBankAccount",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "FeatureId",
                table: "Furnishing",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SpaceId",
                table: "Furnishing",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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

            migrationBuilder.CreateIndex(
                name: "IX_Coworking_CoworkingWorkspaceFurnishingLocationId_CoworkingWorkspaceFurnishingFurnishingId",
                table: "Coworking",
                columns: new[] { "CoworkingWorkspaceFurnishingLocationId", "CoworkingWorkspaceFurnishingFurnishingId" });

            migrationBuilder.CreateIndex(
                name: "IX_Coworking_CoworkingWorkSpaceMarketingImageLocationId_CoworkingWorkSpaceMarketingImageMarketingImagesId",
                table: "Coworking",
                columns: new[] { "CoworkingWorkSpaceMarketingImageLocationId", "CoworkingWorkSpaceMarketingImageMarketingImagesId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_CoworkingWorkspaceFurnishing_Location_LocationId",
                table: "CoworkingWorkspaceFurnishing",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Furnishing_Feature_FeatureId",
                table: "Furnishing",
                column: "FeatureId",
                principalTable: "Feature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
