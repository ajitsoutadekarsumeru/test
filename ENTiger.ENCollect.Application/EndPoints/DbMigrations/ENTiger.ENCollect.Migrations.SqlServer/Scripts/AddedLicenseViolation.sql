BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Permissions]') AND [c].[name] = N'IsActive');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Permissions] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Permissions] DROP COLUMN [IsActive];
GO

EXEC sp_rename N'[Permissions].[PermissionName]', N'Name', N'COLUMN';
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Permissions]') AND [c].[name] = N'Description');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Permissions] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Permissions] ALTER COLUMN [Description] nvarchar(255) NULL;
GO

ALTER TABLE [Permissions] ADD [Section] nvarchar(50) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Designation] ADD [PermissionSchemeId] nvarchar(32) NULL;
GO

ALTER TABLE [ApplicationUser] ADD [UserType] nvarchar(20) NOT NULL DEFAULT N'';
GO

CREATE TABLE [PermissionSchemeChangeLog] (
    [Id] nvarchar(32) NOT NULL,
    [PermissionSchemeId] nvarchar(max) NULL,
    [AddedPermissions] nvarchar(max) NULL,
    [RemovedPermissions] nvarchar(max) NULL,
    [ChangeType] nvarchar(max) NULL,
    [Remarks] nvarchar(max) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_PermissionSchemeChangeLog] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [PermissionSchemes] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [Remarks] nvarchar(500) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_PermissionSchemes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [EnabledPermission] (
    [Id] nvarchar(32) NOT NULL,
    [PermissionId] nvarchar(32) NOT NULL,
    [PermissionSchemeId] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_EnabledPermission] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EnabledPermission_PermissionSchemes_PermissionSchemeId] FOREIGN KEY ([PermissionSchemeId]) REFERENCES [PermissionSchemes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EnabledPermission_Permissions_PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [Permissions] ([Id]) ON DELETE CASCADE
);
GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-13T10:33:52.8771589+05:30', [LastModifiedDate] = '2025-05-13T10:33:52.8771682+05:30'
WHERE [Id] = N'3a185d8db599c016d4caf7aa05af889f';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-13T10:33:52.8771712+05:30', [LastModifiedDate] = '2025-05-13T10:33:52.8771714+05:30'
WHERE [Id] = N'3a185d8db599d1ce3ace0b1c74528678';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-13T10:33:52.8771703+05:30', [LastModifiedDate] = '2025-05-13T10:33:52.8771705+05:30'
WHERE [Id] = N'3a185d8db599f4a83d63dec4faea8a98';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-13T10:33:52.8771752+05:30', [LastModifiedDate] = '2025-05-13T10:33:52.8771754+05:30'
WHERE [Id] = N'3a185d8db599f686a3b157eaeb799b2d';
SELECT @@ROWCOUNT;

GO

CREATE INDEX [IX_Designation_PermissionSchemeId] ON [Designation] ([PermissionSchemeId]);
GO

CREATE INDEX [IX_EnabledPermission_PermissionId] ON [EnabledPermission] ([PermissionId]);
GO

CREATE INDEX [IX_EnabledPermission_PermissionSchemeId] ON [EnabledPermission] ([PermissionSchemeId]);
GO

