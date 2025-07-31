using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
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
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CommunicationTriggerTypeId",
                table: "CommunicationTrigger",
                type: "varchar(32)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailableInAccountDetails",
                table: "CommunicationTemplate",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CommunicationTriggerType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
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
                    table.PrimaryKey("PK_CommunicationTriggerType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TriggerTemplateMapping",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CommunicationTriggerId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CommunicationTemplateId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
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
                    table.PrimaryKey("PK_TriggerTemplateMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TriggerTemplateMapping_CommunicationTemplate_CommunicationTe~",
                        column: x => x.CommunicationTemplateId,
                        principalTable: "CommunicationTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TriggerTemplateMapping_CommunicationTrigger_CommunicationTri~",
                        column: x => x.CommunicationTriggerId,
                        principalTable: "CommunicationTrigger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

          
           
           
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
                name: "FK_CommunicationTrigger_CommunicationTriggerType_CommunicationT~",
                table: "CommunicationTrigger",
                column: "CommunicationTriggerTypeId",
                principalTable: "CommunicationTriggerType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunicationTrigger_CommunicationTriggerType_CommunicationT~",
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

            migrationBuilder.UpdateData(
                table: "CommunicationTrigger",
                keyColumn: "Description",
                keyValue: null,
                column: "Description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CommunicationTrigger",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "MaximumOccurences",
                table: "CommunicationTrigger",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TriggerTypeId",
                table: "CommunicationTrigger",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowAccessFromAccountDetails",
                table: "CommunicationTemplate",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CommunicationTemplateWorkflowState",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Discriminator = table.Column<string>(type: "varchar(34)", maxLength: 34, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsCompleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remarks = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StateChangedBy = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StateChangedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    TFlexId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationTemplateWorkflowState", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CommunicationTriggerTemplate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CommunicationTemplateId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CommunicationTriggerId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedBy = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationTriggerTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunicationTriggerTemplate_CommunicationTemplate_Communica~",
                        column: x => x.CommunicationTemplateId,
                        principalTable: "CommunicationTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunicationTriggerTemplate_CommunicationTrigger_Communicat~",
                        column: x => x.CommunicationTriggerId,
                        principalTable: "CommunicationTrigger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
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
