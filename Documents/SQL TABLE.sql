USE [estay_ecs_1210]
GO
/****** Object:  Table [dbo].[RateCodeControl]    Script Date: 2014/12/23 10:55:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RateCodeControl](
	[id] [int] IDENTITY(1000000,1) NOT NULL,
	[guid] [nvarchar](50) NULL,
	[hotelId] [int] NOT NULL,
	[rmType] [nvarchar](50) NOT NULL,
	[rateCode] [nvarchar](50) NOT NULL,
	[amendStatus] [nvarchar](50) NULL,
	[amendDays] [nvarchar](50) NULL,
	[cancelStatus] [nvarchar](50) NULL,
	[cancelDays] [nvarchar](50) NULL,
	[needPay] [nvarchar](50) NULL,
	[minStay] [nvarchar](50) NULL,
	[maxStay] [nvarchar](50) NULL,
	[booking] [nvarchar](50) NULL,
	[rmLimit] [nvarchar](50) NULL,
 CONSTRAINT [PK_RateCodeControl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RateCodeInfor]    Script Date: 2014/12/23 10:55:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RateCodeInfor](
	[id] [int] IDENTITY(1000000,1) NOT NULL,
	[guid] [nvarchar](50) NULL,
	[hotelId] [int] NOT NULL,
	[rateCode] [nvarchar](50) NOT NULL,
	[cName] [nvarchar](50) NULL,
	[eName] [nvarchar](50) NULL,
	[memberType] [nvarchar](50) NULL,
	[cardType] [nvarchar](50) NULL,
	[isContract] [nvarchar](50) NULL,
	[currency] [nvarchar](50) NULL,
	[rateCat] [nvarchar](50) NULL,
	[begDate] [datetime] NULL,
	[endDate] [datetime] NULL,
	[isdiscount] [nvarchar](50) NULL,
	[market] [nvarchar](50) NULL,
	[source] [nvarchar](50) NULL,
	[weekenddays] [nvarchar](50) NULL,
	[ptCoef] [int] NULL,
	[discountOf] [nvarchar](50) NULL,
	[discountType] [nvarchar](50) NULL,
	[discountValue] [int] NULL,
	[roundType] [nvarchar](50) NULL,
	[targetRound] [nvarchar](50) NULL,
	[status] [nvarchar](50) NULL,
	[defaultShow] [nvarchar](50) NULL,
	[sellBegDate] [datetime] NULL,
	[sellEndDate] [datetime] NULL,
	[bookingThrough] [nvarchar](50) NULL,
	[aliasCn] [nvarchar](50) NULL,
	[aliasEn] [nvarchar](50) NULL,
	[contractId] [int] NULL,
	[dailyinvallocation] [int] NULL,
	[descript] [nvarchar](50) NULL,
	[edescript] [nvarchar](50) NULL,
	[frtMarket] [nvarchar](50) NULL,
	[frtSource] [nvarchar](50) NULL,
	[isUpward] [nvarchar](50) NULL,
	[remarks] [nvarchar](50) NULL,
	[syncstatus] [nvarchar](50) NULL,
 CONSTRAINT [PK_RateCodeInfor] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoomRateWS]    Script Date: 2014/12/23 10:55:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomRateWS](
	[id] [int] IDENTITY(1000000,1) NOT NULL,
	[guid] [nvarchar](50) NULL,
	[hotelId] [int] NOT NULL,
	[rmTypeEName] [nvarchar](50) NULL,
	[breakfastEDesc] [nvarchar](50) NULL,
	[minVacRooms] [int] NULL,
	[rateCodeCName] [nvarchar](50) NULL,
	[rateCodeEName] [nvarchar](50) NULL,
	[rmTypeEDesc] [nvarchar](50) NULL,
	[ratePrice] [decimal](18, 2) NULL,
	[rateCode] [nvarchar](50) NULL,
	[rateDate] [datetime] NULL,
	[rmType] [nvarchar](50) NULL,
	[rmTypeCDesc] [nvarchar](50) NULL,
	[rmTypeCName] [nvarchar](50) NULL,
	[vacRooms] [int] NULL,
	[breakfastDesc] [nvarchar](50) NULL,
	[needPay] [int] NULL,
	[needGuarant] [int] NULL,
 CONSTRAINT [PK_RoomRateWS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
