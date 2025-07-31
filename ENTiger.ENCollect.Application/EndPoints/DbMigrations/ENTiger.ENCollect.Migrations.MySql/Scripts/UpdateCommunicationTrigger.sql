START TRANSACTION;

ALTER TABLE `CommunicationTrigger` DROP COLUMN `ConditionType`;

ALTER TABLE `CommunicationTrigger` MODIFY COLUMN `DaysOffset` int NOT NULL DEFAULT 0;

ALTER TABLE `CommunicationTrigger` ADD `TriggerTypeId` varchar(32) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `CommunicationTemplate` MODIFY COLUMN `TemplateType` varchar(255) CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `CommunicationTemplate` ADD `IsAllowAccessFromAccountDetails` tinyint(1) NULL;




CREATE INDEX `IX_CommunicationTrigger_TriggerTypeId` ON `CommunicationTrigger` (`TriggerTypeId`);

ALTER TABLE `CommunicationTrigger` ADD CONSTRAINT `FK_CommunicationTrigger_CategoryItem_TriggerTypeId` FOREIGN KEY (`TriggerTypeId`) REFERENCES `CategoryItem` (`Id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250613083531_UpdateCommunicationTrigger', '8.0.10');

COMMIT;

