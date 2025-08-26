USE [ProductsAPI]
GO

/****** Object: Table [dbo].[product_audit] Script Date: 26.08.2025 15:30:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[product_audit] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [ChangeType]       NVARCHAR (MAX)  NULL,
    [Source]           NVARCHAR (MAX)  NULL,
    [User]             NVARCHAR (MAX)  NULL,
    [ModificationDate] DATETIME        NULL,
    [ProductId]        INT             NULL,
    [OldTitle]         NVARCHAR (MAX)  NULL,
    [OldQuantity]      DECIMAL (18, 2) NULL,
    [OldPrice]         DECIMAL (18, 2) NULL,
    [NewTitle]         NVARCHAR (MAX)  NULL,
    [NewQuantity]      DECIMAL (18, 2) NULL,
    [NewPrice]         DECIMAL (18, 2) NULL
);


