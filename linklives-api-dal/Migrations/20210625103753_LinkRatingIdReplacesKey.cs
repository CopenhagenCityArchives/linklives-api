using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace linklives_api_dal.Migrations
{
    public partial class LinkRatingIdReplacesKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkRatings_Links_LinkKey",
                table: "LinkRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LinkRatings",
                table: "LinkRatings");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "LinkRatings");

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "LinkRatings",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LinkKey",
                table: "LinkRatings",
                type: "varchar(767)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(767)",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_LinkRatings_Links_LinkKey",
                table: "LinkRatings",
                column: "LinkKey",
                principalTable: "Links",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LinkRatings_Links_LinkKey",
                table: "LinkRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LinkRatings",
                table: "LinkRatings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LinkRatings");

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "LinkRatings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LinkKey",
                table: "LinkRatings",
                type: "varchar(767)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(767)");

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

            migrationBuilder.AddForeignKey(
                name: "FK_LinkRatings_Links_LinkKey",
                table: "LinkRatings",
                column: "LinkKey",
                principalTable: "Links",
                principalColumn: "Key",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
