using library.Impl.Data;
using System;
using System.Net;
using System.Web.Http;

namespace Web.Api.Controllers
{
    public class EmpresaController : ApiController
    {
        public IHttpActionResult Get(string razonsocial = null, bool? activo = null)
        {
            try
            {
                var query = new domain.Query.Empresa();

                if (razonsocial != null)
                    query.Data["RazonSocial"]?.Where(razonsocial, WhereOperator.Like);
                if (activo != null)
                    query.Data["Activo"]?.Where(activo);

                return Ok(new domain.Model.Empresas().Load(query.List().domains).Datas.Entities);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                var load = new domain.Model.Empresa() { Id = id }.Load();
                if (load.result.Success)
                {
                    load.domain.Sucursales_Load();

                    return Ok(load.domain.Data.Entity);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]entities.Model.Empresa entity)
        {
            try
            {
                if (entity != null)
                {
                    var save = new domain.Model.Empresa(entity).Save();
                    if (save.result.Success)
                    {
                        return Created<entities.Model.Empresa>(Request.RequestUri + "/" + save.domain.Id.ToString(), save.domain.Data.Entity);
                    }
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Put(int id, [FromBody]entities.Model.Empresa entity)
        {
            try
            {
                if (entity != null)
                {
                    var load = new domain.Model.Empresa() { Id = id }.Load();
                    if (load.result.Success)
                    {
                        entity.Id = id;
                        load.domain.SetProperties(entity);

                        var save = load.domain.Save();
                        if (save.result.Success)
                        {
                            return Ok(save.domain.Data.Entity);
                        }
                    }
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                var erase = new domain.Model.Empresa() { Id = id }.Erase();
                if (erase.result.Success)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}