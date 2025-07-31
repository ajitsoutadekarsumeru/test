using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class AddedCommunicationTriggers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunicationTrigger_CommunicationTriggerType_CommunicationT~",
                table: "CommunicationTrigger");

            migrationBuilder.DropTable(
                name: "CommunicationTriggerType");

            migrationBuilder.DropIndex(
                name: "IX_CommunicationTrigger_CommunicationTriggerTypeId",
                table: "CommunicationTrigger");

            migrationBuilder.DropColumn(
                name: "CommunicationTriggerTypeId",
                table: "CommunicationTrigger");

            migrationBuilder.CreateTable(
                name: "TriggerType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
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
                    table.PrimaryKey("PK_TriggerType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTrigger_TriggerTypeId",
                table: "CommunicationTrigger",
                column: "TriggerTypeId");

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

            migrationBuilder.DropIndex(
                name: "IX_CommunicationTrigger_TriggerTypeId",
                table: "CommunicationTrigger");

            migrationBuilder.AddColumn<string>(
                name: "CommunicationTriggerTypeId",
                table: "CommunicationTrigger",
                type: "varchar(32)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CommunicationTriggerType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationTriggerType", x => x.Id);
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
                name: "IX_CommunicationTrigger_CommunicationTriggerTypeId",
                table: "CommunicationTrigger",
                column: "CommunicationTriggerTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunicationTrigger_CommunicationTriggerType_CommunicationT~",
                table: "CommunicationTrigger",
                column: "CommunicationTriggerTypeId",
                principalTable: "CommunicationTriggerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
