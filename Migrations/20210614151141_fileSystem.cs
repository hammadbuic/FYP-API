using Microsoft.EntityFrameworkCore.Migrations;

namespace Academic_project_manager_WebAPI.Migrations
{
    public partial class fileSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentFileUpload",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    filename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    file_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    branch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    activitiesId = table.Column<int>(type: "int", nullable: true),
                    groupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentFileUpload", x => x.id);
                    table.ForeignKey(
                        name: "FK_StudentFileUpload_Activities_activitiesId",
                        column: x => x.activitiesId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentFileUpload_groups_groupId",
                        column: x => x.groupId,
                        principalTable: "groups",
                        principalColumn: "groupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentFileUpload_activitiesId",
                table: "StudentFileUpload",
                column: "activitiesId",
                unique: true,
                filter: "[activitiesId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StudentFileUpload_groupId",
                table: "StudentFileUpload",
                column: "groupId",
                unique: true,
                filter: "[groupId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentFileUpload");
        }
    }
}
