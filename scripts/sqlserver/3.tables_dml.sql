USE [test]
GO

------------------------------

INSERT INTO [dbo].[empresa]
           ([razon_social]
           ,[activo])
     VALUES
           ('empresa 1', 1)
GO


INSERT INTO [dbo].[empresa]
           ([razon_social]
           ,[activo])
     VALUES
           ('empresa 2', 1)
GO

--------------

INSERT INTO [dbo].[sucursal]
           ([nombre]
           ,[id_empresa]
           ,[fecha]
           ,[activo])
     VALUES
           ('sucursal 1'
           ,(select id from empresa where razon_social = 'empresa 1')
           ,getdate()
           ,1)
GO


INSERT INTO [dbo].[sucursal]
           ([nombre]
           ,[id_empresa]
           ,[fecha]
           ,[activo])
     VALUES
           ('sucursal 2'
           ,(select id from empresa where razon_social = 'empresa 2')
           ,getdate()
           ,1)
GO

---------------------------------------
