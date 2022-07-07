using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourPlanner.Data.Migrations
{
    public partial class baseMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tours_TourIdDest",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_TourIdStart",
                table: "Tours");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_TourIdDest",
                table: "Tours",
                column: "TourIdDest",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tours_TourIdStart",
                table: "Tours",
                column: "TourIdStart",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tours_TourIdDest",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_TourIdStart",
                table: "Tours");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_TourIdDest",
                table: "Tours",
                column: "TourIdDest");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_TourIdStart",
                table: "Tours",
                column: "TourIdStart");
        }
    }
}
