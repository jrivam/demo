using jrivam.Library.Interface.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace demo.Entities.Table
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
        [ForeignKey(nameof(Empresa))]
        public virtual int? IdEmpresa { get; set; }
        public virtual Entities.Table.Empresa Empresa { get; set; }
    }
}