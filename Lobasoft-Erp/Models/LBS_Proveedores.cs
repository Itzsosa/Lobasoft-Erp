﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;


namespace Lobasoft_Erp.Models

{
    public class LBS_Proveedores
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Provincia { get; set; }

        public string Canton { get; set; }

        public string Distrito { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

    }
}
