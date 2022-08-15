using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class deleteInclusions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationInclusion_Inclusion_InclusionId",
                table: "LocationInclusion");

            migrationBuilder.DropTable(
                name: "Inclusion");

            migrationBuilder.RenameColumn(
                name: "InclusionId",
                table: "LocationInclusion",
                newName: "AmenityId");

            migrationBuilder.RenameIndex(
                name: "IX_LocationInclusion_InclusionId",
                table: "LocationInclusion",
                newName: "IX_LocationInclusion_AmenityId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationInclusion_Amenity_AmenityId",
                table: "LocationInclusion",
                column: "AmenityId",
                principalTable: "Amenity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationInclusion_Amenity_AmenityId",
                table: "LocationInclusion");

            migrationBuilder.RenameColumn(
                name: "AmenityId",
                table: "LocationInclusion",
                newName: "InclusionId");

            migrationBuilder.RenameIndex(
                name: "IX_LocationInclusion_AmenityId",
                table: "LocationInclusion",
                newName: "IX_LocationInclusion_InclusionId");

            migrationBuilder.CreateTable(
                name: "Inclusion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inclusion", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_LocationInclusion_Inclusion_InclusionId",
                table: "LocationInclusion",
                column: "InclusionId",
                principalTable: "Inclusion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
