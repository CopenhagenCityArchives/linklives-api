using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace linklives_api_dal.Migrations
{
    public partial class ratingoptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "LinkRatings");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "LinkRatings",
                newName: "RatingId");

            migrationBuilder.CreateTable(
                name: "RatingOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(type: "text", nullable: true),
                    Heading = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingOptions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkRatings_RatingId",
                table: "LinkRatings",
                column: "RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_LinkRatings_RatingOptions_RatingId",
                table: "LinkRatings",
                column: "RatingId",
                principalTable: "RatingOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkRatings_RatingOptions_RatingId",
                table: "LinkRatings");

            migrationBuilder.DropTable(
                name: "RatingOptions");

            migrationBuilder.DropIndex(
                name: "IX_LinkRatings_RatingId",
                table: "LinkRatings");

            migrationBuilder.RenameColumn(
                name: "RatingId",
                table: "LinkRatings",
                newName: "Description");

            migrationBuilder.AddColumn<bool>(
                name: "Rating",
                table: "LinkRatings",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
