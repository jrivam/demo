using System.ComponentModel.DataAnnotations;

namespace entities.Model
{
    public abstract partial class EmpresaMetadata
    {
        [Required(ErrorMessage = "Razon Social es obligatorio")]
        public string RazonSocial { get; set; }
    }
}
