using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class LatestAllocationDateFieldAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LatestAllocationDate",
                table: "LoanAccounts",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 3, 16, 43, 33, 32, DateTimeKind.Unspecified).AddTicks(8551), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 3, 16, 43, 33, 32, DateTimeKind.Unspecified).AddTicks(8592), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 3, 16, 43, 33, 32, DateTimeKind.Unspecified).AddTicks(8621), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 3, 16, 43, 33, 32, DateTimeKind.Unspecified).AddTicks(8623), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 3, 16, 43, 33, 32, DateTimeKind.Unspecified).AddTicks(8612), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 3, 16, 43, 33, 32, DateTimeKind.Unspecified).AddTicks(8614), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 3, 16, 43, 33, 32, DateTimeKind.Unspecified).AddTicks(8631), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 3, 16, 43, 33, 32, DateTimeKind.Unspecified).AddTicks(8633), new TimeSpan(0, 5, 30, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LatestAllocationDate",
                table: "LoanAccounts");

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
    }
}
