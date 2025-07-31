using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddedGeoLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GeoLocation",
                table: "UserAttendanceLog",
                type: "nvarchar(800)",
                maxLength: 800,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstLogin",
                table: "UserAttendanceLog",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Created_DateOnly",
                table: "UserAttendanceLog",
                type: "date",
                nullable: true,
                computedColumnSql: "CAST(CreatedDate AS DATE)",
                stored: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Created_DateOnly",
                table: "Feedback",
                type: "date",
                nullable: true,
                computedColumnSql: "CAST(CreatedDate AS DATE)",
                stored: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Created_DateOnly",
                table: "Collections",
                type: "date",
                nullable: true,
                computedColumnSql: "CAST(CreatedDate AS DATE)",
                stored: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAttendanceLog_Created_DateOnly",
                table: "UserAttendanceLog",
                column: "Created_DateOnly");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_Created_DateOnly",
                table: "Feedback",
                column: "Created_DateOnly");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_Created_DateOnly",
                table: "Collections",
                column: "Created_DateOnly");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserAttendanceLog_Created_DateOnly",
                table: "UserAttendanceLog");

            migrationBuilder.DropIndex(
                name: "IX_Feedback_Created_DateOnly",
                table: "Feedback");

            migrationBuilder.DropIndex(
                name: "IX_Collections_Created_DateOnly",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "Created_DateOnly",
                table: "UserAttendanceLog");

            migrationBuilder.DropColumn(
                name: "Created_DateOnly",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "Created_DateOnly",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "GeoLocation",
                table: "UserAttendanceLog");

            migrationBuilder.DropColumn(
                name: "IsFirstLogin",
                table: "UserAttendanceLog");
        }
    }
}
