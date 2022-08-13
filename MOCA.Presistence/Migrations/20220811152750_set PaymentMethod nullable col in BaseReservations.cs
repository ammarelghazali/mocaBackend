using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class setPaymentMethodnullablecolinBaseReservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoworkingSpaceReservationBundle_PaymentMethod_PaymentMethodId",
                table: "CoworkingSpaceReservationBundle");

            migrationBuilder.DropForeignKey(
                name: "FK_CoworkingSpaceReservationHourly_PaymentMethod_PaymentMethodId",
                table: "CoworkingSpaceReservationHourly");

            migrationBuilder.DropForeignKey(
                name: "FK_CoworkingSpaceReservationTailored_PaymentMethod_PaymentMethodId",
                table: "CoworkingSpaceReservationTailored");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingReservation_PaymentMethod_PaymentMethodId",
                table: "MeetingReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpaceReservationBundle_PaymentMethod_PaymentMethodId",
                table: "WorkSpaceReservationBundle");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpaceReservationHourly_PaymentMethod_PaymentMethodId",
                table: "WorkSpaceReservationHourly");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpaceReservationTailored_PaymentMethod_PaymentMethodId",
                table: "WorkSpaceReservationTailored");

            migrationBuilder.AlterColumn<long>(
                name: "PaymentMethodId",
                table: "WorkSpaceReservationTailored",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PaymentMethodId",
                table: "WorkSpaceReservationHourly",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PaymentMethodId",
                table: "WorkSpaceReservationBundle",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PaymentMethodId",
                table: "MeetingReservation",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PaymentMethodId",
                table: "CoworkingSpaceReservationTailored",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PaymentMethodId",
                table: "CoworkingSpaceReservationHourly",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PaymentMethodId",
                table: "CoworkingSpaceReservationBundle",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_CoworkingSpaceReservationBundle_PaymentMethod_PaymentMethodId",
                table: "CoworkingSpaceReservationBundle",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoworkingSpaceReservationHourly_PaymentMethod_PaymentMethodId",
                table: "CoworkingSpaceReservationHourly",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoworkingSpaceReservationTailored_PaymentMethod_PaymentMethodId",
                table: "CoworkingSpaceReservationTailored",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingReservation_PaymentMethod_PaymentMethodId",
                table: "MeetingReservation",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpaceReservationBundle_PaymentMethod_PaymentMethodId",
                table: "WorkSpaceReservationBundle",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpaceReservationHourly_PaymentMethod_PaymentMethodId",
                table: "WorkSpaceReservationHourly",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpaceReservationTailored_PaymentMethod_PaymentMethodId",
                table: "WorkSpaceReservationTailored",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoworkingSpaceReservationBundle_PaymentMethod_PaymentMethodId",
                table: "CoworkingSpaceReservationBundle");

            migrationBuilder.DropForeignKey(
                name: "FK_CoworkingSpaceReservationHourly_PaymentMethod_PaymentMethodId",
                table: "CoworkingSpaceReservationHourly");

            migrationBuilder.DropForeignKey(
                name: "FK_CoworkingSpaceReservationTailored_PaymentMethod_PaymentMethodId",
                table: "CoworkingSpaceReservationTailored");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingReservation_PaymentMethod_PaymentMethodId",
                table: "MeetingReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpaceReservationBundle_PaymentMethod_PaymentMethodId",
                table: "WorkSpaceReservationBundle");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpaceReservationHourly_PaymentMethod_PaymentMethodId",
                table: "WorkSpaceReservationHourly");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSpaceReservationTailored_PaymentMethod_PaymentMethodId",
                table: "WorkSpaceReservationTailored");

            migrationBuilder.AlterColumn<long>(
                name: "PaymentMethodId",
                table: "WorkSpaceReservationTailored",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PaymentMethodId",
                table: "WorkSpaceReservationHourly",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PaymentMethodId",
                table: "WorkSpaceReservationBundle",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PaymentMethodId",
                table: "MeetingReservation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PaymentMethodId",
                table: "CoworkingSpaceReservationTailored",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PaymentMethodId",
                table: "CoworkingSpaceReservationHourly",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PaymentMethodId",
                table: "CoworkingSpaceReservationBundle",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CoworkingSpaceReservationBundle_PaymentMethod_PaymentMethodId",
                table: "CoworkingSpaceReservationBundle",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoworkingSpaceReservationHourly_PaymentMethod_PaymentMethodId",
                table: "CoworkingSpaceReservationHourly",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoworkingSpaceReservationTailored_PaymentMethod_PaymentMethodId",
                table: "CoworkingSpaceReservationTailored",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingReservation_PaymentMethod_PaymentMethodId",
                table: "MeetingReservation",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpaceReservationBundle_PaymentMethod_PaymentMethodId",
                table: "WorkSpaceReservationBundle",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpaceReservationHourly_PaymentMethod_PaymentMethodId",
                table: "WorkSpaceReservationHourly",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSpaceReservationTailored_PaymentMethod_PaymentMethodId",
                table: "WorkSpaceReservationTailored",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
