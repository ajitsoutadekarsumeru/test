START TRANSACTION;

ALTER TABLE `DispositionCodeMaster` DROP FOREIGN KEY `FK_DispositionCodeMaster_DispositionGroupMaster_DispositionGrou~`;

ALTER TABLE `State` DROP FOREIGN KEY `FK_State_Regions_RegionId`;

ALTER TABLE `GeoMaster` DROP COLUMN `Zone`;

ALTER TABLE `State` MODIFY COLUMN `SecondaryLanguage` varchar(100) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `State` MODIFY COLUMN `RegionId` varchar(32) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `State` MODIFY COLUMN `PrimaryLanguage` varchar(100) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `State` MODIFY COLUMN `NickName` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `State` MODIFY COLUMN `Name` longtext CHARACTER SET utf8mb4 NULL;

ALTER TABLE `SegmentationAdvanceFilterMasters` ADD `FieldId` varchar(200) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `SegmentationAdvanceFilterMasters` ADD `FieldName` varchar(200) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `SegmentationAdvanceFilterMasters` ADD `Operator` varchar(100) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `LoanAccounts` ADD `LastUploadedDate` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00';

ALTER TABLE `LoanAccounts` ADD `TreatmentId` varchar(32) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Feedback` MODIFY COLUMN `AssignTo` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Feedback` ADD `Place_of_visit` varchar(500) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Feedback` ADD `ThirdPartyContactNo` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Feedback` ADD `ThirdPartyContactPerson` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `DispositionCodeMaster` MODIFY COLUMN `ShortDescription` longtext CHARACTER SET utf8mb4 NULL;

ALTER TABLE `DispositionCodeMaster` MODIFY COLUMN `Permissibleforfieldagent` longtext CHARACTER SET utf8mb4 NULL;

ALTER TABLE `DispositionCodeMaster` MODIFY COLUMN `LongDescription` longtext CHARACTER SET utf8mb4 NULL;

ALTER TABLE `DispositionCodeMaster` MODIFY COLUMN `DispositionGroupMasterId` varchar(32) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `DispositionCodeMaster` MODIFY COLUMN `DispositionCode` longtext CHARACTER SET utf8mb4 NULL;

ALTER TABLE `DispositionCodeMaster` MODIFY COLUMN `DispositionAccess` varchar(150) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `ApplicationUser` ADD `Age` int NULL;

ALTER TABLE `ApplicationUser` ADD `DRAStatus` varchar(20) CHARACTER SET utf8mb4 NULL;

CREATE TABLE `DailyActivityDetail` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `ActivityTs` datetime(6) NULL,
    `ActivityType` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Location` varchar(300) CHARACTER SET utf8mb4 NULL,
    `ActivityWeekDay` varchar(50) CHARACTER SET utf8mb4 NULL,
    `ActivityDayNumber` int NULL,
    `ActivityMonth` int NULL,
    `ActivityYear` int NULL,
    `State` varchar(250) CHARACTER SET utf8mb4 NULL,
    `Lat` double NULL,
    `Department` varchar(250) CHARACTER SET utf8mb4 NULL,
    `Mobile` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Name` varchar(200) CHARACTER SET utf8mb4 NULL,
    `StaffOrAgent` tinyint(1) NULL,
    `EmpanalmentStatus` varchar(250) CHARACTER SET utf8mb4 NULL,
    `Designation` varchar(250) CHARACTER SET utf8mb4 NULL,
    `Agency` varchar(200) CHARACTER SET utf8mb4 NULL,
    `Branch` varchar(200) CHARACTER SET utf8mb4 NULL,
    `UserId` varchar(200) CHARACTER SET utf8mb4 NULL,
    `EmailId` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Long` double NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_DailyActivityDetail` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `LoginDetailsHistory` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `UserId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `Email` varchar(200) CHARACTER SET utf8mb4 NULL,
    `LoginStatus` varchar(500) CHARACTER SET utf8mb4 NULL,
    `LoginInputJson` varchar(2000) CHARACTER SET utf8mb4 NULL,
    `Remarks` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_LoginDetailsHistory` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_LoginDetailsHistory_ApplicationUser_UserId` FOREIGN KEY (`UserId`) REFERENCES `ApplicationUser` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TrailGapDownload` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `InputJson` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `FileName` varchar(250) CHARACTER SET utf8mb4 NULL,
    `FilePath` varchar(500) CHARACTER SET utf8mb4 NULL,
    `Status` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CustomId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TrailGapDownload` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TrailIntensityDownload` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `InputJson` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `FileName` varchar(250) CHARACTER SET utf8mb4 NULL,
    `FilePath` varchar(500) CHARACTER SET utf8mb4 NULL,
    `Status` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CustomId` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_TrailIntensityDownload` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_LoanAccounts_TreatmentId` ON `LoanAccounts` (`TreatmentId`);

CREATE INDEX `IX_LoginDetailsHistory_UserId` ON `LoginDetailsHistory` (`UserId`);

ALTER TABLE `DispositionCodeMaster` ADD CONSTRAINT `FK_DispositionCodeMaster_DispositionGroupMaster_DispositionGrou~` FOREIGN KEY (`DispositionGroupMasterId`) REFERENCES `DispositionGroupMaster` (`Id`);

ALTER TABLE `LoanAccounts` ADD CONSTRAINT `FK_LoanAccounts_Treatment_TreatmentId` FOREIGN KEY (`TreatmentId`) REFERENCES `Treatment` (`Id`);

ALTER TABLE `State` ADD CONSTRAINT `FK_State_Regions_RegionId` FOREIGN KEY (`RegionId`) REFERENCES `Regions` (`Id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240926074507_AddedLoginDetailsHistory', '8.0.6');

COMMIT;

