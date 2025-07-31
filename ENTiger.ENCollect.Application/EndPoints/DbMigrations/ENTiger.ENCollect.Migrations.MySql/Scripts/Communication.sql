START TRANSACTION;

ALTER TABLE `CommunicationTemplate` DROP FOREIGN KEY `FK_CommunicationTemplate_CommunicationTemplateDetail_Communicat~`;

ALTER TABLE `CommunicationTemplate` DROP FOREIGN KEY `FK_CommunicationTemplate_CommunicationTemplateWorkflowState_Com~`;

ALTER TABLE `CommunicationTemplateDetail` DROP INDEX `IX_CommunicationTemplateDetail_Name`;

ALTER TABLE `CommunicationTemplate` DROP INDEX `IX_CommunicationTemplate_CommunicationTemplateDetailId`;

ALTER TABLE `CommunicationTemplate` DROP INDEX `IX_CommunicationTemplate_CommunicationTemplateWorkflowStateId`;

ALTER TABLE `CommunicationTemplateDetail` DROP COLUMN `AddressTo`;

ALTER TABLE `CommunicationTemplateDetail` DROP COLUMN `Name`;

ALTER TABLE `CommunicationTemplateDetail` DROP COLUMN `Salutation`;

ALTER TABLE `CommunicationTemplateDetail` DROP COLUMN `Signature`;

ALTER TABLE `CommunicationTemplate` DROP COLUMN `CommunicationTemplateDetailId`;

ALTER TABLE `CommunicationTemplate` DROP COLUMN `CommunicationTemplateWorkflowStateId`;

ALTER TABLE `CommunicationTemplate` DROP COLUMN `Language`;

ALTER TABLE `CommunicationTemplate` DROP COLUMN `Recipient`;

ALTER TABLE `CommunicationTemplate` DROP COLUMN `WATemplateId`;

ALTER TABLE `CommunicationTemplate` RENAME COLUMN `IsDisabled` TO `IsActive`;

ALTER TABLE `CommunicationTemplateDetail` MODIFY COLUMN `Subject` varchar(200) CHARACTER SET utf8mb4 NULL;

UPDATE `CommunicationTemplateDetail` SET `Body` = ''
WHERE `Body` IS NULL;
SELECT ROW_COUNT();


ALTER TABLE `CommunicationTemplateDetail` MODIFY COLUMN `Body` varchar(5000) CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `CommunicationTemplateDetail` ADD `CommunicationTemplateId` varchar(32) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `CommunicationTemplateDetail` ADD `Language` varchar(50) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `CommunicationTemplateDetail` ADD `Version` int NOT NULL DEFAULT 0;

ALTER TABLE `CommunicationTemplate` MODIFY COLUMN `TemplateType` int NOT NULL DEFAULT 0;

ALTER TABLE `CommunicationTemplate` ADD `Name` varchar(50) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `CommunicationTemplate` ADD `Version` int NOT NULL DEFAULT 0;

CREATE TABLE `CommunicationTrigger` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `ConditionType` int NOT NULL,
    `DaysOffset` int NULL,
    `IsActive` tinyint(1) NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
    `MaximumOccurences` int NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CommunicationTrigger` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;


CREATE TABLE `CommunicationTriggerTemplate` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CommunicationTriggerId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CommunicationTemplateId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CommunicationTriggerTemplate` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_CommunicationTriggerTemplate_CommunicationTemplate_Communica~` FOREIGN KEY (`CommunicationTemplateId`) REFERENCES `CommunicationTemplate` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_CommunicationTriggerTemplate_CommunicationTrigger_Communicat~` FOREIGN KEY (`CommunicationTriggerId`) REFERENCES `CommunicationTrigger` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;


CREATE INDEX `IX_CommunicationTemplateDetail_CommunicationTemplateId` ON `CommunicationTemplateDetail` (`CommunicationTemplateId`);

CREATE INDEX `IX_CommunicationTemplate_Name` ON `CommunicationTemplate` (`Name`);

CREATE INDEX `IX_CommunicationTriggerTemplate_CommunicationTemplateId` ON `CommunicationTriggerTemplate` (`CommunicationTemplateId`);

CREATE INDEX `IX_CommunicationTriggerTemplate_CommunicationTriggerId` ON `CommunicationTriggerTemplate` (`CommunicationTriggerId`);

ALTER TABLE `CommunicationTemplateDetail` ADD CONSTRAINT `FK_CommunicationTemplateDetail_CommunicationTemplate_Communicat~` FOREIGN KEY (`CommunicationTemplateId`) REFERENCES `CommunicationTemplate` (`Id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250612061130_Communication', '8.0.10');

COMMIT;

