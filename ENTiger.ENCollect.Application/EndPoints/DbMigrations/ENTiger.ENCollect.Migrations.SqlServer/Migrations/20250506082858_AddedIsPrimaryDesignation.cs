using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
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
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimaryDesignation",
                table: "AgencyUserDesignation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "InsightDownloadFile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsightDownloadFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settlement",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlement", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 6, 13, 58, 56, 136, DateTimeKind.Unspecified).AddTicks(8604), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 6, 13, 58, 56, 136, DateTimeKind.Unspecified).AddTicks(8635), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 6, 13, 58, 56, 136, DateTimeKind.Unspecified).AddTicks(8651), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 6, 13, 58, 56, 136, DateTimeKind.Unspecified).AddTicks(8653), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 6, 13, 58, 56, 136, DateTimeKind.Unspecified).AddTicks(8644), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 6, 13, 58, 56, 136, DateTimeKind.Unspecified).AddTicks(8645), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 6, 13, 58, 56, 136, DateTimeKind.Unspecified).AddTicks(8668), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 6, 13, 58, 56, 136, DateTimeKind.Unspecified).AddTicks(8670), new TimeSpan(0, 5, 30, 0, 0)) });
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
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 11, 8, 53, 26, 807, DateTimeKind.Unspecified).AddTicks(5447), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 11, 8, 53, 26, 807, DateTimeKind.Unspecified).AddTicks(5486), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 11, 8, 53, 26, 807, DateTimeKind.Unspecified).AddTicks(5501), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 11, 8, 53, 26, 807, DateTimeKind.Unspecified).AddTicks(5502), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 11, 8, 53, 26, 807, DateTimeKind.Unspecified).AddTicks(5494), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 11, 8, 53, 26, 807, DateTimeKind.Unspecified).AddTicks(5495), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 11, 8, 53, 26, 807, DateTimeKind.Unspecified).AddTicks(5513), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 11, 8, 53, 26, 807, DateTimeKind.Unspecified).AddTicks(5514), new TimeSpan(0, 2, 0, 0, 0)) });
        }
    }
}
