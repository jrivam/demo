using jrivam.Library.Impl.Entities.Reader;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Sql.Builder;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

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

        public virtual ICollection<Entities.Table.Sucursal> Sucursales { get; set; }
    }
}

namespace demo.Entities.Reader
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

