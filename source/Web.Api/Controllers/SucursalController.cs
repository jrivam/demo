using library.Impl.Data;
using System;
using System.Net;
using System.Web.Http;

namespace Web.Api.Controllers
{
    public class SucursalController : ApiController
    {
        public IHttpActionResult Get(string search = "")
        {
            try
            {
                var query = new business.Query.Sucursal();

                if (search != "")
                    query.Data["Nombre"].Where(search, WhereOperator.Like);

                var list = new business.Model.Sucursales().Load(query.List().businesses).Datas.Domains;

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
                var load = new business.Model.Sucursal() { Id = id }.Load();

                if (load.result.Success)
                {
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
        public IHttpActionResult Post([FromBody]business.Model.Sucursal entity)
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
                    return Created<domain.Model.Sucursal>(Request.RequestUri + "/" + save.business.Id.ToString(), save.business.Data.Domain);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Put(int id, [FromBody]business.Model.Sucursal entity)
        {
            try
            {
                if (entity == null)
                    return BadRequest();

                var save = entity.Save();
                if (save.result.Success)
                {
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
                var delete = new business.Model.Sucursal() { Id = id }.Erase();

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
