using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class EditsommesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entreprise_User_OwnerId",
                table: "Entreprise");

            migrationBuilder.DropIndex(
                name: "IX_Entreprise_OwnerId",
                table: "Entreprise");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Entreprise_OwnerId",
                table: "Entreprise",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entreprise_User_OwnerId",
                table: "Entreprise",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
