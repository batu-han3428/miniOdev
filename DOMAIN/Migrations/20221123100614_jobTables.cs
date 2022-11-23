using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DOMAIN.Migrations
{
    public partial class jobTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobType",
                columns: table => new
                {
                    ID_JOB_TYPE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JOB_TYPE_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobType", x => x.ID_JOB_TYPE);
                });

            migrationBuilder.CreateTable(
                name: "JobTable",
                columns: table => new
                {
                    ID_JOB = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_JOB_TYPE = table.Column<int>(type: "int", nullable: false),
                    JobTypeID_JOB_TYPE = table.Column<int>(type: "int", nullable: false),
                    JOB_KEY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JOB_TIME = table.Column<TimeSpan>(type: "time", nullable: false),
                    DAY = table.Column<int>(type: "int", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTable", x => x.ID_JOB);
                    table.ForeignKey(
                        name: "FK_JobTable_JobType_JobTypeID_JOB_TYPE",
                        column: x => x.JobTypeID_JOB_TYPE,
                        principalTable: "JobType",
                        principalColumn: "ID_JOB_TYPE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobTable_JobTypeID_JOB_TYPE",
                table: "JobTable",
                column: "JobTypeID_JOB_TYPE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobTable");

            migrationBuilder.DropTable(
                name: "JobType");
        }
    }
}
