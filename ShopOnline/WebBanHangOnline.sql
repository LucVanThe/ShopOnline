USE [WebBanHangOnline]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 04/07/2025 8:28:47 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[Password] [nvarchar](255) NULL,
	[Salt] [nvarchar](255) NULL,
	[Active] [bit] NULL,
	[FullName] [nvarchar](100) NULL,
	[RoleID] [int] NULL,
	[LastLogin] [datetime] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK__Accounts__349DA586A049B599] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attributes]    Script Date: 04/07/2025 8:28:47 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attributes](
	[AttributeID] [int] NOT NULL,
	[Name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[AttributeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AttributesPrices]    Script Date: 04/07/2025 8:28:47 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttributesPrices](
	[AttributesPriceID] [int] IDENTITY(1,1) NOT NULL,
	[AttributeID] [int] NULL,
	[ProductID] [int] NULL,
	[Price] [float] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK__Attribut__699F045CA084E601] PRIMARY KEY CLUSTERED 
(
	[AttributesPriceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 04/07/2025 8:28:47 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CatID] [int] IDENTITY(1,1) NOT NULL,
	[CatName] [nvarchar](100) NULL,
	[Description] [nvarchar](255) NULL,
	[ParentID] [int] NULL,
	[Levels] [int] NULL,
	[Ordering] [int] NULL,
	[Published] [bit] NULL,
	[Thumb] [nvarchar](255) NULL,
	[Title] [nvarchar](255) NULL,
	[Alias] [nvarchar](255) NULL,
	[MetaDesc] [nvarchar](255) NULL,
	[MetaKey] [nvarchar](255) NULL,
	[Cover] [nvarchar](255) NULL,
	[SchemaMarkup] [nvarchar](max) NULL,
 CONSTRAINT [PK__Categori__6A1C8ADA2C670CB0] PRIMARY KEY CLUSTERED 
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 04/07/2025 8:28:47 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](100) NULL,
	[Birthday] [date] NULL,
	[Avatar] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[Email] [nvarchar](100) NULL,
	[Phone] [nvarchar](20) NULL,
	[LocationID] [int] NULL,
	[District] [nvarchar](50) NULL,
	[Ward] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[Password] [nvarchar](255) NULL,
	[Salt] [nvarchar](255) NULL,
	[LastLogin] [datetime] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK__Customer__A4AE64B8EA1F152A] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 04/07/2025 8:28:47 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[LocationID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Type] [nvarchar](50) NULL,
	[Slug] [nvarchar](255) NULL,
	[NameWithType] [nvarchar](255) NULL,
	[PathWithType] [nvarchar](255) NULL,
	[ParentCode] [nvarchar](20) NULL,
	[Levels] [int] NULL,
 CONSTRAINT [PK__Location__E7FEA4779700173A] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 04/07/2025 8:28:47 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[ProductID] [int] NULL,
	[OrderNumber] [nvarchar](50) NULL,
	[Quantity] [int] NULL,
	[Discount] [float] NULL,
	[Total] [float] NULL,
	[ShipDate] [datetime] NULL,
 CONSTRAINT [PK__OrderDet__D3B9D30C26BDBF36] PRIMARY KEY CLUSTERED 
(
	[OrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 04/07/2025 8:28:47 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[OrderDate] [datetime] NULL,
	[ShipDate] [datetime] NULL,
	[TransactStatusID] [int] NULL,
	[Deleted] [bit] NULL,
	[Paid] [bit] NULL,
	[PaymentDate] [datetime] NULL,
	[PaymentID] [int] NULL,
	[Note] [nvarchar](max) NULL,
 CONSTRAINT [PK__Orders__C3905BAFBFC9CC85] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pages]    Script Date: 04/07/2025 8:28:47 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pages](
	[PageID] [int] IDENTITY(1,1) NOT NULL,
	[PageName] [nvarchar](100) NULL,
	[Contents] [nvarchar](max) NULL,
	[Thumb] [nvarchar](255) NULL,
	[Publish] [bit] NULL,
	[Title] [nvarchar](255) NULL,
	[MetaDesc] [nvarchar](255) NULL,
	[MetaKey] [nvarchar](255) NULL,
	[Alias] [nvarchar](255) NULL,
	[CreatedDate] [datetime] NULL,
	[Ordering] [int] NULL,
 CONSTRAINT [PK__Pages__C565B1243D70907E] PRIMARY KEY CLUSTERED 
(
	[PageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 04/07/2025 8:28:47 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](100) NULL,
	[ShortDesc] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[CatID] [int] NULL,
	[Price] [float] NULL,
	[Discount] [float] NULL,
	[Thumb] [nvarchar](255) NULL,
	[Video] [nvarchar](255) NULL,
	[DateCreated] [datetime] NULL,
	[DateModified] [datetime] NULL,
	[BestSellers] [bit] NULL,
	[HomeFlag] [bit] NULL,
	[Active] [bit] NULL,
	[Tags] [nvarchar](255) NULL,
	[Title] [nvarchar](255) NULL,
	[Alias] [nvarchar](255) NULL,
	[MetaDesc] [nvarchar](255) NULL,
	[MetaKey] [nvarchar](255) NULL,
	[UnitInStock] [int] NULL,
 CONSTRAINT [PK__Products__B40CC6ED4B7C9681] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 04/07/2025 8:28:47 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] NOT NULL,
	[RoleName] [nvarchar](50) NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [PK__Roles__8AFACE3AEADD50C3] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactStatus]    Script Date: 04/07/2025 8:28:47 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactStatus](
	[TransactStatusID] [int] NOT NULL,
	[Status] [nvarchar](50) NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [PK__Transact__C8BCD2765B77AD06] PRIMARY KEY CLUSTERED 
(
	[TransactStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Roles]
GO
ALTER TABLE [dbo].[AttributesPrices]  WITH CHECK ADD  CONSTRAINT [FK__Attribute__Attri__5070F446] FOREIGN KEY([AttributeID])
REFERENCES [dbo].[Attributes] ([AttributeID])
GO
ALTER TABLE [dbo].[AttributesPrices] CHECK CONSTRAINT [FK__Attribute__Attri__5070F446]
GO
ALTER TABLE [dbo].[AttributesPrices]  WITH CHECK ADD  CONSTRAINT [FK__Attribute__Produ__5165187F] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[AttributesPrices] CHECK CONSTRAINT [FK__Attribute__Produ__5165187F]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK__OrderDeta__Order__3F466844] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK__OrderDeta__Order__3F466844]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK__Orders__Customer__3B75D760] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK__Orders__Customer__3B75D760]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK__Orders__Transact__3C69FB99] FOREIGN KEY([TransactStatusID])
REFERENCES [dbo].[TransactStatus] ([TransactStatusID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK__Orders__Transact__3C69FB99]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CatID])
REFERENCES [dbo].[Categories] ([CatID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
