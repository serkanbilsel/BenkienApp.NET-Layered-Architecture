using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenkienApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class amk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "CreatedAt", "UpdatedAt", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 25, 16, 29, 56, 275, DateTimeKind.Local).AddTicks(474), new DateTime(2023, 12, 25, 13, 29, 56, 275, DateTimeKind.Utc).AddTicks(468), new DateTime(2023, 12, 25, 13, 29, 56, 275, DateTimeKind.Utc).AddTicks(469), new Guid("e9d6e2e5-ff0b-473b-9389-d8ce8373f381") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "CreatedAt", "UpdatedAt", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 25, 13, 56, 8, 495, DateTimeKind.Local).AddTicks(9307), new DateTime(2023, 12, 25, 10, 56, 8, 495, DateTimeKind.Utc).AddTicks(9302), new DateTime(2023, 12, 25, 10, 56, 8, 495, DateTimeKind.Utc).AddTicks(9302), new Guid("5c1670fc-9ce9-4885-a75d-6210d5ee3a68") });
        }
    }
}
