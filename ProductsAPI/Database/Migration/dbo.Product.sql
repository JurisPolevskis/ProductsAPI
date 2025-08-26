USE [ProductsAPI]
GO

/****** Object: Table [dbo].[Product] Script Date: 26.08.2025 12:28:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Product] (
    [Id]       INT             IDENTITY (1, 1) NOT NULL,
    [Title]    NVARCHAR (MAX)  NULL,
    [Quantity] DECIMAL (18, 2) NULL,
    [Price]    DECIMAL (18, 2) NULL
);

SET IDENTITY_INSERT [dbo].[Product] ON
INSERT INTO [dbo].[Product] ([Id], [Title], [Quantity], [Price]) VALUES (1, N'Chair', CAST(20.00 AS Decimal(18, 2)), CAST(19.99 AS Decimal(18, 2)))
INSERT INTO [dbo].[Product] ([Id], [Title], [Quantity], [Price]) VALUES (2, N'Screw', CAST(2545.00 AS Decimal(18, 2)), CAST(0.02 AS Decimal(18, 2)))
INSERT INTO [dbo].[Product] ([Id], [Title], [Quantity], [Price]) VALUES (3, N'Rope (m)', CAST(105.27 AS Decimal(18, 2)), CAST(2.56 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Product] OFF

