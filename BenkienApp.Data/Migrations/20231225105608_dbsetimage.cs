using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenkienApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class dbsetimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "ProductImages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "CreatedAt", "UpdatedAt", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 25, 13, 56, 8, 495, DateTimeKind.Local).AddTicks(9307), new DateTime(2023, 12, 25, 10, 56, 8, 495, DateTimeKind.Utc).AddTicks(9302), new DateTime(2023, 12, 25, 10, 56, 8, 495, DateTimeKind.Utc).AddTicks(9302), new Guid("5c1670fc-9ce9-4885-a75d-6210d5ee3a68") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ImageUrl",
                table: "ProductImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "CreatedAt", "UpdatedAt", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 22, 21, 30, 21, 439, DateTimeKind.Local).AddTicks(66), new DateTime(2023, 12, 22, 18, 30, 21, 439, DateTimeKind.Utc).AddTicks(61), new DateTime(2023, 12, 22, 18, 30, 21, 439, DateTimeKind.Utc).AddTicks(62), new Guid("440defbd-d58e-427b-9309-dc6c7fbfc649") });
        }
    }
}
