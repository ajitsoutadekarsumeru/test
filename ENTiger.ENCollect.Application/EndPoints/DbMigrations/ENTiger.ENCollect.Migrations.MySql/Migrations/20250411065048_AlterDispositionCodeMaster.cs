using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class AlterDispositionCodeMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DispositionCodeCustomerOrAccountLevel",
                table: "DispositionCodeMaster");

            migrationBuilder.AddColumn<bool>(
                name: "DispositionCodeIsCustomerLevel",
                table: "DispositionCodeMaster",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 11, 8, 50, 46, 300, DateTimeKind.Unspecified).AddTicks(1990), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 11, 8, 50, 46, 300, DateTimeKind.Unspecified).AddTicks(2034), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 11, 8, 50, 46, 300, DateTimeKind.Unspecified).AddTicks(2047), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 11, 8, 50, 46, 300, DateTimeKind.Unspecified).AddTicks(2048), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 11, 8, 50, 46, 300, DateTimeKind.Unspecified).AddTicks(2041), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 11, 8, 50, 46, 300, DateTimeKind.Unspecified).AddTicks(2042), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 11, 8, 50, 46, 300, DateTimeKind.Unspecified).AddTicks(2053), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 11, 8, 50, 46, 300, DateTimeKind.Unspecified).AddTicks(2054), new TimeSpan(0, 2, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DispositionCodeIsCustomerLevel",
                table: "DispositionCodeMaster");

            migrationBuilder.AddColumn<string>(
                name: "DispositionCodeCustomerOrAccountLevel",
                table: "DispositionCodeMaster",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 3, 15, 15, 12, 575, DateTimeKind.Unspecified).AddTicks(7249), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 3, 15, 15, 12, 575, DateTimeKind.Unspecified).AddTicks(7284), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 3, 15, 15, 12, 575, DateTimeKind.Unspecified).AddTicks(7309), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 3, 15, 15, 12, 575, DateTimeKind.Unspecified).AddTicks(7311), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 3, 15, 15, 12, 575, DateTimeKind.Unspecified).AddTicks(7300), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 3, 15, 15, 12, 575, DateTimeKind.Unspecified).AddTicks(7302), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 3, 15, 15, 12, 575, DateTimeKind.Unspecified).AddTicks(7317), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 3, 15, 15, 12, 575, DateTimeKind.Unspecified).AddTicks(7319), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
