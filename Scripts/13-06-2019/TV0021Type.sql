USE [DBDies_Monaco]
GO

/****** Object:  UserDefinedTableType [dbo].[TV0021Type]    Script Date: 13/06/2019 5:46:30 ******/
DROP TYPE [dbo].[TV0021Type]
GO

/****** Object:  UserDefinedTableType [dbo].[TV0021Type]    Script Date: 13/06/2019 5:46:31 ******/
CREATE TYPE [dbo].[TV0021Type] AS TABLE(
	[vdnumi] [int] NOT NULL,
	[vdvc2numi] [int] NULL,
	[vdserv] [int] NULL,
	[edcant] int NULL,
	[servicio] [nvarchar](200) NULL,
	[vdcmin] [decimal](18, 2) NULL,
	[vdpbas] [decimal](18, 2) NULL,
	[vdptot] [decimal](18, 2) NULL,
	[vdporc] [decimal](18, 2) NULL,
	[vddesc] [decimal](18, 2) NULL,
	[vdtotdesc] [decimal](18, 2) NULL,
	[vdobs] [nvarchar](30) NULL,
	[vdpcos] [decimal](18, 2) NULL,
	[vdptot2] [decimal](18, 2) NULL,
	[estado] [int] NOT NULL
)
GO


