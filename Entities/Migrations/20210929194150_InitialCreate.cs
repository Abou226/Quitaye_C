using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExternalLogin",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfLogin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalLogin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gamme",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategorieId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MarqueId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StyleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Prix_Unité = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamme", x => x.Id);
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
                    ServerTime = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "Avarier",
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
                    table.PrimaryKey("PK_Avarier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Genre = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
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
                });

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.Id);
                });

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
                });

            migrationBuilder.CreateTable(
                name: "Fournisseurs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    Unité = table.Column<int>(type: "int", nullable: false),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
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
                });

            migrationBuilder.CreateTable(
                name: "Offre",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GammeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TailleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    Nature = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    Quantité = table.Column<decimal>(type: "decimal (18,2)", nullable: false),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                name: "Styles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telephones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Nom = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Username = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Active = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ServerTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    EntrperiseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    RefreshTokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Refreshable = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServerTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.RefreshTokenId);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
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
                        name: "FK_Ville_Pays_PaysId",
                        column: x => x.PaysId,
                        principalTable: "Pays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entreprise",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    VilleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entreprise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entreprise_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entreprise_Ville_VilleId",
                        column: x => x.VilleId,
                        principalTable: "Ville",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zone",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zone_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zone_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
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
                name: "IX_Avarier_EntrepriseId",
                table: "Avarier",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Avarier_OffreId",
                table: "Avarier",
                column: "OffreId");

            migrationBuilder.CreateIndex(
                name: "IX_Avarier_UserId",
                table: "Avarier",
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
                name: "IX_Client_EntrepriseId",
                table: "Client",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_UserId",
                table: "Client",
                column: "UserId");

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
                name: "IX_Email_EntrepriseId",
                table: "Email",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Email_UserId",
                table: "Email",
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
                name: "IX_EntrepriseUser_EntrepriseId",
                table: "EntrepriseUser",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_EntrepriseUser_UserId",
                table: "EntrepriseUser",
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
                name: "IX_Gamme_CategorieId",
                table: "Gamme",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamme_EntrepriseId",
                table: "Gamme",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamme_MarqueId",
                table: "Gamme",
                column: "MarqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamme_StyleId",
                table: "Gamme",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamme_UserId",
                table: "Gamme",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Livraison_VenteId",
                table: "Livraison",
                column: "VenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Marques_EntrepriseId",
                table: "Marques",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Marques_UserId",
                table: "Marques",
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
                name: "IX_Models_EntrepriseId",
                table: "Models",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_UserId",
                table: "Models",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Num_Payement_EntrepriseId",
                table: "Num_Payement",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Num_Vente_EntrepriseId",
                table: "Num_Vente",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Offre_EntrepriseId",
                table: "Offre",
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
                name: "IX_Offre_UserId",
                table: "Offre",
                column: "UserId");

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
                name: "IX_Payement_EntrepriseId",
                table: "Payement",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Payement_Num_PayementId",
                table: "Payement",
                column: "Num_PayementId");

            migrationBuilder.CreateIndex(
                name: "IX_Payement_UserId",
                table: "Payement",
                column: "UserId");

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
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Produit_EntrepriseId",
                table: "Stock_Produit",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Produit_OffreId",
                table: "Stock_Produit",
                column: "OffreId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Produit_UserId",
                table: "Stock_Produit",
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
                name: "IX_Tailles_EntrepriseId",
                table: "Tailles",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tailles_UserId",
                table: "Tailles",
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

            migrationBuilder.CreateIndex(
                name: "IX_Zone_EntrepriseId",
                table: "Zone",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Zone_UserId",
                table: "Zone",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gamme_Categories_CategorieId",
                table: "Gamme",
                column: "CategorieId",
                principalTable: "Categories",
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
                name: "FK_Gamme_Marques_MarqueId",
                table: "Gamme",
                column: "MarqueId",
                principalTable: "Marques",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gamme_Styles_StyleId",
                table: "Gamme",
                column: "StyleId",
                principalTable: "Styles",
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
                name: "FK_Panier_Client_ClientId",
                table: "Panier",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Panier_Entreprise_EntrepriseId",
                table: "Panier",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Panier_Offre_OffreId",
                table: "Panier",
                column: "OffreId",
                principalTable: "Offre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Panier_Quartier_QuartierId",
                table: "Panier",
                column: "QuartierId",
                principalTable: "Quartier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vente_Client_ClientId",
                table: "Vente",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vente_Entreprise_EntrepriseId",
                table: "Vente",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vente_Num_Vente_Num_VenteId",
                table: "Vente",
                column: "Num_VenteId",
                principalTable: "Num_Vente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vente_Offre_OffreId",
                table: "Vente",
                column: "OffreId",
                principalTable: "Offre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vente_Quartier_QuartierId",
                table: "Vente",
                column: "QuartierId",
                principalTable: "Quartier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quartier_Commune_CommuneId",
                table: "Quartier",
                column: "CommuneId",
                principalTable: "Commune",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quartier_Entreprise_EntrepriseId",
                table: "Quartier",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quartier_Zone_ZoneId",
                table: "Quartier",
                column: "ZoneId",
                principalTable: "Zone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pays_Continent_ContinentId",
                table: "Pays",
                column: "ContinentId",
                principalTable: "Continent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pays_Entreprise_EntrepriseId",
                table: "Pays",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Achat_Matiere_Entreprise_EntrepriseId",
                table: "Achat_Matiere",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Achat_Matiere_Matiere_Premiere_MatiereId",
                table: "Achat_Matiere",
                column: "MatiereId",
                principalTable: "Matiere_Premiere",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Avarier_Entreprise_EntrepriseId",
                table: "Avarier",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Avarier_Offre_OffreId",
                table: "Avarier",
                column: "OffreId",
                principalTable: "Offre",
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
                name: "FK_Commune_Entreprise_EntrepriseId",
                table: "Commune",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Commune_Ville_VilleId",
                table: "Commune",
                column: "VilleId",
                principalTable: "Ville",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Conso_Matiere_Entreprise_EntrepriseId",
                table: "Conso_Matiere",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Conso_Matiere_Matiere_Premiere_MatiereId",
                table: "Conso_Matiere",
                column: "MatiereId",
                principalTable: "Matiere_Premiere",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Continent_Entreprise_EntrepriseId",
                table: "Continent",
                column: "EntrepriseId",
                principalTable: "Entreprise",
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
                name: "FK_EntrepriseUser_Entreprise_EntrepriseId",
                table: "EntrepriseUser",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntrepriseUser_User_UserId",
                table: "EntrepriseUser",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Num_Payement_Entreprise_EntrepriseId",
                table: "Num_Payement",
                column: "EntrepriseId",
                principalTable: "Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Num_Vente_Entreprise_EntrepriseId",
                table: "Num_Vente",
                column: "EntrepriseId",
                principalTable: "Entreprise",
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
                name: "FK_Offre_Tailles_TailleId",
                table: "Offre",
                column: "TailleId",
                principalTable: "Tailles",
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
                name: "FK_Produit_Fini_Entreprise_EntrepriseId",
                table: "Produit_Fini",
                column: "EntrepriseId",
                principalTable: "Entreprise",
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
                name: "FK_User_Entreprise_EntrperiseId",
                table: "User",
                column: "EntrperiseId",
                principalTable: "Entreprise",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Continent_Entreprise_EntrepriseId",
                table: "Continent");

            migrationBuilder.DropForeignKey(
                name: "FK_Pays_Entreprise_EntrepriseId",
                table: "Pays");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Entreprise_EntrperiseId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_Ville_Entreprise_EntrepriseId",
                table: "Ville");

            migrationBuilder.DropTable(
                name: "Achat_Matiere");

            migrationBuilder.DropTable(
                name: "Avarier");

            migrationBuilder.DropTable(
                name: "Conso_Matiere");

            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "EntrepriseUser");

            migrationBuilder.DropTable(
                name: "ExternalLogin");

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
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "Stock_Produit");

            migrationBuilder.DropTable(
                name: "Telephones");

            migrationBuilder.DropTable(
                name: "Matiere_Premiere");

            migrationBuilder.DropTable(
                name: "Vente");

            migrationBuilder.DropTable(
                name: "Num_Payement");

            migrationBuilder.DropTable(
                name: "Client");

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
                name: "Entreprise");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Ville");

            migrationBuilder.DropTable(
                name: "Pays");

            migrationBuilder.DropTable(
                name: "Continent");
        }
    }
}
