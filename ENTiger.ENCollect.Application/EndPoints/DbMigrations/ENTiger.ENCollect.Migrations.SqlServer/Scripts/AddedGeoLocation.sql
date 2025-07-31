BEGIN TRANSACTION;
GO

ALTER TABLE [UserAttendanceLog] ADD [GeoLocation] nvarchar(800) NULL;
GO

ALTER TABLE [UserAttendanceLog] ADD [IsFirstLogin] bit NULL;
GO

ALTER TABLE [UserAttendanceLog] ADD [Created_DateOnly] AS CAST(CreatedDate AS DATE) PERSISTED;
GO

ALTER TABLE [Feedback] ADD [Created_DateOnly] AS CAST(CreatedDate AS DATE) PERSISTED;
GO

ALTER TABLE [Collections] ADD [Created_DateOnly] AS CAST(CreatedDate AS DATE) PERSISTED;
GO

CREATE INDEX [IX_UserAttendanceLog_Created_DateOnly] ON [UserAttendanceLog] ([Created_DateOnly]);
GO

CREATE INDEX [IX_Feedback_Created_DateOnly] ON [Feedback] ([Created_DateOnly]);
GO

CREATE INDEX [IX_Collections_Created_DateOnly] ON [Collections] ([Created_DateOnly]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250115114500_AddedGeoLocation', N'8.0.10');
GO

COMMIT;
GO

