START TRANSACTION;

ALTER TABLE `UserAttendanceLog` ADD `GeoLocation` varchar(800) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `UserAttendanceLog` ADD `IsFirstLogin` tinyint(1) NULL;

ALTER TABLE `State` MODIFY COLUMN `Name` varchar(255) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Regions` MODIFY COLUMN `Name` varchar(255) CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `PayInSlips` MODIFY COLUMN `ProductGroup` varchar(255) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `PayInSlips` MODIFY COLUMN `PayinslipType` varchar(255) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `FeatureMaster` MODIFY COLUMN `Parameter` varchar(255) CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `CompanyUserWorkflowState` MODIFY COLUMN `Name` varchar(255) CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `CommunicationTemplateDetail` MODIFY COLUMN `Name` varchar(255) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `CommunicationTemplate` MODIFY COLUMN `TemplateType` varchar(255) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `CollectionBatches` MODIFY COLUMN `ModeOfPayment` varchar(255) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `CollectionBatches` MODIFY COLUMN `BatchType` varchar(255) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Areas` MODIFY COLUMN `Name` varchar(255) CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `AgencyWorkflowState` MODIFY COLUMN `Name` varchar(255) CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `TreatmentOnCommunicationHistoryDetails` ADD `DeliveryDate_Only` date AS (CAST(DeliveryDate AS DATE)) STORED NULL;

CREATE INDEX `IX_UserVerificationCodeTypes_Description` ON `UserVerificationCodeTypes` (`Description`);

CREATE INDEX `IX_UsersUpdateFile_UploadedDate` ON `UsersUpdateFile` (`UploadedDate`);

CREATE INDEX `IX_UsersCreateFile_Status` ON `UsersCreateFile` (`Status`);

CREATE INDEX `IX_UserAttendanceLog_CreatedDate` ON `UserAttendanceLog` (`CreatedDate`);

CREATE INDEX `IX_UserAttendanceDetail_Date` ON `UserAttendanceDetail` (`Date`);

CREATE INDEX `IX_TreatmentOnCommunicationHistoryDetails_DeliveryDate` ON `TreatmentOnCommunicationHistoryDetails` (`DeliveryDate`);

CREATE INDEX `IX_TreatmentOnCommunicationHistoryDetails_DeliveryDate_Only` ON `TreatmentOnCommunicationHistoryDetails` (`DeliveryDate_Only`);

CREATE INDEX `IX_Treatment_Mode` ON `Treatment` (`Mode`);

CREATE INDEX `IX_Treatment_Name` ON `Treatment` (`Name`);

CREATE INDEX `IX_State_Name` ON `State` (`Name`);

CREATE INDEX `IX_State_NickName` ON `State` (`NickName`);

CREATE INDEX `IX_Segmentation_BOM_Bucket` ON `Segmentation` (`BOM_Bucket`);

CREATE INDEX `IX_Segmentation_City` ON `Segmentation` (`City`);

CREATE INDEX `IX_Segmentation_CreatedDate` ON `Segmentation` (`CreatedDate`);

CREATE INDEX `IX_Segmentation_CurrentBucket` ON `Segmentation` (`CurrentBucket`);

CREATE INDEX `IX_Segmentation_ExecutionType` ON `Segmentation` (`ExecutionType`);

CREATE INDEX `IX_Segmentation_Name` ON `Segmentation` (`Name`);

CREATE INDEX `IX_Segmentation_Product` ON `Segmentation` (`Product`);

CREATE INDEX `IX_Segmentation_ProductGroup` ON `Segmentation` (`ProductGroup`);

CREATE INDEX `IX_Segmentation_State` ON `Segmentation` (`State`);

CREATE INDEX `IX_Segmentation_SubProduct` ON `Segmentation` (`SubProduct`);

CREATE INDEX `IX_Segmentation_Zone` ON `Segmentation` (`Zone`);

CREATE INDEX `IX_SecondaryUnAllocationFile_UploadedDate` ON `SecondaryUnAllocationFile` (`UploadedDate`);

