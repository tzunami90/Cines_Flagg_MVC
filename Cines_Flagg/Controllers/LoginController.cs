using Microsoft.AspNetCore.Mvc;
using Cines_Flagg.Models;

namespace Cines_Flagg.Controllers
{
    public class LoginController : Controller
    {
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
                // Lógica de verificación de credenciales y inicio de sesión
                // Aquí puedes verificar si el email y la contraseña son válidos
                // y autenticar al usuario

                // Ejemplo de redirección a la página principal después de iniciar sesión
                return RedirectToAction("Index", "Home");
            }

            // Si llega aquí, significa que el modelo no es válido, por lo que
            // volvemos a mostrar el formulario con los mensajes de validación
            return View(model);
        }
    }
}
