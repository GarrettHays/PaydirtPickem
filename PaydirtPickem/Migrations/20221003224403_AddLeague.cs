using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaydirtPickem.Migrations
{
    public partial class AddLeague : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeamName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "League",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LeagueName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserLeague",
                columns: table => new
                {
                    LeaguesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserLeague", x => new { x.LeaguesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserLeague_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserLeague_League_LeaguesId",
                        column: x => x.LeaguesId,
                        principalTable: "League",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserLeague_UsersId",
                table: "ApplicationUserLeague",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserLeague");

            migrationBuilder.DropTable(
                name: "League");

            migrationBuilder.DropColumn(
                name: "TeamName",
                table: "AspNetUsers");
        }
    }
}
