using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class UpdateBuildingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FemaleRestroomCount",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "MaleRestroomCount",
                table: "Building");

            migrationBuilder.AddColumn<bool>(
                name: "InstallAccessPoint",
                table: "Building",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstallAccessPoint",
                table: "Building");

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
        }
    }
}
