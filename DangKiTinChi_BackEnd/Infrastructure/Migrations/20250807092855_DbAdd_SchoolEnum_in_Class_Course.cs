using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DbAdd_SchoolEnum_in_Class_Course : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DomainSchool",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "SchoolEnum",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SchoolEnum",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SchoolEnum",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: -1L,
                column: "SchoolEnum",
                value: 0);

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Cookie", "CreatedBy", "CreatedDate", "FullName", "ModifiedBy", "ModifiedDate", "Password", "SchoolEnum", "SemeterName", "UserId", "UserName", "__RequestVerificationToken" },
                values: new object[] { -2L, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyễn Thị Cẩm Thanh", null, null, "Thanhthanh@1", 1, null, -1L, "22F7510310", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: -2L);

            migrationBuilder.DropColumn(
                name: "SchoolEnum",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SchoolEnum",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "SchoolEnum",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "DomainSchool",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: -1L,
                column: "DomainSchool",
                value: "student.husc.edu.vn");
        }
    }
}
