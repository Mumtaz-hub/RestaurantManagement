USE [Restaurant]
GO

/****** Object:  Table [dbo].[User_Rights]    Script Date: 06/22/2018 20:16:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User_Rights](
	[User_Id] [int] NOT NULL,
	[Menu_Code] [int] NOT NULL,
	[U_Right] [tinyint] NULL,
 CONSTRAINT [PK_User_Rights] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC,
	[Menu_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


