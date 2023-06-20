using Lobasoft_Erp.Data;
using Lobasoft_Erp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Security.Claims;

namespace Lobasoft_Erp.Controllers
{
    public class OrdenController : Controller
    {
        private readonly Contexto _context;
        private int idUsuario;
        private int idProvedor;
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


        public async Task<IActionResult> RealizarOrden()
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

                var usuario = _context.LBS_Usuarios.ToList().Find(x => x.U_correo == correoUsuario);

                this.idUsuario = usuario.U_idUsuario;
                Create(this.idUsuario, contactar.IdProveedor, contactar.Asunto, contactar.Descripcion);

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

                return RedirectToAction("RealizarOrden", "Orden");
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
        public IActionResult FiltroProveedores(int? areaComercialId, string? provincia=null, string? canton=null, string? distrito= null)
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

        [HttpGet]
        public async Task<IActionResult> ConsultarOrdenes()
        {
            string correoUsuario = User.FindFirst(ClaimTypes.Email)?.Value;
            var ordenes = _context.SP_OrdenesPorUsuario.FromSqlRaw("EXEC SP_OrdenesPorUsuario @UsuarioCorreo",
                new SqlParameter("@UsuarioCorreo", correoUsuario)).ToList();

            return View(ordenes);
        }

        public async Task<ActionResult> Details(int? id)
        {
            string correoUsuario = User.FindFirst(ClaimTypes.Email)?.Value;
            var ordenes = _context.SP_OrdenesPorUsuario.FromSqlRaw("EXEC SP_OrdenesPorUsuario @UsuarioCorreo",
                new SqlParameter("@UsuarioCorreo", correoUsuario)).ToList().Find(x => x.Id == id);

            if (ordenes == null)
            {
                return NotFound();
            }
            else
            {
                return View(ordenes);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            string correoUsuario = User.FindFirst(ClaimTypes.Email)?.Value;
            var ordenes = _context.SP_OrdenesPorUsuario.FromSqlRaw("EXEC SP_OrdenesPorUsuario @UsuarioCorreo",
                new SqlParameter("@UsuarioCorreo", correoUsuario)).ToList().Find(x => x.Id == id);
            
            if (ordenes == null)
            {
                return NotFound();
            }
            else
            {
                return View(ordenes);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var temp = await _context.LBS_Ordenes.FindAsync(id);
            _context.LBS_Ordenes.Remove(temp);
            await _context.SaveChangesAsync();

            return RedirectToAction("ConsultarOrdenes");
        }


        public async void Create(int idUsuario, int idProveedor,string asunto, string descripcion )
        {
           LBS_Ordenes orden= new LBS_Ordenes();

            orden.O_IdUsuario = idUsuario;
            orden.O_IdProveedor = idProveedor;
            orden.O_Fecha = DateTime.Now;
            orden.O_Asunto = asunto;
            orden.O_Descripcion = descripcion;


             _context.Add(orden);
             await _context.SaveChangesAsync();      
        }


        }
}
