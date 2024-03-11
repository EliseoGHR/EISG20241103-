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

        [EmailAddress(ErrorMessage = "Por favor, introduzca una dirección de correo electrónico válida.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La edad del cliente.")]
        public int Edad { get; set; }

        public IList<DetalleCliente> DetalleClientes { get; set; }
    }
}
