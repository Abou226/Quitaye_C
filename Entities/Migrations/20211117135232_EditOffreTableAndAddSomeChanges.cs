using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class EditOffreTableAndAddSomeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offre_Tailles_TailleId",
                table: "Offre");

            migrationBuilder.RenameColumn(
                name: "TailleId",
                table: "Offre",
                newName: "TailleMinId");

            migrationBuilder.RenameIndex(
                name: "IX_Offre_TailleId",
                table: "Offre",
                newName: "IX_Offre_TailleMinId");

            migrationBuilder.AddColumn<decimal>(
                name: "Prix_Unité",
                table: "Offre",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Offre",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Offre_Tailles_TailleMinId",
                table: "Offre",
                column: "TailleMinId",
                principalTable: "Tailles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offre_Tailles_TailleMinId",
                table: "Offre");

            migrationBuilder.DropColumn(
                name: "Prix_Unité",
                table: "Offre");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Offre");

            migrationBuilder.RenameColumn(
                name: "TailleMinId",
                table: "Offre",
                newName: "TailleId");

            migrationBuilder.RenameIndex(
                name: "IX_Offre_TailleMinId",
                table: "Offre",
                newName: "IX_Offre_TailleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offre_Tailles_TailleId",
                table: "Offre",
                column: "TailleId",
                principalTable: "Tailles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
