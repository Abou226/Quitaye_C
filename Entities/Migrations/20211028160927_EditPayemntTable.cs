using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class EditPayemntTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PanierReservation_Client_ClientId",
                table: "PanierReservation");

            migrationBuilder.DropIndex(
                name: "IX_PanierReservation_ClientId",
                table: "PanierReservation");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "Payement",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payement_ClientId",
                table: "Payement",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payement_Client_ClientId",
                table: "Payement",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payement_Client_ClientId",
                table: "Payement");

            migrationBuilder.DropIndex(
                name: "IX_Payement_ClientId",
                table: "Payement");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Payement");

            migrationBuilder.CreateIndex(
                name: "IX_PanierReservation_ClientId",
                table: "PanierReservation",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_PanierReservation_Client_ClientId",
                table: "PanierReservation",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
