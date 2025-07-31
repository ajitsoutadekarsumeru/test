BEGIN TRANSACTION;
GO

DROP INDEX [IX_UserAttendanceLog_Created_DateOnly] ON [UserAttendanceLog];
GO

DROP INDEX [IX_Feedback_Created_DateOnly] ON [Feedback];
GO

DROP INDEX [IX_Collections_Created_DateOnly] ON [Collections];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserAttendanceLog]') AND [c].[name] = N'Created_DateOnly');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [UserAttendanceLog] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [UserAttendanceLog] DROP COLUMN [Created_DateOnly];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Feedback]') AND [c].[name] = N'Created_DateOnly');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Feedback] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Feedback] DROP COLUMN [Created_DateOnly];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Collections]') AND [c].[name] = N'Created_DateOnly');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Collections] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Collections] DROP COLUMN [Created_DateOnly];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[State]') AND [c].[name] = N'Name');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [State] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [State] ALTER COLUMN [Name] nvarchar(450) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Regions]') AND [c].[name] = N'Name');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Regions] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Regions] ALTER COLUMN [Name] nvarchar(450) NOT NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PayInSlips]') AND [c].[name] = N'ProductGroup');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [PayInSlips] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [PayInSlips] ALTER COLUMN [ProductGroup] nvarchar(450) NULL;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PayInSlips]') AND [c].[name] = N'PayinslipType');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [PayInSlips] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [PayInSlips] ALTER COLUMN [PayinslipType] nvarchar(450) NULL;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FeatureMaster]') AND [c].[name] = N'Parameter');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [FeatureMaster] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [FeatureMaster] ALTER COLUMN [Parameter] nvarchar(450) NOT NULL;
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CompanyUserWorkflowState]') AND [c].[name] = N'Name');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [CompanyUserWorkflowState] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [CompanyUserWorkflowState] ALTER COLUMN [Name] nvarchar(450) NOT NULL;
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationTemplateDetail]') AND [c].[name] = N'Name');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationTemplateDetail] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [CommunicationTemplateDetail] ALTER COLUMN [Name] nvarchar(450) NULL;
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationTemplate]') AND [c].[name] = N'TemplateType');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationTemplate] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [CommunicationTemplate] ALTER COLUMN [TemplateType] nvarchar(450) NULL;
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CollectionBatches]') AND [c].[name] = N'ModeOfPayment');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [CollectionBatches] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [CollectionBatches] ALTER COLUMN [ModeOfPayment] nvarchar(450) NULL;
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CollectionBatches]') AND [c].[name] = N'BatchType');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [CollectionBatches] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [CollectionBatches] ALTER COLUMN [BatchType] nvarchar(450) NULL;
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Areas]') AND [c].[name] = N'Name');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Areas] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [Areas] ALTER COLUMN [Name] nvarchar(450) NOT NULL;
GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AgencyWorkflowState]') AND [c].[name] = N'Name');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [AgencyWorkflowState] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [AgencyWorkflowState] ALTER COLUMN [Name] nvarchar(450) NOT NULL;
GO

ALTER TABLE [TreatmentOnCommunicationHistoryDetails] ADD [DeliveryDate_Only] AS CAST(DeliveryDate AS DATE) PERSISTED;
GO

CREATE INDEX [IX_UserVerificationCodeTypes_Description] ON [UserVerificationCodeTypes] ([Description]);
GO

CREATE INDEX [IX_UsersUpdateFile_UploadedDate] ON [UsersUpdateFile] ([UploadedDate]);
GO

CREATE INDEX [IX_UsersCreateFile_Status] ON [UsersCreateFile] ([Status]);
GO

CREATE INDEX [IX_UserAttendanceLog_CreatedDate] ON [UserAttendanceLog] ([CreatedDate]);
GO

CREATE INDEX [IX_UserAttendanceDetail_Date] ON [UserAttendanceDetail] ([Date]);
GO

CREATE INDEX [IX_TreatmentOnCommunicationHistoryDetails_DeliveryDate] ON [TreatmentOnCommunicationHistoryDetails] ([DeliveryDate]);
GO

CREATE INDEX [IX_TreatmentOnCommunicationHistoryDetails_DeliveryDate_Only] ON [TreatmentOnCommunicationHistoryDetails] ([DeliveryDate_Only]);
GO

CREATE INDEX [IX_Treatment_Mode] ON [Treatment] ([Mode]);
GO

CREATE INDEX [IX_Treatment_Name] ON [Treatment] ([Name]);
GO

CREATE INDEX [IX_State_Name] ON [State] ([Name]);
GO

CREATE INDEX [IX_State_NickName] ON [State] ([NickName]);
GO

CREATE INDEX [IX_Segmentation_BOM_Bucket] ON [Segmentation] ([BOM_Bucket]);
GO

CREATE INDEX [IX_Segmentation_City] ON [Segmentation] ([City]);
GO

