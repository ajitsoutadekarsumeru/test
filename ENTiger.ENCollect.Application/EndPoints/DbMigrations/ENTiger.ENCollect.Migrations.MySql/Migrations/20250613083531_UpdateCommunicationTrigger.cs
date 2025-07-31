using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCommunicationTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConditionType",
                table: "CommunicationTrigger");

            migrationBuilder.AlterColumn<int>(
                name: "DaysOffset",
                table: "CommunicationTrigger",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TriggerTypeId",
                table: "CommunicationTrigger",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TemplateType",
                table: "CommunicationTemplate",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowAccessFromAccountDetails",
                table: "CommunicationTemplate",
                type: "tinyint(1)",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTrigger_TriggerTypeId",
                table: "CommunicationTrigger",
                column: "TriggerTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunicationTrigger_CategoryItem_TriggerTypeId",
                table: "CommunicationTrigger",
                column: "TriggerTypeId",
                principalTable: "CategoryItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunicationTrigger_CategoryItem_TriggerTypeId",
                table: "CommunicationTrigger");

            migrationBuilder.DropIndex(
                name: "IX_CommunicationTrigger_TriggerTypeId",
                table: "CommunicationTrigger");

            migrationBuilder.DropColumn(
                name: "TriggerTypeId",
                table: "CommunicationTrigger");

            migrationBuilder.DropColumn(
                name: "IsAllowAccessFromAccountDetails",
                table: "CommunicationTemplate");

            migrationBuilder.AlterColumn<int>(
                name: "DaysOffset",
                table: "CommunicationTrigger",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ConditionType",
                table: "CommunicationTrigger",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TemplateType",
                table: "CommunicationTemplate",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
