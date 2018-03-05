using library.Interface.Domain;
using System.Collections.Generic;
using System.Linq;

namespace domain.Model
{
    public partial class Empresa : IEntity
    {
        public virtual int? Id { get; set; }
        public virtual string RazonSocial { get; set; }
        public virtual bool? Activo { get; set; }

        public virtual IList<domain.Model.Sucursal> Sucursales { get; set; }
    }
}
