START TRANSACTION;

ALTER TABLE `ReceiptWorkflowState` ADD `Remarks` varchar(500) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `PayInSlipWorkflowState` ADD `Remarks` varchar(500) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `CompanyUserWorkflowState` ADD `Remarks` varchar(500) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `CommunicationTemplateWorkflowState` ADD `Remarks` varchar(500) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `CollectionWorkflowState` ADD `Remarks` varchar(500) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `CollectionBatchWorkflowState` ADD `Remarks` varchar(500) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `AgencyWorkflowState` ADD `Remarks` varchar(500) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `AgencyUserWorkflowState` ADD `Remarks` varchar(500) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `AgencyUserIdentification` MODIFY COLUMN `Value` varchar(500) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `AgencyUserIdentification` MODIFY COLUMN `Status` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `AgencyUserIdentification` MODIFY COLUMN `Remarks` varchar(200) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `AgencyIdentification` MODIFY COLUMN `Value` varchar(500) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `AgencyIdentification` MODIFY COLUMN `Status` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `AgencyIdentification` MODIFY COLUMN `Remarks` varchar(200) CHARACTER SET utf8mb4 NULL;

CREATE TABLE `UsersCreateFile` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CustomId` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `UploadType` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `FileName` varchar(250) CHARACTER SET utf8mb4 NOT NULL,
    `FilePath` varchar(250) CHARACTER SET utf8mb4 NULL,
    `Status` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `UploadedDate` datetime(6) NOT NULL,
    `ProcessedDateTime` datetime(6) NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_UsersCreateFile` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241118115016_RemarksAndUserCreateFile', '8.0.6');

COMMIT;

