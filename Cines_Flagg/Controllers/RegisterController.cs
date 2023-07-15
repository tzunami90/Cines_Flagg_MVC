using Microsoft.AspNetCore.Mvc;
using Cines_Flagg.Models;
using Microsoft.EntityFrameworkCore;

namespace Cines_Flagg.Controllers
{
    public class RegisterController : Controller
    {
        private readonly MyContext _context;

        public RegisterController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Usuario model)
        {
            if (ModelState.IsValid)
            {
                // Lógica de registro de usuario
                // Aquí puedes crear una nueva cuenta de usuario con los datos proporcionados

                // Ejemplo de redirección a la página de inicio de sesión después del registro exitoso
                return RedirectToAction("Index", "Login");
            }

            // Si llega aquí, significa que el modelo no es válido, por lo que
            // volvemos a mostrar el formulario con los mensajes de validación
            return View(model);
        }

        //Registro de usuario nuevo en la BD 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Nombre,Apellido,DNI,Mail,Password")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

    }
}
