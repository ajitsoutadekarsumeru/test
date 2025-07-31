START TRANSACTION;

ALTER TABLE `SettlementStatusHistory` DROP FOREIGN KEY `FK_SettlementStatusHistory_ApplicationUser_ChangedBy`;

DROP TABLE `AssignedUser`;

DROP TABLE `WorkflowAssignment`;

DROP TABLE `LevelDesignation`;

ALTER TABLE `SettlementStatusHistory` DROP INDEX `IX_SettlementStatusHistory_ChangedBy`;

ALTER TABLE `SettlementStatusHistory` DROP COLUMN `ChangedBy`;

ALTER TABLE `SettlementQueueProjection` MODIFY COLUMN `WorkflowInstanceId` varchar(150) CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `SettlementDocument` MODIFY COLUMN `FileName` varchar(100) CHARACTER SET utf8mb4 NULL;

CREATE INDEX `IX_SettlementStatusHistory_ChangedByUserId` ON `SettlementStatusHistory` (`ChangedByUserId`);

ALTER TABLE `SettlementStatusHistory` ADD CONSTRAINT `FK_SettlementStatusHistory_ApplicationUser_ChangedByUserId` FOREIGN KEY (`ChangedByUserId`) REFERENCES `ApplicationUser` (`Id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250528075101_SettlementRemovedUnwantedModels', '8.0.10');

COMMIT;

