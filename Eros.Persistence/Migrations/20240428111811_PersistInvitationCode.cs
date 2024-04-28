using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eros.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PersistInvitationCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Invitations",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Invitations");
        }
    }
}
