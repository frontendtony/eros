using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddListVisitorBookingsPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("c7837520-c674-4dfa-bc4b-9d03e3753125"), "VisitorBooking.List" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c7837520-c674-4dfa-bc4b-9d03e3753125"));
        }
    }
}
