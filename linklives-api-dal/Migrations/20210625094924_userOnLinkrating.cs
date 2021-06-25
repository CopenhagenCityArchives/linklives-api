using Microsoft.EntityFrameworkCore.Migrations;

namespace linklives_api_dal.Migrations
{
    public partial class userOnLinkrating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "LinkRatings",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User",
                table: "LinkRatings");
        }
    }
}
