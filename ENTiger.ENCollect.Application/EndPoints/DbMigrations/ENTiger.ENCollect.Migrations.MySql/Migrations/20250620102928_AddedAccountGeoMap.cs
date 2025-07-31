using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class AddedAccountGeoMap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountGeoMap",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HierarchyId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HierarchyLevel = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_AccountGeoMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountGeoMap_HierarchyMaster_HierarchyId",
                        column: x => x.HierarchyId,
                        principalTable: "HierarchyMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountGeoMap_LoanAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "LoanAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 20, 15, 59, 23, 631, DateTimeKind.Unspecified).AddTicks(9675), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 20, 15, 59, 23, 631, DateTimeKind.Unspecified).AddTicks(9746), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 20, 15, 59, 23, 631, DateTimeKind.Unspecified).AddTicks(9829), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 20, 15, 59, 23, 631, DateTimeKind.Unspecified).AddTicks(9833), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 20, 15, 59, 23, 631, DateTimeKind.Unspecified).AddTicks(9800), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 20, 15, 59, 23, 631, DateTimeKind.Unspecified).AddTicks(9806), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 20, 15, 59, 23, 631, DateTimeKind.Unspecified).AddTicks(9852), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 20, 15, 59, 23, 631, DateTimeKind.Unspecified).AddTicks(9855), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_AccountGeoMap_AccountId",
                table: "AccountGeoMap",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountGeoMap_HierarchyId",
                table: "AccountGeoMap",
                column: "HierarchyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountGeoMap");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 19, 19, 31, 40, 708, DateTimeKind.Unspecified).AddTicks(3701), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 19, 19, 31, 40, 708, DateTimeKind.Unspecified).AddTicks(3752), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 19, 19, 31, 40, 708, DateTimeKind.Unspecified).AddTicks(3828), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 19, 19, 31, 40, 708, DateTimeKind.Unspecified).AddTicks(3832), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 19, 19, 31, 40, 708, DateTimeKind.Unspecified).AddTicks(3804), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 19, 19, 31, 40, 708, DateTimeKind.Unspecified).AddTicks(3809), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 19, 19, 31, 40, 708, DateTimeKind.Unspecified).AddTicks(3846), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 19, 19, 31, 40, 708, DateTimeKind.Unspecified).AddTicks(3851), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
