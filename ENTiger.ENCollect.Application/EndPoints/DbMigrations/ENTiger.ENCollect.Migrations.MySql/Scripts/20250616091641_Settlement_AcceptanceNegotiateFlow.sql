START TRANSACTION;

ALTER TABLE `SettlementQueueProjection` DROP COLUMN `StepIndex`;

ALTER TABLE `SettlementStatusHistory` MODIFY COLUMN `Comment` varchar(500) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `SettlementStatusHistory` ADD `RejectionReason` varchar(200) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `SettlementStatusHistory` ADD `RenegotiationAmount` decimal(65,30) NULL;

ALTER TABLE `SettlementQueueProjection` ADD `StepName` varchar(100) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `SettlementQueueProjection` ADD `StepType` varchar(3502) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `SettlementQueueProjection` ADD `UIActionContext` varchar(50) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `Settlement` ADD `RejectionReason` varchar(1000) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Settlement` ADD `RenegotiationAmount` decimal(65,30) NULL;




INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250616091641_Settlement_AcceptanceNegotiateFlow', '8.0.10');

COMMIT;

