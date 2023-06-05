using Lobasoft_Erp.Models;
using Microsoft.EntityFrameworkCore;

namespace Lobasoft_Erp.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        { 
            
        }

        public DbSet<Proveedor> Proveedor { get; set; }
    }
}
