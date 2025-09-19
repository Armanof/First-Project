using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bulky_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEndPointTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionEndPoints_Permissions_F_PermissionId",
                table: "PermissionEndPoints");

            migrationBuilder.DropColumn(
                name: "ActionName",
                table: "PermissionEndPoints");

            migrationBuilder.DropColumn(
                name: "ControllerName",
                table: "PermissionEndPoints");

            migrationBuilder.DropColumn(
                name: "EndPointName",
                table: "PermissionEndPoints");

            migrationBuilder.DropColumn(
                name: "HttpMethod",
                table: "PermissionEndPoints");

            migrationBuilder.AddColumn<Guid>(
                name: "F_EndPointID",
                table: "PermissionEndPoints",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EndPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    EndPointName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ControllerName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ActionName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    HttpMethod = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    InsertedIP = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    InsertedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    InsertedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedIP = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    UpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndPoints", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionEndPoints_F_EndPointID",
                table: "PermissionEndPoints",
                column: "F_EndPointID");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionEndPoints_EndPoints_F_EndPointID",
                table: "PermissionEndPoints",
                column: "F_EndPointID",
                principalTable: "EndPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionEndPoints_Permissions_F_PermissionId",
                table: "PermissionEndPoints",
                column: "F_PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionEndPoints_EndPoints_F_EndPointID",
                table: "PermissionEndPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionEndPoints_Permissions_F_PermissionId",
                table: "PermissionEndPoints");

            migrationBuilder.DropTable(
                name: "EndPoints");

            migrationBuilder.DropIndex(
                name: "IX_PermissionEndPoints_F_EndPointID",
                table: "PermissionEndPoints");

            migrationBuilder.DropColumn(
                name: "F_EndPointID",
                table: "PermissionEndPoints");

            migrationBuilder.AddColumn<string>(
                name: "ActionName",
                table: "PermissionEndPoints",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ControllerName",
                table: "PermissionEndPoints",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EndPointName",
                table: "PermissionEndPoints",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HttpMethod",
                table: "PermissionEndPoints",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionEndPoints_Permissions_F_PermissionId",
                table: "PermissionEndPoints",
                column: "F_PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
