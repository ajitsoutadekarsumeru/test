using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedPermissionScheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Permissions");

            migrationBuilder.RenameColumn(
                name: "PermissionName",
                table: "Permissions",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Permissions",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Section",
                table: "Permissions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PermissionSchemeId",
                table: "Designation",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "ApplicationUser",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PermissionSchemeChangeLog",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PermissionSchemeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedPermissions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemovedPermissions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionSchemeChangeLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionSchemes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionSchemes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnabledPermission",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PermissionId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    PermissionSchemeId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnabledPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnabledPermission_PermissionSchemes_PermissionSchemeId",
                        column: x => x.PermissionSchemeId,
                        principalTable: "PermissionSchemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnabledPermission_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 13, 10, 33, 52, 877, DateTimeKind.Unspecified).AddTicks(1589), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 13, 10, 33, 52, 877, DateTimeKind.Unspecified).AddTicks(1682), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 13, 10, 33, 52, 877, DateTimeKind.Unspecified).AddTicks(1712), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 13, 10, 33, 52, 877, DateTimeKind.Unspecified).AddTicks(1714), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 13, 10, 33, 52, 877, DateTimeKind.Unspecified).AddTicks(1703), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 13, 10, 33, 52, 877, DateTimeKind.Unspecified).AddTicks(1705), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 13, 10, 33, 52, 877, DateTimeKind.Unspecified).AddTicks(1752), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 13, 10, 33, 52, 877, DateTimeKind.Unspecified).AddTicks(1754), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_Designation_PermissionSchemeId",
                table: "Designation",
                column: "PermissionSchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnabledPermission_PermissionId",
                table: "EnabledPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_EnabledPermission_PermissionSchemeId",
                table: "EnabledPermission",
                column: "PermissionSchemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Designation_PermissionSchemes_PermissionSchemeId",
                table: "Designation",
                column: "PermissionSchemeId",
                principalTable: "PermissionSchemes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Designation_PermissionSchemes_PermissionSchemeId",
                table: "Designation");

            migrationBuilder.DropTable(
                name: "EnabledPermission");

            migrationBuilder.DropTable(
                name: "PermissionSchemeChangeLog");

            migrationBuilder.DropTable(
                name: "PermissionSchemes");

            migrationBuilder.DropIndex(
                name: "IX_Designation_PermissionSchemeId",
                table: "Designation");

            migrationBuilder.DropColumn(
                name: "Section",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "PermissionSchemeId",
                table: "Designation");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "ApplicationUser");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Permissions",
                newName: "PermissionName");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Permissions",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Permissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 6, 13, 58, 56, 136, DateTimeKind.Unspecified).AddTicks(8604), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 6, 13, 58, 56, 136, DateTimeKind.Unspecified).AddTicks(8635), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 6, 13, 58, 56, 136, DateTimeKind.Unspecified).AddTicks(8651), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 6, 13, 58, 56, 136, DateTimeKind.Unspecified).AddTicks(8653), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 6, 13, 58, 56, 136, DateTimeKind.Unspecified).AddTicks(8644), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 6, 13, 58, 56, 136, DateTimeKind.Unspecified).AddTicks(8645), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 6, 13, 58, 56, 136, DateTimeKind.Unspecified).AddTicks(8668), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 6, 13, 58, 56, 136, DateTimeKind.Unspecified).AddTicks(8670), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