ALTER TABLE [Designation] ADD CONSTRAINT [FK_Designation_PermissionSchemes_PermissionSchemeId] FOREIGN KEY ([PermissionSchemeId]) REFERENCES [PermissionSchemes] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250513050356_AddedPermissionScheme', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PermissionSchemeChangeLog]') AND [c].[name] = N'RemovedPermissions');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [PermissionSchemeChangeLog] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [PermissionSchemeChangeLog] ALTER COLUMN [RemovedPermissions] nvarchar(2000) NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PermissionSchemeChangeLog]') AND [c].[name] = N'Remarks');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [PermissionSchemeChangeLog] DROP CONSTRAINT [' + @var3 + '];');
UPDATE [PermissionSchemeChangeLog] SET [Remarks] = N'' WHERE [Remarks] IS NULL;
ALTER TABLE [PermissionSchemeChangeLog] ALTER COLUMN [Remarks] nvarchar(500) NOT NULL;
ALTER TABLE [PermissionSchemeChangeLog] ADD DEFAULT N'' FOR [Remarks];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PermissionSchemeChangeLog]') AND [c].[name] = N'PermissionSchemeId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [PermissionSchemeChangeLog] DROP CONSTRAINT [' + @var4 + '];');
UPDATE [PermissionSchemeChangeLog] SET [PermissionSchemeId] = N'' WHERE [PermissionSchemeId] IS NULL;
ALTER TABLE [PermissionSchemeChangeLog] ALTER COLUMN [PermissionSchemeId] nvarchar(32) NOT NULL;
ALTER TABLE [PermissionSchemeChangeLog] ADD DEFAULT N'' FOR [PermissionSchemeId];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PermissionSchemeChangeLog]') AND [c].[name] = N'ChangeType');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [PermissionSchemeChangeLog] DROP CONSTRAINT [' + @var5 + '];');
UPDATE [PermissionSchemeChangeLog] SET [ChangeType] = N'' WHERE [ChangeType] IS NULL;
ALTER TABLE [PermissionSchemeChangeLog] ALTER COLUMN [ChangeType] nvarchar(50) NOT NULL;
ALTER TABLE [PermissionSchemeChangeLog] ADD DEFAULT N'' FOR [ChangeType];
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PermissionSchemeChangeLog]') AND [c].[name] = N'AddedPermissions');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [PermissionSchemeChangeLog] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [PermissionSchemeChangeLog] ALTER COLUMN [AddedPermissions] nvarchar(2000) NULL;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Permissions]') AND [c].[name] = N'Name');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Permissions] DROP CONSTRAINT [' + @var7 + '];');
UPDATE [Permissions] SET [Name] = N'' WHERE [Name] IS NULL;
ALTER TABLE [Permissions] ALTER COLUMN [Name] nvarchar(100) NOT NULL;
ALTER TABLE [Permissions] ADD DEFAULT N'' FOR [Name];
GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-14T09:50:25.1606003+05:30', [LastModifiedDate] = '2025-05-14T09:50:25.1606034+05:30'
WHERE [Id] = N'3a185d8db599c016d4caf7aa05af889f';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-14T09:50:25.1606051+05:30', [LastModifiedDate] = '2025-05-14T09:50:25.1606052+05:30'
WHERE [Id] = N'3a185d8db599d1ce3ace0b1c74528678';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-14T09:50:25.1606044+05:30', [LastModifiedDate] = '2025-05-14T09:50:25.1606045+05:30'
WHERE [Id] = N'3a185d8db599f4a83d63dec4faea8a98';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-14T09:50:25.1606065+05:30', [LastModifiedDate] = '2025-05-14T09:50:25.1606066+05:30'
WHERE [Id] = N'3a185d8db599f686a3b157eaeb799b2d';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250514042027_UpdatedPermsissionScheme', N'8.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DELETE FROM [AccountScopeConfiguration]
WHERE [Id] = N'3a185d8db599c016d4caf7aa05af889f';
SELECT @@ROWCOUNT;

GO

DELETE FROM [AccountScopeConfiguration]
WHERE [Id] = N'3a185d8db599d1ce3ace0b1c74528678';
SELECT @@ROWCOUNT;

GO

DELETE FROM [AccountScopeConfiguration]
WHERE [Id] = N'3a185d8db599f4a83d63dec4faea8a98';
SELECT @@ROWCOUNT;

GO

DELETE FROM [AccountScopeConfiguration]
WHERE [Id] = N'3a185d8db599f686a3b157eaeb799b2d';
SELECT @@ROWCOUNT;

GO

ALTER TABLE [Settlement] ADD [CURRENT_BUCKET] nvarchar(50) NULL;
GO

ALTER TABLE [Settlement] ADD [CURRENT_DPD] bigint NULL;
GO

ALTER TABLE [Settlement] ADD [ChargesOutstanding] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [Settlement] ADD [CustomId] nvarchar(50) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Settlement] ADD [DateOfDeath] datetime2 NULL;
GO

ALTER TABLE [Settlement] ADD [InterestOutstanding] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [Settlement] ADD [IsDeathSettlement] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Settlement] ADD [LatestHistoryId] nvarchar(32) NULL;
GO

ALTER TABLE [Settlement] ADD [LoanAccountId] nvarchar(32) NULL;
GO

ALTER TABLE [Settlement] ADD [NPA_STAGEID] nvarchar(50) NULL;
GO

ALTER TABLE [Settlement] ADD [NumberOfEmisDue] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Settlement] ADD [NumberOfInstallments] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Settlement] ADD [PrincipalOutstanding] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [Settlement] ADD [SettlementAmount] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

