using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class RemovedDirectWalletFieldsFromAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsedWallet",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "WalletLastUpdatedDate",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "WalletLimit",
                table: "ApplicationUser");

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 13, 56, 36, 328, DateTimeKind.Unspecified).AddTicks(9815), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 13, 56, 36, 328, DateTimeKind.Unspecified).AddTicks(9858), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 13, 56, 36, 328, DateTimeKind.Unspecified).AddTicks(9886), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 13, 56, 36, 328, DateTimeKind.Unspecified).AddTicks(9889), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 13, 56, 36, 328, DateTimeKind.Unspecified).AddTicks(9875), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 13, 56, 36, 328, DateTimeKind.Unspecified).AddTicks(9877), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 13, 56, 36, 328, DateTimeKind.Unspecified).AddTicks(9898), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 13, 56, 36, 328, DateTimeKind.Unspecified).AddTicks(9901), new TimeSpan(0, 5, 30, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "UsedWallet",
                table: "ApplicationUser",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "WalletLastUpdatedDate",
                table: "ApplicationUser",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WalletLimit",
                table: "ApplicationUser",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 13, 46, 56, 128, DateTimeKind.Unspecified).AddTicks(6770), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 13, 46, 56, 128, DateTimeKind.Unspecified).AddTicks(6818), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 13, 46, 56, 128, DateTimeKind.Unspecified).AddTicks(6869), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 13, 46, 56, 128, DateTimeKind.Unspecified).AddTicks(6872), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 13, 46, 56, 128, DateTimeKind.Unspecified).AddTicks(6851), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 13, 46, 56, 128, DateTimeKind.Unspecified).AddTicks(6854), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 13, 46, 56, 128, DateTimeKind.Unspecified).AddTicks(7132), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 13, 46, 56, 128, DateTimeKind.Unspecified).AddTicks(7138), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
