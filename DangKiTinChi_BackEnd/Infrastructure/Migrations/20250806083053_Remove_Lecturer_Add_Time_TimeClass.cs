using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Lecturer_Add_Time_TimeClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Lecturers_LecturerId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_LecturerId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Room",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Classes");

            migrationBuilder.CreateTable(
                name: "Time",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<int>(type: "int", nullable: true),
                    EndTime = table.Column<int>(type: "int", nullable: true),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Time", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeClass",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<long>(type: "bigint", nullable: true),
                    ClassesId = table.Column<long>(type: "bigint", nullable: true),
                    TimeId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeClass_Classes_ClassesId",
                        column: x => x.ClassesId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimeClass_Time_TimeId",
                        column: x => x.TimeId,
                        principalTable: "Time",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeClass_ClassesId",
                table: "TimeClass",
                column: "ClassesId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeClass_TimeId",
                table: "TimeClass",
                column: "TimeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeClass");

            migrationBuilder.DropTable(
                name: "Time");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EndTime",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LecturerId",
                table: "Classes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Room",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartTime",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_LecturerId",
                table: "Classes",
                column: "LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Lecturers_LecturerId",
                table: "Classes",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id");
        }
    }
}
