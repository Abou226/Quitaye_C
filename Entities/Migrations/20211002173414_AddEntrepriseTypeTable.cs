using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class AddEntrepriseTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Entreprise");

            migrationBuilder.AddColumn<Guid>(
                name: "Type_Id",
                table: "Entreprise",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Type_Entreprise",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type_Entreprise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Type_Entreprise_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entreprise_Type_Id",
                table: "Entreprise",
                column: "Type_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Type_Entreprise_UserId",
                table: "Type_Entreprise",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entreprise_Type_Entreprise_Type_Id",
                table: "Entreprise",
                column: "Type_Id",
                principalTable: "Type_Entreprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entreprise_Type_Entreprise_Type_Id",
                table: "Entreprise");

            migrationBuilder.DropTable(
                name: "Type_Entreprise");

            migrationBuilder.DropIndex(
                name: "IX_Entreprise_Type_Id",
                table: "Entreprise");

            migrationBuilder.DropColumn(
                name: "Type_Id",
                table: "Entreprise");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Entreprise",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