ALTER TABLE [Settlement] ADD [SettlementDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Settlement] ADD [SettlementDateForDuesCalc] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Settlement] ADD [SettlementRemarks] nvarchar(1000) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Settlement] ADD [Status] nvarchar(100) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Settlement] ADD [StatusUpdatedOn] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [Settlement] ADD [TOS] nvarchar(50) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Settlement] ADD [TrancheType] nvarchar(50) NOT NULL DEFAULT N'';
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Designation]') AND [c].[name] = N'Level');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Designation] DROP CONSTRAINT [' + @var8 + '];');
UPDATE [Designation] SET [Level] = 0 WHERE [Level] IS NULL;
ALTER TABLE [Designation] ALTER COLUMN [Level] int NOT NULL;
ALTER TABLE [Designation] ADD DEFAULT 0 FOR [Level];
GO

CREATE TABLE [InstallmentDetail] (
    [Id] nvarchar(32) NOT NULL,
    [InstallmentAmount] decimal(18,2) NOT NULL,
    [InstallmentDueDate] datetime2 NOT NULL,
    [SettlementId] nvarchar(32) NULL,
    CONSTRAINT [PK_InstallmentDetail] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_InstallmentDetail_Settlement_SettlementId] FOREIGN KEY ([SettlementId]) REFERENCES [Settlement] ([Id])
);
GO

CREATE TABLE [LevelDesignation] (
    [Id] nvarchar(32) NOT NULL,
    [Level] int NOT NULL,
    [DesignationId] nvarchar(32) NOT NULL,
    CONSTRAINT [PK_LevelDesignation] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LevelDesignation_Designation_DesignationId] FOREIGN KEY ([DesignationId]) REFERENCES [Designation] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [LicenseViolation] (
    [Id] nvarchar(32) NOT NULL,
    [OccurredOn] datetime2 NOT NULL,
    [Feature] nvarchar(50) NOT NULL,
    [Limit] int NOT NULL,
    [Actual] int NOT NULL,
    [ErrorMessage] nvarchar(100) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_LicenseViolation] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SettlementDocument] (
    [Id] nvarchar(32) NOT NULL,
    [SettlementId] nvarchar(32) NOT NULL,
    [DocumentType] nvarchar(50) NOT NULL,
    [DocumentName] nvarchar(50) NOT NULL,
    [FileName] nvarchar(100) NOT NULL,
    [UploadedOn] datetimeoffset NOT NULL,
    CONSTRAINT [PK_SettlementDocument] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SettlementDocument_Settlement_SettlementId] FOREIGN KEY ([SettlementId]) REFERENCES [Settlement] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SettlementQueueProjection] (
    [Id] nvarchar(32) NOT NULL,
    [WorkflowName] nvarchar(50) NOT NULL,
    [WorkflowInstanceId] nvarchar(50) NOT NULL,
    [SettlementId] nvarchar(32) NOT NULL,
    [StepIndex] int NOT NULL,
    [ApplicationUserId] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_SettlementQueueProjection] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SettlementQueueProjection_ApplicationUser_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SettlementQueueProjection_Settlement_SettlementId] FOREIGN KEY ([SettlementId]) REFERENCES [Settlement] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SettlementStatusHistory] (
    [Id] nvarchar(32) NOT NULL,
    [SettlementId] nvarchar(32) NOT NULL,
    [FromStatus] nvarchar(50) NOT NULL,
    [ToStatus] nvarchar(50) NOT NULL,
    [ChangedByUserId] nvarchar(50) NOT NULL,
    [ChangedBy] nvarchar(32) NULL,
    [ChangedDate] datetimeoffset NOT NULL,
    [Comment] nvarchar(500) NOT NULL,
    CONSTRAINT [PK_SettlementStatusHistory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SettlementStatusHistory_ApplicationUser_ChangedBy] FOREIGN KEY ([ChangedBy]) REFERENCES [ApplicationUser] ([Id]),
    CONSTRAINT [FK_SettlementStatusHistory_Settlement_SettlementId] FOREIGN KEY ([SettlementId]) REFERENCES [Settlement] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserLevelProjection] (
    [Id] nvarchar(32) NOT NULL,
    [ApplicationUserId] nvarchar(32) NOT NULL,
    [Level] int NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UserLevelProjection] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserLevelProjection_ApplicationUser_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [WaiverDetail] (
    [Id] nvarchar(32) NOT NULL,
    [ChargeType] nvarchar(50) NOT NULL,
    [AmountAsPerCBS] decimal(18,2) NOT NULL,
    [ApportionmentAmount] decimal(18,2) NOT NULL,
    [WaiverAmount] decimal(18,2) NOT NULL,
    [WaiverPercent] decimal(18,2) NOT NULL,
    [SettlementId] nvarchar(32) NULL,
    CONSTRAINT [PK_WaiverDetail] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WaiverDetail_Settlement_SettlementId] FOREIGN KEY ([SettlementId]) REFERENCES [Settlement] ([Id])
);
GO

