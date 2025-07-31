IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AccountabilityTypes] (
    [Id] nvarchar(32) NOT NULL,
    [Description] nvarchar(50) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AccountabilityTypes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AccountLabels] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Label] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AccountLabels] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Address] (
    [Id] nvarchar(32) NOT NULL,
    [AddressLine1] nvarchar(100) NULL,
    [AddressLine2] nvarchar(100) NULL,
    [AddressLine3] nvarchar(100) NULL,
    [LandMark] nvarchar(100) NULL,
    [State] nvarchar(50) NULL,
    [City] nvarchar(50) NULL,
    [PIN] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AgencyCategory] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AgencyCategory] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AgencyType] (
    [Id] nvarchar(32) NOT NULL,
    [MainType] nvarchar(50) NULL,
    [SubType] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AgencyType] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AgencyUserWorkflowState] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Discriminator] nvarchar(34) NOT NULL,
    [TFlexId] nvarchar(32) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [StateChangedBy] nvarchar(32) NULL,
    [StateChangedDate] datetimeoffset NULL,
    [IsCompleted] bit NOT NULL,
    CONSTRAINT [PK_AgencyUserWorkflowState] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AgencyWorkflowState] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Discriminator] nvarchar(34) NOT NULL,
    [TFlexId] nvarchar(32) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [StateChangedBy] nvarchar(32) NULL,
    [StateChangedDate] datetimeoffset NULL,
    [IsCompleted] bit NOT NULL,
    CONSTRAINT [PK_AgencyWorkflowState] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AllocationDownload] (
    [Id] nvarchar(32) NOT NULL,
    [InputJson] nvarchar(1000) NULL,
    [FileName] nvarchar(250) NULL,
    [FilePath] nvarchar(500) NULL,
    [Status] nvarchar(50) NULL,
    [CustomId] nvarchar(50) NULL,
    [AllocationType] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AllocationDownload] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Bank] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(200) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Bank] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [BankAccountType] (
    [Id] nvarchar(32) NOT NULL,
    [Value] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_BankAccountType] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Buckets] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(50) NULL,
    [Description] nvarchar(100) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Buckets] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [BulkTrailUploadFile] (
    [Id] nvarchar(32) NOT NULL,
    [Description] nvarchar(200) NULL,
    [FileName] nvarchar(250) NULL,
    [FilePath] nvarchar(250) NULL,
    [FileUploadedDate] datetime2 NOT NULL,
    [FileProcessedDateTime] datetime2 NOT NULL,
    [Status] nvarchar(50) NULL,
    [CustomId] nvarchar(50) NULL,
    [MD5Hash] nvarchar(200) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_BulkTrailUploadFile] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [BulkUploadFile] (
    [Id] nvarchar(32) NOT NULL,
    [CustomId] nvarchar(50) NOT NULL,
    [Description] nvarchar(200) NOT NULL,
    [FileName] nvarchar(250) NOT NULL,
    [FilePath] nvarchar(250) NOT NULL,
    [FileUploadedDate] datetime2 NOT NULL,
    [FileProcessedDateTime] datetime2 NOT NULL,
    [Status] nvarchar(200) NOT NULL,
    [StatusFileName] nvarchar(250) NOT NULL,
    [StatusFilePath] nvarchar(250) NOT NULL,
    [MD5Hash] nvarchar(200) NOT NULL,
    [IsUploadstatus] bit NULL,
    [RowsError] int NULL,
    [RowsProcessed] int NULL,
    [RowsSuccess] int NULL,
    [FileType] nvarchar(200) NOT NULL,
    [AllocationType] nvarchar(200) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_BulkUploadFile] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Cash] (
    [Id] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Cash] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CategoryMaster] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(250) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_CategoryMaster] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Cheques] (
    [Id] nvarchar(32) NOT NULL,
    [BankName] nvarchar(100) NULL,
    [BranchName] nvarchar(100) NULL,
    [InstrumentNo] nvarchar(50) NULL,
    [InstrumentDate] datetime2 NULL,
    [MICRCode] nvarchar(50) NULL,
    [IFSCCode] nvarchar(50) NULL,
    [BankCity] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Cheques] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CollectionBatchWorkflowState] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Discriminator] nvarchar(55) NOT NULL,
    [TFlexId] nvarchar(32) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [StateChangedBy] nvarchar(32) NULL,
    [StateChangedDate] datetimeoffset NULL,
    [IsCompleted] bit NOT NULL,
    CONSTRAINT [PK_CollectionBatchWorkflowState] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CollectionWorkflowState] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Discriminator] nvarchar(34) NOT NULL,
    [TFlexId] nvarchar(32) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [StateChangedBy] nvarchar(32) NULL,
    [StateChangedDate] datetimeoffset NULL,
    [IsCompleted] bit NOT NULL,
    CONSTRAINT [PK_CollectionWorkflowState] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CommunicationTemplateDetail] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(max) NULL,
    [Salutation] nvarchar(max) NULL,
    [Body] nvarchar(max) NULL,
    [Signature] nvarchar(max) NULL,
    [Subject] nvarchar(max) NULL,
    [AddressTo] nvarchar(max) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_CommunicationTemplateDetail] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CommunicationTemplateWorkflowState] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Discriminator] nvarchar(34) NOT NULL,
    [TFlexId] nvarchar(32) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [StateChangedBy] nvarchar(32) NULL,
    [StateChangedDate] datetimeoffset NULL,
    [IsCompleted] bit NOT NULL,
    CONSTRAINT [PK_CommunicationTemplateWorkflowState] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Company] (
    [Id] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Company] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CompanyUserWorkflowState] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Discriminator] nvarchar(34) NOT NULL,
    [TFlexId] nvarchar(32) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [StateChangedBy] nvarchar(32) NULL,
    [StateChangedDate] datetimeoffset NULL,
    [IsCompleted] bit NOT NULL,
    CONSTRAINT [PK_CompanyUserWorkflowState] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Countries] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(300) NOT NULL,
    [NickName] nvarchar(50) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Culture] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Culture] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [DepartmentType] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_DepartmentType] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [DepositBankMaster] (
    [Id] nvarchar(32) NOT NULL,
    [DepositBankName] nvarchar(100) NULL,
    [DepositBranchName] nvarchar(100) NULL,
    [DepositAccountNumber] nvarchar(50) NULL,
    [AccountHolderName] nvarchar(100) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_DepositBankMaster] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [DesignationType] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_DesignationType] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [DeviceDetail] (
    [Id] nvarchar(32) NOT NULL,
    [OldIMEI] nvarchar(100) NULL,
    [IMEI] nvarchar(100) NULL,
    [UserId] nvarchar(50) NULL,
    [Email] nvarchar(100) NULL,
    [OTP] nvarchar(max) NULL,
    [OTPTimeStamp] datetimeoffset NOT NULL,
    [IsVerified] bit NOT NULL,
    [VerifiedDate] datetimeoffset NULL,
    [OTPCount] int NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_DeviceDetail] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [DispositionGroupMaster] (
    [Id] nvarchar(32) NOT NULL,
    [SrNo] bigint NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [NickName] nvarchar(max) NOT NULL,
    [DispositionAccess] nvarchar(150) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_DispositionGroupMaster] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [FeatureMaster] (
    [Id] nvarchar(32) NOT NULL,
    [Parameter] nvarchar(max) NOT NULL,
    [Value] nvarchar(max) NOT NULL,
    [IsEnabled] bit NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_FeatureMaster] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [FlexBusinessContext] (
    [Id] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_FlexBusinessContext] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [IdConfigMaster] (
    [Id] nvarchar(32) NOT NULL,
    [CodeType] nvarchar(100) NOT NULL,
    [BaseValue] int NOT NULL,
    [LatestValue] int NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_IdConfigMaster] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [IdConfigMaster_SeedData] (
    [Id] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_IdConfigMaster_SeedData] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MasterFileStatus] (
    [Id] nvarchar(32) NOT NULL,
    [CustomId] nvarchar(50) NULL,
    [FileName] nvarchar(250) NULL,
    [FilePath] nvarchar(250) NULL,
    [FileProcessedDateTime] datetime2 NULL,
    [FileUploadedDate] datetime2 NULL,
    [Status] nvarchar(300) NULL,
    [UploadType] nvarchar(100) NULL,
    [Description] nvarchar(2000) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_MasterFileStatus] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MenuMaster] (
    [Id] nvarchar(32) NOT NULL,
    [MenuName] nvarchar(100) NULL,
    [Etc] nvarchar(100) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_MenuMaster] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MultilingualEntitySet] (
    [Id] nvarchar(32) NOT NULL,
    [DefaultValue] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_MultilingualEntitySet] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PayInSlipWorkflowState] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Discriminator] nvarchar(34) NOT NULL,
    [TFlexId] nvarchar(32) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [StateChangedBy] nvarchar(32) NULL,
    [StateChangedDate] datetimeoffset NULL,
    [IsCompleted] bit NOT NULL,
    CONSTRAINT [PK_PayInSlipWorkflowState] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PaymentGateways] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(50) NULL,
    [MerchantId] nvarchar(50) NULL,
    [MerchantKey] nvarchar(50) NULL,
    [APIKey] nvarchar(150) NULL,
    [ChecksumKey] nvarchar(150) NULL,
    [PostURL] nvarchar(500) NULL,
    [ReturnURL] nvarchar(500) NULL,
    [ServerToServerURL] nvarchar(500) NULL,
    [ErrorURL] nvarchar(500) NULL,
    [CancelURL] nvarchar(500) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_PaymentGateways] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PinCodes] (
    [Id] nvarchar(32) NOT NULL,
    [Value] nvarchar(500) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_PinCodes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PrimaryAllocationFile] (
    [Id] nvarchar(32) NOT NULL,
    [Description] nvarchar(200) NULL,
    [FileName] nvarchar(250) NULL,
    [FilePath] nvarchar(250) NULL,
    [FileUploadedDate] datetime2 NOT NULL,
    [FileProcessedDateTime] datetime2 NOT NULL,
    [Status] nvarchar(50) NULL,
    [CustomId] nvarchar(50) NULL,
    [UploadType] nvarchar(100) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_PrimaryAllocationFile] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PrimaryUnAllocationFile] (
    [Id] nvarchar(32) NOT NULL,
    [CustomId] nvarchar(50) NULL,
    [UploadType] nvarchar(100) NULL,
    [FileName] nvarchar(250) NULL,
    [FilePath] nvarchar(250) NULL,
    [Status] nvarchar(50) NULL,
    [UploadedDate] datetime2 NOT NULL,
    [ProcessedDateTime] datetime2 NULL,
    [Description] nvarchar(200) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_PrimaryUnAllocationFile] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [ReceiptWorkflowState] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Discriminator] nvarchar(34) NOT NULL,
    [TFlexId] nvarchar(32) NOT NULL,
    [IsDeleted] bit NOT NULL,
    [StateChangedBy] nvarchar(32) NULL,
    [StateChangedDate] datetimeoffset NULL,
    [IsCompleted] bit NOT NULL,
    CONSTRAINT [PK_ReceiptWorkflowState] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [RunStatus] (
    [Id] nvarchar(32) NOT NULL,
    [Status] nvarchar(200) NOT NULL,
    [CustomId] nvarchar(200) NOT NULL,
    [ProcessType] nvarchar(800) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_RunStatus] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SecondaryAllocationFile] (
    [Id] nvarchar(32) NOT NULL,
    [Description] nvarchar(200) NULL,
    [FileName] nvarchar(250) NULL,
    [FilePath] nvarchar(250) NULL,
    [Status] nvarchar(50) NULL,
    [CustomId] nvarchar(50) NULL,
    [UploadType] nvarchar(100) NULL,
    [FileUploadedDate] datetime2 NOT NULL,
    [FileProcessedDateTime] datetime2 NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_SecondaryAllocationFile] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SecondaryUnAllocationFile] (
    [Id] nvarchar(32) NOT NULL,
    [CustomId] nvarchar(50) NULL,
    [UploadType] nvarchar(100) NULL,
    [FileName] nvarchar(250) NULL,
    [FilePath] nvarchar(250) NULL,
    [Status] nvarchar(50) NULL,
    [UploadedDate] datetime2 NOT NULL,
    [ProcessedDateTime] datetime2 NULL,
    [Description] nvarchar(200) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_SecondaryUnAllocationFile] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SegmentationAdvanceFilter] (
    [Id] nvarchar(32) NOT NULL,
    [BOM_POS] nvarchar(50) NULL,
    [CHARGEOFF_DATE] nvarchar(50) NULL,
    [CURRENT_POS] nvarchar(50) NULL,
    [LOAN_AMOUNT] nvarchar(50) NULL,
    [NONSTARTER] nvarchar(50) NULL,
    [NPA_STAGEID] nvarchar(50) NULL,
    [PRINCIPAL_OD] nvarchar(50) NULL,
    [TOS] nvarchar(50) NULL,
    [Area] nvarchar(50) NULL,
    [LastDispositionCode] nvarchar(250) NULL,
    [LastPaymentDate] nvarchar(50) NULL,
    [DispCode] nvarchar(200) NULL,
    [PTPDate] nvarchar(50) NULL,
    [CustomerPersona] nvarchar(200) NULL,
    [CurrentDPD] nvarchar(50) NULL,
    [CreditBureauScore] nvarchar(50) NULL,
    [CustomerBehaviourScore1] nvarchar(50) NULL,
    [CustomerBehaviourScore2] nvarchar(50) NULL,
    [EarlyWarningScore] nvarchar(50) NULL,
    [LegalStage] nvarchar(50) NULL,
    [RepoStage] nvarchar(50) NULL,
    [SettlementStage] nvarchar(50) NULL,
    [CustomerBehaviorScoreToKeepHisWord] nvarchar(50) NULL,
    [PreferredModeOfPayment] nvarchar(50) NULL,
    [PropensityToPayOnline] nvarchar(50) NULL,
    [DigitalContactabilityScore] nvarchar(50) NULL,
    [CallContactabilityScore] nvarchar(50) NULL,
    [FieldContactabilityScore] nvarchar(50) NULL,
    [Latest_Status_Of_SMS] nvarchar(50) NULL,
    [Latest_Status_Of_WhatsUp] nvarchar(50) NULL,
    [StatementDate] nvarchar(50) NULL,
    [DueDate] nvarchar(50) NULL,
    [TotalOverdueAmount] nvarchar(50) NULL,
    [DNDFlag] nvarchar(50) NULL,
    [MinimumAmountDue] nvarchar(50) NULL,
    [Month] nvarchar(50) NULL,
    [Year] nvarchar(50) NULL,
    [LOAN_STATUS] nvarchar(50) NULL,
    [EMI_OD_AMT] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_SegmentationAdvanceFilter] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SegmentationAdvanceFilterMasters] (
    [Id] nvarchar(32) NOT NULL,
    [FieldName] nvarchar(200) NULL,
    [FieldId] nvarchar(200) NULL,
    [Operator] nvarchar(100) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_SegmentationAdvanceFilterMasters] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Sequence] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Value] int NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Sequence] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TFlexIdentificationTypes] (
    [Id] nvarchar(32) NOT NULL,
    [Description] nvarchar(50) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TFlexIdentificationTypes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Treatment] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(300) NULL,
    [Description] nvarchar(100) NULL,
    [Mode] nvarchar(50) NULL,
    [IsDisabled] bit NULL,
    [Sequence] int NULL,
    [PaymentStatusToStop] nvarchar(50) NULL,
    [ExecutionStartdate] datetime2 NULL,
    [ExecutionEnddate] datetime2 NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Treatment] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TreatmentHistoryDetails] (
    [Id] nvarchar(32) NOT NULL,
    [bucket] nvarchar(20) NULL,
    [agreementid] nvarchar(100) NULL,
    [customername] nvarchar(300) NULL,
    [allocationownerid] nvarchar(50) NULL,
    [AllocationOwnerName] nvarchar(300) NULL,
    [telecallingagencyid] nvarchar(50) NULL,
    [TCallingAgencyName] nvarchar(300) NULL,
    [current_dpd] nvarchar(10) NULL,
    [telecallerid] nvarchar(50) NULL,
    [agencyid] nvarchar(50) NULL,
    [AgencyName] nvarchar(300) NULL,
    [collectorid] nvarchar(50) NULL,
    [AgentName] nvarchar(300) NULL,
    [treatmentid] nvarchar(50) NULL,
    [TreatmentName] nvarchar(200) NULL,
    [SegmentationName] nvarchar(200) NULL,
    [TCallingAgentName] nvarchar(300) NULL,
    [treatmenthistoryid] nvarchar(50) NULL,
    [npa_stageid] nvarchar(200) NULL,
    [productgroup] nvarchar(200) NULL,
    [latestmobileno] nvarchar(50) NULL,
    [state] nvarchar(100) NULL,
    [zone] nvarchar(100) NULL,
    [segmentationid] nvarchar(100) NULL,
    [bom_pos] float NULL,
    [current_pos] decimal(18,2) NULL,
    [current_bucket] nvarchar(50) NULL,
    [loan_amount] nvarchar(100) NULL,
    [tos] nvarchar(100) NULL,
    [dispcode] nvarchar(100) NULL,
    [branch] nvarchar(200) NULL,
    [city] nvarchar(200) NULL,
    [product] nvarchar(200) NULL,
    [subproduct] nvarchar(200) NULL,
    [region] nvarchar(200) NULL,
    [principal_od] nvarchar(200) NULL,
    [customerid] nvarchar(200) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TreatmentHistoryDetails] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TreatmentUpdateIntermediate] (
    [Id] nvarchar(32) NOT NULL,
    [AgreementId] nvarchar(50) NULL,
    [AllocationOwnerId] nvarchar(50) NULL,
    [TCAgencyId] nvarchar(50) NULL,
    [AgencyId] nvarchar(50) NULL,
    [TellecallerId] nvarchar(50) NULL,
    [CollectorId] nvarchar(50) NULL,
    [TreatmentId] nvarchar(50) NULL,
    [WorkRequestId] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TreatmentUpdateIntermediate] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserCurrentLocationDetails] (
    [Id] nvarchar(32) NOT NULL,
    [Latitude] float NULL,
    [Longitude] float NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UserCurrentLocationDetails] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserLoginKeys] (
    [Id] nvarchar(32) NOT NULL,
    [Key] nvarchar(50) NULL,
    [IsActive] bit NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UserLoginKeys] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserPerformanceBandMaster] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UserPerformanceBandMaster] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserPersonaMaster] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [Code] nvarchar(50) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UserPersonaMaster] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UsersUpdateFile] (
    [Id] nvarchar(32) NOT NULL,
    [CustomId] nvarchar(50) NOT NULL,
    [UploadType] nvarchar(100) NOT NULL,
    [FileName] nvarchar(250) NOT NULL,
    [FilePath] nvarchar(250) NOT NULL,
    [Status] nvarchar(50) NOT NULL,
    [UploadedDate] datetime2 NOT NULL,
    [ProcessedDateTime] datetime2 NULL,
    [Description] nvarchar(200) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UsersUpdateFile] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserVerificationCodeTypes] (
    [Id] nvarchar(32) NOT NULL,
    [Description] nvarchar(800) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UserVerificationCodeTypes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Zone] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [NickName] nvarchar(50) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Zone] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Accountabilities] (
    [Id] nvarchar(32) NOT NULL,
    [CommisionerId] nvarchar(32) NOT NULL,
    [ResponsibleId] nvarchar(32) NOT NULL,
    [AccountabilityTypeId] nvarchar(32) NOT NULL,
    [ValidFrom] datetime2 NULL,
    [ValidTo] datetime2 NULL,
    [LastRenewalDate] datetime2 NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Accountabilities] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Accountabilities_AccountabilityTypes_AccountabilityTypeId] FOREIGN KEY ([AccountabilityTypeId]) REFERENCES [AccountabilityTypes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ApplicationUserDetails] (
    [Id] nvarchar(32) NOT NULL,
    [PanNumber] nvarchar(20) NULL,
    [AadharNumber] nvarchar(20) NULL,
    [Gender] nvarchar(20) NULL,
    [AddressId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ApplicationUserDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApplicationUserDetails_Address_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Address] ([Id])
);
GO

