using Library.Impl.Persistence.Sql;
using System;
using System.Net;
using System.Transactions;
using System.Web.Http;

namespace Web.Api.Controllers
{
    [RoutePrefix("api/sucursal")]
    public class SucursalController : ApiController
    {
        [HttpGet]
        [Route("search")]
        public IHttpActionResult Search(string nombre = null, bool? activo = null, string fecha = null)
        {
            try
            {
                var query = new Business.Query.Sucursal();

                if (nombre != null)
                    query.Nombre = (nombre, WhereOperator.Like);
                if (fecha != null)
                    query.Fecha = (Convert.ToDateTime(fecha), WhereOperator.Equals);
                if (activo != null)
                    query.Activo = (activo, WhereOperator.Equals);

                var list = query.List();
                if (list.result.Success)
                {
                    return Ok(new Business.Table.Sucursales().Load(list.domains)?.Datas?.Entities?.List);
                }

                return InternalServerError();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Search();
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var load = new Business.Table.Sucursal() { Id = id }.Load();
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
        public IHttpActionResult Create([FromBody]Entities.Table.Sucursal entity)
        {
            try
            {
                if (entity != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        var save = new Business.Table.Sucursal(entity).Save();
                        if (save.result.Success)
                        {
                            scope.Complete();

                            return Created<Entities.Table.Sucursal>($"{Request.RequestUri}/{save.domain?.Id?.ToString()}", save.domain?.Data?.Entity);
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

        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody]Entities.Table.Sucursal entity)
        {
            try
            {
                if (entity != null)
                {
                    var load = new Business.Table.Sucursal() { Id = id }.Load();
                    if (load.result.Success)
                    {
                        if (load.domain != null)
                        {
                            load.domain.Entity = entity;

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

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var load = new Business.Table.Sucursal() { Id = id }.Load();
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
