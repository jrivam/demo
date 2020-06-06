USE [test]
GO

------------------------------

INSERT INTO [dbo].[empresa]
           (
            [razon_social]
           ,[ruc]
            ,[activo])
     VALUES
           ('empresa 1', '11111111111', 1)
GO


INSERT INTO [dbo].[empresa]
           (
            [razon_social]
           ,[ruc]
           ,[activo])
     VALUES
           ('empresa 2', '222222222222', 1)
GO

--------------

INSERT INTO [dbo].[sucursal]
           (
            [nombre]
           ,[codigo]
           ,[id_empresa]
           ,[fecha]
           ,[activo])
     VALUES
           ('sucursal 1', 's1'
           ,(select id from empresa where ruc = '11111111111')
           ,getdate()
           ,1)
GO


INSERT INTO [dbo].[sucursal]
           ([nombre]
           ,[codigo]           
           ,[id_empresa]
           ,[fecha]
           ,[activo])
     VALUES
           ('sucursal 2', 's2',
           ,(select id from empresa where ruc = '222222222222')
           ,getdate()
           ,1)
GO

---------------------------------------