CREATE TABLE [BankBranch] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(200) NULL,
    [Code] nvarchar(50) NULL,
    [MICR] nvarchar(50) NULL,
    [BankId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_BankBranch] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BankBranch_Bank_BankId] FOREIGN KEY ([BankId]) REFERENCES [Bank] ([Id])
);
GO

CREATE TABLE [CategoryItem] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(250) NULL,
    [CategoryMasterId] nvarchar(32) NULL,
    [ParentId] nvarchar(32) NULL,
    [Code] nvarchar(100) NULL,
    [Description] nvarchar(700) NULL,
    [IsDisabled] bit NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_CategoryItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CategoryItem_CategoryItem_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [CategoryItem] ([Id]),
    CONSTRAINT [FK_CategoryItem_CategoryMaster_CategoryMasterId] FOREIGN KEY ([CategoryMasterId]) REFERENCES [CategoryMaster] ([Id])
);
GO

CREATE TABLE [CommunicationTemplate] (
    [Id] nvarchar(32) NOT NULL,
    [TemplateType] nvarchar(max) NULL,
    [Recipient] nvarchar(max) NULL,
    [CommunicationTemplateDetailId] nvarchar(32) NULL,
    [IsDisabled] bit NOT NULL,
    [CommunicationTemplateWorkflowStateId] nvarchar(32) NOT NULL,
    [WATemplateId] nvarchar(50) NULL,
    [Language] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_CommunicationTemplate] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CommunicationTemplate_CommunicationTemplateDetail_CommunicationTemplateDetailId] FOREIGN KEY ([CommunicationTemplateDetailId]) REFERENCES [CommunicationTemplateDetail] ([Id]),
    CONSTRAINT [FK_CommunicationTemplate_CommunicationTemplateWorkflowState_CommunicationTemplateWorkflowStateId] FOREIGN KEY ([CommunicationTemplateWorkflowStateId]) REFERENCES [CommunicationTemplateWorkflowState] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Regions] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [NickName] nvarchar(50) NOT NULL,
    [CountryId] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Regions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Regions_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Countries] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Department] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(50) NULL,
    [Acronym] nvarchar(50) NULL,
    [Code] nvarchar(50) NULL,
    [DepartmentTypeId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Department] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Department_DepartmentType_DepartmentTypeId] FOREIGN KEY ([DepartmentTypeId]) REFERENCES [DepartmentType] ([Id])
);
GO

CREATE TABLE [DispositionCodeMaster] (
    [Id] nvarchar(32) NOT NULL,
    [DispositionGroupMasterId] nvarchar(32) NOT NULL,
    [SrNo] bigint NOT NULL,
    [DispositionCode] nvarchar(max) NOT NULL,
    [Permissibleforfieldagent] nvarchar(max) NOT NULL,
    [ShortDescription] nvarchar(max) NOT NULL,
    [LongDescription] nvarchar(max) NOT NULL,
    [DispositionAccess] nvarchar(150) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_DispositionCodeMaster] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DispositionCodeMaster_DispositionGroupMaster_DispositionGroupMasterId] FOREIGN KEY ([DispositionGroupMasterId]) REFERENCES [DispositionGroupMaster] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [FlexDynamicBusinessRuleSequences] (
    [Id] nvarchar(32) NOT NULL,
    [FlexBusinessContextId] nvarchar(50) NOT NULL,
    [PluginId] nvarchar(32) NOT NULL,
    [Sequence] int NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_FlexDynamicBusinessRuleSequences] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FlexDynamicBusinessRuleSequences_FlexBusinessContext_FlexBusinessContextId] FOREIGN KEY ([FlexBusinessContextId]) REFERENCES [FlexBusinessContext] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SubMenuMaster] (
    [Id] nvarchar(32) NOT NULL,
    [SubMenuName] nvarchar(100) NULL,
    [hasAccess] bit NOT NULL,
    [MenuMasterId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_SubMenuMaster] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SubMenuMaster_MenuMaster_MenuMasterId] FOREIGN KEY ([MenuMasterId]) REFERENCES [MenuMaster] ([Id])
);
GO

CREATE TABLE [MultilingualEntity] (
    [Id] nvarchar(32) NOT NULL,
    [MultilingualEntitySetId] nvarchar(32) NULL,
    [CultureId] nvarchar(32) NOT NULL,
    [Value] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_MultilingualEntity] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MultilingualEntity_Culture_CultureId] FOREIGN KEY ([CultureId]) REFERENCES [Culture] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MultilingualEntity_MultilingualEntitySet_MultilingualEntitySetId] FOREIGN KEY ([MultilingualEntitySetId]) REFERENCES [MultilingualEntitySet] ([Id])
);
GO

CREATE TABLE [Segmentation] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(100) NULL,
    [ExecutionType] nvarchar(20) NULL,
    [Description] nvarchar(500) NULL,
    [ProductGroup] nvarchar(500) NULL,
    [Product] nvarchar(500) NULL,
    [SubProduct] nvarchar(500) NULL,
    [BOM_Bucket] nvarchar(50) NULL,
    [CurrentBucket] nvarchar(50) NULL,
    [NPA_Flag] nvarchar(100) NULL,
    [Current_DPD] nvarchar(100) NULL,
    [Zone] nvarchar(100) NULL,
    [Region] nvarchar(100) NULL,
    [State] nvarchar(100) NULL,
    [City] nvarchar(100) NULL,
    [Branch] nvarchar(500) NULL,
    [Sequence] int NULL,
    [IsDisabled] bit NULL,
    [SegmentAdvanceFilterId] nvarchar(32) NULL,
    [ClusterName] nvarchar(max) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Segmentation] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Segmentation_SegmentationAdvanceFilter_SegmentAdvanceFilterId] FOREIGN KEY ([SegmentAdvanceFilterId]) REFERENCES [SegmentationAdvanceFilter] ([Id])
);
GO

CREATE TABLE [TFlexIdentificationDocTypes] (
    [Id] nvarchar(32) NOT NULL,
    [Description] nvarchar(50) NOT NULL,
    [TFlexIdentificationTypeId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TFlexIdentificationDocTypes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TFlexIdentificationDocTypes_TFlexIdentificationTypes_TFlexIdentificationTypeId] FOREIGN KEY ([TFlexIdentificationTypeId]) REFERENCES [TFlexIdentificationTypes] ([Id])
);
GO

CREATE TABLE [SubTreatment] (
    [Id] nvarchar(32) NOT NULL,
    [Order] int NULL,
    [TreatmentType] nvarchar(100) NULL,
    [AllocationType] nvarchar(100) NULL,
    [StartDay] nvarchar(20) NULL,
    [EndDay] nvarchar(20) NULL,
    [ScriptToPersueCustomer] nvarchar(max) NULL,
    [QualifyingCondition] nvarchar(20) NULL,
    [PreSubtreatmentOrder] int NULL,
    [QualifyingStatus] nvarchar(100) NULL,
    [TreatmentId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_SubTreatment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SubTreatment_Treatment_TreatmentId] FOREIGN KEY ([TreatmentId]) REFERENCES [Treatment] ([Id])
);
GO

CREATE TABLE [TreatmentHistory] (
    [Id] nvarchar(32) NOT NULL,
    [TreatmentId] nvarchar(32) NULL,
    [NoOfAccounts] nvarchar(50) NULL,
    [LatestStamping] nvarchar(10) NULL,
    [SubTreatmentOrder] int NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TreatmentHistory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TreatmentHistory_Treatment_TreatmentId] FOREIGN KEY ([TreatmentId]) REFERENCES [Treatment] ([Id])
);
GO

CREATE TABLE [UserVerificationCodes] (
    [Id] nvarchar(32) NOT NULL,
    [UserId] nvarchar(32) NULL,
    [ShortVerificationCode] nvarchar(50) NULL,
    [VerificationCode] nvarchar(4000) NULL,
    [UserVerificationCodeTypeId] nvarchar(32) NULL,
    [TransactionID] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UserVerificationCodes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserVerificationCodes_UserVerificationCodeTypes_UserVerificationCodeTypeId] FOREIGN KEY ([UserVerificationCodeTypeId]) REFERENCES [UserVerificationCodeTypes] ([Id])
);
GO

CREATE TABLE [CreditAccountDetails] (
    [Id] nvarchar(32) NOT NULL,
    [AccountHolderName] nvarchar(200) NULL,
    [BankAccountNo] nvarchar(50) NULL,
    [BankBranchId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_CreditAccountDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CreditAccountDetails_BankBranch_BankBranchId] FOREIGN KEY ([BankBranchId]) REFERENCES [BankBranch] ([Id])
);
GO

