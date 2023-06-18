using System.ComponentModel.DataAnnotations;

namespace Lobasoft_Erp.Models
{
    public class CambiarPassword
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar el password actual")]
        [DataType(DataType.Password)]
        public string PasswordActual { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nuevo password")]
        [DataType(DataType.Password)]
        public string NuevoPassword { get; set; }

        [Required(ErrorMessage = "Debe confirmar el password")]
        [DataType(DataType.Password)]
        public string Confirmar { get; set; }

    
    }
}
