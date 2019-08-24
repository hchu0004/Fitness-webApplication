
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/08/2018 21:50:38
-- Generated from EDMX file: C:\Users\harry\source\repos\AssignmentV3\AssignmentV3\Models\BusinessDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BusinessDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Programs'
CREATE TABLE [dbo].[Programs] (
    [ProgramId] int IDENTITY(1,1) NOT NULL,
    [ProgramName] nvarchar(max)  NOT NULL,
    [ProgramDesc] nvarchar(max)  NOT NULL,
    [ProgramPrice] float  NOT NULL
);
GO

-- Creating table 'Reservations'
CREATE TABLE [dbo].[Reservations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProgramId] int  NOT NULL,
    [DatetimeStart] datetime  NOT NULL,
    [DatetimeEnd] datetime  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [UserId] nvarchar(max)  NOT NULL,
    [memberEmail] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FavoritePrograms'
CREATE TABLE [dbo].[FavoritePrograms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProgramId] int  NOT NULL,
    [UserId] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ProgramId] in table 'Programs'
ALTER TABLE [dbo].[Programs]
ADD CONSTRAINT [PK_Programs]
    PRIMARY KEY CLUSTERED ([ProgramId] ASC);
GO

-- Creating primary key on [Id] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [PK_Reservations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FavoritePrograms'
ALTER TABLE [dbo].[FavoritePrograms]
ADD CONSTRAINT [PK_FavoritePrograms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProgramId] in table 'FavoritePrograms'
ALTER TABLE [dbo].[FavoritePrograms]
ADD CONSTRAINT [FK_ProgramFavoriteProgram]
    FOREIGN KEY ([ProgramId])
    REFERENCES [dbo].[Programs]
        ([ProgramId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProgramFavoriteProgram'
CREATE INDEX [IX_FK_ProgramFavoriteProgram]
ON [dbo].[FavoritePrograms]
    ([ProgramId]);
GO

-- Creating foreign key on [ProgramId] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [FK_ProgramReservation]
    FOREIGN KEY ([ProgramId])
    REFERENCES [dbo].[Programs]
        ([ProgramId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProgramReservation'
CREATE INDEX [IX_FK_ProgramReservation]
ON [dbo].[Reservations]
    ([ProgramId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------