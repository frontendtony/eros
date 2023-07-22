using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstateManager.Migrations
{
    /// <inheritdoc />
    public partial class EstatePermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EstatePermissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("070e110c-bd98-411f-b291-e3f87271451a"), "Permission to update an estate building", "UpdateEstateBuilding" },
                    { new Guid("38e64366-c467-4c73-a19c-8766f0040f23"), "Permission to create an estate", "CreateEstate" },
                    { new Guid("402fb180-e064-4ff8-ae36-a84a3f5bedfa"), "Permission to update an estate", "UpdateEstate" },
                    { new Guid("4d457e8c-8634-4d3e-ae90-f4c7a026e1a6"), "Permission to create an estate building", "CreateEstateBuilding" },
                    { new Guid("6649b456-89d7-49b4-bdbe-d77c1111ecbc"), "Permission to delete an estate", "DeleteEstate" },
                    { new Guid("6f0d50d8-f3e9-408c-be0c-4274cf64086a"), "Permission to delete an estate building", "DeleteEstateBuilding" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("070e110c-bd98-411f-b291-e3f87271451a"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("38e64366-c467-4c73-a19c-8766f0040f23"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("402fb180-e064-4ff8-ae36-a84a3f5bedfa"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("4d457e8c-8634-4d3e-ae90-f4c7a026e1a6"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("6649b456-89d7-49b4-bdbe-d77c1111ecbc"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("6f0d50d8-f3e9-408c-be0c-4274cf64086a"));
        }
    }
}
