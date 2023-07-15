using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EstateManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoleAndPermissionNavigation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "EstateRolePermissions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "EstateRolePermissions");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "EstateRoles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "EstateRoles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "EstateRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "EstateRoles");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "EstateRolePermissions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "EstateRolePermissions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.AddForeignKey(
                name: "FK_EstateRolePermissions_EstatePermissions_EstatePermissionId",
                table: "EstateRolePermissions",
                column: "EstatePermissionId",
                principalTable: "EstatePermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
