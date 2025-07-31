using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class SettlementQueue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ChargeType",
                table: "WaiverDetail",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "WaiverDetail",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Settlement",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CustomId",
                table: "Settlement",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LatestHistoryId",
                table: "Settlement",
                type: "varchar(32)",
                maxLength: 32,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "InstallmentDetail",
                type: "varchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LevelDesignation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Level = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    DesignationId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelDesignation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LevelDesignation_Designation_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SettlementStatusHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SettlementId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FromStatus = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ToStatus = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChangedBy = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChangedDate = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    Comment = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlementStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettlementStatusHistory_Settlement_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkflowAssignment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SettlementStatusHistoryId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LevelDesignationId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowAssignment_LevelDesignation_LevelDesignationId",
                        column: x => x.LevelDesignationId,
                        principalTable: "LevelDesignation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkflowAssignment_SettlementStatusHistory_SettlementStatusH~",
                        column: x => x.SettlementStatusHistoryId,
                        principalTable: "SettlementStatusHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AssignedUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkflowAssignmentId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApplicationUserId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedUser_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedUser_WorkflowAssignment_WorkflowAssignmentId",
                        column: x => x.WorkflowAssignmentId,
                        principalTable: "WorkflowAssignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 14, 10, 44, 7, 411, DateTimeKind.Unspecified).AddTicks(2439), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 14, 10, 44, 7, 411, DateTimeKind.Unspecified).AddTicks(2538), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 14, 10, 44, 7, 411, DateTimeKind.Unspecified).AddTicks(2567), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 14, 10, 44, 7, 411, DateTimeKind.Unspecified).AddTicks(2569), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 14, 10, 44, 7, 411, DateTimeKind.Unspecified).AddTicks(2553), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 14, 10, 44, 7, 411, DateTimeKind.Unspecified).AddTicks(2556), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 14, 10, 44, 7, 411, DateTimeKind.Unspecified).AddTicks(2579), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 14, 10, 44, 7, 411, DateTimeKind.Unspecified).AddTicks(2582), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_Settlement_LatestHistoryId",
                table: "Settlement",
                column: "LatestHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedUser_ApplicationUserId",
                table: "AssignedUser",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedUser_WorkflowAssignmentId",
                table: "AssignedUser",
                column: "WorkflowAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelDesignation_DesignationId",
                table: "LevelDesignation",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementStatusHistory_SettlementId",
                table: "SettlementStatusHistory",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowAssignment_LevelDesignationId",
                table: "WorkflowAssignment",
                column: "LevelDesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowAssignment_SettlementStatusHistoryId",
                table: "WorkflowAssignment",
                column: "SettlementStatusHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Settlement_SettlementStatusHistory_LatestHistoryId",
                table: "Settlement",
                column: "LatestHistoryId",
                principalTable: "SettlementStatusHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settlement_SettlementStatusHistory_LatestHistoryId",
                table: "Settlement");

            migrationBuilder.DropTable(
                name: "AssignedUser");

            migrationBuilder.DropTable(
                name: "WorkflowAssignment");

            migrationBuilder.DropTable(
                name: "LevelDesignation");

            migrationBuilder.DropTable(
                name: "SettlementStatusHistory");

            migrationBuilder.DropIndex(
                name: "IX_Settlement_LatestHistoryId",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "CustomId",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "LatestHistoryId",
                table: "Settlement");

            migrationBuilder.AlterColumn<string>(
                name: "ChargeType",
                table: "WaiverDetail",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "WaiverDetail",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldMaxLength: 32)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Settlement",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "InstallmentDetail",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldMaxLength: 32)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 13, 16, 18, 3, 49, DateTimeKind.Unspecified).AddTicks(5847), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 13, 16, 18, 3, 49, DateTimeKind.Unspecified).AddTicks(5898), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 13, 16, 18, 3, 49, DateTimeKind.Unspecified).AddTicks(5906), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 13, 16, 18, 3, 49, DateTimeKind.Unspecified).AddTicks(5907), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 13, 16, 18, 3, 49, DateTimeKind.Unspecified).AddTicks(5902), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 13, 16, 18, 3, 49, DateTimeKind.Unspecified).AddTicks(5903), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 13, 16, 18, 3, 49, DateTimeKind.Unspecified).AddTicks(5909), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 13, 16, 18, 3, 49, DateTimeKind.Unspecified).AddTicks(5910), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
