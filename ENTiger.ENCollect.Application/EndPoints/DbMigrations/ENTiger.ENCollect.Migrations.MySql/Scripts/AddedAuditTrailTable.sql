START TRANSACTION;

ALTER TABLE `LoanAccountJSON` DROP INDEX `IX_LoanAccountJSON_AccountId`;

ALTER TABLE `Feedback` ADD `DeliquencyReason` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `ApplicationUser` ADD `IsPolicyAccepted` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `ApplicationUser` ADD `PolicyAcceptedDate` datetime(6) NULL;

CREATE TABLE `AuditTrailRecord` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `EntityId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `EntityType` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Operation` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `DiffJson` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AuditTrailRecord` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

UPDATE `AgencyType` SET `SubType` = 'tele calling'
WHERE `Id` = '27d4c2e0ce1a438cb44cd7fb8ed552b9';
SELECT ROW_COUNT();


UPDATE `AgencyType` SET `SubType` = 'field agent'
WHERE `Id` = 'ff379ce22f7b4aca9e74d0dadccb3739';
SELECT ROW_COUNT();


CREATE UNIQUE INDEX `IX_LoanAccountJSON_AccountId` ON `LoanAccountJSON` (`AccountId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250123121244_AddedAuditTrailTable', '8.0.10');

COMMIT;

