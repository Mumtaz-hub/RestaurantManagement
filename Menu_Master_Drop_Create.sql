USE [Restaurant]
GO

drop table menu_master
/****** Object:  Table [dbo].[Menu_Master]    Script Date: 06/22/2018 20:33:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Menu_Master](
	[Menu_Code] [int] IDENTITY(1,1) NOT NULL,
	[Menu_Name] [varchar](max) NULL,
	[Menu_Group] [varchar](50) NULL,
 CONSTRAINT [PK_Menu_Master] PRIMARY KEY CLUSTERED 
(
	[Menu_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Menu_Master] ON
INSERT [dbo].[Menu_Master] ([Menu_Code], [Menu_Name], [Menu_Group]) VALUES (1, N'Billing', N'Billing')
INSERT [dbo].[Menu_Master] ([Menu_Code], [Menu_Name], [Menu_Group]) VALUES (2, N'Item Category', N'Configuration')
INSERT [dbo].[Menu_Master] ([Menu_Code], [Menu_Name], [Menu_Group]) VALUES (3, N'Item Details', N'Configuration')
INSERT [dbo].[Menu_Master] ([Menu_Code], [Menu_Name], [Menu_Group]) VALUES (4, N'Customer', N'Configuration')
INSERT [dbo].[Menu_Master] ([Menu_Code], [Menu_Name], [Menu_Group]) VALUES (5, N'Menu Settings', N'Configuration')
INSERT [dbo].[Menu_Master] ([Menu_Code], [Menu_Name], [Menu_Group]) VALUES (6, N'User Settings', N'Configuration')
INSERT [dbo].[Menu_Master] ([Menu_Code], [Menu_Name], [Menu_Group]) VALUES (7, N'Transfer', N'Configuration')
INSERT [dbo].[Menu_Master] ([Menu_Code], [Menu_Name], [Menu_Group]) VALUES (8, N'Report', N'Report')
INSERT [dbo].[Menu_Master] ([Menu_Code], [Menu_Name], [Menu_Group]) VALUES (9, N'Backup Restore', N'BackupRestore')
SET IDENTITY_INSERT [dbo].[Menu_Master] OFF
