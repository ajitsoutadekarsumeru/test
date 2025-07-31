using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class FeedbackAndCollectionProjectionAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoanAccountsProjection_LoanAccountId",
                table: "LoanAccountsProjection");

            migrationBuilder.CreateTable(
                name: "CollectionProjection",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CollectionId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BUCKET = table.Column<long>(type: "bigint", nullable: true),
                    CURRENT_BUCKET = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NPA_STAGEID = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AgencyId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CollectorId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TeleCallingAgencyId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TeleCallerId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AllocationOwnerId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BOM_POS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CURRENT_POS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PAYMENTSTATUS = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FeedbackProjection",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FeedbackId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BUCKET = table.Column<long>(type: "bigint", nullable: true),
                    CURRENT_BUCKET = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NPA_STAGEID = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AgencyId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CollectorId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TeleCallingAgencyId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TeleCallerId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AllocationOwnerId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BOM_POS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CURRENT_POS = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PAYMENTSTATUS = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AccountScopeConfiguration",
                columns: new[] { "Id", "AccountabilityTypeId", "CreatedBy", "CreatedDate", "IsDeleted", "LastModifiedBy", "LastModifiedDate", "Scope", "ScopeLevel" },
                values: new object[,]
                {
                    { "3a185d8db599c016d4caf7aa05af889f", "AgencyToFrontEndExternalFOS", null, new DateTimeOffset(new DateTime(2025, 5, 30, 18, 5, 0, 576, DateTimeKind.Unspecified).AddTicks(2112), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 5, 30, 18, 5, 0, 576, DateTimeKind.Unspecified).AddTicks(2179), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599d1ce3ace0b1c74528678", "BankToFrontEndInternalFOS", null, new DateTimeOffset(new DateTime(2025, 5, 30, 18, 5, 0, 576, DateTimeKind.Unspecified).AddTicks(2206), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 5, 30, 18, 5, 0, 576, DateTimeKind.Unspecified).AddTicks(2208), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599f4a83d63dec4faea8a98", "AgencyToFrontEndExternalTC", null, new DateTimeOffset(new DateTime(2025, 5, 30, 18, 5, 0, 576, DateTimeKind.Unspecified).AddTicks(2196), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 5, 30, 18, 5, 0, 576, DateTimeKind.Unspecified).AddTicks(2198), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 },
                    { "3a185d8db599f686a3b157eaeb799b2d", "BankToFrontEndInternalTC", null, new DateTimeOffset(new DateTime(2025, 5, 30, 18, 5, 0, 576, DateTimeKind.Unspecified).AddTicks(2215), new TimeSpan(0, 5, 30, 0, 0)), false, null, new DateTimeOffset(new DateTime(2025, 5, 30, 18, 5, 0, 576, DateTimeKind.Unspecified).AddTicks(2217), new TimeSpan(0, 5, 30, 0, 0)), "all", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccountsProjection_LoanAccountId_Year_Month",
                table: "LoanAccountsProjection",
                columns: new[] { "LoanAccountId", "Year", "Month" },
                unique: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectionProjection");

            migrationBuilder.DropTable(
                name: "FeedbackProjection");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccountsProjection_LoanAccountId_Year_Month",
                table: "LoanAccountsProjection");

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

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccountsProjection_LoanAccountId",
                table: "LoanAccountsProjection",
                column: "LoanAccountId");
        }
    }
}
