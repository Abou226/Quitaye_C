using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class AddOccasionListTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Occasions_Offre_OffreId",
                table: "Occasions");

            migrationBuilder.DropIndex(
                name: "IX_Occasions_OffreId",
                table: "Occasions");

            migrationBuilder.DropColumn(
                name: "OffreId",
                table: "Occasions");

            migrationBuilder.CreateTable(
                name: "OccasionLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    EntrepriseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OffreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OccasionLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OccasionLists_Entreprise_EntrepriseId",
                        column: x => x.EntrepriseId,
                        principalTable: "Entreprise",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OccasionLists_Offre_OffreId",
                        column: x => x.OffreId,
                        principalTable: "Offre",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OccasionLists_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OccasionLists_EntrepriseId",
                table: "OccasionLists",
                column: "EntrepriseId");

            migrationBuilder.CreateIndex(
                name: "IX_OccasionLists_OffreId",
                table: "OccasionLists",
                column: "OffreId");

            migrationBuilder.CreateIndex(
                name: "IX_OccasionLists_UserId",
                table: "OccasionLists",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OccasionLists");

            migrationBuilder.AddColumn<Guid>(
                name: "OffreId",
                table: "Occasions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Occasions_OffreId",
                table: "Occasions",
                column: "OffreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Occasions_Offre_OffreId",
                table: "Occasions",
                column: "OffreId",
                principalTable: "Offre",
                principalColumn: "Id");
        }
    }
}
