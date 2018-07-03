using library.Interface.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace entities.Model
{
    [MetadataType(typeof(SucursalDecoration))]
    [Table("sucursal")]
    public partial class Sucursal : IEntity
    {        
        [Column("id")]
        [Key]
        public virtual int? Id { get; set; }
        [Column("nombre")]
        public virtual string Nombre { get; set; }
        [Column("fecha")]
        public virtual DateTime? Fecha { get; set; }
        [Column("activo")]
        public virtual bool? Activo { get; set; }

        [Column("id_empresa")]
        [ForeignKey("Empresa")]
        public virtual int? IdEmpresa { get; set; }
        public virtual entities.Model.Empresa Empresa { get; set; }
    }
}
