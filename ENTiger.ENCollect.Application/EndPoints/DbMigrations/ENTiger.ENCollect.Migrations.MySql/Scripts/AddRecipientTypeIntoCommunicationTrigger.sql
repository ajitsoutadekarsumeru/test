START TRANSACTION;

ALTER TABLE `TriggerType` ADD `CustomName` varchar(50) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `CommunicationTrigger` ADD `RecipientType` varchar(150) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

CREATE TABLE `TriggerAccountQueueProjection` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `RunId` longtext CHARACTER SET utf8mb4 NOT NULL,
    `TriggerId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `AccountId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TriggerAccountQueueProjection` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TriggerAccountQueueProjection_CommunicationTrigger_TriggerId` FOREIGN KEY (`TriggerId`) REFERENCES `CommunicationTrigger` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_TriggerAccountQueueProjection_LoanAccounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `LoanAccounts` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_TriggerAccountQueueProjection_AccountId` ON `TriggerAccountQueueProjection` (`AccountId`);

CREATE INDEX `IX_TriggerAccountQueueProjection_TriggerId` ON `TriggerAccountQueueProjection` (`TriggerId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250716102508_AddRecipientTypeIntoCommunicationTrigger', '8.0.10');

COMMIT;

