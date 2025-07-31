using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class AddedSOW : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {   
            migrationBuilder.DropColumn(
                name: "MaxHotleadCount",
                table: "ApplicationUser");

            migrationBuilder.AddColumn<string>(
                name: "GeoLevelId",
                table: "ApplicationUser",
                type: "varchar(32)",
                maxLength: 32,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ProductLevelId",
                table: "ApplicationUser",
                type: "varchar(32)",
                maxLength: 32,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserBucketScope",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BucketScopeId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApplicationUserId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
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
                    table.PrimaryKey("PK_UserBucketScope", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBucketScope_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserBucketScope_Buckets_BucketScopeId",
                        column: x => x.BucketScopeId,
                        principalTable: "Buckets",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserGeoScope",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GeoScopeId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApplicationUserId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
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
                    table.PrimaryKey("PK_UserGeoScope", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGeoScope_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserGeoScope_HierarchyMaster_GeoScopeId",
                        column: x => x.GeoScopeId,
                        principalTable: "HierarchyMaster",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserProductScope",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductScopeId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApplicationUserId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
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
                    table.PrimaryKey("PK_UserProductScope", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProductScope_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProductScope_HierarchyMaster_ProductScopeId",
                        column: x => x.ProductScopeId,
                        principalTable: "HierarchyMaster",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 18, 15, 12, 41, 813, DateTimeKind.Unspecified).AddTicks(6020), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 18, 15, 12, 41, 813, DateTimeKind.Unspecified).AddTicks(6088), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 18, 15, 12, 41, 813, DateTimeKind.Unspecified).AddTicks(6247), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 18, 15, 12, 41, 813, DateTimeKind.Unspecified).AddTicks(6253), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 18, 15, 12, 41, 813, DateTimeKind.Unspecified).AddTicks(6213), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 18, 15, 12, 41, 813, DateTimeKind.Unspecified).AddTicks(6220), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 18, 15, 12, 41, 813, DateTimeKind.Unspecified).AddTicks(6273), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 18, 15, 12, 41, 813, DateTimeKind.Unspecified).AddTicks(6279), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_GeoLevelId",
                table: "ApplicationUser",
                column: "GeoLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_ProductLevelId",
                table: "ApplicationUser",
                column: "ProductLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBucketScope_ApplicationUserId",
                table: "UserBucketScope",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBucketScope_BucketScopeId",
                table: "UserBucketScope",
                column: "BucketScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGeoScope_ApplicationUserId",
                table: "UserGeoScope",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGeoScope_GeoScopeId",
                table: "UserGeoScope",
                column: "GeoScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProductScope_ApplicationUserId",
                table: "UserProductScope",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProductScope_ProductScopeId",
                table: "UserProductScope",
                column: "ProductScopeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_HierarchyLevel_GeoLevelId",
                table: "ApplicationUser",
                column: "GeoLevelId",
                principalTable: "HierarchyLevel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_HierarchyLevel_ProductLevelId",
                table: "ApplicationUser",
                column: "ProductLevelId",
                principalTable: "HierarchyLevel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_HierarchyLevel_GeoLevelId",
                table: "ApplicationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_HierarchyLevel_ProductLevelId",
                table: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "UserBucketScope");

            migrationBuilder.DropTable(
                name: "UserGeoScope");

            migrationBuilder.DropTable(
                name: "UserProductScope");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_GeoLevelId",
                table: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_ProductLevelId",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "GeoLevelId",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "ProductLevelId",
                table: "ApplicationUser");

            migrationBuilder.AddColumn<string>(
                name: "MaxHotleadCount",
                table: "ApplicationUser",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");            

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 13, 14, 5, 24, 810, DateTimeKind.Unspecified).AddTicks(8465), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 13, 14, 5, 24, 810, DateTimeKind.Unspecified).AddTicks(8556), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 13, 14, 5, 24, 810, DateTimeKind.Unspecified).AddTicks(8622), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 13, 14, 5, 24, 810, DateTimeKind.Unspecified).AddTicks(8628), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 13, 14, 5, 24, 810, DateTimeKind.Unspecified).AddTicks(8589), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 13, 14, 5, 24, 810, DateTimeKind.Unspecified).AddTicks(8596), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 13, 14, 5, 24, 810, DateTimeKind.Unspecified).AddTicks(8653), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 13, 14, 5, 24, 810, DateTimeKind.Unspecified).AddTicks(8659), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