CREATE TABLE [WorkflowAssignment] (
    [Id] nvarchar(32) NOT NULL,
    [SettlementStatusHistoryId] nvarchar(32) NOT NULL,
    [LevelDesignationId] nvarchar(50) NULL,
    CONSTRAINT [PK_WorkflowAssignment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WorkflowAssignment_LevelDesignation_LevelDesignationId] FOREIGN KEY ([LevelDesignationId]) REFERENCES [LevelDesignation] ([Id]),
    CONSTRAINT [FK_WorkflowAssignment_SettlementStatusHistory_SettlementStatusHistoryId] FOREIGN KEY ([SettlementStatusHistoryId]) REFERENCES [SettlementStatusHistory] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AssignedUser] (
    [Id] nvarchar(32) NOT NULL,
    [WorkflowAssignmentId] nvarchar(32) NOT NULL,
    [ApplicationUserId] nvarchar(32) NOT NULL,
    CONSTRAINT [PK_AssignedUser] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AssignedUser_ApplicationUser_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [ApplicationUser] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AssignedUser_WorkflowAssignment_WorkflowAssignmentId] FOREIGN KEY ([WorkflowAssignmentId]) REFERENCES [WorkflowAssignment] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Settlement_LatestHistoryId] ON [Settlement] ([LatestHistoryId]);
GO

CREATE INDEX [IX_Settlement_LoanAccountId] ON [Settlement] ([LoanAccountId]);
GO

CREATE INDEX [IX_AssignedUser_ApplicationUserId] ON [AssignedUser] ([ApplicationUserId]);
GO

CREATE INDEX [IX_AssignedUser_WorkflowAssignmentId] ON [AssignedUser] ([WorkflowAssignmentId]);
GO

CREATE INDEX [IX_InstallmentDetail_SettlementId] ON [InstallmentDetail] ([SettlementId]);
GO

CREATE INDEX [IX_LevelDesignation_DesignationId] ON [LevelDesignation] ([DesignationId]);
GO

CREATE INDEX [IX_SettlementDocument_SettlementId] ON [SettlementDocument] ([SettlementId]);
GO

CREATE INDEX [IX_SettlementQueueProjection_ApplicationUserId] ON [SettlementQueueProjection] ([ApplicationUserId]);
GO

CREATE INDEX [IX_SettlementQueueProjection_SettlementId] ON [SettlementQueueProjection] ([SettlementId]);
GO

CREATE INDEX [IX_SettlementStatusHistory_ChangedBy] ON [SettlementStatusHistory] ([ChangedBy]);
GO

CREATE INDEX [IX_SettlementStatusHistory_SettlementId] ON [SettlementStatusHistory] ([SettlementId]);
GO

CREATE INDEX [IX_UserLevelProjection_ApplicationUserId] ON [UserLevelProjection] ([ApplicationUserId]);
GO

CREATE INDEX [IX_WaiverDetail_SettlementId] ON [WaiverDetail] ([SettlementId]);
GO

CREATE INDEX [IX_WorkflowAssignment_LevelDesignationId] ON [WorkflowAssignment] ([LevelDesignationId]);
GO

CREATE INDEX [IX_WorkflowAssignment_SettlementStatusHistoryId] ON [WorkflowAssignment] ([SettlementStatusHistoryId]);
GO

ALTER TABLE [Settlement] ADD CONSTRAINT [FK_Settlement_LoanAccounts_LoanAccountId] FOREIGN KEY ([LoanAccountId]) REFERENCES [LoanAccounts] ([Id]);
GO

ALTER TABLE [Settlement] ADD CONSTRAINT [FK_Settlement_SettlementStatusHistory_LatestHistoryId] FOREIGN KEY ([LatestHistoryId]) REFERENCES [SettlementStatusHistory] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250527065702_AddedLicenseViolation', N'8.0.10');
GO

COMMIT;
GO

