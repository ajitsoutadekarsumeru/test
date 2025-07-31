START TRANSACTION;

ALTER TABLE `Collections` ADD `Status` varchar(50) CHARACTER SET utf8mb4 NULL;

CREATE TABLE `LoanAccountsProjection` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Year` int NULL,
    `Month` int NULL,
    `LoanAccountId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `TotalCollectionAmount` decimal(65,30) NULL,
    `TotalCollectionCount` int NULL,
    `LastCollectionDate` datetime(6) NULL,
    `TotalTrailCount` int NULL,
    `TotalPTPCount` int NULL,
    `TotalBPTPCount` int NULL,
    `CurrentDispositionGroup` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CurrentDispositionCode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CurrentDispositionDate` datetime(6) NULL,
    `CurrentNextActionDate` datetime(6) NULL,
    `PreviousDispositionGroup` varchar(50) CHARACTER SET utf8mb4 NULL,
    `PreviousDispositionCode` varchar(50) CHARACTER SET utf8mb4 NULL,
    `PreviousDispositionDate` datetime(6) NULL,
    `PreviousNextActionDate` datetime(6) NULL,
    `Version` int NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_LoanAccountsProjection` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_LoanAccountsProjection_LoanAccounts_LoanAccountId` FOREIGN KEY (`LoanAccountId`) REFERENCES `LoanAccounts` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_LoanAccountsProjection_LoanAccountId` ON `LoanAccountsProjection` (`LoanAccountId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250523132329_LoanaccountsProjectiontableAndStatusInCollectionAdded', '8.0.10');

COMMIT;

