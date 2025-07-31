START TRANSACTION;

ALTER TABLE `CommunicationTrigger` DROP FOREIGN KEY `FK_CommunicationTrigger_CommunicationTriggerType_CommunicationT~`;

DROP TABLE `CommunicationTriggerType`;

ALTER TABLE `CommunicationTrigger` DROP INDEX `IX_CommunicationTrigger_CommunicationTriggerTypeId`;

ALTER TABLE `CommunicationTrigger` DROP COLUMN `CommunicationTriggerTypeId`;

CREATE TABLE `TriggerType` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `IsActive` tinyint(1) NOT NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TriggerType` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-06-19 14:01:40', `LastModifiedDate` = TIMESTAMP '2025-06-19 14:01:40'
WHERE `Id` = '3a185d8db599c016d4caf7aa05af889f';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-06-19 14:01:40', `LastModifiedDate` = TIMESTAMP '2025-06-19 14:01:40'
WHERE `Id` = '3a185d8db599d1ce3ace0b1c74528678';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-06-19 14:01:40', `LastModifiedDate` = TIMESTAMP '2025-06-19 14:01:40'
WHERE `Id` = '3a185d8db599f4a83d63dec4faea8a98';
SELECT ROW_COUNT();


UPDATE `AccountScopeConfiguration` SET `CreatedDate` = TIMESTAMP '2025-06-19 14:01:40', `LastModifiedDate` = TIMESTAMP '2025-06-19 14:01:40'
WHERE `Id` = '3a185d8db599f686a3b157eaeb799b2d';
SELECT ROW_COUNT();

ALTER TABLE `CommunicationTrigger` ADD `TriggerTypeId` varchar(32) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

CREATE INDEX `IX_CommunicationTrigger_TriggerTypeId` ON `CommunicationTrigger` (`TriggerTypeId`);

ALTER TABLE `CommunicationTrigger` ADD CONSTRAINT `FK_CommunicationTrigger_TriggerType_TriggerTypeId` FOREIGN KEY (`TriggerTypeId`) REFERENCES `TriggerType` (`Id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250619140144_AddedCommunicationTriggers', '8.0.10');

COMMIT;

