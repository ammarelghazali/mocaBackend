using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class ssoBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Gender",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Gender",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Gender",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Gender",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Gender",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ClientDevice",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ClientDevice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ClientDevice",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "ClientDevice",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ClientDevice",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "BasicUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "BasicUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BasicUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "BasicUser",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "BasicUser",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Gender");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Gender");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Gender");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "Gender");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Gender");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ClientDevice");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ClientDevice");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ClientDevice");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "ClientDevice");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ClientDevice");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BasicUser");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "BasicUser");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BasicUser");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "BasicUser");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "BasicUser");
        }
    }
}
