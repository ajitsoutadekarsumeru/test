START TRANSACTION;

CREATE TABLE `Wallet` (
    `AgentId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `WalletLimit` decimal(65,30) NOT NULL,
    `ConsumedFunds` decimal(65,30) NOT NULL,
    `Id` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedBy` varchar(32) CHARACTER SET utf8mb4 NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Wallet` PRIMARY KEY (`AgentId`),
    CONSTRAINT `FK_Wallet_ApplicationUser_AgentId` FOREIGN KEY (`AgentId`) REFERENCES `ApplicationUser` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Reservation` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Amount` decimal(65,30) NOT NULL,
    `WalletAgentId` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Reservation` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Reservation_Wallet_WalletAgentId` FOREIGN KEY (`WalletAgentId`) REFERENCES `Wallet` (`AgentId`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

UPDATE `RoleAccountScope` SET `CreatedDate` = TIMESTAMP '2025-03-25 08:16:56', `LastModifiedDate` = TIMESTAMP '2025-03-25 08:16:56'
WHERE `Id` = '3a185d8db599c016d4caf7aa05af889f';
SELECT ROW_COUNT();


UPDATE `RoleAccountScope` SET `CreatedDate` = TIMESTAMP '2025-03-25 08:16:56', `LastModifiedDate` = TIMESTAMP '2025-03-25 08:16:56'
WHERE `Id` = '3a185d8db599d1ce3ace0b1c74528678';
SELECT ROW_COUNT();


UPDATE `RoleAccountScope` SET `CreatedDate` = TIMESTAMP '2025-03-25 08:16:56', `LastModifiedDate` = TIMESTAMP '2025-03-25 08:16:56'
WHERE `Id` = '3a185d8db599f4a83d63dec4faea8a98';
SELECT ROW_COUNT();


UPDATE `RoleAccountScope` SET `CreatedDate` = TIMESTAMP '2025-03-25 08:16:56', `LastModifiedDate` = TIMESTAMP '2025-03-25 08:16:56'
WHERE `Id` = '3a185d8db599f686a3b157eaeb799b2d';
SELECT ROW_COUNT();


CREATE INDEX `IX_Reservation_WalletAgentId` ON `Reservation` (`WalletAgentId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250325081659_AddedWallet', '8.0.10');

COMMIT;

