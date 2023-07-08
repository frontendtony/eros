using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstateManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEstateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StateProvince",
                table: "Estates",
                newName: "State");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Estates",
                newName: "StateProvince");
        }
    }
}
