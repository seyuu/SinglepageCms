-- Widen password columns for bcrypt / legacy hash strings. Run once on existing SinglePageCMS databases.
IF COL_LENGTH(N'dbo.Setting', N'AdminSifre') IS NOT NULL
    ALTER TABLE dbo.Setting ALTER COLUMN AdminSifre NVARCHAR(256) NULL;

IF COL_LENGTH(N'dbo.Setting', N'SuperAdminSifre') IS NOT NULL
    ALTER TABLE dbo.Setting ALTER COLUMN SuperAdminSifre NVARCHAR(256) NULL;
