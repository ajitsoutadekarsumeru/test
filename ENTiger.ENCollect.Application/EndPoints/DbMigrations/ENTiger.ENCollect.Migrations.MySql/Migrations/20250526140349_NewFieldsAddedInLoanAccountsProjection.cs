using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class NewFieldsAddedInLoanAccountsProjection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "LastCollectionAmount",
                table: "LoanAccountsProjection",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastCollectionMode",
                table: "LoanAccountsProjection",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastCollectionAmount",
                table: "LoanAccountsProjection");

            migrationBuilder.DropColumn(
                name: "LastCollectionMode",
                table: "LoanAccountsProjection");
        }
    }
}
