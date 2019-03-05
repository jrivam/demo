using System.ComponentModel.DataAnnotations;

namespace entities.Model
{
    public abstract partial class SucursalMetadata
    {
        [Required(ErrorMessage = "Codigo es obligatorio")]
        [StringLength(10)]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }
    }
}
