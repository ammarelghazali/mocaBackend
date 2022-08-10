using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class deleteCountryCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "LocationContact");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "LocationContact",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
