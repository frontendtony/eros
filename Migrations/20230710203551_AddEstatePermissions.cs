using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EstateManager.Migrations
{
    /// <inheritdoc />
    public partial class AddEstatePermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.CreateTable(
                name: "EstatePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstatePermissions", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstatePermissions");

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("059a5b0c-1a62-4b7b-ac78-e90bdd091696"), "Permission to delete an estate", "DeleteEstate" },
                    { new Guid("4017b85c-fd7e-4b0f-8451-4154fc20a537"), "Permission to update an estate building", "UpdateEstateBuilding" },
                    { new Guid("63e58193-05b9-4de8-bdd4-9b14d0716362"), "Permission to create an estate", "CreateEstate" },
                    { new Guid("749cae08-a29d-4260-a553-07fc92a3532a"), "Permission to delete an estate building", "DeleteEstateBuilding" },
                    { new Guid("a4279e5c-00eb-4654-a06b-62a36dae17ca"), "Permission to update an estate", "UpdateEstate" },
                    { new Guid("c3bff129-326d-49ad-98de-e2781941043b"), "Permission to create an estate building", "CreateEstateBuilding" }
                });
        }
    }
}
