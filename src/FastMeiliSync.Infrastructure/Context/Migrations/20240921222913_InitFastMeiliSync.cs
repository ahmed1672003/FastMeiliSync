using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastMeiliSync.Infrastructure.Context.Migrations
{
    /// <inheritdoc />
    public partial class InitFastMeiliSync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Public");

            migrationBuilder.CreateTable(
                name: "MeiliSearche",
                schema: "Public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    ApiKey = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeiliSearche", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Source",
                schema: "Public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Database = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Source", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sync",
                schema: "Public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    SourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    MeiliSearchId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sync_Source_SourceId",
                        column: x => x.SourceId,
                        principalSchema: "Public",
                        principalTable: "Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeiliSearche_Label",
                schema: "Public",
                table: "MeiliSearche",
                column: "Label",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeiliSearche_Url",
                schema: "Public",
                table: "MeiliSearche",
                column: "Url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Source_Database",
                schema: "Public",
                table: "Source",
                column: "Database",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Source_Label",
                schema: "Public",
                table: "Source",
                column: "Label",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Source_Url",
                schema: "Public",
                table: "Source",
                column: "Url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sync_Label",
                schema: "Public",
                table: "Sync",
                column: "Label",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sync_MeiliSearchId",
                schema: "Public",
                table: "Sync",
                column: "MeiliSearchId");

            migrationBuilder.CreateIndex(
                name: "IX_Sync_SourceId_MeiliSearchId",
                schema: "Public",
                table: "Sync",
                columns: new[] { "SourceId", "MeiliSearchId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sync",
                schema: "Public");

            migrationBuilder.DropTable(
                name: "MeiliSearche",
                schema: "Public");

            migrationBuilder.DropTable(
                name: "Source",
                schema: "Public");
        }
    }
}
