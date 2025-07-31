using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class BucketHeatMapResolutionFieldModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BucketHeatMapConfig_Resolutions_ResolutionMasterId",
                table: "BucketHeatMapConfig");

            migrationBuilder.DropIndex(
                name: "IX_BucketHeatMapConfig_ResolutionMasterId",
                table: "BucketHeatMapConfig");

            migrationBuilder.DropColumn(
                name: "ResolutionMasterId",
                table: "BucketHeatMapConfig");

            migrationBuilder.RenameColumn(
                name: "Resolutions",
                table: "BucketHeatMapConfig",
                newName: "ResolutionId");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 18, 18, 44, 58, 741, DateTimeKind.Unspecified).AddTicks(841), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 18, 18, 44, 58, 741, DateTimeKind.Unspecified).AddTicks(878), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 18, 18, 44, 58, 741, DateTimeKind.Unspecified).AddTicks(888), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 18, 18, 44, 58, 741, DateTimeKind.Unspecified).AddTicks(888), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 18, 18, 44, 58, 741, DateTimeKind.Unspecified).AddTicks(883), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 18, 18, 44, 58, 741, DateTimeKind.Unspecified).AddTicks(884), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 18, 18, 44, 58, 741, DateTimeKind.Unspecified).AddTicks(892), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 18, 18, 44, 58, 741, DateTimeKind.Unspecified).AddTicks(893), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_BucketHeatMapConfig_ResolutionId",
                table: "BucketHeatMapConfig",
                column: "ResolutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_BucketHeatMapConfig_Resolutions_ResolutionId",
                table: "BucketHeatMapConfig",
                column: "ResolutionId",
                principalTable: "Resolutions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BucketHeatMapConfig_Resolutions_ResolutionId",
                table: "BucketHeatMapConfig");

            migrationBuilder.DropIndex(
                name: "IX_BucketHeatMapConfig_ResolutionId",
                table: "BucketHeatMapConfig");

            migrationBuilder.RenameColumn(
                name: "ResolutionId",
                table: "BucketHeatMapConfig",
                newName: "Resolutions");

            migrationBuilder.AddColumn<string>(
                name: "ResolutionMasterId",
                table: "BucketHeatMapConfig",
                type: "varchar(32)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 17, 19, 5, 35, 983, DateTimeKind.Unspecified).AddTicks(8237), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 17, 19, 5, 35, 983, DateTimeKind.Unspecified).AddTicks(8317), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 17, 19, 5, 35, 983, DateTimeKind.Unspecified).AddTicks(8337), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 17, 19, 5, 35, 983, DateTimeKind.Unspecified).AddTicks(8339), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 17, 19, 5, 35, 983, DateTimeKind.Unspecified).AddTicks(8329), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 17, 19, 5, 35, 983, DateTimeKind.Unspecified).AddTicks(8331), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 17, 19, 5, 35, 983, DateTimeKind.Unspecified).AddTicks(8345), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 17, 19, 5, 35, 983, DateTimeKind.Unspecified).AddTicks(8347), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_BucketHeatMapConfig_ResolutionMasterId",
                table: "BucketHeatMapConfig",
                column: "ResolutionMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_BucketHeatMapConfig_Resolutions_ResolutionMasterId",
                table: "BucketHeatMapConfig",
                column: "ResolutionMasterId",
                principalTable: "Resolutions",
                principalColumn: "Id");
        }
    }
}