CREATE TABLE [State] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [NickName] nvarchar(50) NOT NULL,
    [PrimaryLanguage] nvarchar(100) NOT NULL,
    [SecondaryLanguage] nvarchar(100) NOT NULL,
    [RegionId] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_State] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_State_Regions_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [Regions] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Designation] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(50) NULL,
    [Acronym] nvarchar(50) NULL,
    [DesignationTypeId] nvarchar(32) NULL,
    [DepartmentId] nvarchar(32) NULL,
    [Level] nvarchar(10) NULL,
    [ReportsToDesignation] nvarchar(200) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Designation] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Designation_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Department] ([Id]),
    CONSTRAINT [FK_Designation_DesignationType_DesignationTypeId] FOREIGN KEY ([DesignationTypeId]) REFERENCES [DesignationType] ([Id])
);
GO

CREATE TABLE [DispositionValidationMaster] (
    [Id] nvarchar(32) NOT NULL,
    [DispositionCodeMasterId] nvarchar(32) NOT NULL,
    [validationFieldName] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_DispositionValidationMaster] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DispositionValidationMaster_DispositionCodeMaster_DispositionCodeMasterId] FOREIGN KEY ([DispositionCodeMasterId]) REFERENCES [DispositionCodeMaster] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ActionMaster] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(100) NULL,
    [HasAccess] bit NOT NULL,
    [SubMenuMasterID] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ActionMaster] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ActionMaster_SubMenuMaster_SubMenuMasterID] FOREIGN KEY ([SubMenuMasterID]) REFERENCES [SubMenuMaster] ([Id])
);
GO

CREATE TABLE [TreatmentAndSegmentMapping] (
    [Id] nvarchar(32) NOT NULL,
    [TreatmentId] nvarchar(32) NULL,
    [SegmentId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TreatmentAndSegmentMapping] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TreatmentAndSegmentMapping_Segmentation_SegmentId] FOREIGN KEY ([SegmentId]) REFERENCES [Segmentation] ([Id]),
    CONSTRAINT [FK_TreatmentAndSegmentMapping_Treatment_TreatmentId] FOREIGN KEY ([TreatmentId]) REFERENCES [Treatment] ([Id])
);
GO

CREATE TABLE [RoundRobinTreatment] (
    [Id] nvarchar(32) NOT NULL,
    [TreatmentId] nvarchar(32) NULL,
    [SubTreatmentId] nvarchar(32) NULL,
    [AllocationId] nvarchar(50) NULL,
    [AllocationName] nvarchar(100) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_RoundRobinTreatment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoundRobinTreatment_SubTreatment_SubTreatmentId] FOREIGN KEY ([SubTreatmentId]) REFERENCES [SubTreatment] ([Id]),
    CONSTRAINT [FK_RoundRobinTreatment_Treatment_TreatmentId] FOREIGN KEY ([TreatmentId]) REFERENCES [Treatment] ([Id])
);
GO

CREATE TABLE [TreatmentOnCommunication] (
    [Id] nvarchar(32) NOT NULL,
    [TreatmentId] nvarchar(32) NULL,
    [SubTreatmentId] nvarchar(32) NULL,
    [CommunicationType] nvarchar(30) NULL,
    [CommunicationTemplateId] nvarchar(32) NULL,
    [CommunicationMobileNumberType] nvarchar(200) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TreatmentOnCommunication] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TreatmentOnCommunication_CommunicationTemplate_CommunicationTemplateId] FOREIGN KEY ([CommunicationTemplateId]) REFERENCES [CommunicationTemplate] ([Id]),
    CONSTRAINT [FK_TreatmentOnCommunication_SubTreatment_SubTreatmentId] FOREIGN KEY ([SubTreatmentId]) REFERENCES [SubTreatment] ([Id]),
    CONSTRAINT [FK_TreatmentOnCommunication_Treatment_TreatmentId] FOREIGN KEY ([TreatmentId]) REFERENCES [Treatment] ([Id])
);
GO

CREATE TABLE [TreatmentOnPerformanceBand] (
    [Id] nvarchar(32) NOT NULL,
    [TreatmentId] nvarchar(32) NULL,
    [SubTreatmentId] nvarchar(32) NULL,
    [PerformanceBand] nvarchar(30) NULL,
    [CustomerPersona] nvarchar(200) NULL,
    [Percentage] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TreatmentOnPerformanceBand] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TreatmentOnPerformanceBand_SubTreatment_SubTreatmentId] FOREIGN KEY ([SubTreatmentId]) REFERENCES [SubTreatment] ([Id]),
    CONSTRAINT [FK_TreatmentOnPerformanceBand_Treatment_TreatmentId] FOREIGN KEY ([TreatmentId]) REFERENCES [Treatment] ([Id])
);
GO

CREATE TABLE [TreatmentOnUpdateTrail] (
    [Id] nvarchar(32) NOT NULL,
    [TreatmentId] nvarchar(32) NULL,
    [SubTreatmentId] nvarchar(32) NULL,
    [DispositionCodeGroup] nvarchar(100) NULL,
    [DispositionCode] nvarchar(100) NULL,
    [NextActionDate] datetime2 NULL,
    [PTPAmount] decimal(18,2) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TreatmentOnUpdateTrail] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TreatmentOnUpdateTrail_SubTreatment_SubTreatmentId] FOREIGN KEY ([SubTreatmentId]) REFERENCES [SubTreatment] ([Id]),
    CONSTRAINT [FK_TreatmentOnUpdateTrail_Treatment_TreatmentId] FOREIGN KEY ([TreatmentId]) REFERENCES [Treatment] ([Id])
);
GO

CREATE TABLE [TreatmentQualifyingStatus] (
    [Id] nvarchar(32) NOT NULL,
    [TreatmentId] nvarchar(32) NULL,
    [SubTreatmentId] nvarchar(32) NULL,
    [Status] nvarchar(200) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TreatmentQualifyingStatus] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TreatmentQualifyingStatus_SubTreatment_SubTreatmentId] FOREIGN KEY ([SubTreatmentId]) REFERENCES [SubTreatment] ([Id]),
    CONSTRAINT [FK_TreatmentQualifyingStatus_Treatment_TreatmentId] FOREIGN KEY ([TreatmentId]) REFERENCES [Treatment] ([Id])
);
GO

CREATE TABLE [Cities] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [NickName] nvarchar(50) NOT NULL,
    [StateId] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Cities] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Cities_State_StateId] FOREIGN KEY ([StateId]) REFERENCES [State] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TreatmentByRule] (
    [Id] nvarchar(32) NOT NULL,
    [TreatmentId] nvarchar(32) NULL,
    [SubTreatmentId] nvarchar(32) NULL,
    [DepartmentId] nvarchar(32) NULL,
    [DesignationId] nvarchar(32) NULL,
    [Rule] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TreatmentByRule] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TreatmentByRule_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Department] ([Id]),
    CONSTRAINT [FK_TreatmentByRule_Designation_DesignationId] FOREIGN KEY ([DesignationId]) REFERENCES [Designation] ([Id]),
    CONSTRAINT [FK_TreatmentByRule_SubTreatment_SubTreatmentId] FOREIGN KEY ([SubTreatmentId]) REFERENCES [SubTreatment] ([Id]),
    CONSTRAINT [FK_TreatmentByRule_Treatment_TreatmentId] FOREIGN KEY ([TreatmentId]) REFERENCES [Treatment] ([Id])
);
GO

CREATE TABLE [TreatmentDesignation] (
    [Id] nvarchar(32) NOT NULL,
    [TreatmentId] nvarchar(32) NULL,
    [DepartmentId] nvarchar(32) NULL,
    [DesignationId] nvarchar(32) NULL,
    [SubTreatmentId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TreatmentDesignation] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TreatmentDesignation_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Department] ([Id]),
    CONSTRAINT [FK_TreatmentDesignation_Designation_DesignationId] FOREIGN KEY ([DesignationId]) REFERENCES [Designation] ([Id]),
    CONSTRAINT [FK_TreatmentDesignation_SubTreatment_SubTreatmentId] FOREIGN KEY ([SubTreatmentId]) REFERENCES [SubTreatment] ([Id]),
    CONSTRAINT [FK_TreatmentDesignation_Treatment_TreatmentId] FOREIGN KEY ([TreatmentId]) REFERENCES [Treatment] ([Id])
);
GO

CREATE TABLE [TreatmentOnAccount] (
    [Id] nvarchar(32) NOT NULL,
    [TreatmentId] nvarchar(32) NULL,
    [SubTreatmentId] nvarchar(32) NULL,
    [DepartmentId] nvarchar(32) NULL,
    [DesignationId] nvarchar(32) NULL,
    [Percentage] nvarchar(50) NULL,
    [AllocationId] nvarchar(50) NULL,
    [AllocationName] nvarchar(100) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TreatmentOnAccount] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TreatmentOnAccount_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Department] ([Id]),
    CONSTRAINT [FK_TreatmentOnAccount_Designation_DesignationId] FOREIGN KEY ([DesignationId]) REFERENCES [Designation] ([Id]),
    CONSTRAINT [FK_TreatmentOnAccount_SubTreatment_SubTreatmentId] FOREIGN KEY ([SubTreatmentId]) REFERENCES [SubTreatment] ([Id]),
    CONSTRAINT [FK_TreatmentOnAccount_Treatment_TreatmentId] FOREIGN KEY ([TreatmentId]) REFERENCES [Treatment] ([Id])
);
GO

CREATE TABLE [TreatmentOnPOS] (
    [Id] nvarchar(32) NOT NULL,
    [TreatmentId] nvarchar(32) NULL,
    [SubTreatmentId] nvarchar(32) NULL,
    [DepartmentId] nvarchar(32) NULL,
    [DesignationId] nvarchar(32) NULL,
    [Percentage] nvarchar(50) NULL,
    [AllocationId] nvarchar(50) NULL,
    [AllocationName] nvarchar(100) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TreatmentOnPOS] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TreatmentOnPOS_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Department] ([Id]),
    CONSTRAINT [FK_TreatmentOnPOS_Designation_DesignationId] FOREIGN KEY ([DesignationId]) REFERENCES [Designation] ([Id]),
    CONSTRAINT [FK_TreatmentOnPOS_SubTreatment_SubTreatmentId] FOREIGN KEY ([SubTreatmentId]) REFERENCES [SubTreatment] ([Id]),
    CONSTRAINT [FK_TreatmentOnPOS_Treatment_TreatmentId] FOREIGN KEY ([TreatmentId]) REFERENCES [Treatment] ([Id])
);
GO

CREATE TABLE [AccessRights] (
    [Id] nvarchar(32) NOT NULL,
    [BusinessEntity] nvarchar(100) NOT NULL,
    [Action] nvarchar(50) NOT NULL,
    [AccountabilityTypeId] nvarchar(32) NOT NULL,
    [AccountabilityAs] nvarchar(5) NOT NULL,
    [Discriminator] nvarchar(21) NOT NULL,
    [MethodType] nvarchar(10) NULL,
    [Route] nvarchar(250) NULL,
    [MenuMasterId] nvarchar(32) NULL,
    [ActionMasterId] nvarchar(32) NULL,
    [SubMenuMasterId] nvarchar(32) NULL,
    [IsMobile] bit NULL,
    [IsFrontEnd] bit NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AccessRights] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AccessRights_AccountabilityTypes_AccountabilityTypeId] FOREIGN KEY ([AccountabilityTypeId]) REFERENCES [AccountabilityTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AccessRights_ActionMaster_ActionMasterId] FOREIGN KEY ([ActionMasterId]) REFERENCES [ActionMaster] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AccessRights_MenuMaster_MenuMasterId] FOREIGN KEY ([MenuMasterId]) REFERENCES [MenuMaster] ([Id]),
    CONSTRAINT [FK_AccessRights_SubMenuMaster_SubMenuMasterId] FOREIGN KEY ([SubMenuMasterId]) REFERENCES [SubMenuMaster] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AgencyIdentification] (
    [Id] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    [TFlexId] nvarchar(32) NOT NULL,
    [TFlexIdentificationTypeId] nvarchar(32) NOT NULL,
    [TFlexIdentificationDocTypeId] nvarchar(32) NOT NULL,
    [IsDeferred] bit NULL,
    [DeferredTillDate] datetime2 NULL,
    [IsWavedOff] bit NULL,
    [Value] nvarchar(500) NULL,
    [Status] nvarchar(50) NULL,
    [Remarks] nvarchar(200) NULL,
    CONSTRAINT [PK_AgencyIdentification] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AgencyIdentification_TFlexIdentificationDocTypes_TFlexIdentificationDocTypeId] FOREIGN KEY ([TFlexIdentificationDocTypeId]) REFERENCES [TFlexIdentificationDocTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AgencyIdentification_TFlexIdentificationTypes_TFlexIdentificationTypeId] FOREIGN KEY ([TFlexIdentificationTypeId]) REFERENCES [TFlexIdentificationTypes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AgencyIdentificationDoc] (
    [Id] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    [TFlexIdentificationId] nvarchar(32) NULL,
    [Path] nvarchar(500) NULL,
    [FileName] nvarchar(100) NULL,
    [FileSize] bigint NULL,
    CONSTRAINT [PK_AgencyIdentificationDoc] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AgencyIdentificationDoc_AgencyIdentification_TFlexIdentificationId] FOREIGN KEY ([TFlexIdentificationId]) REFERENCES [AgencyIdentification] ([Id])
);
GO

CREATE TABLE [AgencyPlaceOfWork] (
    [Id] nvarchar(32) NOT NULL,
    [Product] nvarchar(50) NULL,
    [ProductGroup] nvarchar(50) NULL,
    [SubProduct] nvarchar(50) NULL,
    [Bucket] nvarchar(50) NULL,
    [City] nvarchar(50) NULL,
    [Region] nvarchar(50) NULL,
    [State] nvarchar(50) NULL,
    [Zone] nvarchar(50) NULL,
    [Location] nvarchar(100) NULL,
    [ManagerId] nvarchar(32) NULL,
    [AgencyId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AgencyPlaceOfWork] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AgencyScopeOfWork] (
    [Id] nvarchar(32) NOT NULL,
    [Product] nvarchar(50) NULL,
    [ProductGroup] nvarchar(50) NULL,
    [SubProduct] nvarchar(50) NULL,
    [Bucket] nvarchar(50) NULL,
    [City] nvarchar(50) NULL,
    [Region] nvarchar(50) NULL,
    [State] nvarchar(50) NULL,
    [Zone] nvarchar(50) NULL,
    [ManagerId] nvarchar(32) NULL,
    [AgencyId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AgencyScopeOfWork] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AgencyUserDesignation] (
    [Id] nvarchar(32) NOT NULL,
    [AgencyUserId] nvarchar(32) NULL,
    [DepartmentId] nvarchar(32) NULL,
    [DesignationId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AgencyUserDesignation] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AgencyUserDesignation_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Department] ([Id]),
    CONSTRAINT [FK_AgencyUserDesignation_Designation_DesignationId] FOREIGN KEY ([DesignationId]) REFERENCES [Designation] ([Id])
);
GO

