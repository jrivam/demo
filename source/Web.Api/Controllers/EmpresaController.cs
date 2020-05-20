using Library.Impl.Persistence.Sql;
using System;
using System.Net;
using System.Transactions;
using System.Web.Http;

namespace Web.Api.Controllers
{
    [RoutePrefix("api/empresa")]
    public class EmpresaController : ApiController
    {
        [HttpGet]
        [Route("search")]
        public IHttpActionResult Search(string razonsocial = null, bool? activo = null)
        {
            try
            {
                var query = new Business.Query.Empresa();

                if (razonsocial != null)
                    query.RazonSocial = (razonsocial, WhereOperator.Like);
                if (activo != null)
                    query.Activo = (activo, WhereOperator.Equals);

                var list = query.List();
                if (list.result.Success)
                {
                    return Ok(new Business.Table.Empresas().Load(list.domains)?.Datas?.Entities);
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
                var load = new Business.Table.Empresa() { Id = id }.Load();
                if (load.result.Success)
                {
                    if (load.domain != null)
                    {
                        load.domain.Sucursales.Refresh();

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
        public IHttpActionResult Create([FromBody]Entities.Table.Empresa entity)
        {
            try
            {
                if (entity != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        var save = new Business.Table.Empresa(entity).Save();
                        if (save.result.Success)
                        {
                            scope.Complete();

                            return Created<Entities.Table.Empresa>($"{Request.RequestUri}/{save.domain?.Id?.ToString()}", save.domain?.Data?.Entity);
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
        public IHttpActionResult Update(int id, [FromBody]Entities.Table.Empresa entity)
        {
            try
            {
                if (entity != null)
                {
                    var load = new Business.Table.Empresa() { Id = id }.Load();
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
                var load = new Business.Table.Empresa() { Id = id }.Load();
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