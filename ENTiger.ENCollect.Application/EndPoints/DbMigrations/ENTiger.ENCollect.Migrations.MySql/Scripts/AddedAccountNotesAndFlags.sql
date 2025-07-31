START TRANSACTION;

CREATE TABLE `LoanAccountFlag` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(50) CHARACTER SET utf8mb4 NULL,
    `IsActive` tinyint(1) NOT NULL,
    `LoanAccountId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_LoanAccountFlag` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_LoanAccountFlag_LoanAccounts_LoanAccountId` FOREIGN KEY (`LoanAccountId`) REFERENCES `LoanAccounts` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `LoanAccountNote` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Code` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(500) CHARACTER SET utf8mb4 NULL,
    `LoanAccountId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_LoanAccountNote` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_LoanAccountNote_LoanAccounts_LoanAccountId` FOREIGN KEY (`LoanAccountId`) REFERENCES `LoanAccounts` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_LoanAccountFlag_LoanAccountId` ON `LoanAccountFlag` (`LoanAccountId`);

CREATE INDEX `IX_LoanAccountNote_LoanAccountId` ON `LoanAccountNote` (`LoanAccountId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241016065509_AddedAccountNotesAndFlags', '8.0.6');

COMMIT;

