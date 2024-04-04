using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Eros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBasicPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Permissions");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1a4b7beb-488b-4666-86d4-991895e0872b"), "Role.Update" },
                    { new Guid("1c8a89a0-b6fe-4efc-b0e3-12db170433fe"), "Apartment.Delete" },
                    { new Guid("2a84ccda-2adf-4215-8e2e-5bbd72d5216c"), "Building.Update" },
                    { new Guid("2c67bdb4-7d9e-419a-816d-be29da2e837a"), "Apartment.List" },
                    { new Guid("32e40dda-f579-4fd8-aeee-d157cedeb062"), "Estate.Delete" },
                    { new Guid("4c6e1ac5-b00e-41f5-89fa-c801578a9818"), "Role.Delete" },
                    { new Guid("61e201f7-7859-40b9-bbe8-715f29204291"), "Apartment.Update" },
                    { new Guid("8b7a4964-46b3-4a03-a19e-86c35cb5b3cd"), "Apartment.View" },
                    { new Guid("8cfdbbb4-cb87-4d78-9806-c20a63b87530"), "Estate.Create" },
                    { new Guid("9765a47d-7e67-4d24-aa86-af372748ec7a"), "Apartment.Create" },
                    { new Guid("98d6ab3d-8a40-44bc-85d6-96c739813f9d"), "Role.Create" },
                    { new Guid("af23db24-f6c0-4448-9f43-67288c4f5328"), "Building.View" },
                    { new Guid("b80d1e12-44bb-4457-a1ce-e6fbbbd74cec"), "Building.Create" },
                    { new Guid("bd848626-40c8-499a-b9f8-2886cf57d8c6"), "Building.Delete" },
                    { new Guid("d542cd68-6d1a-4588-92c1-b35f550a2a1b"), "Estate.Update" },
                    { new Guid("e0a2cb5c-28e2-4ee7-8fb9-a00a3e761e5e"), "Role.List" },
                    { new Guid("e300d634-8686-40b2-b396-6e0ac5cb0d09"), "Building.List" },
                    { new Guid("e45f233e-6dbd-4228-8b18-a23a66c1b18b"), "Estate.View" },
                    { new Guid("ed64a073-1821-407c-add6-2e88f5d045e1"), "Role.View" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                table: "Permissions",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Permissions_Name",
                table: "Permissions");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("1a4b7beb-488b-4666-86d4-991895e0872b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("1c8a89a0-b6fe-4efc-b0e3-12db170433fe"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2a84ccda-2adf-4215-8e2e-5bbd72d5216c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2c67bdb4-7d9e-419a-816d-be29da2e837a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("32e40dda-f579-4fd8-aeee-d157cedeb062"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("4c6e1ac5-b00e-41f5-89fa-c801578a9818"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("61e201f7-7859-40b9-bbe8-715f29204291"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8b7a4964-46b3-4a03-a19e-86c35cb5b3cd"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8cfdbbb4-cb87-4d78-9806-c20a63b87530"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("9765a47d-7e67-4d24-aa86-af372748ec7a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("98d6ab3d-8a40-44bc-85d6-96c739813f9d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("af23db24-f6c0-4448-9f43-67288c4f5328"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("b80d1e12-44bb-4457-a1ce-e6fbbbd74cec"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bd848626-40c8-499a-b9f8-2886cf57d8c6"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d542cd68-6d1a-4588-92c1-b35f550a2a1b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e0a2cb5c-28e2-4ee7-8fb9-a00a3e761e5e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e300d634-8686-40b2-b396-6e0ac5cb0d09"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e45f233e-6dbd-4228-8b18-a23a66c1b18b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ed64a073-1821-407c-add6-2e88f5d045e1"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Permissions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Permissions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Permissions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);
        }
    }
}
