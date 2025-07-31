START TRANSACTION;

CREATE TABLE `CollectionUploadFile` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(200) CHARACTER SET utf8mb4 NULL,
    `FileName` varchar(250) CHARACTER SET utf8mb4 NULL,
    `FilePath` varchar(250) CHARACTER SET utf8mb4 NULL,
    `FileUploadedDate` datetime(6) NOT NULL,
    `FileProcessedDateTime` datetime(6) NOT NULL,
    `Status` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CustomId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `UploadType` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CollectionUploadFile` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

UPDATE `AgencyType` SET `SubType` = 'field agents'
WHERE `Id` = 'ff379ce22f7b4aca9e74d0dadccb3739';
SELECT ROW_COUNT();


INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250224083301_CollectionUploadFile', '8.0.10');

COMMIT;