CREATE INDEX `IX_SecondaryAllocationFile_FileName` ON `SecondaryAllocationFile` (`FileName`);

CREATE INDEX `IX_SecondaryAllocationFile_FileUploadedDate` ON `SecondaryAllocationFile` (`FileUploadedDate`);

CREATE INDEX `IX_Regions_Name` ON `Regions` (`Name`);

CREATE INDEX `IX_Regions_NickName` ON `Regions` (`NickName`);

CREATE INDEX `IX_PrimaryUnAllocationFile_UploadedDate` ON `PrimaryUnAllocationFile` (`UploadedDate`);

CREATE INDEX `IX_PrimaryAllocationFile_FileUploadedDate` ON `PrimaryAllocationFile` (`FileUploadedDate`);

CREATE INDEX `IX_PayInSlips_CreatedDate` ON `PayInSlips` (`CreatedDate`);

CREATE INDEX `IX_PayInSlips_ModeOfPayment` ON `PayInSlips` (`ModeOfPayment`);

CREATE INDEX `IX_PayInSlips_PayinslipType` ON `PayInSlips` (`PayinslipType`);

CREATE INDEX `IX_PayInSlips_ProductGroup` ON `PayInSlips` (`ProductGroup`);

CREATE INDEX `IX_MasterFileStatus_FileName` ON `MasterFileStatus` (`FileName`);

CREATE INDEX `IX_MasterFileStatus_FileUploadedDate` ON `MasterFileStatus` (`FileUploadedDate`);

CREATE INDEX `IX_MasterFileStatus_Status` ON `MasterFileStatus` (`Status`);

CREATE INDEX `IX_MasterFileStatus_UploadType` ON `MasterFileStatus` (`UploadType`);

CREATE INDEX `IX_LoanAccounts_BillingCycle` ON `LoanAccounts` (`BILLING_CYCLE`);

CREATE INDEX `IX_LoanAccounts_BranchCode` ON `LoanAccounts` (`BranchCode`);

CREATE INDEX `IX_LoanAccounts_Bucket` ON `LoanAccounts` (`BUCKET`);

CREATE INDEX `IX_LoanAccounts_City` ON `LoanAccounts` (`CITY`);

CREATE INDEX `IX_LoanAccounts_DateOfBirth` ON `LoanAccounts` (`DateOfBirth`);

CREATE INDEX `IX_LoanAccounts_DispCode` ON `LoanAccounts` (`DispCode`);

CREATE INDEX `IX_LoanAccounts_LastUploadedDate` ON `LoanAccounts` (`LastUploadedDate`);

CREATE INDEX `IX_LoanAccounts_LatestFeedbackDate` ON `LoanAccounts` (`LatestFeedbackDate`);

CREATE INDEX `IX_LoanAccounts_LatestPaymentDate` ON `LoanAccounts` (`LatestPaymentDate`);

CREATE INDEX `IX_LoanAccounts_LatestPTPDate` ON `LoanAccounts` (`LatestPTPDate`);

CREATE INDEX `IX_LoanAccounts_NPAStageId` ON `LoanAccounts` (`NPA_STAGEID`);

CREATE INDEX `IX_LoanAccounts_PaymentStatus` ON `LoanAccounts` (`PAYMENTSTATUS`);

CREATE INDEX `IX_LoanAccounts_ProductCode` ON `LoanAccounts` (`ProductCode`);

CREATE INDEX `IX_LoanAccounts_ProductGroup` ON `LoanAccounts` (`ProductGroup`);

CREATE INDEX `IX_LoanAccounts_Region` ON `LoanAccounts` (`Region`);

CREATE INDEX `IX_LoanAccounts_State` ON `LoanAccounts` (`STATE`);

CREATE INDEX `IX_LoanAccounts_SubProduct` ON `LoanAccounts` (`SubProduct`);

CREATE INDEX `IX_Feedback_DispositionCode` ON `Feedback` (`DispositionCode`);

CREATE INDEX `IX_FeatureMaster_Parameter` ON `FeatureMaster` (`Parameter`);

