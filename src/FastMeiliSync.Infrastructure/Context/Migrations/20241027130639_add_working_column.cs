using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastMeiliSync.Infrastructure.Context.Migrations
{
    /// <inheritdoc />
    public partial class add_working_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Working",
                schema: "Public",
                table: "Sync",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Working",
                schema: "Public",
                table: "Sync");
        }
    }
}
