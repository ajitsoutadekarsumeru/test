using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENTiger.ENCollect.Migrations.MySql.Migrations
{
    /// <inheritdoc />
    public partial class AddedCreatedDateIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GeoLocation",
                table: "UserAttendanceLog",
                type: "varchar(800)",
                maxLength: 800,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstLogin",
                table: "UserAttendanceLog",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "State",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Regions",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ProductGroup",
                table: "PayInSlips",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "PayinslipType",
                table: "PayInSlips",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Parameter",
                table: "FeatureMaster",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CompanyUserWorkflowState",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CommunicationTemplateDetail",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TemplateType",
                table: "CommunicationTemplate",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ModeOfPayment",
                table: "CollectionBatches",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "BatchType",
                table: "CollectionBatches",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Areas",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AgencyWorkflowState",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DeliveryDate_Only",
                table: "TreatmentOnCommunicationHistoryDetails",
                type: "date",
                nullable: true,
                computedColumnSql: "CAST(DeliveryDate AS DATE)",
                stored: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserVerificationCodeTypes_Description",
                table: "UserVerificationCodeTypes",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_UsersUpdateFile_UploadedDate",
                table: "UsersUpdateFile",
                column: "UploadedDate");

            migrationBuilder.CreateIndex(
                name: "IX_UsersCreateFile_Status",
                table: "UsersCreateFile",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_UserAttendanceLog_CreatedDate",
                table: "UserAttendanceLog",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_UserAttendanceDetail_Date",
                table: "UserAttendanceDetail",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnCommunicationHistoryDetails_DeliveryDate",
                table: "TreatmentOnCommunicationHistoryDetails",
                column: "DeliveryDate");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentOnCommunicationHistoryDetails_DeliveryDate_Only",
                table: "TreatmentOnCommunicationHistoryDetails",
                column: "DeliveryDate_Only");

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_Mode",
                table: "Treatment",
                column: "Mode");

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_Name",
                table: "Treatment",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_State_Name",
                table: "State",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_State_NickName",
                table: "State",
                column: "NickName");

            migrationBuilder.CreateIndex(
                name: "IX_Segmentation_BOM_Bucket",
                table: "Segmentation",
                column: "BOM_Bucket");

            migrationBuilder.CreateIndex(
                name: "IX_Segmentation_City",
                table: "Segmentation",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_Segmentation_CreatedDate",
                table: "Segmentation",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Segmentation_CurrentBucket",
                table: "Segmentation",
                column: "CurrentBucket");

            migrationBuilder.CreateIndex(
                name: "IX_Segmentation_ExecutionType",
                table: "Segmentation",
                column: "ExecutionType");

            migrationBuilder.CreateIndex(
                name: "IX_Segmentation_Name",
                table: "Segmentation",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Segmentation_Product",
                table: "Segmentation",
                column: "Product");

            migrationBuilder.CreateIndex(
                name: "IX_Segmentation_ProductGroup",
                table: "Segmentation",
                column: "ProductGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Segmentation_State",
                table: "Segmentation",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Segmentation_SubProduct",
                table: "Segmentation",
                column: "SubProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Segmentation_Zone",
                table: "Segmentation",
                column: "Zone");

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryUnAllocationFile_UploadedDate",
                table: "SecondaryUnAllocationFile",
                column: "UploadedDate");

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryAllocationFile_FileName",
                table: "SecondaryAllocationFile",
                column: "FileName");

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryAllocationFile_FileUploadedDate",
                table: "SecondaryAllocationFile",
                column: "FileUploadedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Name",
                table: "Regions",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_NickName",
                table: "Regions",
                column: "NickName");

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryUnAllocationFile_UploadedDate",
                table: "PrimaryUnAllocationFile",
                column: "UploadedDate");

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryAllocationFile_FileUploadedDate",
                table: "PrimaryAllocationFile",
                column: "FileUploadedDate");

            migrationBuilder.CreateIndex(
                name: "IX_PayInSlips_CreatedDate",
                table: "PayInSlips",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_PayInSlips_ModeOfPayment",
                table: "PayInSlips",
                column: "ModeOfPayment");

            migrationBuilder.CreateIndex(
                name: "IX_PayInSlips_PayinslipType",
                table: "PayInSlips",
                column: "PayinslipType");

            migrationBuilder.CreateIndex(
                name: "IX_PayInSlips_ProductGroup",
                table: "PayInSlips",
                column: "ProductGroup");

            migrationBuilder.CreateIndex(
                name: "IX_MasterFileStatus_FileName",
                table: "MasterFileStatus",
                column: "FileName");

            migrationBuilder.CreateIndex(
                name: "IX_MasterFileStatus_FileUploadedDate",
                table: "MasterFileStatus",
                column: "FileUploadedDate");

            migrationBuilder.CreateIndex(
                name: "IX_MasterFileStatus_Status",
                table: "MasterFileStatus",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_MasterFileStatus_UploadType",
                table: "MasterFileStatus",
                column: "UploadType");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_BillingCycle",
                table: "LoanAccounts",
                column: "BILLING_CYCLE");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_BranchCode",
                table: "LoanAccounts",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_Bucket",
                table: "LoanAccounts",
                column: "BUCKET");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_City",
                table: "LoanAccounts",
                column: "CITY");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_DateOfBirth",
                table: "LoanAccounts",
                column: "DateOfBirth");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_DispCode",
                table: "LoanAccounts",
                column: "DispCode");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_LastUploadedDate",
                table: "LoanAccounts",
                column: "LastUploadedDate");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_LatestFeedbackDate",
                table: "LoanAccounts",
                column: "LatestFeedbackDate");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_LatestPaymentDate",
                table: "LoanAccounts",
                column: "LatestPaymentDate");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_LatestPTPDate",
                table: "LoanAccounts",
                column: "LatestPTPDate");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_NPAStageId",
                table: "LoanAccounts",
                column: "NPA_STAGEID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_PaymentStatus",
                table: "LoanAccounts",
                column: "PAYMENTSTATUS");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_ProductCode",
                table: "LoanAccounts",
                column: "ProductCode");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_ProductGroup",
                table: "LoanAccounts",
                column: "ProductGroup");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_Region",
                table: "LoanAccounts",
                column: "Region");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_State",
                table: "LoanAccounts",
                column: "STATE");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccounts_SubProduct",
                table: "LoanAccounts",
                column: "SubProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_DispositionCode",
                table: "Feedback",
                column: "DispositionCode");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureMaster_Parameter",
                table: "FeatureMaster",
                column: "Parameter");

            migrationBuilder.CreateIndex(
                name: "IX_Designation_Acronym",
                table: "Designation",
                column: "Acronym");

            migrationBuilder.CreateIndex(
                name: "IX_Designation_Name",
                table: "Designation",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Department_Code",
                table: "Department",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Department_Name",
                table: "Department",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name",
                table: "Countries",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_NickName",
                table: "Countries",
                column: "NickName");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserWorkflowState_Name",
                table: "CompanyUserWorkflowState",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTemplateDetail_Name",
                table: "CommunicationTemplateDetail",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTemplate_CreatedDate",
                table: "CommunicationTemplate",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationTemplate_TemplateType",
                table: "CommunicationTemplate",
                column: "TemplateType");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_AcknowledgedDate",
                table: "Collections",
                column: "AcknowledgedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_CollectionDate",
                table: "Collections",
                column: "CollectionDate");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_CollectionMode",
                table: "Collections",
                column: "CollectionMode");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_CreatedDate",
                table: "Collections",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_CustomerName",
                table: "Collections",
                column: "CustomerName");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionBatches_BatchType",
                table: "CollectionBatches",
                column: "BatchType");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionBatches_CreatedDate",
                table: "CollectionBatches",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionBatches_ModeOfPayment",
                table: "CollectionBatches",
                column: "ModeOfPayment");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionBatches_ProductGroup",
                table: "CollectionBatches",
                column: "ProductGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_NickName",
                table: "Cities",
                column: "NickName");

            migrationBuilder.CreateIndex(
                name: "IX_Cheques_InstrumentDate",
                table: "Cheques",
                column: "InstrumentDate");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryMaster_Name",
                table: "CategoryMaster",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItem_Code",
                table: "CategoryItem",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItem_Name",
                table: "CategoryItem",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BulkTrailUploadFile_FileUploadedDate",
                table: "BulkTrailUploadFile",
                column: "FileUploadedDate");

            migrationBuilder.CreateIndex(
                name: "IX_Area_Name",
                table: "Areas",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Area_NickName",
                table: "Areas",
                column: "NickName");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_AuthorizationCardExpiryDate",
                table: "ApplicationUser",
                column: "AuthorizationCardExpiryDate");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_PrimaryEMail",
                table: "ApplicationUser",
                column: "PrimaryEMail");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUser_FirstName",
                table: "ApplicationUser",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUser_LastName",
                table: "ApplicationUser",
                column: "LastName");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOrg_ContractExpireDate",
                table: "ApplicationOrg",
                column: "ContractExpireDate");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOrg_FirstName",
                table: "ApplicationOrg",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationOrg_LastName",
                table: "ApplicationOrg",
                column: "LastName");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyWorkflowState_Name",
                table: "AgencyWorkflowState",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserVerificationCodeTypes_Description",
                table: "UserVerificationCodeTypes");

            migrationBuilder.DropIndex(
                name: "IX_UsersUpdateFile_UploadedDate",
                table: "UsersUpdateFile");

            migrationBuilder.DropIndex(
                name: "IX_UsersCreateFile_Status",
                table: "UsersCreateFile");

            migrationBuilder.DropIndex(
                name: "IX_UserAttendanceLog_CreatedDate",
                table: "UserAttendanceLog");

            migrationBuilder.DropIndex(
                name: "IX_UserAttendanceDetail_Date",
                table: "UserAttendanceDetail");

            migrationBuilder.DropIndex(
                name: "IX_TreatmentOnCommunicationHistoryDetails_DeliveryDate",
                table: "TreatmentOnCommunicationHistoryDetails");

            migrationBuilder.DropIndex(
                name: "IX_TreatmentOnCommunicationHistoryDetails_DeliveryDate_Only",
                table: "TreatmentOnCommunicationHistoryDetails");

            migrationBuilder.DropIndex(
                name: "IX_Treatment_Mode",
                table: "Treatment");

            migrationBuilder.DropIndex(
                name: "IX_Treatment_Name",
                table: "Treatment");

            migrationBuilder.DropIndex(
                name: "IX_State_Name",
                table: "State");

            migrationBuilder.DropIndex(
                name: "IX_State_NickName",
                table: "State");

            migrationBuilder.DropIndex(
                name: "IX_Segmentation_BOM_Bucket",
                table: "Segmentation");

            migrationBuilder.DropIndex(
                name: "IX_Segmentation_City",
                table: "Segmentation");

            migrationBuilder.DropIndex(
                name: "IX_Segmentation_CreatedDate",
                table: "Segmentation");

            migrationBuilder.DropIndex(
                name: "IX_Segmentation_CurrentBucket",
                table: "Segmentation");

            migrationBuilder.DropIndex(
                name: "IX_Segmentation_ExecutionType",
                table: "Segmentation");

            migrationBuilder.DropIndex(
                name: "IX_Segmentation_Name",
                table: "Segmentation");

            migrationBuilder.DropIndex(
                name: "IX_Segmentation_Product",
                table: "Segmentation");

            migrationBuilder.DropIndex(
                name: "IX_Segmentation_ProductGroup",
                table: "Segmentation");

            migrationBuilder.DropIndex(
                name: "IX_Segmentation_State",
                table: "Segmentation");

            migrationBuilder.DropIndex(
                name: "IX_Segmentation_SubProduct",
                table: "Segmentation");

            migrationBuilder.DropIndex(
                name: "IX_Segmentation_Zone",
                table: "Segmentation");

            migrationBuilder.DropIndex(
                name: "IX_SecondaryUnAllocationFile_UploadedDate",
                table: "SecondaryUnAllocationFile");

            migrationBuilder.DropIndex(
                name: "IX_SecondaryAllocationFile_FileName",
                table: "SecondaryAllocationFile");

            migrationBuilder.DropIndex(
                name: "IX_SecondaryAllocationFile_FileUploadedDate",
                table: "SecondaryAllocationFile");

            migrationBuilder.DropIndex(
                name: "IX_Regions_Name",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_Regions_NickName",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_PrimaryUnAllocationFile_UploadedDate",
                table: "PrimaryUnAllocationFile");

            migrationBuilder.DropIndex(
                name: "IX_PrimaryAllocationFile_FileUploadedDate",
                table: "PrimaryAllocationFile");

            migrationBuilder.DropIndex(
                name: "IX_PayInSlips_CreatedDate",
                table: "PayInSlips");

            migrationBuilder.DropIndex(
                name: "IX_PayInSlips_ModeOfPayment",
                table: "PayInSlips");

            migrationBuilder.DropIndex(
                name: "IX_PayInSlips_PayinslipType",
                table: "PayInSlips");

            migrationBuilder.DropIndex(
                name: "IX_PayInSlips_ProductGroup",
                table: "PayInSlips");

            migrationBuilder.DropIndex(
                name: "IX_MasterFileStatus_FileName",
                table: "MasterFileStatus");

            migrationBuilder.DropIndex(
                name: "IX_MasterFileStatus_FileUploadedDate",
                table: "MasterFileStatus");

            migrationBuilder.DropIndex(
                name: "IX_MasterFileStatus_Status",
                table: "MasterFileStatus");

            migrationBuilder.DropIndex(
                name: "IX_MasterFileStatus_UploadType",
                table: "MasterFileStatus");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_BillingCycle",
                table: "LoanAccounts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_BranchCode",
                table: "LoanAccounts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_Bucket",
                table: "LoanAccounts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_City",
                table: "LoanAccounts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_DateOfBirth",
                table: "LoanAccounts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_DispCode",
                table: "LoanAccounts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_LastUploadedDate",
                table: "LoanAccounts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_LatestFeedbackDate",
                table: "LoanAccounts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_LatestPaymentDate",
                table: "LoanAccounts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_LatestPTPDate",
                table: "LoanAccounts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_NPAStageId",
                table: "LoanAccounts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_PaymentStatus",
                table: "LoanAccounts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_ProductCode",
                table: "LoanAccounts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_ProductGroup",
                table: "LoanAccounts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_Region",
                table: "LoanAccounts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_State",
                table: "LoanAccounts");

            migrationBuilder.DropIndex(
                name: "IX_LoanAccounts_SubProduct",
                table: "LoanAccounts");

            migrationBuilder.DropIndex(
                name: "IX_Feedback_DispositionCode",
                table: "Feedback");

            migrationBuilder.DropIndex(
                name: "IX_FeatureMaster_Parameter",
                table: "FeatureMaster");

            migrationBuilder.DropIndex(
                name: "IX_Designation_Acronym",
                table: "Designation");

            migrationBuilder.DropIndex(
                name: "IX_Designation_Name",
                table: "Designation");

            migrationBuilder.DropIndex(
                name: "IX_Department_Code",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_Name",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Countries_Name",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_NickName",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_CompanyUserWorkflowState_Name",
                table: "CompanyUserWorkflowState");

            migrationBuilder.DropIndex(
                name: "IX_CommunicationTemplateDetail_Name",
                table: "CommunicationTemplateDetail");

            migrationBuilder.DropIndex(
                name: "IX_CommunicationTemplate_CreatedDate",
                table: "CommunicationTemplate");

            migrationBuilder.DropIndex(
                name: "IX_CommunicationTemplate_TemplateType",
                table: "CommunicationTemplate");

            migrationBuilder.DropIndex(
                name: "IX_Collections_AcknowledgedDate",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_CollectionDate",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_CollectionMode",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_CreatedDate",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_CustomerName",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_CollectionBatches_BatchType",
                table: "CollectionBatches");

            migrationBuilder.DropIndex(
                name: "IX_CollectionBatches_CreatedDate",
                table: "CollectionBatches");

            migrationBuilder.DropIndex(
                name: "IX_CollectionBatches_ModeOfPayment",
                table: "CollectionBatches");

            migrationBuilder.DropIndex(
                name: "IX_CollectionBatches_ProductGroup",
                table: "CollectionBatches");

            migrationBuilder.DropIndex(
                name: "IX_Cities_NickName",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cheques_InstrumentDate",
                table: "Cheques");

            migrationBuilder.DropIndex(
                name: "IX_CategoryMaster_Name",
                table: "CategoryMaster");

            migrationBuilder.DropIndex(
                name: "IX_CategoryItem_Code",
                table: "CategoryItem");

            migrationBuilder.DropIndex(
                name: "IX_CategoryItem_Name",
                table: "CategoryItem");

            migrationBuilder.DropIndex(
                name: "IX_BulkTrailUploadFile_FileUploadedDate",
                table: "BulkTrailUploadFile");

            migrationBuilder.DropIndex(
                name: "IX_Area_Name",
                table: "Areas");

            migrationBuilder.DropIndex(
                name: "IX_Area_NickName",
                table: "Areas");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_AuthorizationCardExpiryDate",
                table: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_PrimaryEMail",
                table: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_CompanyUser_FirstName",
                table: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_CompanyUser_LastName",
                table: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationOrg_ContractExpireDate",
                table: "ApplicationOrg");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationOrg_FirstName",
                table: "ApplicationOrg");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationOrg_LastName",
                table: "ApplicationOrg");

            migrationBuilder.DropIndex(
                name: "IX_AgencyWorkflowState_Name",
                table: "AgencyWorkflowState");

            migrationBuilder.DropColumn(
                name: "DeliveryDate_Only",
                table: "TreatmentOnCommunicationHistoryDetails");

            migrationBuilder.DropColumn(
                name: "GeoLocation",
                table: "UserAttendanceLog");

            migrationBuilder.DropColumn(
                name: "IsFirstLogin",
                table: "UserAttendanceLog");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "State",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Regions",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ProductGroup",
                table: "PayInSlips",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "PayinslipType",
                table: "PayInSlips",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Parameter",
                table: "FeatureMaster",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CompanyUserWorkflowState",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CommunicationTemplateDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TemplateType",
                table: "CommunicationTemplate",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ModeOfPayment",
                table: "CollectionBatches",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "BatchType",
                table: "CollectionBatches",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Areas",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AgencyWorkflowState",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
