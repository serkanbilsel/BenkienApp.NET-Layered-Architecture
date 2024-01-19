using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenkienApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class yeniseylerajaxfln : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductImageToDelete",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImageToDelete", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImageToDelete_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "CreatedAt", "UpdatedAt", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 27, 11, 58, 12, 97, DateTimeKind.Local).AddTicks(5552), new DateTime(2023, 12, 27, 8, 58, 12, 97, DateTimeKind.Utc).AddTicks(5548), new DateTime(2023, 12, 27, 8, 58, 12, 97, DateTimeKind.Utc).AddTicks(5548), new Guid("2fa9c6e1-3de7-49dd-a620-c80d79513f3e") });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImageToDelete_ProductId",
                table: "ProductImageToDelete",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImageToDelete");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "CreatedAt", "UpdatedAt", "UserGuid" },
                values: new object[] { new DateTime(2023, 12, 25, 20, 16, 58, 451, DateTimeKind.Local).AddTicks(2746), new DateTime(2023, 12, 25, 17, 16, 58, 451, DateTimeKind.Utc).AddTicks(2741), new DateTime(2023, 12, 25, 17, 16, 58, 451, DateTimeKind.Utc).AddTicks(2742), new Guid("ce532cc7-94cf-4352-8f91-652e6b607068") });
        }
    }
}
