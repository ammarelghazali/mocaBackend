using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class RemovesUnusedColumnonIssueReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "IssueReport");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "IssueReport",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
