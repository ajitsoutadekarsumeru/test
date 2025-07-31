START TRANSACTION;

CREATE TABLE `HierarchyLevel` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `Order` int NOT NULL,
    `Type` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_HierarchyLevel` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `HierarchyMaster` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Item` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `LevelId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `ParentId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_HierarchyMaster` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_HierarchyMaster_HierarchyLevel_LevelId` FOREIGN KEY (`LevelId`) REFERENCES `HierarchyLevel` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_HierarchyMaster_HierarchyMaster_ParentId` FOREIGN KEY (`ParentId`) REFERENCES `HierarchyMaster` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_HierarchyMaster_LevelId` ON `HierarchyMaster` (`LevelId`);

CREATE INDEX `IX_HierarchyMaster_ParentId` ON `HierarchyMaster` (`ParentId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250612095344_AddedHierarchy', '8.0.10');

COMMIT;

