using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPermsissionScheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RemovedPermissions",
                table: "PermissionSchemeChangeLog",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "PermissionSchemeChangeLog",
                keyColumn: "Remarks",
                keyValue: null,
                column: "Remarks",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PermissionSchemeChangeLog",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "PermissionSchemeChangeLog",
                keyColumn: "PermissionSchemeId",
                keyValue: null,
                column: "PermissionSchemeId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "PermissionSchemeId",
                table: "PermissionSchemeChangeLog",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "PermissionSchemeChangeLog",
                keyColumn: "ChangeType",
                keyValue: null,
                column: "ChangeType",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ChangeType",
                table: "PermissionSchemeChangeLog",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "AddedPermissions",
                table: "PermissionSchemeChangeLog",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Name",
                keyValue: null,
                column: "Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Permissions",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 14, 9, 39, 14, 989, DateTimeKind.Unspecified).AddTicks(9366), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 14, 9, 39, 14, 989, DateTimeKind.Unspecified).AddTicks(9394), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 14, 9, 39, 14, 989, DateTimeKind.Unspecified).AddTicks(9405), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 14, 9, 39, 14, 989, DateTimeKind.Unspecified).AddTicks(9406), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 14, 9, 39, 14, 989, DateTimeKind.Unspecified).AddTicks(9401), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 14, 9, 39, 14, 989, DateTimeKind.Unspecified).AddTicks(9401), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 14, 9, 39, 14, 989, DateTimeKind.Unspecified).AddTicks(9418), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 14, 9, 39, 14, 989, DateTimeKind.Unspecified).AddTicks(9419), new TimeSpan(0, 5, 30, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RemovedPermissions",
                table: "PermissionSchemeChangeLog",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "PermissionSchemeChangeLog",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "PermissionSchemeId",
                table: "PermissionSchemeChangeLog",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldMaxLength: 32)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ChangeType",
                table: "PermissionSchemeChangeLog",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "AddedPermissions",
                table: "PermissionSchemeChangeLog",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Permissions",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 7, 22, 32, 25, 128, DateTimeKind.Unspecified).AddTicks(1528), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 7, 22, 32, 25, 128, DateTimeKind.Unspecified).AddTicks(1571), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 7, 22, 32, 25, 128, DateTimeKind.Unspecified).AddTicks(1589), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 7, 22, 32, 25, 128, DateTimeKind.Unspecified).AddTicks(1591), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 7, 22, 32, 25, 128, DateTimeKind.Unspecified).AddTicks(1581), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 7, 22, 32, 25, 128, DateTimeKind.Unspecified).AddTicks(1583), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 7, 22, 32, 25, 128, DateTimeKind.Unspecified).AddTicks(1597), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 7, 22, 32, 25, 128, DateTimeKind.Unspecified).AddTicks(1599), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
