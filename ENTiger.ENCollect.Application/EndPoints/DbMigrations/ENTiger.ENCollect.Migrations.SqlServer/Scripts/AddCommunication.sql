BEGIN TRANSACTION;
GO

ALTER TABLE [CommunicationTemplate] DROP CONSTRAINT [FK_CommunicationTemplate_CommunicationTemplateDetail_CommunicationTemplateDetailId];
GO

ALTER TABLE [CommunicationTemplate] DROP CONSTRAINT [FK_CommunicationTemplate_CommunicationTemplateWorkflowState_CommunicationTemplateWorkflowStateId];
GO

DROP INDEX [IX_CommunicationTemplateDetail_Name] ON [CommunicationTemplateDetail];
GO

DROP INDEX [IX_CommunicationTemplate_CommunicationTemplateDetailId] ON [CommunicationTemplate];
GO

DROP INDEX [IX_CommunicationTemplate_CommunicationTemplateWorkflowStateId] ON [CommunicationTemplate];
GO



DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationTemplateDetail]') AND [c].[name] = N'AddressTo');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationTemplateDetail] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [CommunicationTemplateDetail] DROP COLUMN [AddressTo];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationTemplateDetail]') AND [c].[name] = N'Name');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationTemplateDetail] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [CommunicationTemplateDetail] DROP COLUMN [Name];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationTemplateDetail]') AND [c].[name] = N'Salutation');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationTemplateDetail] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [CommunicationTemplateDetail] DROP COLUMN [Salutation];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationTemplateDetail]') AND [c].[name] = N'Signature');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationTemplateDetail] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [CommunicationTemplateDetail] DROP COLUMN [Signature];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationTemplate]') AND [c].[name] = N'CommunicationTemplateDetailId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationTemplate] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [CommunicationTemplate] DROP COLUMN [CommunicationTemplateDetailId];
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationTemplate]') AND [c].[name] = N'CommunicationTemplateWorkflowStateId');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationTemplate] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [CommunicationTemplate] DROP COLUMN [CommunicationTemplateWorkflowStateId];
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationTemplate]') AND [c].[name] = N'Language');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationTemplate] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [CommunicationTemplate] DROP COLUMN [Language];
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationTemplate]') AND [c].[name] = N'Recipient');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationTemplate] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [CommunicationTemplate] DROP COLUMN [Recipient];
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationTemplate]') AND [c].[name] = N'WATemplateId');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationTemplate] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [CommunicationTemplate] DROP COLUMN [WATemplateId];
GO

EXEC sp_rename N'[CommunicationTemplate].[IsDisabled]', N'IsActive', N'COLUMN';
GO


DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationTemplateDetail]') AND [c].[name] = N'Subject');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationTemplateDetail] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [CommunicationTemplateDetail] ALTER COLUMN [Subject] nvarchar(200) NULL;
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationTemplateDetail]') AND [c].[name] = N'Body');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationTemplateDetail] DROP CONSTRAINT [' + @var13 + '];');
UPDATE [CommunicationTemplateDetail] SET [Body] = N'' WHERE [Body] IS NULL;
ALTER TABLE [CommunicationTemplateDetail] ALTER COLUMN [Body] nvarchar(max) NOT NULL;
ALTER TABLE [CommunicationTemplateDetail] ADD DEFAULT N'' FOR [Body];
GO

ALTER TABLE [CommunicationTemplateDetail] ADD [CommunicationTemplateId] nvarchar(32) NOT NULL DEFAULT N'';
GO

ALTER TABLE [CommunicationTemplateDetail] ADD [Language] nvarchar(50) NOT NULL DEFAULT N'';
GO

ALTER TABLE [CommunicationTemplateDetail] ADD [Version] int NOT NULL DEFAULT 0;
GO

DROP INDEX [IX_CommunicationTemplate_TemplateType] ON [CommunicationTemplate];
DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationTemplate]') AND [c].[name] = N'TemplateType');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationTemplate] DROP CONSTRAINT [' + @var14 + '];');
UPDATE [CommunicationTemplate] SET [TemplateType] = N'' WHERE [TemplateType] IS NULL;
ALTER TABLE [CommunicationTemplate] ALTER COLUMN [TemplateType] nvarchar(450) NOT NULL;
ALTER TABLE [CommunicationTemplate] ADD DEFAULT N'' FOR [TemplateType];
CREATE INDEX [IX_CommunicationTemplate_TemplateType] ON [CommunicationTemplate] ([TemplateType]);
GO

ALTER TABLE [CommunicationTemplate] ADD [IsAllowAccessFromAccountDetails] bit NULL;
GO

ALTER TABLE [CommunicationTemplate] ADD [Name] nvarchar(50) NOT NULL DEFAULT N'';
GO

ALTER TABLE [CommunicationTemplate] ADD [Version] int NOT NULL DEFAULT 0;
GO

CREATE TABLE [CommunicationTrigger] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [TriggerTypeId] nvarchar(32) NOT NULL,
    [DaysOffset] int NOT NULL,
    [IsActive] bit NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [MaximumOccurences] int NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_CommunicationTrigger] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CommunicationTrigger_CategoryItem_TriggerTypeId] FOREIGN KEY ([TriggerTypeId]) REFERENCES [CategoryItem] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [CommunicationTriggerTemplate] (
    [Id] nvarchar(32) NOT NULL,
    [CommunicationTriggerId] nvarchar(32) NOT NULL,
    [CommunicationTemplateId] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_CommunicationTriggerTemplate] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CommunicationTriggerTemplate_CommunicationTemplate_CommunicationTemplateId] FOREIGN KEY ([CommunicationTemplateId]) REFERENCES [CommunicationTemplate] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CommunicationTriggerTemplate_CommunicationTrigger_CommunicationTriggerId] FOREIGN KEY ([CommunicationTriggerId]) REFERENCES [CommunicationTrigger] ([Id]) ON DELETE CASCADE
);
GO



GO

CREATE INDEX [IX_CommunicationTemplateDetail_CommunicationTemplateId] ON [CommunicationTemplateDetail] ([CommunicationTemplateId]);
GO

CREATE INDEX [IX_CommunicationTemplate_Name] ON [CommunicationTemplate] ([Name]);
GO

CREATE INDEX [IX_CommunicationTrigger_TriggerTypeId] ON [CommunicationTrigger] ([TriggerTypeId]);
GO

CREATE INDEX [IX_CommunicationTriggerTemplate_CommunicationTemplateId] ON [CommunicationTriggerTemplate] ([CommunicationTemplateId]);
GO

CREATE INDEX [IX_CommunicationTriggerTemplate_CommunicationTriggerId] ON [CommunicationTriggerTemplate] ([CommunicationTriggerId]);
GO

ALTER TABLE [CommunicationTemplateDetail] ADD CONSTRAINT [FK_CommunicationTemplateDetail_CommunicationTemplate_CommunicationTemplateId] FOREIGN KEY ([CommunicationTemplateId]) REFERENCES [CommunicationTemplate] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250617062523_AddCommunication', N'8.0.10');
GO

COMMIT;
GO