CREATE TABLE [AgencyUserIdentification] (
    [Id] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    [TFlexId] nvarchar(32) NOT NULL,
    [TFlexIdentificationTypeId] nvarchar(32) NOT NULL,
    [TFlexIdentificationDocTypeId] nvarchar(32) NOT NULL,
    [IsDeferred] bit NULL,
    [DeferredTillDate] datetime2 NULL,
    [IsWavedOff] bit NULL,
    [Value] nvarchar(500) NULL,
    [Status] nvarchar(50) NULL,
    [Remarks] nvarchar(200) NULL,
    CONSTRAINT [PK_AgencyUserIdentification] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AgencyUserIdentification_TFlexIdentificationDocTypes_TFlexIdentificationDocTypeId] FOREIGN KEY ([TFlexIdentificationDocTypeId]) REFERENCES [TFlexIdentificationDocTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AgencyUserIdentification_TFlexIdentificationTypes_TFlexIdentificationTypeId] FOREIGN KEY ([TFlexIdentificationTypeId]) REFERENCES [TFlexIdentificationTypes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AgencyUserIdentificationDoc] (
    [Id] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    [TFlexIdentificationId] nvarchar(32) NULL,
    [Path] nvarchar(500) NULL,
    [FileName] nvarchar(100) NULL,
    [FileSize] bigint NULL,
    CONSTRAINT [PK_AgencyUserIdentificationDoc] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AgencyUserIdentificationDoc_AgencyUserIdentification_TFlexIdentificationId] FOREIGN KEY ([TFlexIdentificationId]) REFERENCES [AgencyUserIdentification] ([Id])
);
GO

CREATE TABLE [AgencyUserPlaceOfWork] (
    [Id] nvarchar(32) NOT NULL,
    [PIN] nvarchar(50) NULL,
    [AgencyUserId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AgencyUserPlaceOfWork] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AgencyUserScopeOfWork] (
    [Id] nvarchar(32) NOT NULL,
    [Product] nvarchar(50) NULL,
    [ProductGroup] nvarchar(50) NULL,
    [SubProduct] nvarchar(50) NULL,
    [Bucket] nvarchar(50) NULL,
    [City] nvarchar(50) NULL,
    [Region] nvarchar(50) NULL,
    [State] nvarchar(50) NULL,
    [Zone] nvarchar(50) NULL,
    [Branch] nvarchar(50) NULL,
    [DepartmentId] nvarchar(32) NULL,
    [DesignationId] nvarchar(32) NULL,
    [AgencyUserId] nvarchar(32) NULL,
    [ManagerId] nvarchar(32) NULL,
    [Language] nvarchar(100) NULL,
    [Experience] bigint NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AgencyUserScopeOfWork] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AgencyUserScopeOfWork_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Department] ([Id]),
    CONSTRAINT [FK_AgencyUserScopeOfWork_Designation_DesignationId] FOREIGN KEY ([DesignationId]) REFERENCES [Designation] ([Id])
);
GO

CREATE TABLE [ApplicationOrg] (
    [Id] nvarchar(32) NOT NULL,
    [CustomId] nvarchar(100) NULL,
    [Discriminator] nvarchar(21) NOT NULL,
    [Remarks] nvarchar(500) NULL,
    [IsBlackListed] bit NULL,
    [BlackListingReason] nvarchar(200) NULL,
    [ContractExpireDate] datetime2 NULL,
    [LastRenewalDate] datetime2 NULL,
    [FirstAgreementDate] datetime2 NULL,
    [ServiceTaxRegistrationNumber] nvarchar(50) NULL,
    [DeactivationReason] nvarchar(200) NULL,
    [IsParentAgency] bit NULL,
    [PAN] nvarchar(50) NULL,
    [TIN] nvarchar(50) NULL,
    [GSTIN] nvarchar(50) NULL,
    [PrimaryOwnerFirstName] nvarchar(50) NULL,
    [PrimaryOwnerLastName] nvarchar(50) NULL,
    [PrimaryContactCountryCode] nvarchar(50) NULL,
    [PrimaryContactAreaCode] nvarchar(50) NULL,
    [SecondaryContactCountryCode] nvarchar(50) NULL,
    [SecondaryContactAreaCode] nvarchar(50) NULL,
    [YardAddress] nvarchar(100) NULL,
    [NumberOfYards] decimal(18,2) NULL,
    [YardSize] decimal(18,2) NULL,
    [DepartmentId] nvarchar(32) NULL,
    [DesignationId] nvarchar(32) NULL,
    [AgencyTypeId] nvarchar(32) NULL,
    [ParentAgencyId] nvarchar(32) NULL,
    [RecommendingOfficerId] nvarchar(32) NULL,
    [CreditAccountDetailsId] nvarchar(32) NULL,
    [AgencyCategoryId] nvarchar(32) NULL,
    [AddressId] nvarchar(32) NULL,
    [AgencyWorkflowStateId] nvarchar(32) NULL,
    [BankSolID] nvarchar(100) NULL,
    [NickName] nvarchar(50) NULL,
    [AddressLine2] nvarchar(max) NULL,
    [AddressLine1] nvarchar(max) NULL,
    [CityId] nvarchar(32) NULL,
    [Zone] nvarchar(50) NULL,
    [Region] nvarchar(50) NULL,
    [State] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    [UserId] nvarchar(200) NULL,
    [FirstName] nvarchar(200) NOT NULL,
    [MiddleName] nvarchar(50) NULL,
    [LastName] nvarchar(50) NULL,
    [PrimaryMobileNumber] nvarchar(50) NULL,
    [SecondaryContactNumber] nvarchar(50) NULL,
    [ProfileImage] nvarchar(500) NULL,
    [PrimaryEMail] nvarchar(200) NULL,
    [ActivationCode] nvarchar(256) NULL,
    [isOrganization] bit NOT NULL,
    [DateOfBirth] datetime2 NULL,
    CONSTRAINT [PK_ApplicationOrg] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApplicationOrg_Address_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Address] ([Id]),
    CONSTRAINT [FK_ApplicationOrg_AgencyCategory_AgencyCategoryId] FOREIGN KEY ([AgencyCategoryId]) REFERENCES [AgencyCategory] ([Id]),
    CONSTRAINT [FK_ApplicationOrg_AgencyType_AgencyTypeId] FOREIGN KEY ([AgencyTypeId]) REFERENCES [AgencyType] ([Id]),
    CONSTRAINT [FK_ApplicationOrg_AgencyWorkflowState_AgencyWorkflowStateId] FOREIGN KEY ([AgencyWorkflowStateId]) REFERENCES [AgencyWorkflowState] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ApplicationOrg_ApplicationOrg_ParentAgencyId] FOREIGN KEY ([ParentAgencyId]) REFERENCES [ApplicationOrg] ([Id]),
    CONSTRAINT [FK_ApplicationOrg_Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [Cities] ([Id]),
    CONSTRAINT [FK_ApplicationOrg_CreditAccountDetails_CreditAccountDetailsId] FOREIGN KEY ([CreditAccountDetailsId]) REFERENCES [CreditAccountDetails] ([Id]),
    CONSTRAINT [FK_ApplicationOrg_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Department] ([Id]),
    CONSTRAINT [FK_ApplicationOrg_Designation_DesignationId] FOREIGN KEY ([DesignationId]) REFERENCES [Designation] ([Id])
);
GO

CREATE TABLE [ApplicationUser] (
    [Id] nvarchar(32) NOT NULL,
    [CustomId] nvarchar(100) NULL,
    [DeactivationReason] nvarchar(max) NULL,
    [IsDeactivated] bit NOT NULL,
    [IsBlackListed] bit NOT NULL,
    [BlackListingReason] nvarchar(200) NULL,
    [RejectionReason] nvarchar(200) NULL,
    [Remarks] nvarchar(500) NULL,
    [ForgotPasswordMailSentCount] int NOT NULL,
    [ForgotPasswordSMSSentCount] int NOT NULL,
    [ForgotPasswordCount] int NOT NULL,
    [ForgotPasswordDateTime] datetime2 NULL,
    [UpdateMobileDateTime] datetime2 NULL,
    [UpdateMobileCount] int NOT NULL,
    [ProductGroup] nvarchar(100) NULL,
    [Product] nvarchar(100) NULL,
    [SubProduct] nvarchar(200) NULL,
    [MaxHotleadCount] nvarchar(50) NULL,
    [WorkOfficeLongitude] nvarchar(50) NULL,
    [WorkOfficeLattitude] nvarchar(50) NULL,
    [PrimaryContactCountryCode] nvarchar(50) NULL,
    [UserLoad] int NULL,
    [Experience] decimal(18,2) NULL,
    [UserCurrentLocationDetailsId] nvarchar(32) NULL,
    [CreditAccountDetailsId] nvarchar(32) NULL,
    [ApplicationUserDetailsId] nvarchar(32) NULL,
    [IsLocked] bit NOT NULL,
    [Discriminator] nvarchar(21) NOT NULL,
    [IdCardNumber] nvarchar(50) NULL,
    [EmploymentDate] datetime2 NULL,
    [AuthorizationCardExpiryDate] datetime2 NULL,
    [LastRenewalDate] datetime2 NULL,
    [SupervisorEmailId] nvarchar(50) NULL,
    [PrimaryContactAreaCode] nvarchar(50) NULL,
    [DiallerId] nvarchar(50) NULL,
    [IDType] nvarchar(50) NULL,
    [UDIDNumber] nvarchar(50) NULL,
    [FatherName] nvarchar(50) NULL,
    [DisableReason] nvarchar(200) NULL,
    [DeactivateReason] nvarchar(200) NULL,
    [IsPrinted] bit NULL,
    [yCoreBankingId] nvarchar(50) NULL,
    [DRACertificateDate] datetime2 NULL,
    [DRATrainingDate] datetime2 NULL,
    [DRAUniqueRegistrationNumber] nvarchar(50) NULL,
    [AddressId] nvarchar(32) NULL,
    [AgencyId] nvarchar(32) NULL,
    [AgencyUserWorkflowStateId] nvarchar(32) NULL,
    [IsFrontEndStaff] bit NULL,
    [DomainId] nvarchar(50) NULL,
    [EmployeeId] nvarchar(50) NULL,
    [CompanyUser_PrimaryContactAreaCode] nvarchar(50) NULL,
    [SinglePointReportingManagerId] nvarchar(32) NULL,
    [BaseBranchId] nvarchar(32) NULL,
    [CompanyId] nvarchar(32) NULL,
    [TrailCap] int NULL,
    [CompanyUserWorkflowStateId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    [UserId] nvarchar(200) NULL,
    [FirstName] nvarchar(200) NOT NULL,
    [MiddleName] nvarchar(50) NULL,
    [LastName] nvarchar(50) NULL,
    [PrimaryMobileNumber] nvarchar(50) NULL,
    [SecondaryContactNumber] nvarchar(50) NULL,
    [ProfileImage] nvarchar(500) NULL,
    [PrimaryEMail] nvarchar(200) NULL,
    [ActivationCode] nvarchar(256) NULL,
    [isOrganization] bit NOT NULL,
    [DateOfBirth] datetime2 NULL,
    CONSTRAINT [PK_ApplicationUser] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ApplicationUser_Address_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Address] ([Id]),
    CONSTRAINT [FK_ApplicationUser_AgencyUserWorkflowState_AgencyUserWorkflowStateId] FOREIGN KEY ([AgencyUserWorkflowStateId]) REFERENCES [AgencyUserWorkflowState] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ApplicationUser_ApplicationOrg_AgencyId] FOREIGN KEY ([AgencyId]) REFERENCES [ApplicationOrg] ([Id]),
    CONSTRAINT [FK_ApplicationUser_ApplicationOrg_BaseBranchId] FOREIGN KEY ([BaseBranchId]) REFERENCES [ApplicationOrg] ([Id]),
    CONSTRAINT [FK_ApplicationUser_ApplicationUserDetails_ApplicationUserDetailsId] FOREIGN KEY ([ApplicationUserDetailsId]) REFERENCES [ApplicationUserDetails] ([Id]),
    CONSTRAINT [FK_ApplicationUser_ApplicationUser_SinglePointReportingManagerId] FOREIGN KEY ([SinglePointReportingManagerId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_ApplicationUser_CompanyUserWorkflowState_CompanyUserWorkflowStateId] FOREIGN KEY ([CompanyUserWorkflowStateId]) REFERENCES [CompanyUserWorkflowState] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ApplicationUser_Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company] ([Id]),
    CONSTRAINT [FK_ApplicationUser_CreditAccountDetails_CreditAccountDetailsId] FOREIGN KEY ([CreditAccountDetailsId]) REFERENCES [CreditAccountDetails] ([Id]),
    CONSTRAINT [FK_ApplicationUser_UserCurrentLocationDetails_UserCurrentLocationDetailsId] FOREIGN KEY ([UserCurrentLocationDetailsId]) REFERENCES [UserCurrentLocationDetails] ([Id])
);
GO

CREATE TABLE [Areas] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [NickName] nvarchar(50) NOT NULL,
    [CityId] nvarchar(32) NOT NULL,
    [BaseBranchId] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Areas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Areas_ApplicationOrg_BaseBranchId] FOREIGN KEY ([BaseBranchId]) REFERENCES [ApplicationOrg] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Areas_Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [Cities] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [GeoMaster] (
    [Id] nvarchar(32) NOT NULL,
    [Country] nvarchar(max) NULL,
    [Zone] nvarchar(max) NULL,
    [Region] nvarchar(max) NULL,
    [State] nvarchar(max) NULL,
    [CITY] nvarchar(max) NULL,
    [Area] nvarchar(max) NULL,
    [BaseBranchId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_GeoMaster] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_GeoMaster_ApplicationOrg_BaseBranchId] FOREIGN KEY ([BaseBranchId]) REFERENCES [ApplicationOrg] ([Id])
);
GO

