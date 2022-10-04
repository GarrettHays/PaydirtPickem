using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaydirtPickem.Migrations
{
    public partial class AddTeamScores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CorrectPick",
                table: "UserPicks",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AwayTeamScore",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HomeTeamScore",
                table: "Games",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectPick",
                table: "UserPicks");

            migrationBuilder.DropColumn(
                name: "AwayTeamScore",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "HomeTeamScore",
                table: "Games");
        }
    }
}
