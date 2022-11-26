using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOMAIN.Migrations
{
    public partial class initDb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobTable_JobType_JobTypeID_JOB_TYPE",
                table: "JobTable");

            migrationBuilder.DropColumn(
                name: "ID_JOB_TYPE",
                table: "JobTable");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "JobTable");

            migrationBuilder.RenameColumn(
                name: "JobTypeID_JOB_TYPE",
                table: "JobTable",
                newName: "JobTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_JobTable_JobTypeID_JOB_TYPE",
                table: "JobTable",
                newName: "IX_JobTable_JobTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTable_JobType_JobTypeId",
                table: "JobTable",
                column: "JobTypeId",
                principalTable: "JobType",
                principalColumn: "ID_JOB_TYPE",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobTable_JobType_JobTypeId",
                table: "JobTable");

            migrationBuilder.RenameColumn(
                name: "JobTypeId",
                table: "JobTable",
                newName: "JobTypeID_JOB_TYPE");

            migrationBuilder.RenameIndex(
                name: "IX_JobTable_JobTypeId",
                table: "JobTable",
                newName: "IX_JobTable_JobTypeID_JOB_TYPE");

            migrationBuilder.AddColumn<int>(
                name: "ID_JOB_TYPE",
                table: "JobTable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "JobTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_JobTable_JobType_JobTypeID_JOB_TYPE",
                table: "JobTable",
                column: "JobTypeID_JOB_TYPE",
                principalTable: "JobType",
                principalColumn: "ID_JOB_TYPE",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
