/*
  SinglePageCMS — yeni kurulum (boş şema + minimum seed)
  Eski PC’deki veritabanı bu repoda yok; SQL Server’da sıfırdan oluşturmak için bu dosyayı çalıştırın.

  1) SQL Server Management Studio veya sqlcmd ile çalıştırın.
  2) İsterseniz aşağıdaki CREATE DATABASE satırını kendi sunucunuza göre düzenleyin.
  3) Web.config içindeki SinglePageCMSEntities bağlantısını bu veritabanına yönlendirin.

  İlk giriş: Ayarlar’daki kullanıcı adı/parola (seed: admin / Admin123!) — ilk başarılı girişte parola bcrypt’e çevrilir.
  İsterseniz Web.config’te BootstrapAdminUser / BootstrapAdminPassword da kullanılabilir.
*/

SET NOCOUNT ON;
SET QUOTED_IDENTIFIER ON;

IF DB_ID(N'SinglePageCMS') IS NULL
BEGIN
    CREATE DATABASE [SinglePageCMS];
END
GO

USE [SinglePageCMS];
GO

/* ---- Tablolar (EDMX SSDL ile uyumlu) ---- */

IF OBJECT_ID(N'dbo.sysdiagrams', N'U') IS NULL
CREATE TABLE [dbo].[sysdiagrams] (
    [name] sysname NOT NULL,
    [principal_id] int NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int NULL,
    [definition] varbinary(max) NULL,
    CONSTRAINT [PK_sysdiagrams] PRIMARY KEY CLUSTERED ([diagram_id])
);

IF OBJECT_ID(N'dbo.Image', N'U') IS NULL
CREATE TABLE [dbo].[Image] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(150) NOT NULL,
    CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED ([ID])
);

IF OBJECT_ID(N'dbo.Setting', N'U') IS NULL
CREATE TABLE [dbo].[Setting] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [AdminKullaniciAdi] nvarchar(50) NULL,
    [AdminSifre] nvarchar(256) NULL,
    [SuperAdminKullaniciAdi] nvarchar(50) NULL,
    [SuperAdminSifre] nvarchar(256) NULL,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Keywords] nvarchar(max) NULL,
    [Favicon] nvarchar(50) NULL,
    CONSTRAINT [PK_Setting] PRIMARY KEY CLUSTERED ([ID])
);

IF OBJECT_ID(N'dbo.Page', N'U') IS NULL
CREATE TABLE [dbo].[Page] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(250) NOT NULL,
    [Description] nvarchar(250) NULL,
    [Keywords] nvarchar(250) NULL,
    [Active] bit NOT NULL,
    CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED ([ID])
);

IF OBJECT_ID(N'dbo.Section', N'U') IS NULL
CREATE TABLE [dbo].[Section] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [PageID] int NOT NULL,
    [No] int NOT NULL,
    [Title] nvarchar(150) NULL,
    [Invert] bit NOT NULL,
    [Full] bit NOT NULL,
    [Container] nvarchar(50) NULL,
    CONSTRAINT [PK_Section] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_Section_Page] FOREIGN KEY ([PageID]) REFERENCES [dbo].[Page] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.Menu', N'U') IS NULL
CREATE TABLE [dbo].[Menu] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED ([ID])
);

IF OBJECT_ID(N'dbo.MenuItem', N'U') IS NULL
CREATE TABLE [dbo].[MenuItem] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [MenuID] int NOT NULL,
    [No] int NOT NULL,
    [Title] nvarchar(50) NOT NULL,
    [Blank] bit NOT NULL,
    [Url] nvarchar(255) NULL,
    CONSTRAINT [PK_MenuItem] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_MenuItem_Menu] FOREIGN KEY ([MenuID]) REFERENCES [dbo].[Menu] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.Block', N'U') IS NULL
CREATE TABLE [dbo].[Block] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SectionID] int NOT NULL,
    [No] int NOT NULL,
    [Width] int NOT NULL,
    CONSTRAINT [PK_Block] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_Block_Section] FOREIGN KEY ([SectionID]) REFERENCES [dbo].[Section] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.BlockItem', N'U') IS NULL
CREATE TABLE [dbo].[BlockItem] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [BlockID] int NOT NULL,
    CONSTRAINT [PK_BlockItem] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_BlockItem_Block] FOREIGN KEY ([BlockID]) REFERENCES [dbo].[Block] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.Accordion', N'U') IS NULL
CREATE TABLE [dbo].[Accordion] (
    [ID] int NOT NULL,
    CONSTRAINT [PK_Accordion] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_Accordion_Block] FOREIGN KEY ([ID]) REFERENCES [dbo].[Block] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.AccordionItems', N'U') IS NULL
