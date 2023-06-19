using Lobasoft_Erp.Data;
using Lobasoft_Erp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Lobasoft_Erp.Controllers
{
    public class OrdenController : Controller
    {
        private readonly Contexto _context;
        

        public OrdenController(Contexto context)
        {
            _context = context;
        }

        //[HttpGet]
        //public async Task<IActionResult> Consultar()
        //{
        //    //var proveedores = await _contexto.LBS_Proveedores.ToListAsync();
        //    //var areasComerciales = await ObternerAreasComerciales();

        //    //ViewData["Proveedores"] = proveedores;
        //    //ViewData["AreasComerciales"] = areasComerciales;

        //    return View(await _contexto.LBS_Proveedores.ToListAsync());
        //}
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contactar(Contactar contactar)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Obtener el correo electrónico del usuario autenticado
                string correoUsuario = User.FindFirst(ClaimTypes.Email)?.Value;

                //se envia el email al usuario
                if (this.EnviarEmail(contactar, correoUsuario))
                {
                    //mensaje si todo salio bien
                    TempData["CorreoEnviado"] = "¡Correo enviado correctamente!";
                }
                else
                {
                    TempData["MensajeError"] = "¡Hubo un error, no se pudo enviar el correo!";
                }

                return RedirectToAction("Index", "Orden");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private bool EnviarEmail(Contactar temp, String correoUsuario)
        {
            try
            {
                //variable control
                bool enviado = false;

                //se instancia el object 
                Email email = new Email();

                email.EnviarCorreoContacto(temp, correoUsuario);

                //se indica que todo salió bien
                enviado = true;

                //se envia la variable control
                return enviado;
            }
            catch (Exception ex)
            {
                //en caso de un error se indica que no fue enviado  la información
                return false;
            }
        }





        [HttpGet]
        public async Task<IActionResult> ObtenerProveedoresPorAreaComercial(int areaComercialId)
        {
            var proveedores = await _context.LBS_Proveedores
                .Where(p => _context.LBS_AsignacionAreaProveedor
                    .Any(a => a.A_idProveedor == p.Id && a.A_idAreaComercial == areaComercialId))
                .Select(p => new LBS_ProveedoresFiltro
                {
                    Nombre = p.Nombre,
                    Provincia = p.Provincia,
                    Email = p.Email
                })
                .ToListAsync();

         
            return Json(proveedores);
        }


    }
}
