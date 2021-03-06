USE [Restaurant]
GO
/****** Object:  Table [dbo].[Customer_Master]    Script Date: 05/12/2018 17:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer_Master](
	[Customer_Code] [int] IDENTITY(1,1) NOT NULL,
	[Customer_Name] [varchar](50) NULL,
	[Customer_Address] [varchar](max) NULL,
	[Customer_Locality] [varchar](max) NULL,
	[Customer_Phone] [varchar](50) NULL,
	[Customer_Type] [varchar](50) NULL,
 CONSTRAINT [PK_Customer_Master] PRIMARY KEY CLUSTERED 
(
	[Customer_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Customer_Master] ON
INSERT [dbo].[Customer_Master] ([Customer_Code], [Customer_Name], [Customer_Address], [Customer_Locality], [Customer_Phone], [Customer_Type]) VALUES (2, N'nashir', N'navsari', N'', N'9898933180', N'C')
INSERT [dbo].[Customer_Master] ([Customer_Code], [Customer_Name], [Customer_Address], [Customer_Locality], [Customer_Phone], [Customer_Type]) VALUES (3, N'', N'', N'', N'', N'C')
INSERT [dbo].[Customer_Master] ([Customer_Code], [Customer_Name], [Customer_Address], [Customer_Locality], [Customer_Phone], [Customer_Type]) VALUES (4, N'Mumtaz Shaikh', N'tata school', N'', N'9724035505', N'C')
INSERT [dbo].[Customer_Master] ([Customer_Code], [Customer_Name], [Customer_Address], [Customer_Locality], [Customer_Phone], [Customer_Type]) VALUES (5, N'Mumtaz Shaikh', N'tata school', N'', N'9724035505', N'C')
INSERT [dbo].[Customer_Master] ([Customer_Code], [Customer_Name], [Customer_Address], [Customer_Locality], [Customer_Phone], [Customer_Type]) VALUES (6, N'Mumtaz Shaikh', N'tata school', N'', N'9724035505', N'C')
INSERT [dbo].[Customer_Master] ([Customer_Code], [Customer_Name], [Customer_Address], [Customer_Locality], [Customer_Phone], [Customer_Type]) VALUES (7, N'xyz', N'', N'', N'', N'C')
INSERT [dbo].[Customer_Master] ([Customer_Code], [Customer_Name], [Customer_Address], [Customer_Locality], [Customer_Phone], [Customer_Type]) VALUES (8, N'customer2', N'nvs', N'', N'98981234567', N'C')
SET IDENTITY_INSERT [dbo].[Customer_Master] OFF
/****** Object:  Table [dbo].[Category_Master]    Script Date: 05/12/2018 17:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category_Master](
	[Category_Id] [int] IDENTITY(1,1) NOT NULL,
	[Category_Name] [varchar](50) NULL,
 CONSTRAINT [PK_Category_Master] PRIMARY KEY CLUSTERED 
(
	[Category_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Category_Master] ON
INSERT [dbo].[Category_Master] ([Category_Id], [Category_Name]) VALUES (1, N'Delight Pizza')
INSERT [dbo].[Category_Master] ([Category_Id], [Category_Name]) VALUES (2, N'Classic Pizza')
INSERT [dbo].[Category_Master] ([Category_Id], [Category_Name]) VALUES (3, N'Exotic Pizza')
INSERT [dbo].[Category_Master] ([Category_Id], [Category_Name]) VALUES (4, N'Extra')
INSERT [dbo].[Category_Master] ([Category_Id], [Category_Name]) VALUES (5, N'Sandwich')
INSERT [dbo].[Category_Master] ([Category_Id], [Category_Name]) VALUES (6, N'Laziz Grill')
INSERT [dbo].[Category_Master] ([Category_Id], [Category_Name]) VALUES (7, N'Burger')
INSERT [dbo].[Category_Master] ([Category_Id], [Category_Name]) VALUES (8, N'Calzone')
INSERT [dbo].[Category_Master] ([Category_Id], [Category_Name]) VALUES (9, N'Sides')
INSERT [dbo].[Category_Master] ([Category_Id], [Category_Name]) VALUES (10, N'Combo Offers')
SET IDENTITY_INSERT [dbo].[Category_Master] OFF
/****** Object:  Table [dbo].[Bill_Master]    Script Date: 05/12/2018 17:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Bill_Master](
	[Bill_No] [int] IDENTITY(1,1) NOT NULL,
	[Bill_Date] [date] NULL,
	[Bill_Time] [time](7) NULL,
	[Bill_Type] [varchar](50) NULL,
	[TableNo] [varchar](50) NULL,
	[Person] [int] NULL,
	[Customer_Code] [int] NULL,
	[Discount] [numeric](18, 2) NULL,
	[Delivery_Charge] [numeric](18, 2) NULL,
	[Bill_Print] [tinyint] NULL,
	[Bill_T] [tinyint] NULL,
 CONSTRAINT [PK_Bill_Master] PRIMARY KEY CLUSTERED 
(
	[Bill_No] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Bill_Master] ON
INSERT [dbo].[Bill_Master] ([Bill_No], [Bill_Date], [Bill_Time], [Bill_Type], [TableNo], [Person], [Customer_Code], [Discount], [Delivery_Charge], [Bill_Print], [Bill_T]) VALUES (1, CAST(0x2E3E0B00 AS Date), CAST(0x0700000000000000 AS Time), N'DineIn', N'1', 2, 0, CAST(50.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 1, 1)
INSERT [dbo].[Bill_Master] ([Bill_No], [Bill_Date], [Bill_Time], [Bill_Type], [TableNo], [Person], [Customer_Code], [Discount], [Delivery_Charge], [Bill_Print], [Bill_T]) VALUES (2, CAST(0x2E3E0B00 AS Date), CAST(0x0700000000000000 AS Time), N'DineIn', N'7', 5, 3, CAST(50.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 1, 1)
INSERT [dbo].[Bill_Master] ([Bill_No], [Bill_Date], [Bill_Time], [Bill_Type], [TableNo], [Person], [Customer_Code], [Discount], [Delivery_Charge], [Bill_Print], [Bill_T]) VALUES (3, CAST(0x2E3E0B00 AS Date), CAST(0x0700000000000000 AS Time), N'DineIn', N'6', 1, 0, CAST(30.00 AS Numeric(18, 2)), CAST(50.00 AS Numeric(18, 2)), 1, 1)
INSERT [dbo].[Bill_Master] ([Bill_No], [Bill_Date], [Bill_Time], [Bill_Type], [TableNo], [Person], [Customer_Code], [Discount], [Delivery_Charge], [Bill_Print], [Bill_T]) VALUES (4, CAST(0x2E3E0B00 AS Date), CAST(0x0700000000000000 AS Time), N'DineIn', N'9', 2, 2, CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 1, NULL)
INSERT [dbo].[Bill_Master] ([Bill_No], [Bill_Date], [Bill_Time], [Bill_Type], [TableNo], [Person], [Customer_Code], [Discount], [Delivery_Charge], [Bill_Print], [Bill_T]) VALUES (5, CAST(0x2E3E0B00 AS Date), CAST(0x0700000000000000 AS Time), N'DineIn', N'7', 3, 6, CAST(20.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 1, NULL)
INSERT [dbo].[Bill_Master] ([Bill_No], [Bill_Date], [Bill_Time], [Bill_Type], [TableNo], [Person], [Customer_Code], [Discount], [Delivery_Charge], [Bill_Print], [Bill_T]) VALUES (6, CAST(0x2E3E0B00 AS Date), CAST(0x071E9AE700AC0000 AS Time), N'DineIn', N'', 0, 0, CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 0, NULL)
INSERT [dbo].[Bill_Master] ([Bill_No], [Bill_Date], [Bill_Time], [Bill_Type], [TableNo], [Person], [Customer_Code], [Discount], [Delivery_Charge], [Bill_Print], [Bill_T]) VALUES (7, CAST(0x2E3E0B00 AS Date), CAST(0x0700000000000000 AS Time), N'DineIn', N'4', 2, 7, CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 1, NULL)
INSERT [dbo].[Bill_Master] ([Bill_No], [Bill_Date], [Bill_Time], [Bill_Type], [TableNo], [Person], [Customer_Code], [Discount], [Delivery_Charge], [Bill_Print], [Bill_T]) VALUES (8, CAST(0x2E3E0B00 AS Date), CAST(0x0718520BDAAD0000 AS Time), N'DineIn', N'3', 2, 0, CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 0, NULL)
INSERT [dbo].[Bill_Master] ([Bill_No], [Bill_Date], [Bill_Time], [Bill_Type], [TableNo], [Person], [Customer_Code], [Discount], [Delivery_Charge], [Bill_Print], [Bill_T]) VALUES (9, CAST(0x2E3E0B00 AS Date), CAST(0x07FD76042DAE0000 AS Time), N'DineIn', N'2', 0, 0, CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 0, NULL)
INSERT [dbo].[Bill_Master] ([Bill_No], [Bill_Date], [Bill_Time], [Bill_Type], [TableNo], [Person], [Customer_Code], [Discount], [Delivery_Charge], [Bill_Print], [Bill_T]) VALUES (10, CAST(0x2E3E0B00 AS Date), CAST(0x0700000000000000 AS Time), N'DineIn', N'4', 0, 0, CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 0, NULL)
INSERT [dbo].[Bill_Master] ([Bill_No], [Bill_Date], [Bill_Time], [Bill_Type], [TableNo], [Person], [Customer_Code], [Discount], [Delivery_Charge], [Bill_Print], [Bill_T]) VALUES (11, CAST(0x2E3E0B00 AS Date), CAST(0x0700000000000000 AS Time), N'DineIn', N'3', 0, 0, CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 1, NULL)
INSERT [dbo].[Bill_Master] ([Bill_No], [Bill_Date], [Bill_Time], [Bill_Type], [TableNo], [Person], [Customer_Code], [Discount], [Delivery_Charge], [Bill_Print], [Bill_T]) VALUES (12, CAST(0x2F3E0B00 AS Date), CAST(0x0700000000000000 AS Time), N'DineIn', N'3', 0, 0, CAST(50.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 0, NULL)
INSERT [dbo].[Bill_Master] ([Bill_No], [Bill_Date], [Bill_Time], [Bill_Type], [TableNo], [Person], [Customer_Code], [Discount], [Delivery_Charge], [Bill_Print], [Bill_T]) VALUES (13, CAST(0x2F3E0B00 AS Date), CAST(0x0700000000000000 AS Time), N'DineIn', N'6', 7, 4, CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 0, NULL)
INSERT [dbo].[Bill_Master] ([Bill_No], [Bill_Date], [Bill_Time], [Bill_Type], [TableNo], [Person], [Customer_Code], [Discount], [Delivery_Charge], [Bill_Print], [Bill_T]) VALUES (14, CAST(0x303E0B00 AS Date), CAST(0x0700000000000000 AS Time), N'DineIn', N'5', 2, 2, CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 1, NULL)
INSERT [dbo].[Bill_Master] ([Bill_No], [Bill_Date], [Bill_Time], [Bill_Type], [TableNo], [Person], [Customer_Code], [Discount], [Delivery_Charge], [Bill_Print], [Bill_T]) VALUES (15, CAST(0x303E0B00 AS Date), CAST(0x0700000000000000 AS Time), N'DineIn', N'6', 0, 4, CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 0, NULL)
INSERT [dbo].[Bill_Master] ([Bill_No], [Bill_Date], [Bill_Time], [Bill_Type], [TableNo], [Person], [Customer_Code], [Discount], [Delivery_Charge], [Bill_Print], [Bill_T]) VALUES (16, CAST(0x303E0B00 AS Date), CAST(0x0700000000000000 AS Time), N'DineIn', N'7', 4, 2, CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 0, NULL)
INSERT [dbo].[Bill_Master] ([Bill_No], [Bill_Date], [Bill_Time], [Bill_Type], [TableNo], [Person], [Customer_Code], [Discount], [Delivery_Charge], [Bill_Print], [Bill_T]) VALUES (17, CAST(0x303E0B00 AS Date), CAST(0x0700000000000000 AS Time), N'DineIn', N'4', 2, 8, CAST(110.00 AS Numeric(18, 2)), CAST(50.00 AS Numeric(18, 2)), 1, NULL)
SET IDENTITY_INSERT [dbo].[Bill_Master] OFF
/****** Object:  Table [dbo].[User_Master]    Script Date: 05/12/2018 17:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User_Master](
	[User_Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Name] [varchar](50) NULL,
	[User_Password] [nvarchar](max) NULL,
	[User_Type] [varchar](50) NULL,
	[User_Guid] [nvarchar](max) NULL,
 CONSTRAINT [PK_User_Master] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[User_Master] ON
INSERT [dbo].[User_Master] ([User_Id], [User_Name], [User_Password], [User_Type], [User_Guid]) VALUES (1, N'A', N'BDC7C54C67DDB984B8D0D4C325C8CBA5B876BCA6', N'U', N'A0818882-0499-4DB2-8556-283BB68705AA')
SET IDENTITY_INSERT [dbo].[User_Master] OFF
/****** Object:  Table [dbo].[Item_Master]    Script Date: 05/12/2018 17:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Item_Master](
	[Item_Code] [int] IDENTITY(1,1) NOT NULL,
	[Item_Number] [int] NULL,
	[Item_Name] [varchar](50) NULL,
	[Short_Code] [varchar](50) NULL,
	[UnitPrice] [numeric](18, 2) NULL,
	[Category_Id] [int] NULL,
 CONSTRAINT [PK_Item_Master] PRIMARY KEY CLUSTERED 
(
	[Item_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Item_Master] ON
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (1, 1, N'Delight Pizza Small', N'1S', CAST(90.00 AS Numeric(18, 2)), 1)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (2, 1, N'Delight Pizza Medium', N'1M', CAST(120.00 AS Numeric(18, 2)), 1)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (3, 1, N'Delight Pizza Large', N'1L', CAST(180.00 AS Numeric(18, 2)), 1)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (4, 1, N'Delight Pizza XL', N'1XL', CAST(250.00 AS Numeric(18, 2)), 1)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (5, 2, N'Margherita Small', N'2S', CAST(90.00 AS Numeric(18, 2)), 1)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (6, 2, N'Margherita Medium', N'2M', CAST(120.00 AS Numeric(18, 2)), 1)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (7, 2, N'Margherita Large', N'2L', CAST(180.00 AS Numeric(18, 2)), 1)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (8, 2, N'Margherita XL', N'2XL', CAST(250.00 AS Numeric(18, 2)), 1)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (9, 3, N'Laziz Desi Small', N'3S', CAST(90.00 AS Numeric(18, 2)), 1)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (10, 3, N'Laziz Desi Medium', N'3M', CAST(120.00 AS Numeric(18, 2)), 1)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (11, 3, N'Laziz Desi Large', N'3L', CAST(180.00 AS Numeric(18, 2)), 1)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (12, 3, N'Laziz Desi XL', N'3XL', CAST(250.00 AS Numeric(18, 2)), 1)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (13, 4, N'Double Cheese Pizza Small', N'4S', CAST(120.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (14, 4, N'Double Cheese Pizza Medium', N'4M', CAST(220.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (15, 4, N'Double Cheese Pizza Large', N'4L', CAST(330.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (16, 4, N'Double Cheese Pizza XL', N'4XL', CAST(450.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (17, 5, N'Four Seasons Small', N'5S', CAST(120.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (18, 5, N'Four Seasons Medium', N'5M', CAST(220.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (19, 5, N'Four Seasons Large', N'5L', CAST(330.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (20, 5, N'Four Seasons XL', N'5XL', CAST(450.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (21, 6, N'Italian Lazizo Small', N'6S', CAST(120.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (22, 6, N'Italian Lazizo Medium', N'6M', CAST(220.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (23, 6, N'Italian Lazizo Large', N'6L', CAST(330.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (24, 6, N'Italian Lazizo XL', N'6XL', CAST(450.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (25, 7, N'Laziz Veg. Super small', N'7S', CAST(120.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (26, 7, N'Laziz Veg. Super Medium', N'7M', CAST(220.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (27, 7, N'Laziz Veg. Super Large', N'7L', CAST(330.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (28, 7, N'Laziz Veg. Super XL', N'7XL', CAST(450.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (29, 8, N'Mushroom Paprika Small', N'8S', CAST(120.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (30, 8, N'Mushroom Paprika Medium', N'8M', CAST(220.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (31, 8, N'Mushroom Paprika Large', N'8L', CAST(330.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (32, 8, N'Mushroom Paprika XL', N'8XL', CAST(450.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (33, 9, N'Creamy Cheese Pizza Small', N'9S', CAST(120.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (34, 9, N'Creamy Cheese Pizza Medium', N'9M', CAST(220.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (35, 9, N'Creamy Cheese Pizza Large', N'9L', CAST(330.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (36, 9, N'Creamy Cheese Pizza XL', N'9XL', CAST(450.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (37, 10, N'Jannat-E-Laziz Small', N'10S', CAST(120.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (38, 10, N'Jannat-E-Laziz Medium', N'10M', CAST(220.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (39, 10, N'Jannat-E-Laziz Large', N'10L', CAST(330.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (40, 10, N'Jannat-E-Laziz XL', N'10XL', CAST(450.00 AS Numeric(18, 2)), 2)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (41, 11, N'Laziz-E-All Rounder Small', N'11S', CAST(140.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (42, 11, N'Laziz-E-All Rounder Medium', N'11M', CAST(260.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (43, 11, N'Laziz-E-All Rounder Large', N'11L', CAST(380.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (44, 11, N'Laziz-E-All Rounder XL', N'11XL', CAST(490.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (45, 12, N'Veg. Sezwan Small', N'12S', CAST(140.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (46, 12, N'Veg. Sezwan Medium', N'12M', CAST(260.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (47, 12, N'Veg. Sezwan Large', N'12L', CAST(380.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (48, 12, N'Veg. Sezwan XL', N'12XL', CAST(490.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (49, 13, N'Veg. Maxican Small', N'13S', CAST(140.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (50, 13, N'Veg. Maxican Medium', N'13M', CAST(260.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (51, 13, N'Veg. Maxican Large', N'13L', CAST(380.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (52, 13, N'Veg. Maxican XL', N'13XL', CAST(490.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (53, 14, N'Veg. Spicy Fusion Small', N'14S', CAST(140.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (54, 14, N'Veg. Spicy Fusion Medium', N'14M', CAST(260.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (55, 14, N'Veg. Spicy Fusion Large', N'14L', CAST(380.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (56, 14, N'Veg. Spicy Fusion XL', N'14XL', CAST(490.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (57, 15, N'Paneer Tikka Small', N'15S', CAST(140.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (58, 15, N'Paneer Tikka Medium', N'15M', CAST(260.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (59, 15, N'Paneer Tikka Large', N'15L', CAST(380.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (60, 15, N'Paneer Tikka XL', N'15XL', CAST(490.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (61, 16, N'Chef''s Special Small', N'16S', CAST(140.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (62, 16, N'Chef''s Special Medium', N'16M', CAST(260.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (63, 16, N'Chef''s Special Large', N'16L', CAST(380.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (64, 16, N'Chef''s Special XL', N'16XL', CAST(490.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (65, 17, N'Paneer Paprika Small', N'17S', CAST(140.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (66, 17, N'Paneer Paprika Medium', N'17M', CAST(260.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (67, 17, N'Paneer Paprika Large', N'17L', CAST(380.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (68, 17, N'Paneer Paprika XL', N'17XL', CAST(490.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (69, 18, N'Fiery Panner Small', N'18S', CAST(140.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (70, 18, N'Fiery Panner Medium', N'18M', CAST(260.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (71, 18, N'Fiery Panner Large', N'18L', CAST(380.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (72, 18, N'Fiery Panner XL', N'18XL', CAST(490.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (73, 19, N'Veg. Carnival Small', N'19S', CAST(140.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (74, 19, N'Veg. Carnival Medium', N'19M', CAST(260.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (75, 19, N'Veg. Carnival Large', N'19L', CAST(380.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (76, 19, N'Veg. Carnival XL', N'19XL', CAST(490.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (77, 20, N'Friench Fries Pizza Small', N'20S', CAST(140.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (78, 20, N'Friench Fries Pizza Medium', N'20M', CAST(260.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (79, 20, N'Friench Fries Pizza Large', N'20L', CAST(380.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (80, 20, N'Friench Fries Pizza XL', N'20XL', CAST(490.00 AS Numeric(18, 2)), 3)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (81, 21, N'Veg. Cheese Toast', N'21', CAST(70.00 AS Numeric(18, 2)), 5)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (82, 22, N'Veg. Cheese Sandwich', N'22', CAST(50.00 AS Numeric(18, 2)), 5)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (83, 23, N'Veg. Sandwich', N'23', CAST(40.00 AS Numeric(18, 2)), 5)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (84, 24, N'Chocolate Sandwich', N'24', CAST(50.00 AS Numeric(18, 2)), 5)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (85, 25, N'Jam Cheese Sandwich', N'25', CAST(50.00 AS Numeric(18, 2)), 5)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (86, 26, N'Chatni Sandwich', N'26', CAST(40.00 AS Numeric(18, 2)), 5)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (87, 27, N'Pizza Grill', N'27', CAST(110.00 AS Numeric(18, 2)), 6)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (88, 28, N'Sweet Corn Grill', N'28', CAST(80.00 AS Numeric(18, 2)), 6)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (89, 29, N'Paneer Tikka Grill', N'29', CAST(100.00 AS Numeric(18, 2)), 6)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (90, 30, N'Laziz Veg Grill', N'30', CAST(80.00 AS Numeric(18, 2)), 6)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (91, 31, N'Cheesolizo Grill', N'31', CAST(70.00 AS Numeric(18, 2)), 6)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (92, 32, N'Chocolate Grill', N'32', CAST(70.00 AS Numeric(18, 2)), 6)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (93, 33, N'Laziz Spicy Grill', N'33', CAST(80.00 AS Numeric(18, 2)), 6)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (94, 34, N'Coleslaw Burger', N'34', CAST(40.00 AS Numeric(18, 2)), 7)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (95, 35, N'Veg. Burger', N'35', CAST(50.00 AS Numeric(18, 2)), 7)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (96, 36, N'Veg. Cheese Burger', N'36', CAST(70.00 AS Numeric(18, 2)), 7)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (97, 37, N'Double Cheese Burger', N'37', CAST(90.00 AS Numeric(18, 2)), 7)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (98, 38, N'Sezwan Burger', N'38', CAST(100.00 AS Numeric(18, 2)), 7)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (99, 39, N'Maxican Burger', N'39', CAST(100.00 AS Numeric(18, 2)), 7)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (100, 40, N'Monster Burger', N'40', CAST(140.00 AS Numeric(18, 2)), 7)
GO
print 'Processed 100 total records'
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (101, 41, N'Veg. Calzone', N'41', CAST(50.00 AS Numeric(18, 2)), 8)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (102, 42, N'Italian Calzone', N'42', CAST(80.00 AS Numeric(18, 2)), 8)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (103, 43, N'Hot & Spicy', N'43', CAST(90.00 AS Numeric(18, 2)), 8)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (104, 44, N'Paneer Tikka', N'44', CAST(100.00 AS Numeric(18, 2)), 8)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (105, 45, N'Garlic Bread with Cheese', N'45', CAST(80.00 AS Numeric(18, 2)), 9)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (106, 46, N'Garlic Bread with Jalapenos', N'46', CAST(90.00 AS Numeric(18, 2)), 9)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (107, 47, N'Spicy Garlic Bread', N'47', CAST(90.00 AS Numeric(18, 2)), 9)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (108, 48, N'Pizza Garlic Bread', N'48', CAST(110.00 AS Numeric(18, 2)), 9)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (109, 49, N'Garlic Bread Plater', N'49', CAST(160.00 AS Numeric(18, 2)), 9)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (110, 50, N'Garlic Sticks-Regular', N'50', CAST(60.00 AS Numeric(18, 2)), 9)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (111, 51, N'Spicy garlic Sticks', N'51', CAST(70.00 AS Numeric(18, 2)), 9)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (112, 52, N'Stuffed Garlic Sticks', N'52', CAST(80.00 AS Numeric(18, 2)), 9)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (113, 53, N'Stuffed Spicy Garlic Sticks', N'53', CAST(90.00 AS Numeric(18, 2)), 9)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (114, 54, N'French Fries Small', N'54S', CAST(40.00 AS Numeric(18, 2)), 9)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (115, 54, N'French Fries Medium', N'54M', CAST(70.00 AS Numeric(18, 2)), 9)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (116, 55, N'Masala French Fries Small', N'55S', CAST(50.00 AS Numeric(18, 2)), 9)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (117, 55, N'Masala French Fries Medium', N'55M', CAST(80.00 AS Numeric(18, 2)), 9)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (118, 56, N'Cheese French Fries Small', N'56S', CAST(60.00 AS Numeric(18, 2)), 9)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (119, 56, N'Cheese French Fries Medium', N'56M', CAST(90.00 AS Numeric(18, 2)), 9)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (120, NULL, N'Cheese Burst Pizza Medium', N'CPM', CAST(80.00 AS Numeric(18, 2)), 4)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (121, NULL, N'Cheese Burst Pizza Large', N'CPL', CAST(120.00 AS Numeric(18, 2)), 4)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (122, NULL, N'Cheese Burst Pizza XL', N'CPXL', CAST(170.00 AS Numeric(18, 2)), 4)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (123, NULL, N'Spicy Cheese Burst Pizza Medium', N'SCPM', CAST(90.00 AS Numeric(18, 2)), 4)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (124, NULL, N'Spicy Cheese Burst Pizza Large', N'SCPL', CAST(130.00 AS Numeric(18, 2)), 4)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (125, NULL, N'Spicy Cheese Burst Pizza XL', N'SCPXL', CAST(180.00 AS Numeric(18, 2)), 4)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (126, NULL, N'Extra Topping Small', N'ETS', CAST(20.00 AS Numeric(18, 2)), 4)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (127, NULL, N'Extra Topping Medium', N'ETM', CAST(30.00 AS Numeric(18, 2)), 4)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (128, NULL, N'Extra Topping Large', N'ETL', CAST(40.00 AS Numeric(18, 2)), 4)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (129, NULL, N'Extra Topping XL', N'ETXL', CAST(50.00 AS Numeric(18, 2)), 4)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (130, NULL, N'Combo1 Delight', N'C1D', CAST(100.00 AS Numeric(18, 2)), 10)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (131, NULL, N'Combo1 Classic', N'C1C', CAST(140.00 AS Numeric(18, 2)), 10)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (132, NULL, N'Combo1 Exotic', N'C1E', CAST(160.00 AS Numeric(18, 2)), 10)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (133, NULL, N'Combo2', N'C2', CAST(100.00 AS Numeric(18, 2)), 10)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (134, NULL, N'Combo3', N'C3', CAST(120.00 AS Numeric(18, 2)), 10)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (135, NULL, N'Combo4', N'C4', CAST(120.00 AS Numeric(18, 2)), 10)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (136, NULL, N'Combo5', N'C5', CAST(150.00 AS Numeric(18, 2)), 10)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (137, NULL, N'Combo6', N'C6', CAST(170.00 AS Numeric(18, 2)), 10)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (138, NULL, N'For 2 Person', N'2PN', CAST(279.00 AS Numeric(18, 2)), 10)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (139, NULL, N'For 4 Person', N'4PN', CAST(519.00 AS Numeric(18, 2)), 10)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (140, NULL, N'For 6 Person', N'6PN', CAST(749.00 AS Numeric(18, 2)), 10)
INSERT [dbo].[Item_Master] ([Item_Code], [Item_Number], [Item_Name], [Short_Code], [UnitPrice], [Category_Id]) VALUES (141, NULL, N'For 8 Person', N'8PN', CAST(1229.00 AS Numeric(18, 2)), 10)
SET IDENTITY_INSERT [dbo].[Item_Master] OFF
/****** Object:  Table [dbo].[Bill_Trans]    Script Date: 05/12/2018 17:28:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill_Trans](
	[DetailId] [int] IDENTITY(1,1) NOT NULL,
	[Bill_No] [int] NULL,
	[Item_Code] [int] NULL,
	[Qty] [int] NULL,
	[UnitPrice] [numeric](18, 2) NULL,
 CONSTRAINT [PK_Bill_Trans] PRIMARY KEY CLUSTERED 
(
	[DetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bill_Trans] ON
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (1, 1, 24, 1, CAST(450.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (2, 2, 11, 1, CAST(180.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (3, 2, 17, 1, CAST(120.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (4, 3, 1, 1, CAST(250.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (5, 4, 141, 1, CAST(1229.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (8, 5, 77, 1, CAST(140.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (9, 5, 93, 1, CAST(80.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (10, 5, 100, 1, CAST(140.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (11, 6, 24, 2, CAST(450.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (12, 7, 24, 3, CAST(450.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (13, 8, 12, 1, CAST(250.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (14, 9, 10, 1, CAST(120.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (15, 10, 93, 1, CAST(80.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (16, 11, 105, 1, CAST(80.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (17, 12, 79, 1, CAST(380.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (19, 13, 110, 1, CAST(60.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (20, 13, 116, 1, CAST(50.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (21, 12, 23, 1, CAST(330.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (23, 14, 79, 1, CAST(380.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (24, 14, 104, 1, CAST(100.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (25, 15, 93, 1, CAST(80.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (26, 15, 24, 1, CAST(450.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (27, 16, 117, 1, CAST(80.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (28, 16, 114, 1, CAST(40.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (29, 17, 31, 1, CAST(330.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (30, 17, 93, 1, CAST(80.00 AS Numeric(18, 2)))
INSERT [dbo].[Bill_Trans] ([DetailId], [Bill_No], [Item_Code], [Qty], [UnitPrice]) VALUES (31, 17, 113, 1, CAST(90.00 AS Numeric(18, 2)))
SET IDENTITY_INSERT [dbo].[Bill_Trans] OFF
/****** Object:  Default [DF_Bill_Master_Bill_Print]    Script Date: 05/12/2018 17:28:55 ******/
ALTER TABLE [dbo].[Bill_Master] ADD  CONSTRAINT [DF_Bill_Master_Bill_Print]  DEFAULT ((0)) FOR [Bill_Print]
GO
/****** Object:  ForeignKey [FK_Bill_Trans_Bill_Master]    Script Date: 05/12/2018 17:28:55 ******/
ALTER TABLE [dbo].[Bill_Trans]  WITH CHECK ADD  CONSTRAINT [FK_Bill_Trans_Bill_Master] FOREIGN KEY([Bill_No])
REFERENCES [dbo].[Bill_Master] ([Bill_No])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bill_Trans] CHECK CONSTRAINT [FK_Bill_Trans_Bill_Master]
GO
/****** Object:  ForeignKey [FK_Item_Master_Category_Master]    Script Date: 05/12/2018 17:28:55 ******/
ALTER TABLE [dbo].[Item_Master]  WITH CHECK ADD  CONSTRAINT [FK_Item_Master_Category_Master] FOREIGN KEY([Category_Id])
REFERENCES [dbo].[Category_Master] ([Category_Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Item_Master] CHECK CONSTRAINT [FK_Item_Master_Category_Master]
GO
