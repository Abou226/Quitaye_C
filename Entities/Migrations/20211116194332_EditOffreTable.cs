using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class EditOffreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offre_Gamme_GammeId",
                table: "Offre");

            migrationBuilder.RenameColumn(
                name: "GammeId",
                table: "Offre",
                newName: "StyleId");

            migrationBuilder.RenameIndex(
                name: "IX_Offre_GammeId",
                table: "Offre",
                newName: "IX_Offre_StyleId");

            migrationBuilder.AddColumn<Guid>(
                name: "CategorieId",
                table: "Offre",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MarqueId",
                table: "Offre",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NiveauId",
                table: "Offre",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OccasionId",
                table: "Offre",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offre_CategorieId",
                table: "Offre",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Offre_MarqueId",
                table: "Offre",
                column: "MarqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Offre_NiveauId",
                table: "Offre",
                column: "NiveauId");

            migrationBuilder.CreateIndex(
                name: "IX_Offre_OccasionId",
                table: "Offre",
                column: "OccasionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offre_Categories_CategorieId",
                table: "Offre",
                column: "CategorieId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offre_Marques_MarqueId",
                table: "Offre",
                column: "MarqueId",
                principalTable: "Marques",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offre_Niveaus_NiveauId",
                table: "Offre",
                column: "NiveauId",
                principalTable: "Niveaus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offre_Occasions_OccasionId",
                table: "Offre",
                column: "OccasionId",
                principalTable: "Occasions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offre_Styles_StyleId",
                table: "Offre",
                column: "StyleId",
                principalTable: "Styles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offre_Categories_CategorieId",
                table: "Offre");

            migrationBuilder.DropForeignKey(
                name: "FK_Offre_Marques_MarqueId",
                table: "Offre");

            migrationBuilder.DropForeignKey(
                name: "FK_Offre_Niveaus_NiveauId",
                table: "Offre");

            migrationBuilder.DropForeignKey(
                name: "FK_Offre_Occasions_OccasionId",
                table: "Offre");

            migrationBuilder.DropForeignKey(
                name: "FK_Offre_Styles_StyleId",
                table: "Offre");

            migrationBuilder.DropIndex(
                name: "IX_Offre_CategorieId",
                table: "Offre");

            migrationBuilder.DropIndex(
                name: "IX_Offre_MarqueId",
                table: "Offre");

            migrationBuilder.DropIndex(
                name: "IX_Offre_NiveauId",
                table: "Offre");

            migrationBuilder.DropIndex(
                name: "IX_Offre_OccasionId",
                table: "Offre");

            migrationBuilder.DropColumn(
                name: "CategorieId",
                table: "Offre");

            migrationBuilder.DropColumn(
                name: "MarqueId",
                table: "Offre");

            migrationBuilder.DropColumn(
                name: "NiveauId",
                table: "Offre");

            migrationBuilder.DropColumn(
                name: "OccasionId",
                table: "Offre");

            migrationBuilder.RenameColumn(
                name: "StyleId",
                table: "Offre",
                newName: "GammeId");

            migrationBuilder.RenameIndex(
                name: "IX_Offre_StyleId",
                table: "Offre",
                newName: "IX_Offre_GammeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offre_Gamme_GammeId",
                table: "Offre",
                column: "GammeId",
                principalTable: "Gamme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
