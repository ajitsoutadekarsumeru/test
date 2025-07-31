using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedCommunicationTriggers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunicationTrigger_CommunicationTriggerType_CommunicationTriggerTypeId",
                table: "CommunicationTrigger");

            migrationBuilder.DropTable(
                name: "CommunicationTriggerType");

            migrationBuilder.RenameColumn(
                name: "CommunicationTriggerTypeId",
                table: "CommunicationTrigger",
                newName: "TriggerTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunicationTrigger_CommunicationTriggerTypeId",
                table: "CommunicationTrigger",
                newName: "IX_CommunicationTrigger_TriggerTypeId");

            migrationBuilder.CreateTable(
                name: "TriggerType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriggerType", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 20, 10, 31, 14, 128, DateTimeKind.Unspecified).AddTicks(8230), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 20, 10, 31, 14, 128, DateTimeKind.Unspecified).AddTicks(8267), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 20, 10, 31, 14, 128, DateTimeKind.Unspecified).AddTicks(8280), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 20, 10, 31, 14, 128, DateTimeKind.Unspecified).AddTicks(8281), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 20, 10, 31, 14, 128, DateTimeKind.Unspecified).AddTicks(8274), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 20, 10, 31, 14, 128, DateTimeKind.Unspecified).AddTicks(8275), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 20, 10, 31, 14, 128, DateTimeKind.Unspecified).AddTicks(8581), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 20, 10, 31, 14, 128, DateTimeKind.Unspecified).AddTicks(8582), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.AddForeignKey(
                name: "FK_CommunicationTrigger_TriggerType_TriggerTypeId",
                table: "CommunicationTrigger",
                column: "TriggerTypeId",
                principalTable: "TriggerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunicationTrigger_TriggerType_TriggerTypeId",
                table: "CommunicationTrigger");

            migrationBuilder.DropTable(
                name: "TriggerType");

            migrationBuilder.RenameColumn(
                name: "TriggerTypeId",
                table: "CommunicationTrigger",
                newName: "CommunicationTriggerTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunicationTrigger_TriggerTypeId",
                table: "CommunicationTrigger",
                newName: "IX_CommunicationTrigger_CommunicationTriggerTypeId");

            migrationBuilder.CreateTable(
                name: "CommunicationTriggerType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationTriggerType", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 19, 17, 45, 15, 13, DateTimeKind.Unspecified).AddTicks(1644), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 19, 17, 45, 15, 13, DateTimeKind.Unspecified).AddTicks(1718), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 19, 17, 45, 15, 13, DateTimeKind.Unspecified).AddTicks(1755), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 19, 17, 45, 15, 13, DateTimeKind.Unspecified).AddTicks(1758), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 19, 17, 45, 15, 13, DateTimeKind.Unspecified).AddTicks(1741), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 19, 17, 45, 15, 13, DateTimeKind.Unspecified).AddTicks(1744), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 19, 17, 45, 15, 13, DateTimeKind.Unspecified).AddTicks(1791), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 19, 17, 45, 15, 13, DateTimeKind.Unspecified).AddTicks(1794), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.AddForeignKey(
                name: "FK_CommunicationTrigger_CommunicationTriggerType_CommunicationTriggerTypeId",
                table: "CommunicationTrigger",
                column: "CommunicationTriggerTypeId",
                principalTable: "CommunicationTriggerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
