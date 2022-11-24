using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOMAIN.Migrations
{
    public partial class JobTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "JobTable");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "JobTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
