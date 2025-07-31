START TRANSACTION;

ALTER TABLE `LoanAccountsProjection` DROP INDEX `IX_LoanAccountsProjection_LoanAccountId`;

CREATE TABLE `CollectionProjection` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `CollectionId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `BUCKET` bigint NULL,
    `CURRENT_BUCKET` varchar(50) CHARACTER SET utf8mb4 NULL,
    `NPA_STAGEID` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AgencyId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CollectorId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `TeleCallingAgencyId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `TeleCallerId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AllocationOwnerId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `BOM_POS` decimal(18,2) NULL,
    `CURRENT_POS` decimal(18,2) NULL,
    `PAYMENTSTATUS` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_CollectionProjection` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_CollectionProjection_ApplicationOrg_AgencyId` FOREIGN KEY (`AgencyId`) REFERENCES `ApplicationOrg` (`Id`),
    CONSTRAINT `FK_CollectionProjection_ApplicationOrg_TeleCallingAgencyId` FOREIGN KEY (`TeleCallingAgencyId`) REFERENCES `ApplicationOrg` (`Id`),
    CONSTRAINT `FK_CollectionProjection_ApplicationUser_AllocationOwnerId` FOREIGN KEY (`AllocationOwnerId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_CollectionProjection_ApplicationUser_CollectorId` FOREIGN KEY (`CollectorId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_CollectionProjection_ApplicationUser_TeleCallerId` FOREIGN KEY (`TeleCallerId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_CollectionProjection_Collections_CollectionId` FOREIGN KEY (`CollectionId`) REFERENCES `Collections` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `FeedbackProjection` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `FeedbackId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `BUCKET` bigint NULL,
    `CURRENT_BUCKET` varchar(50) CHARACTER SET utf8mb4 NULL,
    `NPA_STAGEID` varchar(50) CHARACTER SET utf8mb4 NULL,
    `AgencyId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CollectorId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `TeleCallingAgencyId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `TeleCallerId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `AllocationOwnerId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `BOM_POS` decimal(18,2) NULL,
    `CURRENT_POS` decimal(18,2) NULL,
    `PAYMENTSTATUS` varchar(50) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_FeedbackProjection` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_FeedbackProjection_ApplicationOrg_AgencyId` FOREIGN KEY (`AgencyId`) REFERENCES `ApplicationOrg` (`Id`),
    CONSTRAINT `FK_FeedbackProjection_ApplicationOrg_TeleCallingAgencyId` FOREIGN KEY (`TeleCallingAgencyId`) REFERENCES `ApplicationOrg` (`Id`),
    CONSTRAINT `FK_FeedbackProjection_ApplicationUser_AllocationOwnerId` FOREIGN KEY (`AllocationOwnerId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_FeedbackProjection_ApplicationUser_CollectorId` FOREIGN KEY (`CollectorId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_FeedbackProjection_ApplicationUser_TeleCallerId` FOREIGN KEY (`TeleCallerId`) REFERENCES `ApplicationUser` (`Id`),
    CONSTRAINT `FK_FeedbackProjection_Feedback_FeedbackId` FOREIGN KEY (`FeedbackId`) REFERENCES `Feedback` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

INSERT INTO `AccountScopeConfiguration` (`Id`, `AccountabilityTypeId`, `CreatedBy`, `CreatedDate`, `IsDeleted`, `LastModifiedBy`, `LastModifiedDate`, `Scope`, `ScopeLevel`)
VALUES ('3a185d8db599c016d4caf7aa05af889f', 'AgencyToFrontEndExternalFOS', NULL, TIMESTAMP '2025-05-30 12:35:00', FALSE, NULL, TIMESTAMP '2025-05-30 12:35:00', 'all', 1),
('3a185d8db599d1ce3ace0b1c74528678', 'BankToFrontEndInternalFOS', NULL, TIMESTAMP '2025-05-30 12:35:00', FALSE, NULL, TIMESTAMP '2025-05-30 12:35:00', 'all', 1),
('3a185d8db599f4a83d63dec4faea8a98', 'AgencyToFrontEndExternalTC', NULL, TIMESTAMP '2025-05-30 12:35:00', FALSE, NULL, TIMESTAMP '2025-05-30 12:35:00', 'all', 1),
('3a185d8db599f686a3b157eaeb799b2d', 'BankToFrontEndInternalTC', NULL, TIMESTAMP '2025-05-30 12:35:00', FALSE, NULL, TIMESTAMP '2025-05-30 12:35:00', 'all', 1);

CREATE UNIQUE INDEX `IX_LoanAccountsProjection_LoanAccountId_Year_Month` ON `LoanAccountsProjection` (`LoanAccountId`, `Year`, `Month`);

CREATE INDEX `IX_CollectionProjection_AgencyId` ON `CollectionProjection` (`AgencyId`);

CREATE INDEX `IX_CollectionProjection_AllocationOwnerId` ON `CollectionProjection` (`AllocationOwnerId`);

CREATE INDEX `IX_CollectionProjection_CollectionId` ON `CollectionProjection` (`CollectionId`);

CREATE INDEX `IX_CollectionProjection_CollectorId` ON `CollectionProjection` (`CollectorId`);

CREATE INDEX `IX_CollectionProjection_TeleCallerId` ON `CollectionProjection` (`TeleCallerId`);

CREATE INDEX `IX_CollectionProjection_TeleCallingAgencyId` ON `CollectionProjection` (`TeleCallingAgencyId`);

CREATE INDEX `IX_FeedbackProjection_AgencyId` ON `FeedbackProjection` (`AgencyId`);

CREATE INDEX `IX_FeedbackProjection_AllocationOwnerId` ON `FeedbackProjection` (`AllocationOwnerId`);

CREATE INDEX `IX_FeedbackProjection_CollectorId` ON `FeedbackProjection` (`CollectorId`);

CREATE INDEX `IX_FeedbackProjection_FeedbackId` ON `FeedbackProjection` (`FeedbackId`);

CREATE INDEX `IX_FeedbackProjection_TeleCallerId` ON `FeedbackProjection` (`TeleCallerId`);

CREATE INDEX `IX_FeedbackProjection_TeleCallingAgencyId` ON `FeedbackProjection` (`TeleCallingAgencyId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250530123504_FeedbackAndCollectionProjectionAdded', '8.0.10');

COMMIT;

