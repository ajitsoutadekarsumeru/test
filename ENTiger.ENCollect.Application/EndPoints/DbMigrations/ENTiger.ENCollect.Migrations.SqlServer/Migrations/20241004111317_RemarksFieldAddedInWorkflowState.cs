using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class RemarksFieldAddedInWorkflowState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "ReceiptWorkflowState",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "PayInSlipWorkflowState",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "CompanyUserWorkflowState",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "CommunicationTemplateWorkflowState",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "CollectionWorkflowState",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "CollectionBatchWorkflowState",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "AgencyWorkflowState",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "AgencyUserWorkflowState",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "ReceiptWorkflowState");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "PayInSlipWorkflowState");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "CompanyUserWorkflowState");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "CommunicationTemplateWorkflowState");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "CollectionWorkflowState");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "CollectionBatchWorkflowState");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "AgencyWorkflowState");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "AgencyUserWorkflowState");
        }
    }
}
