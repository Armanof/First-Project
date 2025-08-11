using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bulky_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeSomeColumnOOfProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price100",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price50",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ListPrice",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price100",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price50",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3bd11b1f-d805-4564-961a-f5a69ebb490b"),
                columns: new[] { "ListPrice", "Price100", "Price50" },
                values: new object[] { 49.990000000000002, 34.990000000000002, 39.990000000000002 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4c53d0b3-09a9-42c4-a569-74bdcf7d7640"),
                columns: new[] { "ListPrice", "Price100", "Price50" },
                values: new object[] { 79.989999999999995, 59.990000000000002, 65.989999999999995 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b824129f-3925-4de0-a014-9ea5535591d8"),
                columns: new[] { "ListPrice", "Price100", "Price50" },
                values: new object[] { 99.989999999999995, 79.989999999999995, 85.989999999999995 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e57d5e7f-0148-4f10-81e2-8051faebffa8"),
                columns: new[] { "ListPrice", "Price100", "Price50" },
                values: new object[] { 89.989999999999995, 69.989999999999995, 74.989999999999995 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e82a1c1b-0f1c-4222-94fb-50f1b5b46e12"),
                columns: new[] { "ListPrice", "Price100", "Price50" },
                values: new object[] { 59.990000000000002, 39.990000000000002, 45.990000000000002 });
        }
    }
}
