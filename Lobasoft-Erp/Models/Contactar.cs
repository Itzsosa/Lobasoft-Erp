using System.ComponentModel.DataAnnotations;

namespace Lobasoft_Erp.Models
{
    public class Contactar
    {
        public int IdProveedor { get; set; }

        public string Asunto { get; set; }

        public string Descripcion { get; set; }

        [Required]
        [EmailAddress]
        public string CorreoProveedor { get; set; }
    }
}