CREATE INDEX `IX_Designation_Acronym` ON `Designation` (`Acronym`);

CREATE INDEX `IX_Designation_Name` ON `Designation` (`Name`);

CREATE INDEX `IX_Department_Code` ON `Department` (`Code`);

CREATE INDEX `IX_Department_Name` ON `Department` (`Name`);

CREATE INDEX `IX_Countries_Name` ON `Countries` (`Name`);

CREATE INDEX `IX_Countries_NickName` ON `Countries` (`NickName`);

CREATE INDEX `IX_CompanyUserWorkflowState_Name` ON `CompanyUserWorkflowState` (`Name`);

CREATE INDEX `IX_CommunicationTemplateDetail_Name` ON `CommunicationTemplateDetail` (`Name`);

CREATE INDEX `IX_CommunicationTemplate_CreatedDate` ON `CommunicationTemplate` (`CreatedDate`);

CREATE INDEX `IX_CommunicationTemplate_TemplateType` ON `CommunicationTemplate` (`TemplateType`);

CREATE INDEX `IX_Collections_AcknowledgedDate` ON `Collections` (`AcknowledgedDate`);

CREATE INDEX `IX_Collections_CollectionDate` ON `Collections` (`CollectionDate`);

CREATE INDEX `IX_Collections_CollectionMode` ON `Collections` (`CollectionMode`);

CREATE INDEX `IX_Collections_CreatedDate` ON `Collections` (`CreatedDate`);

CREATE INDEX `IX_Collections_CustomerName` ON `Collections` (`CustomerName`);

CREATE INDEX `IX_CollectionBatches_BatchType` ON `CollectionBatches` (`BatchType`);

CREATE INDEX `IX_CollectionBatches_CreatedDate` ON `CollectionBatches` (`CreatedDate`);

CREATE INDEX `IX_CollectionBatches_ModeOfPayment` ON `CollectionBatches` (`ModeOfPayment`);

CREATE INDEX `IX_CollectionBatches_ProductGroup` ON `CollectionBatches` (`ProductGroup`);

CREATE INDEX `IX_Cities_NickName` ON `Cities` (`NickName`);

CREATE INDEX `IX_Cheques_InstrumentDate` ON `Cheques` (`InstrumentDate`);

CREATE INDEX `IX_CategoryMaster_Name` ON `CategoryMaster` (`Name`);

CREATE INDEX `IX_CategoryItem_Code` ON `CategoryItem` (`Code`);

CREATE INDEX `IX_CategoryItem_Name` ON `CategoryItem` (`Name`);

CREATE INDEX `IX_BulkTrailUploadFile_FileUploadedDate` ON `BulkTrailUploadFile` (`FileUploadedDate`);

CREATE INDEX `IX_Area_Name` ON `Areas` (`Name`);

CREATE INDEX `IX_Area_NickName` ON `Areas` (`NickName`);

CREATE INDEX `IX_ApplicationUser_AuthorizationCardExpiryDate` ON `ApplicationUser` (`AuthorizationCardExpiryDate`);

CREATE INDEX `IX_ApplicationUser_PrimaryEMail` ON `ApplicationUser` (`PrimaryEMail`);

CREATE INDEX `IX_CompanyUser_FirstName` ON `ApplicationUser` (`FirstName`);

CREATE INDEX `IX_CompanyUser_LastName` ON `ApplicationUser` (`LastName`);

CREATE INDEX `IX_ApplicationOrg_ContractExpireDate` ON `ApplicationOrg` (`ContractExpireDate`);

CREATE INDEX `IX_ApplicationOrg_FirstName` ON `ApplicationOrg` (`FirstName`);

CREATE INDEX `IX_ApplicationOrg_LastName` ON `ApplicationOrg` (`LastName`);

CREATE INDEX `IX_AgencyWorkflowState_Name` ON `AgencyWorkflowState` (`Name`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250228053946_AddedCreatedDateIndexes', '8.0.10');

COMMIT;

