using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class MakeMeetingReservationTransactioninheritfromBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingAttendee_MeetingReservation_MeetingSpaceReservationId",
                table: "MeetingAttendee");

            migrationBuilder.RenameColumn(
                name: "MeetingSpaceReservationId",
                table: "MeetingAttendee",
                newName: "MeetingReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingAttendee_MeetingSpaceReservationId",
                table: "MeetingAttendee",
                newName: "IX_MeetingAttendee_MeetingReservationId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MeetingReservationTransaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MeetingReservationTransaction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "MeetingReservationTransaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MeetingReservationTransaction",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "MeetingReservationTransaction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "MeetingReservationTransaction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingAttendee_MeetingReservation_MeetingReservationId",
                table: "MeetingAttendee",
                column: "MeetingReservationId",
                principalTable: "MeetingReservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingAttendee_MeetingReservation_MeetingReservationId",
                table: "MeetingAttendee");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MeetingReservationTransaction");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MeetingReservationTransaction");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MeetingReservationTransaction");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MeetingReservationTransaction");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "MeetingReservationTransaction");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "MeetingReservationTransaction");

            migrationBuilder.RenameColumn(
                name: "MeetingReservationId",
                table: "MeetingAttendee",
                newName: "MeetingSpaceReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingAttendee_MeetingReservationId",
                table: "MeetingAttendee",
                newName: "IX_MeetingAttendee_MeetingSpaceReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingAttendee_MeetingReservation_MeetingSpaceReservationId",
                table: "MeetingAttendee",
                column: "MeetingSpaceReservationId",
                principalTable: "MeetingReservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
