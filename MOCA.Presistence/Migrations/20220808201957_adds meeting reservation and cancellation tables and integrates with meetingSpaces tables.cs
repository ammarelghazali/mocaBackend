using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class addsmeetingreservationandcancellationtablesandintegrateswithmeetingSpacestables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingReservationTopUp_MeetingReservation_MeetingSpaceReservationId",
                table: "MeetingReservationTopUp");

            migrationBuilder.DropColumn(
                name: "MeetingroomId",
                table: "MeetingReservation");

            migrationBuilder.RenameColumn(
                name: "MeetingSpaceReservationId",
                table: "MeetingReservationTopUp",
                newName: "MeetingSpaceHourlyPricingId");

            migrationBuilder.RenameColumn(
                name: "MeetingRoomTimePriceId",
                table: "MeetingReservationTopUp",
                newName: "MeetingReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingReservationTopUp_MeetingSpaceReservationId",
                table: "MeetingReservationTopUp",
                newName: "IX_MeetingReservationTopUp_MeetingSpaceHourlyPricingId");

            migrationBuilder.RenameColumn(
                name: "MeetingRoomTimePriceId",
                table: "MeetingReservation",
                newName: "MeetingSpaceId");

            migrationBuilder.AlterColumn<long>(
                name: "ReservationTransactionId",
                table: "MeetingReservationTransaction",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<long>(
                name: "MeetingReservationId",
                table: "MeetingReservationTransaction",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<long>(
                name: "MeetingSpaceHourlyPricingId",
                table: "MeetingReservation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "MeetingReservationCancellation",
                columns: table => new
                {
                    MeetingReservationId = table.Column<long>(type: "bigint", nullable: false),
                    CancellationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingReservationCancellation", x => new { x.MeetingReservationId, x.CancellationId });
                    table.ForeignKey(
                        name: "FK_MeetingReservationCancellation_CancelReservation_CancellationId",
                        column: x => x.CancellationId,
                        principalTable: "CancelReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeetingReservationCancellation_MeetingReservation_MeetingReservationId",
                        column: x => x.MeetingReservationId,
                        principalTable: "MeetingReservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservationTopUp_MeetingReservationId",
                table: "MeetingReservationTopUp",
                column: "MeetingReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservation_MeetingSpaceHourlyPricingId",
                table: "MeetingReservation",
                column: "MeetingSpaceHourlyPricingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservation_MeetingSpaceId",
                table: "MeetingReservation",
                column: "MeetingSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingReservationCancellation_CancellationId",
                table: "MeetingReservationCancellation",
                column: "CancellationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingReservation_MeetingSpace_MeetingSpaceId",
                table: "MeetingReservation",
                column: "MeetingSpaceId",
                principalTable: "MeetingSpace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingReservation_MeetingSpaceHourlyPricing_MeetingSpaceHourlyPricingId",
                table: "MeetingReservation",
                column: "MeetingSpaceHourlyPricingId",
                principalTable: "MeetingSpaceHourlyPricing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingReservationTopUp_MeetingReservation_MeetingReservationId",
                table: "MeetingReservationTopUp",
                column: "MeetingReservationId",
                principalTable: "MeetingReservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingReservationTopUp_MeetingSpaceHourlyPricing_MeetingSpaceHourlyPricingId",
                table: "MeetingReservationTopUp",
                column: "MeetingSpaceHourlyPricingId",
                principalTable: "MeetingSpaceHourlyPricing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingReservation_MeetingSpace_MeetingSpaceId",
                table: "MeetingReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingReservation_MeetingSpaceHourlyPricing_MeetingSpaceHourlyPricingId",
                table: "MeetingReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingReservationTopUp_MeetingReservation_MeetingReservationId",
                table: "MeetingReservationTopUp");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingReservationTopUp_MeetingSpaceHourlyPricing_MeetingSpaceHourlyPricingId",
                table: "MeetingReservationTopUp");

            migrationBuilder.DropTable(
                name: "MeetingReservationCancellation");

            migrationBuilder.DropIndex(
                name: "IX_MeetingReservationTopUp_MeetingReservationId",
                table: "MeetingReservationTopUp");

            migrationBuilder.DropIndex(
                name: "IX_MeetingReservation_MeetingSpaceHourlyPricingId",
                table: "MeetingReservation");

            migrationBuilder.DropIndex(
                name: "IX_MeetingReservation_MeetingSpaceId",
                table: "MeetingReservation");

            migrationBuilder.DropColumn(
                name: "MeetingSpaceHourlyPricingId",
                table: "MeetingReservation");

            migrationBuilder.RenameColumn(
                name: "MeetingSpaceHourlyPricingId",
                table: "MeetingReservationTopUp",
                newName: "MeetingSpaceReservationId");

            migrationBuilder.RenameColumn(
                name: "MeetingReservationId",
                table: "MeetingReservationTopUp",
                newName: "MeetingRoomTimePriceId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingReservationTopUp_MeetingSpaceHourlyPricingId",
                table: "MeetingReservationTopUp",
                newName: "IX_MeetingReservationTopUp_MeetingSpaceReservationId");

            migrationBuilder.RenameColumn(
                name: "MeetingSpaceId",
                table: "MeetingReservation",
                newName: "MeetingRoomTimePriceId");

            migrationBuilder.AlterColumn<long>(
                name: "ReservationTransactionId",
                table: "MeetingReservationTransaction",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<long>(
                name: "MeetingReservationId",
                table: "MeetingReservationTransaction",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<long>(
                name: "MeetingroomId",
                table: "MeetingReservation",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingReservationTopUp_MeetingReservation_MeetingSpaceReservationId",
                table: "MeetingReservationTopUp",
                column: "MeetingSpaceReservationId",
                principalTable: "MeetingReservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
