using Microsoft.EntityFrameworkCore.Migrations;

namespace Taskit_server.Migrations
{
    public partial class try9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_Teams_TeamId",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_TeamId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Role");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Role",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Role_TeamId",
                table: "Role",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Teams_TeamId",
                table: "Role",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
