using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Academic_project_manager_WebAPI.Migrations
{
    public partial class coordinatorDataUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Coordinator",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Coordinator",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "gitId",
                table: "Coordinator",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "groupName",
                table: "Coordinator",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "groupPath",
                table: "Coordinator",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "web_url",
                table: "Coordinator",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Coordinator");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Coordinator");

            migrationBuilder.DropColumn(
                name: "gitId",
                table: "Coordinator");

            migrationBuilder.DropColumn(
                name: "groupName",
                table: "Coordinator");

            migrationBuilder.DropColumn(
                name: "groupPath",
                table: "Coordinator");

            migrationBuilder.DropColumn(
                name: "web_url",
                table: "Coordinator");
        }
    }
}
