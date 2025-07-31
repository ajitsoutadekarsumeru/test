BEGIN TRANSACTION;
GO

ALTER TABLE [ApplicationUser] ADD [BloodGroup] nvarchar(5) NULL;
GO

ALTER TABLE [ApplicationUser] ADD [EmergencyContactNo] nvarchar(10) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240926094211_ApplicationUser_newFields', N'8.0.6');
GO

COMMIT;
GO

