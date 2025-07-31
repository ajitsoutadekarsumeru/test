using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedCustomerConsent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerConsent",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    RequestedVisitTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsentResponseTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecureToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerConsent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerConsent_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerConsent_LoanAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "LoanAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 33, 22, 531, DateTimeKind.Unspecified).AddTicks(9426), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 33, 22, 531, DateTimeKind.Unspecified).AddTicks(9461), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 33, 22, 531, DateTimeKind.Unspecified).AddTicks(9472), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 33, 22, 531, DateTimeKind.Unspecified).AddTicks(9472), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 33, 22, 531, DateTimeKind.Unspecified).AddTicks(9466), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 33, 22, 531, DateTimeKind.Unspecified).AddTicks(9467), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 33, 22, 531, DateTimeKind.Unspecified).AddTicks(9480), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 33, 22, 531, DateTimeKind.Unspecified).AddTicks(9481), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerConsent_AccountId",
                table: "CustomerConsent",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerConsent_UserId",
                table: "CustomerConsent",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerConsent");

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
