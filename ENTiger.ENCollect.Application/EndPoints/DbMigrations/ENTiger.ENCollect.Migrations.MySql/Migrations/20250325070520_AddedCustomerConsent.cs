using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
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
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RequestedVisitTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ConsentResponseTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ExpiryTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecureToken = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 5, 17, 433, DateTimeKind.Unspecified).AddTicks(6515), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 5, 17, 433, DateTimeKind.Unspecified).AddTicks(6557), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 5, 17, 433, DateTimeKind.Unspecified).AddTicks(6581), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 5, 17, 433, DateTimeKind.Unspecified).AddTicks(6583), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 5, 17, 433, DateTimeKind.Unspecified).AddTicks(6570), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 5, 17, 433, DateTimeKind.Unspecified).AddTicks(6573), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 5, 17, 433, DateTimeKind.Unspecified).AddTicks(6608), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 5, 17, 433, DateTimeKind.Unspecified).AddTicks(6610), new TimeSpan(0, 2, 0, 0, 0)) });

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
