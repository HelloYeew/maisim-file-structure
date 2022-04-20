using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maisim_file_structure.Migrations
{
    public partial class ChangeKeyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beatmaps_TrackMetadata_TrackMetadataBeatmapSetID",
                table: "Beatmaps");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackMetadata_BeatmapSets_BeatmapSetID",
                table: "TrackMetadata");

            migrationBuilder.RenameColumn(
                name: "BeatmapSetID",
                table: "TrackMetadata",
                newName: "ConnectBeatmapSetID");

            migrationBuilder.RenameColumn(
                name: "TrackMetadataBeatmapSetID",
                table: "Beatmaps",
                newName: "TrackMetadataConnectBeatmapSetID");

            migrationBuilder.RenameIndex(
                name: "IX_Beatmaps_TrackMetadataBeatmapSetID",
                table: "Beatmaps",
                newName: "IX_Beatmaps_TrackMetadataConnectBeatmapSetID");

            migrationBuilder.AlterColumn<int>(
                name: "ConnectBeatmapSetID",
                table: "TrackMetadata",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "TrackMetadataConnectBeatmapSetID",
                table: "BeatmapSets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BeatmapSets_TrackMetadataConnectBeatmapSetID",
                table: "BeatmapSets",
                column: "TrackMetadataConnectBeatmapSetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Beatmaps_TrackMetadata_TrackMetadataConnectBeatmapSetID",
                table: "Beatmaps",
                column: "TrackMetadataConnectBeatmapSetID",
                principalTable: "TrackMetadata",
                principalColumn: "ConnectBeatmapSetID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BeatmapSets_TrackMetadata_TrackMetadataConnectBeatmapSetID",
                table: "BeatmapSets",
                column: "TrackMetadataConnectBeatmapSetID",
                principalTable: "TrackMetadata",
                principalColumn: "ConnectBeatmapSetID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beatmaps_TrackMetadata_TrackMetadataConnectBeatmapSetID",
                table: "Beatmaps");

            migrationBuilder.DropForeignKey(
                name: "FK_BeatmapSets_TrackMetadata_TrackMetadataConnectBeatmapSetID",
                table: "BeatmapSets");

            migrationBuilder.DropIndex(
                name: "IX_BeatmapSets_TrackMetadataConnectBeatmapSetID",
                table: "BeatmapSets");

            migrationBuilder.DropColumn(
                name: "TrackMetadataConnectBeatmapSetID",
                table: "BeatmapSets");

            migrationBuilder.RenameColumn(
                name: "ConnectBeatmapSetID",
                table: "TrackMetadata",
                newName: "BeatmapSetID");

            migrationBuilder.RenameColumn(
                name: "TrackMetadataConnectBeatmapSetID",
                table: "Beatmaps",
                newName: "TrackMetadataBeatmapSetID");

            migrationBuilder.RenameIndex(
                name: "IX_Beatmaps_TrackMetadataConnectBeatmapSetID",
                table: "Beatmaps",
                newName: "IX_Beatmaps_TrackMetadataBeatmapSetID");

            migrationBuilder.AlterColumn<int>(
                name: "BeatmapSetID",
                table: "TrackMetadata",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_Beatmaps_TrackMetadata_TrackMetadataBeatmapSetID",
                table: "Beatmaps",
                column: "TrackMetadataBeatmapSetID",
                principalTable: "TrackMetadata",
                principalColumn: "BeatmapSetID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrackMetadata_BeatmapSets_BeatmapSetID",
                table: "TrackMetadata",
                column: "BeatmapSetID",
                principalTable: "BeatmapSets",
                principalColumn: "BeatmapSetID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
