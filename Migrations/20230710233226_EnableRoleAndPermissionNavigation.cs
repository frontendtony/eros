using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EstateManager.Migrations
{
    /// <inheritdoc />
    public partial class EnableRoleAndPermissionNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("169b0302-cb70-4a8c-a046-398d4daf74a4"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("31c35a30-3f72-4edb-9c0d-cceb9f9aa2b8"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("6088b2d8-3400-4677-96bc-04c90dc57b40"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("d898d4c4-1b6a-4225-8013-e9b8a8ffec20"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("dc34dd3d-24fd-4489-80cb-c61603e84747"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("e8e3c5a0-8e21-4c2a-94ad-344ed8e8ed6d"));

            migrationBuilder.InsertData(
                table: "EstatePermissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("5aefed4f-f17f-47b4-89fc-d62275aab1e7"), "Permission to create an estate", "CreateEstate" },
                    { new Guid("5c089e93-b47f-4554-b87d-c99a18f52ab1"), "Permission to delete an estate", "DeleteEstate" },
                    { new Guid("5e9dfe37-8b83-4d14-9087-65c4d02aeed3"), "Permission to update an estate", "UpdateEstate" },
                    { new Guid("c773fd51-5d5c-4783-a845-b3dd265f25f7"), "Permission to update an estate building", "UpdateEstateBuilding" },
                    { new Guid("df0a5e8e-6a0f-4575-894f-678640f9b394"), "Permission to delete an estate building", "DeleteEstateBuilding" },
                    { new Guid("f434477d-e806-465b-8f6e-db578e9fa827"), "Permission to create an estate building", "CreateEstateBuilding" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstateRolePermissions_EstatePermissionId",
                table: "EstateRolePermissions",
                column: "EstatePermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_EstateRolePermissions_EstateRoleId",
                table: "EstateRolePermissions",
                column: "EstateRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_EstateRolePermissions_EstatePermissions_EstatePermissionId",
                table: "EstateRolePermissions",
                column: "EstatePermissionId",
                principalTable: "EstatePermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EstateRolePermissions_EstateRoles_EstateRoleId",
                table: "EstateRolePermissions",
                column: "EstateRoleId",
                principalTable: "EstateRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstateRolePermissions_EstatePermissions_EstatePermissionId",
                table: "EstateRolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_EstateRolePermissions_EstateRoles_EstateRoleId",
                table: "EstateRolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_EstateRolePermissions_EstatePermissionId",
                table: "EstateRolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_EstateRolePermissions_EstateRoleId",
                table: "EstateRolePermissions");

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("5aefed4f-f17f-47b4-89fc-d62275aab1e7"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("5c089e93-b47f-4554-b87d-c99a18f52ab1"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("5e9dfe37-8b83-4d14-9087-65c4d02aeed3"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("c773fd51-5d5c-4783-a845-b3dd265f25f7"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("df0a5e8e-6a0f-4575-894f-678640f9b394"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("f434477d-e806-465b-8f6e-db578e9fa827"));

            migrationBuilder.InsertData(
                table: "EstatePermissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("169b0302-cb70-4a8c-a046-398d4daf74a4"), "Permission to update an estate building", "UpdateEstateBuilding" },
                    { new Guid("31c35a30-3f72-4edb-9c0d-cceb9f9aa2b8"), "Permission to create an estate building", "CreateEstateBuilding" },
                    { new Guid("6088b2d8-3400-4677-96bc-04c90dc57b40"), "Permission to update an estate", "UpdateEstate" },
                    { new Guid("d898d4c4-1b6a-4225-8013-e9b8a8ffec20"), "Permission to delete an estate", "DeleteEstate" },
                    { new Guid("dc34dd3d-24fd-4489-80cb-c61603e84747"), "Permission to create an estate", "CreateEstate" },
                    { new Guid("e8e3c5a0-8e21-4c2a-94ad-344ed8e8ed6d"), "Permission to delete an estate building", "DeleteEstateBuilding" }
                });
        }
    }
}
