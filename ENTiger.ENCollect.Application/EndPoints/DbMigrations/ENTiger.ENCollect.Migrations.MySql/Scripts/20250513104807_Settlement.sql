START TRANSACTION;

ALTER TABLE `Settlement` ADD `CURRENT_BUCKET` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Settlement` ADD `CURRENT_DPD` bigint NULL;

ALTER TABLE `Settlement` ADD `ChargesOutstanding` decimal(65,30) NOT NULL DEFAULT 0.0;

ALTER TABLE `Settlement` ADD `DateOfDeath` datetime(6) NULL;

ALTER TABLE `Settlement` ADD `InterestOutstanding` decimal(65,30) NOT NULL DEFAULT 0.0;

ALTER TABLE `Settlement` ADD `IsDeathSettlement` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `Settlement` ADD `LoanAccountId` varchar(32) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Settlement` ADD `NPA_STAGEID` varchar(50) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `Settlement` ADD `NumberOfEmisDue` int NOT NULL DEFAULT 0;

ALTER TABLE `Settlement` ADD `NumberOfInstallments` int NOT NULL DEFAULT 0;

ALTER TABLE `Settlement` ADD `PrincipalOutstanding` decimal(65,30) NOT NULL DEFAULT 0.0;

ALTER TABLE `Settlement` ADD `SettlementAmount` decimal(65,30) NOT NULL DEFAULT 0.0;

ALTER TABLE `Settlement` ADD `SettlementDateForDuesCalc` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00';

ALTER TABLE `Settlement` ADD `SettlementRemarks` varchar(1000) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `Settlement` ADD `Status` longtext CHARACTER SET utf8mb4 NOT NULL;

ALTER TABLE `Settlement` ADD `StatusUpdatedOn` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00';

ALTER TABLE `Settlement` ADD `TOS` varchar(50) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

ALTER TABLE `Settlement` ADD `TrancheType` varchar(50) CHARACTER SET utf8mb4 NOT NULL DEFAULT '';

CREATE TABLE `InstallmentDetail` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `InstallmentAmount` decimal(65,30) NOT NULL,
    `InstallmentDueDate` datetime(6) NOT NULL,
    `SettlementId` varchar(32) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_InstallmentDetail` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_InstallmentDetail_Settlement_SettlementId` FOREIGN KEY (`SettlementId`) REFERENCES `Settlement` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `WaiverDetail` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ChargeType` longtext CHARACTER SET utf8mb4 NOT NULL,
    `AmountAsPerCBS` decimal(65,30) NOT NULL,
    `ApportionmentAmount` decimal(65,30) NOT NULL,
    `WaiverAmount` decimal(65,30) NOT NULL,
    `WaiverPercent` decimal(65,30) NOT NULL,
    `SettlementId` varchar(32) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_WaiverDetail` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_WaiverDetail_Settlement_SettlementId` FOREIGN KEY (`SettlementId`) REFERENCES `Settlement` (`Id`)
) CHARACTER SET=utf8mb4;


CREATE INDEX `IX_Settlement_LoanAccountId` ON `Settlement` (`LoanAccountId`);

CREATE INDEX `IX_InstallmentDetail_SettlementId` ON `InstallmentDetail` (`SettlementId`);

CREATE INDEX `IX_WaiverDetail_SettlementId` ON `WaiverDetail` (`SettlementId`);

-- Index on CustomerId
CREATE INDEX IX_LoanAccount_CustomerId ON LoanAccount(CustomerId);

-- Index on AccountId
CREATE INDEX IX_LoanAccount_AccountId ON LoanAccount(AccountId);

-- Index on CurrentDPD (important for range searches)
CREATE INDEX IX_LoanAccount_CurrentDPD ON LoanAccount(CurrentDPD);

-- Index on NPAFlag (boolean)
CREATE INDEX IX_LoanAccount_NPAFlag ON LoanAccount(NPAFlag);

-- Index on IsEligibleForSettlement (boolean)
CREATE INDEX IX_LoanAccount_IsEligibleForSettlement ON LoanAccount(IsEligibleForSettlement);




ALTER TABLE `Settlement` ADD CONSTRAINT `FK_Settlement_LoanAccounts_LoanAccountId` FOREIGN KEY (`LoanAccountId`) REFERENCES `LoanAccounts` (`Id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250513104807_Settlement', '8.0.10');

COMMIT;

