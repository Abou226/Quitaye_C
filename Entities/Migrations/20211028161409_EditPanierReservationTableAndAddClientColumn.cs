using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class EditPanierReservationTableAndAddClientColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PanierReservation_Client_ClientId",
                table: "PanierReservation");

            migrationBuilder.DropIndex(
                name: "IX_PanierReservation_ClientId",
                table: "PanierReservation");
        }
    }
}
