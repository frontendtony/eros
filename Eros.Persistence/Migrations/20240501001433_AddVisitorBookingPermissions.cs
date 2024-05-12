using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Eros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddVisitorBookingPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Purpose",
                table: "VisitorBookings",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "VisitorBookings",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("72571cf2-435b-440a-a5c3-d9fb84c550ba"), "VisitorBooking.Reject" },
                    { new Guid("c08453b3-f11d-4fe3-ad43-b50b6cb47ec1"), "VisitorBooking.Admit" },
                    { new Guid("ff644b10-f762-403b-b4a9-a511fa0b6e08"), "VisitorBooking.Create" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("72571cf2-435b-440a-a5c3-d9fb84c550ba"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c08453b3-f11d-4fe3-ad43-b50b6cb47ec1"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ff644b10-f762-403b-b4a9-a511fa0b6e08"));

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "VisitorBookings");

            migrationBuilder.AlterColumn<string>(
                name: "Purpose",
                table: "VisitorBookings",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
