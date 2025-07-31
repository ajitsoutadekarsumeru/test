START TRANSACTION;

CREATE TABLE `AccountGeoMap` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `AccountId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `HierarchyId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `HierarchyLevel` int NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AccountGeoMap` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AccountGeoMap_HierarchyMaster_HierarchyId` FOREIGN KEY (`HierarchyId`) REFERENCES `HierarchyMaster` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AccountGeoMap_LoanAccounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `LoanAccounts` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `AccountProductMap` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `AccountId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `HierarchyId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `HierarchyLevel` int NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_AccountProductMap` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AccountProductMap_HierarchyMaster_HierarchyId` FOREIGN KEY (`HierarchyId`) REFERENCES `HierarchyMaster` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AccountProductMap_LoanAccounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `LoanAccounts` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;


CREATE INDEX `IX_AccountGeoMap_AccountId` ON `AccountGeoMap` (`AccountId`);

CREATE INDEX `IX_AccountGeoMap_HierarchyId` ON `AccountGeoMap` (`HierarchyId`);

CREATE INDEX `IX_AccountProductMap_AccountId` ON `AccountProductMap` (`AccountId`);

CREATE INDEX `IX_AccountProductMap_HierarchyId` ON `AccountProductMap` (`HierarchyId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250625111448_AccountProductMappingAdded', '8.0.10');

COMMIT;

