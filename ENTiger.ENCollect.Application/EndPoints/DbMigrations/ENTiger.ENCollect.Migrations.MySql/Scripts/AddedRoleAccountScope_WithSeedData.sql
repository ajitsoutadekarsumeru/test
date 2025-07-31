START TRANSACTION;

CREATE TABLE `RoleAccountScope` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `AccountabilityTypeId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Scope` longtext CHARACTER SET utf8mb4 NOT NULL,
    `ScopeLevel` int NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_RoleAccountScope` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_RoleAccountScope_AccountabilityTypes_AccountabilityTypeId` FOREIGN KEY (`AccountabilityTypeId`) REFERENCES `AccountabilityTypes` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

INSERT INTO `RoleAccountScope` (`Id`, `AccountabilityTypeId`, `CreatedBy`, `CreatedDate`, `IsDeleted`, `LastModifiedBy`, `LastModifiedDate`, `Scope`, `ScopeLevel`)
VALUES ('3a185d8db599c016d4caf7aa05af889f', 'AgencyToFrontEndExternalFOS', NULL, TIMESTAMP '2025-02-28 07:17:03', FALSE, NULL, TIMESTAMP '2025-02-28 07:17:03', 'all', 1),
('3a185d8db599d1ce3ace0b1c74528678', 'BankToFrontEndInternalFOS', NULL, TIMESTAMP '2025-02-28 07:17:03', FALSE, NULL, TIMESTAMP '2025-02-28 07:17:03', 'all', 1),
('3a185d8db599f4a83d63dec4faea8a98', 'AgencyToFrontEndExternalTC', NULL, TIMESTAMP '2025-02-28 07:17:03', FALSE, NULL, TIMESTAMP '2025-02-28 07:17:03', 'all', 1),
('3a185d8db599f686a3b157eaeb799b2d', 'BankToFrontEndInternalTC', NULL, TIMESTAMP '2025-02-28 07:17:03', FALSE, NULL, TIMESTAMP '2025-02-28 07:17:03', 'all', 1);

CREATE INDEX `IX_RoleAccountScope_AccountabilityTypeId` ON `RoleAccountScope` (`AccountabilityTypeId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250228071706_AddedRoleAccountScope_WithSeedData', '8.0.10');

COMMIT;

