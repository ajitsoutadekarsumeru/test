using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedLastXDigitsOfAccountNoAndPrimaryCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastXDigitsOfAgreementId",
                table: "LoanAccounts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastXDigitsOfPrimaryCard",
                table: "LoanAccounts",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastXDigitsOfAgreementId",
                table: "LoanAccounts");

            migrationBuilder.DropColumn(
                name: "LastXDigitsOfPrimaryCard",
                table: "LoanAccounts");
        }
    }
}
