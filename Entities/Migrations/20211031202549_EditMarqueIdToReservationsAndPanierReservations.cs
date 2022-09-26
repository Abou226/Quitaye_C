using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class EditMarqueIdToReservationsAndPanierReservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MarqueId",
                table: "Reservation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MarqueId",
                table: "PanierReservation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_MarqueId",
                table: "Reservation",
                column: "MarqueId");

            migrationBuilder.CreateIndex(
                name: "IX_PanierReservation_MarqueId",
                table: "PanierReservation",
                column: "MarqueId");

            migrationBuilder.AddForeignKey(
                name: "FK_PanierReservation_Marques_MarqueId",
                table: "PanierReservation",
                column: "MarqueId",
                principalTable: "Marques",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Marques_MarqueId",
                table: "Reservation",
                column: "MarqueId",
                principalTable: "Marques",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PanierReservation_Marques_MarqueId",
                table: "PanierReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Marques_MarqueId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_MarqueId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_PanierReservation_MarqueId",
                table: "PanierReservation");

            migrationBuilder.DropColumn(
                name: "MarqueId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "MarqueId",
                table: "PanierReservation");
        }
    }
}