CREATE TABLE [dbo].[AccordionItems] (
    [ID] int NOT NULL,
    [Title] nvarchar(150) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_AccordionItems] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_AccordionItems_BlockItem] FOREIGN KEY ([ID]) REFERENCES [dbo].[BlockItem] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.Banner', N'U') IS NULL
CREATE TABLE [dbo].[Banner] (
    [ID] int NOT NULL,
    [Title] nvarchar(max) NULL,
    CONSTRAINT [PK_Banner] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_Banner_Block] FOREIGN KEY ([ID]) REFERENCES [dbo].[Block] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.BigSlide', N'U') IS NULL
CREATE TABLE [dbo].[BigSlide] (
    [ID] int NOT NULL,
    [Items] int NOT NULL,
    [Nav] bit NOT NULL,
    [Dot] bit NOT NULL,
    [Autoplay] bit NOT NULL,
    CONSTRAINT [PK_BigSlide] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_BigSlide_Block] FOREIGN KEY ([ID]) REFERENCES [dbo].[Block] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.BigSlideItem', N'U') IS NULL
CREATE TABLE [dbo].[BigSlideItem] (
    [ID] int NOT NULL,
    [No] int NOT NULL,
    [Image] nvarchar(250) NOT NULL,
    [Title] nvarchar(250) NULL,
    [Description] nvarchar(250) NULL,
    CONSTRAINT [PK_BigSlideItem] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_BigSlideItem_BlockItem] FOREIGN KEY ([ID]) REFERENCES [dbo].[BlockItem] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.Feature', N'U') IS NULL
CREATE TABLE [dbo].[Feature] (
    [ID] int NOT NULL,
    [Title] nvarchar(150) NULL,
    [Description] nvarchar(max) NULL,
    [Image] nvarchar(255) NULL,
    [Link] nvarchar(255) NULL,
    [Icon] bit NOT NULL,
    CONSTRAINT [PK_Feature] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_Feature_Block] FOREIGN KEY ([ID]) REFERENCES [dbo].[Block] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.Form', N'U') IS NULL
CREATE TABLE [dbo].[Form] (
    [ID] int NOT NULL,
    [Email] nvarchar(1000) NOT NULL,
    [Subject] nvarchar(255) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Form] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_Form_Block] FOREIGN KEY ([ID]) REFERENCES [dbo].[Block] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.FormItem', N'U') IS NULL
CREATE TABLE [dbo].[FormItem] (
    [ID] int NOT NULL,
    [Title] nvarchar(150) NOT NULL,
    [Type] nvarchar(50) NOT NULL,
    [Required] bit NOT NULL,
    CONSTRAINT [PK_FormItem] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_FormItem_BlockItem] FOREIGN KEY ([ID]) REFERENCES [dbo].[BlockItem] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.Gallery', N'U') IS NULL
CREATE TABLE [dbo].[Gallery] (
    [ID] int NOT NULL,
    [Columns] int NOT NULL,
    [Items] int NOT NULL,
    CONSTRAINT [PK_Gallery] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_Gallery_Block] FOREIGN KEY ([ID]) REFERENCES [dbo].[Block] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.GalleryItem', N'U') IS NULL
CREATE TABLE [dbo].[GalleryItem] (
    [ID] int NOT NULL,
    [Title] nvarchar(255) NULL,
    [Image] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_GalleryItem] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_GalleryItem_BlockItem] FOREIGN KEY ([ID]) REFERENCES [dbo].[BlockItem] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.Map', N'U') IS NULL
CREATE TABLE [dbo].[Map] (
    [ID] int NOT NULL,
    [Location] geography NOT NULL,
    CONSTRAINT [PK_Map] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_Map_Block] FOREIGN KEY ([ID]) REFERENCES [dbo].[Block] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.MiniSlide', N'U') IS NULL
CREATE TABLE [dbo].[MiniSlide] (
    [ID] int NOT NULL,
    [Items] int NOT NULL,
    [Nav] bit NOT NULL,
    [Dot] bit NOT NULL,
    [Autoplay] bit NOT NULL,
    CONSTRAINT [PK_MiniSlide] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_MiniSlide_Block] FOREIGN KEY ([ID]) REFERENCES [dbo].[Block] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.MiniSlideItem', N'U') IS NULL
CREATE TABLE [dbo].[MiniSlideItem] (
    [ID] int NOT NULL,
    [No] int NOT NULL,
    [Image] nvarchar(250) NOT NULL,
    [Title] nvarchar(250) NULL,
    [Description] nvarchar(250) NULL,
    CONSTRAINT [PK_MiniSlideItem] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_MiniSlideItem_BlockItem] FOREIGN KEY ([ID]) REFERENCES [dbo].[BlockItem] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.Proje', N'U') IS NULL
