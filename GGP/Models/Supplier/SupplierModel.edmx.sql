
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/07/2014 22:49:11
-- Generated from EDMX file: C:\Users\tnasan\Source\Repos\GGP\GGP\Models\Supplier\SupplierModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [GGPDB];
GO
IF SCHEMA_ID(N'Supplier') IS NULL EXECUTE(N'CREATE SCHEMA [Supplier]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[Supplier].[FK_SupplierContact]', 'F') IS NOT NULL
    ALTER TABLE [Supplier].[Contacts] DROP CONSTRAINT [FK_SupplierContact];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[Supplier].[Suppliers]', 'U') IS NOT NULL
    DROP TABLE [Supplier].[Suppliers];
GO
IF OBJECT_ID(N'[Supplier].[Contacts]', 'U') IS NOT NULL
    DROP TABLE [Supplier].[Contacts];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Suppliers'
CREATE TABLE [Supplier].[Suppliers] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [TelephoneNumber] nvarchar(20)  NOT NULL,
    [FaxNumber] nvarchar(20)  NOT NULL,
    [Email] nvarchar(100)  NOT NULL,
    [WebsiteUrl] nvarchar(100)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Contacts'
CREATE TABLE [Supplier].[Contacts] (
    [SupplierId] bigint  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [TelephoneNumber] nvarchar(20)  NULL,
    [Email] nvarchar(20)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Suppliers'
ALTER TABLE [Supplier].[Suppliers]
ADD CONSTRAINT [PK_Suppliers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [SupplierId], [Name] in table 'Contacts'
ALTER TABLE [Supplier].[Contacts]
ADD CONSTRAINT [PK_Contacts]
    PRIMARY KEY CLUSTERED ([SupplierId], [Name] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SupplierId] in table 'Contacts'
ALTER TABLE [Supplier].[Contacts]
ADD CONSTRAINT [FK_SupplierContact]
    FOREIGN KEY ([SupplierId])
    REFERENCES [Supplier].[Suppliers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------