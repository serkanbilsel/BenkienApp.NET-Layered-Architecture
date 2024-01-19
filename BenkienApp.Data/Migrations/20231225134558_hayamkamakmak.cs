using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenkienApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class hayamkamakmak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "CreatedAt", "UpdatedAt", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 25, 16, 45, 58, 180, DateTimeKind.Local).AddTicks(5086), new DateTime(2023, 12, 25, 13, 45, 58, 180, DateTimeKind.Utc).AddTicks(5081), new DateTime(2023, 12, 25, 13, 45, 58, 180, DateTimeKind.Utc).AddTicks(5081), new Guid("b8148de1-fa25-4a7f-8370-0672996a21db") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "CreatedAt", "UpdatedAt", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 25, 16, 29, 56, 275, DateTimeKind.Local).AddTicks(474), new DateTime(2023, 12, 25, 13, 29, 56, 275, DateTimeKind.Utc).AddTicks(468), new DateTime(2023, 12, 25, 13, 29, 56, 275, DateTimeKind.Utc).AddTicks(469), new Guid("e9d6e2e5-ff0b-473b-9389-d8ce8373f381") });
        }
    }
}
