using System.ComponentModel.DataAnnotations;

namespace entities.Model
{
    public abstract partial class EmpresaMetadata
    {
        [Required(ErrorMessage = "RUC es obligatorio")]
        [StringLength(20)]
        public string Ruc { get; set; }
        [Required(ErrorMessage = "Razon Social es obligatorio")]
        [StringLength(100)]
        public string RazonSocial { get; set; }
    }
}
