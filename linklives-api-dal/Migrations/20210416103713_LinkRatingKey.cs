using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace linklives_api_dal.Migrations
{
    public partial class LinkRatingKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LinkRatings",
                table: "LinkRatings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LinkRatings");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "LinkRatings",
                type: "varchar(767)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LinkRatings",
                table: "LinkRatings",
                column: "Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LinkRatings",
                table: "LinkRatings");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "LinkRatings");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "LinkRatings",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LinkRatings",
                table: "LinkRatings",
                column: "Id");
        }
    }
}
