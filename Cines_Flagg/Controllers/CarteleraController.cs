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

            var peliculas = new List<Pelicula>
            {

                new Pelicula { Nombre = "Jhon Wick 4", Poster = "https://arepavolatil.com/wp-content/uploads/2023/02/john-wick-4-poster.jpg"},
                new Pelicula { Nombre = "Guardianes de la Galaxia 3", Poster = "https://i0.wp.com/www.universomarvel.com/wp-content/uploads/2023/02/guardians-of-the-galaxy-3-superbowl-poster.jpg?ssl=1"},
                new Pelicula { Nombre = "El conjuro 3", Poster = "https://www.themoviedb.org/t/p/original/79QjdRiT9zTLkrOq9FltoIxClma.jpg"},
                new Pelicula { Nombre = "Super Mario Bros La Pelicula", Poster = "https://www.latercera.com/resizer/JzHhj0HjOVLt-5SYbHUSz2mzNUk=/800x0/smart/cloudfront-us-east-1.images.arcpublishing.com/copesa/ZPEQXUEQKFELNAMFUTACTVA6NY.jpeg"},
                new Pelicula { Nombre = "Super Mario Bros La Pelicula", Poster = "https://www.latercera.com/resizer/JzHhj0HjOVLt-5SYbHUSz2mzNUk=/800x0/smart/cloudfront-us-east-1.images.arcpublishing.com/copesa/ZPEQXUEQKFELNAMFUTACTVA6NY.jpeg"}

            };


            return peliculas;
        }


    }
}
