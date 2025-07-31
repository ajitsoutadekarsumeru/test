using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class Settlement_AcceptanceNegotiateFlow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StepIndex",
                table: "SettlementQueueProjection");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "SettlementStatusHistory",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "SettlementStatusHistory",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "RenegotiationAmount",
                table: "SettlementStatusHistory",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StepName",
                table: "SettlementQueueProjection",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "StepType",
                table: "SettlementQueueProjection",
                type: "varchar(3502)",
                maxLength: 3502,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UIActionContext",
                table: "SettlementQueueProjection",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "Settlement",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "RenegotiationAmount",
                table: "Settlement",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 16, 14, 46, 39, 150, DateTimeKind.Unspecified).AddTicks(6766), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 16, 14, 46, 39, 150, DateTimeKind.Unspecified).AddTicks(6840), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 16, 14, 46, 39, 150, DateTimeKind.Unspecified).AddTicks(6869), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 16, 14, 46, 39, 150, DateTimeKind.Unspecified).AddTicks(6871), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 16, 14, 46, 39, 150, DateTimeKind.Unspecified).AddTicks(6855), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 16, 14, 46, 39, 150, DateTimeKind.Unspecified).AddTicks(6858), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 16, 14, 46, 39, 150, DateTimeKind.Unspecified).AddTicks(6881), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 16, 14, 46, 39, 150, DateTimeKind.Unspecified).AddTicks(6884), new TimeSpan(0, 5, 30, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "SettlementStatusHistory");

            migrationBuilder.DropColumn(
                name: "RenegotiationAmount",
                table: "SettlementStatusHistory");

            migrationBuilder.DropColumn(
                name: "StepName",
                table: "SettlementQueueProjection");

            migrationBuilder.DropColumn(
                name: "StepType",
                table: "SettlementQueueProjection");

            migrationBuilder.DropColumn(
                name: "UIActionContext",
                table: "SettlementQueueProjection");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "RenegotiationAmount",
                table: "Settlement");

            migrationBuilder.UpdateData(
                table: "SettlementStatusHistory",
                keyColumn: "Comment",
                keyValue: null,
                column: "Comment",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "SettlementStatusHistory",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "StepIndex",
                table: "SettlementQueueProjection",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 12, 15, 23, 41, 475, DateTimeKind.Unspecified).AddTicks(5214), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 12, 15, 23, 41, 475, DateTimeKind.Unspecified).AddTicks(5257), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 12, 15, 23, 41, 475, DateTimeKind.Unspecified).AddTicks(5285), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 12, 15, 23, 41, 475, DateTimeKind.Unspecified).AddTicks(5288), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 12, 15, 23, 41, 475, DateTimeKind.Unspecified).AddTicks(5275), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 12, 15, 23, 41, 475, DateTimeKind.Unspecified).AddTicks(5278), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 12, 15, 23, 41, 475, DateTimeKind.Unspecified).AddTicks(5295), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 12, 15, 23, 41, 475, DateTimeKind.Unspecified).AddTicks(5297), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
