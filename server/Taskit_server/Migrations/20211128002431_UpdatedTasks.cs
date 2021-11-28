using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Taskit_server.Migrations
{
    public partial class UpdatedTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_TaskId",
                table: "Roles",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Tasks_TaskId",
                table: "Roles",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Tasks_TaskId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_TaskId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Roles");
        }
    }
}
