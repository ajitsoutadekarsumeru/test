BEGIN TRANSACTION;
GO

ALTER TABLE [CompanyUserDesignation] ADD [IsPrimaryDesignation] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [AgencyUserDesignation] ADD [IsPrimaryDesignation] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

CREATE TABLE [InsightDownloadFile] (
    [Id] nvarchar(32) NOT NULL,
    [Description] nvarchar(200) NULL,
    [FileName] nvarchar(250) NULL,
    [FilePath] nvarchar(250) NULL,
    [Status] nvarchar(50) NULL,
    [CustomId] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_InsightDownloadFile] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Settlement] (
    [Id] nvarchar(32) NOT NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Settlement] PRIMARY KEY ([Id])
);
GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-06T13:58:56.1368604+05:30', [LastModifiedDate] = '2025-05-06T13:58:56.1368635+05:30'
WHERE [Id] = N'3a185d8db599c016d4caf7aa05af889f';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-06T13:58:56.1368651+05:30', [LastModifiedDate] = '2025-05-06T13:58:56.1368653+05:30'
WHERE [Id] = N'3a185d8db599d1ce3ace0b1c74528678';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-06T13:58:56.1368644+05:30', [LastModifiedDate] = '2025-05-06T13:58:56.1368645+05:30'
WHERE [Id] = N'3a185d8db599f4a83d63dec4faea8a98';
SELECT @@ROWCOUNT;

GO

UPDATE [AccountScopeConfiguration] SET [CreatedDate] = '2025-05-06T13:58:56.1368668+05:30', [LastModifiedDate] = '2025-05-06T13:58:56.1368670+05:30'
WHERE [Id] = N'3a185d8db599f686a3b157eaeb799b2d';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250506082858_AddedIsPrimaryDesignation', N'8.0.10');
GO

COMMIT;
GO

