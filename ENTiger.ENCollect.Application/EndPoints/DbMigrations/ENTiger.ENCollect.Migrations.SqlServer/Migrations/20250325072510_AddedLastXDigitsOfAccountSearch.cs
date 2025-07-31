using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
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
                type: "nvarchar(450)",
                nullable: true,
                computedColumnSql: "REVERSE(AgreementId)",
                stored: true);

            migrationBuilder.AddColumn<string>(
                name: "ReverseOfPrimaryCard",
                table: "LoanAccounts",
                type: "nvarchar(450)",
                nullable: true,
                computedColumnSql: "REVERSE(PRIMARY_CARD_NUMBER)",
                stored: true);

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 25, 6, 998, DateTimeKind.Unspecified).AddTicks(4127), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 25, 6, 998, DateTimeKind.Unspecified).AddTicks(4164), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 25, 6, 998, DateTimeKind.Unspecified).AddTicks(4176), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 25, 6, 998, DateTimeKind.Unspecified).AddTicks(4178), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 25, 6, 998, DateTimeKind.Unspecified).AddTicks(4170), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 25, 6, 998, DateTimeKind.Unspecified).AddTicks(4171), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 25, 6, 998, DateTimeKind.Unspecified).AddTicks(4187), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 25, 6, 998, DateTimeKind.Unspecified).AddTicks(4188), new TimeSpan(0, 2, 0, 0, 0)) });

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
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastXDigitsOfPrimaryCard",
                table: "LoanAccounts",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 18, 14, 20, 22, 694, DateTimeKind.Unspecified).AddTicks(8), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 18, 14, 20, 22, 694, DateTimeKind.Unspecified).AddTicks(59), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 18, 14, 20, 22, 694, DateTimeKind.Unspecified).AddTicks(75), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 18, 14, 20, 22, 694, DateTimeKind.Unspecified).AddTicks(76), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 18, 14, 20, 22, 694, DateTimeKind.Unspecified).AddTicks(68), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 18, 14, 20, 22, 694, DateTimeKind.Unspecified).AddTicks(69), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 18, 14, 20, 22, 694, DateTimeKind.Unspecified).AddTicks(89), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 18, 14, 20, 22, 694, DateTimeKind.Unspecified).AddTicks(91), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
