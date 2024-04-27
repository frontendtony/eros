using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GenerateInvitationCodeFromId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Invitations");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Invitations",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Invitations");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Invitations",
                type: "character varying(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");
        }
    }
}
