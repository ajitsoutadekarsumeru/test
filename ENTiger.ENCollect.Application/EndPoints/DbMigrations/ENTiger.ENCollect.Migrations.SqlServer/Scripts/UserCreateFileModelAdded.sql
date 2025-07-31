BEGIN TRANSACTION;
GO

CREATE TABLE [UsersCreateFile] (
    [Id] nvarchar(32) NOT NULL,
    [CustomId] nvarchar(50) NOT NULL,
    [UploadType] nvarchar(100) NOT NULL,
    [FileName] nvarchar(250) NOT NULL,
    [FilePath] nvarchar(250) NULL,
    [Status] nvarchar(50) NOT NULL,
    [UploadedDate] datetime2 NOT NULL,
    [ProcessedDateTime] datetime2 NULL,
    [Description] nvarchar(200) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UsersCreateFile] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241014123300_UserCreateFileModelAdded', N'8.0.6');
GO

COMMIT;
GO

