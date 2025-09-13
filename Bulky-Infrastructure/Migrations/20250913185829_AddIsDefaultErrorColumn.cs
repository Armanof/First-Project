using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bulky_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDefaultErrorColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValueSql: "0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Roles");
        }
    }
}
