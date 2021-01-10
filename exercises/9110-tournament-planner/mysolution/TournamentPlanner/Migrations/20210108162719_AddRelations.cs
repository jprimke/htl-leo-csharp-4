using Microsoft.EntityFrameworkCore.Migrations;

namespace TournamentPlanner.Migrations
{
    public partial class AddRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_Player1ID",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_Player2ID",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_WinnerID",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Players",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "WinnerID",
                table: "Matches",
                newName: "WinnerId");

            migrationBuilder.RenameColumn(
                name: "Player2ID",
                table: "Matches",
                newName: "Player2Id");

            migrationBuilder.RenameColumn(
                name: "Player1ID",
                table: "Matches",
                newName: "Player1Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Matches",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_WinnerID",
                table: "Matches",
                newName: "IX_Matches_WinnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_Player2ID",
                table: "Matches",
                newName: "IX_Matches_Player2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_Player1ID",
                table: "Matches",
                newName: "IX_Matches_Player1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_Player1Id",
                table: "Matches",
                column: "Player1Id",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_Player2Id",
                table: "Matches",
                column: "Player2Id",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_WinnerId",
                table: "Matches",
                column: "WinnerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_Player1Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_Player2Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_WinnerId",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Players",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "WinnerId",
                table: "Matches",
                newName: "WinnerID");

            migrationBuilder.RenameColumn(
                name: "Player2Id",
                table: "Matches",
                newName: "Player2ID");

            migrationBuilder.RenameColumn(
                name: "Player1Id",
                table: "Matches",
                newName: "Player1ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Matches",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_WinnerId",
                table: "Matches",
                newName: "IX_Matches_WinnerID");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_Player2Id",
                table: "Matches",
                newName: "IX_Matches_Player2ID");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_Player1Id",
                table: "Matches",
                newName: "IX_Matches_Player1ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_Player1ID",
                table: "Matches",
                column: "Player1ID",
                principalTable: "Players",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_Player2ID",
                table: "Matches",
                column: "Player2ID",
                principalTable: "Players",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_WinnerID",
                table: "Matches",
                column: "WinnerID",
                principalTable: "Players",
                principalColumn: "ID");
        }
    }
}
