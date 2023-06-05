using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace Lobasoft_Erp.Models
{
    public class LBS_Usuario
    {

        [Key]
        public int U_idUsuario { get; set; }
        public string U_nombreUsuario { get; set; }

        [DataType(DataType.Password)]
        public string U_contrasena { get; set; }
        public string U_correo { get; set; }
        public string U_rol { get; set; }
        public string U_estado { get; set; }
    }
}
