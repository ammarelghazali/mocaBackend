using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class AddsEmailTemplateTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "EmailTemplate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "EmailTemplate",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "EmailTemplate",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "EmailTemplate",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmailTemplateType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplateType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplate_EmailTemplateTypeID",
                table: "EmailTemplate",
                column: "EmailTemplateTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailTemplate_EmailTemplateType_EmailTemplateTypeID",
                table: "EmailTemplate",
                column: "EmailTemplateTypeID",
                principalTable: "EmailTemplateType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailTemplate_EmailTemplateType_EmailTemplateTypeID",
                table: "EmailTemplate");

            migrationBuilder.DropTable(
                name: "EmailTemplateType");

            migrationBuilder.DropIndex(
                name: "IX_EmailTemplate_EmailTemplateTypeID",
                table: "EmailTemplate");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EmailTemplate");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EmailTemplate");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "EmailTemplate");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "EmailTemplate");
        }
    }
}
