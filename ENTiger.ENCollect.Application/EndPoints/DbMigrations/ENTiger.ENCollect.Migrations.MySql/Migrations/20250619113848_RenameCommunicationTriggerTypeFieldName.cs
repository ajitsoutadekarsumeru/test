using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class RenameCommunicationTriggerTypeFieldName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunicationTrigger_CommunicationTriggerType_CommunicationT~",
                table: "CommunicationTrigger");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "CommunicationTriggerType",
                newName: "Name");

            migrationBuilder.UpdateData(
                table: "CommunicationTrigger",
                keyColumn: "CommunicationTriggerTypeId",
                keyValue: null,
                column: "CommunicationTriggerTypeId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CommunicationTriggerTypeId",
                table: "CommunicationTrigger",
                type: "varchar(32)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 19, 17, 8, 44, 194, DateTimeKind.Unspecified).AddTicks(7879), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 19, 17, 8, 44, 194, DateTimeKind.Unspecified).AddTicks(7935), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 19, 17, 8, 44, 194, DateTimeKind.Unspecified).AddTicks(7953), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 19, 17, 8, 44, 194, DateTimeKind.Unspecified).AddTicks(7954), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 19, 17, 8, 44, 194, DateTimeKind.Unspecified).AddTicks(7945), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 19, 17, 8, 44, 194, DateTimeKind.Unspecified).AddTicks(7947), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 19, 17, 8, 44, 194, DateTimeKind.Unspecified).AddTicks(7960), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 19, 17, 8, 44, 194, DateTimeKind.Unspecified).AddTicks(7961), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.AddForeignKey(
                name: "FK_CommunicationTrigger_CommunicationTriggerType_CommunicationT~",
                table: "CommunicationTrigger",
                column: "CommunicationTriggerTypeId",
                principalTable: "CommunicationTriggerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunicationTrigger_CommunicationTriggerType_CommunicationT~",
                table: "CommunicationTrigger");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CommunicationTriggerType",
                newName: "Type");

            migrationBuilder.AlterColumn<string>(
                name: "CommunicationTriggerTypeId",
                table: "CommunicationTrigger",
                type: "varchar(32)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(32)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 18, 18, 26, 41, 767, DateTimeKind.Unspecified).AddTicks(146), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 18, 18, 26, 41, 767, DateTimeKind.Unspecified).AddTicks(240), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 18, 18, 26, 41, 767, DateTimeKind.Unspecified).AddTicks(260), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 18, 18, 26, 41, 767, DateTimeKind.Unspecified).AddTicks(262), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 18, 18, 26, 41, 767, DateTimeKind.Unspecified).AddTicks(251), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 18, 18, 26, 41, 767, DateTimeKind.Unspecified).AddTicks(253), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 18, 18, 26, 41, 767, DateTimeKind.Unspecified).AddTicks(270), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 18, 18, 26, 41, 767, DateTimeKind.Unspecified).AddTicks(272), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.AddForeignKey(
                name: "FK_CommunicationTrigger_CommunicationTriggerType_CommunicationT~",
                table: "CommunicationTrigger",
                column: "CommunicationTriggerTypeId",
                principalTable: "CommunicationTriggerType",
                principalColumn: "Id");
        }
    }
}
