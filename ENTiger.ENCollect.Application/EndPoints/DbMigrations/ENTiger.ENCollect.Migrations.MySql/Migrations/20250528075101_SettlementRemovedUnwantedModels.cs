using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class SettlementRemovedUnwantedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SettlementStatusHistory_ApplicationUser_ChangedBy",
                table: "SettlementStatusHistory");

            migrationBuilder.DropTable(
                name: "AssignedUser");

            migrationBuilder.DropTable(
                name: "WorkflowAssignment");

            migrationBuilder.DropTable(
                name: "LevelDesignation");

            migrationBuilder.DropIndex(
                name: "IX_SettlementStatusHistory_ChangedBy",
                table: "SettlementStatusHistory");

            migrationBuilder.DropColumn(
                name: "ChangedBy",
                table: "SettlementStatusHistory");

            migrationBuilder.AlterColumn<string>(
                name: "WorkflowInstanceId",
                table: "SettlementQueueProjection",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "SettlementDocument",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementStatusHistory_ChangedByUserId",
                table: "SettlementStatusHistory",
                column: "ChangedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SettlementStatusHistory_ApplicationUser_ChangedByUserId",
                table: "SettlementStatusHistory",
                column: "ChangedByUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SettlementStatusHistory_ApplicationUser_ChangedByUserId",
                table: "SettlementStatusHistory");

            migrationBuilder.DropIndex(
                name: "IX_SettlementStatusHistory_ChangedByUserId",
                table: "SettlementStatusHistory");

            migrationBuilder.AddColumn<string>(
                name: "ChangedBy",
                table: "SettlementStatusHistory",
                type: "varchar(32)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "WorkflowInstanceId",
                table: "SettlementQueueProjection",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "SettlementDocument",
                keyColumn: "FileName",
                keyValue: null,
                column: "FileName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "SettlementDocument",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LevelDesignation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DesignationId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Level = table.Column<int>(type: "int", maxLength: 50, nullable: false)
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
                name: "WorkflowAssignment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LevelDesignationId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SettlementStatusHistoryId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowAssignment_LevelDesignation_LevelDesignationId",
                        column: x => x.LevelDesignationId,
                        principalTable: "LevelDesignation",
                        principalColumn: "Id");
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
                    ApplicationUserId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkflowAssignmentId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_SettlementStatusHistory_ChangedBy",
                table: "SettlementStatusHistory",
                column: "ChangedBy");

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
                name: "IX_WorkflowAssignment_LevelDesignationId",
                table: "WorkflowAssignment",
                column: "LevelDesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowAssignment_SettlementStatusHistoryId",
                table: "WorkflowAssignment",
                column: "SettlementStatusHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SettlementStatusHistory_ApplicationUser_ChangedBy",
                table: "SettlementStatusHistory",
                column: "ChangedBy",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }
    }
}
