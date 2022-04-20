using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maisim_file_structure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeatmapSets",
                columns: table => new
                {
                    BeatmapSetID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Creator = table.Column<string>(type: "TEXT", nullable: false),
                    AudioFilename = table.Column<string>(type: "TEXT", nullable: false),
                    PreviewTime = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeatmapSets", x => x.BeatmapSetID);
                });

            migrationBuilder.CreateTable(
                name: "TrackMetadata",
                columns: table => new
                {
                    BeatmapSetID = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    TitleUnicode = table.Column<string>(type: "TEXT", nullable: false),
                    Artist = table.Column<string>(type: "TEXT", nullable: false),
                    ArtistUnicode = table.Column<string>(type: "TEXT", nullable: false),
                    Source = table.Column<string>(type: "TEXT", nullable: false),
                    CoverPath = table.Column<string>(type: "TEXT", nullable: false),
                    Bpm = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackMetadata", x => x.BeatmapSetID);
                    table.ForeignKey(
                        name: "FK_TrackMetadata_BeatmapSets_BeatmapSetID",
                        column: x => x.BeatmapSetID,
                        principalTable: "BeatmapSets",
                        principalColumn: "BeatmapSetID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beatmaps",
                columns: table => new
                {
                    BeatmapID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BeatmapSetID = table.Column<int>(type: "INTEGER", nullable: false),
                    DifficultyRating = table.Column<float>(type: "REAL", nullable: false),
                    DifficultyLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    TrackMetadataBeatmapSetID = table.Column<int>(type: "INTEGER", nullable: false),
                    NoteDesigner = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beatmaps", x => x.BeatmapID);
                    table.ForeignKey(
                        name: "FK_Beatmaps_BeatmapSets_BeatmapSetID",
                        column: x => x.BeatmapSetID,
                        principalTable: "BeatmapSets",
                        principalColumn: "BeatmapSetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Beatmaps_TrackMetadata_TrackMetadataBeatmapSetID",
                        column: x => x.TrackMetadataBeatmapSetID,
                        principalTable: "TrackMetadata",
                        principalColumn: "BeatmapSetID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beatmaps_BeatmapSetID",
                table: "Beatmaps",
                column: "BeatmapSetID");

            migrationBuilder.CreateIndex(
                name: "IX_Beatmaps_TrackMetadataBeatmapSetID",
                table: "Beatmaps",
                column: "TrackMetadataBeatmapSetID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beatmaps");

            migrationBuilder.DropTable(
                name: "TrackMetadata");

            migrationBuilder.DropTable(
                name: "BeatmapSets");
        }
    }
}
