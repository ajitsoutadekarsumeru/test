using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserCashWalletLimit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
