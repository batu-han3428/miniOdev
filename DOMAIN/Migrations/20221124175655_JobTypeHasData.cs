using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOMAIN.Migrations
{
    public partial class JobTypeHasData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "JobType",
                columns: new[] { "ID_JOB_TYPE", "JOB_TYPE_NAME" },
                values: new object[] { 1, "Günlük" });

            migrationBuilder.InsertData(
                table: "JobType",
                columns: new[] { "ID_JOB_TYPE", "JOB_TYPE_NAME" },
                values: new object[] { 2, "Haftalık" });

            migrationBuilder.InsertData(
                table: "JobType",
                columns: new[] { "ID_JOB_TYPE", "JOB_TYPE_NAME" },
                values: new object[] { 3, "Aylık" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JobType",
                keyColumn: "ID_JOB_TYPE",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobType",
                keyColumn: "ID_JOB_TYPE",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "JobType",
                keyColumn: "ID_JOB_TYPE",
                keyValue: 3);
        }
    }
}
