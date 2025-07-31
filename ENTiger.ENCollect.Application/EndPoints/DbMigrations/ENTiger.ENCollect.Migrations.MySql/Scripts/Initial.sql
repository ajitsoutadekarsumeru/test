CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `AccountabilityTypes` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AccountabilityTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AccountLabels` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Label` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AccountLabels` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Address` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `AddressLine1` varchar(100) CHARACTER SET utf8mb4 NULL,
    `AddressLine2` varchar(100) CHARACTER SET utf8mb4 NULL,
    `AddressLine3` varchar(100) CHARACTER SET utf8mb4 NULL,
    `LandMark` varchar(100) CHARACTER SET utf8mb4 NULL,
    `State` varchar(50) CHARACTER SET utf8mb4 NULL,
    `City` varchar(50) CHARACTER SET utf8mb4 NULL,
    `PIN` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Address` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AgencyCategory` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AgencyCategory` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AgencyType` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `MainType` varchar(50) CHARACTER SET utf8mb4 NULL,
    `SubType` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AgencyType` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AgencyUserWorkflowState` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Discriminator` varchar(34) CHARACTER SET utf8mb4 NOT NULL,
    `TFlexId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    `StateChangedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `StateChangedDate` datetime(6) NULL,
    `IsCompleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AgencyUserWorkflowState` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AgencyWorkflowState` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Discriminator` varchar(34) CHARACTER SET utf8mb4 NOT NULL,
    `TFlexId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    `StateChangedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `StateChangedDate` datetime(6) NULL,
    `IsCompleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AgencyWorkflowState` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AllocationDownload` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `InputJson` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `FileName` varchar(250) CHARACTER SET utf8mb4 NULL,
    `FilePath` varchar(500) CHARACTER SET utf8mb4 NULL,
    `Status` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CustomId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AllocationType` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AllocationDownload` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Bank` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(200) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Bank` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `BankAccountType` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Value` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_BankAccountType` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Buckets` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Buckets` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `BulkTrailUploadFile` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NULL,
    `FileName` varchar(250) CHARACTER SET utf8mb4 NULL,
    `FilePath` varchar(250) CHARACTER SET utf8mb4 NULL,
    `FileUploadedDate` datetime(6) NOT NULL,
    `FileProcessedDateTime` datetime(6) NOT NULL,
    `Status` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CustomId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `MD5Hash` varchar(200) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_BulkTrailUploadFile` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `BulkUploadFile` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CustomId` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `FileName` varchar(250) CHARACTER SET utf8mb4 NOT NULL,
    `FilePath` varchar(250) CHARACTER SET utf8mb4 NOT NULL,
    `FileUploadedDate` datetime(6) NOT NULL,
    `FileProcessedDateTime` datetime(6) NOT NULL,
    `Status` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `StatusFileName` varchar(250) CHARACTER SET utf8mb4 NOT NULL,
    `StatusFilePath` varchar(250) CHARACTER SET utf8mb4 NOT NULL,
    `MD5Hash` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `IsUploadstatus` tinyint(1) NULL,
    `RowsError` int NULL,
    `RowsProcessed` int NULL,
    `RowsSuccess` int NULL,
    `FileType` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `AllocationType` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_BulkUploadFile` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Cash` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Cash` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CategoryMaster` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(250) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CategoryMaster` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Cheques` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `BankName` varchar(100) CHARACTER SET utf8mb4 NULL,
    `BranchName` varchar(100) CHARACTER SET utf8mb4 NULL,
    `InstrumentNo` varchar(50) CHARACTER SET utf8mb4 NULL,
    `InstrumentDate` datetime(6) NULL,
    `MICRCode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `IFSCCode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `BankCity` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Cheques` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CollectionBatchWorkflowState` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Discriminator` varchar(55) CHARACTER SET utf8mb4 NOT NULL,
    `TFlexId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    `StateChangedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `StateChangedDate` datetime(6) NULL,
    `IsCompleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CollectionBatchWorkflowState` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CollectionWorkflowState` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Discriminator` varchar(34) CHARACTER SET utf8mb4 NOT NULL,
    `TFlexId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    `StateChangedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `StateChangedDate` datetime(6) NULL,
    `IsCompleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CollectionWorkflowState` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CommunicationTemplateDetail` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Salutation` longtext CHARACTER SET utf8mb4 NULL,
    `Body` longtext CHARACTER SET utf8mb4 NULL,
    `Signature` longtext CHARACTER SET utf8mb4 NULL,
    `Subject` longtext CHARACTER SET utf8mb4 NULL,
    `AddressTo` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CommunicationTemplateDetail` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CommunicationTemplateWorkflowState` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Discriminator` varchar(34) CHARACTER SET utf8mb4 NOT NULL,
    `TFlexId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    `StateChangedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `StateChangedDate` datetime(6) NULL,
    `IsCompleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CommunicationTemplateWorkflowState` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Company` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Company` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CompanyUserWorkflowState` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Discriminator` varchar(34) CHARACTER SET utf8mb4 NOT NULL,
    `TFlexId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    `StateChangedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `StateChangedDate` datetime(6) NULL,
    `IsCompleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CompanyUserWorkflowState` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Countries` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(300) CHARACTER SET utf8mb4 NOT NULL,
    `NickName` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Countries` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Culture` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Culture` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `DepartmentType` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_DepartmentType` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `DepositBankMaster` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `DepositBankName` varchar(100) CHARACTER SET utf8mb4 NULL,
    `DepositBranchName` varchar(100) CHARACTER SET utf8mb4 NULL,
    `DepositAccountNumber` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AccountHolderName` varchar(100) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_DepositBankMaster` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `DesignationType` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_DesignationType` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `DeviceDetail` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `OldIMEI` varchar(100) CHARACTER SET utf8mb4 NULL,
    `IMEI` varchar(100) CHARACTER SET utf8mb4 NULL,
    `UserId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Email` varchar(100) CHARACTER SET utf8mb4 NULL,
    `OTP` longtext CHARACTER SET utf8mb4 NULL,
    `OTPTimeStamp` datetime(6) NOT NULL,
    `IsVerified` tinyint(1) NOT NULL,
    `VerifiedDate` datetime(6) NULL,
    `OTPCount` int NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_DeviceDetail` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `DispositionGroupMaster` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `SrNo` bigint NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `NickName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `DispositionAccess` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_DispositionGroupMaster` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `FeatureMaster` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Parameter` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Value` longtext CHARACTER SET utf8mb4 NOT NULL,
    `IsEnabled` tinyint(1) NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_FeatureMaster` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `FlexBusinessContext` (
    `Id` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_FlexBusinessContext` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `IdConfigMaster` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CodeType` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `BaseValue` int NOT NULL,
    `LatestValue` int NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_IdConfigMaster` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `IdConfigMaster_SeedData` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_IdConfigMaster_SeedData` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `MasterFileStatus` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CustomId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `FileName` varchar(250) CHARACTER SET utf8mb4 NULL,
    `FilePath` varchar(250) CHARACTER SET utf8mb4 NULL,
    `FileProcessedDateTime` datetime(6) NULL,
    `FileUploadedDate` datetime(6) NULL,
    `Status` varchar(300) CHARACTER SET utf8mb4 NULL,
    `UploadType` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(2000) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_MasterFileStatus` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `MenuMaster` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `MenuName` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Etc` varchar(100) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_MenuMaster` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `MultilingualEntitySet` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `DefaultValue` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_MultilingualEntitySet` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PayInSlipWorkflowState` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Discriminator` varchar(34) CHARACTER SET utf8mb4 NOT NULL,
    `TFlexId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    `StateChangedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `StateChangedDate` datetime(6) NULL,
    `IsCompleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_PayInSlipWorkflowState` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentGateways` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(50) CHARACTER SET utf8mb4 NULL,
    `MerchantId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `MerchantKey` varchar(50) CHARACTER SET utf8mb4 NULL,
    `APIKey` varchar(150) CHARACTER SET utf8mb4 NULL,
    `ChecksumKey` varchar(150) CHARACTER SET utf8mb4 NULL,
    `PostURL` varchar(500) CHARACTER SET utf8mb4 NULL,
    `ReturnURL` varchar(500) CHARACTER SET utf8mb4 NULL,
    `ServerToServerURL` varchar(500) CHARACTER SET utf8mb4 NULL,
    `ErrorURL` varchar(500) CHARACTER SET utf8mb4 NULL,
    `CancelURL` varchar(500) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_PaymentGateways` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PinCodes` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Value` varchar(500) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_PinCodes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PrimaryAllocationFile` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NULL,
    `FileName` varchar(250) CHARACTER SET utf8mb4 NULL,
    `FilePath` varchar(250) CHARACTER SET utf8mb4 NULL,
    `FileUploadedDate` datetime(6) NOT NULL,
    `FileProcessedDateTime` datetime(6) NOT NULL,
    `Status` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CustomId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `UploadType` varchar(100) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_PrimaryAllocationFile` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PrimaryUnAllocationFile` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CustomId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `UploadType` varchar(100) CHARACTER SET utf8mb4 NULL,
    `FileName` varchar(250) CHARACTER SET utf8mb4 NULL,
    `FilePath` varchar(250) CHARACTER SET utf8mb4 NULL,
    `Status` varchar(50) CHARACTER SET utf8mb4 NULL,
    `UploadedDate` datetime(6) NOT NULL,
    `ProcessedDateTime` datetime(6) NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_PrimaryUnAllocationFile` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ReceiptWorkflowState` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Discriminator` varchar(34) CHARACTER SET utf8mb4 NOT NULL,
    `TFlexId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    `StateChangedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `StateChangedDate` datetime(6) NULL,
    `IsCompleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_ReceiptWorkflowState` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `RunStatus` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Status` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `CustomId` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `ProcessType` varchar(800) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_RunStatus` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SecondaryAllocationFile` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NULL,
    `FileName` varchar(250) CHARACTER SET utf8mb4 NULL,
    `FilePath` varchar(250) CHARACTER SET utf8mb4 NULL,
    `Status` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CustomId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `UploadType` varchar(100) CHARACTER SET utf8mb4 NULL,
    `FileUploadedDate` datetime(6) NOT NULL,
    `FileProcessedDateTime` datetime(6) NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_SecondaryAllocationFile` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SecondaryUnAllocationFile` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CustomId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `UploadType` varchar(100) CHARACTER SET utf8mb4 NULL,
    `FileName` varchar(250) CHARACTER SET utf8mb4 NULL,
    `FilePath` varchar(250) CHARACTER SET utf8mb4 NULL,
    `Status` varchar(50) CHARACTER SET utf8mb4 NULL,
    `UploadedDate` datetime(6) NOT NULL,
    `ProcessedDateTime` datetime(6) NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_SecondaryUnAllocationFile` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SegmentationAdvanceFilter` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `BOM_POS` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CHARGEOFF_DATE` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CURRENT_POS` varchar(50) CHARACTER SET utf8mb4 NULL,
    `LOAN_AMOUNT` varchar(50) CHARACTER SET utf8mb4 NULL,
    `NONSTARTER` varchar(50) CHARACTER SET utf8mb4 NULL,
    `NPA_STAGEID` varchar(50) CHARACTER SET utf8mb4 NULL,
    `PRINCIPAL_OD` varchar(50) CHARACTER SET utf8mb4 NULL,
    `TOS` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Area` varchar(50) CHARACTER SET utf8mb4 NULL,
    `LastDispositionCode` varchar(250) CHARACTER SET utf8mb4 NULL,
    `LastPaymentDate` varchar(50) CHARACTER SET utf8mb4 NULL,
    `DispCode` varchar(200) CHARACTER SET utf8mb4 NULL,
    `PTPDate` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CustomerPersona` varchar(200) CHARACTER SET utf8mb4 NULL,
    `CurrentDPD` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreditBureauScore` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CustomerBehaviourScore1` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CustomerBehaviourScore2` varchar(50) CHARACTER SET utf8mb4 NULL,
    `EarlyWarningScore` varchar(50) CHARACTER SET utf8mb4 NULL,
    `LegalStage` varchar(50) CHARACTER SET utf8mb4 NULL,
    `RepoStage` varchar(50) CHARACTER SET utf8mb4 NULL,
    `SettlementStage` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CustomerBehaviorScoreToKeepHisWord` varchar(50) CHARACTER SET utf8mb4 NULL,
    `PreferredModeOfPayment` varchar(50) CHARACTER SET utf8mb4 NULL,
    `PropensityToPayOnline` varchar(50) CHARACTER SET utf8mb4 NULL,
    `DigitalContactabilityScore` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CallContactabilityScore` varchar(50) CHARACTER SET utf8mb4 NULL,
    `FieldContactabilityScore` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Latest_Status_Of_SMS` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Latest_Status_Of_WhatsUp` varchar(50) CHARACTER SET utf8mb4 NULL,
    `StatementDate` varchar(50) CHARACTER SET utf8mb4 NULL,
    `DueDate` varchar(50) CHARACTER SET utf8mb4 NULL,
    `TotalOverdueAmount` varchar(50) CHARACTER SET utf8mb4 NULL,
    `DNDFlag` varchar(50) CHARACTER SET utf8mb4 NULL,
    `MinimumAmountDue` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Month` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Year` varchar(50) CHARACTER SET utf8mb4 NULL,
    `LOAN_STATUS` varchar(50) CHARACTER SET utf8mb4 NULL,
    `EMI_OD_AMT` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_SegmentationAdvanceFilter` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SegmentationAdvanceFilterMasters` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_SegmentationAdvanceFilterMasters` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Sequence` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Value` int NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Sequence` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TFlexIdentificationTypes` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TFlexIdentificationTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Treatment` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(300) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Mode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `IsDisabled` tinyint(1) NULL,
    `Sequence` int NULL,
    `PaymentStatusToStop` varchar(50) CHARACTER SET utf8mb4 NULL,
    `ExecutionStartdate` datetime(6) NULL,
    `ExecutionEnddate` datetime(6) NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Treatment` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TreatmentHistoryDetails` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `bucket` varchar(20) CHARACTER SET utf8mb4 NULL,
    `agreementid` varchar(100) CHARACTER SET utf8mb4 NULL,
    `customername` varchar(300) CHARACTER SET utf8mb4 NULL,
    `allocationownerid` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AllocationOwnerName` varchar(300) CHARACTER SET utf8mb4 NULL,
    `telecallingagencyid` varchar(50) CHARACTER SET utf8mb4 NULL,
    `TCallingAgencyName` varchar(300) CHARACTER SET utf8mb4 NULL,
    `current_dpd` varchar(10) CHARACTER SET utf8mb4 NULL,
    `telecallerid` varchar(50) CHARACTER SET utf8mb4 NULL,
    `agencyid` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AgencyName` varchar(300) CHARACTER SET utf8mb4 NULL,
    `collectorid` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AgentName` varchar(300) CHARACTER SET utf8mb4 NULL,
    `treatmentid` varchar(50) CHARACTER SET utf8mb4 NULL,
    `TreatmentName` varchar(200) CHARACTER SET utf8mb4 NULL,
    `SegmentationName` varchar(200) CHARACTER SET utf8mb4 NULL,
    `TCallingAgentName` varchar(300) CHARACTER SET utf8mb4 NULL,
    `treatmenthistoryid` varchar(50) CHARACTER SET utf8mb4 NULL,
    `npa_stageid` varchar(200) CHARACTER SET utf8mb4 NULL,
    `productgroup` varchar(200) CHARACTER SET utf8mb4 NULL,
    `latestmobileno` varchar(50) CHARACTER SET utf8mb4 NULL,
    `state` varchar(100) CHARACTER SET utf8mb4 NULL,
    `zone` varchar(100) CHARACTER SET utf8mb4 NULL,
    `segmentationid` varchar(100) CHARACTER SET utf8mb4 NULL,
    `bom_pos` double NULL,
    `current_pos` decimal(65,30) NULL,
    `current_bucket` varchar(50) CHARACTER SET utf8mb4 NULL,
    `loan_amount` varchar(100) CHARACTER SET utf8mb4 NULL,
    `tos` varchar(100) CHARACTER SET utf8mb4 NULL,
    `dispcode` varchar(100) CHARACTER SET utf8mb4 NULL,
    `branch` varchar(200) CHARACTER SET utf8mb4 NULL,
    `city` varchar(200) CHARACTER SET utf8mb4 NULL,
    `product` varchar(200) CHARACTER SET utf8mb4 NULL,
    `subproduct` varchar(200) CHARACTER SET utf8mb4 NULL,
    `region` varchar(200) CHARACTER SET utf8mb4 NULL,
    `principal_od` varchar(200) CHARACTER SET utf8mb4 NULL,
    `customerid` varchar(200) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TreatmentHistoryDetails` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TreatmentUpdateIntermediate` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `AgreementId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AllocationOwnerId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `TCAgencyId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AgencyId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `TellecallerId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CollectorId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `TreatmentId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `WorkRequestId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TreatmentUpdateIntermediate` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserCurrentLocationDetails` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Latitude` double NULL,
    `Longitude` double NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_UserCurrentLocationDetails` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserLoginKeys` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Key` varchar(50) CHARACTER SET utf8mb4 NULL,
    `IsActive` tinyint(1) NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_UserLoginKeys` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserPerformanceBandMaster` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_UserPerformanceBandMaster` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserPersonaMaster` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `Code` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_UserPersonaMaster` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `UsersUpdateFile` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CustomId` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `UploadType` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `FileName` varchar(250) CHARACTER SET utf8mb4 NOT NULL,
    `FilePath` varchar(250) CHARACTER SET utf8mb4 NOT NULL,
    `Status` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `UploadedDate` datetime(6) NOT NULL,
    `ProcessedDateTime` datetime(6) NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_UsersUpdateFile` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserVerificationCodeTypes` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(800) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_UserVerificationCodeTypes` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Zone` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `NickName` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Zone` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Accountabilities` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CommisionerId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `ResponsibleId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `AccountabilityTypeId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `ValidFrom` datetime(6) NULL,
    `ValidTo` datetime(6) NULL,
    `LastRenewalDate` datetime(6) NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Accountabilities` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Accountabilities_AccountabilityTypes_AccountabilityTypeId` FOREIGN KEY (`AccountabilityTypeId`) REFERENCES `AccountabilityTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ApplicationUserDetails` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `PanNumber` varchar(20) CHARACTER SET utf8mb4 NULL,
    `AadharNumber` varchar(20) CHARACTER SET utf8mb4 NULL,
    `Gender` varchar(20) CHARACTER SET utf8mb4 NULL,
    `AddressId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_ApplicationUserDetails` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ApplicationUserDetails_Address_AddressId` FOREIGN KEY (`AddressId`) REFERENCES `Address` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `BankBranch` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Code` varchar(50) CHARACTER SET utf8mb4 NULL,
    `MICR` varchar(50) CHARACTER SET utf8mb4 NULL,
    `BankId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_BankBranch` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_BankBranch_Bank_BankId` FOREIGN KEY (`BankId`) REFERENCES `Bank` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CategoryItem` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(250) CHARACTER SET utf8mb4 NULL,
    `CategoryMasterId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `ParentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `Code` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(700) CHARACTER SET utf8mb4 NULL,
    `IsDisabled` tinyint(1) NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CategoryItem` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_CategoryItem_CategoryItem_ParentId` FOREIGN KEY (`ParentId`) REFERENCES `CategoryItem` (`Id`),
    CONSTRAINT `FK_CategoryItem_CategoryMaster_CategoryMasterId` FOREIGN KEY (`CategoryMasterId`) REFERENCES `CategoryMaster` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CommunicationTemplate` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `TemplateType` longtext CHARACTER SET utf8mb4 NULL,
    `Recipient` longtext CHARACTER SET utf8mb4 NULL,
    `CommunicationTemplateDetailId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `IsDisabled` tinyint(1) NOT NULL,
    `CommunicationTemplateWorkflowStateId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `WATemplateId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Language` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CommunicationTemplate` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_CommunicationTemplate_CommunicationTemplateDetail_Communicat~` FOREIGN KEY (`CommunicationTemplateDetailId`) REFERENCES `CommunicationTemplateDetail` (`Id`),
    CONSTRAINT `FK_CommunicationTemplate_CommunicationTemplateWorkflowState_Com~` FOREIGN KEY (`CommunicationTemplateWorkflowStateId`) REFERENCES `CommunicationTemplateWorkflowState` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Regions` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `NickName` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `CountryId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Regions` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Regions_Countries_CountryId` FOREIGN KEY (`CountryId`) REFERENCES `Countries` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Department` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Acronym` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Code` varchar(50) CHARACTER SET utf8mb4 NULL,
    `DepartmentTypeId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Department` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Department_DepartmentType_DepartmentTypeId` FOREIGN KEY (`DepartmentTypeId`) REFERENCES `DepartmentType` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `DispositionCodeMaster` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `DispositionGroupMasterId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `SrNo` bigint NOT NULL,
    `DispositionCode` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Permissibleforfieldagent` longtext CHARACTER SET utf8mb4 NOT NULL,
    `ShortDescription` longtext CHARACTER SET utf8mb4 NOT NULL,
    `LongDescription` longtext CHARACTER SET utf8mb4 NOT NULL,
    `DispositionAccess` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_DispositionCodeMaster` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_DispositionCodeMaster_DispositionGroupMaster_DispositionGrou~` FOREIGN KEY (`DispositionGroupMasterId`) REFERENCES `DispositionGroupMaster` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `FlexDynamicBusinessRuleSequences` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `FlexBusinessContextId` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `PluginId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Sequence` int NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_FlexDynamicBusinessRuleSequences` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_FlexDynamicBusinessRuleSequences_FlexBusinessContext_FlexBus~` FOREIGN KEY (`FlexBusinessContextId`) REFERENCES `FlexBusinessContext` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SubMenuMaster` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `SubMenuName` varchar(100) CHARACTER SET utf8mb4 NULL,
    `hasAccess` tinyint(1) NOT NULL,
    `MenuMasterId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_SubMenuMaster` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_SubMenuMaster_MenuMaster_MenuMasterId` FOREIGN KEY (`MenuMasterId`) REFERENCES `MenuMaster` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `MultilingualEntity` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `MultilingualEntitySetId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CultureId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Value` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_MultilingualEntity` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_MultilingualEntity_Culture_CultureId` FOREIGN KEY (`CultureId`) REFERENCES `Culture` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_MultilingualEntity_MultilingualEntitySet_MultilingualEntityS~` FOREIGN KEY (`MultilingualEntitySetId`) REFERENCES `MultilingualEntitySet` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Segmentation` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NULL,
    `ExecutionType` varchar(20) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(500) CHARACTER SET utf8mb4 NULL,
    `ProductGroup` varchar(500) CHARACTER SET utf8mb4 NULL,
    `Product` varchar(500) CHARACTER SET utf8mb4 NULL,
    `SubProduct` varchar(500) CHARACTER SET utf8mb4 NULL,
    `BOM_Bucket` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CurrentBucket` varchar(50) CHARACTER SET utf8mb4 NULL,
    `NPA_Flag` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Current_DPD` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Zone` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Region` varchar(100) CHARACTER SET utf8mb4 NULL,
    `State` varchar(100) CHARACTER SET utf8mb4 NULL,
    `City` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Branch` varchar(500) CHARACTER SET utf8mb4 NULL,
    `Sequence` int NULL,
    `IsDisabled` tinyint(1) NULL,
    `SegmentAdvanceFilterId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `ClusterName` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Segmentation` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Segmentation_SegmentationAdvanceFilter_SegmentAdvanceFilterId` FOREIGN KEY (`SegmentAdvanceFilterId`) REFERENCES `SegmentationAdvanceFilter` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TFlexIdentificationDocTypes` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `TFlexIdentificationTypeId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TFlexIdentificationDocTypes` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TFlexIdentificationDocTypes_TFlexIdentificationTypes_TFlexId~` FOREIGN KEY (`TFlexIdentificationTypeId`) REFERENCES `TFlexIdentificationTypes` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `SubTreatment` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Order` int NULL,
    `TreatmentType` varchar(100) CHARACTER SET utf8mb4 NULL,
    `AllocationType` varchar(100) CHARACTER SET utf8mb4 NULL,
    `StartDay` varchar(20) CHARACTER SET utf8mb4 NULL,
    `EndDay` varchar(20) CHARACTER SET utf8mb4 NULL,
    `ScriptToPersueCustomer` longtext CHARACTER SET utf8mb4 NULL,
    `QualifyingCondition` varchar(20) CHARACTER SET utf8mb4 NULL,
    `PreSubtreatmentOrder` int NULL,
    `QualifyingStatus` varchar(100) CHARACTER SET utf8mb4 NULL,
    `TreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_SubTreatment` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_SubTreatment_Treatment_TreatmentId` FOREIGN KEY (`TreatmentId`) REFERENCES `Treatment` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TreatmentHistory` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `TreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `NoOfAccounts` varchar(50) CHARACTER SET utf8mb4 NULL,
    `LatestStamping` varchar(10) CHARACTER SET utf8mb4 NULL,
    `SubTreatmentOrder` int NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TreatmentHistory` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TreatmentHistory_Treatment_TreatmentId` FOREIGN KEY (`TreatmentId`) REFERENCES `Treatment` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserVerificationCodes` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `UserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `ShortVerificationCode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `VerificationCode` varchar(4000) CHARACTER SET utf8mb4 NULL,
    `UserVerificationCodeTypeId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `TransactionID` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_UserVerificationCodes` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_UserVerificationCodes_UserVerificationCodeTypes_UserVerifica~` FOREIGN KEY (`UserVerificationCodeTypeId`) REFERENCES `UserVerificationCodeTypes` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CreditAccountDetails` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `AccountHolderName` varchar(200) CHARACTER SET utf8mb4 NULL,
    `BankAccountNo` varchar(50) CHARACTER SET utf8mb4 NULL,
    `BankBranchId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CreditAccountDetails` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_CreditAccountDetails_BankBranch_BankBranchId` FOREIGN KEY (`BankBranchId`) REFERENCES `BankBranch` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `State` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `NickName` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `PrimaryLanguage` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `SecondaryLanguage` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `RegionId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_State` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_State_Regions_RegionId` FOREIGN KEY (`RegionId`) REFERENCES `Regions` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Designation` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Acronym` varchar(50) CHARACTER SET utf8mb4 NULL,
    `DesignationTypeId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `DepartmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `Level` varchar(10) CHARACTER SET utf8mb4 NULL,
    `ReportsToDesignation` varchar(200) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Designation` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Designation_Department_DepartmentId` FOREIGN KEY (`DepartmentId`) REFERENCES `Department` (`Id`),
    CONSTRAINT `FK_Designation_DesignationType_DesignationTypeId` FOREIGN KEY (`DesignationTypeId`) REFERENCES `DesignationType` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `DispositionValidationMaster` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `DispositionCodeMasterId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `validationFieldName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_DispositionValidationMaster` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_DispositionValidationMaster_DispositionCodeMaster_Dispositio~` FOREIGN KEY (`DispositionCodeMasterId`) REFERENCES `DispositionCodeMaster` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `ActionMaster` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(100) CHARACTER SET utf8mb4 NULL,
    `HasAccess` tinyint(1) NOT NULL,
    `SubMenuMasterID` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_ActionMaster` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ActionMaster_SubMenuMaster_SubMenuMasterID` FOREIGN KEY (`SubMenuMasterID`) REFERENCES `SubMenuMaster` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TreatmentAndSegmentMapping` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `TreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `SegmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TreatmentAndSegmentMapping` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TreatmentAndSegmentMapping_Segmentation_SegmentId` FOREIGN KEY (`SegmentId`) REFERENCES `Segmentation` (`Id`),
    CONSTRAINT `FK_TreatmentAndSegmentMapping_Treatment_TreatmentId` FOREIGN KEY (`TreatmentId`) REFERENCES `Treatment` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `RoundRobinTreatment` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `TreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `SubTreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AllocationId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AllocationName` varchar(100) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_RoundRobinTreatment` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_RoundRobinTreatment_SubTreatment_SubTreatmentId` FOREIGN KEY (`SubTreatmentId`) REFERENCES `SubTreatment` (`Id`),
    CONSTRAINT `FK_RoundRobinTreatment_Treatment_TreatmentId` FOREIGN KEY (`TreatmentId`) REFERENCES `Treatment` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TreatmentOnCommunication` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `TreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `SubTreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CommunicationType` varchar(30) CHARACTER SET utf8mb4 NULL,
    `CommunicationTemplateId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CommunicationMobileNumberType` varchar(200) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TreatmentOnCommunication` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TreatmentOnCommunication_CommunicationTemplate_Communication~` FOREIGN KEY (`CommunicationTemplateId`) REFERENCES `CommunicationTemplate` (`Id`),
    CONSTRAINT `FK_TreatmentOnCommunication_SubTreatment_SubTreatmentId` FOREIGN KEY (`SubTreatmentId`) REFERENCES `SubTreatment` (`Id`),
    CONSTRAINT `FK_TreatmentOnCommunication_Treatment_TreatmentId` FOREIGN KEY (`TreatmentId`) REFERENCES `Treatment` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TreatmentOnPerformanceBand` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `TreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `SubTreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `PerformanceBand` varchar(30) CHARACTER SET utf8mb4 NULL,
    `CustomerPersona` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Percentage` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TreatmentOnPerformanceBand` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TreatmentOnPerformanceBand_SubTreatment_SubTreatmentId` FOREIGN KEY (`SubTreatmentId`) REFERENCES `SubTreatment` (`Id`),
    CONSTRAINT `FK_TreatmentOnPerformanceBand_Treatment_TreatmentId` FOREIGN KEY (`TreatmentId`) REFERENCES `Treatment` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TreatmentOnUpdateTrail` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `TreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `SubTreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `DispositionCodeGroup` varchar(100) CHARACTER SET utf8mb4 NULL,
    `DispositionCode` varchar(100) CHARACTER SET utf8mb4 NULL,
    `NextActionDate` datetime(6) NULL,
    `PTPAmount` decimal(65,30) NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TreatmentOnUpdateTrail` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TreatmentOnUpdateTrail_SubTreatment_SubTreatmentId` FOREIGN KEY (`SubTreatmentId`) REFERENCES `SubTreatment` (`Id`),
    CONSTRAINT `FK_TreatmentOnUpdateTrail_Treatment_TreatmentId` FOREIGN KEY (`TreatmentId`) REFERENCES `Treatment` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TreatmentQualifyingStatus` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `TreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `SubTreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `Status` varchar(200) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TreatmentQualifyingStatus` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TreatmentQualifyingStatus_SubTreatment_SubTreatmentId` FOREIGN KEY (`SubTreatmentId`) REFERENCES `SubTreatment` (`Id`),
    CONSTRAINT `FK_TreatmentQualifyingStatus_Treatment_TreatmentId` FOREIGN KEY (`TreatmentId`) REFERENCES `Treatment` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Cities` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `NickName` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `StateId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Cities` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Cities_State_StateId` FOREIGN KEY (`StateId`) REFERENCES `State` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `TreatmentByRule` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `TreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `SubTreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `DepartmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `DesignationId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `Rule` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TreatmentByRule` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TreatmentByRule_Department_DepartmentId` FOREIGN KEY (`DepartmentId`) REFERENCES `Department` (`Id`),
    CONSTRAINT `FK_TreatmentByRule_Designation_DesignationId` FOREIGN KEY (`DesignationId`) REFERENCES `Designation` (`Id`),
    CONSTRAINT `FK_TreatmentByRule_SubTreatment_SubTreatmentId` FOREIGN KEY (`SubTreatmentId`) REFERENCES `SubTreatment` (`Id`),
    CONSTRAINT `FK_TreatmentByRule_Treatment_TreatmentId` FOREIGN KEY (`TreatmentId`) REFERENCES `Treatment` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TreatmentDesignation` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `TreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `DepartmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `DesignationId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `SubTreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TreatmentDesignation` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TreatmentDesignation_Department_DepartmentId` FOREIGN KEY (`DepartmentId`) REFERENCES `Department` (`Id`),
    CONSTRAINT `FK_TreatmentDesignation_Designation_DesignationId` FOREIGN KEY (`DesignationId`) REFERENCES `Designation` (`Id`),
    CONSTRAINT `FK_TreatmentDesignation_SubTreatment_SubTreatmentId` FOREIGN KEY (`SubTreatmentId`) REFERENCES `SubTreatment` (`Id`),
    CONSTRAINT `FK_TreatmentDesignation_Treatment_TreatmentId` FOREIGN KEY (`TreatmentId`) REFERENCES `Treatment` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TreatmentOnAccount` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `TreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `SubTreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `DepartmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `DesignationId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `Percentage` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AllocationId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AllocationName` varchar(100) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TreatmentOnAccount` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TreatmentOnAccount_Department_DepartmentId` FOREIGN KEY (`DepartmentId`) REFERENCES `Department` (`Id`),
    CONSTRAINT `FK_TreatmentOnAccount_Designation_DesignationId` FOREIGN KEY (`DesignationId`) REFERENCES `Designation` (`Id`),
    CONSTRAINT `FK_TreatmentOnAccount_SubTreatment_SubTreatmentId` FOREIGN KEY (`SubTreatmentId`) REFERENCES `SubTreatment` (`Id`),
    CONSTRAINT `FK_TreatmentOnAccount_Treatment_TreatmentId` FOREIGN KEY (`TreatmentId`) REFERENCES `Treatment` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TreatmentOnPOS` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `TreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `SubTreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `DepartmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `DesignationId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `Percentage` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AllocationId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AllocationName` varchar(100) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TreatmentOnPOS` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TreatmentOnPOS_Department_DepartmentId` FOREIGN KEY (`DepartmentId`) REFERENCES `Department` (`Id`),
    CONSTRAINT `FK_TreatmentOnPOS_Designation_DesignationId` FOREIGN KEY (`DesignationId`) REFERENCES `Designation` (`Id`),
    CONSTRAINT `FK_TreatmentOnPOS_SubTreatment_SubTreatmentId` FOREIGN KEY (`SubTreatmentId`) REFERENCES `SubTreatment` (`Id`),
    CONSTRAINT `FK_TreatmentOnPOS_Treatment_TreatmentId` FOREIGN KEY (`TreatmentId`) REFERENCES `Treatment` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AccessRights` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `BusinessEntity` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Action` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `AccountabilityTypeId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `AccountabilityAs` varchar(5) CHARACTER SET utf8mb4 NOT NULL,
    `Discriminator` varchar(21) CHARACTER SET utf8mb4 NOT NULL,
    `MethodType` varchar(10) CHARACTER SET utf8mb4 NULL,
    `Route` varchar(250) CHARACTER SET utf8mb4 NULL,
    `MenuMasterId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `ActionMasterId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `SubMenuMasterId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `IsMobile` tinyint(1) NULL,
    `IsFrontEnd` tinyint(1) NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AccessRights` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AccessRights_AccountabilityTypes_AccountabilityTypeId` FOREIGN KEY (`AccountabilityTypeId`) REFERENCES `AccountabilityTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AccessRights_ActionMaster_ActionMasterId` FOREIGN KEY (`ActionMasterId`) REFERENCES `ActionMaster` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AccessRights_MenuMaster_MenuMasterId` FOREIGN KEY (`MenuMasterId`) REFERENCES `MenuMaster` (`Id`),
    CONSTRAINT `FK_AccessRights_SubMenuMaster_SubMenuMasterId` FOREIGN KEY (`SubMenuMasterId`) REFERENCES `SubMenuMaster` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AgencyIdentification` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    `TFlexId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `TFlexIdentificationTypeId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `TFlexIdentificationDocTypeId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `IsDeferred` tinyint(1) NULL,
    `DeferredTillDate` datetime(6) NULL,
    `IsWavedOff` tinyint(1) NULL,
    `Value` varchar(500) CHARACTER SET utf8mb4 NULL,
    `Status` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Remarks` varchar(200) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AgencyIdentification` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AgencyIdentification_TFlexIdentificationDocTypes_TFlexIdenti~` FOREIGN KEY (`TFlexIdentificationDocTypeId`) REFERENCES `TFlexIdentificationDocTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AgencyIdentification_TFlexIdentificationTypes_TFlexIdentific~` FOREIGN KEY (`TFlexIdentificationTypeId`) REFERENCES `TFlexIdentificationTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AgencyIdentificationDoc` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    `TFlexIdentificationId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `Path` varchar(500) CHARACTER SET utf8mb4 NULL,
    `FileName` varchar(100) CHARACTER SET utf8mb4 NULL,
    `FileSize` bigint NULL,
    CONSTRAINT `PK_AgencyIdentificationDoc` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AgencyIdentificationDoc_AgencyIdentification_TFlexIdentifica~` FOREIGN KEY (`TFlexIdentificationId`) REFERENCES `AgencyIdentification` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AgencyPlaceOfWork` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Product` varchar(50) CHARACTER SET utf8mb4 NULL,
    `ProductGroup` varchar(50) CHARACTER SET utf8mb4 NULL,
    `SubProduct` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Bucket` varchar(50) CHARACTER SET utf8mb4 NULL,
    `City` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Region` varchar(50) CHARACTER SET utf8mb4 NULL,
    `State` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Zone` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Location` varchar(100) CHARACTER SET utf8mb4 NULL,
    `ManagerId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AgencyId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AgencyPlaceOfWork` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AgencyScopeOfWork` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Product` varchar(50) CHARACTER SET utf8mb4 NULL,
    `ProductGroup` varchar(50) CHARACTER SET utf8mb4 NULL,
    `SubProduct` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Bucket` varchar(50) CHARACTER SET utf8mb4 NULL,
    `City` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Region` varchar(50) CHARACTER SET utf8mb4 NULL,
    `State` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Zone` varchar(50) CHARACTER SET utf8mb4 NULL,
    `ManagerId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AgencyId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AgencyScopeOfWork` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AgencyUserDesignation` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `AgencyUserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `DepartmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `DesignationId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AgencyUserDesignation` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AgencyUserDesignation_Department_DepartmentId` FOREIGN KEY (`DepartmentId`) REFERENCES `Department` (`Id`),
    CONSTRAINT `FK_AgencyUserDesignation_Designation_DesignationId` FOREIGN KEY (`DesignationId`) REFERENCES `Designation` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AgencyUserIdentification` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    `TFlexId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `TFlexIdentificationTypeId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `TFlexIdentificationDocTypeId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `IsDeferred` tinyint(1) NULL,
    `DeferredTillDate` datetime(6) NULL,
    `IsWavedOff` tinyint(1) NULL,
    `Value` varchar(500) CHARACTER SET utf8mb4 NULL,
    `Status` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Remarks` varchar(200) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AgencyUserIdentification` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AgencyUserIdentification_TFlexIdentificationDocTypes_TFlexId~` FOREIGN KEY (`TFlexIdentificationDocTypeId`) REFERENCES `TFlexIdentificationDocTypes` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AgencyUserIdentification_TFlexIdentificationTypes_TFlexIdent~` FOREIGN KEY (`TFlexIdentificationTypeId`) REFERENCES `TFlexIdentificationTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AgencyUserIdentificationDoc` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    `TFlexIdentificationId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `Path` varchar(500) CHARACTER SET utf8mb4 NULL,
    `FileName` varchar(100) CHARACTER SET utf8mb4 NULL,
    `FileSize` bigint NULL,
    CONSTRAINT `PK_AgencyUserIdentificationDoc` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AgencyUserIdentificationDoc_AgencyUserIdentification_TFlexId~` FOREIGN KEY (`TFlexIdentificationId`) REFERENCES `AgencyUserIdentification` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AgencyUserPlaceOfWork` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `PIN` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AgencyUserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AgencyUserPlaceOfWork` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AgencyUserScopeOfWork` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Product` varchar(50) CHARACTER SET utf8mb4 NULL,
    `ProductGroup` varchar(50) CHARACTER SET utf8mb4 NULL,
    `SubProduct` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Bucket` varchar(50) CHARACTER SET utf8mb4 NULL,
    `City` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Region` varchar(50) CHARACTER SET utf8mb4 NULL,
    `State` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Zone` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Branch` varchar(50) CHARACTER SET utf8mb4 NULL,
    `DepartmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `DesignationId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AgencyUserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `ManagerId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `Language` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Experience` bigint NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AgencyUserScopeOfWork` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AgencyUserScopeOfWork_Department_DepartmentId` FOREIGN KEY (`DepartmentId`) REFERENCES `Department` (`Id`),
    CONSTRAINT `FK_AgencyUserScopeOfWork_Designation_DesignationId` FOREIGN KEY (`DesignationId`) REFERENCES `Designation` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ApplicationOrg` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CustomId` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Discriminator` varchar(21) CHARACTER SET utf8mb4 NOT NULL,
    `Remarks` varchar(500) CHARACTER SET utf8mb4 NULL,
    `IsBlackListed` tinyint(1) NULL,
    `BlackListingReason` varchar(200) CHARACTER SET utf8mb4 NULL,
    `ContractExpireDate` datetime(6) NULL,
    `LastRenewalDate` datetime(6) NULL,
    `FirstAgreementDate` datetime(6) NULL,
    `ServiceTaxRegistrationNumber` varchar(50) CHARACTER SET utf8mb4 NULL,
    `DeactivationReason` varchar(200) CHARACTER SET utf8mb4 NULL,
    `IsParentAgency` tinyint(1) NULL,
    `PAN` varchar(50) CHARACTER SET utf8mb4 NULL,
    `TIN` varchar(50) CHARACTER SET utf8mb4 NULL,
    `GSTIN` varchar(50) CHARACTER SET utf8mb4 NULL,
    `PrimaryOwnerFirstName` varchar(50) CHARACTER SET utf8mb4 NULL,
    `PrimaryOwnerLastName` varchar(50) CHARACTER SET utf8mb4 NULL,
    `PrimaryContactCountryCode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `PrimaryContactAreaCode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `SecondaryContactCountryCode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `SecondaryContactAreaCode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `YardAddress` varchar(100) CHARACTER SET utf8mb4 NULL,
    `NumberOfYards` decimal(65,30) NULL,
    `YardSize` decimal(65,30) NULL,
    `DepartmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `DesignationId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AgencyTypeId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `ParentAgencyId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `RecommendingOfficerId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreditAccountDetailsId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AgencyCategoryId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AddressId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AgencyWorkflowStateId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `BankSolID` varchar(100) CHARACTER SET utf8mb4 NULL,
    `NickName` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AddressLine2` longtext CHARACTER SET utf8mb4 NULL,
    `AddressLine1` longtext CHARACTER SET utf8mb4 NULL,
    `CityId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `Zone` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Region` varchar(50) CHARACTER SET utf8mb4 NULL,
    `State` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    `UserId` varchar(200) CHARACTER SET utf8mb4 NULL,
    `FirstName` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `MiddleName` varchar(50) CHARACTER SET utf8mb4 NULL,
    `LastName` varchar(50) CHARACTER SET utf8mb4 NULL,
    `PrimaryMobileNumber` varchar(50) CHARACTER SET utf8mb4 NULL,
    `SecondaryContactNumber` varchar(50) CHARACTER SET utf8mb4 NULL,
    `ProfileImage` varchar(500) CHARACTER SET utf8mb4 NULL,
    `PrimaryEMail` varchar(200) CHARACTER SET utf8mb4 NULL,
    `ActivationCode` varchar(256) CHARACTER SET utf8mb4 NULL,
    `isOrganization` tinyint(1) NOT NULL,
    `DateOfBirth` datetime(6) NULL,
    CONSTRAINT `PK_ApplicationOrg` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ApplicationOrg_Address_AddressId` FOREIGN KEY (`AddressId`) REFERENCES `Address` (`Id`),
    CONSTRAINT `FK_ApplicationOrg_AgencyCategory_AgencyCategoryId` FOREIGN KEY (`AgencyCategoryId`) REFERENCES `AgencyCategory` (`Id`),
    CONSTRAINT `FK_ApplicationOrg_AgencyType_AgencyTypeId` FOREIGN KEY (`AgencyTypeId`) REFERENCES `AgencyType` (`Id`),
    CONSTRAINT `FK_ApplicationOrg_AgencyWorkflowState_AgencyWorkflowStateId` FOREIGN KEY (`AgencyWorkflowStateId`) REFERENCES `AgencyWorkflowState` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ApplicationOrg_ApplicationOrg_ParentAgencyId` FOREIGN KEY (`ParentAgencyId`) REFERENCES `ApplicationOrg` (`Id`),
    CONSTRAINT `FK_ApplicationOrg_Cities_CityId` FOREIGN KEY (`CityId`) REFERENCES `Cities` (`Id`),
    CONSTRAINT `FK_ApplicationOrg_CreditAccountDetails_CreditAccountDetailsId` FOREIGN KEY (`CreditAccountDetailsId`) REFERENCES `CreditAccountDetails` (`Id`),
    CONSTRAINT `FK_ApplicationOrg_Department_DepartmentId` FOREIGN KEY (`DepartmentId`) REFERENCES `Department` (`Id`),
    CONSTRAINT `FK_ApplicationOrg_Designation_DesignationId` FOREIGN KEY (`DesignationId`) REFERENCES `Designation` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ApplicationUser` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CustomId` varchar(100) CHARACTER SET utf8mb4 NULL,
    `DeactivationReason` longtext CHARACTER SET utf8mb4 NULL,
    `IsDeactivated` tinyint(1) NOT NULL,
    `IsBlackListed` tinyint(1) NOT NULL,
    `BlackListingReason` varchar(200) CHARACTER SET utf8mb4 NULL,
    `RejectionReason` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Remarks` varchar(500) CHARACTER SET utf8mb4 NULL,
    `ForgotPasswordMailSentCount` int NOT NULL,
    `ForgotPasswordSMSSentCount` int NOT NULL,
    `ForgotPasswordCount` int NOT NULL,
    `ForgotPasswordDateTime` datetime(6) NULL,
    `UpdateMobileDateTime` datetime(6) NULL,
    `UpdateMobileCount` int NOT NULL,
    `ProductGroup` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Product` varchar(100) CHARACTER SET utf8mb4 NULL,
    `SubProduct` varchar(200) CHARACTER SET utf8mb4 NULL,
    `MaxHotleadCount` varchar(50) CHARACTER SET utf8mb4 NULL,
    `WorkOfficeLongitude` varchar(50) CHARACTER SET utf8mb4 NULL,
    `WorkOfficeLattitude` varchar(50) CHARACTER SET utf8mb4 NULL,
    `PrimaryContactCountryCode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `UserLoad` int NULL,
    `Experience` decimal(18,2) NULL,
    `UserCurrentLocationDetailsId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreditAccountDetailsId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `ApplicationUserDetailsId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `IsLocked` tinyint(1) NOT NULL,
    `Discriminator` varchar(21) CHARACTER SET utf8mb4 NOT NULL,
    `IdCardNumber` varchar(50) CHARACTER SET utf8mb4 NULL,
    `EmploymentDate` datetime(6) NULL,
    `AuthorizationCardExpiryDate` datetime(6) NULL,
    `LastRenewalDate` datetime(6) NULL,
    `SupervisorEmailId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `PrimaryContactAreaCode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `DiallerId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `IDType` varchar(50) CHARACTER SET utf8mb4 NULL,
    `UDIDNumber` varchar(50) CHARACTER SET utf8mb4 NULL,
    `FatherName` varchar(50) CHARACTER SET utf8mb4 NULL,
    `DisableReason` varchar(200) CHARACTER SET utf8mb4 NULL,
    `DeactivateReason` varchar(200) CHARACTER SET utf8mb4 NULL,
    `IsPrinted` tinyint(1) NULL,
    `yCoreBankingId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `DRACertificateDate` datetime(6) NULL,
    `DRATrainingDate` datetime(6) NULL,
    `DRAUniqueRegistrationNumber` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AddressId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AgencyId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AgencyUserWorkflowStateId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `IsFrontEndStaff` tinyint(1) NULL,
    `DomainId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `EmployeeId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CompanyUser_PrimaryContactAreaCode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `SinglePointReportingManagerId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `BaseBranchId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CompanyId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `TrailCap` int NULL,
    `CompanyUserWorkflowStateId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    `UserId` varchar(200) CHARACTER SET utf8mb4 NULL,
    `FirstName` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `MiddleName` varchar(50) CHARACTER SET utf8mb4 NULL,
    `LastName` varchar(50) CHARACTER SET utf8mb4 NULL,
    `PrimaryMobileNumber` varchar(50) CHARACTER SET utf8mb4 NULL,
    `SecondaryContactNumber` varchar(50) CHARACTER SET utf8mb4 NULL,
    `ProfileImage` varchar(500) CHARACTER SET utf8mb4 NULL,
    `PrimaryEMail` varchar(200) CHARACTER SET utf8mb4 NULL,
    `ActivationCode` varchar(256) CHARACTER SET utf8mb4 NULL,
    `isOrganization` tinyint(1) NOT NULL,
    `DateOfBirth` datetime(6) NULL,
    CONSTRAINT `PK_ApplicationUser` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ApplicationUser_Address_AddressId` FOREIGN KEY (`AddressId`) REFERENCES `Address` (`Id`),
    CONSTRAINT `FK_ApplicationUser_AgencyUserWorkflowState_AgencyUserWorkflowSt~` FOREIGN KEY (`AgencyUserWorkflowStateId`) REFERENCES `AgencyUserWorkflowState` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ApplicationUser_ApplicationOrg_AgencyId` FOREIGN KEY (`AgencyId`) REFERENCES `ApplicationOrg` (`Id`),
    CONSTRAINT `FK_ApplicationUser_ApplicationOrg_BaseBranchId` FOREIGN KEY (`BaseBranchId`) REFERENCES `ApplicationOrg` (`Id`),
    CONSTRAINT `FK_ApplicationUser_ApplicationUserDetails_ApplicationUserDetail~` FOREIGN KEY (`ApplicationUserDetailsId`) REFERENCES `ApplicationUserDetails` (`Id`),
    CONSTRAINT `FK_ApplicationUser_ApplicationUser_SinglePointReportingManagerId` FOREIGN KEY (`SinglePointReportingManagerId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_ApplicationUser_CompanyUserWorkflowState_CompanyUserWorkflow~` FOREIGN KEY (`CompanyUserWorkflowStateId`) REFERENCES `CompanyUserWorkflowState` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ApplicationUser_Company_CompanyId` FOREIGN KEY (`CompanyId`) REFERENCES `Company` (`Id`),
    CONSTRAINT `FK_ApplicationUser_CreditAccountDetails_CreditAccountDetailsId` FOREIGN KEY (`CreditAccountDetailsId`) REFERENCES `CreditAccountDetails` (`Id`),
    CONSTRAINT `FK_ApplicationUser_UserCurrentLocationDetails_UserCurrentLocati~` FOREIGN KEY (`UserCurrentLocationDetailsId`) REFERENCES `UserCurrentLocationDetails` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Areas` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `NickName` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `CityId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `BaseBranchId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Areas` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Areas_ApplicationOrg_BaseBranchId` FOREIGN KEY (`BaseBranchId`) REFERENCES `ApplicationOrg` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Areas_Cities_CityId` FOREIGN KEY (`CityId`) REFERENCES `Cities` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `GeoMaster` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Country` longtext CHARACTER SET utf8mb4 NULL,
    `Zone` longtext CHARACTER SET utf8mb4 NULL,
    `Region` longtext CHARACTER SET utf8mb4 NULL,
    `State` longtext CHARACTER SET utf8mb4 NULL,
    `CITY` longtext CHARACTER SET utf8mb4 NULL,
    `Area` longtext CHARACTER SET utf8mb4 NULL,
    `BaseBranchId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_GeoMaster` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_GeoMaster_ApplicationOrg_BaseBranchId` FOREIGN KEY (`BaseBranchId`) REFERENCES `ApplicationOrg` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CompanyUserARMScopeOfWork` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Product` varchar(50) CHARACTER SET utf8mb4 NULL,
    `ProductGroup` varchar(50) CHARACTER SET utf8mb4 NULL,
    `SubProduct` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Bucket` varchar(50) CHARACTER SET utf8mb4 NULL,
    `City` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Region` varchar(50) CHARACTER SET utf8mb4 NULL,
    `State` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Zone` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Branch` varchar(50) CHARACTER SET utf8mb4 NULL,
    `SupervisingManagerId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `ReportingAgencyId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `Location` varchar(100) CHARACTER SET utf8mb4 NULL,
    `CompanyUserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CompanyUserARMScopeOfWork` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_CompanyUserARMScopeOfWork_ApplicationOrg_ReportingAgencyId` FOREIGN KEY (`ReportingAgencyId`) REFERENCES `ApplicationOrg` (`Id`),
    CONSTRAINT `FK_CompanyUserARMScopeOfWork_ApplicationUser_CompanyUserId` FOREIGN KEY (`CompanyUserId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_CompanyUserARMScopeOfWork_ApplicationUser_SupervisingManager~` FOREIGN KEY (`SupervisingManagerId`) REFERENCES `ApplicationUser` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CompanyUserDesignation` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CompanyUserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `DepartmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `DesignationId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CompanyUserDesignation` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_CompanyUserDesignation_ApplicationUser_CompanyUserId` FOREIGN KEY (`CompanyUserId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_CompanyUserDesignation_Department_DepartmentId` FOREIGN KEY (`DepartmentId`) REFERENCES `Department` (`Id`),
    CONSTRAINT `FK_CompanyUserDesignation_Designation_DesignationId` FOREIGN KEY (`DesignationId`) REFERENCES `Designation` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CompanyUserPlaceOfWork` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `PIN` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CompanyUserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CompanyUserPlaceOfWork` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_CompanyUserPlaceOfWork_ApplicationUser_CompanyUserId` FOREIGN KEY (`CompanyUserId`) REFERENCES `ApplicationUser` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CompanyUserScopeOfWork` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Product` varchar(50) CHARACTER SET utf8mb4 NULL,
    `ProductGroup` varchar(50) CHARACTER SET utf8mb4 NULL,
    `SubProduct` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Bucket` varchar(50) CHARACTER SET utf8mb4 NULL,
    `City` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Region` varchar(50) CHARACTER SET utf8mb4 NULL,
    `State` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Zone` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Branch` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Location` varchar(100) CHARACTER SET utf8mb4 NULL,
    `SupervisingManagerId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `DepartmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `DesignationId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CompanyUserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CompanyUserScopeOfWork` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_CompanyUserScopeOfWork_ApplicationUser_CompanyUserId` FOREIGN KEY (`CompanyUserId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_CompanyUserScopeOfWork_ApplicationUser_SupervisingManagerId` FOREIGN KEY (`SupervisingManagerId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_CompanyUserScopeOfWork_Department_DepartmentId` FOREIGN KEY (`DepartmentId`) REFERENCES `Department` (`Id`),
    CONSTRAINT `FK_CompanyUserScopeOfWork_Designation_DesignationId` FOREIGN KEY (`DesignationId`) REFERENCES `Designation` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `GeoTagDetails` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `GeoTagReason` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Latitude` double NOT NULL,
    `Longitude` double NOT NULL,
    `GeoLocation` varchar(500) CHARACTER SET utf8mb4 NULL,
    `ApplicationUserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `TransactionType` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_GeoTagDetails` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_GeoTagDetails_ApplicationUser_ApplicationUserId` FOREIGN KEY (`ApplicationUserId`) REFERENCES `ApplicationUser` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Languages` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NULL,
    `ApplicationUserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Languages` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Languages_ApplicationUser_ApplicationUserId` FOREIGN KEY (`ApplicationUserId`) REFERENCES `ApplicationUser` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `LoanAccounts` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CustomId` varchar(100) CHARACTER SET utf8mb4 NULL,
    `AGREEMENTID` varchar(50) CHARACTER SET utf8mb4 NULL,
    `BRANCH` varchar(100) CHARACTER SET utf8mb4 NULL,
    `CUSTOMERID` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CUSTOMERNAME` varchar(100) CHARACTER SET utf8mb4 NULL,
    `DispCode` varchar(100) CHARACTER SET utf8mb4 NULL,
    `PRODUCT` varchar(100) CHARACTER SET utf8mb4 NULL,
    `SubProduct` varchar(100) CHARACTER SET utf8mb4 NULL,
    `ProductGroup` varchar(100) CHARACTER SET utf8mb4 NULL,
    `PTPDate` datetime(6) NULL,
    `Region` varchar(100) CHARACTER SET utf8mb4 NULL,
    `LatestMobileNo` varchar(50) CHARACTER SET utf8mb4 NULL,
    `LatestEmailId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `LatestLatitude` varchar(20) CHARACTER SET utf8mb4 NULL,
    `LatestLongitude` varchar(20) CHARACTER SET utf8mb4 NULL,
    `LatestPTPDate` datetime(6) NULL,
    `LatestPTPAmount` decimal(12,2) NULL,
    `LatestPaymentDate` datetime(6) NULL,
    `LatestFeedbackDate` datetime(6) NULL,
    `LatestFeedbackId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `BranchCode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `ProductCode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `GroupId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `DueDate` varchar(100) CHARACTER SET utf8mb4 NULL,
    `LenderId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CustomerPersona` varchar(200) CHARACTER SET utf8mb4 NULL,
    `IsDNDEnabled` tinyint(1) NOT NULL,
    `BOM_POS` decimal(18,2) NULL,
    `BUCKET` bigint NULL,
    `CITY` varchar(100) CHARACTER SET utf8mb4 NULL,
    `CURRENT_BUCKET` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AllocationOwnerExpiryDate` datetime(6) NULL,
    `CURRENT_DPD` bigint NULL,
    `CURRENT_POS` decimal(18,2) NULL,
    `DISBURSEDAMOUNT` varchar(50) CHARACTER SET utf8mb4 NULL,
    `EMI_OD_AMT` decimal(18,2) NULL,
    `EMI_START_DATE` varchar(50) CHARACTER SET utf8mb4 NULL,
    `EMIAMT` decimal(18,2) NULL,
    `INTEREST_OD` decimal(18,2) NULL,
    `MAILINGMOBILE` varchar(50) CHARACTER SET utf8mb4 NULL,
    `MAILINGZIPCODE` varchar(50) CHARACTER SET utf8mb4 NULL,
    `MONTH` int NULL,
    `NO_OF_EMI_OD` varchar(50) CHARACTER SET utf8mb4 NULL,
    `NPA_STAGEID` varchar(50) CHARACTER SET utf8mb4 NULL,
    `PENAL_PENDING` decimal(18,2) NULL,
    `PRINCIPAL_OD` decimal(65,30) NULL,
    `REGDNUM` varchar(100) CHARACTER SET utf8mb4 NULL,
    `STATE` varchar(100) CHARACTER SET utf8mb4 NULL,
    `PAYMENTSTATUS` varchar(50) CHARACTER SET utf8mb4 NULL,
    `YEAR` int NULL,
    `AgencyId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CollectorId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `TOS` varchar(50) CHARACTER SET utf8mb4 NULL,
    `TOTAL_ARREARS` varchar(50) CHARACTER SET utf8mb4 NULL,
    `OVERDUE_DATE` varchar(50) CHARACTER SET utf8mb4 NULL,
    `NEXT_DUE_DATE` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Excess` varchar(50) CHARACTER SET utf8mb4 NULL,
    `LOAN_STATUS` varchar(100) CHARACTER SET utf8mb4 NULL,
    `OTHER_CHARGES` varchar(100) CHARACTER SET utf8mb4 NULL,
    `TOTAL_OUTSTANDING` decimal(18,2) NULL,
    `OVERDUE_DAYS` varchar(100) CHARACTER SET utf8mb4 NULL,
    `DateOfBirth` datetime(6) NULL,
    `TeleCallingAgencyId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `TeleCallerId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AllocationOwnerId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AgencyAllocationExpiryDate` datetime(6) NULL,
    `TeleCallerAgencyAllocationExpiryDate` datetime(6) NULL,
    `AgentAllocationExpiryDate` datetime(6) NULL,
    `CollectorAllocationExpiryDate` datetime(6) NULL,
    `TeleCallerAllocationExpiryDate` datetime(6) NULL,
    `CO_APPLICANT1_NAME` varchar(100) CHARACTER SET utf8mb4 NULL,
    `NEXT_DUE_AMOUNT` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Paid` int NULL,
    `Attempted` int NULL,
    `UnAttempted` int NULL,
    `Partner_Loan_ID` varchar(50) CHARACTER SET utf8mb4 NULL,
    `IsEligibleForSettlement` tinyint(1) NULL,
    `IsEligibleForRepossession` tinyint(1) NULL,
    `IsEligibleForLegal` tinyint(1) NULL,
    `IsEligibleForRestructure` tinyint(1) NULL,
    `EMAIL_ID` varchar(50) CHARACTER SET utf8mb4 NULL,
    `PAN_CARD_DETAILS` varchar(25) CHARACTER SET utf8mb4 NULL,
    `SCHEME_DESC` varchar(80) CHARACTER SET utf8mb4 NULL,
    `ZONE` varchar(80) CHARACTER SET utf8mb4 NULL,
    `CentreID` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CentreName` varchar(50) CHARACTER SET utf8mb4 NULL,
    `GroupName` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Area` varchar(200) CHARACTER SET utf8mb4 NULL,
    `PRIMARY_CARD_NUMBER` varchar(25) CHARACTER SET utf8mb4 NULL,
    `BILLING_CYCLE` varchar(2) CHARACTER SET utf8mb4 NULL,
    `LAST_STATEMENT_DATE` datetime(6) NULL,
    `CURRENT_MINIMUM_AMOUNT_DUE` decimal(16,2) NULL,
    `CURRENT_TOTAL_AMOUNT_DUE` decimal(16,2) NULL,
    `RESIDENTIAL_CUSTOMER_CITY` varchar(30) CHARACTER SET utf8mb4 NULL,
    `RESIDENTIAL_CUSTOMER_STATE` varchar(30) CHARACTER SET utf8mb4 NULL,
    `RESIDENTIAL_PIN_CODE` varchar(10) CHARACTER SET utf8mb4 NULL,
    `RESIDENTIAL_COUNTRY` varchar(25) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_LoanAccounts` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_LoanAccounts_ApplicationOrg_AgencyId` FOREIGN KEY (`AgencyId`) REFERENCES `ApplicationOrg` (`Id`),
    CONSTRAINT `FK_LoanAccounts_ApplicationOrg_TeleCallingAgencyId` FOREIGN KEY (`TeleCallingAgencyId`) REFERENCES `ApplicationOrg` (`Id`),
    CONSTRAINT `FK_LoanAccounts_ApplicationUser_AllocationOwnerId` FOREIGN KEY (`AllocationOwnerId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_LoanAccounts_ApplicationUser_CollectorId` FOREIGN KEY (`CollectorId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_LoanAccounts_ApplicationUser_TeleCallerId` FOREIGN KEY (`TeleCallerId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_LoanAccounts_CategoryItem_LenderId` FOREIGN KEY (`LenderId`) REFERENCES `CategoryItem` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PayInSlips` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CustomId` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `CMSPayInSlipNo` varchar(100) CHARACTER SET utf8mb4 NULL,
    `BankName` varchar(200) CHARACTER SET utf8mb4 NULL,
    `BranchName` varchar(200) CHARACTER SET utf8mb4 NULL,
    `DateOfDeposit` datetime(6) NULL,
    `BankAccountNo` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AccountHolderName` varchar(100) CHARACTER SET utf8mb4 NULL,
    `ModeOfPayment` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Amount` decimal(18,2) NOT NULL,
    `Currency` varchar(50) CHARACTER SET utf8mb4 NULL,
    `IsPrintValid` tinyint(50) NULL,
    `PrintedById` varchar(32) CHARACTER SET utf8mb4 NULL,
    `PrintedDate` datetime(6) NULL,
    `Lattitude` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Longitude` varchar(50) CHARACTER SET utf8mb4 NULL,
    `PayInSlipImageName` varchar(250) CHARACTER SET utf8mb4 NULL,
    `PayinslipType` longtext CHARACTER SET utf8mb4 NULL,
    `ProductGroup` longtext CHARACTER SET utf8mb4 NULL,
    `PayInSlipWorkflowStateId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_PayInSlips` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_PayInSlips_ApplicationUser_PrintedById` FOREIGN KEY (`PrintedById`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_PayInSlips_PayInSlipWorkflowState_PayInSlipWorkflowStateId` FOREIGN KEY (`PayInSlipWorkflowStateId`) REFERENCES `PayInSlipWorkflowState` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Receipts` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CollectorId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CustomId` longtext CHARACTER SET utf8mb4 NULL,
    `ReceiptWorkflowStateId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Receipts` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Receipts_ApplicationUser_CollectorId` FOREIGN KEY (`CollectorId`) REFERENCES `ApplicationUser` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Receipts_ReceiptWorkflowState_ReceiptWorkflowStateId` FOREIGN KEY (`ReceiptWorkflowStateId`) REFERENCES `ReceiptWorkflowState` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserAttendanceDetail` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `TotalHours` double NOT NULL,
    `Date` datetime(6) NOT NULL,
    `ApplicationUserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_UserAttendanceDetail` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_UserAttendanceDetail_ApplicationUser_ApplicationUserId` FOREIGN KEY (`ApplicationUserId`) REFERENCES `ApplicationUser` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserAttendanceLog` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `ApplicationUserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `SessionId` longtext CHARACTER SET utf8mb4 NULL,
    `IsSessionValid` tinyint(1) NOT NULL,
    `LogOutLongitude` double NULL,
    `LogInLongitude` double NULL,
    `LogOutLatitude` double NULL,
    `LogInLatitude` double NULL,
    `LogInTime` datetime(6) NULL,
    `LogOutTime` datetime(6) NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_UserAttendanceLog` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_UserAttendanceLog_ApplicationUser_ApplicationUserId` FOREIGN KEY (`ApplicationUserId`) REFERENCES `ApplicationUser` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserCustomerPersona` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(50) CHARACTER SET utf8mb4 NULL,
    `ApplicationUserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_UserCustomerPersona` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_UserCustomerPersona_ApplicationUser_ApplicationUserId` FOREIGN KEY (`ApplicationUserId`) REFERENCES `ApplicationUser` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserPerformanceBand` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CompanyUserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AgencyUserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_UserPerformanceBand` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_UserPerformanceBand_ApplicationUser_AgencyUserId` FOREIGN KEY (`AgencyUserId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_UserPerformanceBand_ApplicationUser_CompanyUserId` FOREIGN KEY (`CompanyUserId`) REFERENCES `ApplicationUser` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserSearchCriteria` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `isdisable` tinyint(1) NULL,
    `FilterValues` longtext CHARACTER SET utf8mb4 NOT NULL,
    `filterName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `UserId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `UseCaseName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_UserSearchCriteria` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_UserSearchCriteria_ApplicationUser_UserId` FOREIGN KEY (`UserId`) REFERENCES `ApplicationUser` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AreaBaseBranchMappings` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `AreaId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `BaseBranchId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AreaBaseBranchMappings` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AreaBaseBranchMappings_ApplicationOrg_BaseBranchId` FOREIGN KEY (`BaseBranchId`) REFERENCES `ApplicationOrg` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AreaBaseBranchMappings_Areas_AreaId` FOREIGN KEY (`AreaId`) REFERENCES `Areas` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AreaPinCodeMappings` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `AreaId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `PinCodeId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AreaPinCodeMappings` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AreaPinCodeMappings_Areas_AreaId` FOREIGN KEY (`AreaId`) REFERENCES `Areas` (`Id`),
    CONSTRAINT `FK_AreaPinCodeMappings_PinCodes_PinCodeId` FOREIGN KEY (`PinCodeId`) REFERENCES `PinCodes` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Feedback` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `UploadedFileName` varchar(200) CHARACTER SET utf8mb4 NULL,
    `CustomerMet` varchar(100) CHARACTER SET utf8mb4 NULL,
    `DispositionCode` varchar(100) CHARACTER SET utf8mb4 NULL,
    `DispositionGroup` varchar(200) CHARACTER SET utf8mb4 NULL,
    `PTPDate` datetime(6) NULL,
    `EscalateTo` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Remarks` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `FeedbackDate` datetime(6) NULL,
    `IsReallocationRequest` varchar(20) CHARACTER SET utf8mb4 NULL,
    `ReallocationRequestReason` varchar(500) CHARACTER SET utf8mb4 NULL,
    `NewArea` varchar(200) CHARACTER SET utf8mb4 NULL,
    `NewAddress` varchar(500) CHARACTER SET utf8mb4 NULL,
    `City` varchar(200) CHARACTER SET utf8mb4 NULL,
    `NewContactNo` varchar(50) CHARACTER SET utf8mb4 NULL,
    `DispositionDate` datetime(6) NULL,
    `RightPartyContact` varchar(50) CHARACTER SET utf8mb4 NULL,
    `NextAction` varchar(50) CHARACTER SET utf8mb4 NULL,
    `NonPaymentReason` varchar(100) CHARACTER SET utf8mb4 NULL,
    `AssignReason` varchar(100) CHARACTER SET utf8mb4 NULL,
    `NewContactCountryCode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `NewContactAreaCode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `State` varchar(50) CHARACTER SET utf8mb4 NULL,
    `NewEmailId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `PickAddress` varchar(100) CHARACTER SET utf8mb4 NULL,
    `OtherPickAddress` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Latitude` double NULL,
    `Longitude` double NULL,
    `OfflineFeedbackDate` datetime(6) NULL,
    `CollectorId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AssigneeId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `UserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `GeoLocation` varchar(500) CHARACTER SET utf8mb4 NULL,
    `AccountId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `AssignTo` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `PTPAmount` decimal(65,30) NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Feedback` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Feedback_ApplicationUser_AssigneeId` FOREIGN KEY (`AssigneeId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_Feedback_ApplicationUser_CollectorId` FOREIGN KEY (`CollectorId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_Feedback_ApplicationUser_UserId` FOREIGN KEY (`UserId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_Feedback_LoanAccounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `LoanAccounts` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `LoanAccountJSON` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `AccountId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AccountJSON` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_LoanAccountJSON` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_LoanAccountJSON_LoanAccounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `LoanAccounts` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `PaymentTransactions` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `PaymentGatewayID` varchar(32) CHARACTER SET utf8mb4 NULL,
    `MerchantReferenceNumber` varchar(50) CHARACTER SET utf8mb4 NULL,
    `MerchantTransactionId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `BankTransactionId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `BankReferenceNumber` varchar(50) CHARACTER SET utf8mb4 NULL,
    `BankId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Amount` decimal(65,30) NULL,
    `Currency` varchar(50) CHARACTER SET utf8mb4 NULL,
    `TransactionDate` datetime(6) NULL,
    `StatusCode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Status` varchar(50) CHARACTER SET utf8mb4 NULL,
    `ResponseMessage` longtext CHARACTER SET utf8mb4 NULL,
    `ErrorMessage` longtext CHARACTER SET utf8mb4 NULL,
    `ErrorCode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `IsPaid` tinyint(1) NULL,
    `TransactionStatus` varchar(50) CHARACTER SET utf8mb4 NULL,
    `RRN` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AuthCode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CardNumber` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CardType` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CardHolderName` varchar(50) CHARACTER SET utf8mb4 NULL,
    `LoanAccountId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_PaymentTransactions` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_PaymentTransactions_LoanAccounts_LoanAccountId` FOREIGN KEY (`LoanAccountId`) REFERENCES `LoanAccounts` (`Id`),
    CONSTRAINT `FK_PaymentTransactions_PaymentGateways_PaymentGatewayID` FOREIGN KEY (`PaymentGatewayID`) REFERENCES `PaymentGateways` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `CollectionBatches` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CustomId` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Amount` decimal(18,2) NULL,
    `ProductGroup` varchar(100) CHARACTER SET utf8mb4 NULL,
    `CurrencyId` varchar(5) CHARACTER SET utf8mb4 NULL,
    `ModeOfPayment` longtext CHARACTER SET utf8mb4 NULL,
    `BankAccountNo` varchar(50) CHARACTER SET utf8mb4 NULL,
    `BankName` varchar(50) CHARACTER SET utf8mb4 NULL,
    `BranchName` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AccountHolderName` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Latitude` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Longitude` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CollectionBatchOrgId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AcknowledgedById` varchar(32) CHARACTER SET utf8mb4 NULL,
    `PayInSlipId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CollectionBatchWorkflowStateId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `BatchType` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CollectionBatches` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_CollectionBatches_ApplicationOrg_CollectionBatchOrgId` FOREIGN KEY (`CollectionBatchOrgId`) REFERENCES `ApplicationOrg` (`Id`),
    CONSTRAINT `FK_CollectionBatches_ApplicationUser_AcknowledgedById` FOREIGN KEY (`AcknowledgedById`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_CollectionBatches_CollectionBatchWorkflowState_CollectionBat~` FOREIGN KEY (`CollectionBatchWorkflowStateId`) REFERENCES `CollectionBatchWorkflowState` (`Id`),
    CONSTRAINT `FK_CollectionBatches_PayInSlips_PayInSlipId` FOREIGN KEY (`PayInSlipId`) REFERENCES `PayInSlips` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TreatmentOnCommunicationHistoryDetails` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `TreatmentHistoryId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LoanAccountId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CustomId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `ReasonForFailure` varchar(800) CHARACTER SET utf8mb4 NULL,
    `ReturnDate` datetime(6) NULL,
    `ReasonForReturn` longtext CHARACTER SET utf8mb4 NULL,
    `DispatchID` varchar(200) CHARACTER SET utf8mb4 NULL,
    `DispatchDate` datetime(6) NULL,
    `DeliveryDate` datetime(6) NULL,
    `Status` varchar(100) CHARACTER SET utf8mb4 NULL,
    `MessageContent` varchar(8000) CHARACTER SET utf8mb4 NULL,
    `WAapiResponse` varchar(2000) CHARACTER SET utf8mb4 NULL,
    `WADeliveredStatus` varchar(100) CHARACTER SET utf8mb4 NULL,
    `WADeliveredResponse` varchar(800) CHARACTER SET utf8mb4 NULL,
    `FileName` varchar(800) CHARACTER SET utf8mb4 NULL,
    `SubTreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `IsRead` tinyint(1) NOT NULL,
    `ReadDate` datetime(6) NULL,
    `IsDelivered` tinyint(1) NOT NULL,
    `SMSContentCreated` longtext CHARACTER SET utf8mb4 NULL,
    `SMSContentCreatedTimeStamp` datetime(6) NULL,
    `SMSContentRequest` longtext CHARACTER SET utf8mb4 NULL,
    `SMSContentRequestTimeStamp` datetime(6) NULL,
    `SMSResponse` longtext CHARACTER SET utf8mb4 NULL,
    `SMSResponseTimeStamp` datetime(6) NULL,
    `SMSResponseStatus` varchar(50) CHARACTER SET utf8mb4 NULL,
    `EmailContentCreated` longtext CHARACTER SET utf8mb4 NULL,
    `EmailContentCreatedTimeStamp` datetime(6) NULL,
    `EmailContentRequest` longtext CHARACTER SET utf8mb4 NULL,
    `EmailContentRequestTimeStamp` datetime(6) NULL,
    `EmailResponse` longtext CHARACTER SET utf8mb4 NULL,
    `EmailResponseTimeStamp` datetime(6) NULL,
    `EmailResponseStatus` varchar(50) CHARACTER SET utf8mb4 NULL,
    `WAContentCreated` longtext CHARACTER SET utf8mb4 NULL,
    `WAContentCreatedTimeStamp` datetime(6) NULL,
    `WAContentRequest` longtext CHARACTER SET utf8mb4 NULL,
    `WAContentRequestTimeStamp` datetime(6) NULL,
    `WAResponse` longtext CHARACTER SET utf8mb4 NULL,
    `WAResponseTimeStamp` datetime(6) NULL,
    `WAResponseStatus` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CommunicationTemplateId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `PaymentTransactionId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `SMS_Aggregator_TransactionID` varchar(200) CHARACTER SET utf8mb4 NULL,
    `WA_Aggregator_TransactionID` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Recipient_Operator` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Recipient_Circle` varchar(200) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TreatmentOnCommunicationHistoryDetails` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TreatmentOnCommunicationHistoryDetails_CommunicationTemplate~` FOREIGN KEY (`CommunicationTemplateId`) REFERENCES `CommunicationTemplate` (`Id`),
    CONSTRAINT `FK_TreatmentOnCommunicationHistoryDetails_LoanAccounts_LoanAcco~` FOREIGN KEY (`LoanAccountId`) REFERENCES `LoanAccounts` (`Id`),
    CONSTRAINT `FK_TreatmentOnCommunicationHistoryDetails_PaymentTransactions_P~` FOREIGN KEY (`PaymentTransactionId`) REFERENCES `PaymentTransactions` (`Id`),
    CONSTRAINT `FK_TreatmentOnCommunicationHistoryDetails_SubTreatment_SubTreat~` FOREIGN KEY (`SubTreatmentId`) REFERENCES `SubTreatment` (`Id`),
    CONSTRAINT `FK_TreatmentOnCommunicationHistoryDetails_TreatmentHistory_Trea~` FOREIGN KEY (`TreatmentHistoryId`) REFERENCES `TreatmentHistory` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Collections` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CustomId` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Amount` decimal(18,2) NULL,
    `CurrencyId` varchar(5) CHARACTER SET utf8mb4 NULL,
    `CollectionDate` datetime(6) NULL,
    `RecordNo` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CollectionMode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `MobileNo` varchar(50) CHARACTER SET utf8mb4 NULL,
    `ContactType` varchar(20) CHARACTER SET utf8mb4 NULL,
    `CountryCode` varchar(20) CHARACTER SET utf8mb4 NULL,
    `AreaCode` varchar(20) CHARACTER SET utf8mb4 NULL,
    `EMailId` varchar(200) CHARACTER SET utf8mb4 NULL,
    `PayerImageName` varchar(200) CHARACTER SET utf8mb4 NULL,
    `CustomerName` varchar(200) CHARACTER SET utf8mb4 NULL,
    `ChangeRequestImageName` varchar(200) CHARACTER SET utf8mb4 NULL,
    `PhysicalReceiptNumber` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Latitude` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Longitude` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Remarks` varchar(500) CHARACTER SET utf8mb4 NULL,
    `AccountId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CollectorId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AckingAgentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `ReceiptId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CollectionOrgId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CollectionBatchId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CashId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `ChequeId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `MailSentCount` int NOT NULL,
    `SMSSentCount` int NOT NULL,
    `TransactionNumber` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AcknowledgedDate` datetime(6) NULL,
    `CollectionWorkflowStateId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CancelledCollectionId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CancellationRemarks` varchar(500) CHARACTER SET utf8mb4 NULL,
    `OfflineCollectionDate` datetime(6) NULL,
    `GeoLocation` varchar(500) CHARACTER SET utf8mb4 NULL,
    `EncredibleUserId` varchar(500) CHARACTER SET utf8mb4 NULL,
    `yForeClosureAmount` varchar(50) CHARACTER SET utf8mb4 NULL,
    `yOverdueAmount` varchar(50) CHARACTER SET utf8mb4 NULL,
    `yBounceCharges` varchar(50) CHARACTER SET utf8mb4 NULL,
    `othercharges` varchar(50) CHARACTER SET utf8mb4 NULL,
    `yPenalInterest` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Settlement` varchar(50) CHARACTER SET utf8mb4 NULL,
    `yRelationshipWithCustomer` varchar(100) CHARACTER SET utf8mb4 NULL,
    `yPANNo` varchar(50) CHARACTER SET utf8mb4 NULL,
    `yUploadSource` varchar(50) CHARACTER SET utf8mb4 NULL,
    `yBatchUploadID` varchar(50) CHARACTER SET utf8mb4 NULL,
    `yTest` longtext CHARACTER SET utf8mb4 NULL,
    `DepositAccountNumber` varchar(50) CHARACTER SET utf8mb4 NULL,
    `DepositBankName` varchar(50) CHARACTER SET utf8mb4 NULL,
    `DepositBankBranch` varchar(50) CHARACTER SET utf8mb4 NULL,
    `IsPoolAccount` tinyint(1) NULL,
    `IsDepositAccount` tinyint(1) NULL,
    `ReceiptType` longtext CHARACTER SET utf8mb4 NULL,
    `IsNewPhonenumber` tinyint(1) NULL,
    `ErrorMessgae` longtext CHARACTER SET utf8mb4 NULL,
    `amountBreakUp1` decimal(16,2) NULL,
    `amountBreakUp2` decimal(16,2) NULL,
    `amountBreakUp3` decimal(16,2) NULL,
    `amountBreakUp4` decimal(16,2) NULL,
    `amountBreakUp5` decimal(16,2) NULL,
    `amountBreakUp6` decimal(16,2) NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Collections` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Collections_ApplicationOrg_CollectionOrgId` FOREIGN KEY (`CollectionOrgId`) REFERENCES `ApplicationOrg` (`Id`),
    CONSTRAINT `FK_Collections_ApplicationUser_AckingAgentId` FOREIGN KEY (`AckingAgentId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_Collections_ApplicationUser_CollectorId` FOREIGN KEY (`CollectorId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_Collections_Cash_CashId` FOREIGN KEY (`CashId`) REFERENCES `Cash` (`Id`),
    CONSTRAINT `FK_Collections_Cheques_ChequeId` FOREIGN KEY (`ChequeId`) REFERENCES `Cheques` (`Id`),
    CONSTRAINT `FK_Collections_CollectionBatches_CollectionBatchId` FOREIGN KEY (`CollectionBatchId`) REFERENCES `CollectionBatches` (`Id`),
    CONSTRAINT `FK_Collections_CollectionWorkflowState_CollectionWorkflowStateId` FOREIGN KEY (`CollectionWorkflowStateId`) REFERENCES `CollectionWorkflowState` (`Id`),
    CONSTRAINT `FK_Collections_Collections_CancelledCollectionId` FOREIGN KEY (`CancelledCollectionId`) REFERENCES `Collections` (`Id`),
    CONSTRAINT `FK_Collections_LoanAccounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `LoanAccounts` (`Id`),
    CONSTRAINT `FK_Collections_Receipts_ReceiptId` FOREIGN KEY (`ReceiptId`) REFERENCES `Receipts` (`Id`)
) CHARACTER SET=utf8mb4;

INSERT INTO `AgencyType` (`Id`, `CreatedBy`, `CreatedDate`, `IsDeleted`, `LastModifiedBy`, `LastModifiedDate`, `MainType`, `SubType`)
VALUES ('27d4c2e0ce1a438cb44cd7fb8ed552b9', NULL, TIMESTAMP '0001-01-01 00:00:00', FALSE, NULL, TIMESTAMP '0001-01-01 00:00:00', 'Collections', 'Tele calling'),
('ff379ce22f7b4aca9e74d0dadccb3739', NULL, TIMESTAMP '0001-01-01 00:00:00', FALSE, NULL, TIMESTAMP '0001-01-01 00:00:00', 'Collections', 'Field Agents');

CREATE INDEX `IX_AccessRights_AccountabilityTypeId` ON `AccessRights` (`AccountabilityTypeId`);

CREATE INDEX `IX_AccessRights_ActionMasterId` ON `AccessRights` (`ActionMasterId`);

CREATE INDEX `IX_AccessRights_MenuMasterId` ON `AccessRights` (`MenuMasterId`);

CREATE INDEX `IX_AccessRights_SubMenuMasterId` ON `AccessRights` (`SubMenuMasterId`);

CREATE INDEX `IX_Accountabilities_AccountabilityTypeId` ON `Accountabilities` (`AccountabilityTypeId`);

CREATE UNIQUE INDEX `IX_Accountabilities_CommisionerId_ResponsibleId_AccountabilityT~` ON `Accountabilities` (`CommisionerId`, `ResponsibleId`, `AccountabilityTypeId`);

CREATE INDEX `IX_ActionMaster_SubMenuMasterID` ON `ActionMaster` (`SubMenuMasterID`);

CREATE INDEX `IX_AgencyIdentification_TFlexId` ON `AgencyIdentification` (`TFlexId`);

CREATE INDEX `IX_AgencyIdentification_TFlexIdentificationDocTypeId` ON `AgencyIdentification` (`TFlexIdentificationDocTypeId`);

CREATE INDEX `IX_AgencyIdentification_TFlexIdentificationTypeId` ON `AgencyIdentification` (`TFlexIdentificationTypeId`);

CREATE INDEX `IX_AgencyIdentificationDoc_TFlexIdentificationId` ON `AgencyIdentificationDoc` (`TFlexIdentificationId`);

CREATE INDEX `IX_AgencyPlaceOfWork_AgencyId` ON `AgencyPlaceOfWork` (`AgencyId`);

CREATE INDEX `IX_AgencyPlaceOfWork_ManagerId` ON `AgencyPlaceOfWork` (`ManagerId`);

CREATE INDEX `IX_AgencyScopeOfWork_AgencyId` ON `AgencyScopeOfWork` (`AgencyId`);

CREATE INDEX `IX_AgencyUserDesignation_AgencyUserId` ON `AgencyUserDesignation` (`AgencyUserId`);

CREATE INDEX `IX_AgencyUserDesignation_DepartmentId` ON `AgencyUserDesignation` (`DepartmentId`);

CREATE INDEX `IX_AgencyUserDesignation_DesignationId` ON `AgencyUserDesignation` (`DesignationId`);

CREATE INDEX `IX_AgencyUserIdentification_TFlexId` ON `AgencyUserIdentification` (`TFlexId`);

CREATE INDEX `IX_AgencyUserIdentification_TFlexIdentificationDocTypeId` ON `AgencyUserIdentification` (`TFlexIdentificationDocTypeId`);

CREATE INDEX `IX_AgencyUserIdentification_TFlexIdentificationTypeId` ON `AgencyUserIdentification` (`TFlexIdentificationTypeId`);

CREATE INDEX `IX_AgencyUserIdentificationDoc_TFlexIdentificationId` ON `AgencyUserIdentificationDoc` (`TFlexIdentificationId`);

CREATE INDEX `IX_AgencyUserPlaceOfWork_AgencyUserId` ON `AgencyUserPlaceOfWork` (`AgencyUserId`);

CREATE INDEX `IX_AgencyUserScopeOfWork_AgencyUserId` ON `AgencyUserScopeOfWork` (`AgencyUserId`);

CREATE INDEX `IX_AgencyUserScopeOfWork_DepartmentId` ON `AgencyUserScopeOfWork` (`DepartmentId`);

CREATE INDEX `IX_AgencyUserScopeOfWork_DesignationId` ON `AgencyUserScopeOfWork` (`DesignationId`);

CREATE INDEX `IX_ApplicationOrg_AddressId` ON `ApplicationOrg` (`AddressId`);

CREATE INDEX `IX_ApplicationOrg_AgencyCategoryId` ON `ApplicationOrg` (`AgencyCategoryId`);

CREATE INDEX `IX_ApplicationOrg_AgencyTypeId` ON `ApplicationOrg` (`AgencyTypeId`);

CREATE INDEX `IX_ApplicationOrg_AgencyWorkflowStateId` ON `ApplicationOrg` (`AgencyWorkflowStateId`);

CREATE INDEX `IX_ApplicationOrg_CityId` ON `ApplicationOrg` (`CityId`);

CREATE INDEX `IX_ApplicationOrg_CreditAccountDetailsId` ON `ApplicationOrg` (`CreditAccountDetailsId`);

CREATE INDEX `IX_ApplicationOrg_DepartmentId` ON `ApplicationOrg` (`DepartmentId`);

CREATE INDEX `IX_ApplicationOrg_DesignationId` ON `ApplicationOrg` (`DesignationId`);

CREATE INDEX `IX_ApplicationOrg_ParentAgencyId` ON `ApplicationOrg` (`ParentAgencyId`);

CREATE INDEX `IX_ApplicationOrg_RecommendingOfficerId` ON `ApplicationOrg` (`RecommendingOfficerId`);

CREATE INDEX `IX_ApplicationUser_AddressId` ON `ApplicationUser` (`AddressId`);

CREATE INDEX `IX_ApplicationUser_AgencyId` ON `ApplicationUser` (`AgencyId`);

CREATE INDEX `IX_ApplicationUser_AgencyUserWorkflowStateId` ON `ApplicationUser` (`AgencyUserWorkflowStateId`);

CREATE INDEX `IX_ApplicationUser_ApplicationUserDetailsId` ON `ApplicationUser` (`ApplicationUserDetailsId`);

CREATE INDEX `IX_ApplicationUser_BaseBranchId` ON `ApplicationUser` (`BaseBranchId`);

CREATE INDEX `IX_ApplicationUser_CompanyId` ON `ApplicationUser` (`CompanyId`);

CREATE INDEX `IX_ApplicationUser_CompanyUserWorkflowStateId` ON `ApplicationUser` (`CompanyUserWorkflowStateId`);

CREATE INDEX `IX_ApplicationUser_CreditAccountDetailsId` ON `ApplicationUser` (`CreditAccountDetailsId`);

CREATE INDEX `IX_ApplicationUser_SinglePointReportingManagerId` ON `ApplicationUser` (`SinglePointReportingManagerId`);

CREATE INDEX `IX_ApplicationUser_UserCurrentLocationDetailsId` ON `ApplicationUser` (`UserCurrentLocationDetailsId`);

CREATE INDEX `IX_ApplicationUserDetails_AddressId` ON `ApplicationUserDetails` (`AddressId`);

CREATE INDEX `IX_AreaBaseBranchMappings_AreaId` ON `AreaBaseBranchMappings` (`AreaId`);

CREATE INDEX `IX_AreaBaseBranchMappings_BaseBranchId` ON `AreaBaseBranchMappings` (`BaseBranchId`);

CREATE INDEX `IX_AreaPinCodeMappings_AreaId` ON `AreaPinCodeMappings` (`AreaId`);

CREATE INDEX `IX_AreaPinCodeMappings_PinCodeId` ON `AreaPinCodeMappings` (`PinCodeId`);

CREATE INDEX `IX_Areas_BaseBranchId` ON `Areas` (`BaseBranchId`);

CREATE INDEX `IX_Areas_CityId` ON `Areas` (`CityId`);

CREATE INDEX `IX_BankBranch_BankId` ON `BankBranch` (`BankId`);

CREATE INDEX `IX_CategoryItem_CategoryMasterId` ON `CategoryItem` (`CategoryMasterId`);

CREATE INDEX `IX_CategoryItem_ParentId` ON `CategoryItem` (`ParentId`);

CREATE INDEX `IX_Cities_StateId` ON `Cities` (`StateId`);

CREATE INDEX `IX_CollectionBatches_AcknowledgedById` ON `CollectionBatches` (`AcknowledgedById`);

CREATE INDEX `IX_CollectionBatches_CollectionBatchOrgId` ON `CollectionBatches` (`CollectionBatchOrgId`);

CREATE INDEX `IX_CollectionBatches_CollectionBatchWorkflowStateId` ON `CollectionBatches` (`CollectionBatchWorkflowStateId`);

CREATE INDEX `IX_CollectionBatches_PayInSlipId` ON `CollectionBatches` (`PayInSlipId`);

CREATE INDEX `IX_Collections_AccountId` ON `Collections` (`AccountId`);

CREATE INDEX `IX_Collections_AckingAgentId` ON `Collections` (`AckingAgentId`);

CREATE INDEX `IX_Collections_CancelledCollectionId` ON `Collections` (`CancelledCollectionId`);

CREATE INDEX `IX_Collections_CashId` ON `Collections` (`CashId`);

CREATE INDEX `IX_Collections_ChequeId` ON `Collections` (`ChequeId`);

CREATE INDEX `IX_Collections_CollectionBatchId` ON `Collections` (`CollectionBatchId`);

CREATE INDEX `IX_Collections_CollectionOrgId` ON `Collections` (`CollectionOrgId`);

CREATE INDEX `IX_Collections_CollectionWorkflowStateId` ON `Collections` (`CollectionWorkflowStateId`);

CREATE INDEX `IX_Collections_CollectorId` ON `Collections` (`CollectorId`);

CREATE INDEX `IX_Collections_ReceiptId` ON `Collections` (`ReceiptId`);

CREATE INDEX `IX_CommunicationTemplate_CommunicationTemplateDetailId` ON `CommunicationTemplate` (`CommunicationTemplateDetailId`);

CREATE INDEX `IX_CommunicationTemplate_CommunicationTemplateWorkflowStateId` ON `CommunicationTemplate` (`CommunicationTemplateWorkflowStateId`);

CREATE INDEX `IX_CompanyUserARMScopeOfWork_CompanyUserId` ON `CompanyUserARMScopeOfWork` (`CompanyUserId`);

CREATE INDEX `IX_CompanyUserARMScopeOfWork_ReportingAgencyId` ON `CompanyUserARMScopeOfWork` (`ReportingAgencyId`);

CREATE INDEX `IX_CompanyUserARMScopeOfWork_SupervisingManagerId` ON `CompanyUserARMScopeOfWork` (`SupervisingManagerId`);

CREATE INDEX `IX_CompanyUserDesignation_CompanyUserId` ON `CompanyUserDesignation` (`CompanyUserId`);

CREATE INDEX `IX_CompanyUserDesignation_DepartmentId` ON `CompanyUserDesignation` (`DepartmentId`);

CREATE INDEX `IX_CompanyUserDesignation_DesignationId` ON `CompanyUserDesignation` (`DesignationId`);

CREATE INDEX `IX_CompanyUserPlaceOfWork_CompanyUserId` ON `CompanyUserPlaceOfWork` (`CompanyUserId`);

CREATE INDEX `IX_CompanyUserScopeOfWork_CompanyUserId` ON `CompanyUserScopeOfWork` (`CompanyUserId`);

CREATE INDEX `IX_CompanyUserScopeOfWork_DepartmentId` ON `CompanyUserScopeOfWork` (`DepartmentId`);

CREATE INDEX `IX_CompanyUserScopeOfWork_DesignationId` ON `CompanyUserScopeOfWork` (`DesignationId`);

CREATE INDEX `IX_CompanyUserScopeOfWork_SupervisingManagerId` ON `CompanyUserScopeOfWork` (`SupervisingManagerId`);

CREATE INDEX `IX_CreditAccountDetails_BankBranchId` ON `CreditAccountDetails` (`BankBranchId`);

CREATE INDEX `IX_Department_DepartmentTypeId` ON `Department` (`DepartmentTypeId`);

CREATE INDEX `IX_Designation_DepartmentId` ON `Designation` (`DepartmentId`);

CREATE INDEX `IX_Designation_DesignationTypeId` ON `Designation` (`DesignationTypeId`);

CREATE INDEX `IX_DispositionCodeMaster_DispositionGroupMasterId` ON `DispositionCodeMaster` (`DispositionGroupMasterId`);

CREATE INDEX `IX_DispositionValidationMaster_DispositionCodeMasterId` ON `DispositionValidationMaster` (`DispositionCodeMasterId`);

CREATE INDEX `IX_Feedback_AccountId` ON `Feedback` (`AccountId`);

CREATE INDEX `IX_Feedback_AssigneeId` ON `Feedback` (`AssigneeId`);

CREATE INDEX `IX_Feedback_CollectorId` ON `Feedback` (`CollectorId`);

CREATE INDEX `IX_Feedback_UserId` ON `Feedback` (`UserId`);

CREATE INDEX `IX_FlexDynamicBusinessRuleSequences_FlexBusinessContextId` ON `FlexDynamicBusinessRuleSequences` (`FlexBusinessContextId`);

CREATE INDEX `IX_GeoMaster_BaseBranchId` ON `GeoMaster` (`BaseBranchId`);

CREATE INDEX `IX_GeoTagDetails_ApplicationUserId` ON `GeoTagDetails` (`ApplicationUserId`);

CREATE INDEX `IX_Languages_ApplicationUserId` ON `Languages` (`ApplicationUserId`);

CREATE INDEX `IX_LoanAccountJSON_AccountId` ON `LoanAccountJSON` (`AccountId`);

CREATE INDEX `IX_LoanAccounts_AgencyId` ON `LoanAccounts` (`AgencyId`);

CREATE INDEX `IX_LoanAccounts_AllocationOwnerId` ON `LoanAccounts` (`AllocationOwnerId`);

CREATE INDEX `IX_LoanAccounts_CollectorId` ON `LoanAccounts` (`CollectorId`);

CREATE INDEX `IX_LoanAccounts_LenderId` ON `LoanAccounts` (`LenderId`);

CREATE INDEX `IX_LoanAccounts_TeleCallerId` ON `LoanAccounts` (`TeleCallerId`);

CREATE INDEX `IX_LoanAccounts_TeleCallingAgencyId` ON `LoanAccounts` (`TeleCallingAgencyId`);

CREATE INDEX `IX_MultilingualEntity_CultureId` ON `MultilingualEntity` (`CultureId`);

CREATE INDEX `IX_MultilingualEntity_MultilingualEntitySetId` ON `MultilingualEntity` (`MultilingualEntitySetId`);

CREATE INDEX `IX_PayInSlips_PayInSlipWorkflowStateId` ON `PayInSlips` (`PayInSlipWorkflowStateId`);

CREATE INDEX `IX_PayInSlips_PrintedById` ON `PayInSlips` (`PrintedById`);

CREATE INDEX `IX_PaymentTransactions_LoanAccountId` ON `PaymentTransactions` (`LoanAccountId`);

CREATE INDEX `IX_PaymentTransactions_PaymentGatewayID` ON `PaymentTransactions` (`PaymentGatewayID`);

CREATE INDEX `IX_Receipts_CollectorId` ON `Receipts` (`CollectorId`);

CREATE INDEX `IX_Receipts_ReceiptWorkflowStateId` ON `Receipts` (`ReceiptWorkflowStateId`);

CREATE INDEX `IX_Regions_CountryId` ON `Regions` (`CountryId`);

CREATE INDEX `IX_RoundRobinTreatment_SubTreatmentId` ON `RoundRobinTreatment` (`SubTreatmentId`);

CREATE INDEX `IX_RoundRobinTreatment_TreatmentId` ON `RoundRobinTreatment` (`TreatmentId`);

CREATE INDEX `IX_Segmentation_SegmentAdvanceFilterId` ON `Segmentation` (`SegmentAdvanceFilterId`);

CREATE INDEX `IX_State_RegionId` ON `State` (`RegionId`);

CREATE INDEX `IX_SubMenuMaster_MenuMasterId` ON `SubMenuMaster` (`MenuMasterId`);

CREATE INDEX `IX_SubTreatment_TreatmentId` ON `SubTreatment` (`TreatmentId`);

CREATE INDEX `IX_TFlexIdentificationDocTypes_TFlexIdentificationTypeId` ON `TFlexIdentificationDocTypes` (`TFlexIdentificationTypeId`);

CREATE INDEX `IX_TreatmentAndSegmentMapping_SegmentId` ON `TreatmentAndSegmentMapping` (`SegmentId`);

CREATE INDEX `IX_TreatmentAndSegmentMapping_TreatmentId` ON `TreatmentAndSegmentMapping` (`TreatmentId`);

CREATE INDEX `IX_TreatmentByRule_DepartmentId` ON `TreatmentByRule` (`DepartmentId`);

CREATE INDEX `IX_TreatmentByRule_DesignationId` ON `TreatmentByRule` (`DesignationId`);

CREATE INDEX `IX_TreatmentByRule_SubTreatmentId` ON `TreatmentByRule` (`SubTreatmentId`);

CREATE INDEX `IX_TreatmentByRule_TreatmentId` ON `TreatmentByRule` (`TreatmentId`);

CREATE INDEX `IX_TreatmentDesignation_DepartmentId` ON `TreatmentDesignation` (`DepartmentId`);

CREATE INDEX `IX_TreatmentDesignation_DesignationId` ON `TreatmentDesignation` (`DesignationId`);

CREATE INDEX `IX_TreatmentDesignation_SubTreatmentId` ON `TreatmentDesignation` (`SubTreatmentId`);

CREATE INDEX `IX_TreatmentDesignation_TreatmentId` ON `TreatmentDesignation` (`TreatmentId`);

CREATE INDEX `IX_TreatmentHistory_TreatmentId` ON `TreatmentHistory` (`TreatmentId`);

CREATE INDEX `IX_TreatmentOnAccount_DepartmentId` ON `TreatmentOnAccount` (`DepartmentId`);

CREATE INDEX `IX_TreatmentOnAccount_DesignationId` ON `TreatmentOnAccount` (`DesignationId`);

CREATE INDEX `IX_TreatmentOnAccount_SubTreatmentId` ON `TreatmentOnAccount` (`SubTreatmentId`);

CREATE INDEX `IX_TreatmentOnAccount_TreatmentId` ON `TreatmentOnAccount` (`TreatmentId`);

CREATE INDEX `IX_TreatmentOnCommunication_CommunicationTemplateId` ON `TreatmentOnCommunication` (`CommunicationTemplateId`);

CREATE UNIQUE INDEX `IX_TreatmentOnCommunication_SubTreatmentId` ON `TreatmentOnCommunication` (`SubTreatmentId`);

CREATE INDEX `IX_TreatmentOnCommunication_TreatmentId` ON `TreatmentOnCommunication` (`TreatmentId`);

CREATE INDEX `IX_TreatmentOnCommunicationHistoryDetails_CommunicationTemplate~` ON `TreatmentOnCommunicationHistoryDetails` (`CommunicationTemplateId`);

CREATE INDEX `IX_TreatmentOnCommunicationHistoryDetails_LoanAccountId` ON `TreatmentOnCommunicationHistoryDetails` (`LoanAccountId`);

CREATE INDEX `IX_TreatmentOnCommunicationHistoryDetails_PaymentTransactionId` ON `TreatmentOnCommunicationHistoryDetails` (`PaymentTransactionId`);

CREATE INDEX `IX_TreatmentOnCommunicationHistoryDetails_SubTreatmentId` ON `TreatmentOnCommunicationHistoryDetails` (`SubTreatmentId`);

CREATE INDEX `IX_TreatmentOnCommunicationHistoryDetails_TreatmentHistoryId` ON `TreatmentOnCommunicationHistoryDetails` (`TreatmentHistoryId`);

CREATE INDEX `IX_TreatmentOnPerformanceBand_SubTreatmentId` ON `TreatmentOnPerformanceBand` (`SubTreatmentId`);

CREATE INDEX `IX_TreatmentOnPerformanceBand_TreatmentId` ON `TreatmentOnPerformanceBand` (`TreatmentId`);

CREATE INDEX `IX_TreatmentOnPOS_DepartmentId` ON `TreatmentOnPOS` (`DepartmentId`);

CREATE INDEX `IX_TreatmentOnPOS_DesignationId` ON `TreatmentOnPOS` (`DesignationId`);

CREATE INDEX `IX_TreatmentOnPOS_SubTreatmentId` ON `TreatmentOnPOS` (`SubTreatmentId`);

CREATE INDEX `IX_TreatmentOnPOS_TreatmentId` ON `TreatmentOnPOS` (`TreatmentId`);

CREATE UNIQUE INDEX `IX_TreatmentOnUpdateTrail_SubTreatmentId` ON `TreatmentOnUpdateTrail` (`SubTreatmentId`);

CREATE INDEX `IX_TreatmentOnUpdateTrail_TreatmentId` ON `TreatmentOnUpdateTrail` (`TreatmentId`);

CREATE INDEX `IX_TreatmentQualifyingStatus_SubTreatmentId` ON `TreatmentQualifyingStatus` (`SubTreatmentId`);

CREATE INDEX `IX_TreatmentQualifyingStatus_TreatmentId` ON `TreatmentQualifyingStatus` (`TreatmentId`);

CREATE INDEX `IX_UserAttendanceDetail_ApplicationUserId` ON `UserAttendanceDetail` (`ApplicationUserId`);

CREATE INDEX `IX_UserAttendanceLog_ApplicationUserId` ON `UserAttendanceLog` (`ApplicationUserId`);

CREATE INDEX `IX_UserCustomerPersona_ApplicationUserId` ON `UserCustomerPersona` (`ApplicationUserId`);

CREATE INDEX `IX_UserPerformanceBand_AgencyUserId` ON `UserPerformanceBand` (`AgencyUserId`);

CREATE INDEX `IX_UserPerformanceBand_CompanyUserId` ON `UserPerformanceBand` (`CompanyUserId`);

CREATE INDEX `IX_UserSearchCriteria_UserId` ON `UserSearchCriteria` (`UserId`);

CREATE INDEX `IX_UserVerificationCodes_UserVerificationCodeTypeId` ON `UserVerificationCodes` (`UserVerificationCodeTypeId`);

ALTER TABLE `AgencyIdentification` ADD CONSTRAINT `FK_AgencyIdentification_ApplicationOrg_TFlexId` FOREIGN KEY (`TFlexId`) REFERENCES `ApplicationOrg` (`Id`) ON DELETE CASCADE;

ALTER TABLE `AgencyPlaceOfWork` ADD CONSTRAINT `FK_AgencyPlaceOfWork_ApplicationOrg_AgencyId` FOREIGN KEY (`AgencyId`) REFERENCES `ApplicationOrg` (`Id`);

ALTER TABLE `AgencyPlaceOfWork` ADD CONSTRAINT `FK_AgencyPlaceOfWork_ApplicationUser_ManagerId` FOREIGN KEY (`ManagerId`) REFERENCES `ApplicationUser` (`Id`);

ALTER TABLE `AgencyScopeOfWork` ADD CONSTRAINT `FK_AgencyScopeOfWork_ApplicationOrg_AgencyId` FOREIGN KEY (`AgencyId`) REFERENCES `ApplicationOrg` (`Id`);

ALTER TABLE `AgencyUserDesignation` ADD CONSTRAINT `FK_AgencyUserDesignation_ApplicationUser_AgencyUserId` FOREIGN KEY (`AgencyUserId`) REFERENCES `ApplicationUser` (`Id`);

ALTER TABLE `AgencyUserIdentification` ADD CONSTRAINT `FK_AgencyUserIdentification_ApplicationUser_TFlexId` FOREIGN KEY (`TFlexId`) REFERENCES `ApplicationUser` (`Id`) ON DELETE CASCADE;

ALTER TABLE `AgencyUserPlaceOfWork` ADD CONSTRAINT `FK_AgencyUserPlaceOfWork_ApplicationUser_AgencyUserId` FOREIGN KEY (`AgencyUserId`) REFERENCES `ApplicationUser` (`Id`);

ALTER TABLE `AgencyUserScopeOfWork` ADD CONSTRAINT `FK_AgencyUserScopeOfWork_ApplicationUser_AgencyUserId` FOREIGN KEY (`AgencyUserId`) REFERENCES `ApplicationUser` (`Id`);

ALTER TABLE `ApplicationOrg` ADD CONSTRAINT `FK_ApplicationOrg_ApplicationUser_RecommendingOfficerId` FOREIGN KEY (`RecommendingOfficerId`) REFERENCES `ApplicationUser` (`Id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240813115101_Initial', '8.0.6');

COMMIT;

