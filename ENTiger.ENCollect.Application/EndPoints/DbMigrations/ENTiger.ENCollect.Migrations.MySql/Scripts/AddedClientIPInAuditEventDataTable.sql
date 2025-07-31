START TRANSACTION;

ALTER TABLE `AuditTrailRecord` ADD `ClientIP` longtext CHARACTER SET utf8mb4 NOT NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250127092139_AddedClientIPInAuditEventDataTable', '8.0.10');

COMMIT;

