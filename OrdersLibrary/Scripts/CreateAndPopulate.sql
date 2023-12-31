/******
Instructions

Best done in SSMS (SQL-Server Managment Studio)

1. Create  Database named FluentScheduler 
2. Run the script below
******/

USE [FluentScheduler]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 10/21/2023 10:40:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [date] NULL,
	[OrderTime] [time](7) NULL,
	[OrderIsNew] [bit] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [OrderDate], [OrderTime], [OrderIsNew]) VALUES (1, CAST(N'2023-10-19' AS Date), CAST(N'13:00:00' AS Time), 1)
INSERT [dbo].[Orders] ([Id], [OrderDate], [OrderTime], [OrderIsNew]) VALUES (2, CAST(N'2023-10-21' AS Date), CAST(N'14:15:00' AS Time), 0)
INSERT [dbo].[Orders] ([Id], [OrderDate], [OrderTime], [OrderIsNew]) VALUES (3, CAST(N'2023-10-21' AS Date), CAST(N'15:30:00' AS Time), 0)
INSERT [dbo].[Orders] ([Id], [OrderDate], [OrderTime], [OrderIsNew]) VALUES (4, CAST(N'2023-10-21' AS Date), CAST(N'03:37:04' AS Time), 0)
INSERT [dbo].[Orders] ([Id], [OrderDate], [OrderTime], [OrderIsNew]) VALUES (5, CAST(N'2023-10-21' AS Date), CAST(N'03:40:56' AS Time), 0)
INSERT [dbo].[Orders] ([Id], [OrderDate], [OrderTime], [OrderIsNew]) VALUES (6, CAST(N'2023-10-21' AS Date), CAST(N'03:41:03' AS Time), 1)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Orders', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date of order' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Orders', @level2type=N'COLUMN',@level2name=N'OrderDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Time of order' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Orders', @level2type=N'COLUMN',@level2name=N'OrderTime'
GO
USE [master]
GO
ALTER DATABASE [FluentScheduler] SET  READ_WRITE 
GO
