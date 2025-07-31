using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedLengthOfCollectionPanNo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "yPANNo",
                table: "Collections",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 15, 11, 22, 12, 603, DateTimeKind.Unspecified).AddTicks(1260), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 15, 11, 22, 12, 603, DateTimeKind.Unspecified).AddTicks(1289), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 15, 11, 22, 12, 603, DateTimeKind.Unspecified).AddTicks(1304), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 15, 11, 22, 12, 603, DateTimeKind.Unspecified).AddTicks(1306), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 15, 11, 22, 12, 603, DateTimeKind.Unspecified).AddTicks(1298), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 15, 11, 22, 12, 603, DateTimeKind.Unspecified).AddTicks(1299), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 15, 11, 22, 12, 603, DateTimeKind.Unspecified).AddTicks(1312), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 15, 11, 22, 12, 603, DateTimeKind.Unspecified).AddTicks(1313), new TimeSpan(0, 5, 30, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "yPANNo",
                table: "Collections",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified).AddTicks(90), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified).AddTicks(150), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified).AddTicks(184), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified).AddTicks(187), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified).AddTicks(166), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified).AddTicks(168), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified).AddTicks(200), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified).AddTicks(202), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
