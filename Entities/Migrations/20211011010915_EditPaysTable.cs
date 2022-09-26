using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class EditPaysTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Continent_Entreprise_EntrepriseId",
                table: "Continent");

            migrationBuilder.DropForeignKey(
                name: "FK_Pays_Continent_ContinentId",
                table: "Pays");

            migrationBuilder.DropIndex(
                name: "IX_Pays_ContinentId",
                table: "Pays");

            migrationBuilder.DropIndex(
                name: "IX_Continent_EntrepriseId",
                table: "Continent");

            migrationBuilder.DropColumn(
                name: "ContinentId",
                table: "Pays");

            migrationBuilder.DropColumn(
                name: "EntrepriseId",
                table: "Continent");

            migrationBuilder.AddColumn<string>(
                name: "Alpha_2",
                table: "Pays",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Alpha_3",
                table: "Pays",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Indicatif",
                table: "Pays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "Pays",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alpha_2",
                table: "Pays");

            migrationBuilder.DropColumn(
                name: "Alpha_3",
                table: "Pays");

            migrationBuilder.DropColumn(
                name: "Indicatif",
                table: "Pays");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "Pays");

            migrationBuilder.AddColumn<Guid>(
                name: "ContinentId",
                table: "Pays",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntrepriseId",
                table: "Continent",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pays_ContinentId",
                table: "Pays",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_Continent_EntrepriseId",
                table: "Continent",
                column: "EntrepriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Continent_Entreprise_EntrepriseId",
                table: "Continent",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pays_Continent_ContinentId",
                table: "Pays",
                column: "ContinentId",
                principalTable: "Continent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
