using library.Impl.Data;
using System;
using System.Net;
using System.Web.Http;

namespace Web.Api.Controllers
{
    public class EmpresaController : ApiController
    {
        public IHttpActionResult Get(string search = "")
        {
            try
            {
                var query = new business.Query.Empresa();

                if (search != "")
                    query.Data["RazonSocial"].Where(search, WhereOperator.Like);

                var list = new business.Model.Empresas().Load(query.List().businesses).Datas.Domains;

                return Ok(list);
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
        public IHttpActionResult Post([FromBody]business.Model.Empresa entity)
        {
            try
            {
                if (entity == null)
                {
                    return BadRequest();
                }

                var save = entity.Save();
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

        public IHttpActionResult Put(int id, [FromBody]business.Model.Empresa entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest();

                var save = entity.Save();
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