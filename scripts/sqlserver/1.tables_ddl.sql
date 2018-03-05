USE [test]
GO

-----------------------------

CREATE TABLE [dbo].[empresa](
	[razon_social] [varchar](100) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[activo] [bit] NULL,
 CONSTRAINT [PK_empresa_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


----------------


CREATE TABLE [dbo].[sucursal](
	[nombre] [varchar](100) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_empresa] [int] NULL,
	[fecha] [datetime] NULL,
	[activo] [bit] NULL,
 CONSTRAINT [PK_sucursal_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


ALTER TABLE [dbo].[sucursal]  WITH CHECK ADD  CONSTRAINT [FK_sucursal_empresa] FOREIGN KEY([id_empresa])
REFERENCES [dbo].[empresa] ([id])
GO

ALTER TABLE [dbo].[sucursal] CHECK CONSTRAINT [FK_sucursal_empresa]
GO

------------------------------