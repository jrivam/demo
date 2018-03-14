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
                var query = new business.Query.Empresa();

                if (razonsocial != null)
                    query.Data["RazonSocial"].Where(razonsocial, WhereOperator.Like);
                if (activo != null)
                    query.Data["Activo"].Where(activo);

                return Ok(new business.Model.Empresas().Load(query.List().businesses).Datas.Domains);
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
                var load = new business.Model.Empresa() { Id = id }.Load();

                if (load.result.Success)
                {
                    load.business.Sucursales_Load();

                    return Ok(load.business.Data.Domain);
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
        public IHttpActionResult Post([FromBody]business.Model.Empresa business)
        {
            try
            {
                if (business == null)
                    return BadRequest();

                var save = business.Save();
                if (save.result.Success)
                {
                    return Created<domain.Model.Empresa>(Request.RequestUri + "/" + save.business.Id.ToString(), save.business.Data.Domain);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Put(int id, [FromBody]business.Model.Empresa business)
        {
            try
            {
                if (business == null)
                    return BadRequest();

                var save = business.Save();
                if (save.result.Success)
                {
                    save.business.Sucursales_Load();

                    return Ok(save.business.Data.Domain);
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
                var delete = new business.Model.Empresa() { Id = id }.Erase();

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