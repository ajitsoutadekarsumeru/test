START TRANSACTION;

DROP TABLE `RoleAccountScope`;

CREATE TABLE `AccountScopeConfiguration` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `AccountabilityTypeId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Scope` longtext CHARACTER SET utf8mb4 NOT NULL,
    `ScopeLevel` int NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AccountScopeConfiguration` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AccountScopeConfiguration_AccountabilityTypes_Accountability~` FOREIGN KEY (`AccountabilityTypeId`) REFERENCES `AccountabilityTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

INSERT INTO `AccountScopeConfiguration` (`Id`, `AccountabilityTypeId`, `CreatedBy`, `CreatedDate`, `IsDeleted`, `LastModifiedBy`, `LastModifiedDate`, `Scope`, `ScopeLevel`)
VALUES ('3a185d8db599c016d4caf7aa05af889f', 'AgencyToFrontEndExternalFOS', NULL, TIMESTAMP '2025-04-03 09:45:12', FALSE, NULL, TIMESTAMP '2025-04-03 09:45:12', 'all', 1),
('3a185d8db599d1ce3ace0b1c74528678', 'BankToFrontEndInternalFOS', NULL, TIMESTAMP '2025-04-03 09:45:12', FALSE, NULL, TIMESTAMP '2025-04-03 09:45:12', 'all', 1),
('3a185d8db599f4a83d63dec4faea8a98', 'AgencyToFrontEndExternalTC', NULL, TIMESTAMP '2025-04-03 09:45:12', FALSE, NULL, TIMESTAMP '2025-04-03 09:45:12', 'all', 1),
('3a185d8db599f686a3b157eaeb799b2d', 'BankToFrontEndInternalTC', NULL, TIMESTAMP '2025-04-03 09:45:12', FALSE, NULL, TIMESTAMP '2025-04-03 09:45:12', 'all', 1);

CREATE INDEX `IX_AccountScopeConfiguration_AccountabilityTypeId` ON `AccountScopeConfiguration` (`AccountabilityTypeId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250403094515_AddedAccountScopeConfiguration', '8.0.10');

COMMIT;

