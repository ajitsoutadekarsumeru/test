using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
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
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 15, 11, 27, 23, 533, DateTimeKind.Unspecified).AddTicks(6084), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 15, 11, 27, 23, 533, DateTimeKind.Unspecified).AddTicks(6112), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 15, 11, 27, 23, 533, DateTimeKind.Unspecified).AddTicks(6128), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 15, 11, 27, 23, 533, DateTimeKind.Unspecified).AddTicks(6129), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 15, 11, 27, 23, 533, DateTimeKind.Unspecified).AddTicks(6122), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 15, 11, 27, 23, 533, DateTimeKind.Unspecified).AddTicks(6123), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 15, 11, 27, 23, 533, DateTimeKind.Unspecified).AddTicks(6139), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 15, 11, 27, 23, 533, DateTimeKind.Unspecified).AddTicks(6140), new TimeSpan(0, 5, 30, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "yPANNo",
                table: "Collections",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 14, 15, 55, 57, 168, DateTimeKind.Unspecified).AddTicks(1495), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 14, 15, 55, 57, 168, DateTimeKind.Unspecified).AddTicks(1535), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 14, 15, 55, 57, 168, DateTimeKind.Unspecified).AddTicks(1551), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 14, 15, 55, 57, 168, DateTimeKind.Unspecified).AddTicks(1553), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 14, 15, 55, 57, 168, DateTimeKind.Unspecified).AddTicks(1543), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 14, 15, 55, 57, 168, DateTimeKind.Unspecified).AddTicks(1545), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 14, 15, 55, 57, 168, DateTimeKind.Unspecified).AddTicks(1565), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 14, 15, 55, 57, 168, DateTimeKind.Unspecified).AddTicks(1566), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
