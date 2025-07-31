BEGIN TRANSACTION;
GO

DROP TABLE [RoleAccountScope];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DispositionCodeMaster]') AND [c].[name] = N'DispositionCodeCustomerOrAccountLevel');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [DispositionCodeMaster] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [DispositionCodeMaster] DROP COLUMN [DispositionCodeCustomerOrAccountLevel];
GO

ALTER TABLE [DispositionCodeMaster] ADD [DispositionCodeIsCustomerLevel] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

CREATE TABLE [AccountScopeConfiguration] (
    [Id] nvarchar(32) NOT NULL,
    [AccountabilityTypeId] nvarchar(32) NOT NULL,
    [Scope] nvarchar(max) NOT NULL,
    [ScopeLevel] int NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AccountScopeConfiguration] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AccountScopeConfiguration_AccountabilityTypes_AccountabilityTypeId] FOREIGN KEY ([AccountabilityTypeId]) REFERENCES [AccountabilityTypes] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccountabilityTypeId', N'CreatedBy', N'CreatedDate', N'IsDeleted', N'LastModifiedBy', N'LastModifiedDate', N'Scope', N'ScopeLevel') AND [object_id] = OBJECT_ID(N'[AccountScopeConfiguration]'))
    SET IDENTITY_INSERT [AccountScopeConfiguration] ON;
INSERT INTO [AccountScopeConfiguration] ([Id], [AccountabilityTypeId], [CreatedBy], [CreatedDate], [IsDeleted], [LastModifiedBy], [LastModifiedDate], [Scope], [ScopeLevel])
VALUES (N'3a185d8db599c016d4caf7aa05af889f', N'AgencyToFrontEndExternalFOS', NULL, '2025-04-11T08:53:26.8075447+02:00', CAST(0 AS bit), NULL, '2025-04-11T08:53:26.8075486+02:00', N'all', 1),
(N'3a185d8db599d1ce3ace0b1c74528678', N'BankToFrontEndInternalFOS', NULL, '2025-04-11T08:53:26.8075501+02:00', CAST(0 AS bit), NULL, '2025-04-11T08:53:26.8075502+02:00', N'all', 1),
(N'3a185d8db599f4a83d63dec4faea8a98', N'AgencyToFrontEndExternalTC', NULL, '2025-04-11T08:53:26.8075494+02:00', CAST(0 AS bit), NULL, '2025-04-11T08:53:26.8075495+02:00', N'all', 1),
(N'3a185d8db599f686a3b157eaeb799b2d', N'BankToFrontEndInternalTC', NULL, '2025-04-11T08:53:26.8075513+02:00', CAST(0 AS bit), NULL, '2025-04-11T08:53:26.8075514+02:00', N'all', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccountabilityTypeId', N'CreatedBy', N'CreatedDate', N'IsDeleted', N'LastModifiedBy', N'LastModifiedDate', N'Scope', N'ScopeLevel') AND [object_id] = OBJECT_ID(N'[AccountScopeConfiguration]'))
    SET IDENTITY_INSERT [AccountScopeConfiguration] OFF;
GO

CREATE INDEX [IX_AccountScopeConfiguration_AccountabilityTypeId] ON [AccountScopeConfiguration] ([AccountabilityTypeId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250411065329_AlterDispositionCodeMaster', N'8.0.10');
GO

COMMIT;
GO