CREATE TABLE [CompanyUserARMScopeOfWork] (
    [Id] nvarchar(32) NOT NULL,
    [Product] nvarchar(50) NULL,
    [ProductGroup] nvarchar(50) NULL,
    [SubProduct] nvarchar(50) NULL,
    [Bucket] nvarchar(50) NULL,
    [City] nvarchar(50) NULL,
    [Region] nvarchar(50) NULL,
    [State] nvarchar(50) NULL,
    [Zone] nvarchar(50) NULL,
    [Branch] nvarchar(50) NULL,
    [SupervisingManagerId] nvarchar(32) NULL,
    [ReportingAgencyId] nvarchar(32) NULL,
    [Location] nvarchar(100) NULL,
    [CompanyUserId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_CompanyUserARMScopeOfWork] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CompanyUserARMScopeOfWork_ApplicationOrg_ReportingAgencyId] FOREIGN KEY ([ReportingAgencyId]) REFERENCES [ApplicationOrg] ([Id]),
    CONSTRAINT [FK_CompanyUserARMScopeOfWork_ApplicationUser_CompanyUserId] FOREIGN KEY ([CompanyUserId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_CompanyUserARMScopeOfWork_ApplicationUser_SupervisingManagerId] FOREIGN KEY ([SupervisingManagerId]) REFERENCES [ApplicationUser] ([Id])
);
GO

CREATE TABLE [CompanyUserDesignation] (
    [Id] nvarchar(32) NOT NULL,
    [CompanyUserId] nvarchar(32) NULL,
    [DepartmentId] nvarchar(32) NULL,
    [DesignationId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_CompanyUserDesignation] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CompanyUserDesignation_ApplicationUser_CompanyUserId] FOREIGN KEY ([CompanyUserId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_CompanyUserDesignation_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Department] ([Id]),
    CONSTRAINT [FK_CompanyUserDesignation_Designation_DesignationId] FOREIGN KEY ([DesignationId]) REFERENCES [Designation] ([Id])
);
GO

CREATE TABLE [CompanyUserPlaceOfWork] (
    [Id] nvarchar(32) NOT NULL,
    [PIN] nvarchar(50) NULL,
    [CompanyUserId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_CompanyUserPlaceOfWork] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CompanyUserPlaceOfWork_ApplicationUser_CompanyUserId] FOREIGN KEY ([CompanyUserId]) REFERENCES [ApplicationUser] ([Id])
);
GO

CREATE TABLE [CompanyUserScopeOfWork] (
    [Id] nvarchar(32) NOT NULL,
    [Product] nvarchar(50) NULL,
    [ProductGroup] nvarchar(50) NULL,
    [SubProduct] nvarchar(50) NULL,
    [Bucket] nvarchar(50) NULL,
    [City] nvarchar(50) NULL,
    [Region] nvarchar(50) NULL,
    [State] nvarchar(50) NULL,
    [Zone] nvarchar(50) NULL,
    [Branch] nvarchar(50) NULL,
    [Location] nvarchar(100) NULL,
    [SupervisingManagerId] nvarchar(32) NULL,
    [DepartmentId] nvarchar(32) NULL,
    [DesignationId] nvarchar(32) NULL,
    [CompanyUserId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_CompanyUserScopeOfWork] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CompanyUserScopeOfWork_ApplicationUser_CompanyUserId] FOREIGN KEY ([CompanyUserId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_CompanyUserScopeOfWork_ApplicationUser_SupervisingManagerId] FOREIGN KEY ([SupervisingManagerId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_CompanyUserScopeOfWork_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Department] ([Id]),
    CONSTRAINT [FK_CompanyUserScopeOfWork_Designation_DesignationId] FOREIGN KEY ([DesignationId]) REFERENCES [Designation] ([Id])
);
GO

CREATE TABLE [GeoTagDetails] (
    [Id] nvarchar(32) NOT NULL,
    [GeoTagReason] nvarchar(200) NULL,
    [Latitude] float NOT NULL,
    [Longitude] float NOT NULL,
    [GeoLocation] nvarchar(500) NULL,
    [ApplicationUserId] nvarchar(32) NULL,
    [TransactionType] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_GeoTagDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_GeoTagDetails_ApplicationUser_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser] ([Id])
);
GO

CREATE TABLE [Languages] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(50) NULL,
    [Description] nvarchar(200) NULL,
    [ApplicationUserId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Languages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Languages_ApplicationUser_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser] ([Id])
);
GO

CREATE TABLE [LoanAccounts] (
    [Id] nvarchar(32) NOT NULL,
    [CustomId] nvarchar(100) NULL,
    [AGREEMENTID] nvarchar(50) NULL,
    [BRANCH] nvarchar(100) NULL,
    [CUSTOMERID] nvarchar(50) NULL,
    [CUSTOMERNAME] nvarchar(100) NULL,
    [DispCode] nvarchar(100) NULL,
    [PRODUCT] nvarchar(100) NULL,
    [SubProduct] nvarchar(100) NULL,
    [ProductGroup] nvarchar(100) NULL,
    [PTPDate] datetime2 NULL,
    [Region] nvarchar(100) NULL,
    [LatestMobileNo] nvarchar(50) NULL,
    [LatestEmailId] nvarchar(50) NULL,
    [LatestLatitude] nvarchar(20) NULL,
    [LatestLongitude] nvarchar(20) NULL,
    [LatestPTPDate] datetime2 NULL,
    [LatestPTPAmount] decimal(12,2) NULL,
    [LatestPaymentDate] datetime2 NULL,
    [LatestFeedbackDate] datetime2 NULL,
    [LatestFeedbackId] nvarchar(32) NULL,
    [BranchCode] nvarchar(50) NULL,
    [ProductCode] nvarchar(50) NULL,
    [GroupId] nvarchar(50) NULL,
    [DueDate] nvarchar(100) NULL,
    [TreatmentId] nvarchar(32) NULL,
    [LenderId] nvarchar(32) NULL,
    [CustomerPersona] nvarchar(200) NULL,
    [IsDNDEnabled] bit NOT NULL,
    [BOM_POS] decimal(18,2) NULL,
    [BUCKET] bigint NULL,
    [CITY] nvarchar(100) NULL,
    [CURRENT_BUCKET] nvarchar(50) NULL,
    [AllocationOwnerExpiryDate] datetime2 NULL,
    [CURRENT_DPD] bigint NULL,
    [CURRENT_POS] decimal(18,2) NULL,
    [DISBURSEDAMOUNT] nvarchar(50) NULL,
    [EMI_OD_AMT] decimal(18,2) NULL,
    [EMI_START_DATE] nvarchar(50) NULL,
    [EMIAMT] decimal(18,2) NULL,
    [INTEREST_OD] decimal(18,2) NULL,
    [MAILINGMOBILE] nvarchar(50) NULL,
    [MAILINGZIPCODE] nvarchar(50) NULL,
    [MONTH] int NULL,
    [NO_OF_EMI_OD] nvarchar(50) NULL,
    [NPA_STAGEID] nvarchar(50) NULL,
    [PENAL_PENDING] decimal(18,2) NULL,
    [PRINCIPAL_OD] decimal(18,2) NULL,
    [REGDNUM] nvarchar(100) NULL,
    [STATE] nvarchar(100) NULL,
    [PAYMENTSTATUS] nvarchar(50) NULL,
    [YEAR] int NULL,
    [AgencyId] nvarchar(32) NULL,
    [CollectorId] nvarchar(32) NULL,
    [TOS] nvarchar(50) NULL,
    [TOTAL_ARREARS] nvarchar(50) NULL,
    [OVERDUE_DATE] nvarchar(50) NULL,
    [NEXT_DUE_DATE] nvarchar(50) NULL,
    [Excess] nvarchar(50) NULL,
    [LOAN_STATUS] nvarchar(100) NULL,
    [OTHER_CHARGES] nvarchar(100) NULL,
    [TOTAL_OUTSTANDING] decimal(18,2) NULL,
    [OVERDUE_DAYS] nvarchar(100) NULL,
    [DateOfBirth] datetime2 NULL,
    [TeleCallingAgencyId] nvarchar(32) NULL,
    [TeleCallerId] nvarchar(32) NULL,
    [AllocationOwnerId] nvarchar(32) NULL,
    [AgencyAllocationExpiryDate] datetime2 NULL,
    [TeleCallerAgencyAllocationExpiryDate] datetime2 NULL,
    [AgentAllocationExpiryDate] datetime2 NULL,
    [CollectorAllocationExpiryDate] datetime2 NULL,
    [TeleCallerAllocationExpiryDate] datetime2 NULL,
    [CO_APPLICANT1_NAME] nvarchar(100) NULL,
    [NEXT_DUE_AMOUNT] nvarchar(50) NULL,
    [Paid] int NULL,
    [Attempted] int NULL,
    [UnAttempted] int NULL,
    [Partner_Loan_ID] nvarchar(50) NULL,
    [IsEligibleForSettlement] bit NULL,
    [IsEligibleForRepossession] bit NULL,
    [IsEligibleForLegal] bit NULL,
    [IsEligibleForRestructure] bit NULL,
    [EMAIL_ID] nvarchar(50) NULL,
    [PAN_CARD_DETAILS] nvarchar(25) NULL,
    [SCHEME_DESC] nvarchar(80) NULL,
    [ZONE] nvarchar(80) NULL,
    [CentreID] nvarchar(50) NULL,
    [CentreName] nvarchar(50) NULL,
    [GroupName] nvarchar(50) NULL,
    [Area] nvarchar(200) NULL,
    [PRIMARY_CARD_NUMBER] nvarchar(25) NULL,
    [BILLING_CYCLE] nvarchar(2) NULL,
    [LAST_STATEMENT_DATE] datetime2 NULL,
    [CURRENT_MINIMUM_AMOUNT_DUE] decimal(16,2) NULL,
    [CURRENT_TOTAL_AMOUNT_DUE] decimal(16,2) NULL,
    [RESIDENTIAL_CUSTOMER_CITY] nvarchar(30) NULL,
    [RESIDENTIAL_CUSTOMER_STATE] nvarchar(30) NULL,
    [RESIDENTIAL_PIN_CODE] nvarchar(10) NULL,
    [RESIDENTIAL_COUNTRY] nvarchar(25) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_LoanAccounts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanAccounts_ApplicationOrg_AgencyId] FOREIGN KEY ([AgencyId]) REFERENCES [ApplicationOrg] ([Id]),
    CONSTRAINT [FK_LoanAccounts_ApplicationOrg_TeleCallingAgencyId] FOREIGN KEY ([TeleCallingAgencyId]) REFERENCES [ApplicationOrg] ([Id]),
    CONSTRAINT [FK_LoanAccounts_ApplicationUser_AllocationOwnerId] FOREIGN KEY ([AllocationOwnerId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_LoanAccounts_ApplicationUser_CollectorId] FOREIGN KEY ([CollectorId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_LoanAccounts_ApplicationUser_TeleCallerId] FOREIGN KEY ([TeleCallerId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_LoanAccounts_CategoryItem_LenderId] FOREIGN KEY ([LenderId]) REFERENCES [CategoryItem] ([Id]),
    CONSTRAINT [FK_LoanAccounts_Treatment_TreatmentId] FOREIGN KEY ([TreatmentId]) REFERENCES [Treatment] ([Id])
);
GO

CREATE TABLE [PayInSlips] (
    [Id] nvarchar(32) NOT NULL,
    [CustomId] nvarchar(100) NOT NULL,
    [CMSPayInSlipNo] nvarchar(100) NULL,
    [BankName] nvarchar(200) NULL,
    [BranchName] nvarchar(200) NULL,
    [DateOfDeposit] datetime2 NULL,
    [BankAccountNo] nvarchar(50) NULL,
    [AccountHolderName] nvarchar(100) NULL,
    [ModeOfPayment] nvarchar(100) NULL,
    [Amount] decimal(18,2) NOT NULL,
    [Currency] nvarchar(50) NULL,
    [IsPrintValid] bit NULL,
    [PrintedById] nvarchar(32) NULL,
    [PrintedDate] datetime2 NULL,
    [Lattitude] nvarchar(50) NULL,
    [Longitude] nvarchar(50) NULL,
    [PayInSlipImageName] nvarchar(250) NULL,
    [PayinslipType] nvarchar(max) NULL,
    [ProductGroup] nvarchar(max) NULL,
    [PayInSlipWorkflowStateId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_PayInSlips] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PayInSlips_ApplicationUser_PrintedById] FOREIGN KEY ([PrintedById]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_PayInSlips_PayInSlipWorkflowState_PayInSlipWorkflowStateId] FOREIGN KEY ([PayInSlipWorkflowStateId]) REFERENCES [PayInSlipWorkflowState] ([Id])
);
GO

CREATE TABLE [Receipts] (
    [Id] nvarchar(32) NOT NULL,
    [CollectorId] nvarchar(32) NOT NULL,
    [CustomId] nvarchar(max) NULL,
    [ReceiptWorkflowStateId] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Receipts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Receipts_ApplicationUser_CollectorId] FOREIGN KEY ([CollectorId]) REFERENCES [ApplicationUser] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Receipts_ReceiptWorkflowState_ReceiptWorkflowStateId] FOREIGN KEY ([ReceiptWorkflowStateId]) REFERENCES [ReceiptWorkflowState] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserAttendanceDetail] (
    [Id] nvarchar(32) NOT NULL,
    [TotalHours] float NOT NULL,
    [Date] datetime2 NOT NULL,
    [ApplicationUserId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UserAttendanceDetail] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserAttendanceDetail_ApplicationUser_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser] ([Id])
);
GO

CREATE TABLE [UserAttendanceLog] (
    [Id] nvarchar(32) NOT NULL,
    [ApplicationUserId] nvarchar(32) NULL,
    [SessionId] nvarchar(max) NULL,
    [IsSessionValid] bit NOT NULL,
    [LogOutLongitude] float NULL,
    [LogInLongitude] float NULL,
    [LogOutLatitude] float NULL,
    [LogInLatitude] float NULL,
    [LogInTime] datetime2 NULL,
    [LogOutTime] datetime2 NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UserAttendanceLog] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserAttendanceLog_ApplicationUser_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser] ([Id])
);
GO

CREATE TABLE [UserCustomerPersona] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(50) NULL,
    [ApplicationUserId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UserCustomerPersona] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserCustomerPersona_ApplicationUser_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser] ([Id])
);
GO

