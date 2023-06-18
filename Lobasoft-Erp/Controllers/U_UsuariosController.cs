using Lobasoft_Erp.Data;
using Lobasoft_Erp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

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

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Login(LBS_Usuario user)
        {
            
            var temp = this.ValidarUsuario(user);

            if (temp != null)
            {
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, temp.U_nombreUsuario),
                        new Claim(ClaimTypes.Email, temp.U_correo),
                        new Claim(ClaimTypes.Role, temp.U_rol),
                    };

                    //Se crea la instancia para la entidad del usuario y el tipo de autenticación
                    var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });

                    //Se realiza la autenticación dentro del contexto http
                    HttpContext.SignInAsync(userPrincipal);

                  
                    return RedirectToAction("Index", "Home");
                
            }
            
            TempData["Mensaje"] = "Error, el usuario o password incorrecto";

            
            return View(user);

        }

        private LBS_Usuario ValidarUsuario(LBS_Usuario temp)
        {
            LBS_Usuario autorizado = null;

            //por medio del ORM buscamos en nuestra base de datos un usuario con el email correspondiente
            var user = _contexto.LBS_Usuarios.FirstOrDefault(U => U.U_correo == temp.U_correo);

            //user contiene datos
            if (user != null)
            {
                //Valida el password
                if (user.U_contrasena.Equals(temp.U_contrasena))
                {
                    //Se almacena el usuario autorizado
                    autorizado = user;
                }
            }
            return autorizado;
        }

        public async Task<IActionResult> Logout()
        {
            
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CrearCuenta()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearCuenta([Bind] LBS_Usuario user)
        {
            if (user != null)
            {
                //Primero que todo validar si ya hay una cuenta con el correo
                var correoExiste = _contexto.LBS_Usuarios.FirstOrDefault(u => u.U_correo == user.U_correo);
                if (correoExiste != null)
                {
                    // El correo ya está registrado
                    TempData["MensajeError"] = "Existe una cuenta con el correo " + user.U_correo;
                    return View();
                }

                user.U_estado = "Activo";
                user.U_rol = "Cliente";

                //se asigna una contraseña
                user.U_contrasena = this.GenerarClave();

                try
                {
                    //se envia el email al usuario
                    if (this.EnviarEmail(user))
                    {
                        _contexto.LBS_Usuarios.Add(user);
                        _contexto.SaveChanges();
                        //mensaje si todo salio bien
                        TempData["MensajeCreado"] = "Usuario creado correctamente.Su contraseña fue envia por correo.";
                    }
                    else
                    {
                        TempData["MensajeCreado"] = "Usuario creado pero no se envió el email. Comuniquese el administrador";
                    }
                }
                catch (Exception ex)
                {

                    
                }
                return RedirectToAction("Login", "U_Usuarios");
            }
            else
            {
                return View();
            }
        }//cierre método

        private string GenerarClave()
        {
            Random random = new Random();
            string clave = string.Empty;
            clave = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            //se genera una contraseña 
            return new string(Enumerable.Repeat(clave, 12).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private bool EnviarEmail(LBS_Usuario temp)
        {
            try
            {
                //variable control
                bool enviado = false;

                //se instancia el object 
                Email email = new Email();

                //se utiliza el método enviar con los datos del usuario
                //se envia los datos del usuario como parámetro
                email.EnviarCorreoRegistro(temp);

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


    }
}
