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

                return Ok(new domain.Model.Empresas().Load(query.List().domains).Datas.Domains);
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
        public IHttpActionResult Post([FromBody]domain.Model.Empresa domain)
        {
            try
            {
                if (domain == null)
                    return BadRequest();

                var save = domain.Save();
                if (save.result.Success)
                {
                    return Created<entities.Model.Empresa>(Request.RequestUri + "/" + save.domain.Id.ToString(), save.domain.Data.Entity);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Put(int id, [FromBody]domain.Model.Empresa domain)
        {
            try
            {
                if (domain == null)
                    return BadRequest();

                var save = domain.Save();
                if (save.result.Success)
                {
                    save.domain.Sucursales_Load();

                    return Ok(save.domain.Data.Entity);
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
                var delete = new domain.Model.Empresa() { Id = id }.Erase();

                if (delete.result.Success)
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