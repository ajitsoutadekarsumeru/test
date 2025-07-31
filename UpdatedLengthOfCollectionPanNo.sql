BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Collections]') AND [c].[name] = N'yPANNo');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Collections] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Collections] ALTER COLUMN [yPANNo] nvarchar(200) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250715055728_UpdatedLengthOfCollectionPanNo', N'8.0.10');
GO

COMMIT;
GO

