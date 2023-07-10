using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EstateManager.Migrations
{
    /// <inheritdoc />
    public partial class AddEstateRolesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("0233999b-0f4d-4cbf-8a9c-d1010b53ad7c"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("2af22c40-d3eb-40be-8cb9-7f637205973a"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("8339ba07-c29e-4e61-a362-ae7b2ef105b4"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("8639962b-5b20-4e25-afe0-84b3e07a6b20"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("bfc2c719-ba2f-4b97-9565-d117c58c3cba"));

            migrationBuilder.DeleteData(
                table: "EstatePermissions",
                keyColumn: "Id",
                keyValue: new Guid("de843a02-648f-4331-af34-84c676968b9f"));

            migrationBuilder.CreateTable(
                name: "EstateRolePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EstateRoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    EstatePermissionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateRolePermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstateRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstateRoles", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstateRolePermissions");

            migrationBuilder.DropTable(
                name: "EstateRoles");

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

            migrationBuilder.InsertData(
                table: "EstatePermissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("0233999b-0f4d-4cbf-8a9c-d1010b53ad7c"), "Permission to create an estate", "CreateEstate" },
                    { new Guid("2af22c40-d3eb-40be-8cb9-7f637205973a"), "Permission to update an estate", "UpdateEstate" },
                    { new Guid("8339ba07-c29e-4e61-a362-ae7b2ef105b4"), "Permission to create an estate building", "CreateEstateBuilding" },
                    { new Guid("8639962b-5b20-4e25-afe0-84b3e07a6b20"), "Permission to delete an estate building", "DeleteEstateBuilding" },
                    { new Guid("bfc2c719-ba2f-4b97-9565-d117c58c3cba"), "Permission to update an estate building", "UpdateEstateBuilding" },
                    { new Guid("de843a02-648f-4331-af34-84c676968b9f"), "Permission to delete an estate", "DeleteEstate" }
                });
        }
    }
}
