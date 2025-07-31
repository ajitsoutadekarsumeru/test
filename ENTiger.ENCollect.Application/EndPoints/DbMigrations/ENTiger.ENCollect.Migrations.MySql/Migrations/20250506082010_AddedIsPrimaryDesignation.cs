using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsPrimaryDesignation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrimaryDesignation",
                table: "CompanyUserDesignation",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimaryDesignation",
                table: "AgencyUserDesignation",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.CreateTable(
                name: "Settlement",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
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
                    table.PrimaryKey("PK_Settlement", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 6, 13, 50, 7, 715, DateTimeKind.Unspecified).AddTicks(5070), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 6, 13, 50, 7, 715, DateTimeKind.Unspecified).AddTicks(5112), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 6, 13, 50, 7, 715, DateTimeKind.Unspecified).AddTicks(5127), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 6, 13, 50, 7, 715, DateTimeKind.Unspecified).AddTicks(5128), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 6, 13, 50, 7, 715, DateTimeKind.Unspecified).AddTicks(5121), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 6, 13, 50, 7, 715, DateTimeKind.Unspecified).AddTicks(5122), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 6, 13, 50, 7, 715, DateTimeKind.Unspecified).AddTicks(5133), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 6, 13, 50, 7, 715, DateTimeKind.Unspecified).AddTicks(5134), new TimeSpan(0, 5, 30, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsightDownloadFile");

            migrationBuilder.DropTable(
                name: "Settlement");

            migrationBuilder.DropColumn(
                name: "IsPrimaryDesignation",
                table: "CompanyUserDesignation");

            migrationBuilder.DropColumn(
                name: "IsPrimaryDesignation",
                table: "AgencyUserDesignation");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 11, 8, 50, 46, 300, DateTimeKind.Unspecified).AddTicks(1990), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 11, 8, 50, 46, 300, DateTimeKind.Unspecified).AddTicks(2034), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 11, 8, 50, 46, 300, DateTimeKind.Unspecified).AddTicks(2047), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 11, 8, 50, 46, 300, DateTimeKind.Unspecified).AddTicks(2048), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 11, 8, 50, 46, 300, DateTimeKind.Unspecified).AddTicks(2041), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 11, 8, 50, 46, 300, DateTimeKind.Unspecified).AddTicks(2042), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 11, 8, 50, 46, 300, DateTimeKind.Unspecified).AddTicks(2053), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 11, 8, 50, 46, 300, DateTimeKind.Unspecified).AddTicks(2054), new TimeSpan(0, 2, 0, 0, 0)) });
        }
    }
}
