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
                    life_course_key = table.Column<string>(type: "varchar(767)", nullable: false),
                    life_course_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeCourses", x => x.life_course_key);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    link_key = table.Column<string>(type: "varchar(767)", nullable: false),
                    link_id = table.Column<int>(type: "int", nullable: false),
                    iteration = table.Column<int>(type: "int", nullable: false),
                    iteration_inner = table.Column<int>(type: "int", nullable: false),
                    method_id = table.Column<int>(type: "int", nullable: false),
                    pa_id1 = table.Column<int>(type: "int", nullable: false),
                    score = table.Column<double>(type: "double", nullable: false),
                    pa_id2 = table.Column<int>(type: "int", nullable: false),
                    source_id1 = table.Column<int>(type: "int", nullable: false),
                    source_id2 = table.Column<int>(type: "int", nullable: false),
                    method_type = table.Column<string>(type: "text", nullable: true),
                    method_subtype1 = table.Column<string>(type: "text", nullable: true),
                    method_description = table.Column<string>(type: "text", nullable: true),
                    LifeCourselife_course_key = table.Column<string>(type: "varchar(767)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.link_key);
                    table.ForeignKey(
                        name: "FK_Links_LifeCourses_LifeCourselife_course_key",
                        column: x => x.LifeCourselife_course_key,
                        principalTable: "LifeCourses",
                        principalColumn: "life_course_key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LinkRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    rating = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Description = table.Column<int>(type: "int", nullable: false),
                    link_key = table.Column<string>(type: "varchar(767)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkRatings_Links_link_key",
                        column: x => x.link_key,
                        principalTable: "Links",
                        principalColumn: "link_key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkRatings_link_key",
                table: "LinkRatings",
                column: "link_key");

            migrationBuilder.CreateIndex(
                name: "IX_Links_LifeCourselife_course_key",
                table: "Links",
                column: "LifeCourselife_course_key");
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
