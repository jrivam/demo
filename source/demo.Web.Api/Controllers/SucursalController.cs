using Autofac;
using jrivam.Library;
using jrivam.Library.Impl;
using jrivam.Library.Impl.Persistence.Sql;
using System;
using System.Net;
using System.Transactions;
using System.Web.Http;

namespace demo.Web.Api.Controllers
{
    [RoutePrefix("api/sucursal")]
    public class SucursalController : ApiController
    {
        [HttpGet]
        [Route(nameof(Search))]
        public IHttpActionResult Search(string nombre = null, bool? activo = null, string fecha = null)
        {
            try
            {
                var query = AutofacConfiguration.Container.Resolve<Business.Query.Sucursal>();

                if (nombre != null)
                    query.Nombre = (nombre, WhereOperator.Like);
                if (fecha != null)
                    query.Fecha = (Convert.ToDateTime(fecha), WhereOperator.Equals);
                if (activo != null)
                    query.Activo = (activo, WhereOperator.Equals);

                var list = query.List();
                if (list.result.Success)
                {
                    return Ok(new Business.Table.Sucursales().Load(list.domains)?.Datas?.Entities);
                }

                if (list.result.Exception == null)
                    return BadRequest(list.result.GetMessagesAsString(x => x.Category == (x.Category & ResultCategory.Error)));
                throw list.result.Exception;
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
            if (id > 0)
            {
                try
                {
                    var sucursal = AutofacConfiguration.Container.Resolve<Business.Table.Sucursal>();
                    sucursal.Id = id;

                    var load = sucursal.Load();
                    if (load.result.Success)
                    {
                        if (load.domain != null)
                        {
                            return Ok(load.domain?.Data?.Entity);
                        }

                        return NotFound();
                    }

                    if (load.result.Exception == null)
                        return BadRequest(load.result.GetMessagesAsString(x => x.Category == (x.Category & ResultCategory.Error)));
                    throw load.result.Exception;
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }

            return BadRequest("Id should be greater than 0");
        }

        [HttpPost]
        public IHttpActionResult Create([FromBody] Entities.Table.Sucursal entity)
        {
            if (entity != null)
            {
                try
                {
                    using (var scope = new TransactionScope())
                    {
                        var save = AutofacConfiguration.Container.Resolve<Business.Table.Sucursal>(new TypedParameter(typeof(Entities.Table.Sucursal), entity)).Save();
                        if (save.result.Success)
                        {
                            scope.Complete();

                            return Created($"{Request.RequestUri}/{save.domain?.Id?.ToString()}", save.domain?.Data?.Entity);
                        }

                        if (save.result.Exception == null)
                            return BadRequest(save.result.GetMessagesAsString(x => x.Category == (x.Category & ResultCategory.Error)));
                        throw save.result.Exception;
                    }
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }

            return BadRequest("Entity cannot be null");
        }

        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody] Entities.Table.Sucursal entity)
        {
            if (id > 0)
            {
                try
                {
                    var sucursal = AutofacConfiguration.Container.Resolve<Business.Table.Sucursal>();
                    sucursal.Id = id;

                    var load = sucursal.Load();
                    if (load.result.Success)
                    {
                        if (load.domain != null)
                        {
                            load.domain.Data.Entity = entity;

                            using (var scope = new TransactionScope())
                            {
                                var save = load.domain.Save();
                                if (save.result.Success)
                                {
                                    scope.Complete();

                                    return Ok(save.domain?.Data?.Entity);
                                }

                                if (save.result.Exception == null)
                                    return BadRequest(save.result.GetMessagesAsString(x => x.Category == (x.Category & ResultCategory.Error)));
                                throw save.result.Exception;
                            }
                        }

                        return NotFound();
                    }

                    if (load.result.Exception == null)
                        return BadRequest(load.result.GetMessagesAsString(x => x.Category == (x.Category & ResultCategory.Error)));
                    throw load.result.Exception;
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }

            return BadRequest("Id should be greater than 0");
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id > 0)
            {
                try
                {
                    var sucursal = AutofacConfiguration.Container.Resolve<Business.Table.Sucursal>();
                    sucursal.Id = id;

                    var load = sucursal.Load();
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

                                if (erase.result.Exception == null)
                                    return BadRequest(erase.result.GetMessagesAsString(x => x.Category == (x.Category & ResultCategory.Error)));
                                throw erase.result.Exception;
                            }
                        }

                        return NotFound();
                    }

                    if (load.result.Exception == null)
                        return BadRequest(load.result.GetMessagesAsString(x => x.Category == (x.Category & ResultCategory.Error)));
                    throw load.result.Exception;
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }

            return BadRequest("Id should be greater than 0");
        }
    }
}
