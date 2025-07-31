using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class SettlementStatusHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ChangedBy",
                table: "SettlementStatusHistory",
                type: "varchar(32)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ChangedByUserId",
                table: "SettlementStatusHistory",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SettlementStatusHistory_ChangedBy",
                table: "SettlementStatusHistory",
                column: "ChangedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_SettlementStatusHistory_ApplicationUser_ChangedBy",
                table: "SettlementStatusHistory",
                column: "ChangedBy",
                principalTable: "ApplicationUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SettlementStatusHistory_ApplicationUser_ChangedBy",
                table: "SettlementStatusHistory");

            migrationBuilder.DropIndex(
                name: "IX_SettlementStatusHistory_ChangedBy",
                table: "SettlementStatusHistory");

            migrationBuilder.DropColumn(
                name: "ChangedByUserId",
                table: "SettlementStatusHistory");

            migrationBuilder.UpdateData(
                table: "SettlementStatusHistory",
                keyColumn: "ChangedBy",
                keyValue: null,
                column: "ChangedBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ChangedBy",
                table: "SettlementStatusHistory",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
