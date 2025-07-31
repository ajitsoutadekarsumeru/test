using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class Settlement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CURRENT_BUCKET",
                table: "Settlement",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<long>(
                name: "CURRENT_DPD",
                table: "Settlement",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ChargesOutstanding",
                table: "Settlement",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfDeath",
                table: "Settlement",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "InterestOutstanding",
                table: "Settlement",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeathSettlement",
                table: "Settlement",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LoanAccountId",
                table: "Settlement",
                type: "varchar(32)",
                maxLength: 32,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NPA_STAGEID",
                table: "Settlement",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

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
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SettlementAmount",
                table: "Settlement",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "SettlementDateForDuesCalc",
                table: "Settlement",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SettlementRemarks",
                table: "Settlement",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Settlement",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusUpdatedOn",
                table: "Settlement",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TOS",
                table: "Settlement",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TrancheType",
                table: "Settlement",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InstallmentDetail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InstallmentAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    InstallmentDueDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SettlementId = table.Column<string>(type: "varchar(32)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallmentDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstallmentDetail_Settlement_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlement",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WaiverDetail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChargeType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AmountAsPerCBS = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ApportionmentAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    WaiverAmount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    WaiverPercent = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    SettlementId = table.Column<string>(type: "varchar(32)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaiverDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WaiverDetail_Settlement_SettlementId",
                        column: x => x.SettlementId,
                        principalTable: "Settlement",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_Settlement_LoanAccountId",
                table: "Settlement",
                column: "LoanAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallmentDetail_SettlementId",
                table: "InstallmentDetail",
                column: "SettlementId");

            migrationBuilder.CreateIndex(
                name: "IX_WaiverDetail_SettlementId",
                table: "WaiverDetail",
                column: "SettlementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Settlement_LoanAccounts_LoanAccountId",
                table: "Settlement",
                column: "LoanAccountId",
                principalTable: "LoanAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settlement_LoanAccounts_LoanAccountId",
                table: "Settlement");

            migrationBuilder.DropTable(
                name: "InstallmentDetail");

            migrationBuilder.DropTable(
                name: "WaiverDetail");

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
                name: "DateOfDeath",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "InterestOutstanding",
                table: "Settlement");

            migrationBuilder.DropColumn(
                name: "IsDeathSettlement",
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

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 6, 13, 50, 7, 715, DateTimeKind.Unspecified).AddTicks(5070), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 6, 13, 50, 7, 715, DateTimeKind.Unspecified).AddTicks(5112), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 6, 13, 50, 7, 715, DateTimeKind.Unspecified).AddTicks(5127), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 6, 13, 50, 7, 715, DateTimeKind.Unspecified).AddTicks(5128), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 6, 13, 50, 7, 715, DateTimeKind.Unspecified).AddTicks(5121), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 6, 13, 50, 7, 715, DateTimeKind.Unspecified).AddTicks(5122), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "AccountScopeConfiguration",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 5, 6, 13, 50, 7, 715, DateTimeKind.Unspecified).AddTicks(5133), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 5, 6, 13, 50, 7, 715, DateTimeKind.Unspecified).AddTicks(5134), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
