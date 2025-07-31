using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class Communication_EntryPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TriggerTemplateMapping");

            migrationBuilder.AddColumn<string>(
                name: "EntryPoint",
                table: "TriggerType",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OffsetBasis",
                table: "TriggerType",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OffsetDirection",
                table: "TriggerType",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "RequiresDaysOffset",
                table: "TriggerType",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EntryPoint",
                table: "CommunicationTemplate",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RecipientType",
                table: "CommunicationTemplate",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TriggerDeliverySpec",
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
                    table.PrimaryKey("PK_TriggerDeliverySpec", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TriggerDeliverySpec_CommunicationTemplate_CommunicationTempl~",
                        column: x => x.CommunicationTemplateId,
                        principalTable: "CommunicationTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TriggerDeliverySpec_CommunicationTrigger_CommunicationTrigge~",
                        column: x => x.CommunicationTriggerId,
                        principalTable: "CommunicationTrigger",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "TriggerDeliverySpec");

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
                name: "EntryPoint",
                table: "CommunicationTemplate");

            migrationBuilder.DropColumn(
                name: "RecipientType",
                table: "CommunicationTemplate");

            migrationBuilder.CreateTable(
                name: "TriggerTemplateMapping",
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
