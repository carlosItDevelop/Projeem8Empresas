USE [newprojeem_iv_consolidado]
GO
/****** Object:  Table [dbo].[id_consolidado]    Script Date: 04/25/2012 15:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[id_consolidado]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[id_consolidado](
	[id] [int] IDENTITY(3,1) NOT NULL,
	[inutil] [nchar](1) NOT NULL,
 CONSTRAINT [PK_id_consolidado] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[consolidado]    Script Date: 04/25/2012 15:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[consolidado]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[consolidado](
	[id] [int] NOT NULL,
	[referencia] [nchar](6) NOT NULL,
	[dt_fechamento] [date] NOT NULL,
	[MaxSb] [int] NOT NULL,
	[saldo] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_consolidado] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[consolidado] ([id], [referencia], [dt_fechamento], [MaxSb], [saldo]) VALUES (13, N'201204', CAST(0x98350B00 AS Date), 4, CAST(1288.82 AS Decimal(18, 2)))
INSERT [dbo].[consolidado] ([id], [referencia], [dt_fechamento], [MaxSb], [saldo]) VALUES (14, N'201204', CAST(0x98350B00 AS Date), 4, CAST(-749.04 AS Decimal(18, 2)))
INSERT [dbo].[consolidado] ([id], [referencia], [dt_fechamento], [MaxSb], [saldo]) VALUES (15, N'201204', CAST(0x98350B00 AS Date), 5, CAST(-2530.60 AS Decimal(18, 2)))
INSERT [dbo].[consolidado] ([id], [referencia], [dt_fechamento], [MaxSb], [saldo]) VALUES (16, N'201204', CAST(0x98350B00 AS Date), 4, CAST(-2304.88 AS Decimal(18, 2)))
INSERT [dbo].[consolidado] ([id], [referencia], [dt_fechamento], [MaxSb], [saldo]) VALUES (17, N'201204', CAST(0x99350B00 AS Date), 3, CAST(615.92 AS Decimal(18, 2)))
INSERT [dbo].[consolidado] ([id], [referencia], [dt_fechamento], [MaxSb], [saldo]) VALUES (18, N'201204', CAST(0x99350B00 AS Date), 3, CAST(52.10 AS Decimal(18, 2)))
INSERT [dbo].[consolidado] ([id], [referencia], [dt_fechamento], [MaxSb], [saldo]) VALUES (19, N'201204', CAST(0x99350B00 AS Date), 3, CAST(801.61 AS Decimal(18, 2)))
INSERT [dbo].[consolidado] ([id], [referencia], [dt_fechamento], [MaxSb], [saldo]) VALUES (20, N'201204', CAST(0x99350B00 AS Date), 3, CAST(146.07 AS Decimal(18, 2)))
/****** Object:  StoredProcedure [dbo].[AddConsolidado]    Script Date: 04/25/2012 15:32:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddConsolidado]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Carlos Alberto dos Santos>
-- Create date: <18-04-2012>
-- Description:	<Grava o consolidado de cada aplicação>
-- ====================================================
CREATE PROCEDURE [dbo].[AddConsolidado] 
	@id int,
	@referencia nvarchar(6),
	@dt_fechamento date,
	@MaxSb int,
	@saldo decimal(18,2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO consolidado (id, referencia, dt_fechamento, MaxSb, saldo) 
    values (@id, @referencia, @dt_fechamento, @MaxSb, @saldo);
	
	
END
' 
END
GO
