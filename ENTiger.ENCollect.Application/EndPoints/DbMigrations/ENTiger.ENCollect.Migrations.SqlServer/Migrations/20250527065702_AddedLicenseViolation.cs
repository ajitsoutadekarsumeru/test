using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedLicenseViolation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f");

            migrationBuilder.DeleteData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678");

            migrationBuilder.DeleteData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98");

            migrationBuilder.DeleteData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d");

            migrationBuilder.AddColumn<string>(
                name: "CURRENT_BUCKET",
                table: "Settlement",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CURRENT_DPD",
                table: "Settlement",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ChargesOutstanding",
                table: "Settlement",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CustomId",
                table: "Settlement",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfDeath",
                table: "Settlement",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "InterestOutstanding",
                table: "Settlement",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeathSettlement",
                table: "Settlement",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LatestHistoryId",
                table: "Settlement",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoanAccountId",
                table: "Settlement",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NPA_STAGEID",
                table: "Settlement",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfEmisDue",
                table: "Settlement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfInstallments",
                table: "Settlement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PrincipalOutstanding",
                table: "Settlement",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SettlementAmount",
                table: "Settlement",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "SettlementDate",
                table: "Settlement",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SettlementDateForDuesCalc",
                table: "Settlement",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SettlementRemarks",
                table: "Settlement",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Settlement",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusUpdatedOn",
                table: "Settlement",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TOS",
                table: "Settlement",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TrancheType",
                table: "Settlement",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "Designation",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "InstallmentDetail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    InstallmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InstallmentDueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SettlementId = table.Column<string>(type: "nvarchar(32)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstallmentDetail_Settlement_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlement",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LevelDesignation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Level = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    DesignationId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "LicenseViolation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    OccurredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Feature = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Limit = table.Column<int>(type: "int", nullable: false),
                    Actual = table.Column<int>(type: "int", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseViolation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettlementDocument",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    SettlementId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UploadedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlementDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettlementDocument_Settlement_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SettlementQueueProjection",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    WorkflowName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WorkflowInstanceId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SettlementId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    StepIndex = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlementQueueProjection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettlementQueueProjection_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SettlementQueueProjection_Settlement_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SettlementStatusHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    SettlementId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    FromStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ToStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ChangedByUserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    ChangedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettlementStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettlementStatusHistory_ApplicationUser_ChangedBy",
                        column: x => x.ChangedBy,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SettlementStatusHistory_Settlement_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLevelProjection",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLevelProjection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLevelProjection_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WaiverDetail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ChargeType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AmountAsPerCBS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApportionmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaiverAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WaiverPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SettlementId = table.Column<string>(type: "nvarchar(32)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaiverDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WaiverDetail_Settlement_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlement",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkflowAssignment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    SettlementStatusHistoryId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    LevelDesignationId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
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
                        name: "FK_WorkflowAssignment_SettlementStatusHistory_SettlementStatusHistoryId",
                        column: x => x.SettlementStatusHistoryId,
                        principalTable: "SettlementStatusHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignedUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    WorkflowAssignmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_Settlement_LatestHistoryId",
                table: "Settlement",
                column: "LatestHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Settlement_LoanAccountId",
                table: "Settlement",
                column: "LoanAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedUser_ApplicationUserId",
                table: "AssignedUser",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedUser_WorkflowAssignmentId",
                table: "AssignedUser",
                column: "WorkflowAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentDetail_SettlementId",
                table: "InstallmentDetail",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelDesignation_DesignationId",
                table: "LevelDesignation",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementDocument_SettlementId",
                table: "SettlementDocument",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementQueueProjection_ApplicationUserId",
                table: "SettlementQueueProjection",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementQueueProjection_SettlementId",
                table: "SettlementQueueProjection",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementStatusHistory_ChangedBy",
                table: "SettlementStatusHistory",
                column: "ChangedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementStatusHistory_SettlementId",
                table: "SettlementStatusHistory",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLevelProjection_ApplicationUserId",
                table: "UserLevelProjection",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WaiverDetail_SettlementId",
                table: "WaiverDetail",
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
                name: "FK_Settlement_LoanAccounts_LoanAccountId",
                table: "Settlement",
                column: "LoanAccountId",
                principalTable: "LoanAccounts",
                principalColumn: "Id");

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
                name: "FK_Settlement_LoanAccounts_LoanAccountId",
                table: "Settlement");

            migrationBuilder.DropForeignKey(
                name: "FK_Settlement_SettlementStatusHistory_LatestHistoryId",
                table: "Settlement");

            migrationBuilder.DropTable(
                name: "AssignedUser");

            migrationBuilder.DropTable(
                name: "InstallmentDetail");

            migrationBuilder.DropTable(
                name: "LicenseViolation");

            migrationBuilder.DropTable(
                name: "SettlementDocument");

            migrationBuilder.DropTable(
                name: "SettlementQueueProjection");

            migrationBuilder.DropTable(
                name: "UserLevelProjection");

            migrationBuilder.DropTable(
                name: "WaiverDetail");

            migrationBuilder.DropTable(
                name: "WorkflowAssignment");

            migrationBuilder.DropTable(
                name: "LevelDesignation");

            migrationBuilder.DropTable(
                name: "SettlementStatusHistory");

            migrationBuilder.DropIndex(
                name: "IX_Settlement_LatestHistoryId",
                table: "Settlement");

            migrationBuilder.DropIndex(
                name: "IX_Settlement_LoanAccountId",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "CURRENT_BUCKET",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "CURRENT_DPD",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "ChargesOutstanding",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "CustomId",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "DateOfDeath",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "InterestOutstanding",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "IsDeathSettlement",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "LatestHistoryId",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "LoanAccountId",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "NPA_STAGEID",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "NumberOfEmisDue",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "NumberOfInstallments",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "PrincipalOutstanding",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "SettlementAmount",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "SettlementDate",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "SettlementDateForDuesCalc",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "SettlementRemarks",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "StatusUpdatedOn",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "TOS",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "TrancheType",
                table: "Settlement");

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Designation",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 10);

            migrationBuilder.InsertData(
                table: "AccountScopeConfiguration",
                columns: new[] { "Id", "AccountabilityTypeId", "CreatedBy", "CreatedDate", "IsDeleted", "LastModifiedBy", "LastModifiedDate", "Scope", "ScopeLevel" },
                values: new object[,]
                {
                    { "3a185d8db599c016d4caf7aa05af889f", "AgencyToFrontEndExternalFOS", null, new DateTimeOffset(new DateTime(2025, 5, 14, 9, 50, 25, 160, DateTimeKind.Unspecified).AddTicks(6003), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 5, 14, 9, 50, 25, 160, DateTimeKind.Unspecified).AddTicks(6034), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599d1ce3ace0b1c74528678", "BankToFrontEndInternalFOS", null, new DateTimeOffset(new DateTime(2025, 5, 14, 9, 50, 25, 160, DateTimeKind.Unspecified).AddTicks(6051), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 5, 14, 9, 50, 25, 160, DateTimeKind.Unspecified).AddTicks(6052), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599f4a83d63dec4faea8a98", "AgencyToFrontEndExternalTC", null, new DateTimeOffset(new DateTime(2025, 5, 14, 9, 50, 25, 160, DateTimeKind.Unspecified).AddTicks(6044), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 5, 14, 9, 50, 25, 160, DateTimeKind.Unspecified).AddTicks(6045), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599f686a3b157eaeb799b2d", "BankToFrontEndInternalTC", null, new DateTimeOffset(new DateTime(2025, 5, 14, 9, 50, 25, 160, DateTimeKind.Unspecified).AddTicks(6065), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 5, 14, 9, 50, 25, 160, DateTimeKind.Unspecified).AddTicks(6066), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 }
                });
        }
    }
}
