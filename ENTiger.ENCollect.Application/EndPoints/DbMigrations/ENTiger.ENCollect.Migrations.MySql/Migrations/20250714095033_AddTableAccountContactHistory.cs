using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class AddTableAccountContactHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountContactHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactValue = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitude = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    ContactSource = table.Column<int>(type: "int", nullable: false),
                    ContactType = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
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
                    table.PrimaryKey("PK_AccountContactHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountContactHistory_LoanAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "LoanAccounts",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_AccountContactHistory_AccountId",
                table: "AccountContactHistory",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountContactHistory");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 7, 17, 6, 21, 805, DateTimeKind.Unspecified).AddTicks(9550), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 7, 17, 6, 21, 805, DateTimeKind.Unspecified).AddTicks(9584), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 7, 17, 6, 21, 805, DateTimeKind.Unspecified).AddTicks(9599), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 7, 17, 6, 21, 805, DateTimeKind.Unspecified).AddTicks(9601), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 7, 17, 6, 21, 805, DateTimeKind.Unspecified).AddTicks(9592), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 7, 17, 6, 21, 805, DateTimeKind.Unspecified).AddTicks(9594), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 7, 17, 6, 21, 805, DateTimeKind.Unspecified).AddTicks(9605), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 7, 17, 6, 21, 805, DateTimeKind.Unspecified).AddTicks(9607), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
