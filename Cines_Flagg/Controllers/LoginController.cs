using Microsoft.AspNetCore.Mvc;
using Cines_Flagg.Models;
using System.Web;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Cines_Flagg.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly MyContext _context;
        private Usuario UsuarioActual;
        public LoginController(ILogger<LoginController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario model)
        {
            if (ModelState.IsValid)
            {
                // Lógica para validar las credenciales del usuario en la base de datos
                bool isValidUser = ValidateUser(model.Mail, model.Password);

                if (isValidUser)
                {
                    // Aquí puedes guardar información del usuario en la sesión si es necesario
                    HttpContext.Session.SetString("logueado", "si");
                    // Redirigir al controlador y acción que representan la página de inicio después del inicio de sesión exitoso
                    return RedirectToAction("Index", "Cartelera");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                    ModelState.AddModelError("", "Credenciales inválidas. Inténtalo de nuevo.");
                }

            }

            return View(model);
        }

        // Método para validar las credenciales del usuario en la base de datos
        private bool ValidateUser(string mail, string password)
        {
            try
            {
                string mailU = mail;
                string passU = password; //¿¿COMO PUEDE SER QUE AL IGUALARLO PONGA LA PASS Y SINO EL MAIL??

                string comprobar = "";
                _context.usuarios.Load();
                Usuario usr = _context.usuarios.Where(u => u.Mail == mailU && u.Bloqueado == false).FirstOrDefault();
                if (usr != null)
                {
                    if (usr.Password == passU)
                    {
                        UsuarioActual = usr;
                        comprobar = "ok";
                        usr.IntentosFallidos = 0;
                        _context.usuarios.Update(usr);//Actualiza los intentos fallidos a 0
                        _context.SaveChanges();
                    }
                    else
                    {
                        usr.IntentosFallidos++;
                        if (usr.IntentosFallidos == 4)
                        {
                            usr.Bloqueado = true;
                        }
                        _context.usuarios.Update(usr);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    return false;
                }
                if (comprobar == "ok") 
                    return true;
                else 
                    return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
    }
}
