using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulky_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    InsertedIP = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    InsertedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    InsertedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedIP = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    F_CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InsertedIP = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    InsertedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    InsertedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedIP = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_F_CategoryId",
                        column: x => x.F_CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "InsertedIP", "InsertedUserID", "Name", "UpdatedDateTime", "UpdatedIP", "UpdatedUserID" },
                values: new object[,]
                {
                    { new Guid("33224a6d-aefe-442e-8afd-00335258c001"), 0, null, null, "Fantasy", null, null, null },
                    { new Guid("7a687151-1b28-4217-b1dd-89bac7f8be86"), 0, null, null, "Sci-Fi", null, null, null },
                    { new Guid("86e47e6e-03db-4e6a-bea3-a17cc6fa3408"), 0, null, null, "Romance", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Description", "F_CategoryId", "ISBN", "ImageUrl", "InsertedIP", "InsertedUserID", "ListPrice", "Price", "Price100", "Price50", "Title", "UpdatedDateTime", "UpdatedIP", "UpdatedUserID" },
                values: new object[,]
                {
                    { new Guid("3bd11b1f-d805-4564-961a-f5a69ebb490b"), "Robert Martin", "Principles of software craftsmanship", null, "978-3216549870", null, null, null, 49.990000000000002, 42.990000000000002, 34.990000000000002, 39.990000000000002, "Clean Code", null, null, null },
                    { new Guid("4c53d0b3-09a9-42c4-a569-74bdcf7d7640"), "Jane Smith", "Building scalable web applications", null, "978-0987654321", null, null, null, 79.989999999999995, 69.989999999999995, 59.990000000000002, 65.989999999999995, "ASP.NET Core Guide", null, null, null },
                    { new Guid("b824129f-3925-4de0-a014-9ea5535591d8"), "Sarah Lee", "Hands-on ML algorithms and data science", null, "978-6789012345", null, null, null, 99.989999999999995, 89.989999999999995, 79.989999999999995, 85.989999999999995, "Machine Learning with Python", null, null, null },
                    { new Guid("e57d5e7f-0148-4f10-81e2-8051faebffa8"), "Michael Chen", "Modern distributed system design", null, "978-5678901234", null, null, null, 89.989999999999995, 79.989999999999995, 69.989999999999995, 74.989999999999995, "Introduction to Microservices", null, null, null },
                    { new Guid("e82a1c1b-0f1c-4222-94fb-50f1b5b46e12"), "John Doe", "Advanced C# techniques", null, "978-1234567890", null, null, null, 59.990000000000002, 49.990000000000002, 39.990000000000002, 45.990000000000002, "C# Programming", null, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_F_CategoryId",
                table: "Products",
                column: "F_CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
