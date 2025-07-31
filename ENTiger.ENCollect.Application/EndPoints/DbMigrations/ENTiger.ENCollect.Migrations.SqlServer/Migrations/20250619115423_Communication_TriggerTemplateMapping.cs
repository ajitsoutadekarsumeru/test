using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Communication_TriggerTemplateMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunicationTrigger_CategoryItem_TriggerTypeId",
                table: "CommunicationTrigger");

            migrationBuilder.DropTable(
                name: "CommunicationTemplateWorkflowState");

            migrationBuilder.DropTable(
                name: "CommunicationTriggerTemplate");

            migrationBuilder.DropIndex(
                name: "IX_CommunicationTrigger_TriggerTypeId",
                table: "CommunicationTrigger");

            migrationBuilder.DropColumn(
                name: "MaximumOccurences",
                table: "CommunicationTrigger");

            migrationBuilder.DropColumn(
                name: "TriggerTypeId",
                table: "CommunicationTrigger");

            migrationBuilder.DropColumn(
                name: "IsAllowAccessFromAccountDetails",
                table: "CommunicationTemplate");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CommunicationTrigger",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CommunicationTriggerTypeId",
                table: "CommunicationTrigger",
                type: "nvarchar(32)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailableInAccountDetails",
                table: "CommunicationTemplate",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CommunicationTriggerType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                    table.PrimaryKey("PK_CommunicationTriggerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TriggerTemplateMapping",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CommunicationTriggerId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CommunicationTemplateId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriggerTemplateMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TriggerTemplateMapping_CommunicationTemplate_CommunicationTemplateId",
                        column: x => x.CommunicationTemplateId,
                        principalTable: "CommunicationTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TriggerTemplateMapping_CommunicationTrigger_CommunicationTriggerId",
                        column: x => x.CommunicationTriggerId,
                        principalTable: "CommunicationTrigger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 19, 17, 24, 16, 818, DateTimeKind.Unspecified).AddTicks(6103), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 19, 17, 24, 16, 818, DateTimeKind.Unspecified).AddTicks(6147), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 19, 17, 24, 16, 818, DateTimeKind.Unspecified).AddTicks(6168), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 19, 17, 24, 16, 818, DateTimeKind.Unspecified).AddTicks(6169), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 19, 17, 24, 16, 818, DateTimeKind.Unspecified).AddTicks(6161), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 19, 17, 24, 16, 818, DateTimeKind.Unspecified).AddTicks(6162), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 19, 17, 24, 16, 818, DateTimeKind.Unspecified).AddTicks(6178), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 19, 17, 24, 16, 818, DateTimeKind.Unspecified).AddTicks(6179), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTrigger_CommunicationTriggerTypeId",
                table: "CommunicationTrigger",
                column: "CommunicationTriggerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TriggerTemplateMapping_CommunicationTemplateId",
                table: "TriggerTemplateMapping",
                column: "CommunicationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TriggerTemplateMapping_CommunicationTriggerId",
                table: "TriggerTemplateMapping",
                column: "CommunicationTriggerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunicationTrigger_CommunicationTriggerType_CommunicationTriggerTypeId",
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
                name: "FK_CommunicationTrigger_CommunicationTriggerType_CommunicationTriggerTypeId",
                table: "CommunicationTrigger");

            migrationBuilder.DropTable(
                name: "CommunicationTriggerType");

            migrationBuilder.DropTable(
                name: "TriggerTemplateMapping");

            migrationBuilder.DropIndex(
                name: "IX_CommunicationTrigger_CommunicationTriggerTypeId",
                table: "CommunicationTrigger");

            migrationBuilder.DropColumn(
                name: "CommunicationTriggerTypeId",
                table: "CommunicationTrigger");

            migrationBuilder.DropColumn(
                name: "IsAvailableInAccountDetails",
                table: "CommunicationTemplate");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CommunicationTrigger",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaximumOccurences",
                table: "CommunicationTrigger",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TriggerTypeId",
                table: "CommunicationTrigger",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowAccessFromAccountDetails",
                table: "CommunicationTemplate",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CommunicationTemplateWorkflowState",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StateChangedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    StateChangedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    TFlexId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationTemplateWorkflowState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationTriggerTemplate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CommunicationTemplateId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CommunicationTriggerId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationTriggerTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunicationTriggerTemplate_CommunicationTemplate_CommunicationTemplateId",
                        column: x => x.CommunicationTemplateId,
                        principalTable: "CommunicationTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunicationTriggerTemplate_CommunicationTrigger_CommunicationTriggerId",
                        column: x => x.CommunicationTriggerId,
                        principalTable: "CommunicationTrigger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 17, 11, 55, 17, 220, DateTimeKind.Unspecified).AddTicks(887), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 17, 11, 55, 17, 220, DateTimeKind.Unspecified).AddTicks(924), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 17, 11, 55, 17, 220, DateTimeKind.Unspecified).AddTicks(937), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 17, 11, 55, 17, 220, DateTimeKind.Unspecified).AddTicks(938), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 17, 11, 55, 17, 220, DateTimeKind.Unspecified).AddTicks(931), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 17, 11, 55, 17, 220, DateTimeKind.Unspecified).AddTicks(932), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 17, 11, 55, 17, 220, DateTimeKind.Unspecified).AddTicks(945), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 17, 11, 55, 17, 220, DateTimeKind.Unspecified).AddTicks(946), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTrigger_TriggerTypeId",
                table: "CommunicationTrigger",
                column: "TriggerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTriggerTemplate_CommunicationTemplateId",
                table: "CommunicationTriggerTemplate",
                column: "CommunicationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTriggerTemplate_CommunicationTriggerId",
                table: "CommunicationTriggerTemplate",
                column: "CommunicationTriggerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunicationTrigger_CategoryItem_TriggerTypeId",
                table: "CommunicationTrigger",
                column: "TriggerTypeId",
                principalTable: "CategoryItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
