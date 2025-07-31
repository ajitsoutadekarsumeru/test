START TRANSACTION;

ALTER TABLE `CommunicationTrigger` DROP FOREIGN KEY `FK_CommunicationTrigger_CategoryItem_TriggerTypeId`;

DROP TABLE `CommunicationTemplateWorkflowState`;

DROP TABLE `CommunicationTriggerTemplate`;

ALTER TABLE `CommunicationTrigger` DROP INDEX `IX_CommunicationTrigger_TriggerTypeId`;

ALTER TABLE `CommunicationTrigger` DROP COLUMN `MaximumOccurences`;

ALTER TABLE `CommunicationTrigger` DROP COLUMN `TriggerTypeId`;

ALTER TABLE `CommunicationTemplate` DROP COLUMN `IsAllowAccessFromAccountDetails`;

ALTER TABLE `CommunicationTrigger` MODIFY COLUMN `Description` varchar(1000) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `CommunicationTrigger` ADD `CommunicationTriggerTypeId` varchar(32) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `CommunicationTemplate` ADD `IsAvailableInAccountDetails` tinyint(1) NOT NULL DEFAULT FALSE;

CREATE TABLE `CommunicationTriggerType` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Type` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `IsActive` tinyint(1) NOT NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CommunicationTriggerType` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TriggerTemplateMapping` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CommunicationTriggerId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CommunicationTemplateId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TriggerTemplateMapping` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TriggerTemplateMapping_CommunicationTemplate_CommunicationTe~` FOREIGN KEY (`CommunicationTemplateId`) REFERENCES `CommunicationTemplate` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_TriggerTemplateMapping_CommunicationTrigger_CommunicationTri~` FOREIGN KEY (`CommunicationTriggerId`) REFERENCES `CommunicationTrigger` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_CommunicationTrigger_CommunicationTriggerTypeId` ON `CommunicationTrigger` (`CommunicationTriggerTypeId`);

CREATE INDEX `IX_TriggerTemplateMapping_CommunicationTemplateId` ON `TriggerTemplateMapping` (`CommunicationTemplateId`);

CREATE INDEX `IX_TriggerTemplateMapping_CommunicationTriggerId` ON `TriggerTemplateMapping` (`CommunicationTriggerId`);

ALTER TABLE `CommunicationTrigger` ADD CONSTRAINT `FK_CommunicationTrigger_CommunicationTriggerType_CommunicationT~` FOREIGN KEY (`CommunicationTriggerTypeId`) REFERENCES `CommunicationTriggerType` (`Id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250618125645_Communication_TriggerTemplateMapping', '8.0.10');

COMMIT;

