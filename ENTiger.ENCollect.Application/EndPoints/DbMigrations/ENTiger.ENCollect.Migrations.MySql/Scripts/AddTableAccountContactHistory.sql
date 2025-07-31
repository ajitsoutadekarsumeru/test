START TRANSACTION;

CREATE TABLE `AccountContactHistory` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `ContactValue` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `Latitude` decimal(9,6) NULL,
    `Longitude` decimal(9,6) NULL,
    `ContactSource` int NOT NULL,
    `ContactType` int NOT NULL,
    `AccountId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AccountContactHistory` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AccountContactHistory_LoanAccounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `LoanAccounts` (`Id`)
) CHARACTER SET=utf8mb4;




CREATE INDEX `IX_AccountContactHistory_AccountId` ON `AccountContactHistory` (`AccountId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250714095033_AddTableAccountContactHistory', '8.0.10');

COMMIT;

