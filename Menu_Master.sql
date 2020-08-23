USE [Restaurant]
GO

/****** Object:  Table [dbo].[Menu_Master]    Script Date: 06/22/2018 20:17:22 ******/
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


