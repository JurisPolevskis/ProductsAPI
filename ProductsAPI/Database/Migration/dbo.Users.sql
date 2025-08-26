USE [ProductsAPI]
GO

/****** Object: Table [dbo].[Users] Script Date: 26.08.2025 11:12:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Username] NVARCHAR (MAX) NULL,
    [Hash]     NVARCHAR (MAX) NULL
);


