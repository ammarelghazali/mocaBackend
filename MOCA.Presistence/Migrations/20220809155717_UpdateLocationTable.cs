using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class UpdateLocationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommercialName",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "EstimatedRamp",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "EventspaceContract",
                table: "Location");

            migrationBuilder.RenameColumn(
                name: "VenuesBrochureURL",
                table: "Location",
                newName: "LandlordCommercialName");

            migrationBuilder.RenameColumn(
                name: "UtilizationPeriod",
                table: "Location",
                newName: "Percentage");

            migrationBuilder.RenameColumn(
                name: "Url360Tour",
                table: "Location",
                newName: "EventspaceLeaseContract");

            migrationBuilder.RenameColumn(
                name: "UploadContract",
                table: "Location",
                newName: "CountryCode");

            migrationBuilder.RenameColumn(
                name: "ServiceFeesPricePerMeter",
                table: "Location",
                newName: "EstimatedRampUpAmount");

            migrationBuilder.AddColumn<long>(
                name: "CityId",
                table: "Location",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CountryId",
                table: "Location",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservationCancellation_MeetingReservationId",
                table: "MeetingReservationCancellation",
                column: "MeetingReservationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_CityId",
                table: "Location",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_CountryId",
                table: "Location",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_City_CityId",
                table: "Location",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Country_CountryId",
                table: "Location",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_City_CityId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Country_CountryId",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_MeetingReservationCancellation_MeetingReservationId",
                table: "MeetingReservationCancellation");

            migrationBuilder.DropIndex(
                name: "IX_Location_CityId",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Location_CountryId",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Location");

            migrationBuilder.RenameColumn(
                name: "Percentage",
                table: "Location",
                newName: "UtilizationPeriod");

            migrationBuilder.RenameColumn(
                name: "LandlordCommercialName",
                table: "Location",
                newName: "VenuesBrochureURL");

            migrationBuilder.RenameColumn(
                name: "EventspaceLeaseContract",
                table: "Location",
                newName: "Url360Tour");

            migrationBuilder.RenameColumn(
                name: "EstimatedRampUpAmount",
                table: "Location",
                newName: "ServiceFeesPricePerMeter");

            migrationBuilder.RenameColumn(
                name: "CountryCode",
                table: "Location",
                newName: "UploadContract");

            migrationBuilder.AddColumn<string>(
                name: "CommercialName",
                table: "Location",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedRamp",
                table: "Location",
                type: "decimal(18,3)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventspaceContract",
                table: "Location",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
