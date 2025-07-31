using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class Communication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunicationTemplate_CommunicationTemplateDetail_Communicat~",
                table: "CommunicationTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunicationTemplate_CommunicationTemplateWorkflowState_Com~",
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
                name: "Subject",
                table: "CommunicationTemplateDetail",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "CommunicationTemplateDetail",
                keyColumn: "Body",
                keyValue: null,
                column: "Body",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "CommunicationTemplateDetail",
                type: "varchar(5000)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CommunicationTemplateId",
                table: "CommunicationTemplateDetail",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "CommunicationTemplateDetail",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "CommunicationTemplateDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TemplateType",
                table: "CommunicationTemplate",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CommunicationTemplate",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

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
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConditionType = table.Column<int>(type: "int", nullable: false),
                    DaysOffset = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaximumOccurences = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_CommunicationTrigger", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HierarchyLevel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
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
                    table.PrimaryKey("PK_HierarchyLevel", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CommunicationTriggerTemplate",
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

            migrationBuilder.CreateTable(
                name: "HierarchyMaster",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Item = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    LevelId1 = table.Column<string>(type: "varchar(32)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    ParentId1 = table.Column<string>(type: "varchar(32)", nullable: false)
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
                    table.PrimaryKey("PK_HierarchyMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HierarchyMaster_HierarchyLevel_LevelId1",
                        column: x => x.LevelId1,
                        principalTable: "HierarchyLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HierarchyMaster_HierarchyMaster_ParentId1",
                        column: x => x.ParentId1,
                        principalTable: "HierarchyMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 12, 11, 41, 27, 224, DateTimeKind.Unspecified).AddTicks(5795), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 12, 11, 41, 27, 224, DateTimeKind.Unspecified).AddTicks(5831), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 12, 11, 41, 27, 224, DateTimeKind.Unspecified).AddTicks(5846), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 12, 11, 41, 27, 224, DateTimeKind.Unspecified).AddTicks(5847), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 12, 11, 41, 27, 224, DateTimeKind.Unspecified).AddTicks(5841), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 12, 11, 41, 27, 224, DateTimeKind.Unspecified).AddTicks(5842), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 12, 11, 41, 27, 224, DateTimeKind.Unspecified).AddTicks(5852), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 12, 11, 41, 27, 224, DateTimeKind.Unspecified).AddTicks(5853), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTemplateDetail_CommunicationTemplateId",
                table: "CommunicationTemplateDetail",
                column: "CommunicationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTemplate_Name",
                table: "CommunicationTemplate",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTriggerTemplate_CommunicationTemplateId",
                table: "CommunicationTriggerTemplate",
                column: "CommunicationTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTriggerTemplate_CommunicationTriggerId",
                table: "CommunicationTriggerTemplate",
                column: "CommunicationTriggerId");

            migrationBuilder.CreateIndex(
                name: "IX_HierarchyMaster_LevelId1",
                table: "HierarchyMaster",
                column: "LevelId1");

            migrationBuilder.CreateIndex(
                name: "IX_HierarchyMaster_ParentId1",
                table: "HierarchyMaster",
                column: "ParentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunicationTemplateDetail_CommunicationTemplate_Communicat~",
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
                name: "FK_CommunicationTemplateDetail_CommunicationTemplate_Communicat~",
                table: "CommunicationTemplateDetail");

            migrationBuilder.DropTable(
                name: "CommunicationTriggerTemplate");

            migrationBuilder.DropTable(
                name: "HierarchyMaster");

            migrationBuilder.DropTable(
                name: "CommunicationTrigger");

            migrationBuilder.DropTable(
                name: "HierarchyLevel");

            migrationBuilder.DropIndex(
                name: "IX_CommunicationTemplateDetail_CommunicationTemplateId",
                table: "CommunicationTemplateDetail");

            migrationBuilder.DropIndex(
                name: "IX_CommunicationTemplate_Name",
                table: "CommunicationTemplate");

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
                name: "Subject",
                table: "CommunicationTemplateDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "CommunicationTemplateDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(5000)",
                oldMaxLength: 5000)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AddressTo",
                table: "CommunicationTemplateDetail",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CommunicationTemplateDetail",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Salutation",
                table: "CommunicationTemplateDetail",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "CommunicationTemplateDetail",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TemplateType",
                table: "CommunicationTemplate",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CommunicationTemplateDetailId",
                table: "CommunicationTemplate",
                type: "varchar(32)",
                maxLength: 32,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CommunicationTemplateWorkflowStateId",
                table: "CommunicationTemplate",
                type: "varchar(32)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "CommunicationTemplate",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Recipient",
                table: "CommunicationTemplate",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "WATemplateId",
                table: "CommunicationTemplate",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 9, 16, 57, 24, 19, DateTimeKind.Unspecified).AddTicks(4128), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 9, 16, 57, 24, 19, DateTimeKind.Unspecified).AddTicks(4326), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 9, 16, 57, 24, 19, DateTimeKind.Unspecified).AddTicks(4406), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 9, 16, 57, 24, 19, DateTimeKind.Unspecified).AddTicks(4408), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 9, 16, 57, 24, 19, DateTimeKind.Unspecified).AddTicks(4392), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 9, 16, 57, 24, 19, DateTimeKind.Unspecified).AddTicks(4395), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 6, 9, 16, 57, 24, 19, DateTimeKind.Unspecified).AddTicks(4419), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 6, 9, 16, 57, 24, 19, DateTimeKind.Unspecified).AddTicks(4422), new TimeSpan(0, 5, 30, 0, 0)) });

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
                name: "FK_CommunicationTemplate_CommunicationTemplateDetail_Communicat~",
                table: "CommunicationTemplate",
                column: "CommunicationTemplateDetailId",
                principalTable: "CommunicationTemplateDetail",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunicationTemplate_CommunicationTemplateWorkflowState_Com~",
                table: "CommunicationTemplate",
                column: "CommunicationTemplateWorkflowStateId",
                principalTable: "CommunicationTemplateWorkflowState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
