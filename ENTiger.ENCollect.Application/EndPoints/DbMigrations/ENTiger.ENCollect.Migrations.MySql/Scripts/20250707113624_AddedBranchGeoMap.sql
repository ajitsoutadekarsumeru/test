START TRANSACTION;

DROP PROCEDURE IF EXISTS `POMELO_AFTER_ADD_PRIMARY_KEY`;
DELIMITER //
CREATE PROCEDURE `POMELO_AFTER_ADD_PRIMARY_KEY`(IN `SCHEMA_NAME_ARGUMENT` VARCHAR(255), IN `TABLE_NAME_ARGUMENT` VARCHAR(255), IN `COLUMN_NAME_ARGUMENT` VARCHAR(255))
BEGIN
	DECLARE HAS_AUTO_INCREMENT_ID INT(11);
	DECLARE PRIMARY_KEY_COLUMN_NAME VARCHAR(255);
	DECLARE PRIMARY_KEY_TYPE VARCHAR(255);
	DECLARE SQL_EXP VARCHAR(1000);
	SELECT COUNT(*)
		INTO HAS_AUTO_INCREMENT_ID
		FROM `information_schema`.`COLUMNS`
		WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
			AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
			AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
			AND `COLUMN_TYPE` LIKE '%int%'
			AND `COLUMN_KEY` = 'PRI';
	IF HAS_AUTO_INCREMENT_ID THEN
		SELECT `COLUMN_TYPE`
			INTO PRIMARY_KEY_TYPE
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
				AND `COLUMN_TYPE` LIKE '%int%'
				AND `COLUMN_KEY` = 'PRI';
		SELECT `COLUMN_NAME`
			INTO PRIMARY_KEY_COLUMN_NAME
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
				AND `COLUMN_TYPE` LIKE '%int%'
				AND `COLUMN_KEY` = 'PRI';
		SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL AUTO_INCREMENT;');
		SET @SQL_EXP = SQL_EXP;
		PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
		EXECUTE SQL_EXP_EXECUTE;
		DEALLOCATE PREPARE SQL_EXP_EXECUTE;
	END IF;
END //
DELIMITER ;

ALTER TABLE `CommunicationTrigger` DROP FOREIGN KEY `FK_CommunicationTrigger_CategoryItem_TriggerTypeId`;

DROP TABLE `CommunicationTriggerTemplate`;

ALTER TABLE `CommunicationTrigger` DROP COLUMN `MaximumOccurences`;

ALTER TABLE `CommunicationTemplate` DROP COLUMN `IsAllowAccessFromAccountDetails`;

ALTER TABLE `ApplicationUser` DROP COLUMN `MaxHotleadCount`;

ALTER TABLE `Address` DROP COLUMN `AddressLine1`;

ALTER TABLE `Address` DROP COLUMN `AddressLine2`;

ALTER TABLE `Address` DROP COLUMN `AddressLine3`;

UPDATE `TriggerTemplateMapping` SET `CommunicationTriggerId` = ''
WHERE `CommunicationTriggerId` IS NULL;
SELECT ROW_COUNT();


ALTER TABLE `TriggerTemplateMapping` MODIFY COLUMN `CommunicationTriggerId` varchar(32) CHARACTER SET utf8mb4 NOT NULL;

UPDATE `TriggerTemplateMapping` SET `CommunicationTemplateId` = ''
WHERE `CommunicationTemplateId` IS NULL;
SELECT ROW_COUNT();


ALTER TABLE `TriggerTemplateMapping` MODIFY COLUMN `CommunicationTemplateId` varchar(32) CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `TriggerTemplateMapping` ADD `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `TriggerTemplateMapping` ADD `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `TriggerTemplateMapping` ADD `CreatedDate` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00';

ALTER TABLE `TriggerTemplateMapping` ADD `IsDeleted` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `TriggerTemplateMapping` ADD `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `TriggerTemplateMapping` ADD `LastModifiedDate` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00';

ALTER TABLE `CommunicationTrigger` MODIFY COLUMN `Description` varchar(1000) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `CommunicationTemplate` ADD `IsAvailableInAccountDetails` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `Buckets` ADD `DPD_From` int NOT NULL DEFAULT 0;

ALTER TABLE `Buckets` ADD `DPD_To` int NOT NULL DEFAULT 0;

ALTER TABLE `Buckets` ADD `DisplayLabel` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `ApplicationUser` ADD `GeoLevelId` varchar(32) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `ApplicationUser` ADD `ProductLevelId` varchar(32) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Address` ADD `AddressLine` varchar(200) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `TriggerTemplateMapping` ADD CONSTRAINT `PK_TriggerTemplateMapping` PRIMARY KEY (`Id`);
CALL POMELO_AFTER_ADD_PRIMARY_KEY(NULL, 'TriggerTemplateMapping', 'Id');

CREATE TABLE `BranchGeoMap` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `BranchId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `HierarchyId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `HierarchyLevel` int NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_BranchGeoMap` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_BranchGeoMap_ApplicationOrg_BranchId` FOREIGN KEY (`BranchId`) REFERENCES `ApplicationOrg` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_BranchGeoMap_HierarchyMaster_HierarchyId` FOREIGN KEY (`HierarchyId`) REFERENCES `HierarchyMaster` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;


CREATE INDEX `IX_TriggerTemplateMapping_CommunicationTemplateId` ON `TriggerTemplateMapping` (`CommunicationTemplateId`);

CREATE INDEX `IX_TriggerTemplateMapping_CommunicationTriggerId` ON `TriggerTemplateMapping` (`CommunicationTriggerId`);

CREATE INDEX `IX_ApplicationUser_GeoLevelId` ON `ApplicationUser` (`GeoLevelId`);

CREATE INDEX `IX_ApplicationUser_ProductLevelId` ON `ApplicationUser` (`ProductLevelId`);

CREATE INDEX `IX_BranchGeoMap_BranchId` ON `BranchGeoMap` (`BranchId`);

CREATE INDEX `IX_BranchGeoMap_HierarchyId` ON `BranchGeoMap` (`HierarchyId`);

ALTER TABLE `ApplicationUser` ADD CONSTRAINT `FK_ApplicationUser_HierarchyLevel_GeoLevelId` FOREIGN KEY (`GeoLevelId`) REFERENCES `HierarchyLevel` (`Id`);

ALTER TABLE `ApplicationUser` ADD CONSTRAINT `FK_ApplicationUser_HierarchyLevel_ProductLevelId` FOREIGN KEY (`ProductLevelId`) REFERENCES `HierarchyLevel` (`Id`);

ALTER TABLE `CommunicationTrigger` ADD CONSTRAINT `FK_CommunicationTrigger_TriggerType_TriggerTypeId` FOREIGN KEY (`TriggerTypeId`) REFERENCES `TriggerType` (`Id`) ON DELETE CASCADE;

ALTER TABLE `UserBucketScope` ADD CONSTRAINT `FK_UserBucketScope_ApplicationUser_ApplicationUserId` FOREIGN KEY (`ApplicationUserId`) REFERENCES `ApplicationUser` (`Id`);

ALTER TABLE `UserBucketScope` ADD CONSTRAINT `FK_UserBucketScope_Buckets_BucketScopeId` FOREIGN KEY (`BucketScopeId`) REFERENCES `Buckets` (`Id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250707113624_AddedBranchGeoMap', '8.0.10');

DROP PROCEDURE `POMELO_AFTER_ADD_PRIMARY_KEY`;

COMMIT;

