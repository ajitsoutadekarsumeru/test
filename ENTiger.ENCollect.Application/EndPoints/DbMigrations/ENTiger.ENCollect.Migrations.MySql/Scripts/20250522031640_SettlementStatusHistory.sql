START TRANSACTION;

ALTER TABLE `SettlementStatusHistory` MODIFY COLUMN `ChangedBy` varchar(32) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `SettlementStatusHistory` ADD `ChangedByUserId` varchar(50) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

CREATE INDEX `IX_SettlementStatusHistory_ChangedBy` ON `SettlementStatusHistory` (`ChangedBy`);

ALTER TABLE `SettlementStatusHistory` ADD CONSTRAINT `FK_SettlementStatusHistory_ApplicationUser_ChangedBy` FOREIGN KEY (`ChangedBy`) REFERENCES `ApplicationUser` (`Id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250522031640_SettlementStatusHistory', '8.0.10');

COMMIT;

