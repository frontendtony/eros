using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EstateManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoleAndPermissionNavigation3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("2b328be2-1b62-4fdb-8229-355bb5be69fa"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("33281eda-3d5b-4b11-9c8e-3e865693bfec"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("53779268-773e-4649-8fa8-a3545f9116e9"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("5cb2022d-15ad-457e-85bd-cd6c1aa7bf6f"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("a79d58ee-6d39-459b-9d2a-4dbb7fa87810"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("aee36fd2-93a7-4eab-afa8-6baae529349c"));

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

            migrationBuilder.CreateIndex(
                name: "IX_EstateRolePermissions_EstatePermissionId",
                table: "EstateRolePermissions",
                column: "EstatePermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_EstateRolePermissions_EstatePermissions_EstatePermissionId",
                table: "EstateRolePermissions",
                column: "EstatePermissionId",
                principalTable: "EstatePermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstateRolePermissions_EstatePermissions_EstatePermissionId",
                table: "EstateRolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_EstateRolePermissions_EstatePermissionId",
                table: "EstateRolePermissions");

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

            migrationBuilder.InsertData(
                table: "EstatePermissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("2b328be2-1b62-4fdb-8229-355bb5be69fa"), "Permission to update an estate building", "UpdateEstateBuilding" },
                    { new Guid("33281eda-3d5b-4b11-9c8e-3e865693bfec"), "Permission to create an estate", "CreateEstate" },
                    { new Guid("53779268-773e-4649-8fa8-a3545f9116e9"), "Permission to delete an estate", "DeleteEstate" },
                    { new Guid("5cb2022d-15ad-457e-85bd-cd6c1aa7bf6f"), "Permission to update an estate", "UpdateEstate" },
                    { new Guid("a79d58ee-6d39-459b-9d2a-4dbb7fa87810"), "Permission to delete an estate building", "DeleteEstateBuilding" },
                    { new Guid("aee36fd2-93a7-4eab-afa8-6baae529349c"), "Permission to create an estate building", "CreateEstateBuilding" }
                });
        }
    }
}
