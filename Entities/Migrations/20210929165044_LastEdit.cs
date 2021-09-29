using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class LastEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EntrepriseId",
                table: "Zone",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Zone",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ServerTime",
                table: "Vente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Active",
                table: "User",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "User",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "User",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prenom",
                table: "User",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ServerTime",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "EntrepriseId",
                table: "Telephones",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Telephones",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntrepriseId",
                table: "Tailles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Tailles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntrepriseId",
                table: "Styles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Styles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntrepriseId",
                table: "Stock_Produit",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Stock_Produit",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntrepriseId",
                table: "Payement",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Payement",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntrepriseId",
                table: "Offre",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Offre",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntrepriseId",
                table: "Models",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Models",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntrepriseId",
                table: "Matiere_Premiere",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Matiere_Premiere",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntrepriseId",
                table: "Marques",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Marques",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntrepriseId",
                table: "Gamme",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Gamme",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntrepriseId",
                table: "Fournisseurs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Fournisseurs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adresse",
                table: "Entreprise",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Entreprise",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Entreprise",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "VilleId",
                table: "Entreprise",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EntrepriseId",
                table: "Email",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Email",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntrepriseId",
                table: "Client",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Client",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntrepriseId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Avarier",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "EntrepriseId",
                table: "Avarier",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Avarier",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EntrepriseUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateOfAdd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntrepriseUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntrepriseUser_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntrepriseUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zone_EntrepriseId",
                table: "Zone",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Zone_UserId",
                table: "Zone",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Telephones_EntrepriseId",
                table: "Telephones",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Telephones_UserId",
                table: "Telephones",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tailles_EntrepriseId",
                table: "Tailles",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tailles_UserId",
                table: "Tailles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Styles_EntrepriseId",
                table: "Styles",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Styles_UserId",
                table: "Styles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Produit_EntrepriseId",
                table: "Stock_Produit",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Produit_UserId",
                table: "Stock_Produit",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payement_EntrepriseId",
                table: "Payement",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Payement_UserId",
                table: "Payement",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Offre_EntrepriseId",
                table: "Offre",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Offre_UserId",
                table: "Offre",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_EntrepriseId",
                table: "Models",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_UserId",
                table: "Models",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Matiere_Premiere_EntrepriseId",
                table: "Matiere_Premiere",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Matiere_Premiere_UserId",
                table: "Matiere_Premiere",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Marques_EntrepriseId",
                table: "Marques",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Marques_UserId",
                table: "Marques",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamme_EntrepriseId",
                table: "Gamme",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamme_UserId",
                table: "Gamme",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Fournisseurs_EntrepriseId",
                table: "Fournisseurs",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Fournisseurs_UserId",
                table: "Fournisseurs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Entreprise_OwnerId",
                table: "Entreprise",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Entreprise_VilleId",
                table: "Entreprise",
                column: "VilleId");

            migrationBuilder.CreateIndex(
                name: "IX_Email_EntrepriseId",
                table: "Email",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Email_UserId",
                table: "Email",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_EntrepriseId",
                table: "Client",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_UserId",
                table: "Client",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_EntrepriseId",
                table: "Categories",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Avarier_EntrepriseId",
                table: "Avarier",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Avarier_UserId",
                table: "Avarier",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EntrepriseUser_EntrepriseId",
                table: "EntrepriseUser",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_EntrepriseUser_UserId",
                table: "EntrepriseUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avarier_Entreprise_EntrepriseId",
                table: "Avarier",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Avarier_User_UserId",
                table: "Avarier",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Entreprise_EntrepriseId",
                table: "Categories",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_User_UserId",
                table: "Categories",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Entreprise_EntrepriseId",
                table: "Client",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Client_User_UserId",
                table: "Client",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Email_Entreprise_EntrepriseId",
                table: "Email",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Email_User_UserId",
                table: "Email",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entreprise_User_OwnerId",
                table: "Entreprise",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entreprise_Ville_VilleId",
                table: "Entreprise",
                column: "VilleId",
                principalTable: "Ville",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fournisseurs_Entreprise_EntrepriseId",
                table: "Fournisseurs",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Fournisseurs_User_UserId",
                table: "Fournisseurs",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gamme_Entreprise_EntrepriseId",
                table: "Gamme",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gamme_User_UserId",
                table: "Gamme",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Marques_Entreprise_EntrepriseId",
                table: "Marques",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Marques_User_UserId",
                table: "Marques",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matiere_Premiere_Entreprise_EntrepriseId",
                table: "Matiere_Premiere",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Matiere_Premiere_User_UserId",
                table: "Matiere_Premiere",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Entreprise_EntrepriseId",
                table: "Models",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_User_UserId",
                table: "Models",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offre_Entreprise_EntrepriseId",
                table: "Offre",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offre_User_UserId",
                table: "Offre",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payement_Entreprise_EntrepriseId",
                table: "Payement",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payement_User_UserId",
                table: "Payement",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Produit_Entreprise_EntrepriseId",
                table: "Stock_Produit",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Produit_User_UserId",
                table: "Stock_Produit",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Styles_Entreprise_EntrepriseId",
                table: "Styles",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Styles_User_UserId",
                table: "Styles",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tailles_Entreprise_EntrepriseId",
                table: "Tailles",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tailles_User_UserId",
                table: "Tailles",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Telephones_Entreprise_EntrepriseId",
                table: "Telephones",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Telephones_User_UserId",
                table: "Telephones",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zone_Entreprise_EntrepriseId",
                table: "Zone",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zone_User_UserId",
                table: "Zone",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avarier_Entreprise_EntrepriseId",
                table: "Avarier");

            migrationBuilder.DropForeignKey(
                name: "FK_Avarier_User_UserId",
                table: "Avarier");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Entreprise_EntrepriseId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_User_UserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Client_Entreprise_EntrepriseId",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Client_User_UserId",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Email_Entreprise_EntrepriseId",
                table: "Email");

            migrationBuilder.DropForeignKey(
                name: "FK_Email_User_UserId",
                table: "Email");

            migrationBuilder.DropForeignKey(
                name: "FK_Entreprise_User_OwnerId",
                table: "Entreprise");

            migrationBuilder.DropForeignKey(
                name: "FK_Entreprise_Ville_VilleId",
                table: "Entreprise");

            migrationBuilder.DropForeignKey(
                name: "FK_Fournisseurs_Entreprise_EntrepriseId",
                table: "Fournisseurs");

            migrationBuilder.DropForeignKey(
                name: "FK_Fournisseurs_User_UserId",
                table: "Fournisseurs");

            migrationBuilder.DropForeignKey(
                name: "FK_Gamme_Entreprise_EntrepriseId",
                table: "Gamme");

            migrationBuilder.DropForeignKey(
                name: "FK_Gamme_User_UserId",
                table: "Gamme");

            migrationBuilder.DropForeignKey(
                name: "FK_Marques_Entreprise_EntrepriseId",
                table: "Marques");

            migrationBuilder.DropForeignKey(
                name: "FK_Marques_User_UserId",
                table: "Marques");

            migrationBuilder.DropForeignKey(
                name: "FK_Matiere_Premiere_Entreprise_EntrepriseId",
                table: "Matiere_Premiere");

            migrationBuilder.DropForeignKey(
                name: "FK_Matiere_Premiere_User_UserId",
                table: "Matiere_Premiere");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Entreprise_EntrepriseId",
                table: "Models");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_User_UserId",
                table: "Models");

            migrationBuilder.DropForeignKey(
                name: "FK_Offre_Entreprise_EntrepriseId",
                table: "Offre");

            migrationBuilder.DropForeignKey(
                name: "FK_Offre_User_UserId",
                table: "Offre");

            migrationBuilder.DropForeignKey(
                name: "FK_Payement_Entreprise_EntrepriseId",
                table: "Payement");

            migrationBuilder.DropForeignKey(
                name: "FK_Payement_User_UserId",
                table: "Payement");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Produit_Entreprise_EntrepriseId",
                table: "Stock_Produit");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Produit_User_UserId",
                table: "Stock_Produit");

            migrationBuilder.DropForeignKey(
                name: "FK_Styles_Entreprise_EntrepriseId",
                table: "Styles");

            migrationBuilder.DropForeignKey(
                name: "FK_Styles_User_UserId",
                table: "Styles");

            migrationBuilder.DropForeignKey(
                name: "FK_Tailles_Entreprise_EntrepriseId",
                table: "Tailles");

            migrationBuilder.DropForeignKey(
                name: "FK_Tailles_User_UserId",
                table: "Tailles");

            migrationBuilder.DropForeignKey(
                name: "FK_Telephones_Entreprise_EntrepriseId",
                table: "Telephones");

            migrationBuilder.DropForeignKey(
                name: "FK_Telephones_User_UserId",
                table: "Telephones");

            migrationBuilder.DropForeignKey(
                name: "FK_Zone_Entreprise_EntrepriseId",
                table: "Zone");

            migrationBuilder.DropForeignKey(
                name: "FK_Zone_User_UserId",
                table: "Zone");

            migrationBuilder.DropTable(
                name: "EntrepriseUser");

            migrationBuilder.DropIndex(
                name: "IX_Zone_EntrepriseId",
                table: "Zone");

            migrationBuilder.DropIndex(
                name: "IX_Zone_UserId",
                table: "Zone");

            migrationBuilder.DropIndex(
                name: "IX_Telephones_EntrepriseId",
                table: "Telephones");

            migrationBuilder.DropIndex(
                name: "IX_Telephones_UserId",
                table: "Telephones");

            migrationBuilder.DropIndex(
                name: "IX_Tailles_EntrepriseId",
                table: "Tailles");

            migrationBuilder.DropIndex(
                name: "IX_Tailles_UserId",
                table: "Tailles");

            migrationBuilder.DropIndex(
                name: "IX_Styles_EntrepriseId",
                table: "Styles");

            migrationBuilder.DropIndex(
                name: "IX_Styles_UserId",
                table: "Styles");

            migrationBuilder.DropIndex(
                name: "IX_Stock_Produit_EntrepriseId",
                table: "Stock_Produit");

            migrationBuilder.DropIndex(
                name: "IX_Stock_Produit_UserId",
                table: "Stock_Produit");

            migrationBuilder.DropIndex(
                name: "IX_Payement_EntrepriseId",
                table: "Payement");

            migrationBuilder.DropIndex(
                name: "IX_Payement_UserId",
                table: "Payement");

            migrationBuilder.DropIndex(
                name: "IX_Offre_EntrepriseId",
                table: "Offre");

            migrationBuilder.DropIndex(
                name: "IX_Offre_UserId",
                table: "Offre");

            migrationBuilder.DropIndex(
                name: "IX_Models_EntrepriseId",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_UserId",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Matiere_Premiere_EntrepriseId",
                table: "Matiere_Premiere");

            migrationBuilder.DropIndex(
                name: "IX_Matiere_Premiere_UserId",
                table: "Matiere_Premiere");

            migrationBuilder.DropIndex(
                name: "IX_Marques_EntrepriseId",
                table: "Marques");

            migrationBuilder.DropIndex(
                name: "IX_Marques_UserId",
                table: "Marques");

            migrationBuilder.DropIndex(
                name: "IX_Gamme_EntrepriseId",
                table: "Gamme");

            migrationBuilder.DropIndex(
                name: "IX_Gamme_UserId",
                table: "Gamme");

            migrationBuilder.DropIndex(
                name: "IX_Fournisseurs_EntrepriseId",
                table: "Fournisseurs");

            migrationBuilder.DropIndex(
                name: "IX_Fournisseurs_UserId",
                table: "Fournisseurs");

            migrationBuilder.DropIndex(
                name: "IX_Entreprise_OwnerId",
                table: "Entreprise");

            migrationBuilder.DropIndex(
                name: "IX_Entreprise_VilleId",
                table: "Entreprise");

            migrationBuilder.DropIndex(
                name: "IX_Email_EntrepriseId",
                table: "Email");

            migrationBuilder.DropIndex(
                name: "IX_Email_UserId",
                table: "Email");

            migrationBuilder.DropIndex(
                name: "IX_Client_EntrepriseId",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_UserId",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Categories_EntrepriseId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UserId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Avarier_EntrepriseId",
                table: "Avarier");

            migrationBuilder.DropIndex(
                name: "IX_Avarier_UserId",
                table: "Avarier");

            migrationBuilder.DropColumn(
                name: "EntrepriseId",
                table: "Zone");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Zone");

            migrationBuilder.DropColumn(
                name: "ServerTime",
                table: "Vente");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Nom",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Prenom",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ServerTime",
                table: "User");

            migrationBuilder.DropColumn(
                name: "EntrepriseId",
                table: "Telephones");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Telephones");

            migrationBuilder.DropColumn(
                name: "EntrepriseId",
                table: "Tailles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tailles");

            migrationBuilder.DropColumn(
                name: "EntrepriseId",
                table: "Styles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Styles");

            migrationBuilder.DropColumn(
                name: "EntrepriseId",
                table: "Stock_Produit");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Stock_Produit");

            migrationBuilder.DropColumn(
                name: "EntrepriseId",
                table: "Payement");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Payement");

            migrationBuilder.DropColumn(
                name: "EntrepriseId",
                table: "Offre");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Offre");

            migrationBuilder.DropColumn(
                name: "EntrepriseId",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "EntrepriseId",
                table: "Matiere_Premiere");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Matiere_Premiere");

            migrationBuilder.DropColumn(
                name: "EntrepriseId",
                table: "Marques");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Marques");

            migrationBuilder.DropColumn(
                name: "EntrepriseId",
                table: "Gamme");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Gamme");

            migrationBuilder.DropColumn(
                name: "EntrepriseId",
                table: "Fournisseurs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Fournisseurs");

            migrationBuilder.DropColumn(
                name: "Adresse",
                table: "Entreprise");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Entreprise");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Entreprise");

            migrationBuilder.DropColumn(
                name: "VilleId",
                table: "Entreprise");

            migrationBuilder.DropColumn(
                name: "EntrepriseId",
                table: "Email");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Email");

            migrationBuilder.DropColumn(
                name: "EntrepriseId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "EntrepriseId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Avarier");

            migrationBuilder.DropColumn(
                name: "EntrepriseId",
                table: "Avarier");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Avarier");
        }
    }
}
