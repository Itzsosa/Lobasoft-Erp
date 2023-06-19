using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;


namespace Lobasoft_Erp.Models

{
    public class Sp_FiltroProveedores
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Provincia { get; set; }

        
        public string Email { get; set; }

    }
}