CREATE TABLE [UserPerformanceBand] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(50) NULL,
    [CompanyUserId] nvarchar(32) NULL,
    [AgencyUserId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UserPerformanceBand] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserPerformanceBand_ApplicationUser_AgencyUserId] FOREIGN KEY ([AgencyUserId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_UserPerformanceBand_ApplicationUser_CompanyUserId] FOREIGN KEY ([CompanyUserId]) REFERENCES [ApplicationUser] ([Id])
);
GO

CREATE TABLE [UserSearchCriteria] (
    [Id] nvarchar(32) NOT NULL,
    [isdisable] bit NULL,
    [FilterValues] nvarchar(max) NOT NULL,
    [filterName] nvarchar(max) NOT NULL,
    [UserId] nvarchar(32) NOT NULL,
    [UseCaseName] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UserSearchCriteria] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserSearchCriteria_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [ApplicationUser] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AreaBaseBranchMappings] (
    [Id] nvarchar(32) NOT NULL,
    [AreaId] nvarchar(32) NOT NULL,
    [BaseBranchId] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AreaBaseBranchMappings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AreaBaseBranchMappings_ApplicationOrg_BaseBranchId] FOREIGN KEY ([BaseBranchId]) REFERENCES [ApplicationOrg] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AreaBaseBranchMappings_Areas_AreaId] FOREIGN KEY ([AreaId]) REFERENCES [Areas] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
);
GO

CREATE TABLE [AreaPinCodeMappings] (
    [Id] nvarchar(32) NOT NULL,
    [AreaId] nvarchar(32) NULL,
    [PinCodeId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AreaPinCodeMappings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AreaPinCodeMappings_Areas_AreaId] FOREIGN KEY ([AreaId]) REFERENCES [Areas] ([Id]),
    CONSTRAINT [FK_AreaPinCodeMappings_PinCodes_PinCodeId] FOREIGN KEY ([PinCodeId]) REFERENCES [PinCodes] ([Id])
);
GO

CREATE TABLE [Feedback] (
    [Id] nvarchar(32) NOT NULL,
    [UploadedFileName] nvarchar(200) NULL,
    [CustomerMet] nvarchar(100) NULL,
    [DispositionCode] nvarchar(100) NULL,
    [DispositionGroup] nvarchar(200) NULL,
    [PTPDate] datetime2 NULL,
    [EscalateTo] nvarchar(200) NULL,
    [Remarks] nvarchar(1000) NULL,
    [FeedbackDate] datetime2 NULL,
    [IsReallocationRequest] nvarchar(20) NULL,
    [ReallocationRequestReason] nvarchar(500) NULL,
    [NewArea] nvarchar(200) NULL,
    [NewAddress] nvarchar(500) NULL,
    [City] nvarchar(200) NULL,
    [NewContactNo] nvarchar(50) NULL,
    [DispositionDate] datetime2 NULL,
    [RightPartyContact] nvarchar(50) NULL,
    [NextAction] nvarchar(50) NULL,
    [NonPaymentReason] nvarchar(100) NULL,
    [AssignReason] nvarchar(100) NULL,
    [NewContactCountryCode] nvarchar(50) NULL,
    [NewContactAreaCode] nvarchar(50) NULL,
    [State] nvarchar(50) NULL,
    [NewEmailId] nvarchar(50) NULL,
    [PickAddress] nvarchar(100) NULL,
    [OtherPickAddress] nvarchar(100) NULL,
    [Latitude] float NULL,
    [Longitude] float NULL,
    [OfflineFeedbackDate] datetime2 NULL,
    [CollectorId] nvarchar(32) NULL,
    [AssigneeId] nvarchar(32) NULL,
    [UserId] nvarchar(32) NULL,
    [GeoLocation] nvarchar(500) NULL,
    [AccountId] nvarchar(32) NOT NULL,
    [AssignTo] nvarchar(50) NOT NULL,
    [PTPAmount] decimal(18,2) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Feedback] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Feedback_ApplicationUser_AssigneeId] FOREIGN KEY ([AssigneeId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_Feedback_ApplicationUser_CollectorId] FOREIGN KEY ([CollectorId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_Feedback_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_Feedback_LoanAccounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [LoanAccounts] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [LoanAccountJSON] (
    [Id] nvarchar(32) NOT NULL,
    [AccountId] nvarchar(32) NULL,
    [AccountJSON] nvarchar(max) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_LoanAccountJSON] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LoanAccountJSON_LoanAccounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [LoanAccounts] ([Id])
);
GO

CREATE TABLE [PaymentTransactions] (
    [Id] nvarchar(32) NOT NULL,
    [PaymentGatewayID] nvarchar(32) NULL,
    [MerchantReferenceNumber] nvarchar(50) NULL,
    [MerchantTransactionId] nvarchar(50) NULL,
    [BankTransactionId] nvarchar(50) NULL,
    [BankReferenceNumber] nvarchar(50) NULL,
    [BankId] nvarchar(50) NULL,
    [Amount] decimal(18,2) NULL,
    [Currency] nvarchar(50) NULL,
    [TransactionDate] datetime2 NULL,
    [StatusCode] nvarchar(50) NULL,
    [Status] nvarchar(50) NULL,
    [ResponseMessage] nvarchar(max) NULL,
    [ErrorMessage] nvarchar(max) NULL,
    [ErrorCode] nvarchar(50) NULL,
    [IsPaid] bit NULL,
    [TransactionStatus] nvarchar(50) NULL,
    [RRN] nvarchar(50) NULL,
    [AuthCode] nvarchar(50) NULL,
    [CardNumber] nvarchar(50) NULL,
    [CardType] nvarchar(50) NULL,
    [CardHolderName] nvarchar(50) NULL,
    [LoanAccountId] nvarchar(32) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_PaymentTransactions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PaymentTransactions_LoanAccounts_LoanAccountId] FOREIGN KEY ([LoanAccountId]) REFERENCES [LoanAccounts] ([Id]),
    CONSTRAINT [FK_PaymentTransactions_PaymentGateways_PaymentGatewayID] FOREIGN KEY ([PaymentGatewayID]) REFERENCES [PaymentGateways] ([Id])
);
GO

CREATE TABLE [CollectionBatches] (
    [Id] nvarchar(32) NOT NULL,
    [CustomId] nvarchar(100) NULL,
    [Amount] decimal(18,2) NULL,
    [ProductGroup] nvarchar(100) NULL,
    [CurrencyId] nvarchar(5) NULL,
    [ModeOfPayment] nvarchar(max) NULL,
    [BankAccountNo] nvarchar(50) NULL,
    [BankName] nvarchar(50) NULL,
    [BranchName] nvarchar(50) NULL,
    [AccountHolderName] nvarchar(50) NULL,
    [Latitude] nvarchar(50) NULL,
    [Longitude] nvarchar(50) NULL,
    [CollectionBatchOrgId] nvarchar(32) NULL,
    [AcknowledgedById] nvarchar(32) NULL,
    [PayInSlipId] nvarchar(32) NULL,
    [CollectionBatchWorkflowStateId] nvarchar(32) NULL,
    [BatchType] nvarchar(max) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_CollectionBatches] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CollectionBatches_ApplicationOrg_CollectionBatchOrgId] FOREIGN KEY ([CollectionBatchOrgId]) REFERENCES [ApplicationOrg] ([Id]),
    CONSTRAINT [FK_CollectionBatches_ApplicationUser_AcknowledgedById] FOREIGN KEY ([AcknowledgedById]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_CollectionBatches_CollectionBatchWorkflowState_CollectionBatchWorkflowStateId] FOREIGN KEY ([CollectionBatchWorkflowStateId]) REFERENCES [CollectionBatchWorkflowState] ([Id]),
    CONSTRAINT [FK_CollectionBatches_PayInSlips_PayInSlipId] FOREIGN KEY ([PayInSlipId]) REFERENCES [PayInSlips] ([Id])
);
GO

CREATE TABLE [TreatmentOnCommunicationHistoryDetails] (
    [Id] nvarchar(32) NOT NULL,
    [TreatmentHistoryId] nvarchar(32) NULL,
    [LoanAccountId] nvarchar(32) NULL,
    [CustomId] nvarchar(50) NULL,
    [ReasonForFailure] nvarchar(800) NULL,
    [ReturnDate] datetime2 NULL,
    [ReasonForReturn] nvarchar(max) NULL,
    [DispatchID] nvarchar(200) NULL,
    [DispatchDate] datetime2 NULL,
    [DeliveryDate] datetime2 NULL,
    [Status] nvarchar(100) NULL,
    [MessageContent] nvarchar(max) NULL,
    [WAapiResponse] nvarchar(2000) NULL,
    [WADeliveredStatus] nvarchar(100) NULL,
    [WADeliveredResponse] nvarchar(800) NULL,
    [FileName] nvarchar(800) NULL,
    [SubTreatmentId] nvarchar(32) NULL,
    [IsRead] bit NOT NULL,
    [ReadDate] datetime2 NULL,
    [IsDelivered] bit NOT NULL,
    [SMSContentCreated] nvarchar(max) NULL,
    [SMSContentCreatedTimeStamp] datetime2 NULL,
    [SMSContentRequest] nvarchar(max) NULL,
    [SMSContentRequestTimeStamp] datetime2 NULL,
    [SMSResponse] nvarchar(max) NULL,
    [SMSResponseTimeStamp] datetime2 NULL,
    [SMSResponseStatus] nvarchar(50) NULL,
    [EmailContentCreated] nvarchar(max) NULL,
    [EmailContentCreatedTimeStamp] datetime2 NULL,
    [EmailContentRequest] nvarchar(max) NULL,
    [EmailContentRequestTimeStamp] datetime2 NULL,
    [EmailResponse] nvarchar(max) NULL,
    [EmailResponseTimeStamp] datetime2 NULL,
    [EmailResponseStatus] nvarchar(50) NULL,
    [WAContentCreated] nvarchar(max) NULL,
    [WAContentCreatedTimeStamp] datetime2 NULL,
    [WAContentRequest] nvarchar(max) NULL,
    [WAContentRequestTimeStamp] datetime2 NULL,
    [WAResponse] nvarchar(max) NULL,
    [WAResponseTimeStamp] datetime2 NULL,
    [WAResponseStatus] nvarchar(50) NULL,
    [CommunicationTemplateId] nvarchar(32) NULL,
    [PaymentTransactionId] nvarchar(32) NULL,
    [SMS_Aggregator_TransactionID] nvarchar(200) NULL,
    [WA_Aggregator_TransactionID] nvarchar(200) NULL,
    [Recipient_Operator] nvarchar(200) NULL,
    [Recipient_Circle] nvarchar(200) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TreatmentOnCommunicationHistoryDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TreatmentOnCommunicationHistoryDetails_CommunicationTemplate_CommunicationTemplateId] FOREIGN KEY ([CommunicationTemplateId]) REFERENCES [CommunicationTemplate] ([Id]),
    CONSTRAINT [FK_TreatmentOnCommunicationHistoryDetails_LoanAccounts_LoanAccountId] FOREIGN KEY ([LoanAccountId]) REFERENCES [LoanAccounts] ([Id]),
    CONSTRAINT [FK_TreatmentOnCommunicationHistoryDetails_PaymentTransactions_PaymentTransactionId] FOREIGN KEY ([PaymentTransactionId]) REFERENCES [PaymentTransactions] ([Id]),
    CONSTRAINT [FK_TreatmentOnCommunicationHistoryDetails_SubTreatment_SubTreatmentId] FOREIGN KEY ([SubTreatmentId]) REFERENCES [SubTreatment] ([Id]),
    CONSTRAINT [FK_TreatmentOnCommunicationHistoryDetails_TreatmentHistory_TreatmentHistoryId] FOREIGN KEY ([TreatmentHistoryId]) REFERENCES [TreatmentHistory] ([Id])
);
GO

