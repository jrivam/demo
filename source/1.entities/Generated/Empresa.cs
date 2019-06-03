using Library.Impl.Entities;
using Library.Impl.Entities.Reader;
using Library.Interface.Entities;
using Library.Interface.Persistence.Sql.Builder;
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
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int? Id { get; set; }
        [Column("ruc")]
        [StringLength(20)]
        //[Index(IsUnique = true)]
        public virtual string Ruc { get; set; }
        [Column("razon_social")]
        [StringLength(100)]
        public virtual string RazonSocial { get; set; }
        [Column("activo")]
        public virtual bool? Activo { get; set; }

        public virtual ICollection<Entities.Table.Sucursal> Sucursales { get; set; }
    }

    public partial class Empresas : ListEntity<Entities.Table.Empresa>
    {
        public Empresas(List<Entities.Table.Empresa> entities)
            : base(entities)
        {
        }

        public Empresas()
            : this(new List<Entities.Table.Empresa>())
        {
        }
    }
}

namespace Entities.Reader
{
    public partial class Empresa : BaseReader<Entities.Table.Empresa>
    {
        public Empresa(ISqlSyntaxSign sqlsyntaxsign)
            : base(sqlsyntaxsign)
        {
        }

        public override Entities.Table.Empresa Read(Entities.Table.Empresa entity, IDataReader reader, IList<string> prefixname, int maxdepth = 1, int depth = 0)
        {
            entity = base.Read(entity, reader, prefixname, maxdepth, depth);

            return entity;
        }
    }
}

