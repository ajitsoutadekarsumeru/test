using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class ReportsDownloadLogTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "HierarchyLevel",
                table: "AccountGeoMap",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ReportDownloadLog",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReportType = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReportFilters = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileName = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FilePath = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CustomId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
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
                    table.PrimaryKey("PK_ReportDownloadLog", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 11, 11, 58, 33, 649, DateTimeKind.Unspecified).AddTicks(8601), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 11, 11, 58, 33, 649, DateTimeKind.Unspecified).AddTicks(8677), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 11, 11, 58, 33, 649, DateTimeKind.Unspecified).AddTicks(8697), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 11, 11, 58, 33, 649, DateTimeKind.Unspecified).AddTicks(8699), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 11, 11, 58, 33, 649, DateTimeKind.Unspecified).AddTicks(8690), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 11, 11, 58, 33, 649, DateTimeKind.Unspecified).AddTicks(8692), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 11, 11, 58, 33, 649, DateTimeKind.Unspecified).AddTicks(8704), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 11, 11, 58, 33, 649, DateTimeKind.Unspecified).AddTicks(8705), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountProductMap_HierarchyMaster_HierarchyId",
                table: "AccountProductMap",
                column: "HierarchyId",
                principalTable: "HierarchyMaster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountProductMap_LoanAccounts_AccountId",
                table: "AccountProductMap",
                column: "AccountId",
                principalTable: "LoanAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountProductMap_HierarchyMaster_HierarchyId",
                table: "AccountProductMap");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountProductMap_LoanAccounts_AccountId",
                table: "AccountProductMap");

            migrationBuilder.DropTable(
                name: "ReportDownloadLog");

            migrationBuilder.AlterColumn<int>(
                name: "HierarchyLevel",
                table: "AccountGeoMap",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
