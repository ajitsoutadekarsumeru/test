using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class AddedAuditTrailTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LoanAccountJSON_AccountId",
                table: "LoanAccountJSON");

            migrationBuilder.AddColumn<string>(
                name: "DeliquencyReason",
                table: "Feedback",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsPolicyAccepted",
                table: "ApplicationUser",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PolicyAcceptedDate",
                table: "ApplicationUser",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuditTrailRecord",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EntityId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EntityType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Operation = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DiffJson = table.Column<string>(type: "longtext", nullable: false)
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
                    table.PrimaryKey("PK_AuditTrailRecord", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "AgencyType",
                keyColumn: "Id",
                keyValue: "27d4c2e0ce1a438cb44cd7fb8ed552b9",
                column: "SubType",
                value: "tele calling");

            migrationBuilder.UpdateData(
                table: "AgencyType",
                keyColumn: "Id",
                keyValue: "ff379ce22f7b4aca9e74d0dadccb3739",
                column: "SubType",
                value: "field agent");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccountJSON_AccountId",
                table: "LoanAccountJSON",
                column: "AccountId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTrailRecord");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccountJSON_AccountId",
                table: "LoanAccountJSON");

            migrationBuilder.DropColumn(
                name: "DeliquencyReason",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "IsPolicyAccepted",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "PolicyAcceptedDate",
                table: "ApplicationUser");

            migrationBuilder.UpdateData(
                table: "AgencyType",
                keyColumn: "Id",
                keyValue: "27d4c2e0ce1a438cb44cd7fb8ed552b9",
                column: "SubType",
                value: "Tele calling");

            migrationBuilder.UpdateData(
                table: "AgencyType",
                keyColumn: "Id",
                keyValue: "ff379ce22f7b4aca9e74d0dadccb3739",
                column: "SubType",
                value: "Field Agents");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccountJSON_AccountId",
                table: "LoanAccountJSON",
                column: "AccountId");
        }
    }
}
