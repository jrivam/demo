using library.Impl.Entities;
using library.Impl.Entities.Reader;
using library.Interface.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace entities.Model
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
        public virtual entities.Model.Empresa Empresa { get; set; }
    }

    public partial class Sucursales : ListEntity<entities.Model.Sucursal>
    {
        public Sucursales()
            : base()
        {
        }
        public Sucursales(List<entities.Model.Sucursal> entities)
            : base(entities)
        {
        }
    }
}

namespace entities.Reader
{
    public partial class Sucursal : BaseReaderEntity<entities.Model.Sucursal>
    {
        public Sucursal()
            : base()
        {
        }

        public override entities.Model.Sucursal CreateInstance()
        {
            return base.CreateInstance();
        }

        public override entities.Model.Sucursal Clear(entities.Model.Sucursal entity, int maxdepth = 1, int depth = 0)
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

        public override entities.Model.Sucursal Read(entities.Model.Sucursal entity, IDataReader reader, IList<string> prefixname, string columnseparator, int maxdepth = 1, int depth = 0)
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
                entity.Empresa = new entities.Reader.Empresa().Read(new entities.Model.Empresa(), reader, new List<string>(prefixname), columnseparator, maxdepth, depth);
            }

            return entity;
        }
    }
}