using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bulky_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValueSql: "0");

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    F_ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Permissions_F_ParentId",
                        column: x => x.F_ParentId,
                        principalTable: "Permissions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
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
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "varchar(512)", unicode: false, maxLength: 512, nullable: false),
                    SaltPassword = table.Column<string>(type: "varchar(512)", unicode: false, maxLength: 512, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true),
                    SentCode = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
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
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionEndPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    EndPointName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ControllerName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ActionName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    HttpMethod = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    F_PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_PermissionEndPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionEndPoints_Permissions_F_PermissionId",
                        column: x => x.F_PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    F_RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    F_PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_F_PermissionId",
                        column: x => x.F_PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_F_RoleId",
                        column: x => x.F_RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    F_UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    F_RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_F_RoleId",
                        column: x => x.F_RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_F_UserId",
                        column: x => x.F_UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("33224a6d-aefe-442e-8afd-00335258c001"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7a687151-1b28-4217-b1dd-89bac7f8be86"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("86e47e6e-03db-4e6a-bea3-a17cc6fa3408"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3bd11b1f-d805-4564-961a-f5a69ebb490b"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4c53d0b3-09a9-42c4-a569-74bdcf7d7640"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b824129f-3925-4de0-a014-9ea5535591d8"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e57d5e7f-0148-4f10-81e2-8051faebffa8"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e82a1c1b-0f1c-4222-94fb-50f1b5b46e12"),
                column: "IsDeleted",
                value: false);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionEndPoints_F_PermissionId",
                table: "PermissionEndPoints",
                column: "F_PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_F_ParentId",
                table: "Permissions",
                column: "F_ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_F_PermissionId",
                table: "RolePermissions",
                column: "F_PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_F_RoleId",
                table: "RolePermissions",
                column: "F_RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_F_RoleId",
                table: "UserRoles",
                column: "F_RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_F_UserId",
                table: "UserRoles",
                column: "F_UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionEndPoints");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");
        }
    }
}
