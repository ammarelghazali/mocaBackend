using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class RemovesBookEventSpace_IDFromEventSpaceTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookEventSpace_ID",
                table: "EventSpaceTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BookEventSpace_ID",
                table: "EventSpaceTime",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
