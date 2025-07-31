using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddTableAccountContactHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BucketHeatMapConfig");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Resolutions");

            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "AddressLine3",
                table: "Address");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Resolutions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DPD_From",
                table: "Buckets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DPD_To",
                table: "Buckets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DisplayLabel",
                table: "Buckets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine",
                table: "Address",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountContactHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ContactValue = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    ContactSource = table.Column<int>(type: "int", nullable: false),
                    ContactType = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountContactHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountContactHistory_LoanAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "LoanAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BranchGeoMap",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    BranchId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    HierarchyId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    HierarchyLevel = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchGeoMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchGeoMap_ApplicationOrg_BranchId",
                        column: x => x.BranchId,
                        principalTable: "ApplicationOrg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchGeoMap_HierarchyMaster_HierarchyId",
                        column: x => x.HierarchyId,
                        principalTable: "HierarchyMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeatMapConfig",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    HeatMapType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RowId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ColumnId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    RangeFrom = table.Column<int>(type: "int", nullable: false),
                    RangeTo = table.Column<int>(type: "int", nullable: false),
                    HeatIndicator = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeatMapConfig", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 14, 15, 55, 57, 168, DateTimeKind.Unspecified).AddTicks(1495), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 14, 15, 55, 57, 168, DateTimeKind.Unspecified).AddTicks(1535), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 14, 15, 55, 57, 168, DateTimeKind.Unspecified).AddTicks(1551), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 14, 15, 55, 57, 168, DateTimeKind.Unspecified).AddTicks(1553), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 14, 15, 55, 57, 168, DateTimeKind.Unspecified).AddTicks(1543), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 14, 15, 55, 57, 168, DateTimeKind.Unspecified).AddTicks(1545), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 14, 15, 55, 57, 168, DateTimeKind.Unspecified).AddTicks(1565), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 14, 15, 55, 57, 168, DateTimeKind.Unspecified).AddTicks(1566), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_AccountContactHistory_AccountId",
                table: "AccountContactHistory",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchGeoMap_BranchId",
                table: "BranchGeoMap",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchGeoMap_HierarchyId",
                table: "BranchGeoMap",
                column: "HierarchyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountContactHistory");

            migrationBuilder.DropTable(
                name: "BranchGeoMap");

            migrationBuilder.DropTable(
                name: "HeatMapConfig");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Resolutions");

            migrationBuilder.DropColumn(
                name: "DPD_From",
                table: "Buckets");

            migrationBuilder.DropColumn(
                name: "DPD_To",
                table: "Buckets");

            migrationBuilder.DropColumn(
                name: "DisplayLabel",
                table: "Buckets");

            migrationBuilder.DropColumn(
                name: "AddressLine",
                table: "Address");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Resolutions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "Address",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "Address",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine3",
                table: "Address",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BucketHeatMapConfig",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    BucketId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ResolutionMasterId = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RangeFrom = table.Column<int>(type: "int", nullable: false),
                    RangeTo = table.Column<int>(type: "int", nullable: false),
                    Resolutions = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BucketHeatMapConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BucketHeatMapConfig_Buckets_BucketId",
                        column: x => x.BucketId,
                        principalTable: "Buckets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BucketHeatMapConfig_Resolutions_ResolutionMasterId",
                        column: x => x.ResolutionMasterId,
                        principalTable: "Resolutions",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 20, 16, 5, 20, 121, DateTimeKind.Unspecified).AddTicks(6624), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 20, 16, 5, 20, 121, DateTimeKind.Unspecified).AddTicks(6698), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 20, 16, 5, 20, 121, DateTimeKind.Unspecified).AddTicks(6795), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 20, 16, 5, 20, 121, DateTimeKind.Unspecified).AddTicks(6800), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 20, 16, 5, 20, 121, DateTimeKind.Unspecified).AddTicks(6759), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 20, 16, 5, 20, 121, DateTimeKind.Unspecified).AddTicks(6766), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 20, 16, 5, 20, 121, DateTimeKind.Unspecified).AddTicks(6825), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 20, 16, 5, 20, 121, DateTimeKind.Unspecified).AddTicks(6831), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_BucketHeatMapConfig_BucketId",
                table: "BucketHeatMapConfig",
                column: "BucketId");

            migrationBuilder.CreateIndex(
                name: "IX_BucketHeatMapConfig_ResolutionMasterId",
                table: "BucketHeatMapConfig",
                column: "ResolutionMasterId");
        }
    }
}
