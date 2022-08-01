using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class AddsEmailTemplatetableandnullablerelationsinEventSpaceBookingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSpaceBooking_EventAttendance_EventAttendanceId",
                table: "EventSpaceBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpaceBooking_EventCategory_EventCategoryId",
                table: "EventSpaceBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpaceBooking_EventOpportunityStatus_EventOpportunityStatusId",
                table: "EventSpaceBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpaceBooking_EventReccurance_EventReccuranceId",
                table: "EventSpaceBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpaceBooking_EventType_EventTypeId",
                table: "EventSpaceBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpaceBooking_Initiated_InitiatedId",
                table: "EventSpaceBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpaceBooking_OpportunityStage_OpportunityStageId",
                table: "EventSpaceBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_SendEmail_EventSpaceBooking_EventSpaceBookingId",
                table: "SendEmail");

            migrationBuilder.AlterColumn<long>(
                name: "EventSpaceBookingId",
                table: "SendEmail",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "EmailTemplateId",
                table: "SendEmail",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "OpportunityStageId",
                table: "EventSpaceBooking",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "InitiatedId",
                table: "EventSpaceBooking",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "EventTypeId",
                table: "EventSpaceBooking",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "EventReccuranceId",
                table: "EventSpaceBooking",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "EventOpportunityStatusId",
                table: "EventSpaceBooking",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "EventCategoryId",
                table: "EventSpaceBooking",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "EventAttendanceId",
                table: "EventSpaceBooking",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailTemplateTypeID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SendEmail_EmailTemplateId",
                table: "SendEmail",
                column: "EmailTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpaceBooking_EventAttendance_EventAttendanceId",
                table: "EventSpaceBooking",
                column: "EventAttendanceId",
                principalTable: "EventAttendance",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpaceBooking_EventCategory_EventCategoryId",
                table: "EventSpaceBooking",
                column: "EventCategoryId",
                principalTable: "EventCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpaceBooking_EventOpportunityStatus_EventOpportunityStatusId",
                table: "EventSpaceBooking",
                column: "EventOpportunityStatusId",
                principalTable: "EventOpportunityStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpaceBooking_EventReccurance_EventReccuranceId",
                table: "EventSpaceBooking",
                column: "EventReccuranceId",
                principalTable: "EventReccurance",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpaceBooking_EventType_EventTypeId",
                table: "EventSpaceBooking",
                column: "EventTypeId",
                principalTable: "EventType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpaceBooking_Initiated_InitiatedId",
                table: "EventSpaceBooking",
                column: "InitiatedId",
                principalTable: "Initiated",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpaceBooking_OpportunityStage_OpportunityStageId",
                table: "EventSpaceBooking",
                column: "OpportunityStageId",
                principalTable: "OpportunityStage",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SendEmail_EmailTemplate_EmailTemplateId",
                table: "SendEmail",
                column: "EmailTemplateId",
                principalTable: "EmailTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SendEmail_EventSpaceBooking_EventSpaceBookingId",
                table: "SendEmail",
                column: "EventSpaceBookingId",
                principalTable: "EventSpaceBooking",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventSpaceBooking_EventAttendance_EventAttendanceId",
                table: "EventSpaceBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpaceBooking_EventCategory_EventCategoryId",
                table: "EventSpaceBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpaceBooking_EventOpportunityStatus_EventOpportunityStatusId",
                table: "EventSpaceBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpaceBooking_EventReccurance_EventReccuranceId",
                table: "EventSpaceBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpaceBooking_EventType_EventTypeId",
                table: "EventSpaceBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpaceBooking_Initiated_InitiatedId",
                table: "EventSpaceBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_EventSpaceBooking_OpportunityStage_OpportunityStageId",
                table: "EventSpaceBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_SendEmail_EmailTemplate_EmailTemplateId",
                table: "SendEmail");

            migrationBuilder.DropForeignKey(
                name: "FK_SendEmail_EventSpaceBooking_EventSpaceBookingId",
                table: "SendEmail");

            migrationBuilder.DropTable(
                name: "EmailTemplate");

            migrationBuilder.DropIndex(
                name: "IX_SendEmail_EmailTemplateId",
                table: "SendEmail");

            migrationBuilder.AlterColumn<long>(
                name: "EventSpaceBookingId",
                table: "SendEmail",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "EmailTemplateId",
                table: "SendEmail",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "OpportunityStageId",
                table: "EventSpaceBooking",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "InitiatedId",
                table: "EventSpaceBooking",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "EventTypeId",
                table: "EventSpaceBooking",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "EventReccuranceId",
                table: "EventSpaceBooking",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "EventOpportunityStatusId",
                table: "EventSpaceBooking",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "EventCategoryId",
                table: "EventSpaceBooking",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "EventAttendanceId",
                table: "EventSpaceBooking",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpaceBooking_EventAttendance_EventAttendanceId",
                table: "EventSpaceBooking",
                column: "EventAttendanceId",
                principalTable: "EventAttendance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpaceBooking_EventCategory_EventCategoryId",
                table: "EventSpaceBooking",
                column: "EventCategoryId",
                principalTable: "EventCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpaceBooking_EventOpportunityStatus_EventOpportunityStatusId",
                table: "EventSpaceBooking",
                column: "EventOpportunityStatusId",
                principalTable: "EventOpportunityStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpaceBooking_EventReccurance_EventReccuranceId",
                table: "EventSpaceBooking",
                column: "EventReccuranceId",
                principalTable: "EventReccurance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpaceBooking_EventType_EventTypeId",
                table: "EventSpaceBooking",
                column: "EventTypeId",
                principalTable: "EventType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpaceBooking_Initiated_InitiatedId",
                table: "EventSpaceBooking",
                column: "InitiatedId",
                principalTable: "Initiated",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventSpaceBooking_OpportunityStage_OpportunityStageId",
                table: "EventSpaceBooking",
                column: "OpportunityStageId",
                principalTable: "OpportunityStage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SendEmail_EventSpaceBooking_EventSpaceBookingId",
                table: "SendEmail",
                column: "EventSpaceBookingId",
                principalTable: "EventSpaceBooking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
