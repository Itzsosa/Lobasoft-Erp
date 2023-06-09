﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Lobasoft_Erp.Models
{
    public class LBS_Ordenes
    {

        [Key]
        public int O_IdOrden { get; set; }

        public int O_IdProveedor { get; set; }

        public int O_IdUsuario { get; set; }

        public DateTime O_Fecha { get; set; }

        public string O_Asunto { get; set; }

        public string O_Descripcion { get; set; }
    }
}
