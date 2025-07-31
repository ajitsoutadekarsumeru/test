BEGIN TRANSACTION;
GO

CREATE TABLE [RoleAccountScope] (
    [Id] nvarchar(32) NOT NULL,
    [AccountabilityTypeId] nvarchar(32) NOT NULL,
    [Scope] nvarchar(max) NOT NULL,
    [ScopeLevel] int NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_RoleAccountScope] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoleAccountScope_AccountabilityTypes_AccountabilityTypeId] FOREIGN KEY ([AccountabilityTypeId]) REFERENCES [AccountabilityTypes] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccountabilityTypeId', N'CreatedBy', N'CreatedDate', N'IsDeleted', N'LastModifiedBy', N'LastModifiedDate', N'Scope', N'ScopeLevel') AND [object_id] = OBJECT_ID(N'[RoleAccountScope]'))
    SET IDENTITY_INSERT [RoleAccountScope] ON;
INSERT INTO [RoleAccountScope] ([Id], [AccountabilityTypeId], [CreatedBy], [CreatedDate], [IsDeleted], [LastModifiedBy], [LastModifiedDate], [Scope], [ScopeLevel])
VALUES (N'3a185d8db599c016d4caf7aa05af889f', N'AgencyToFrontEndExternalFOS', NULL, '2025-02-28T12:36:34.2652788+05:30', CAST(0 AS bit), NULL, '2025-02-28T12:36:34.2652828+05:30', N'all', 1),
(N'3a185d8db599d1ce3ace0b1c74528678', N'BankToFrontEndInternalFOS', NULL, '2025-02-28T12:36:34.2652930+05:30', CAST(0 AS bit), NULL, '2025-02-28T12:36:34.2652932+05:30', N'all', 1),
(N'3a185d8db599f4a83d63dec4faea8a98', N'AgencyToFrontEndExternalTC', NULL, '2025-02-28T12:36:34.2652912+05:30', CAST(0 AS bit), NULL, '2025-02-28T12:36:34.2652914+05:30', N'all', 1),
(N'3a185d8db599f686a3b157eaeb799b2d', N'BankToFrontEndInternalTC', NULL, '2025-02-28T12:36:34.2652953+05:30', CAST(0 AS bit), NULL, '2025-02-28T12:36:34.2652954+05:30', N'all', 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccountabilityTypeId', N'CreatedBy', N'CreatedDate', N'IsDeleted', N'LastModifiedBy', N'LastModifiedDate', N'Scope', N'ScopeLevel') AND [object_id] = OBJECT_ID(N'[RoleAccountScope]'))
    SET IDENTITY_INSERT [RoleAccountScope] OFF;
GO

CREATE INDEX [IX_RoleAccountScope_AccountabilityTypeId] ON [RoleAccountScope] ([AccountabilityTypeId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250228070637_AddedRoleAccountScope_WithSeedData', N'8.0.10');
GO

COMMIT;
GO

