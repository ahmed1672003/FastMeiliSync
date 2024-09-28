using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastMeiliSync.Infrastructure.Context.Migrations
{
    /// <inheritdoc />
    public partial class added_master_to_users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Master",
                schema: "Public",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Master",
                schema: "Public",
                table: "User");
        }
    }
}
