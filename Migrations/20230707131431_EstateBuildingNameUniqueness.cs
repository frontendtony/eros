using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstateManager.Migrations
{
    /// <inheritdoc />
    public partial class EstateBuildingNameUniqueness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EstateBuildings_Name",
                table: "EstateBuildings",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EstateBuildings_Name",
                table: "EstateBuildings");
        }
    }
}
