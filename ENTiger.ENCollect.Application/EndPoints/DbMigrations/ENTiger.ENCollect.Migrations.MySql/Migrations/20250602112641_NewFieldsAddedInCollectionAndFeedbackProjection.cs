using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class NewFieldsAddedInCollectionAndFeedbackProjection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastDispositionCode",
                table: "FeedbackProjection",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LastDispositionCodeGroup",
                table: "FeedbackProjection",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastDispositionDate",
                table: "FeedbackProjection",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPTPDate",
                table: "FeedbackProjection",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CURRENT_TOTAL_AMOUNT_DUE",
                table: "CollectionProjection",
                type: "decimal(16,2)",
                nullable: true);

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastDispositionCode",
                table: "FeedbackProjection");

            migrationBuilder.DropColumn(
                name: "LastDispositionCodeGroup",
                table: "FeedbackProjection");

            migrationBuilder.DropColumn(
                name: "LastDispositionDate",
                table: "FeedbackProjection");

            migrationBuilder.DropColumn(
                name: "LastPTPDate",
                table: "FeedbackProjection");

            migrationBuilder.DropColumn(
                name: "CURRENT_TOTAL_AMOUNT_DUE",
                table: "CollectionProjection");

         
        }
    }
}
