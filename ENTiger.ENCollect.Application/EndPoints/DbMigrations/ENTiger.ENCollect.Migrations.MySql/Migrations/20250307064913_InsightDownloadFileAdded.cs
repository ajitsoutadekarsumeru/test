using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class InsightDownloadFileAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsightDownloadFile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
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
                    table.PrimaryKey("PK_InsightDownloadFile", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 7, 12, 19, 10, 514, DateTimeKind.Unspecified).AddTicks(296), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 7, 12, 19, 10, 514, DateTimeKind.Unspecified).AddTicks(336), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "AccountabilityTypeId", "CreatedDate", "LastModifiedDate" },
                values: new object[] { "BankToFrontEndInternalFOS", new DateTimeOffset(new DateTime(2025, 3, 7, 12, 19, 10, 514, DateTimeKind.Unspecified).AddTicks(352), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 7, 12, 19, 10, 514, DateTimeKind.Unspecified).AddTicks(353), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 7, 12, 19, 10, 514, DateTimeKind.Unspecified).AddTicks(346), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 7, 12, 19, 10, 514, DateTimeKind.Unspecified).AddTicks(347), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "AccountabilityTypeId", "CreatedDate", "LastModifiedDate" },
                values: new object[] { "BankToFrontEndInternalTC", new DateTimeOffset(new DateTime(2025, 3, 7, 12, 19, 10, 514, DateTimeKind.Unspecified).AddTicks(357), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 7, 12, 19, 10, 514, DateTimeKind.Unspecified).AddTicks(359), new TimeSpan(0, 5, 30, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsightDownloadFile");

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 2, 28, 12, 47, 3, 82, DateTimeKind.Unspecified).AddTicks(552), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 28, 12, 47, 3, 82, DateTimeKind.Unspecified).AddTicks(675), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "AccountabilityTypeId", "CreatedDate", "LastModifiedDate" },
                values: new object[] { "BankToForntendInternalFOS", new DateTimeOffset(new DateTime(2025, 2, 28, 12, 47, 3, 82, DateTimeKind.Unspecified).AddTicks(717), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 28, 12, 47, 3, 82, DateTimeKind.Unspecified).AddTicks(719), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 2, 28, 12, 47, 3, 82, DateTimeKind.Unspecified).AddTicks(703), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 28, 12, 47, 3, 82, DateTimeKind.Unspecified).AddTicks(706), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "AccountabilityTypeId", "CreatedDate", "LastModifiedDate" },
                values: new object[] { "BankToForntendInternalTC", new DateTimeOffset(new DateTime(2025, 2, 28, 12, 47, 3, 82, DateTimeKind.Unspecified).AddTicks(728), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 28, 12, 47, 3, 82, DateTimeKind.Unspecified).AddTicks(730), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
