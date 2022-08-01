using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class SetIntiatedIDnotnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSpaceBooking_Initiated_InitiatedId",
                table: "EventSpaceBooking");

            migrationBuilder.AlterColumn<long>(
                name: "InitiatedId",
                table: "EventSpaceBooking",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpaceBooking_Initiated_InitiatedId",
                table: "EventSpaceBooking",
                column: "InitiatedId",
                principalTable: "Initiated",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSpaceBooking_Initiated_InitiatedId",
                table: "EventSpaceBooking");

            migrationBuilder.AlterColumn<long>(
                name: "InitiatedId",
                table: "EventSpaceBooking",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpaceBooking_Initiated_InitiatedId",
                table: "EventSpaceBooking",
                column: "InitiatedId",
                principalTable: "Initiated",
                principalColumn: "Id");
        }
    }
}
