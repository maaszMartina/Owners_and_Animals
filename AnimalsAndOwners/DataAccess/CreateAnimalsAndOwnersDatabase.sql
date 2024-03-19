--Create database
CREATE DATABASE [AnimalsAndOwners]
GO
 
--Create tables
USE [AnimalsAndOwners]
GO

CREATE TABLE [dbo].[Animal](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Species] [nvarchar](50) NOT NULL,
	[Age] [int] NOT NULL,
	[OwnerId] [int] NOT NULL,
 CONSTRAINT [PK_Animal] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
 )

CREATE TABLE [dbo].[Owner](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Owner] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
 )

ALTER TABLE [dbo].[Animal] ADD CONSTRAINT [FK_Animal_Owner] FOREIGN KEY([OwnerId]) REFERENCES [dbo].[Owner] ([Id])

