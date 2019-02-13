using System.ComponentModel.DataAnnotations;

namespace entities.Model
{
    public abstract partial class SucursalMetadata
    {
        [Required(ErrorMessage = "Nombre es obligatorio")]
        public string Nombre { get; set; }
    }
}
