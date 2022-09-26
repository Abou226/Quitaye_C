using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class EditOccasionPropertyInOffreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offre_Occasions_OccasionId",
                table: "Offre");

            migrationBuilder.DropIndex(
                name: "IX_Offre_OccasionId",
                table: "Offre");

            migrationBuilder.DropColumn(
                name: "OccasionId",
                table: "Offre");

            migrationBuilder.AddColumn<Guid>(
                name: "OffreId",
                table: "Occasions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DepartementId",
                table: "EntrepriseUser",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "EntrepriseUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Departements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departements", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Occasions_OffreId",
                table: "Occasions",
                column: "OffreId");

            migrationBuilder.CreateIndex(
                name: "IX_EntrepriseUser_DepartementId",
                table: "EntrepriseUser",
                column: "DepartementId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntrepriseUser_Departements_DepartementId",
                table: "EntrepriseUser",
                column: "DepartementId",
                principalTable: "Departements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Occasions_Offre_OffreId",
                table: "Occasions",
                column: "OffreId",
                principalTable: "Offre",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntrepriseUser_Departements_DepartementId",
                table: "EntrepriseUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Occasions_Offre_OffreId",
                table: "Occasions");

            migrationBuilder.DropTable(
                name: "Departements");

            migrationBuilder.DropIndex(
                name: "IX_Occasions_OffreId",
                table: "Occasions");

            migrationBuilder.DropIndex(
                name: "IX_EntrepriseUser_DepartementId",
                table: "EntrepriseUser");

            migrationBuilder.DropColumn(
                name: "OffreId",
                table: "Occasions");

            migrationBuilder.DropColumn(
                name: "DepartementId",
                table: "EntrepriseUser");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "EntrepriseUser");

            migrationBuilder.AddColumn<Guid>(
                name: "OccasionId",
                table: "Offre",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offre_OccasionId",
                table: "Offre",
                column: "OccasionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offre_Occasions_OccasionId",
                table: "Offre",
                column: "OccasionId",
                principalTable: "Occasions",
                principalColumn: "Id");
        }
    }
}
