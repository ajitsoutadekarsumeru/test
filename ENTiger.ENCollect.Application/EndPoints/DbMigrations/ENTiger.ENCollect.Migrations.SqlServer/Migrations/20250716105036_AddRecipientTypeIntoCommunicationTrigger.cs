using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddRecipientTypeIntoCommunicationTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TriggerTemplateMapping");

            migrationBuilder.AddColumn<string>(
                name: "CustomName",
                table: "TriggerType",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EntryPoint",
                table: "TriggerType",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OffsetBasis",
                table: "TriggerType",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OffsetDirection",
                table: "TriggerType",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresDaysOffset",
                table: "TriggerType",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "SettlementStatusHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CancellationReason",
                table: "Settlement",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipientType",
                table: "CommunicationTrigger",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EntryPoint",
                table: "CommunicationTemplate",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecipientType",
                table: "CommunicationTemplate",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TriggerAccountQueueProjection",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    RunId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TriggerId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriggerAccountQueueProjection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TriggerAccountQueueProjection_CommunicationTrigger_TriggerId",
                        column: x => x.TriggerId,
                        principalTable: "CommunicationTrigger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TriggerAccountQueueProjection_LoanAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "LoanAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TriggerDeliverySpec",
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
                    table.PrimaryKey("PK_TriggerDeliverySpec", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TriggerDeliverySpec_CommunicationTemplate_CommunicationTemplateId",
                        column: x => x.CommunicationTemplateId,
                        principalTable: "CommunicationTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TriggerDeliverySpec_CommunicationTrigger_CommunicationTriggerId",
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
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_TriggerAccountQueueProjection_AccountId",
                table: "TriggerAccountQueueProjection",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TriggerAccountQueueProjection_TriggerId",
                table: "TriggerAccountQueueProjection",
                column: "TriggerId");

            migrationBuilder.CreateIndex(
                name: "IX_TriggerDeliverySpec_CommunicationTemplateId",
                table: "TriggerDeliverySpec",
                column: "CommunicationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TriggerDeliverySpec_CommunicationTriggerId",
                table: "TriggerDeliverySpec",
                column: "CommunicationTriggerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TriggerAccountQueueProjection");

            migrationBuilder.DropTable(
                name: "TriggerDeliverySpec");

            migrationBuilder.DropColumn(
                name: "CustomName",
                table: "TriggerType");

            migrationBuilder.DropColumn(
                name: "EntryPoint",
                table: "TriggerType");

            migrationBuilder.DropColumn(
                name: "OffsetBasis",
                table: "TriggerType");

            migrationBuilder.DropColumn(
                name: "OffsetDirection",
                table: "TriggerType");

            migrationBuilder.DropColumn(
                name: "RequiresDaysOffset",
                table: "TriggerType");

            migrationBuilder.DropColumn(
                name: "Action",
                table: "SettlementStatusHistory");

            migrationBuilder.DropColumn(
                name: "CancellationReason",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "RecipientType",
                table: "CommunicationTrigger");

            migrationBuilder.DropColumn(
                name: "EntryPoint",
                table: "CommunicationTemplate");

            migrationBuilder.DropColumn(
                name: "RecipientType",
                table: "CommunicationTemplate");

            migrationBuilder.CreateTable(
                name: "TriggerTemplateMapping",
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
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 15, 11, 27, 23, 533, DateTimeKind.Unspecified).AddTicks(6084), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 15, 11, 27, 23, 533, DateTimeKind.Unspecified).AddTicks(6112), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 15, 11, 27, 23, 533, DateTimeKind.Unspecified).AddTicks(6128), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 15, 11, 27, 23, 533, DateTimeKind.Unspecified).AddTicks(6129), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 15, 11, 27, 23, 533, DateTimeKind.Unspecified).AddTicks(6122), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 15, 11, 27, 23, 533, DateTimeKind.Unspecified).AddTicks(6123), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 7, 15, 11, 27, 23, 533, DateTimeKind.Unspecified).AddTicks(6139), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 7, 15, 11, 27, 23, 533, DateTimeKind.Unspecified).AddTicks(6140), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_TriggerTemplateMapping_CommunicationTemplateId",
                table: "TriggerTemplateMapping",
                column: "CommunicationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TriggerTemplateMapping_CommunicationTriggerId",
                table: "TriggerTemplateMapping",
                column: "CommunicationTriggerId");
        }
    }
}
