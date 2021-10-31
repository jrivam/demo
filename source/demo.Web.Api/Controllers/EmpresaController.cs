using Autofac;
using jrivam.Library;
using jrivam.Library.Impl;
using jrivam.Library.Impl.Persistence.Sql;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http;

namespace demo.Web.Api.Controllers
{
    [RoutePrefix("api/empresa")]
    public class EmpresaController : ApiController
    {
        [HttpGet]
        [Route(nameof(Search))]
        public async Task<IHttpActionResult> Search(string razonsocial = null, bool? activo = null)
        {
            try
            {
                var query = AutofacConfiguration.Container.Resolve<Business.Query.Empresa>();

                if (razonsocial != null)
                    query.RazonSocial = (razonsocial, WhereOperator.Like);
                if (activo != null)
                    query.Activo = (activo, WhereOperator.Equals);

                var list = await query.ListAsync();
                if (list.result.Success)
                {
                    return Ok(new Business.Table.Empresas().Load(list.domains)?.Datas?.Entities);
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
        public async Task<IHttpActionResult> GetAll()
        {
            return await Search();
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            if (id > 0)
            {
                try
                {
                    var empresa = AutofacConfiguration.Container.Resolve<Business.Table.Empresa>();
                    empresa.Id = id;

                    var load = await empresa.LoadAsync();
                    if (load.result.Success)
                    {
                        if (load.domain != null)
                        {
                            await load.domain.Sucursales.RefreshAsync();

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
        public async Task<IHttpActionResult> Create([FromBody] Entities.Table.Empresa entity)
        {
            if (entity != null)
            {
                try
                {
                    using (var scope = new TransactionScope())
                    {
                        var save = await AutofacConfiguration.Container.Resolve<Business.Table.Empresa>(new TypedParameter(typeof(Entities.Table.Empresa), entity)).SaveAsync();
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
        public async Task<IHttpActionResult> Update(int id, [FromBody] Entities.Table.Empresa entity)
        {
            if (id > 0)
            {
                try
                {
                    var empresa = AutofacConfiguration.Container.Resolve<Business.Table.Empresa>();
                    empresa.Id = id;

                    var load = await empresa.LoadAsync();
                    if (load.result.Success)
                    {
                        if (load.domain != null)
                        {
                            load.domain.Data.Entity = entity;

                            using (var scope = new TransactionScope())
                            {
                                var save = await load.domain.SaveAsync();
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
        public async Task<IHttpActionResult> Delete(int id)
        {
            if (id > 0)
            {
                try
                {
                    var empresa = AutofacConfiguration.Container.Resolve<Business.Table.Empresa>();
                    empresa.Id = id;

                    var load = await empresa.LoadAsync();
                    if (load.result.Success)
                    {
                        if (load.domain != null)
                        {
                            using (var scope = new TransactionScope())
                            {
                                var erase = await load.domain.EraseAsync();
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