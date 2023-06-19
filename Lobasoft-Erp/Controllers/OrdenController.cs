using Lobasoft_Erp.Data;
using Lobasoft_Erp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace Lobasoft_Erp.Controllers
{
    public class OrdenController : Controller
    {
        private readonly Contexto _context;
        

        public OrdenController(Contexto context)
        {
            _context = context;
        }

        public async Task<List<LBS_AreaComercial>> ObternerAreasComerciales()
        {
            List<LBS_AreaComercial> lista = new List<LBS_AreaComercial>();

            lista = await _context.LBS_AreaComercial.ToListAsync();

            return lista;
        }


        public async Task<IActionResult> Index()
        {
            List<LBS_AreaComercial> lista = await ObternerAreasComerciales();
            ViewData["AreasComerciales"] = lista;
            return View(await _context.LBS_Proveedores.ToListAsync());
        }


        [HttpGet]
        public async Task<IActionResult> Contactar()
        {
            return View();
        }



        [HttpGet]
        public IActionResult ObtenerProveedoresPorAreaComercial(int? areaComercialId, string? provincia=null, string? canton=null, string? distrito= null)
        {
            
            if (provincia.Equals("Seleccione una provincia"))
            {
                provincia = null;
            }
            if (canton.Equals("Seleccione una cantón"))
            {
                canton = null;  
            }
            if (distrito.Equals("Seleccione una distrito"))
            {
                distrito = null;
            }
            var proveedores = _context.Sp_FiltroProveedores.FromSqlRaw("EXEC Sp_FiltroProveedores @areaComercialId,  @provincia,  @canton, @distrito",
                new SqlParameter("@areaComercialId",(object)areaComercialId ?? DBNull.Value),
                new SqlParameter("@provincia", (object)provincia ?? DBNull.Value),
                new SqlParameter("@canton", (object)canton ?? DBNull.Value),
                new SqlParameter("@distrito", (object)distrito ?? DBNull.Value)).ToList();

            return Json(proveedores);
        }

        //[HttpGet]
        //public async Task<IActionResult> ObtenerProveedoresPorAreaComercial(int? areaComercialId)
        //{
        //    var proveedores = await _context.LBS_Proveedores
        //            .Where(p => _context.LBS_AsignacionAreaProveedor
        //                .Any(a => a.A_idProveedor == p.Id && a.A_idAreaComercial == areaComercialId))
        //            .ToListAsync();

        //    return Json(proveedores);
        //}





    }
}
