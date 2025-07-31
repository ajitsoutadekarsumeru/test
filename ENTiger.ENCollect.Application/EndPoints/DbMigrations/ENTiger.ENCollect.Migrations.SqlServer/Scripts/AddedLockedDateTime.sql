BEGIN TRANSACTION;
GO

ALTER TABLE [ApplicationUser] ADD [LockedDateTime] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250224113815_AddedLockedDateTime', N'8.0.10');
GO

COMMIT;
GO

