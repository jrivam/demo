using Library.Impl.Entities;
using Library.Impl.Entities.Reader;
using Library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Entities.Table
{
    [MetadataType(typeof(SucursalMetadata))]
    [Table("sucursal")]
    public partial class Sucursal : IEntity
    {        
        [Column("id")]
        [Key]
        public virtual int? Id { get; set; }
        [Column("codigo")]
        public virtual string Codigo { get; set; }
        [Column("nombre")]
        public virtual string Nombre { get; set; }
        [Column("fecha")]
        public virtual DateTime? Fecha { get; set; }
        [Column("activo")]
        public virtual bool? Activo { get; set; }

        [Column("id_empresa")]
        [ForeignKey("empresa")]
        public virtual int? IdEmpresa { get; set; }
        public virtual Entities.Table.Empresa Empresa { get; set; }
    }

    public partial class Sucursales : ListEntity<Entities.Table.Sucursal>
    {
        public Sucursales()
            : base()
        {
        }
        public Sucursales(List<Entities.Table.Sucursal> entities)
            : base(entities)
        {
        }
    }
}

namespace Entities.Reader
{
    public partial class Sucursal : BaseReader<Entities.Table.Sucursal>
    {
        public Sucursal()
            : base()
        {
        }

        public override Entities.Table.Sucursal CreateInstance()
        {
            return base.CreateInstance();
        }

        public override Entities.Table.Sucursal Clear(Entities.Table.Sucursal entity, int maxdepth = 1, int depth = 0)
        {
            entity.Id = null;
            entity.Codigo = null;
            entity.Nombre = null;
            entity.Fecha = null;
            entity.Activo = null;
            entity.IdEmpresa = null;

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                entity.Empresa = null;
            }

            return entity;
        }

        public override Entities.Table.Sucursal Read(Entities.Table.Sucursal entity, IDataReader reader, IList<string> prefixname, string columnseparator, int maxdepth = 1, int depth = 0)
        {
            prefixname.Add("Sucursal");

            var prefix = string.Join(columnseparator, prefixname);
            prefix += (prefix == string.Empty ? prefix : columnseparator);

            entity.Id = reader[$"{prefix}Id"] as int?;
            entity.Codigo = reader[$"{prefix}Codigo"] as string;
            entity.Nombre = reader[$"{prefix}Nombre"] as string;
            entity.Fecha = reader[$"{prefix}Fecha"] as DateTime?;
            entity.Activo = reader[$"{prefix}Activo"] as bool?;

            entity.IdEmpresa = reader[$"{prefix}IdEmpresa"] as int?;

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                entity.Empresa = new Entities.Reader.Empresa().Read(new Entities.Table.Empresa(), reader, new List<string>(prefixname), columnseparator, maxdepth, depth);
            }

            return entity;
        }
    }
}