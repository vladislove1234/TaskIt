using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Taskit_server.Migrations
{
    public partial class try7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_TaskColumn_TaskColumnId",
                table: "Role");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Teams_TeamId",
                table: "Role");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Teams_TeamId",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Users_AuthorId",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskColumn_Teams_TeamId",
                table: "TaskColumn");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Task_TaskId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_UserId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "Users",
                newName: "FirendsId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserId",
                table: "Users",
                newName: "IX_Users_UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_TaskId",
                table: "Users",
                newName: "IX_Users_FirendsId");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "TaskColumn",
                newName: "ColumnsId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskColumn_TeamId",
                table: "TaskColumn",
                newName: "IX_TaskColumn_ColumnsId");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Task",
                newName: "TasksId");

            migrationBuilder.RenameIndex(
                name: "IX_Task_TeamId",
                table: "Task",
                newName: "IX_Task_TasksId");

            migrationBuilder.RenameColumn(
                name: "TaskColumnId",
                table: "Role",
                newName: "RolesId");

            migrationBuilder.RenameIndex(
                name: "IX_Role_TaskColumnId",
                table: "Role",
                newName: "IX_Role_RolesId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TaskColumn",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Task",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Task",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Task",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Role",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Role",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Role",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_TaskColumn_RolesId",
                table: "Role",
                column: "RolesId",
                principalTable: "TaskColumn",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Teams_TeamId",
                table: "Role",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_TaskColumn_TasksId",
                table: "Task",
                column: "TasksId",
                principalTable: "TaskColumn",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Teams_TasksId",
                table: "Task",
                column: "TasksId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Users_AuthorId",
                table: "Task",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskColumn_Teams_ColumnsId",
                table: "TaskColumn",
                column: "ColumnsId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Task_UsersId",
                table: "Users",
                column: "UsersId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_FirendsId",
                table: "Users",
                column: "FirendsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Role_TaskColumn_RolesId",
                table: "Role");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Teams_TeamId",
                table: "Role");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_TaskColumn_TasksId",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Teams_TasksId",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Users_AuthorId",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskColumn_Teams_ColumnsId",
                table: "TaskColumn");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Task_UsersId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_FirendsId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "FirendsId",
                table: "Users",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UsersId",
                table: "Users",
                newName: "IX_Users_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_FirendsId",
                table: "Users",
                newName: "IX_Users_TaskId");

            migrationBuilder.RenameColumn(
                name: "ColumnsId",
                table: "TaskColumn",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TaskColumn_ColumnsId",
                table: "TaskColumn",
                newName: "IX_TaskColumn_TeamId");

            migrationBuilder.RenameColumn(
                name: "TasksId",
                table: "Task",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_Task_TasksId",
                table: "Task",
                newName: "IX_Task_TeamId");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "Role",
                newName: "TaskColumnId");

            migrationBuilder.RenameIndex(
                name: "IX_Role_RolesId",
                table: "Role",
                newName: "IX_Role_TaskColumnId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TaskColumn",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Task",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Task",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Task",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Role",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Role",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Role",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_TaskColumn_TaskColumnId",
                table: "Role",
                column: "TaskColumnId",
                principalTable: "TaskColumn",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Teams_TeamId",
                table: "Role",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Teams_TeamId",
                table: "Task",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Users_AuthorId",
                table: "Task",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskColumn_Teams_TeamId",
                table: "TaskColumn",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Task_TaskId",
                table: "Users",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_UserId",
                table: "Users",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
