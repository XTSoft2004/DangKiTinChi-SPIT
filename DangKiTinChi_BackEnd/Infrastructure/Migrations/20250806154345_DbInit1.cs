using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DbInit1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeClass_Time_TimeId",
                table: "TimeClass");

            migrationBuilder.AlterColumn<long>(
                name: "TimeId",
                table: "TimeClass",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ClassId",
                table: "TimeClass",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Cookie", "CreatedBy", "CreatedDate", "DomainSchool", "FullName", "ModifiedBy", "ModifiedDate", "Password", "SemeterName", "UserId", "UserName", "__RequestVerificationToken" },
                values: new object[] { -1L, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Xuantruong23*", null, -1L, "22T1020784", null });

            migrationBuilder.AddForeignKey(
                name: "FK_TimeClass_Time_TimeId",
                table: "TimeClass",
                column: "TimeId",
                principalTable: "Time",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeClass_Time_TimeId",
                table: "TimeClass");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: -1L);

            migrationBuilder.AlterColumn<long>(
                name: "TimeId",
                table: "TimeClass",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ClassId",
                table: "TimeClass",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeClass_Time_TimeId",
                table: "TimeClass",
                column: "TimeId",
                principalTable: "Time",
                principalColumn: "Id");
        }
    }
}
