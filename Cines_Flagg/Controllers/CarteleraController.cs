using Cines_Flagg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Web;
using Newtonsoft.Json;

namespace Cines_Flagg.Controllers
{
    public class CarteleraController : Controller
    {

        private readonly ILogger<CarteleraController> _logger;

        private readonly MyContext _context;
        private Usuario usuarioActual;


        public CarteleraController(ILogger<CarteleraController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;

        }


        public ActionResult CerrarSesion()
        {
            // Eliminar las variables de sesión relacionadas con el usuario
            HttpContext.Session.Clear(); // O puedes utilizar Session.Remove("nombreVariable") para eliminar variables específicas.
            usuarioActual = null;   
            // Redirigir al usuario a la página de inicio de sesión (o a otra página de tu elección)
            return RedirectToAction("Index", "Cartelera"); // Redirige a la acción "Index" del controlador "Login".
        }

        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("logueado") != null)
            {
                // Tu código actual para obtener el usuario actual
                //HttpContext.Session.SetString("logueado", "si");

                this.usuarioActual = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("objetoUsuario"));
                //usuarioActual = _context.usuarios.FirstOrDefault(usuario => usuario.ID == HttpContext.Session.GetInt32("idUsuarioActual"));

                if (usuarioActual != null)
                {
                    ViewBag.UsuarioActual = usuarioActual;
                }
            }

            ViewBag.usAct = usuarioActual;
            var peliculas = _context.peliculas.ToList();

            return View(peliculas);
        }


        public ActionResult Logout()
        {
            // Código para cerrar la sesión del usuario, por ejemplo:
             HttpContext.Session.Clear();
            // Aquí también puedes agregar la lógica adicional, como limpiar las cookies o datos de sesión si es necesario.

            return RedirectToAction("Index", "Cartelera"); // Redirige a la página de inicio después de cerrar sesión.
        }

        private List<Pelicula> ObtenerPeliculas()
        {

            using (var context = new MyContext())
            {
                return context.peliculas.Select(p => new Pelicula
                {
                    Nombre = p.Nombre,
                    Poster = p.Poster
                }).ToList();

            }

        }

        public IActionResult Compra(string nombrePelicula)
        {

            if (HttpContext.Session.GetString("logueado") == null)
            {               
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.NombrePelicula = nombrePelicula;

                var salas = _context.funciones
                    .Where(f => f.MiPelicula.Nombre == nombrePelicula)
                    .Select(f => f.MiSala)
                    .Distinct()
                    .ToList();


                var funciones = _context.funciones
                    .Where(f => f.MiPelicula.Nombre == nombrePelicula)
                    .Select(f => f.Fecha)
                    .Distinct()
                    .ToList();


                var costos = _context.funciones
                    .Where(f => f.MiPelicula.Nombre == nombrePelicula)
                    .Select(f => f.Costo)
                    .Distinct()
                    .ToList();


                ViewBag.Salas = salas;
                ViewBag.Funciones = funciones;
                ViewBag.Costos = costos;

                return View();
            }            
        }
    }
}