CREATE TABLE [Collections] (
    [Id] nvarchar(32) NOT NULL,
    [CustomId] nvarchar(100) NULL,
    [Amount] decimal(18,2) NULL,
    [CurrencyId] nvarchar(5) NULL,
    [CollectionDate] datetime2 NULL,
    [RecordNo] nvarchar(50) NULL,
    [CollectionMode] nvarchar(50) NULL,
    [MobileNo] nvarchar(50) NULL,
    [ContactType] nvarchar(20) NULL,
    [CountryCode] nvarchar(20) NULL,
    [AreaCode] nvarchar(20) NULL,
    [EMailId] nvarchar(200) NULL,
    [PayerImageName] nvarchar(200) NULL,
    [CustomerName] nvarchar(200) NULL,
    [ChangeRequestImageName] nvarchar(200) NULL,
    [PhysicalReceiptNumber] nvarchar(50) NULL,
    [Latitude] nvarchar(50) NULL,
    [Longitude] nvarchar(50) NULL,
    [Remarks] nvarchar(500) NULL,
    [AccountId] nvarchar(32) NULL,
    [CollectorId] nvarchar(32) NULL,
    [AckingAgentId] nvarchar(32) NULL,
    [ReceiptId] nvarchar(32) NULL,
    [CollectionOrgId] nvarchar(32) NULL,
    [CollectionBatchId] nvarchar(32) NULL,
    [CashId] nvarchar(32) NULL,
    [ChequeId] nvarchar(32) NULL,
    [MailSentCount] int NOT NULL,
    [SMSSentCount] int NOT NULL,
    [TransactionNumber] nvarchar(32) NULL,
    [AcknowledgedDate] datetime2 NULL,
    [CollectionWorkflowStateId] nvarchar(32) NULL,
    [CancelledCollectionId] nvarchar(32) NULL,
    [CancellationRemarks] nvarchar(500) NULL,
    [OfflineCollectionDate] datetime2 NULL,
    [GeoLocation] nvarchar(500) NULL,
    [EncredibleUserId] nvarchar(500) NULL,
    [yForeClosureAmount] nvarchar(50) NULL,
    [yOverdueAmount] nvarchar(50) NULL,
    [yBounceCharges] nvarchar(50) NULL,
    [othercharges] nvarchar(50) NULL,
    [yPenalInterest] nvarchar(50) NULL,
    [Settlement] nvarchar(50) NULL,
    [yRelationshipWithCustomer] nvarchar(100) NULL,
    [yPANNo] nvarchar(50) NULL,
    [yUploadSource] nvarchar(50) NULL,
    [yBatchUploadID] nvarchar(50) NULL,
    [yTest] nvarchar(max) NULL,
    [DepositAccountNumber] nvarchar(50) NULL,
    [DepositBankName] nvarchar(50) NULL,
    [DepositBankBranch] nvarchar(50) NULL,
    [IsPoolAccount] bit NULL,
    [IsDepositAccount] bit NULL,
    [ReceiptType] nvarchar(max) NULL,
    [IsNewPhonenumber] bit NULL,
    [ErrorMessgae] nvarchar(max) NULL,
    [amountBreakUp1] decimal(16,2) NULL,
    [amountBreakUp2] decimal(16,2) NULL,
    [amountBreakUp3] decimal(16,2) NULL,
    [amountBreakUp4] decimal(16,2) NULL,
    [amountBreakUp5] decimal(16,2) NULL,
    [amountBreakUp6] decimal(16,2) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Collections] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Collections_ApplicationOrg_CollectionOrgId] FOREIGN KEY ([CollectionOrgId]) REFERENCES [ApplicationOrg] ([Id]),
    CONSTRAINT [FK_Collections_ApplicationUser_AckingAgentId] FOREIGN KEY ([AckingAgentId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_Collections_ApplicationUser_CollectorId] FOREIGN KEY ([CollectorId]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_Collections_Cash_CashId] FOREIGN KEY ([CashId]) REFERENCES [Cash] ([Id]),
    CONSTRAINT [FK_Collections_Cheques_ChequeId] FOREIGN KEY ([ChequeId]) REFERENCES [Cheques] ([Id]),
    CONSTRAINT [FK_Collections_CollectionBatches_CollectionBatchId] FOREIGN KEY ([CollectionBatchId]) REFERENCES [CollectionBatches] ([Id]),
    CONSTRAINT [FK_Collections_CollectionWorkflowState_CollectionWorkflowStateId] FOREIGN KEY ([CollectionWorkflowStateId]) REFERENCES [CollectionWorkflowState] ([Id]),
    CONSTRAINT [FK_Collections_Collections_CancelledCollectionId] FOREIGN KEY ([CancelledCollectionId]) REFERENCES [Collections] ([Id]),
    CONSTRAINT [FK_Collections_LoanAccounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [LoanAccounts] ([Id]),
    CONSTRAINT [FK_Collections_Receipts_ReceiptId] FOREIGN KEY ([ReceiptId]) REFERENCES [Receipts] ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedBy', N'CreatedDate', N'IsDeleted', N'LastModifiedBy', N'LastModifiedDate', N'MainType', N'SubType') AND [object_id] = OBJECT_ID(N'[AgencyType]'))
    SET IDENTITY_INSERT [AgencyType] ON;
INSERT INTO [AgencyType] ([Id], [CreatedBy], [CreatedDate], [IsDeleted], [LastModifiedBy], [LastModifiedDate], [MainType], [SubType])
VALUES (N'27d4c2e0ce1a438cb44cd7fb8ed552b9', NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', N'Collections', N'Tele calling'),
(N'ff379ce22f7b4aca9e74d0dadccb3739', NULL, '0001-01-01T00:00:00.0000000+00:00', CAST(0 AS bit), NULL, '0001-01-01T00:00:00.0000000+00:00', N'Collections', N'Field Agents');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedBy', N'CreatedDate', N'IsDeleted', N'LastModifiedBy', N'LastModifiedDate', N'MainType', N'SubType') AND [object_id] = OBJECT_ID(N'[AgencyType]'))
    SET IDENTITY_INSERT [AgencyType] OFF;
GO

CREATE INDEX [IX_AccessRights_AccountabilityTypeId] ON [AccessRights] ([AccountabilityTypeId]);
GO

CREATE INDEX [IX_AccessRights_ActionMasterId] ON [AccessRights] ([ActionMasterId]);
GO

CREATE INDEX [IX_AccessRights_MenuMasterId] ON [AccessRights] ([MenuMasterId]);
GO

CREATE INDEX [IX_AccessRights_SubMenuMasterId] ON [AccessRights] ([SubMenuMasterId]);
GO

CREATE INDEX [IX_Accountabilities_AccountabilityTypeId] ON [Accountabilities] ([AccountabilityTypeId]);
GO

CREATE UNIQUE INDEX [IX_Accountabilities_CommisionerId_ResponsibleId_AccountabilityTypeId] ON [Accountabilities] ([CommisionerId], [ResponsibleId], [AccountabilityTypeId]);
GO

CREATE INDEX [IX_ActionMaster_SubMenuMasterID] ON [ActionMaster] ([SubMenuMasterID]);
GO

CREATE INDEX [IX_AgencyIdentification_TFlexId] ON [AgencyIdentification] ([TFlexId]);
GO

CREATE INDEX [IX_AgencyIdentification_TFlexIdentificationDocTypeId] ON [AgencyIdentification] ([TFlexIdentificationDocTypeId]);
GO

CREATE INDEX [IX_AgencyIdentification_TFlexIdentificationTypeId] ON [AgencyIdentification] ([TFlexIdentificationTypeId]);
GO

CREATE INDEX [IX_AgencyIdentificationDoc_TFlexIdentificationId] ON [AgencyIdentificationDoc] ([TFlexIdentificationId]);
GO

CREATE INDEX [IX_AgencyPlaceOfWork_AgencyId] ON [AgencyPlaceOfWork] ([AgencyId]);
GO

CREATE INDEX [IX_AgencyPlaceOfWork_ManagerId] ON [AgencyPlaceOfWork] ([ManagerId]);
GO

CREATE INDEX [IX_AgencyScopeOfWork_AgencyId] ON [AgencyScopeOfWork] ([AgencyId]);
GO

CREATE INDEX [IX_AgencyUserDesignation_AgencyUserId] ON [AgencyUserDesignation] ([AgencyUserId]);
GO

CREATE INDEX [IX_AgencyUserDesignation_DepartmentId] ON [AgencyUserDesignation] ([DepartmentId]);
GO

CREATE INDEX [IX_AgencyUserDesignation_DesignationId] ON [AgencyUserDesignation] ([DesignationId]);
GO

CREATE INDEX [IX_AgencyUserIdentification_TFlexId] ON [AgencyUserIdentification] ([TFlexId]);
GO

CREATE INDEX [IX_AgencyUserIdentification_TFlexIdentificationDocTypeId] ON [AgencyUserIdentification] ([TFlexIdentificationDocTypeId]);
GO

CREATE INDEX [IX_AgencyUserIdentification_TFlexIdentificationTypeId] ON [AgencyUserIdentification] ([TFlexIdentificationTypeId]);
GO

CREATE INDEX [IX_AgencyUserIdentificationDoc_TFlexIdentificationId] ON [AgencyUserIdentificationDoc] ([TFlexIdentificationId]);
GO

CREATE INDEX [IX_AgencyUserPlaceOfWork_AgencyUserId] ON [AgencyUserPlaceOfWork] ([AgencyUserId]);
GO

CREATE INDEX [IX_AgencyUserScopeOfWork_AgencyUserId] ON [AgencyUserScopeOfWork] ([AgencyUserId]);
GO

CREATE INDEX [IX_AgencyUserScopeOfWork_DepartmentId] ON [AgencyUserScopeOfWork] ([DepartmentId]);
GO

CREATE INDEX [IX_AgencyUserScopeOfWork_DesignationId] ON [AgencyUserScopeOfWork] ([DesignationId]);
GO

CREATE INDEX [IX_ApplicationOrg_AddressId] ON [ApplicationOrg] ([AddressId]);
GO

CREATE INDEX [IX_ApplicationOrg_AgencyCategoryId] ON [ApplicationOrg] ([AgencyCategoryId]);
GO

CREATE INDEX [IX_ApplicationOrg_AgencyTypeId] ON [ApplicationOrg] ([AgencyTypeId]);
GO

CREATE INDEX [IX_ApplicationOrg_AgencyWorkflowStateId] ON [ApplicationOrg] ([AgencyWorkflowStateId]);
GO

CREATE INDEX [IX_ApplicationOrg_CityId] ON [ApplicationOrg] ([CityId]);
GO

CREATE INDEX [IX_ApplicationOrg_CreditAccountDetailsId] ON [ApplicationOrg] ([CreditAccountDetailsId]);
GO

CREATE INDEX [IX_ApplicationOrg_DepartmentId] ON [ApplicationOrg] ([DepartmentId]);
GO

CREATE INDEX [IX_ApplicationOrg_DesignationId] ON [ApplicationOrg] ([DesignationId]);
GO

CREATE INDEX [IX_ApplicationOrg_ParentAgencyId] ON [ApplicationOrg] ([ParentAgencyId]);
GO

CREATE INDEX [IX_ApplicationOrg_RecommendingOfficerId] ON [ApplicationOrg] ([RecommendingOfficerId]);
GO

CREATE INDEX [IX_ApplicationUser_AddressId] ON [ApplicationUser] ([AddressId]);
GO

CREATE INDEX [IX_ApplicationUser_AgencyId] ON [ApplicationUser] ([AgencyId]);
GO

CREATE INDEX [IX_ApplicationUser_AgencyUserWorkflowStateId] ON [ApplicationUser] ([AgencyUserWorkflowStateId]);
GO

CREATE INDEX [IX_ApplicationUser_ApplicationUserDetailsId] ON [ApplicationUser] ([ApplicationUserDetailsId]);
GO

CREATE INDEX [IX_ApplicationUser_BaseBranchId] ON [ApplicationUser] ([BaseBranchId]);
GO

CREATE INDEX [IX_ApplicationUser_CompanyId] ON [ApplicationUser] ([CompanyId]);
GO

CREATE INDEX [IX_ApplicationUser_CompanyUserWorkflowStateId] ON [ApplicationUser] ([CompanyUserWorkflowStateId]);
GO

CREATE INDEX [IX_ApplicationUser_CreditAccountDetailsId] ON [ApplicationUser] ([CreditAccountDetailsId]);
GO

CREATE INDEX [IX_ApplicationUser_SinglePointReportingManagerId] ON [ApplicationUser] ([SinglePointReportingManagerId]);
GO

CREATE INDEX [IX_ApplicationUser_UserCurrentLocationDetailsId] ON [ApplicationUser] ([UserCurrentLocationDetailsId]);
GO

CREATE INDEX [IX_ApplicationUserDetails_AddressId] ON [ApplicationUserDetails] ([AddressId]);
GO

CREATE INDEX [IX_AreaBaseBranchMappings_AreaId] ON [AreaBaseBranchMappings] ([AreaId]);
GO

CREATE INDEX [IX_AreaBaseBranchMappings_BaseBranchId] ON [AreaBaseBranchMappings] ([BaseBranchId]);
GO

CREATE INDEX [IX_AreaPinCodeMappings_AreaId] ON [AreaPinCodeMappings] ([AreaId]);
GO

CREATE INDEX [IX_AreaPinCodeMappings_PinCodeId] ON [AreaPinCodeMappings] ([PinCodeId]);
GO

CREATE INDEX [IX_Areas_BaseBranchId] ON [Areas] ([BaseBranchId]);
GO

CREATE INDEX [IX_Areas_CityId] ON [Areas] ([CityId]);
GO

CREATE INDEX [IX_BankBranch_BankId] ON [BankBranch] ([BankId]);
GO

CREATE INDEX [IX_CategoryItem_CategoryMasterId] ON [CategoryItem] ([CategoryMasterId]);
GO

CREATE INDEX [IX_CategoryItem_ParentId] ON [CategoryItem] ([ParentId]);
GO

CREATE INDEX [IX_Cities_StateId] ON [Cities] ([StateId]);
GO

CREATE INDEX [IX_CollectionBatches_AcknowledgedById] ON [CollectionBatches] ([AcknowledgedById]);
GO

CREATE INDEX [IX_CollectionBatches_CollectionBatchOrgId] ON [CollectionBatches] ([CollectionBatchOrgId]);
GO

CREATE INDEX [IX_CollectionBatches_CollectionBatchWorkflowStateId] ON [CollectionBatches] ([CollectionBatchWorkflowStateId]);
GO

CREATE INDEX [IX_CollectionBatches_PayInSlipId] ON [CollectionBatches] ([PayInSlipId]);
GO

CREATE INDEX [IX_Collections_AccountId] ON [Collections] ([AccountId]);
GO

CREATE INDEX [IX_Collections_AckingAgentId] ON [Collections] ([AckingAgentId]);
GO

CREATE INDEX [IX_Collections_CancelledCollectionId] ON [Collections] ([CancelledCollectionId]);
GO

CREATE INDEX [IX_Collections_CashId] ON [Collections] ([CashId]);
GO

CREATE INDEX [IX_Collections_ChequeId] ON [Collections] ([ChequeId]);
GO

CREATE INDEX [IX_Collections_CollectionBatchId] ON [Collections] ([CollectionBatchId]);
GO

CREATE INDEX [IX_Collections_CollectionOrgId] ON [Collections] ([CollectionOrgId]);
GO

CREATE INDEX [IX_Collections_CollectionWorkflowStateId] ON [Collections] ([CollectionWorkflowStateId]);
GO

CREATE INDEX [IX_Collections_CollectorId] ON [Collections] ([CollectorId]);
GO

CREATE INDEX [IX_Collections_ReceiptId] ON [Collections] ([ReceiptId]);
GO

CREATE INDEX [IX_CommunicationTemplate_CommunicationTemplateDetailId] ON [CommunicationTemplate] ([CommunicationTemplateDetailId]);
GO

CREATE INDEX [IX_CommunicationTemplate_CommunicationTemplateWorkflowStateId] ON [CommunicationTemplate] ([CommunicationTemplateWorkflowStateId]);
GO

CREATE INDEX [IX_CompanyUserARMScopeOfWork_CompanyUserId] ON [CompanyUserARMScopeOfWork] ([CompanyUserId]);
GO

CREATE INDEX [IX_CompanyUserARMScopeOfWork_ReportingAgencyId] ON [CompanyUserARMScopeOfWork] ([ReportingAgencyId]);
GO

CREATE INDEX [IX_CompanyUserARMScopeOfWork_SupervisingManagerId] ON [CompanyUserARMScopeOfWork] ([SupervisingManagerId]);
GO

CREATE INDEX [IX_CompanyUserDesignation_CompanyUserId] ON [CompanyUserDesignation] ([CompanyUserId]);
GO

CREATE INDEX [IX_CompanyUserDesignation_DepartmentId] ON [CompanyUserDesignation] ([DepartmentId]);
GO

CREATE INDEX [IX_CompanyUserDesignation_DesignationId] ON [CompanyUserDesignation] ([DesignationId]);
GO

CREATE INDEX [IX_CompanyUserPlaceOfWork_CompanyUserId] ON [CompanyUserPlaceOfWork] ([CompanyUserId]);
GO

CREATE INDEX [IX_CompanyUserScopeOfWork_CompanyUserId] ON [CompanyUserScopeOfWork] ([CompanyUserId]);
GO

CREATE INDEX [IX_CompanyUserScopeOfWork_DepartmentId] ON [CompanyUserScopeOfWork] ([DepartmentId]);
GO

CREATE INDEX [IX_CompanyUserScopeOfWork_DesignationId] ON [CompanyUserScopeOfWork] ([DesignationId]);
GO

CREATE INDEX [IX_CompanyUserScopeOfWork_SupervisingManagerId] ON [CompanyUserScopeOfWork] ([SupervisingManagerId]);
GO

CREATE INDEX [IX_CreditAccountDetails_BankBranchId] ON [CreditAccountDetails] ([BankBranchId]);
GO

CREATE INDEX [IX_Department_DepartmentTypeId] ON [Department] ([DepartmentTypeId]);
GO

CREATE INDEX [IX_Designation_DepartmentId] ON [Designation] ([DepartmentId]);
GO

CREATE INDEX [IX_Designation_DesignationTypeId] ON [Designation] ([DesignationTypeId]);
GO

CREATE INDEX [IX_DispositionCodeMaster_DispositionGroupMasterId] ON [DispositionCodeMaster] ([DispositionGroupMasterId]);
GO

CREATE INDEX [IX_DispositionValidationMaster_DispositionCodeMasterId] ON [DispositionValidationMaster] ([DispositionCodeMasterId]);
GO

CREATE INDEX [IX_Feedback_AccountId] ON [Feedback] ([AccountId]);
GO

CREATE INDEX [IX_Feedback_AssigneeId] ON [Feedback] ([AssigneeId]);
GO

CREATE INDEX [IX_Feedback_CollectorId] ON [Feedback] ([CollectorId]);
GO

CREATE INDEX [IX_Feedback_UserId] ON [Feedback] ([UserId]);
GO

CREATE INDEX [IX_FlexDynamicBusinessRuleSequences_FlexBusinessContextId] ON [FlexDynamicBusinessRuleSequences] ([FlexBusinessContextId]);
GO

CREATE INDEX [IX_GeoMaster_BaseBranchId] ON [GeoMaster] ([BaseBranchId]);
GO

CREATE INDEX [IX_GeoTagDetails_ApplicationUserId] ON [GeoTagDetails] ([ApplicationUserId]);
GO

CREATE INDEX [IX_Languages_ApplicationUserId] ON [Languages] ([ApplicationUserId]);
GO

CREATE INDEX [IX_LoanAccountJSON_AccountId] ON [LoanAccountJSON] ([AccountId]);
GO

CREATE INDEX [IX_LoanAccounts_AgencyId] ON [LoanAccounts] ([AgencyId]);
GO

CREATE INDEX [IX_LoanAccounts_AllocationOwnerId] ON [LoanAccounts] ([AllocationOwnerId]);
GO

CREATE INDEX [IX_LoanAccounts_CollectorId] ON [LoanAccounts] ([CollectorId]);
GO

CREATE INDEX [IX_LoanAccounts_LenderId] ON [LoanAccounts] ([LenderId]);
GO

CREATE INDEX [IX_LoanAccounts_TeleCallerId] ON [LoanAccounts] ([TeleCallerId]);
GO

CREATE INDEX [IX_LoanAccounts_TeleCallingAgencyId] ON [LoanAccounts] ([TeleCallingAgencyId]);
GO

CREATE INDEX [IX_LoanAccounts_TreatmentId] ON [LoanAccounts] ([TreatmentId]);
GO

CREATE INDEX [IX_MultilingualEntity_CultureId] ON [MultilingualEntity] ([CultureId]);
GO

CREATE INDEX [IX_MultilingualEntity_MultilingualEntitySetId] ON [MultilingualEntity] ([MultilingualEntitySetId]);
GO

CREATE INDEX [IX_PayInSlips_PayInSlipWorkflowStateId] ON [PayInSlips] ([PayInSlipWorkflowStateId]);
GO

CREATE INDEX [IX_PayInSlips_PrintedById] ON [PayInSlips] ([PrintedById]);
GO

CREATE INDEX [IX_PaymentTransactions_LoanAccountId] ON [PaymentTransactions] ([LoanAccountId]);
GO

CREATE INDEX [IX_PaymentTransactions_PaymentGatewayID] ON [PaymentTransactions] ([PaymentGatewayID]);
GO

CREATE INDEX [IX_Receipts_CollectorId] ON [Receipts] ([CollectorId]);
GO

CREATE INDEX [IX_Receipts_ReceiptWorkflowStateId] ON [Receipts] ([ReceiptWorkflowStateId]);
GO

CREATE INDEX [IX_Regions_CountryId] ON [Regions] ([CountryId]);
GO

CREATE INDEX [IX_RoundRobinTreatment_SubTreatmentId] ON [RoundRobinTreatment] ([SubTreatmentId]);
GO

CREATE INDEX [IX_RoundRobinTreatment_TreatmentId] ON [RoundRobinTreatment] ([TreatmentId]);
GO

CREATE INDEX [IX_Segmentation_SegmentAdvanceFilterId] ON [Segmentation] ([SegmentAdvanceFilterId]);
GO

CREATE INDEX [IX_State_RegionId] ON [State] ([RegionId]);
GO

CREATE INDEX [IX_SubMenuMaster_MenuMasterId] ON [SubMenuMaster] ([MenuMasterId]);
GO

CREATE INDEX [IX_SubTreatment_TreatmentId] ON [SubTreatment] ([TreatmentId]);
GO

CREATE INDEX [IX_TFlexIdentificationDocTypes_TFlexIdentificationTypeId] ON [TFlexIdentificationDocTypes] ([TFlexIdentificationTypeId]);
GO

CREATE INDEX [IX_TreatmentAndSegmentMapping_SegmentId] ON [TreatmentAndSegmentMapping] ([SegmentId]);
GO

CREATE INDEX [IX_TreatmentAndSegmentMapping_TreatmentId] ON [TreatmentAndSegmentMapping] ([TreatmentId]);
GO

CREATE INDEX [IX_TreatmentByRule_DepartmentId] ON [TreatmentByRule] ([DepartmentId]);
GO

CREATE INDEX [IX_TreatmentByRule_DesignationId] ON [TreatmentByRule] ([DesignationId]);
GO

CREATE INDEX [IX_TreatmentByRule_SubTreatmentId] ON [TreatmentByRule] ([SubTreatmentId]);
GO

CREATE INDEX [IX_TreatmentByRule_TreatmentId] ON [TreatmentByRule] ([TreatmentId]);
GO

CREATE INDEX [IX_TreatmentDesignation_DepartmentId] ON [TreatmentDesignation] ([DepartmentId]);
GO

CREATE INDEX [IX_TreatmentDesignation_DesignationId] ON [TreatmentDesignation] ([DesignationId]);
GO

CREATE INDEX [IX_TreatmentDesignation_SubTreatmentId] ON [TreatmentDesignation] ([SubTreatmentId]);
GO

CREATE INDEX [IX_TreatmentDesignation_TreatmentId] ON [TreatmentDesignation] ([TreatmentId]);
GO

CREATE INDEX [IX_TreatmentHistory_TreatmentId] ON [TreatmentHistory] ([TreatmentId]);
GO

CREATE INDEX [IX_TreatmentOnAccount_DepartmentId] ON [TreatmentOnAccount] ([DepartmentId]);
GO

CREATE INDEX [IX_TreatmentOnAccount_DesignationId] ON [TreatmentOnAccount] ([DesignationId]);
GO

CREATE INDEX [IX_TreatmentOnAccount_SubTreatmentId] ON [TreatmentOnAccount] ([SubTreatmentId]);
GO

CREATE INDEX [IX_TreatmentOnAccount_TreatmentId] ON [TreatmentOnAccount] ([TreatmentId]);
GO

CREATE INDEX [IX_TreatmentOnCommunication_CommunicationTemplateId] ON [TreatmentOnCommunication] ([CommunicationTemplateId]);
GO

CREATE UNIQUE INDEX [IX_TreatmentOnCommunication_SubTreatmentId] ON [TreatmentOnCommunication] ([SubTreatmentId]) WHERE [SubTreatmentId] IS NOT NULL;
GO

CREATE INDEX [IX_TreatmentOnCommunication_TreatmentId] ON [TreatmentOnCommunication] ([TreatmentId]);
GO

CREATE INDEX [IX_TreatmentOnCommunicationHistoryDetails_CommunicationTemplateId] ON [TreatmentOnCommunicationHistoryDetails] ([CommunicationTemplateId]);
GO

CREATE INDEX [IX_TreatmentOnCommunicationHistoryDetails_LoanAccountId] ON [TreatmentOnCommunicationHistoryDetails] ([LoanAccountId]);
GO

CREATE INDEX [IX_TreatmentOnCommunicationHistoryDetails_PaymentTransactionId] ON [TreatmentOnCommunicationHistoryDetails] ([PaymentTransactionId]);
GO

CREATE INDEX [IX_TreatmentOnCommunicationHistoryDetails_SubTreatmentId] ON [TreatmentOnCommunicationHistoryDetails] ([SubTreatmentId]);
GO

CREATE INDEX [IX_TreatmentOnCommunicationHistoryDetails_TreatmentHistoryId] ON [TreatmentOnCommunicationHistoryDetails] ([TreatmentHistoryId]);
GO

CREATE INDEX [IX_TreatmentOnPerformanceBand_SubTreatmentId] ON [TreatmentOnPerformanceBand] ([SubTreatmentId]);
GO

CREATE INDEX [IX_TreatmentOnPerformanceBand_TreatmentId] ON [TreatmentOnPerformanceBand] ([TreatmentId]);
GO

CREATE INDEX [IX_TreatmentOnPOS_DepartmentId] ON [TreatmentOnPOS] ([DepartmentId]);
GO

CREATE INDEX [IX_TreatmentOnPOS_DesignationId] ON [TreatmentOnPOS] ([DesignationId]);
GO

CREATE INDEX [IX_TreatmentOnPOS_SubTreatmentId] ON [TreatmentOnPOS] ([SubTreatmentId]);
GO

CREATE INDEX [IX_TreatmentOnPOS_TreatmentId] ON [TreatmentOnPOS] ([TreatmentId]);
GO

CREATE UNIQUE INDEX [IX_TreatmentOnUpdateTrail_SubTreatmentId] ON [TreatmentOnUpdateTrail] ([SubTreatmentId]) WHERE [SubTreatmentId] IS NOT NULL;
GO

CREATE INDEX [IX_TreatmentOnUpdateTrail_TreatmentId] ON [TreatmentOnUpdateTrail] ([TreatmentId]);
GO

CREATE INDEX [IX_TreatmentQualifyingStatus_SubTreatmentId] ON [TreatmentQualifyingStatus] ([SubTreatmentId]);
GO

CREATE INDEX [IX_TreatmentQualifyingStatus_TreatmentId] ON [TreatmentQualifyingStatus] ([TreatmentId]);
GO

CREATE INDEX [IX_UserAttendanceDetail_ApplicationUserId] ON [UserAttendanceDetail] ([ApplicationUserId]);
GO

CREATE INDEX [IX_UserAttendanceLog_ApplicationUserId] ON [UserAttendanceLog] ([ApplicationUserId]);
GO

CREATE INDEX [IX_UserCustomerPersona_ApplicationUserId] ON [UserCustomerPersona] ([ApplicationUserId]);
GO

CREATE INDEX [IX_UserPerformanceBand_AgencyUserId] ON [UserPerformanceBand] ([AgencyUserId]);
GO

CREATE INDEX [IX_UserPerformanceBand_CompanyUserId] ON [UserPerformanceBand] ([CompanyUserId]);
GO

CREATE INDEX [IX_UserSearchCriteria_UserId] ON [UserSearchCriteria] ([UserId]);
GO

CREATE INDEX [IX_UserVerificationCodes_UserVerificationCodeTypeId] ON [UserVerificationCodes] ([UserVerificationCodeTypeId]);
GO

ALTER TABLE [AgencyIdentification] ADD CONSTRAINT [FK_AgencyIdentification_ApplicationOrg_TFlexId] FOREIGN KEY ([TFlexId]) REFERENCES [ApplicationOrg] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [AgencyPlaceOfWork] ADD CONSTRAINT [FK_AgencyPlaceOfWork_ApplicationOrg_AgencyId] FOREIGN KEY ([AgencyId]) REFERENCES [ApplicationOrg] ([Id]);
GO

ALTER TABLE [AgencyPlaceOfWork] ADD CONSTRAINT [FK_AgencyPlaceOfWork_ApplicationUser_ManagerId] FOREIGN KEY ([ManagerId]) REFERENCES [ApplicationUser] ([Id]);
GO

ALTER TABLE [AgencyScopeOfWork] ADD CONSTRAINT [FK_AgencyScopeOfWork_ApplicationOrg_AgencyId] FOREIGN KEY ([AgencyId]) REFERENCES [ApplicationOrg] ([Id]);
GO

ALTER TABLE [AgencyUserDesignation] ADD CONSTRAINT [FK_AgencyUserDesignation_ApplicationUser_AgencyUserId] FOREIGN KEY ([AgencyUserId]) REFERENCES [ApplicationUser] ([Id]);
GO

ALTER TABLE [AgencyUserIdentification] ADD CONSTRAINT [FK_AgencyUserIdentification_ApplicationUser_TFlexId] FOREIGN KEY ([TFlexId]) REFERENCES [ApplicationUser] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [AgencyUserPlaceOfWork] ADD CONSTRAINT [FK_AgencyUserPlaceOfWork_ApplicationUser_AgencyUserId] FOREIGN KEY ([AgencyUserId]) REFERENCES [ApplicationUser] ([Id]);
GO

ALTER TABLE [AgencyUserScopeOfWork] ADD CONSTRAINT [FK_AgencyUserScopeOfWork_ApplicationUser_AgencyUserId] FOREIGN KEY ([AgencyUserId]) REFERENCES [ApplicationUser] ([Id]);
GO

ALTER TABLE [ApplicationOrg] ADD CONSTRAINT [FK_ApplicationOrg_ApplicationUser_RecommendingOfficerId] FOREIGN KEY ([RecommendingOfficerId]) REFERENCES [ApplicationUser] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240822034713_Initial', N'8.0.6');
GO

COMMIT;
GO

