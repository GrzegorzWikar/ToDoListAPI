using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoAPI.Migrations
{
    /// <inheritdoc />
    public partial class newUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "PlanedDate",
                table: "ToDoTask");

            migrationBuilder.DropColumn(
                name: "PlanedTime",
                table: "ToDoTask");

            migrationBuilder.AlterColumn<int>(
                name: "Title",
                table: "ToDoTask",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "ToDoTask",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ToDoTask",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoTask_UserId",
                table: "ToDoTask",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoTask_Users_UserId",
                table: "ToDoTask",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoTask_Users_UserId",
                table: "ToDoTask");

            migrationBuilder.DropIndex(
                name: "IX_ToDoTask_UserId",
                table: "ToDoTask");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "ToDoTask");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ToDoTask");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ToDoTask",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "PlanedDate",
                table: "ToDoTask",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlanedTime",
                table: "ToDoTask",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Username", "isActive" },
                values: new object[] { 1, "System", "", "System", "System", false });
        }
    }
}
