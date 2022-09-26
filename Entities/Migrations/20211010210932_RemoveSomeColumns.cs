using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class RemoveSomeColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pays_Entreprise_EntrepriseId",
                table: "Pays");

            migrationBuilder.DropForeignKey(
                name: "FK_Type_Entreprise_User_UserId",
                table: "Type_Entreprise");

            migrationBuilder.DropForeignKey(
                name: "FK_Ville_Entreprise_EntrepriseId",
                table: "Ville");

            migrationBuilder.DropIndex(
                name: "IX_Ville_EntrepriseId",
                table: "Ville");

            migrationBuilder.DropIndex(
                name: "IX_Type_Entreprise_UserId",
                table: "Type_Entreprise");

            migrationBuilder.DropIndex(
                name: "IX_Pays_EntrepriseId",
                table: "Pays");

            migrationBuilder.DropColumn(
                name: "EntrepriseId",
                table: "Ville");

            migrationBuilder.DropColumn(
                name: "DateOfCreation",
                table: "Type_Entreprise");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Type_Entreprise");

            migrationBuilder.DropColumn(
                name: "EntrepriseId",
                table: "Pays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EntrepriseId",
                table: "Ville",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCreation",
                table: "Type_Entreprise",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Type_Entreprise",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntrepriseId",
                table: "Pays",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ville_EntrepriseId",
                table: "Ville",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Type_Entreprise_UserId",
                table: "Type_Entreprise",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pays_EntrepriseId",
                table: "Pays",
                column: "EntrepriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pays_Entreprise_EntrepriseId",
                table: "Pays",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Type_Entreprise_User_UserId",
                table: "Type_Entreprise",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ville_Entreprise_EntrepriseId",
                table: "Ville",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
