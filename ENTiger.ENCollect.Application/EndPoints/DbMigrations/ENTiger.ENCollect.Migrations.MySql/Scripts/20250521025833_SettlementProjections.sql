START TRANSACTION;

ALTER TABLE `WorkflowAssignment` DROP FOREIGN KEY `FK_WorkflowAssignment_LevelDesignation_LevelDesignationId`;



ALTER TABLE `WorkflowAssignment` MODIFY COLUMN `LevelDesignationId` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Settlement` ADD `SettlementDate` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00';

ALTER TABLE `Designation` MODIFY COLUMN `Level` int NOT NULL DEFAULT 0;

CREATE TABLE `SettlementDocument` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `SettlementId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `DocumentType` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `DocumentName` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `FileName` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `UploadedOn` datetime(6) NOT NULL,
    CONSTRAINT `PK_SettlementDocument` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_SettlementDocument_Settlement_SettlementId` FOREIGN KEY (`SettlementId`) REFERENCES `Settlement` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SettlementQueueProjection` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `WorkflowName` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `WorkflowInstanceId` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `SettlementId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `StepIndex` int NOT NULL,
    `ApplicationUserId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_SettlementQueueProjection` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_SettlementQueueProjection_ApplicationUser_ApplicationUserId` FOREIGN KEY (`ApplicationUserId`) REFERENCES `ApplicationUser` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_SettlementQueueProjection_Settlement_SettlementId` FOREIGN KEY (`SettlementId`) REFERENCES `Settlement` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserLevelProjection` (
    `ApplicationUserId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `MaxLevel` int NOT NULL,
    `Id` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_UserLevelProjection` PRIMARY KEY (`ApplicationUserId`),
    CONSTRAINT `FK_UserLevelProjection_ApplicationUser_ApplicationUserId` FOREIGN KEY (`ApplicationUserId`) REFERENCES `ApplicationUser` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_SettlementDocument_SettlementId` ON `SettlementDocument` (`SettlementId`);

CREATE INDEX `IX_SettlementQueueProjection_ApplicationUserId` ON `SettlementQueueProjection` (`ApplicationUserId`);

CREATE INDEX `IX_SettlementQueueProjection_SettlementId` ON `SettlementQueueProjection` (`SettlementId`);

ALTER TABLE `WorkflowAssignment` ADD CONSTRAINT `FK_WorkflowAssignment_LevelDesignation_LevelDesignationId` FOREIGN KEY (`LevelDesignationId`) REFERENCES `LevelDesignation` (`Id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250521025833_SettlementProjections', '8.0.10');

COMMIT;

