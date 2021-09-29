using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entreprise",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entreprise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fournisseurs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fournisseurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marques",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marques", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matiere_Premiere",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Unité = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matiere_Premiere", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Styles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Styles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tailles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tailles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Telephones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telephones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zone",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zone", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Continent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Continent_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Num_Payement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Num_Payement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Num_Payement_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Num_Vente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Num_Vente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Num_Vente_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntrperiseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Entreprise_EntrperiseId",
                        column: x => x.EntrperiseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Achat_Matiere",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatiereId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantité = table.Column<decimal>(type: "decimal (18,0)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Unité = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achat_Matiere", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achat_Matiere_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Achat_Matiere_Matiere_Premiere_MatiereId",
                        column: x => x.MatiereId,
                        principalTable: "Matiere_Premiere",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Conso_Matiere",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatiereId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantité = table.Column<decimal>(type: "decimal (18,0)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Unité = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conso_Matiere", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conso_Matiere_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Conso_Matiere_Matiere_Premiere_MatiereId",
                        column: x => x.MatiereId,
                        principalTable: "Matiere_Premiere",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gamme",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategorieId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MarqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StyleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Prix_Unité = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamme", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gamme_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gamme_Marques_MarqueId",
                        column: x => x.MarqueId,
                        principalTable: "Marques",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gamme_Styles_StyleId",
                        column: x => x.StyleId,
                        principalTable: "Styles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ContinentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pays_Continent_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pays_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Montant = table.Column<decimal>(type: "decimal (18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date_Payement = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Num_Operation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Num_PayementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Nature = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payement_Num_Payement_Num_PayementId",
                        column: x => x.Num_PayementId,
                        principalTable: "Num_Payement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Offre",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GammeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TailleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offre_Gamme_GammeId",
                        column: x => x.GammeId,
                        principalTable: "Gamme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offre_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offre_Tailles_TailleId",
                        column: x => x.TailleId,
                        principalTable: "Tailles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ville",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    PaysId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ville", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ville_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ville_Pays_PaysId",
                        column: x => x.PaysId,
                        principalTable: "Pays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Avarier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OffreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantité = table.Column<decimal>(type: "decimal (18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avarier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Avarier_Offre_OffreId",
                        column: x => x.OffreId,
                        principalTable: "Offre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Produit_Fini",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OffreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantité = table.Column<decimal>(type: "decimal (18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produit_Fini", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produit_Fini_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produit_Fini_Offre_OffreId",
                        column: x => x.OffreId,
                        principalTable: "Offre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stock_Produit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OffreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantité = table.Column<decimal>(type: "decimal (18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock_Produit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stock_Produit_Offre_OffreId",
                        column: x => x.OffreId,
                        principalTable: "Offre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Commune",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    VilleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commune", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commune_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Commune_Ville_VilleId",
                        column: x => x.VilleId,
                        principalTable: "Ville",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Quartier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CommuneId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quartier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quartier_Commune_CommuneId",
                        column: x => x.CommuneId,
                        principalTable: "Commune",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quartier_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Quartier_Zone_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Panier",
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
                    Date_Livraison = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Heure_Livraison = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Contact_Livraison = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Panier_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Panier_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Panier_Offre_OffreId",
                        column: x => x.OffreId,
                        principalTable: "Offre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Panier_Quartier_QuartierId",
                        column: x => x.QuartierId,
                        principalTable: "Quartier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vente",
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
                    Num_VenteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuartierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date_Livraison = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Heure_Livraison = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Contact_Livraison = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vente_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vente_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vente_Num_Vente_Num_VenteId",
                        column: x => x.Num_VenteId,
                        principalTable: "Num_Vente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vente_Offre_OffreId",
                        column: x => x.OffreId,
                        principalTable: "Offre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vente_Quartier_QuartierId",
                        column: x => x.QuartierId,
                        principalTable: "Quartier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Livraison",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VenteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Livré = table.Column<bool>(type: "bit", nullable: false),
                    Raison = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livraison", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Livraison_Vente_VenteId",
                        column: x => x.VenteId,
                        principalTable: "Vente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achat_Matiere_EntrepriseId",
                table: "Achat_Matiere",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Achat_Matiere_MatiereId",
                table: "Achat_Matiere",
                column: "MatiereId");

            migrationBuilder.CreateIndex(
                name: "IX_Avarier_OffreId",
                table: "Avarier",
                column: "OffreId");

            migrationBuilder.CreateIndex(
                name: "IX_Commune_EntrepriseId",
                table: "Commune",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Commune_VilleId",
                table: "Commune",
                column: "VilleId");

            migrationBuilder.CreateIndex(
                name: "IX_Conso_Matiere_EntrepriseId",
                table: "Conso_Matiere",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Conso_Matiere_MatiereId",
                table: "Conso_Matiere",
                column: "MatiereId");

            migrationBuilder.CreateIndex(
                name: "IX_Continent_EntrepriseId",
                table: "Continent",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamme_CategorieId",
                table: "Gamme",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamme_MarqueId",
                table: "Gamme",
                column: "MarqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamme_StyleId",
                table: "Gamme",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_Livraison_VenteId",
                table: "Livraison",
                column: "VenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Num_Payement_EntrepriseId",
                table: "Num_Payement",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Num_Vente_EntrepriseId",
                table: "Num_Vente",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Offre_GammeId",
                table: "Offre",
                column: "GammeId");

            migrationBuilder.CreateIndex(
                name: "IX_Offre_ModelId",
                table: "Offre",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Offre_TailleId",
                table: "Offre",
                column: "TailleId");

            migrationBuilder.CreateIndex(
                name: "IX_Panier_ClientId",
                table: "Panier",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Panier_EntrepriseId",
                table: "Panier",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Panier_OffreId",
                table: "Panier",
                column: "OffreId");

            migrationBuilder.CreateIndex(
                name: "IX_Panier_QuartierId",
                table: "Panier",
                column: "QuartierId");

            migrationBuilder.CreateIndex(
                name: "IX_Payement_Num_PayementId",
                table: "Payement",
                column: "Num_PayementId");

            migrationBuilder.CreateIndex(
                name: "IX_Pays_ContinentId",
                table: "Pays",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_Pays_EntrepriseId",
                table: "Pays",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_Fini_EntrepriseId",
                table: "Produit_Fini",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_Fini_OffreId",
                table: "Produit_Fini",
                column: "OffreId");

            migrationBuilder.CreateIndex(
                name: "IX_Quartier_CommuneId",
                table: "Quartier",
                column: "CommuneId");

            migrationBuilder.CreateIndex(
                name: "IX_Quartier_EntrepriseId",
                table: "Quartier",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Quartier_ZoneId",
                table: "Quartier",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Produit_OffreId",
                table: "Stock_Produit",
                column: "OffreId");

            migrationBuilder.CreateIndex(
                name: "IX_User_EntrperiseId",
                table: "User",
                column: "EntrperiseId");

            migrationBuilder.CreateIndex(
                name: "IX_Vente_ClientId",
                table: "Vente",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Vente_EntrepriseId",
                table: "Vente",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Vente_Num_VenteId",
                table: "Vente",
                column: "Num_VenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Vente_OffreId",
                table: "Vente",
                column: "OffreId");

            migrationBuilder.CreateIndex(
                name: "IX_Vente_QuartierId",
                table: "Vente",
                column: "QuartierId");

            migrationBuilder.CreateIndex(
                name: "IX_Ville_EntrepriseId",
                table: "Ville",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Ville_PaysId",
                table: "Ville",
                column: "PaysId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achat_Matiere");

            migrationBuilder.DropTable(
                name: "Avarier");

            migrationBuilder.DropTable(
                name: "Conso_Matiere");

            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "Fournisseurs");

            migrationBuilder.DropTable(
                name: "Livraison");

            migrationBuilder.DropTable(
                name: "Panier");

            migrationBuilder.DropTable(
                name: "Payement");

            migrationBuilder.DropTable(
                name: "Produit_Fini");

            migrationBuilder.DropTable(
                name: "Stock_Produit");

            migrationBuilder.DropTable(
                name: "Telephones");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Matiere_Premiere");

            migrationBuilder.DropTable(
                name: "Vente");

            migrationBuilder.DropTable(
                name: "Num_Payement");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Num_Vente");

            migrationBuilder.DropTable(
                name: "Offre");

            migrationBuilder.DropTable(
                name: "Quartier");

            migrationBuilder.DropTable(
                name: "Gamme");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Tailles");

            migrationBuilder.DropTable(
                name: "Commune");

            migrationBuilder.DropTable(
                name: "Zone");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Marques");

            migrationBuilder.DropTable(
                name: "Styles");

            migrationBuilder.DropTable(
                name: "Ville");

            migrationBuilder.DropTable(
                name: "Pays");

            migrationBuilder.DropTable(
                name: "Continent");

            migrationBuilder.DropTable(
                name: "Entreprise");
        }
    }
}
