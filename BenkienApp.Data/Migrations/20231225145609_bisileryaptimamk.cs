using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenkienApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class bisileryaptimamk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "CreatedAt", "UpdatedAt", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 25, 17, 56, 9, 769, DateTimeKind.Local).AddTicks(8622), new DateTime(2023, 12, 25, 14, 56, 9, 769, DateTimeKind.Utc).AddTicks(8616), new DateTime(2023, 12, 25, 14, 56, 9, 769, DateTimeKind.Utc).AddTicks(8616), new Guid("c520b10d-0b68-4aec-9594-3005f9fcc168") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "CreatedAt", "UpdatedAt", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 25, 16, 45, 58, 180, DateTimeKind.Local).AddTicks(5086), new DateTime(2023, 12, 25, 13, 45, 58, 180, DateTimeKind.Utc).AddTicks(5081), new DateTime(2023, 12, 25, 13, 45, 58, 180, DateTimeKind.Utc).AddTicks(5081), new Guid("b8148de1-fa25-4a7f-8370-0672996a21db") });
        }
    }
}
