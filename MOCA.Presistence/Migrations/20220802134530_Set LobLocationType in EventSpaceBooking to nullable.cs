using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class SetLobLocationTypeinEventSpaceBookingtonullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSpaceBooking_LocationType_LobLocationTypeId",
                table: "EventSpaceBooking");

            migrationBuilder.AlterColumn<long>(
                name: "LobLocationTypeId",
                table: "EventSpaceBooking",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpaceBooking_LocationType_LobLocationTypeId",
                table: "EventSpaceBooking",
                column: "LobLocationTypeId",
                principalTable: "LocationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSpaceBooking_LocationType_LobLocationTypeId",
                table: "EventSpaceBooking");

            migrationBuilder.AlterColumn<long>(
                name: "LobLocationTypeId",
                table: "EventSpaceBooking",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpaceBooking_LocationType_LobLocationTypeId",
                table: "EventSpaceBooking",
                column: "LobLocationTypeId",
                principalTable: "LocationType",
                principalColumn: "Id");
        }
    }
}
