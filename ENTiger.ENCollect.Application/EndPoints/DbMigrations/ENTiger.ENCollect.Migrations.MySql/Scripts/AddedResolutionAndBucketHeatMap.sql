START TRANSACTION;


CREATE TABLE `Resolutions` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(100) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Resolutions` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `BucketHeatMapConfig` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `BucketId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `Resolutions` varchar(32) CHARACTER SET utf8mb4 NULL,
    `ResolutionMasterId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `RangeFrom` int NOT NULL,
    `RangeTo` int NOT NULL,
    `Color` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_BucketHeatMapConfig` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_BucketHeatMapConfig_Buckets_BucketId` FOREIGN KEY (`BucketId`) REFERENCES `Buckets` (`Id`),
    CONSTRAINT `FK_BucketHeatMapConfig_Resolutions_ResolutionMasterId` FOREIGN KEY (`ResolutionMasterId`) REFERENCES `Resolutions` (`Id`)
) CHARACTER SET=utf8mb4;


CREATE INDEX `IX_BucketHeatMapConfig_BucketId` ON `BucketHeatMapConfig` (`BucketId`);

CREATE INDEX `IX_BucketHeatMapConfig_ResolutionMasterId` ON `BucketHeatMapConfig` (`ResolutionMasterId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250617100533_AddedResolutionAndBucketHeatMap', '8.0.10');

COMMIT;

