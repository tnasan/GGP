
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/03/2014 19:31:18
-- Generated from EDMX file: C:\Users\tnasan\Source\Repos\GGP\GGP\Models\CustomerModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [GGPDB];
GO
IF SCHEMA_ID(N'Customer') IS NULL EXECUTE(N'CREATE SCHEMA [Customer]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[Customer].[FK_CustomerContact]', 'F') IS NOT NULL
    ALTER TABLE [Customer].[Contacts] DROP CONSTRAINT [FK_CustomerContact];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[Customer].[Customers]', 'U') IS NOT NULL
    DROP TABLE [Customer].[Customers];
GO
IF OBJECT_ID(N'[Customer].[Contacts]', 'U') IS NOT NULL
    DROP TABLE [Customer].[Contacts];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customers'
CREATE TABLE [Customer].[Customers] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [TelephoneNumber] nvarchar(20)  NOT NULL,
    [FaxNumber] nvarchar(20)  NOT NULL,
    [Email] nvarchar(100)  NOT NULL,
    [WebsiteUrl] nvarchar(100)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Contacts'
CREATE TABLE [Customer].[Contacts] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [TelephoneNumber] nvarchar(20)  NULL,
    [Email] nvarchar(100)  NULL
);
GO

-- Creating table 'CustomerContact'
CREATE TABLE [Customer].[CustomerContact] (
    [Customers_Id] bigint  NOT NULL,
    [Contacts_Id] bigint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Customers'
ALTER TABLE [Customer].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Contacts'
ALTER TABLE [Customer].[Contacts]
ADD CONSTRAINT [PK_Contacts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Customers_Id], [Contacts_Id] in table 'CustomerContact'
ALTER TABLE [Customer].[CustomerContact]
ADD CONSTRAINT [PK_CustomerContact]
    PRIMARY KEY CLUSTERED ([Customers_Id], [Contacts_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Customers_Id] in table 'CustomerContact'
ALTER TABLE [Customer].[CustomerContact]
ADD CONSTRAINT [FK_CustomerContact_Customer]
    FOREIGN KEY ([Customers_Id])
    REFERENCES [Customer].[Customers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Contacts_Id] in table 'CustomerContact'
ALTER TABLE [Customer].[CustomerContact]
ADD CONSTRAINT [FK_CustomerContact_Contact]
    FOREIGN KEY ([Contacts_Id])
    REFERENCES [Customer].[Contacts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerContact_Contact'
CREATE INDEX [IX_FK_CustomerContact_Contact]
ON [Customer].[CustomerContact]
    ([Contacts_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------