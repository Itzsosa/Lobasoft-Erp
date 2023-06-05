using Lobasoft_Erp.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lobasoft_Erp.Controllers
{
    public class U_UsuariosController : Controller
    {
        private readonly Contexto _contexto;

        public U_UsuariosController(Contexto contexto)
        {
            _contexto = contexto;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



    }
}
