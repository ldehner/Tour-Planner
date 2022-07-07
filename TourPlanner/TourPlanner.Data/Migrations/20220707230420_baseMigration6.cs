using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourPlanner.Data.Migrations
{
    public partial class baseMigration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Tours_TourId",
                table: "Logs");

            migrationBuilder.AlterColumn<Guid>(
                name: "TourId",
                table: "Logs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Tours_TourId",
                table: "Logs",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "TourId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Tours_TourId",
                table: "Logs");

            migrationBuilder.AlterColumn<Guid>(
                name: "TourId",
                table: "Logs",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Tours_TourId",
                table: "Logs",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "TourId");
        }
    }
}
