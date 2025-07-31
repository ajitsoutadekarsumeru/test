using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class TransactionSourceFieldAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransactionSource",
                table: "UserAttendanceLog",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionSource",
                table: "PayInSlips",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionSource",
                table: "Feedback",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DispositionCodeCustomerOrAccountLevel",
                table: "DispositionCodeMaster",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionSource",
                table: "Collections",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AgencyType",
                keyColumn: "Id",
                keyValue: "ff379ce22f7b4aca9e74d0dadccb3739",
                column: "SubType",
                value: "field agents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionSource",
                table: "UserAttendanceLog");

            migrationBuilder.DropColumn(
                name: "TransactionSource",
                table: "PayInSlips");

            migrationBuilder.DropColumn(
                name: "TransactionSource",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "DispositionCodeCustomerOrAccountLevel",
                table: "DispositionCodeMaster");

            migrationBuilder.DropColumn(
                name: "TransactionSource",
                table: "Collections");

            migrationBuilder.UpdateData(
                table: "AgencyType",
                keyColumn: "Id",
                keyValue: "ff379ce22f7b4aca9e74d0dadccb3739",
                column: "SubType",
                value: "field agent");
        }
    }
}
