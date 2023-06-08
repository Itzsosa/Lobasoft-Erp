using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace Lobasoft_Erp.Models
{
    public class LBS_AreaComercial
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre de la área comercial")]
        [Display(Name = "Nombre Area comercial")]
        [StringLength(200, ErrorMessage = "Muy grande el nombre")]
        public string NombreAreaComercial { get; set; }

        [Required(ErrorMessage = "Debe ingresar la descripción")]
        [Display(Name = "Descripcion")]
        [StringLength(200, ErrorMessage = "Muy grande la descripcion")]
        public string Descripcion { get; set; }
    }//fin de la class
}//fin del namespace