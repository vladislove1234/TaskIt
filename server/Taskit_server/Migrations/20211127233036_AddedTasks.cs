using Microsoft.EntityFrameworkCore.Migrations;

namespace Taskit_server.Migrations
{
    public partial class AddedTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TeamMembers_TeamMemberId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_AuthorId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tasks_TaskId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TaskId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AuthorId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TeamMemberId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TeamMemberId",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TaskTeamMember",
                columns: table => new
                {
                    PerformersId = table.Column<int>(type: "int", nullable: false),
                    TasksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTeamMember", x => new { x.PerformersId, x.TasksId });
                    table.ForeignKey(
                        name: "FK_TaskTeamMember_Tasks_TasksId",
                        column: x => x.TasksId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskTeamMember_TeamMembers_PerformersId",
                        column: x => x.PerformersId,
                        principalTable: "TeamMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskTeamMember_TasksId",
                table: "TaskTeamMember",
                column: "TasksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskTeamMember");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamMemberId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TaskId",
                table: "Users",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AuthorId",
                table: "Tasks",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TeamMemberId",
                table: "Tasks",
                column: "TeamMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TeamMembers_TeamMemberId",
                table: "Tasks",
                column: "TeamMemberId",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_AuthorId",
                table: "Tasks",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Tasks_TaskId",
                table: "Users",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
