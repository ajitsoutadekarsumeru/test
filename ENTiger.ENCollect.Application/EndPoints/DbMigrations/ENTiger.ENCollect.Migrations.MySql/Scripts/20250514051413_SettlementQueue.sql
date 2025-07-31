 QSTART TRANSACTION;

ALTER TABLE `WaiverDetail` MODIFY COLUMN `ChargeType` varchar(50) CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `WaiverDetail` MODIFY COLUMN `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `Settlement` MODIFY COLUMN `Status` varchar(100) CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `Settlement` ADD `CustomId` varchar(50) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `Settlement` ADD `LatestHistoryId` varchar(32) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `InstallmentDetail` MODIFY COLUMN `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL;

CREATE TABLE `LevelDesignation` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Level` int NOT NULL,
    `DesignationId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_LevelDesignation` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_LevelDesignation_Designation_DesignationId` FOREIGN KEY (`DesignationId`) REFERENCES `Designation` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `SettlementStatusHistory` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `SettlementId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `FromStatus` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `ToStatus` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `ChangedBy` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `ChangedDate` datetime(6) NOT NULL,
    `Comment` varchar(500) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_SettlementStatusHistory` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_SettlementStatusHistory_Settlement_SettlementId` FOREIGN KEY (`SettlementId`) REFERENCES `Settlement` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `WorkflowAssignment` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `SettlementStatusHistoryId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `LevelDesignationId` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_WorkflowAssignment` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_WorkflowAssignment_LevelDesignation_LevelDesignationId` FOREIGN KEY (`LevelDesignationId`) REFERENCES `LevelDesignation` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_WorkflowAssignment_SettlementStatusHistory_SettlementStatusH~` FOREIGN KEY (`SettlementStatusHistoryId`) REFERENCES `SettlementStatusHistory` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AssignedUser` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `WorkflowAssignmentId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `ApplicationUserId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_AssignedUser` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AssignedUser_ApplicationUser_ApplicationUserId` FOREIGN KEY (`ApplicationUserId`) REFERENCES `ApplicationUser` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AssignedUser_WorkflowAssignment_WorkflowAssignmentId` FOREIGN KEY (`WorkflowAssignmentId`) REFERENCES `WorkflowAssignment` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;




CREATE INDEX `IX_Settlement_LatestHistoryId` ON `Settlement` (`LatestHistoryId`);

CREATE INDEX `IX_AssignedUser_ApplicationUserId` ON `AssignedUser` (`ApplicationUserId`);

CREATE INDEX `IX_AssignedUser_WorkflowAssignmentId` ON `AssignedUser` (`WorkflowAssignmentId`);

CREATE INDEX `IX_LevelDesignation_DesignationId` ON `LevelDesignation` (`DesignationId`);

CREATE INDEX `IX_SettlementStatusHistory_SettlementId` ON `SettlementStatusHistory` (`SettlementId`);

CREATE INDEX `IX_WorkflowAssignment_LevelDesignationId` ON `WorkflowAssignment` (`LevelDesignationId`);

CREATE INDEX `IX_WorkflowAssignment_SettlementStatusHistoryId` ON `WorkflowAssignment` (`SettlementStatusHistoryId`);

-- Indexes for optimizing MySettlement and MyQueue queries

-- For MySettlements summary (filter by creator and status)
CREATE INDEX `IX_Settlement_CreatedBy_Status` 
ON `Settlement` (`CreatedBy`, `Status`);

-- For MySettlements aging and status filtering
CREATE INDEX `IX_Settlement_Status_CreatedDate` 
ON `Settlement` (`Status`, `CreatedDate`);

-- For MyQueue join to latest history
CREATE INDEX `IX_Settlement_LatestHistoryId` 
ON `Settlement` (`LatestHistoryId`);

-- For AssignedUser lookups by workflow assignment and user
CREATE INDEX `IX_AssignedUser_WorkflowAssignmentId_UserId` 
ON `AssignedUser` (`WorkflowAssignmentId`, `ApplicationUserId`);

-- Optional: faster filtering directly by user
CREATE INDEX `IX_AssignedUser_UserId` 
ON `AssignedUser` (`UserId`);

ALTER TABLE `Settlement` ADD CONSTRAINT `FK_Settlement_SettlementStatusHistory_LatestHistoryId` FOREIGN KEY (`LatestHistoryId`) REFERENCES `SettlementStatusHistory` (`Id`) ON DELETE RESTRICT;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250514051413_SettlementQueue', '8.0.10');

COMMIT;

