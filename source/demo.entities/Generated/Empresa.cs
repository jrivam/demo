using jrivam.Library.Interface.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace demo.Entities.Table
{
    [MetadataType(typeof(EmpresaMetadata))]
    [Table("empresa")]
    public partial class Empresa : IEntity
    {
        [Column("id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int? Id { get; set; }
        [Column("ruc")]
        //[Index(IsUnique = true)]
        public virtual string Ruc { get; set; }
        [Column("razon_social")]
        public virtual string RazonSocial { get; set; }
        [Column("activo")]
        public virtual bool? Activo { get; set; }

        public virtual ICollection<Entities.Table.Sucursal> Sucursales { get; protected set; } = new Collection<Entities.Table.Sucursal>();
    }
}
