using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedGeoTagDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TOS",
                table: "Settlement",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "SettlementRemarks",
                table: "Settlement",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "GeoTagDetails",
                type: "varchar(32)",
                maxLength: 32,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TransactionSource",
                table: "GeoTagDetails",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 9, 16, 57, 24, 19, DateTimeKind.Unspecified).AddTicks(4128), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 9, 16, 57, 24, 19, DateTimeKind.Unspecified).AddTicks(4326), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 9, 16, 57, 24, 19, DateTimeKind.Unspecified).AddTicks(4406), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 9, 16, 57, 24, 19, DateTimeKind.Unspecified).AddTicks(4408), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 9, 16, 57, 24, 19, DateTimeKind.Unspecified).AddTicks(4392), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 9, 16, 57, 24, 19, DateTimeKind.Unspecified).AddTicks(4395), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 9, 16, 57, 24, 19, DateTimeKind.Unspecified).AddTicks(4419), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 9, 16, 57, 24, 19, DateTimeKind.Unspecified).AddTicks(4422), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_GeoTagDetails_AccountId",
                table: "GeoTagDetails",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeoTagDetails_LoanAccounts_AccountId",
                table: "GeoTagDetails",
                column: "AccountId",
                principalTable: "LoanAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeoTagDetails_LoanAccounts_AccountId",
                table: "GeoTagDetails");

            migrationBuilder.DropIndex(
                name: "IX_GeoTagDetails_AccountId",
                table: "GeoTagDetails");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "GeoTagDetails");

            migrationBuilder.DropColumn(
                name: "TransactionSource",
                table: "GeoTagDetails");

            migrationBuilder.UpdateData(
                table: "Settlement",
                keyColumn: "TOS",
                keyValue: null,
                column: "TOS",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TOS",
                table: "Settlement",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Settlement",
                keyColumn: "SettlementRemarks",
                keyValue: null,
                column: "SettlementRemarks",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "SettlementRemarks",
                table: "Settlement",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 2, 16, 56, 37, 21, DateTimeKind.Unspecified).AddTicks(1157), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 2, 16, 56, 37, 21, DateTimeKind.Unspecified).AddTicks(1234), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 2, 16, 56, 37, 21, DateTimeKind.Unspecified).AddTicks(1254), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 2, 16, 56, 37, 21, DateTimeKind.Unspecified).AddTicks(1256), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 2, 16, 56, 37, 21, DateTimeKind.Unspecified).AddTicks(1246), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 2, 16, 56, 37, 21, DateTimeKind.Unspecified).AddTicks(1248), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 2, 16, 56, 37, 21, DateTimeKind.Unspecified).AddTicks(1281), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 2, 16, 56, 37, 21, DateTimeKind.Unspecified).AddTicks(1283), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
