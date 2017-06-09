CREATE TABLE [dbo].[Products] (
    [ProductID]     INT             IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (100)  NOT NULL,
    [Description]   NVARCHAR (500)  NOT NULL,
    [Category]      NVARCHAR (50)   NOT NULL,
    [Price]         DECIMAL (16, 2) NOT NULL,
    [ImageData]     VARBINARY (MAX) NULL,
    [ImageMimeType] VARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([ProductID] ASC)
);


SET IDENTITY_INSERT [dbo].[Products] ON
INSERT INTO [dbo].[Products] ([ProductID], [Name], [Description], [Category], [Price], [ImageData], [ImageMimeType]) VALUES (1, N'Free People Walk This Way Clog', N'SKU: #8808582, Nothing can stop you when you wear the Free People® Walk this Way Clog! Ankle wrap with buckle closure. Leather upper. Comfortable leather lining.', N'SANDALS', CAST(100.00 AS Decimal(16, 2)), <Binary Data>, N'image/jpeg')
INSERT INTO [dbo].[Products] ([ProductID], [Name], [Description], [Category], [Price], [ImageData], [ImageMimeType]) VALUES (2, N'Vans LPE', N'SKU: #7579608, It''s a mash-up of epic proportions! Combining the best design features of the Authentic™ and Era™, the LPE (Lo Pro Era) remixes it all into one slim and sleek package. Clean uppers of durable canvas or textile materials. Padded collar for added comfort. Doubled stitched vamp.', N'SNEAKERS AND ATHLETIC SHOES', CAST(40.00 AS Decimal(16, 2)), <Binary Data>, N'image/jpeg')
INSERT INTO [dbo].[Products] ([ProductID], [Name], [Description], [Category], [Price], [ImageData], [ImageMimeType]) VALUES (3, N'Aquatalia Yulia', N'SKU: #8679339. Embrace the elements in a standout style with the Yulia bootie. Weatherproof patent or metallic leather upper. Pull-on construction. Dual side goring panels for easy wear. Almond toe. Leather lining.', N'Boots', CAST(250.91 AS Decimal(16, 2)), <Binary Data>, N'image/jpeg')
INSERT INTO [dbo].[Products] ([ProductID], [Name], [Description], [Category], [Price], [ImageData], [ImageMimeType]) VALUES (5, N'Steve Madden Stecy', N'SKU: #8328024 Say hello to the weekend with the Stecy sandal! Man-made upper. Single vamp strap.', N'HEELS', CAST(52.05 AS Decimal(16, 2)), <Binary Data>, N'image/jpeg')
INSERT INTO [dbo].[Products] ([ProductID], [Name], [Description], [Category], [Price], [ImageData], [ImageMimeType]) VALUES (6, N'Steve Madden Nobell', N'SKU: #8764309 Stay on trend in the cute Nobell bootie. Leather upper. Slip-on design with goring panels for easy on and off.', N'HEELS', CAST(47.99 AS Decimal(16, 2)), <Binary Data>, N'image/jpeg')
SET IDENTITY_INSERT [dbo].[Products] OFF

