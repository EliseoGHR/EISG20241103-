using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EISGG20241103.Models
{
    public class Cliente
    {
        public Cliente()
        {
            DetalleClientes = new List<DetalleCliente>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido del cliente es obligatorio.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El Email es requerido")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El Email El debe tener entre 2 y 50 caracteres")]
        [EmailAddress(ErrorMessage = "El Email no es valido, por favor ingrese un Email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La edad del cliente.")]
        public int Edad { get; set; }

        public IList<DetalleCliente> DetalleClientes { get; set; }
    }
}
