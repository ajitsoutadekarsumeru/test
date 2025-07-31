using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedGeoTagDetails : Migration
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

            migrationBuilder.DropIndex(
                name: "IX_LoanAccountsProjection_LoanAccountId",
                table: "LoanAccountsProjection");

            migrationBuilder.DropColumn(
                name: "ChangedBy",
                table: "SettlementStatusHistory");

            migrationBuilder.AlterColumn<string>(
                name: "WorkflowInstanceId",
                table: "SettlementQueueProjection",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "SettlementDocument",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "TOS",
                table: "Settlement",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "SettlementRemarks",
                table: "Settlement",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<decimal>(
                name: "LastCollectionAmount",
                table: "LoanAccountsProjection",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastCollectionMode",
                table: "LoanAccountsProjection",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "GeoTagDetails",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionSource",
                table: "GeoTagDetails",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CollectionProjection",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CollectionId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    BUCKET = table.Column<long>(type: "bigint", nullable: true),
                    CURRENT_BUCKET = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NPA_STAGEID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AgencyId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CollectorId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    TeleCallingAgencyId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    TeleCallerId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AllocationOwnerId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    BOM_POS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CURRENT_POS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PAYMENTSTATUS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CURRENT_TOTAL_AMOUNT_DUE = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionProjection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionProjection_ApplicationOrg_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "ApplicationOrg",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CollectionProjection_ApplicationOrg_TeleCallingAgencyId",
                        column: x => x.TeleCallingAgencyId,
                        principalTable: "ApplicationOrg",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CollectionProjection_ApplicationUser_AllocationOwnerId",
                        column: x => x.AllocationOwnerId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CollectionProjection_ApplicationUser_CollectorId",
                        column: x => x.CollectorId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CollectionProjection_ApplicationUser_TeleCallerId",
                        column: x => x.TeleCallerId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CollectionProjection_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackProjection",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    FeedbackId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    BUCKET = table.Column<long>(type: "bigint", nullable: true),
                    CURRENT_BUCKET = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NPA_STAGEID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AgencyId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CollectorId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    TeleCallingAgencyId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    TeleCallerId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AllocationOwnerId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    BOM_POS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CURRENT_POS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PAYMENTSTATUS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastDispositionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastDispositionCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastDispositionCodeGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastPTPDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackProjection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedbackProjection_ApplicationOrg_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "ApplicationOrg",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FeedbackProjection_ApplicationOrg_TeleCallingAgencyId",
                        column: x => x.TeleCallingAgencyId,
                        principalTable: "ApplicationOrg",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FeedbackProjection_ApplicationUser_AllocationOwnerId",
                        column: x => x.AllocationOwnerId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FeedbackProjection_ApplicationUser_CollectorId",
                        column: x => x.CollectorId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FeedbackProjection_ApplicationUser_TeleCallerId",
                        column: x => x.TeleCallerId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FeedbackProjection_Feedback_FeedbackId",
                        column: x => x.FeedbackId,
                        principalTable: "Feedback",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountScopeConfiguration",
                columns: new[] { "Id", "AccountabilityTypeId", "CreatedBy", "CreatedDate", "IsDeleted", "LastModifiedBy", "LastModifiedDate", "Scope", "ScopeLevel" },
                values: new object[,]
                {
                    { "3a185d8db599c016d4caf7aa05af889f", "AgencyToFrontEndExternalFOS", null, new DateTimeOffset(new DateTime(2025, 6, 9, 17, 5, 20, 948, DateTimeKind.Unspecified).AddTicks(5688), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 6, 9, 17, 5, 20, 948, DateTimeKind.Unspecified).AddTicks(5720), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599d1ce3ace0b1c74528678", "BankToFrontEndInternalFOS", null, new DateTimeOffset(new DateTime(2025, 6, 9, 17, 5, 20, 948, DateTimeKind.Unspecified).AddTicks(5740), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 6, 9, 17, 5, 20, 948, DateTimeKind.Unspecified).AddTicks(5742), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599f4a83d63dec4faea8a98", "AgencyToFrontEndExternalTC", null, new DateTimeOffset(new DateTime(2025, 6, 9, 17, 5, 20, 948, DateTimeKind.Unspecified).AddTicks(5733), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 6, 9, 17, 5, 20, 948, DateTimeKind.Unspecified).AddTicks(5734), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599f686a3b157eaeb799b2d", "BankToFrontEndInternalTC", null, new DateTimeOffset(new DateTime(2025, 6, 9, 17, 5, 20, 948, DateTimeKind.Unspecified).AddTicks(5759), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 6, 9, 17, 5, 20, 948, DateTimeKind.Unspecified).AddTicks(5761), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SettlementStatusHistory_ChangedByUserId",
                table: "SettlementStatusHistory",
                column: "ChangedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccountsProjection_LoanAccountId_Year_Month",
                table: "LoanAccountsProjection",
                columns: new[] { "LoanAccountId", "Year", "Month" },
                unique: true,
                filter: "[Year] IS NOT NULL AND [Month] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GeoTagDetails_AccountId",
                table: "GeoTagDetails",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionProjection_AgencyId",
                table: "CollectionProjection",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionProjection_AllocationOwnerId",
                table: "CollectionProjection",
                column: "AllocationOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionProjection_CollectionId",
                table: "CollectionProjection",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionProjection_CollectorId",
                table: "CollectionProjection",
                column: "CollectorId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionProjection_TeleCallerId",
                table: "CollectionProjection",
                column: "TeleCallerId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionProjection_TeleCallingAgencyId",
                table: "CollectionProjection",
                column: "TeleCallingAgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackProjection_AgencyId",
                table: "FeedbackProjection",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackProjection_AllocationOwnerId",
                table: "FeedbackProjection",
                column: "AllocationOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackProjection_CollectorId",
                table: "FeedbackProjection",
                column: "CollectorId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackProjection_FeedbackId",
                table: "FeedbackProjection",
                column: "FeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackProjection_TeleCallerId",
                table: "FeedbackProjection",
                column: "TeleCallerId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackProjection_TeleCallingAgencyId",
                table: "FeedbackProjection",
                column: "TeleCallingAgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeoTagDetails_LoanAccounts_AccountId",
                table: "GeoTagDetails",
                column: "AccountId",
                principalTable: "LoanAccounts",
                principalColumn: "Id");

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
                name: "FK_GeoTagDetails_LoanAccounts_AccountId",
                table: "GeoTagDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SettlementStatusHistory_ApplicationUser_ChangedByUserId",
                table: "SettlementStatusHistory");

            migrationBuilder.DropTable(
                name: "CollectionProjection");

            migrationBuilder.DropTable(
                name: "FeedbackProjection");

            migrationBuilder.DropIndex(
                name: "IX_SettlementStatusHistory_ChangedByUserId",
                table: "SettlementStatusHistory");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccountsProjection_LoanAccountId_Year_Month",
                table: "LoanAccountsProjection");

            migrationBuilder.DropIndex(
                name: "IX_GeoTagDetails_AccountId",
                table: "GeoTagDetails");

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

            migrationBuilder.DropColumn(
                name: "LastCollectionAmount",
                table: "LoanAccountsProjection");

            migrationBuilder.DropColumn(
                name: "LastCollectionMode",
                table: "LoanAccountsProjection");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "GeoTagDetails");

            migrationBuilder.DropColumn(
                name: "TransactionSource",
                table: "GeoTagDetails");

            migrationBuilder.AddColumn<string>(
                name: "ChangedBy",
                table: "SettlementStatusHistory",
                type: "nvarchar(32)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WorkflowInstanceId",
                table: "SettlementQueueProjection",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "SettlementDocument",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TOS",
                table: "Settlement",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SettlementRemarks",
                table: "Settlement",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "LevelDesignation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    DesignationId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "WorkflowAssignment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    LevelDesignationId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SettlementStatusHistoryId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
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
                    ApplicationUserId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    WorkflowAssignmentId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
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
                name: "IX_SettlementStatusHistory_ChangedBy",
                table: "SettlementStatusHistory",
                column: "ChangedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccountsProjection_LoanAccountId",
                table: "LoanAccountsProjection",
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
