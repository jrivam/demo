using Library.Impl.Entities;
using Library.Impl.Entities.Reader;
using Library.Interface.Entities;
using Library.Interface.Persistence.Sql.Builder;
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
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int? Id { get; set; }
        [Column("codigo")]
        [StringLength(10)]
        //[Index(IsUnique = true)]
        public virtual string Codigo { get; set; }
        [Column("nombre")]
        [StringLength(100)]
        public virtual string Nombre { get; set; }
        [Column("fecha")]
        public virtual DateTime? Fecha { get; set; }
        [Column("activo")]
        public virtual bool? Activo { get; set; }

        [Column("id_empresa")]
        [ForeignKey("empresa")]
        public virtual int? IdEmpresa { get; set; }
        public virtual Entities.Table.Empresa Empresa { get; protected set; }
    }

    public partial class Sucursales : ListEntity<Entities.Table.Sucursal>
    {
        public Sucursales(IList<Entities.Table.Sucursal> entities)
            : base(entities)
        {
        }

        public Sucursales()
            : this(new List<Entities.Table.Sucursal>())
        {
        }
    }
}

namespace Entities.Reader
{
    public partial class Sucursal : BaseReader<Entities.Table.Sucursal>
    {
        public Sucursal(ISqlSyntaxSign sqlsyntaxsign)
            : base(sqlsyntaxsign)
        {
        }

        public override Entities.Table.Sucursal Read(Entities.Table.Sucursal entity, IDataReader reader, IList<string> prefixname, int maxdepth = 1, int depth = 0)
        {
            entity = base.Read(entity, reader, prefixname, maxdepth, depth);

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                //entity.Empresa = new Entities.Reader.Empresa(_sqlsyntaxsign).Read(new Entities.Table.Empresa(), reader, new List<string>(prefixname), maxdepth, depth);
            }

            return entity;
        }
    }
}