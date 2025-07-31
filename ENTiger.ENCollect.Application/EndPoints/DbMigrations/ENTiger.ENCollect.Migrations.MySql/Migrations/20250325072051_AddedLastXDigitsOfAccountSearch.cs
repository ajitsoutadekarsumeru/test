using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class AddedLastXDigitsOfAccountSearch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastXDigitsOfAgreementId",
                table: "LoanAccounts");

            migrationBuilder.DropColumn(
                name: "LastXDigitsOfPrimaryCard",
                table: "LoanAccounts");

            migrationBuilder.AddColumn<string>(
                name: "ReverseOfAgreementId",
                table: "LoanAccounts",
                type: "varchar(255)",
                nullable: true,
                computedColumnSql: "REVERSE(AgreementId)",
                stored: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ReverseOfPrimaryCard",
                table: "LoanAccounts",
                type: "varchar(255)",
                nullable: true,
                computedColumnSql: "REVERSE(PRIMARY_CARD_NUMBER)",
                stored: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 20, 48, 505, DateTimeKind.Unspecified).AddTicks(5023), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 20, 48, 505, DateTimeKind.Unspecified).AddTicks(5056), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 20, 48, 505, DateTimeKind.Unspecified).AddTicks(5067), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 20, 48, 505, DateTimeKind.Unspecified).AddTicks(5068), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 20, 48, 505, DateTimeKind.Unspecified).AddTicks(5062), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 20, 48, 505, DateTimeKind.Unspecified).AddTicks(5063), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 20, 48, 505, DateTimeKind.Unspecified).AddTicks(5073), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 20, 48, 505, DateTimeKind.Unspecified).AddTicks(5073), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_ReverseOfAgreementId",
                table: "LoanAccounts",
                column: "ReverseOfAgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_ReverseOfPrimaryCard",
                table: "LoanAccounts",
                column: "ReverseOfPrimaryCard");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_ReverseOfAgreementId",
                table: "LoanAccounts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_ReverseOfPrimaryCard",
                table: "LoanAccounts");

            migrationBuilder.DropColumn(
                name: "ReverseOfAgreementId",
                table: "LoanAccounts");

            migrationBuilder.DropColumn(
                name: "ReverseOfPrimaryCard",
                table: "LoanAccounts");

            migrationBuilder.AddColumn<string>(
                name: "LastXDigitsOfAgreementId",
                table: "LoanAccounts",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LastXDigitsOfPrimaryCard",
                table: "LoanAccounts",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 18, 14, 15, 38, 787, DateTimeKind.Unspecified).AddTicks(3580), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 18, 14, 15, 38, 787, DateTimeKind.Unspecified).AddTicks(3635), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 18, 14, 15, 38, 787, DateTimeKind.Unspecified).AddTicks(3653), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 18, 14, 15, 38, 787, DateTimeKind.Unspecified).AddTicks(3655), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 18, 14, 15, 38, 787, DateTimeKind.Unspecified).AddTicks(3646), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 18, 14, 15, 38, 787, DateTimeKind.Unspecified).AddTicks(3648), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 18, 14, 15, 38, 787, DateTimeKind.Unspecified).AddTicks(3684), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 18, 14, 15, 38, 787, DateTimeKind.Unspecified).AddTicks(3685), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
