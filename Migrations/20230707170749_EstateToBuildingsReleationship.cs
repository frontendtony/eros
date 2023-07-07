using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstateManager.Migrations
{
    /// <inheritdoc />
    public partial class EstateToBuildingsReleationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "EstateBuildings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_EstateBuildings_EstateId",
                table: "EstateBuildings",
                column: "EstateId");

            migrationBuilder.AddForeignKey(
                name: "FK_EstateBuildings_Estates_EstateId",
                table: "EstateBuildings",
                column: "EstateId",
                principalTable: "Estates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstateBuildings_Estates_EstateId",
                table: "EstateBuildings");

            migrationBuilder.DropIndex(
                name: "IX_EstateBuildings_EstateId",
                table: "EstateBuildings");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "EstateBuildings",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