CREATE INDEX [IX_Segmentation_CreatedDate] ON [Segmentation] ([CreatedDate]);
GO

CREATE INDEX [IX_Segmentation_CurrentBucket] ON [Segmentation] ([CurrentBucket]);
GO

CREATE INDEX [IX_Segmentation_ExecutionType] ON [Segmentation] ([ExecutionType]);
GO

CREATE INDEX [IX_Segmentation_Name] ON [Segmentation] ([Name]);
GO

CREATE INDEX [IX_Segmentation_Product] ON [Segmentation] ([Product]);
GO

CREATE INDEX [IX_Segmentation_ProductGroup] ON [Segmentation] ([ProductGroup]);
GO

CREATE INDEX [IX_Segmentation_State] ON [Segmentation] ([State]);
GO

CREATE INDEX [IX_Segmentation_SubProduct] ON [Segmentation] ([SubProduct]);
GO

CREATE INDEX [IX_Segmentation_Zone] ON [Segmentation] ([Zone]);
GO

CREATE INDEX [IX_SecondaryUnAllocationFile_UploadedDate] ON [SecondaryUnAllocationFile] ([UploadedDate]);
GO

CREATE INDEX [IX_SecondaryAllocationFile_FileName] ON [SecondaryAllocationFile] ([FileName]);
GO

CREATE INDEX [IX_SecondaryAllocationFile_FileUploadedDate] ON [SecondaryAllocationFile] ([FileUploadedDate]);
GO

CREATE INDEX [IX_Regions_Name] ON [Regions] ([Name]);
GO

CREATE INDEX [IX_Regions_NickName] ON [Regions] ([NickName]);
GO

CREATE INDEX [IX_PrimaryUnAllocationFile_UploadedDate] ON [PrimaryUnAllocationFile] ([UploadedDate]);
GO

CREATE INDEX [IX_PrimaryAllocationFile_FileUploadedDate] ON [PrimaryAllocationFile] ([FileUploadedDate]);
GO

CREATE INDEX [IX_PayInSlips_CreatedDate] ON [PayInSlips] ([CreatedDate]);
GO

CREATE INDEX [IX_PayInSlips_ModeOfPayment] ON [PayInSlips] ([ModeOfPayment]);
GO

CREATE INDEX [IX_PayInSlips_PayinslipType] ON [PayInSlips] ([PayinslipType]);
GO

CREATE INDEX [IX_PayInSlips_ProductGroup] ON [PayInSlips] ([ProductGroup]);
GO

CREATE INDEX [IX_MasterFileStatus_FileName] ON [MasterFileStatus] ([FileName]);
GO

CREATE INDEX [IX_MasterFileStatus_FileUploadedDate] ON [MasterFileStatus] ([FileUploadedDate]);
GO

CREATE INDEX [IX_MasterFileStatus_Status] ON [MasterFileStatus] ([Status]);
GO

CREATE INDEX [IX_MasterFileStatus_UploadType] ON [MasterFileStatus] ([UploadType]);
GO

CREATE INDEX [IX_LoanAccounts_BillingCycle] ON [LoanAccounts] ([BILLING_CYCLE]);
GO

CREATE INDEX [IX_LoanAccounts_BranchCode] ON [LoanAccounts] ([BranchCode]);
GO

CREATE INDEX [IX_LoanAccounts_Bucket] ON [LoanAccounts] ([BUCKET]);
GO

CREATE INDEX [IX_LoanAccounts_City] ON [LoanAccounts] ([CITY]);
GO

CREATE INDEX [IX_LoanAccounts_DateOfBirth] ON [LoanAccounts] ([DateOfBirth]);
GO

CREATE INDEX [IX_LoanAccounts_DispCode] ON [LoanAccounts] ([DispCode]);
GO

CREATE INDEX [IX_LoanAccounts_LastUploadedDate] ON [LoanAccounts] ([LastUploadedDate]);
GO

CREATE INDEX [IX_LoanAccounts_LatestFeedbackDate] ON [LoanAccounts] ([LatestFeedbackDate]);
GO

CREATE INDEX [IX_LoanAccounts_LatestPaymentDate] ON [LoanAccounts] ([LatestPaymentDate]);
GO

CREATE INDEX [IX_LoanAccounts_LatestPTPDate] ON [LoanAccounts] ([LatestPTPDate]);
GO

CREATE INDEX [IX_LoanAccounts_NPAStageId] ON [LoanAccounts] ([NPA_STAGEID]);
GO

CREATE INDEX [IX_LoanAccounts_PaymentStatus] ON [LoanAccounts] ([PAYMENTSTATUS]);
GO

CREATE INDEX [IX_LoanAccounts_ProductCode] ON [LoanAccounts] ([ProductCode]);
GO

CREATE INDEX [IX_LoanAccounts_ProductGroup] ON [LoanAccounts] ([ProductGroup]);
GO

