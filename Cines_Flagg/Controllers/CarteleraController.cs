using Cines_Flagg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace Cines_Flagg.Controllers
{
    public class CarteleraController : Controller
    {

        private readonly ILogger<CarteleraController> _logger;

        private readonly MyContext _context;


        public CarteleraController(ILogger<CarteleraController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

            var peliculas = _context.peliculas.ToList();

            return View(peliculas);
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
