using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class UpdateLocationTableEditedCountryCodeAndUtilizationPercentage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Location");

            migrationBuilder.RenameColumn(
                name: "Percentage",
                table: "Location",
                newName: "UtilizationPercentage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UtilizationPercentage",
                table: "Location",
                newName: "Percentage");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Location",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
