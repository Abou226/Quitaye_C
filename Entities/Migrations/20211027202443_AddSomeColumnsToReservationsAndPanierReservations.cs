using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class AddSomeColumnsToReservationsAndPanierReservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PanierReservation_Offre_OffreId",
                table: "PanierReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Offre_OffreId",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "OffreId",
                table: "Reservation",
                newName: "GammeId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_OffreId",
                table: "Reservation",
                newName: "IX_Reservation_GammeId");

            migrationBuilder.RenameColumn(
                name: "OffreId",
                table: "PanierReservation",
                newName: "GammeId");

            migrationBuilder.RenameIndex(
                name: "IX_PanierReservation_OffreId",
                table: "PanierReservation",
                newName: "IX_PanierReservation_GammeId");

            migrationBuilder.AddColumn<Guid>(
                name: "ModelId",
                table: "Reservation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TailleId",
                table: "Reservation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModelId",
                table: "PanierReservation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TailleId",
                table: "PanierReservation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ModelId",
                table: "Reservation",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_TailleId",
                table: "Reservation",
                column: "TailleId");

            migrationBuilder.CreateIndex(
                name: "IX_PanierReservation_ModelId",
                table: "PanierReservation",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PanierReservation_TailleId",
                table: "PanierReservation",
                column: "TailleId");

            migrationBuilder.AddForeignKey(
                name: "FK_PanierReservation_Gamme_GammeId",
                table: "PanierReservation",
                column: "GammeId",
                principalTable: "Gamme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PanierReservation_Models_ModelId",
                table: "PanierReservation",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PanierReservation_Tailles_TailleId",
                table: "PanierReservation",
                column: "TailleId",
                principalTable: "Tailles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Gamme_GammeId",
                table: "Reservation",
                column: "GammeId",
                principalTable: "Gamme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Models_ModelId",
                table: "Reservation",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Tailles_TailleId",
                table: "Reservation",
                column: "TailleId",
                principalTable: "Tailles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PanierReservation_Gamme_GammeId",
                table: "PanierReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_PanierReservation_Models_ModelId",
                table: "PanierReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_PanierReservation_Tailles_TailleId",
                table: "PanierReservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Gamme_GammeId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Models_ModelId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Tailles_TailleId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_ModelId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_TailleId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_PanierReservation_ModelId",
                table: "PanierReservation");

            migrationBuilder.DropIndex(
                name: "IX_PanierReservation_TailleId",
                table: "PanierReservation");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "TailleId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "PanierReservation");

            migrationBuilder.DropColumn(
                name: "TailleId",
                table: "PanierReservation");

            migrationBuilder.RenameColumn(
                name: "GammeId",
                table: "Reservation",
                newName: "OffreId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_GammeId",
                table: "Reservation",
                newName: "IX_Reservation_OffreId");

            migrationBuilder.RenameColumn(
                name: "GammeId",
                table: "PanierReservation",
                newName: "OffreId");

            migrationBuilder.RenameIndex(
                name: "IX_PanierReservation_GammeId",
                table: "PanierReservation",
                newName: "IX_PanierReservation_OffreId");

            migrationBuilder.AddForeignKey(
                name: "FK_PanierReservation_Offre_OffreId",
                table: "PanierReservation",
                column: "OffreId",
                principalTable: "Offre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Offre_OffreId",
                table: "Reservation",
                column: "OffreId",
                principalTable: "Offre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
