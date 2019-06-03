using System.ComponentModel.DataAnnotations;

namespace Entities.Table
{
    public abstract partial class EmpresaMetadata
    {
        [Required(ErrorMessage = "RUC es obligatorio")]
        public string Ruc { get; set; }
        [Required(ErrorMessage = "Razon Social es obligatorio")]
        public string RazonSocial { get; set; }
    }
}
