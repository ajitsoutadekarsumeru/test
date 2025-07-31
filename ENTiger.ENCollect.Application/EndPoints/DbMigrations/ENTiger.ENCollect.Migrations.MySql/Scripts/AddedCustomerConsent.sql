START TRANSACTION;

CREATE TABLE `CustomerConsent` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `AccountId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `UserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `RequestedVisitTime` datetime(6) NULL,
    `ConsentResponseTime` datetime(6) NULL,
    `ExpiryTime` datetime(6) NULL,
    `IsActive` tinyint(1) NULL,
    `Status` longtext CHARACTER SET utf8mb4 NOT NULL,
    `SecureToken` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CustomerConsent` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_CustomerConsent_ApplicationUser_UserId` FOREIGN KEY (`UserId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_CustomerConsent_LoanAccounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `LoanAccounts` (`Id`)
) CHARACTER SET=utf8mb4;

UPDATE `RoleAccountScope` SET `CreatedDate` = TIMESTAMP '2025-03-25 07:05:17', `LastModifiedDate` = TIMESTAMP '2025-03-25 07:05:17'
WHERE `Id` = '3a185d8db599c016d4caf7aa05af889f';
SELECT ROW_COUNT();


UPDATE `RoleAccountScope` SET `CreatedDate` = TIMESTAMP '2025-03-25 07:05:17', `LastModifiedDate` = TIMESTAMP '2025-03-25 07:05:17'
WHERE `Id` = '3a185d8db599d1ce3ace0b1c74528678';
SELECT ROW_COUNT();


UPDATE `RoleAccountScope` SET `CreatedDate` = TIMESTAMP '2025-03-25 07:05:17', `LastModifiedDate` = TIMESTAMP '2025-03-25 07:05:17'
WHERE `Id` = '3a185d8db599f4a83d63dec4faea8a98';
SELECT ROW_COUNT();


UPDATE `RoleAccountScope` SET `CreatedDate` = TIMESTAMP '2025-03-25 07:05:17', `LastModifiedDate` = TIMESTAMP '2025-03-25 07:05:17'
WHERE `Id` = '3a185d8db599f686a3b157eaeb799b2d';
SELECT ROW_COUNT();


CREATE INDEX `IX_CustomerConsent_AccountId` ON `CustomerConsent` (`AccountId`);

CREATE INDEX `IX_CustomerConsent_UserId` ON `CustomerConsent` (`UserId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250325070520_AddedCustomerConsent', '8.0.10');

COMMIT;

