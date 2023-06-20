using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;


namespace Lobasoft_Erp.Models

{
    public class SP_OrdenesPorUsuario
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        
        public string Email { get; set; }

        public string Asunto { get; set; }

        
        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; }

    }
}
