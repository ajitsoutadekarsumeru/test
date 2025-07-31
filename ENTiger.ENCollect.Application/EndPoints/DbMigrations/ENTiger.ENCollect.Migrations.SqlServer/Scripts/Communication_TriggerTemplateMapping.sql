BEGIN TRANSACTION;
GO

ALTER TABLE [CommunicationTrigger] DROP CONSTRAINT [FK_CommunicationTrigger_CategoryItem_TriggerTypeId];
GO

DROP TABLE [CommunicationTemplateWorkflowState];
GO

DROP TABLE [CommunicationTriggerTemplate];
GO

DROP INDEX [IX_CommunicationTrigger_TriggerTypeId] ON [CommunicationTrigger];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationTrigger]') AND [c].[name] = N'MaximumOccurences');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationTrigger] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [CommunicationTrigger] DROP COLUMN [MaximumOccurences];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationTrigger]') AND [c].[name] = N'TriggerTypeId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationTrigger] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [CommunicationTrigger] DROP COLUMN [TriggerTypeId];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationTemplate]') AND [c].[name] = N'IsAllowAccessFromAccountDetails');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationTemplate] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [CommunicationTemplate] DROP COLUMN [IsAllowAccessFromAccountDetails];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CommunicationTrigger]') AND [c].[name] = N'Description');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [CommunicationTrigger] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [CommunicationTrigger] ALTER COLUMN [Description] nvarchar(1000) NULL;
GO

ALTER TABLE [CommunicationTrigger] ADD [CommunicationTriggerTypeId] nvarchar(32) NOT NULL DEFAULT N'';
GO

ALTER TABLE [CommunicationTemplate] ADD [IsAvailableInAccountDetails] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

CREATE TABLE [CommunicationTriggerType] (
    [Id] nvarchar(32) NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [IsActive] bit NOT NULL,
    [Description] nvarchar(100) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_CommunicationTriggerType] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TriggerTemplateMapping] (
    [Id] nvarchar(32) NOT NULL,
    [CommunicationTriggerId] nvarchar(32) NOT NULL,
    [CommunicationTemplateId] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TriggerTemplateMapping] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TriggerTemplateMapping_CommunicationTemplate_CommunicationTemplateId] FOREIGN KEY ([CommunicationTemplateId]) REFERENCES [CommunicationTemplate] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_TriggerTemplateMapping_CommunicationTrigger_CommunicationTriggerId] FOREIGN KEY ([CommunicationTriggerId]) REFERENCES [CommunicationTrigger] ([Id]) ON DELETE CASCADE
);
GO


GO

CREATE INDEX [IX_CommunicationTrigger_CommunicationTriggerTypeId] ON [CommunicationTrigger] ([CommunicationTriggerTypeId]);
GO

CREATE INDEX [IX_TriggerTemplateMapping_CommunicationTemplateId] ON [TriggerTemplateMapping] ([CommunicationTemplateId]);
GO

CREATE INDEX [IX_TriggerTemplateMapping_CommunicationTriggerId] ON [TriggerTemplateMapping] ([CommunicationTriggerId]);
GO

ALTER TABLE [CommunicationTrigger] ADD CONSTRAINT [FK_CommunicationTrigger_CommunicationTriggerType_CommunicationTriggerTypeId] FOREIGN KEY ([CommunicationTriggerTypeId]) REFERENCES [CommunicationTriggerType] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250619115423_Communication_TriggerTemplateMapping', N'8.0.10');
GO

COMMIT;
GO

