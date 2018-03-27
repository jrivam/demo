using library.Interface.Entities;
using System.Collections.Generic;

namespace entities.Model
{
    public partial class Empresa : IEntity
    {
        public virtual int? Id { get; set; }
        public virtual string RazonSocial { get; set; }
        public virtual bool? Activo { get; set; }

        public virtual IList<entities.Model.Sucursal> Sucursales { get; set; }
    }
}
