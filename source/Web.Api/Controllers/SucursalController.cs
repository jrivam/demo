using library.Impl.Data.Sql;
using System;
using System.Net;
using System.Transactions;
using System.Web.Http;

namespace Web.Api.Controllers
{
    public class SucursalController : ApiController
    {
        public IHttpActionResult Get(string nombre = null, bool? activo = null, string fecha = null)
        {
            try
            {
                var query = new domain.Query.Sucursal();

                if (nombre != null)
                    query.Nombre = (nombre, WhereOperator.Like);
                if (fecha != null)
                    query.Fecha = (Convert.ToDateTime(fecha), WhereOperator.Equals);
                if (activo != null)
                    query.Activo = (activo, WhereOperator.Equals);

                var list = query.List();
                if (list.result.Success)
                {
                    return Ok(new domain.Model.Sucursales().Load(list.domains)?.Datas?.Entities);
                }

                return InternalServerError();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                var load = new domain.Model.Sucursal() { Id = id }.Load();
                if (load.result.Success)
                {
                    if (load.domain != null)
                    {
                        return Ok(load.domain?.Data?.Entity);
                    }

                    return NotFound();
                }

                return InternalServerError();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]entities.Model.Sucursal entity)
        {
            try
            {
                if (entity != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        var save = new domain.Model.Sucursal(entity).Save();
                        if (save.result.Success)
                        {
                            scope.Complete();

                            return Created<entities.Model.Sucursal>($"{Request.RequestUri}/{save.domain?.Id?.ToString()}", save.domain?.Data?.Entity);
                        }

                        return InternalServerError();
                    }
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Put(int id, [FromBody]entities.Model.Sucursal entity)
        {
            try
            {
                if (entity != null)
                {
                    var load = new domain.Model.Sucursal() { Id = id }.Load();
                    if (load.result.Success)
                    {
                        if (load.domain != null)
                        {
                            load.domain?.SetProperties(entity);

                            using (var scope = new TransactionScope())
                            {
                                var save = load.domain.Save();
                                if (save.result.Success)
                                {
                                    scope.Complete();

                                    return Ok(save.domain?.Data?.Entity);
                                }

                                return InternalServerError();
                            }
                        }

                        return NotFound();
                    }

                    return InternalServerError();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                var load = new domain.Model.Sucursal() { Id = id }.Load();
                if (load.result.Success)
                {
                    if (load.domain != null)
                    {
                        using (var scope = new TransactionScope())
                        {
                            var erase = load.domain.Erase();
                            if (erase.result.Success)
                            {
                                scope.Complete();

                                return StatusCode(HttpStatusCode.NoContent);
                            }

                            return InternalServerError();
                        }
                    }

                    return NotFound();
                }

                return InternalServerError();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
