using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class LatestAllocationDateFieldAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsedWallet",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "WalletLastUpdatedDate",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "WalletLimit",
                table: "ApplicationUser");

            migrationBuilder.AddColumn<DateTime>(
                name: "LatestAllocationDate",
                table: "LoanAccounts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    AgentId = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    WalletLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConsumedFunds = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.AgentId);
                    table.ForeignKey(
                        name: "FK_Wallet_ApplicationUser_AgentId",
                        column: x => x.AgentId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WalletAgentId = table.Column<string>(type: "nvarchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_Wallet_WalletAgentId",
                        column: x => x.WalletAgentId,
                        principalTable: "Wallet",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 3, 16, 53, 20, 868, DateTimeKind.Unspecified).AddTicks(3272), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 3, 16, 53, 20, 868, DateTimeKind.Unspecified).AddTicks(3330), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 3, 16, 53, 20, 868, DateTimeKind.Unspecified).AddTicks(3378), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 3, 16, 53, 20, 868, DateTimeKind.Unspecified).AddTicks(3382), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 3, 16, 53, 20, 868, DateTimeKind.Unspecified).AddTicks(3355), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 3, 16, 53, 20, 868, DateTimeKind.Unspecified).AddTicks(3360), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 4, 3, 16, 53, 20, 868, DateTimeKind.Unspecified).AddTicks(3425), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2025, 4, 3, 16, 53, 20, 868, DateTimeKind.Unspecified).AddTicks(3429), new TimeSpan(0, 5, 30, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_WalletAgentId",
                table: "Reservation",
                column: "WalletAgentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.DropColumn(
                name: "LatestAllocationDate",
                table: "LoanAccounts");

            migrationBuilder.AddColumn<decimal>(
                name: "UsedWallet",
                table: "ApplicationUser",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "WalletLastUpdatedDate",
                table: "ApplicationUser",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WalletLimit",
                table: "ApplicationUser",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599c016d4caf7aa05af889f",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 25, 6, 998, DateTimeKind.Unspecified).AddTicks(4127), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 25, 6, 998, DateTimeKind.Unspecified).AddTicks(4164), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599d1ce3ace0b1c74528678",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 25, 6, 998, DateTimeKind.Unspecified).AddTicks(4176), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 25, 6, 998, DateTimeKind.Unspecified).AddTicks(4178), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f4a83d63dec4faea8a98",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 25, 6, 998, DateTimeKind.Unspecified).AddTicks(4170), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 25, 6, 998, DateTimeKind.Unspecified).AddTicks(4171), new TimeSpan(0, 2, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "RoleAccountScope",
                keyColumn: "Id",
                keyValue: "3a185d8db599f686a3b157eaeb799b2d",
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 25, 9, 25, 6, 998, DateTimeKind.Unspecified).AddTicks(4187), new TimeSpan(0, 2, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 25, 9, 25, 6, 998, DateTimeKind.Unspecified).AddTicks(4188), new TimeSpan(0, 2, 0, 0, 0)) });
        }
    }
}
