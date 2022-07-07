using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourPlanner.Data.Migrations
{
    public partial class baseMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TourId",
                table: "Adress",
                newName: "AdressId");

            migrationBuilder.AddColumn<Guid>(
                name: "TourIdDest",
                table: "Tours",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TourIdStart",
                table: "Tours",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adress",
                table: "Adress",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_TourIdDest",
                table: "Tours",
                column: "TourIdDest");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_TourIdStart",
                table: "Tours",
                column: "TourIdStart");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Adress_TourIdDest",
                table: "Tours",
                column: "TourIdDest",
                principalTable: "Adress",
                principalColumn: "AdressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Adress_TourIdStart",
                table: "Tours",
                column: "TourIdStart",
                principalTable: "Adress",
                principalColumn: "AdressId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Adress_TourIdDest",
                table: "Tours");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Adress_TourIdStart",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_TourIdDest",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_TourIdStart",
                table: "Tours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adress",
                table: "Adress");

            migrationBuilder.DropColumn(
                name: "TourIdDest",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "TourIdStart",
                table: "Tours");

            migrationBuilder.RenameColumn(
                name: "AdressId",
                table: "Adress",
                newName: "TourId");
        }
    }
}
