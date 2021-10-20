using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class EditSomeConfigTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Autres_Info",
                table: "Vente",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "Vente",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PanierId",
                table: "Vente",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CategorieId",
                table: "Tailles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Marques",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Prix_Min",
                table: "Gamme",
                type: "decimal (18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Gamme",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Categories",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PanierReservation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OffreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantité = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Autres_Info = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Prix_Vente_Unité = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Adresse_Livraison = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Heure_Livraison = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Date_Livraison = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServerTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumVenteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Mention = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Details_Adresse = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    QuartierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Contact_Client = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Contact_Livraison = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Annulée = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PanierReservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PanierReservation_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PanierReservation_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PanierReservation_Num_Vente_NumVenteId",
                        column: x => x.NumVenteId,
                        principalTable: "Num_Vente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PanierReservation_Offre_OffreId",
                        column: x => x.OffreId,
                        principalTable: "Offre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PanierReservation_Quartier_QuartierId",
                        column: x => x.QuartierId,
                        principalTable: "Quartier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PanierReservation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PanierVente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OffreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantité = table.Column<decimal>(type: "decimal (18,0)", nullable: false),
                    Prix_Unité = table.Column<decimal>(type: "decimal (18,0)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Details_Adresse = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    QuartierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Autres_Info = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Livraison = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Heure_Livraison = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Contact_Livraison = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PanierVente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PanierVente_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PanierVente_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PanierVente_Offre_OffreId",
                        column: x => x.OffreId,
                        principalTable: "Offre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PanierVente_Quartier_QuartierId",
                        column: x => x.QuartierId,
                        principalTable: "Quartier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OffreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantité = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Autres_Info = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Prix_Vente_Unité = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Details_Adresse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuartierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Heure_Livraison = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Date_Livraison = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServerTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumVenteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Mention = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Contact_Client = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Contact_Livraison = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PanierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Annulée = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservation_Num_Vente_NumVenteId",
                        column: x => x.NumVenteId,
                        principalTable: "Num_Vente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservation_Offre_OffreId",
                        column: x => x.OffreId,
                        principalTable: "Offre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservation_Quartier_QuartierId",
                        column: x => x.QuartierId,
                        principalTable: "Quartier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tailles_CategorieId",
                table: "Tailles",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_PanierReservation_ClientId",
                table: "PanierReservation",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PanierReservation_EntrepriseId",
                table: "PanierReservation",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_PanierReservation_NumVenteId",
                table: "PanierReservation",
                column: "NumVenteId");

            migrationBuilder.CreateIndex(
                name: "IX_PanierReservation_OffreId",
                table: "PanierReservation",
                column: "OffreId");

            migrationBuilder.CreateIndex(
                name: "IX_PanierReservation_QuartierId",
                table: "PanierReservation",
                column: "QuartierId");

            migrationBuilder.CreateIndex(
                name: "IX_PanierReservation_UserId",
                table: "PanierReservation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PanierVente_ClientId",
                table: "PanierVente",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PanierVente_EntrepriseId",
                table: "PanierVente",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_PanierVente_OffreId",
                table: "PanierVente",
                column: "OffreId");

            migrationBuilder.CreateIndex(
                name: "IX_PanierVente_QuartierId",
                table: "PanierVente",
                column: "QuartierId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ClientId",
                table: "Reservation",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_NumVenteId",
                table: "Reservation",
                column: "NumVenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_OffreId",
                table: "Reservation",
                column: "OffreId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_QuartierId",
                table: "Reservation",
                column: "QuartierId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_UserId",
                table: "Reservation",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tailles_Categories_CategorieId",
                table: "Tailles",
                column: "CategorieId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tailles_Categories_CategorieId",
                table: "Tailles");

            migrationBuilder.DropTable(
                name: "PanierReservation");

            migrationBuilder.DropTable(
                name: "PanierVente");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Tailles_CategorieId",
                table: "Tailles");

            migrationBuilder.DropColumn(
                name: "Autres_Info",
                table: "Vente");

            migrationBuilder.DropColumn(
                name: "Designation",
                table: "Vente");

            migrationBuilder.DropColumn(
                name: "PanierId",
                table: "Vente");

            migrationBuilder.DropColumn(
                name: "CategorieId",
                table: "Tailles");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Marques");

            migrationBuilder.DropColumn(
                name: "Prix_Min",
                table: "Gamme");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Gamme");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Categories");
        }
    }
}
