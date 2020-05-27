using System.ComponentModel.DataAnnotations;

namespace Entities.Table
{
    public abstract partial class SucursalMetadata
    {
        [Required(ErrorMessage = "Codigo es obligatorio")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Nombre es obligatorio")]
        public string Nombre { get; set; }
    }
}
