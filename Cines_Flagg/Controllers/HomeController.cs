using Cines_Flagg.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cines_Flagg.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly MyContext _context;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }
     //   [Authorize]
        public IActionResult Index()
        {
            Usuario usuario = _context.usuarios.Where(usuario => usuario.ID == 3).FirstOrDefault();
           // System.Diagnostics.Debug.WriteLine(usuario.EsAdmin);
           // ViewData["usuario"] = usuario;
            ViewBag.usuario = usuario;
            return View(usuario);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}