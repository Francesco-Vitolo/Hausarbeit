
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/23/2022 16:22:27
-- Generated from EDMX file: C:\Users\Francesco\Desktop\Neu\Hausarbeit\Verwaltungsprogramm_Vinothek\Verwaltungsprogramm_Vinothek\Vinothek.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DB_Vinothek];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[fk_Benutzer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Logins] DROP CONSTRAINT [fk_Benutzer];
GO
IF OBJECT_ID(N'[dbo].[fk_Produzent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Produkt] DROP CONSTRAINT [fk_Produzent];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Benutzer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Benutzer];
GO
IF OBJECT_ID(N'[dbo].[Event]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Event];
GO
IF OBJECT_ID(N'[dbo].[EventPos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventPos];
GO
IF OBJECT_ID(N'[dbo].[Logins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Logins];
GO
IF OBJECT_ID(N'[dbo].[Produkt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Produkt];
GO
IF OBJECT_ID(N'[dbo].[Produzent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Produzent];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Benutzer'
CREATE TABLE [dbo].[Benutzer] (
    [ID_Benutzer] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(50)  NULL,
    [Passwort] nvarchar(64)  NULL
);
GO

-- Creating table 'Event'
CREATE TABLE [dbo].[Event] (
    [ID_Veranstaltung] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [AnzahlPersonen] int  NULL,
    [Datum] datetime  NULL,
    [Zeit] nvarchar(20)  NULL
);
GO

-- Creating table 'EventPos'
CREATE TABLE [dbo].[EventPos] (
    [ID_Veranstaltung] int  NOT NULL,
    [ID_Produkt] int  NOT NULL
);
GO

-- Creating table 'Logins'
CREATE TABLE [dbo].[Logins] (
    [ID_Login] int IDENTITY(1,1) NOT NULL,
    [ID_Benutzer] int  NULL,
    [Datum] datetime  NULL
);
GO

-- Creating table 'Produkt'
CREATE TABLE [dbo].[Produkt] (
    [ID_Produkt] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Art] nvarchar(50)  NULL,
    [Qualitätssiegel] nvarchar(50)  NULL,
    [Rebsorten] nvarchar(50)  NULL,
    [Geschmack] nvarchar(50)  NULL,
    [Alkoholgehalt] float  NULL,
    [Jahrgang] int  NULL,
    [Beschreibung] nvarchar(500)  NULL,
    [Preis] float  NULL,
    [Aktiv] bit  NULL,
    [Picture] varbinary(max)  NULL,
    [PDF_file] varbinary(max)  NULL,
    [ID_Produzent] int  NOT NULL
);
GO

-- Creating table 'Produzent'
CREATE TABLE [dbo].[Produzent] (
    [ID_Produzent] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Land] nvarchar(50)  NULL,
    [Region] nvarchar(50)  NULL,
    [Adresse] nvarchar(100)  NULL,
    [Email] nvarchar(100)  NULL,
    [Telefon] nvarchar(50)  NULL,
    [Hektar] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID_Benutzer] in table 'Benutzer'
ALTER TABLE [dbo].[Benutzer]
ADD CONSTRAINT [PK_Benutzer]
    PRIMARY KEY CLUSTERED ([ID_Benutzer] ASC);
GO

-- Creating primary key on [ID_Veranstaltung] in table 'Event'
ALTER TABLE [dbo].[Event]
ADD CONSTRAINT [PK_Event]
    PRIMARY KEY CLUSTERED ([ID_Veranstaltung] ASC);
GO

-- Creating primary key on [ID_Veranstaltung], [ID_Produkt] in table 'EventPos'
ALTER TABLE [dbo].[EventPos]
ADD CONSTRAINT [PK_EventPos]
    PRIMARY KEY CLUSTERED ([ID_Veranstaltung], [ID_Produkt] ASC);
GO

-- Creating primary key on [ID_Login] in table 'Logins'
ALTER TABLE [dbo].[Logins]
ADD CONSTRAINT [PK_Logins]
    PRIMARY KEY CLUSTERED ([ID_Login] ASC);
GO

-- Creating primary key on [ID_Produkt] in table 'Produkt'
ALTER TABLE [dbo].[Produkt]
ADD CONSTRAINT [PK_Produkt]
    PRIMARY KEY CLUSTERED ([ID_Produkt] ASC);
GO

-- Creating primary key on [ID_Produzent] in table 'Produzent'
ALTER TABLE [dbo].[Produzent]
ADD CONSTRAINT [PK_Produzent]
    PRIMARY KEY CLUSTERED ([ID_Produzent] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ID_Benutzer] in table 'Logins'
ALTER TABLE [dbo].[Logins]
ADD CONSTRAINT [fk_Benutzer]
    FOREIGN KEY ([ID_Benutzer])
    REFERENCES [dbo].[Benutzer]
        ([ID_Benutzer])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Benutzer'
CREATE INDEX [IX_fk_Benutzer]
ON [dbo].[Logins]
    ([ID_Benutzer]);
GO

-- Creating foreign key on [ID_Produzent] in table 'Produkt'
ALTER TABLE [dbo].[Produkt]
ADD CONSTRAINT [fk_Produzent]
    FOREIGN KEY ([ID_Produzent])
    REFERENCES [dbo].[Produzent]
        ([ID_Produzent])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Produzent'
CREATE INDEX [IX_fk_Produzent]
ON [dbo].[Produkt]
    ([ID_Produzent]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------