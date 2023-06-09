using System.ComponentModel.DataAnnotations.Schema;

namespace Lobasoft_Erp.Models
{
    public class LBS_AsignacionAreaProveedor
    {
        public int Id { get; set; }
        public int A_idProveedor { get; set; }
        public int A_idAreaComercial { get; set; }
    }
}
