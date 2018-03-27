using library.Interface.Entities;
using System;

namespace entities.Model
{
    public partial class Sucursal : IEntity
    {
        public virtual int? Id { get; set; }
        public virtual string Nombre { get; set; }
        public virtual int? IdEmpresa { get; set; }
        public virtual bool? Activo { get; set; }
        public virtual DateTime? Fecha { get; set; }

        public virtual Empresa Empresa { get; set; }
    }
}
