USE [test]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-----------------



CREATE PROCEDURE [dbo].[gsp_empresa_insert] 
		@EmpresaRazonSocial varchar(100), 
		@EmpresaActivo bit
AS
BEGIN

	insert into empresa 
	(
		razon_social,
			activo
		)values(
		@EmpresaRazonSocial,
			@EmpresaActivo
	);

	SELECT SCOPE_IDENTITY();
		
END
GO

--------------


CREATE PROCEDURE [dbo].[gsp_empresa_select]	
	@EmpresaId int
AS
BEGIN
	SET NOCOUNT ON;
	
	select
	empresa.id as [Empresa.Id],
	empresa.razon_social as [Empresa.RazonSocial],
	empresa.activo as [Empresa.Activo]
	from empresa
	where empresa.id = @EmpresaId
	
END
GO

----------


CREATE PROCEDURE [dbo].[gsp_empresa_update] 
		@EmpresaId int,
		@EmpresaRazonSocial varchar(100), 
		@EmpresaActivo bit
AS
BEGIN

		update empresa set
		razon_social = @EmpresaRazonSocial,
			activo = @EmpresaActivo
		where id = @EmpresaId ;		
END
GO

-------

CREATE PROCEDURE [dbo].[gsp_empresa_delete]	
	@EmpresaId int
AS
BEGIN

	delete
	from empresa 
	where empresa.id = @EmpresaId
	
END
GO



--------------------


CREATE PROCEDURE [dbo].[gsp_sucursal_insert] 
		@SucursalNombre varchar(100), 
		@SucursalActivo bit, 
		@SucursalFecha datetime, 
		@SucursalIdEmpresa int
AS
BEGIN
	
	insert into sucursal 
	(
		nombre,
			activo,
			fecha,
			id_empresa
		)values(
		@SucursalNombre,
			@SucursalActivo,
			@SucursalFecha,
			@SucursalIdEmpresa
	);

	SELECT SCOPE_IDENTITY();
		
END
GO

--------------


CREATE PROCEDURE [dbo].[gsp_sucursal_select]	
	@SucursalId int
AS
BEGIN
	SET NOCOUNT ON;
	
	select
	sucursal.id as [Sucursal.Id],
	sucursal.nombre as [Sucursal.Nombre],
	sucursal.id_empresa as [Sucursal.IdEmpresa],
	sucursal.fecha as [Sucursal.Fecha],
	sucursal.activo as [Sucursal.Activo]
	from sucursal
	where sucursal.id = @SucursalId
	
END
GO

----------


CREATE PROCEDURE [dbo].[gsp_sucursal_update] 
		@SucursalId int,
		@SucursalNombre varchar(100), 
		@SucursalActivo bit, 
		@SucursalFecha datetime, 
		@SucursalIdEmpresa int
AS
BEGIN

		update sucursal set
		nombre = @SucursalNombre,
			activo = @SucursalActivo,
			fecha = @SucursalFecha,
			id_empresa = @SucursalIdEmpresa
		where id = @SucursalId ;		
END
GO

-------

CREATE PROCEDURE [dbo].[gsp_sucursal_delete]	
	@SucursalId int
AS
BEGIN
	
	delete
	from sucursal 
	where sucursal.id = @SucursalId
	
END
GO