﻿using Lobasoft_Erp.Models;
using Microsoft.EntityFrameworkCore;

namespace Lobasoft_Erp.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }

        public DbSet<LBS_Proveedores> LBS_Proveedores { get; set; }
        public DbSet<LBS_Usuario> LBS_Usuarios { get; set; }
        public DbSet<LBS_AreaComercial> LBS_AreaComercial { get; set; }
        public DbSet<LBS_AsignacionAreaProveedor> LBS_AsignacionAreaProveedor { get; set; }

    }
}

