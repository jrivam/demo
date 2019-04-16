using Library.Impl.Entities;
using Library.Impl.Entities.Reader;
using Library.Interface.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Entities.Table
{
    [MetadataType(typeof(EmpresaMetadata))]
    [Table("empresa")]
    public partial class Empresa : IEntity
    {
        [Column("id")]
        [Key]
        public virtual int? Id { get; set; }
        [Column("ruc")]
        public virtual string Ruc { get; set; }
        [Column("razon_social")]
        public virtual string RazonSocial { get; set; }
        [Column("activo")]
        public virtual bool? Activo { get; set; }

        public virtual ICollection<Entities.Table.Sucursal> Sucursales { get; set; }
    }

    public partial class Empresas : ListEntity<Entities.Table.Empresa>
    {
        public Empresas()
            : base()
        {
        }
        public Empresas(List<Entities.Table.Empresa> entities)
            : base(entities)
        {
        }
    }
}

namespace Entities.Reader
{
    public partial class Empresa : BaseReader<Entities.Table.Empresa>
    {
        public Empresa()
            : base()
        {
        }

        public override Entities.Table.Empresa Clear(Entities.Table.Empresa entity, int maxdepth = 1, int depth = 0)
        {
            entity.Id = null;
            entity.Ruc = null;
            entity.RazonSocial = null;
            entity.Activo = null;

            entity.Sucursales = null;

            return entity;
        }

        public override Entities.Table.Empresa Read(Entities.Table.Empresa entity, IDataReader reader, IList<string> prefixname, string columnseparator, int maxdepth = 1, int depth = 0)
        {
            prefixname.Add("Empresa");

            var prefix = string.Join(columnseparator, prefixname);
            prefix += (prefix == string.Empty ? prefix : columnseparator);

            entity.Id = reader[$"{prefix}Id"] as int?;
            entity.Ruc = reader[$"{prefix}Ruc"] as string;
            entity.RazonSocial = reader[$"{prefix}RazonSocial"] as string;
            entity.Activo = reader[$"{prefix}Activo"] as bool?;

            return entity;
        }
    }
}