CREATE TABLE [dbo].[Proje] (
    [ID] int NOT NULL,
    [Columns] int NOT NULL,
    [Items] int NOT NULL,
    CONSTRAINT [PK_Proje] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_Proje_Block] FOREIGN KEY ([ID]) REFERENCES [dbo].[Block] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.ProjeItem', N'U') IS NULL
CREATE TABLE [dbo].[ProjeItem] (
    [ID] int NOT NULL,
    [Title] nvarchar(255) NULL,
    [Image] nvarchar(255) NOT NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_ProjeItem] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_ProjeItem_BlockItem] FOREIGN KEY ([ID]) REFERENCES [dbo].[BlockItem] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.Quote', N'U') IS NULL
CREATE TABLE [dbo].[Quote] (
    [ID] int NOT NULL,
    [Autoplay] bit NOT NULL,
    CONSTRAINT [PK_Quote] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_Quote_Block] FOREIGN KEY ([ID]) REFERENCES [dbo].[Block] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.QuoteItem', N'U') IS NULL
CREATE TABLE [dbo].[QuoteItem] (
    [ID] int NOT NULL,
    [No] int NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Name] nvarchar(150) NULL,
    [Title] nvarchar(150) NULL,
    CONSTRAINT [PK_QuoteItem] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_QuoteItem_BlockItem] FOREIGN KEY ([ID]) REFERENCES [dbo].[BlockItem] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.Slide', N'U') IS NULL
CREATE TABLE [dbo].[Slide] (
    [ID] int NOT NULL,
    [Items] int NOT NULL,
    [Nav] bit NOT NULL,
    [Dot] bit NOT NULL,
    [Autoplay] bit NOT NULL,
    CONSTRAINT [PK_Slide] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_Slide_Block] FOREIGN KEY ([ID]) REFERENCES [dbo].[Block] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.SlideItem', N'U') IS NULL
CREATE TABLE [dbo].[SlideItem] (
    [ID] int NOT NULL,
    [No] int NOT NULL,
    [Image] nvarchar(250) NOT NULL,
    [Title] nvarchar(250) NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_SlideItem] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_SlideItem_BlockItem] FOREIGN KEY ([ID]) REFERENCES [dbo].[BlockItem] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.Stage', N'U') IS NULL
CREATE TABLE [dbo].[Stage] (
    [ID] int NOT NULL,
    [Icon] nvarchar(50) NOT NULL,
    [Status] int NOT NULL,
    [Title] nvarchar(150) NOT NULL,
    [Description] nvarchar(150) NULL,
    CONSTRAINT [PK_Stage] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_Stage_Block] FOREIGN KEY ([ID]) REFERENCES [dbo].[Block] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.Text', N'U') IS NULL
CREATE TABLE [dbo].[Text] (
    [ID] int NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Text] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_Text_Block] FOREIGN KEY ([ID]) REFERENCES [dbo].[Block] ([ID]) ON DELETE CASCADE
);

IF OBJECT_ID(N'dbo.Video', N'U') IS NULL
CREATE TABLE [dbo].[Video] (
    [ID] int NOT NULL,
    [Thumbnail] nvarchar(250) NULL,
    [URL] nvarchar(250) NOT NULL,
    CONSTRAINT [PK_Video] PRIMARY KEY CLUSTERED ([ID]),
    CONSTRAINT [FK_Video_Block] FOREIGN KEY ([ID]) REFERENCES [dbo].[Block] ([ID]) ON DELETE CASCADE
);
GO

/* ---- Minimum seed (uygulama FirstOrDefault ile patlamasın) ---- */

IF NOT EXISTS (SELECT 1 FROM [dbo].[Setting])
INSERT INTO [dbo].[Setting] ([AdminKullaniciAdi], [AdminSifre], [Title], [Description], [Keywords])
VALUES (N'admin', N'Admin123!', N'SinglePageCMS', N'', N'');

IF NOT EXISTS (SELECT 1 FROM [dbo].[Page])
INSERT INTO [dbo].[Page] ([Title], [Description], [Keywords], [Active])
VALUES (N'Anasayfa', N'', N'', 1);

IF NOT EXISTS (SELECT 1 FROM [dbo].[Menu])
INSERT INTO [dbo].[Menu] ([Name]) VALUES (N'Ana menü');

DECLARE @menuId int = (SELECT TOP 1 [ID] FROM [dbo].[Menu] ORDER BY [ID]);
IF @menuId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM [dbo].[MenuItem] WHERE [MenuID] = @menuId)
INSERT INTO [dbo].[MenuItem] ([MenuID], [No], [Title], [Blank], [Url])
VALUES (@menuId, 1, N'Anasayfa', 0, N'/');
GO

PRINT N'SinglePageCMS veritabanı hazır. İlk giriş: admin / Admin123! (hemen değiştirin).';
GO
