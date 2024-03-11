using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EISGG20241103.Models
{
    public class DetalleCliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Teléfono es requerido")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El teléfono debe tener exactamente 8 dígitos")]
        public string Telefono { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "El campo cliente es obligatorio.")]
        public int ClienteId { get; set; }

        public virtual Cliente? Cliente { get; set; } = null!;
    }
}
