using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace linklives_api_dal.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LifeCourses",
                columns: table => new
                {
                    Key = table.Column<string>(type: "varchar(767)", nullable: false),
                    Life_course_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeCourses", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    Key = table.Column<string>(type: "varchar(767)", nullable: false),
                    Link_id = table.Column<int>(type: "int", nullable: false),
                    Iteration = table.Column<int>(type: "int", nullable: false),
                    Iteration_inner = table.Column<int>(type: "int", nullable: false),
                    Method_id = table.Column<int>(type: "int", nullable: false),
                    Pa_id1 = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<double>(type: "double", nullable: false),
                    Pa_id2 = table.Column<int>(type: "int", nullable: false),
                    Source_id1 = table.Column<int>(type: "int", nullable: false),
                    Source_id2 = table.Column<int>(type: "int", nullable: false),
                    Method_type = table.Column<string>(type: "text", nullable: true),
                    Method_subtype1 = table.Column<string>(type: "text", nullable: true),
                    Method_description = table.Column<string>(type: "text", nullable: true),
                    LifeCourseKey = table.Column<string>(type: "varchar(767)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Links_LifeCourses_LifeCourseKey",
                        column: x => x.LifeCourseKey,
                        principalTable: "LifeCourses",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LinkRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Rating = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Description = table.Column<int>(type: "int", nullable: false),
                    LinkKey = table.Column<string>(type: "varchar(767)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkRatings_Links_LinkKey",
                        column: x => x.LinkKey,
                        principalTable: "Links",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkRatings_LinkKey",
                table: "LinkRatings",
                column: "LinkKey");

            migrationBuilder.CreateIndex(
                name: "IX_Links_LifeCourseKey",
                table: "Links",
                column: "LifeCourseKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkRatings");

            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "LifeCourses");
        }
    }
}
