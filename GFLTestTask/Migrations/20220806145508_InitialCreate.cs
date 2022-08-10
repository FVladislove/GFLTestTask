using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GFLTestTask.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileSystemModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileSystemModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileSystemModel_FileSystemModel_ParentId",
                        column: x => x.ParentId,
                        principalTable: "FileSystemModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileSystemModel_ParentId",
                table: "FileSystemModel",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileSystemModel");
        }
    }
}
