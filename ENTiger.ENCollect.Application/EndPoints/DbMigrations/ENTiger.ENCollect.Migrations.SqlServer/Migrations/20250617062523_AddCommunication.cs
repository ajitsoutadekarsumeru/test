using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddCommunication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunicationTemplate_CommunicationTemplateDetail_CommunicationTemplateDetailId",
                table: "CommunicationTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunicationTemplate_CommunicationTemplateWorkflowState_CommunicationTemplateWorkflowStateId",
                table: "CommunicationTemplate");

            migrationBuilder.DropIndex(
                name: "IX_CommunicationTemplateDetail_Name",
                table: "CommunicationTemplateDetail");

            migrationBuilder.DropIndex(
                name: "IX_CommunicationTemplate_CommunicationTemplateDetailId",
                table: "CommunicationTemplate");

            migrationBuilder.DropIndex(
                name: "IX_CommunicationTemplate_CommunicationTemplateWorkflowStateId",
                table: "CommunicationTemplate");

            migrationBuilder.DropColumn(
                name: "StepIndex",
                table: "SettlementQueueProjection");

            migrationBuilder.DropColumn(
                name: "AddressTo",
                table: "CommunicationTemplateDetail");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CommunicationTemplateDetail");

            migrationBuilder.DropColumn(
                name: "Salutation",
                table: "CommunicationTemplateDetail");

            migrationBuilder.DropColumn(
                name: "Signature",
                table: "CommunicationTemplateDetail");

            migrationBuilder.DropColumn(
                name: "CommunicationTemplateDetailId",
                table: "CommunicationTemplate");

            migrationBuilder.DropColumn(
                name: "CommunicationTemplateWorkflowStateId",
                table: "CommunicationTemplate");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "CommunicationTemplate");

            migrationBuilder.DropColumn(
                name: "Recipient",
                table: "CommunicationTemplate");

            migrationBuilder.DropColumn(
                name: "WATemplateId",
                table: "CommunicationTemplate");

            migrationBuilder.RenameColumn(
                name: "IsDisabled",
                table: "CommunicationTemplate",
                newName: "IsActive");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "SettlementStatusHistory",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "SettlementStatusHistory",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RenegotiationAmount",
                table: "SettlementStatusHistory",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StepName",
                table: "SettlementQueueProjection",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StepType",
                table: "SettlementQueueProjection",
                type: "nvarchar(3502)",
                maxLength: 3502,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UIActionContext",
                table: "SettlementQueueProjection",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "Settlement",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RenegotiationAmount",
                table: "Settlement",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LatestBPTPDate",
                table: "LoanAccountsProjection",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "CommunicationTemplateDetail",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "CommunicationTemplateDetail",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommunicationTemplateId",
                table: "CommunicationTemplateDetail",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "CommunicationTemplateDetail",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "CommunicationTemplateDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "TemplateType",
                table: "CommunicationTemplate",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowAccessFromAccountDetails",
                table: "CommunicationTemplate",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CommunicationTemplate",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "CommunicationTemplate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CommunicationTrigger",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TriggerTypeId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    DaysOffset = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaximumOccurences = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationTrigger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunicationTrigger_CategoryItem_TriggerTypeId",
                        column: x => x.TriggerTypeId,
                        principalTable: "CategoryItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationTriggerTemplate",
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
                name: "IX_CommunicationTemplateDetail_CommunicationTemplateId",
                table: "CommunicationTemplateDetail",
                column: "CommunicationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTemplate_Name",
                table: "CommunicationTemplate",
                column: "Name");

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
                name: "FK_CommunicationTemplateDetail_CommunicationTemplate_CommunicationTemplateId",
                table: "CommunicationTemplateDetail",
                column: "CommunicationTemplateId",
                principalTable: "CommunicationTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunicationTemplateDetail_CommunicationTemplate_CommunicationTemplateId",
                table: "CommunicationTemplateDetail");

            migrationBuilder.DropTable(
                name: "CommunicationTriggerTemplate");

            migrationBuilder.DropTable(
                name: "CommunicationTrigger");

            migrationBuilder.DropIndex(
                name: "IX_CommunicationTemplateDetail_CommunicationTemplateId",
                table: "CommunicationTemplateDetail");

            migrationBuilder.DropIndex(
                name: "IX_CommunicationTemplate_Name",
                table: "CommunicationTemplate");

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

            migrationBuilder.DropColumn(
                name: "CommunicationTemplateId",
                table: "CommunicationTemplateDetail");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "CommunicationTemplateDetail");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "CommunicationTemplateDetail");

            migrationBuilder.DropColumn(
                name: "IsAllowAccessFromAccountDetails",
                table: "CommunicationTemplate");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CommunicationTemplate");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "CommunicationTemplate");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "CommunicationTemplate",
                newName: "IsDisabled");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "SettlementStatusHistory",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StepIndex",
                table: "SettlementQueueProjection",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LatestBPTPDate",
                table: "LoanAccountsProjection",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "CommunicationTemplateDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "CommunicationTemplateDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.AddColumn<string>(
                name: "AddressTo",
                table: "CommunicationTemplateDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CommunicationTemplateDetail",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Salutation",
                table: "CommunicationTemplateDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "CommunicationTemplateDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TemplateType",
                table: "CommunicationTemplate",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CommunicationTemplateDetailId",
                table: "CommunicationTemplate",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommunicationTemplateWorkflowStateId",
                table: "CommunicationTemplate",
                type: "nvarchar(32)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "CommunicationTemplate",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Recipient",
                table: "CommunicationTemplate",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WATemplateId",
                table: "CommunicationTemplate",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 12, 15, 13, 48, 503, DateTimeKind.Unspecified).AddTicks(105), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 12, 15, 13, 48, 503, DateTimeKind.Unspecified).AddTicks(147), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 12, 15, 13, 48, 503, DateTimeKind.Unspecified).AddTicks(172), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 12, 15, 13, 48, 503, DateTimeKind.Unspecified).AddTicks(175), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 12, 15, 13, 48, 503, DateTimeKind.Unspecified).AddTicks(161), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 12, 15, 13, 48, 503, DateTimeKind.Unspecified).AddTicks(164), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 12, 15, 13, 48, 503, DateTimeKind.Unspecified).AddTicks(189), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 12, 15, 13, 48, 503, DateTimeKind.Unspecified).AddTicks(192), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTemplateDetail_Name",
                table: "CommunicationTemplateDetail",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTemplate_CommunicationTemplateDetailId",
                table: "CommunicationTemplate",
                column: "CommunicationTemplateDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTemplate_CommunicationTemplateWorkflowStateId",
                table: "CommunicationTemplate",
                column: "CommunicationTemplateWorkflowStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunicationTemplate_CommunicationTemplateDetail_CommunicationTemplateDetailId",
                table: "CommunicationTemplate",
                column: "CommunicationTemplateDetailId",
                principalTable: "CommunicationTemplateDetail",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunicationTemplate_CommunicationTemplateWorkflowState_CommunicationTemplateWorkflowStateId",
                table: "CommunicationTemplate",
                column: "CommunicationTemplateWorkflowStateId",
                principalTable: "CommunicationTemplateWorkflowState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
