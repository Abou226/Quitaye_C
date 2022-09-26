using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class EditClientTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entreprise_Type_Entreprise_Type_Id",
                table: "Entreprise");

            migrationBuilder.DropColumn(
                name: "Adresse",
                table: "Entreprise");

            migrationBuilder.AlterColumn<Guid>(
                name: "Type_Id",
                table: "Entreprise",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "QuartierId",
                table: "Entreprise",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCreation",
                table: "Client",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Client",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Client",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Client",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Client",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entreprise_QuartierId",
                table: "Entreprise",
                column: "QuartierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entreprise_Quartier_QuartierId",
                table: "Entreprise",
                column: "QuartierId",
                principalTable: "Quartier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entreprise_Type_Entreprise_Type_Id",
                table: "Entreprise",
                column: "Type_Id",
                principalTable: "Type_Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entreprise_Quartier_QuartierId",
                table: "Entreprise");

            migrationBuilder.DropForeignKey(
                name: "FK_Entreprise_Type_Entreprise_Type_Id",
                table: "Entreprise");

            migrationBuilder.DropIndex(
                name: "IX_Entreprise_QuartierId",
                table: "Entreprise");

            migrationBuilder.DropColumn(
                name: "QuartierId",
                table: "Entreprise");

            migrationBuilder.DropColumn(
                name: "DateOfCreation",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Client");

            migrationBuilder.AlterColumn<Guid>(
                name: "Type_Id",
                table: "Entreprise",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adresse",
                table: "Entreprise",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Entreprise_Type_Entreprise_Type_Id",
                table: "Entreprise",
                column: "Type_Id",
                principalTable: "Type_Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
