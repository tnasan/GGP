
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/05/2014 21:39:35
-- Generated from EDMX file: C:\Users\Thanasarn\Source\Repos\GGP\GGP\Models\CustomerModel.edmx
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
    [TelephoneNumber] nvarchar(20)  NULL,
    [FaxNumber] nvarchar(20)  NULL,
    [Email] nvarchar(100)  NULL,
    [WebsiteUrl] nvarchar(100)  NULL,
    [Address] nvarchar(max)  NULL
);
GO

-- Creating table 'Contacts'
CREATE TABLE [Customer].[Contacts] (
    [CustomerId] bigint  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [TelephoneNumber] nvarchar(20)  NULL,
    [Email] nvarchar(100)  NULL
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

-- Creating primary key on [CustomerId], [Name] in table 'Contacts'
ALTER TABLE [Customer].[Contacts]
ADD CONSTRAINT [PK_Contacts]
    PRIMARY KEY CLUSTERED ([CustomerId], [Name] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CustomerId] in table 'Contacts'
ALTER TABLE [Customer].[Contacts]
ADD CONSTRAINT [FK_CustomerContact]
    FOREIGN KEY ([CustomerId])
    REFERENCES [Customer].[Customers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------