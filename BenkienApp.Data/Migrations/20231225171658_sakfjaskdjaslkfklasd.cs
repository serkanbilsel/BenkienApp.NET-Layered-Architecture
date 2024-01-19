using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenkienApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class sakfjaskdjaslkfklasd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "CreatedAt", "UpdatedAt", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 25, 20, 16, 58, 451, DateTimeKind.Local).AddTicks(2746), new DateTime(2023, 12, 25, 17, 16, 58, 451, DateTimeKind.Utc).AddTicks(2741), new DateTime(2023, 12, 25, 17, 16, 58, 451, DateTimeKind.Utc).AddTicks(2742), new Guid("ce532cc7-94cf-4352-8f91-652e6b607068") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "CreatedAt", "UpdatedAt", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 25, 17, 56, 9, 769, DateTimeKind.Local).AddTicks(8622), new DateTime(2023, 12, 25, 14, 56, 9, 769, DateTimeKind.Utc).AddTicks(8616), new DateTime(2023, 12, 25, 14, 56, 9, 769, DateTimeKind.Utc).AddTicks(8616), new Guid("c520b10d-0b68-4aec-9594-3005f9fcc168") });
        }
    }
}
