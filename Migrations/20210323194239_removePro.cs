using Microsoft.EntityFrameworkCore.Migrations;

namespace Academic_project_manager_WebAPI.Migrations
{
    public partial class removePro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "groupId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "groups",
                columns: table => new
                {
                    groupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    groupName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groups", x => x.groupId);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    projectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    projectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    projectRef = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.projectId);
                    table.ForeignKey(
                        name: "FK_Project_groups_projectRef",
                        column: x => x.projectRef,
                        principalTable: "groups",
                        principalColumn: "groupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_groupId",
                table: "Students",
                column: "groupId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_projectRef",
                table: "Project",
                column: "projectRef",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_groups_groupId",
                table: "Students",
                column: "groupId",
                principalTable: "groups",
                principalColumn: "groupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_groups_groupId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "groups");

            migrationBuilder.DropIndex(
                name: "IX_Students_groupId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "groupId",
                table: "Students");
        }
    }
}
