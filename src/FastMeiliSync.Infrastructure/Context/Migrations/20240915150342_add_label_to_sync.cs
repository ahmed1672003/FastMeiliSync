using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeiliSync.Infrastructure.Context.Migrations
{
    /// <inheritdoc />
    public partial class add_label_to_sync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Label",
                schema: "Public",
                table: "Sync",
                type: "text",
                nullable: false,
                defaultValue: ""
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Label", schema: "Public", table: "Sync");
        }
    }
}