CREATE INDEX [IX_LoanAccounts_Region] ON [LoanAccounts] ([Region]);
GO

CREATE INDEX [IX_LoanAccounts_State] ON [LoanAccounts] ([STATE]);
GO

CREATE INDEX [IX_LoanAccounts_SubProduct] ON [LoanAccounts] ([SubProduct]);
GO

CREATE INDEX [IX_Feedback_DispositionCode] ON [Feedback] ([DispositionCode]);
GO

CREATE INDEX [IX_FeatureMaster_Parameter] ON [FeatureMaster] ([Parameter]);
GO

CREATE INDEX [IX_Designation_Acronym] ON [Designation] ([Acronym]);
GO

CREATE INDEX [IX_Designation_Name] ON [Designation] ([Name]);
GO

CREATE INDEX [IX_Department_Code] ON [Department] ([Code]);
GO

CREATE INDEX [IX_Department_Name] ON [Department] ([Name]);
GO

CREATE INDEX [IX_Countries_Name] ON [Countries] ([Name]);
GO

CREATE INDEX [IX_Countries_NickName] ON [Countries] ([NickName]);
GO

CREATE INDEX [IX_CompanyUserWorkflowState_Name] ON [CompanyUserWorkflowState] ([Name]);
GO

CREATE INDEX [IX_CommunicationTemplateDetail_Name] ON [CommunicationTemplateDetail] ([Name]);
GO

CREATE INDEX [IX_CommunicationTemplate_CreatedDate] ON [CommunicationTemplate] ([CreatedDate]);
GO

CREATE INDEX [IX_CommunicationTemplate_TemplateType] ON [CommunicationTemplate] ([TemplateType]);
GO

CREATE INDEX [IX_Collections_AcknowledgedDate] ON [Collections] ([AcknowledgedDate]);
GO

CREATE INDEX [IX_Collections_CollectionDate] ON [Collections] ([CollectionDate]);
GO

CREATE INDEX [IX_Collections_CollectionMode] ON [Collections] ([CollectionMode]);
GO

CREATE INDEX [IX_Collections_CreatedDate] ON [Collections] ([CreatedDate]);
GO

CREATE INDEX [IX_Collections_CustomerName] ON [Collections] ([CustomerName]);
GO

CREATE INDEX [IX_CollectionBatches_BatchType] ON [CollectionBatches] ([BatchType]);
GO

CREATE INDEX [IX_CollectionBatches_CreatedDate] ON [CollectionBatches] ([CreatedDate]);
GO

CREATE INDEX [IX_CollectionBatches_ModeOfPayment] ON [CollectionBatches] ([ModeOfPayment]);
GO

CREATE INDEX [IX_CollectionBatches_ProductGroup] ON [CollectionBatches] ([ProductGroup]);
GO

CREATE INDEX [IX_Cities_NickName] ON [Cities] ([NickName]);
GO

CREATE INDEX [IX_Cheques_InstrumentDate] ON [Cheques] ([InstrumentDate]);
GO

CREATE INDEX [IX_CategoryMaster_Name] ON [CategoryMaster] ([Name]);
GO

CREATE INDEX [IX_CategoryItem_Code] ON [CategoryItem] ([Code]);
GO

CREATE INDEX [IX_CategoryItem_Name] ON [CategoryItem] ([Name]);
GO

CREATE INDEX [IX_BulkTrailUploadFile_FileUploadedDate] ON [BulkTrailUploadFile] ([FileUploadedDate]);
GO

CREATE INDEX [IX_Area_Name] ON [Areas] ([Name]);
GO

CREATE INDEX [IX_Area_NickName] ON [Areas] ([NickName]);
GO

CREATE INDEX [IX_ApplicationUser_AuthorizationCardExpiryDate] ON [ApplicationUser] ([AuthorizationCardExpiryDate]);
GO

CREATE INDEX [IX_ApplicationUser_PrimaryEMail] ON [ApplicationUser] ([PrimaryEMail]);
GO

CREATE INDEX [IX_CompanyUser_FirstName] ON [ApplicationUser] ([FirstName]);
GO

CREATE INDEX [IX_CompanyUser_LastName] ON [ApplicationUser] ([LastName]);
GO

CREATE INDEX [IX_ApplicationOrg_ContractExpireDate] ON [ApplicationOrg] ([ContractExpireDate]);
GO

CREATE INDEX [IX_ApplicationOrg_FirstName] ON [ApplicationOrg] ([FirstName]);
GO

CREATE INDEX [IX_ApplicationOrg_LastName] ON [ApplicationOrg] ([LastName]);
GO

CREATE INDEX [IX_AgencyWorkflowState_Name] ON [AgencyWorkflowState] ([Name]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250228085449_AddedCreatedDateIndexes', N'8.0.10');
GO

COMMIT;
GO

