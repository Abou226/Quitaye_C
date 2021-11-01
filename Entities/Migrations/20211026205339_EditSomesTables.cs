using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class EditSomesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PanierReservation_Num_Vente_NumVenteId",
                table: "PanierReservation");

            migrationBuilder.DropIndex(
                name: "IX_PanierReservation_NumVenteId",
                table: "PanierReservation");

            migrationBuilder.DropColumn(
                name: "NumVenteId",
                table: "PanierReservation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NumVenteId",
                table: "PanierReservation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PanierReservation_NumVenteId",
                table: "PanierReservation",
                column: "NumVenteId");

            migrationBuilder.AddForeignKey(
                name: "FK_PanierReservation_Num_Vente_NumVenteId",
                table: "PanierReservation",
                column: "NumVenteId",
                principalTable: "Num_Vente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
