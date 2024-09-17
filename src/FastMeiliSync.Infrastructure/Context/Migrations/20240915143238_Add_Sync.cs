using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeiliSync.Infrastructure.Context.Migrations
{
    /// <inheritdoc />
    public partial class Add_Sync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sync",
                schema: "Public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    MeiliSearchId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    UpdatedOn = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    ),
                    DeletedOn = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: true
                    ),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sync", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sync_MeiliSearche_MeiliSearchId",
                        column: x => x.MeiliSearchId,
                        principalSchema: "Public",
                        principalTable: "MeiliSearche",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Sync_Source_SourceId",
                        column: x => x.SourceId,
                        principalSchema: "Public",
                        principalTable: "Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Sync_MeiliSearchId",
                schema: "Public",
                table: "Sync",
                column: "MeiliSearchId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Sync_SourceId_MeiliSearchId",
                schema: "Public",
                table: "Sync",
                columns: new[] { "SourceId", "MeiliSearchId" },
                unique: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Sync", schema: "Public");
        }
    }
}
