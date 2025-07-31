using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedAccountContactDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latest_Address_From_Trail",
                table: "LoanAccounts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Latest_Email_From_Trail",
                table: "LoanAccounts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Latest_Number_From_Receipt",
                table: "LoanAccounts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Latest_Number_From_Send_Payment",
                table: "LoanAccounts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Latest_Number_From_Trail",
                table: "LoanAccounts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SegmentId",
                table: "LoanAccounts",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_SegmentId",
                table: "LoanAccounts",
                column: "SegmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanAccounts_Segmentation_SegmentId",
                table: "LoanAccounts",
                column: "SegmentId",
                principalTable: "Segmentation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanAccounts_Segmentation_SegmentId",
                table: "LoanAccounts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_SegmentId",
                table: "LoanAccounts");

            migrationBuilder.DropColumn(
                name: "Latest_Address_From_Trail",
                table: "LoanAccounts");

            migrationBuilder.DropColumn(
                name: "Latest_Email_From_Trail",
                table: "LoanAccounts");

            migrationBuilder.DropColumn(
                name: "Latest_Number_From_Receipt",
                table: "LoanAccounts");

            migrationBuilder.DropColumn(
                name: "Latest_Number_From_Send_Payment",
                table: "LoanAccounts");

            migrationBuilder.DropColumn(
                name: "Latest_Number_From_Trail",
                table: "LoanAccounts");

            migrationBuilder.DropColumn(
                name: "SegmentId",
                table: "LoanAccounts");
        }
    }
}
