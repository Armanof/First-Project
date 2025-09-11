using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bulky_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeveloperColumnToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeveloper",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValueSql: "0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeveloper",
                table: "Users");
        }
    }
}
