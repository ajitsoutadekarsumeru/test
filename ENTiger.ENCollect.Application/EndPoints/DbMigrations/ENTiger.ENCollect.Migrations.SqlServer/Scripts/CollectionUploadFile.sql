BEGIN TRANSACTION;
GO

CREATE TABLE [CollectionUploadFile] (
    [Id] nvarchar(32) NOT NULL,
    [Description] nvarchar(200) NULL,
    [FileName] nvarchar(250) NULL,
    [FilePath] nvarchar(250) NULL,
    [FileUploadedDate] datetime2 NOT NULL,
    [FileProcessedDateTime] datetime2 NOT NULL,
    [Status] nvarchar(50) NULL,
    [CustomId] nvarchar(50) NULL,
    [UploadType] nvarchar(50) NULL,
    [CreatedBy] nvarchar(32) NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastModifiedBy] nvarchar(32) NULL,
    [LastModifiedDate] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_CollectionUploadFile] PRIMARY KEY ([Id])
);
GO

UPDATE [AgencyType] SET [SubType] = N'field agents'
WHERE [Id] = N'ff379ce22f7b4aca9e74d0dadccb3739';
SELECT @@ROWCOUNT;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250224082745_CollectionUploadFile', N'8.0.10');
GO

COMMIT;
GO

