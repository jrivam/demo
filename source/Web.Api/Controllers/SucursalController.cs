using library.Impl.Data;
using System;
using System.Net;
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
                    query.Data["Nombre"].Where(nombre, WhereOperator.Like);
                if (activo != null)
                    query.Data["Activo"].Where(activo);
                if (fecha != null)
                    query.Data["Fecha"].Where(Convert.ToDateTime(fecha).ToString("dd/MM/yyyy"));

                return Ok(new domain.Model.Sucursales().Load(query.List().domains).Datas.Domains);
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
                var load = new domain.Model.Sucursal() { Id = id }.Load();

                if (load.result.Success)
                {
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
        public IHttpActionResult Post([FromBody]domain.Model.Sucursal domain)
        {
            try
            {
                if (domain == null)
                    return BadRequest();

                var save = domain.Save();
                if (save.result.Success)
                {
                    return Created<entities.Model.Sucursal>(Request.RequestUri + "/" + save.domain.Id.ToString(), save.domain.Data.Entity);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult Put(int id, [FromBody]domain.Model.Sucursal domain)
        {
            try
            {
                if (domain == null)
                    return BadRequest();

                var save = domain.Save();
                if (save.result.Success)
                {
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
                var delete = new domain.Model.Sucursal() { Id = id }.Erase();

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
