START TRANSACTION;

DROP TABLE `TriggerTemplateMapping`;

ALTER TABLE `TriggerType` ADD `EntryPoint` varchar(150) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `TriggerType` ADD `OffsetBasis` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `TriggerType` ADD `OffsetDirection` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `TriggerType` ADD `RequiresDaysOffset` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `CommunicationTemplate` ADD `EntryPoint` varchar(150) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `CommunicationTemplate` ADD `RecipientType` varchar(150) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

CREATE TABLE `TriggerDeliverySpec` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CommunicationTriggerId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CommunicationTemplateId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TriggerDeliverySpec` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TriggerDeliverySpec_CommunicationTemplate_CommunicationTempl~` FOREIGN KEY (`CommunicationTemplateId`) REFERENCES `CommunicationTemplate` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_TriggerDeliverySpec_CommunicationTrigger_CommunicationTrigge~` FOREIGN KEY (`CommunicationTriggerId`) REFERENCES `CommunicationTrigger` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_TriggerDeliverySpec_CommunicationTemplateId` ON `TriggerDeliverySpec` (`CommunicationTemplateId`);

CREATE INDEX `IX_TriggerDeliverySpec_CommunicationTriggerId` ON `TriggerDeliverySpec` (`CommunicationTriggerId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250715080432_Communication_EntryPoint', '8.0.10');

COMMIT;

