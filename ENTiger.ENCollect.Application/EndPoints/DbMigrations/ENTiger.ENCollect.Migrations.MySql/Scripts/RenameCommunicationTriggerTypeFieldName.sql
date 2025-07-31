START TRANSACTION;

ALTER TABLE `CommunicationTrigger` DROP FOREIGN KEY `FK_CommunicationTrigger_CommunicationTriggerType_CommunicationT~`;

ALTER TABLE `CommunicationTriggerType` RENAME COLUMN `Type` TO `Name`;

UPDATE `CommunicationTrigger` SET `CommunicationTriggerTypeId` = ''
WHERE `CommunicationTriggerTypeId` IS NULL;
SELECT ROW_COUNT();


ALTER TABLE `CommunicationTrigger` MODIFY COLUMN `CommunicationTriggerTypeId` varchar(32) CHARACTER SET utf8mb4 NOT NULL;


ALTER TABLE `CommunicationTrigger` ADD CONSTRAINT `FK_CommunicationTrigger_CommunicationTriggerType_CommunicationT~` FOREIGN KEY (`CommunicationTriggerTypeId`) REFERENCES `CommunicationTriggerType` (`Id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250619113848_RenameCommunicationTriggerTypeFieldName', '8.0.10');

COMMIT;

