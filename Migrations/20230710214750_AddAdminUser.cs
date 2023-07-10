using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EstateManager.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("04f3d8cb-cb23-44d8-bec6-56d90384d8b3"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("16be238e-82dd-4513-a543-34dff0944807"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("32e9d95c-e4f2-4c50-9688-8bfe081a1334"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("4e3a6dbc-4837-4a70-9f79-2275bae6d021"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("6ed5907a-1744-4db9-8ae9-cc2381563711"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("9b985cf7-ba77-4adf-991e-87774bfeeedf"));

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "EstatePermissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("04f3d8cb-cb23-44d8-bec6-56d90384d8b3"), "Permission to delete an estate", "DeleteEstate" },
                    { new Guid("16be238e-82dd-4513-a543-34dff0944807"), "Permission to update an estate building", "UpdateEstateBuilding" },
                    { new Guid("32e9d95c-e4f2-4c50-9688-8bfe081a1334"), "Permission to create an estate", "CreateEstate" },
                    { new Guid("4e3a6dbc-4837-4a70-9f79-2275bae6d021"), "Permission to create an estate building", "CreateEstateBuilding" },
                    { new Guid("6ed5907a-1744-4db9-8ae9-cc2381563711"), "Permission to delete an estate building", "DeleteEstateBuilding" },
                    { new Guid("9b985cf7-ba77-4adf-991e-87774bfeeedf"), "Permission to update an estate", "UpdateEstate" }
                });
        }
    }
}
