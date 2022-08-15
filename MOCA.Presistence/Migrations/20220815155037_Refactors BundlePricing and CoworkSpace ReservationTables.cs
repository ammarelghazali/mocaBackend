using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class RefactorsBundlePricingandCoworkSpaceReservationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoworkingSpaceReservationBundle_Coworking_CoworkingId",
                table: "CoworkingSpaceReservationBundle");

            migrationBuilder.DropForeignKey(
                name: "FK_CoworkingSpaceReservationBundle_CoworkingWorkSpace_CoworkSpaceId",
                table: "CoworkingSpaceReservationBundle");

            migrationBuilder.DropForeignKey(
                name: "FK_CoworkingSpaceReservationHourly_Coworking_CoworkingId",
                table: "CoworkingSpaceReservationHourly");

            migrationBuilder.DropForeignKey(
                name: "FK_CoworkingSpaceReservationHourly_CoworkingWorkSpace_CoworkSpaceId",
                table: "CoworkingSpaceReservationHourly");

            migrationBuilder.DropForeignKey(
                name: "FK_CoworkingSpaceReservationTailored_Coworking_CoworkingId",
                table: "CoworkingSpaceReservationTailored");

            migrationBuilder.DropForeignKey(
                name: "FK_CoworkingSpaceReservationTailored_CoworkingWorkSpace_CoworkSpaceId",
                table: "CoworkingSpaceReservationTailored");

            migrationBuilder.DropIndex(
                name: "IX_CoworkingSpaceReservationTailored_CoworkSpaceId",
                table: "CoworkingSpaceReservationTailored");

            migrationBuilder.DropIndex(
                name: "IX_CoworkingSpaceReservationHourly_CoworkSpaceId",
                table: "CoworkingSpaceReservationHourly");

            migrationBuilder.DropIndex(
                name: "IX_CoworkingSpaceReservationBundle_CoworkSpaceId",
                table: "CoworkingSpaceReservationBundle");

            migrationBuilder.DropColumn(
                name: "CoworkSpaceId",
                table: "CoworkingSpaceReservationTailored");

            migrationBuilder.DropColumn(
                name: "CoworkSpaceId",
                table: "CoworkingSpaceReservationHourly");

            migrationBuilder.DropColumn(
                name: "CoworkSpaceId",
                table: "CoworkingSpaceReservationBundle");

            migrationBuilder.AlterColumn<long>(
                name: "CoworkingId",
                table: "CoworkingSpaceReservationTailored",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CoworkingId",
                table: "CoworkingSpaceReservationHourly",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CoworkingId",
                table: "CoworkingSpaceReservationBundle",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CoworkingSpaceReservationBundle_Coworking_CoworkingId",
                table: "CoworkingSpaceReservationBundle",
                column: "CoworkingId",
                principalTable: "Coworking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoworkingSpaceReservationHourly_Coworking_CoworkingId",
                table: "CoworkingSpaceReservationHourly",
                column: "CoworkingId",
                principalTable: "Coworking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoworkingSpaceReservationTailored_Coworking_CoworkingId",
                table: "CoworkingSpaceReservationTailored",
                column: "CoworkingId",
                principalTable: "Coworking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoworkingSpaceReservationBundle_Coworking_CoworkingId",
                table: "CoworkingSpaceReservationBundle");

            migrationBuilder.DropForeignKey(
                name: "FK_CoworkingSpaceReservationHourly_Coworking_CoworkingId",
                table: "CoworkingSpaceReservationHourly");

            migrationBuilder.DropForeignKey(
                name: "FK_CoworkingSpaceReservationTailored_Coworking_CoworkingId",
                table: "CoworkingSpaceReservationTailored");

            migrationBuilder.AlterColumn<long>(
                name: "CoworkingId",
                table: "CoworkingSpaceReservationTailored",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "CoworkSpaceId",
                table: "CoworkingSpaceReservationTailored",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "CoworkingId",
                table: "CoworkingSpaceReservationHourly",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "CoworkSpaceId",
                table: "CoworkingSpaceReservationHourly",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "CoworkingId",
                table: "CoworkingSpaceReservationBundle",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "CoworkSpaceId",
                table: "CoworkingSpaceReservationBundle",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationTailored_CoworkSpaceId",
                table: "CoworkingSpaceReservationTailored",
                column: "CoworkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationHourly_CoworkSpaceId",
                table: "CoworkingSpaceReservationHourly",
                column: "CoworkSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingSpaceReservationBundle_CoworkSpaceId",
                table: "CoworkingSpaceReservationBundle",
                column: "CoworkSpaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoworkingSpaceReservationBundle_Coworking_CoworkingId",
                table: "CoworkingSpaceReservationBundle",
                column: "CoworkingId",
                principalTable: "Coworking",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoworkingSpaceReservationBundle_CoworkingWorkSpace_CoworkSpaceId",
                table: "CoworkingSpaceReservationBundle",
                column: "CoworkSpaceId",
                principalTable: "CoworkingWorkSpace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoworkingSpaceReservationHourly_Coworking_CoworkingId",
                table: "CoworkingSpaceReservationHourly",
                column: "CoworkingId",
                principalTable: "Coworking",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoworkingSpaceReservationHourly_CoworkingWorkSpace_CoworkSpaceId",
                table: "CoworkingSpaceReservationHourly",
                column: "CoworkSpaceId",
                principalTable: "CoworkingWorkSpace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoworkingSpaceReservationTailored_Coworking_CoworkingId",
                table: "CoworkingSpaceReservationTailored",
                column: "CoworkingId",
                principalTable: "Coworking",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoworkingSpaceReservationTailored_CoworkingWorkSpace_CoworkSpaceId",
                table: "CoworkingSpaceReservationTailored",
                column: "CoworkSpaceId",
                principalTable: "CoworkingWorkSpace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
