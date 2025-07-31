CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `FlexTenant` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `HostName` longtext CHARACTER SET utf8mb4 NOT NULL,
    `TenantDbType` longtext CHARACTER SET utf8mb4 NOT NULL,
    `DefaultWriteDbConnectionString` longtext CHARACTER SET utf8mb4 NOT NULL,
    `DefaultReadDbConnectionString` longtext CHARACTER SET utf8mb4 NOT NULL,
    `IsSharedTenant` tinyint(1) NOT NULL,
    `Discriminator` varchar(21) CHARACTER SET utf8mb4 NOT NULL,
    `IsDeleted` tinyint(1) NULL,
    `CreatedDate` datetime(6) NULL,
    `LastModifiedDate` datetime(6) NULL,
    `Color` varchar(50) CHARACTER SET utf8mb4 NULL,
    `Logo` varchar(50) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_FlexTenant` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TenantEmailConfiguration` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `TenantId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `Mailcc` varchar(500) CHARACTER SET utf8mb4 NULL,
    `MailFrom` varchar(100) CHARACTER SET utf8mb4 NULL,
    `EmailLogPath` varchar(200) CHARACTER SET utf8mb4 NULL,
    `MailTo` varchar(100) CHARACTER SET utf8mb4 NULL,
    `MailSignature` varchar(100) CHARACTER SET utf8mb4 NULL,
    `SmtpServer` varchar(100) CHARACTER SET utf8mb4 NULL,
    `SmtpPort` varchar(20) CHARACTER SET utf8mb4 NULL,
    `SmtpUser` varchar(100) CHARACTER SET utf8mb4 NULL,
    `SmtpPassword` varchar(100) CHARACTER SET utf8mb4 NULL,
    `EnableSsl` varchar(50) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_TenantEmailConfiguration` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TenantEmailConfiguration_FlexTenant_TenantId` FOREIGN KEY (`TenantId`) REFERENCES `FlexTenant` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `TenantSMSConfiguration` (
    `Id` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    `IsDeleted` tinyint(1) NOT NULL,
    `CreatedDate` datetime(6) NOT NULL,
    `LastModifiedDate` datetime(6) NOT NULL,
    `TenantId` varchar(32) CHARACTER SET utf8mb4 NULL,
    `Key` longtext CHARACTER SET utf8mb4 NULL,
    `Value` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_TenantSMSConfiguration` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_TenantSMSConfiguration_FlexTenant_TenantId` FOREIGN KEY (`TenantId`) REFERENCES `FlexTenant` (`Id`)
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_TenantEmailConfiguration_TenantId` ON `TenantEmailConfiguration` (`TenantId`);

CREATE INDEX `IX_TenantSMSConfiguration_TenantId` ON `TenantSMSConfiguration` (`TenantId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240628132718_initial', '8.0.6');

COMMIT;

