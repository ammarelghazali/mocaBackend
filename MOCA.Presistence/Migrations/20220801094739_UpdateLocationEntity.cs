using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOCA.Presistence.Migrations
{
    public partial class UpdateLocationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_LocationBankAccount_LocationBankAccountId",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Location_LocationBankAccountId",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "LocationBankAccountId",
                table: "Location");

            migrationBuilder.CreateIndex(
                name: "IX_LocationBankAccount_LocationId",
                table: "LocationBankAccount",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationBankAccount_Location_LocationId",
                table: "LocationBankAccount",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationBankAccount_Location_LocationId",
                table: "LocationBankAccount");

            migrationBuilder.DropIndex(
                name: "IX_LocationBankAccount_LocationId",
                table: "LocationBankAccount");

            migrationBuilder.AddColumn<long>(
                name: "LocationBankAccountId",
                table: "Location",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_LocationBankAccountId",
                table: "Location",
                column: "LocationBankAccountId",
                unique: true,
                filter: "[LocationBankAccountId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_LocationBankAccount_LocationBankAccountId",
                table: "Location",
                column: "LocationBankAccountId",
                principalTable: "LocationBankAccount",
                principalColumn: "Id");
        }
    }
}
