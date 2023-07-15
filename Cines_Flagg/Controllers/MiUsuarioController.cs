using Cines_Flagg.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cines_Flagg.Controllers
{
    public class MiUsuarioController : Controller
    {
        private readonly ILogger<MiUsuarioController> _logger;
        private readonly MyContext _context;

        //[Authorize]
        public MiUsuarioController(ILogger<MiUsuarioController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            Usuario usuario = _context.usuarios.Where(usuario => usuario.ID == 1).FirstOrDefault();
            return View(usuario);
        }

        public IActionResult MisFunciones() 
        {

            return View();       
        
        }
    }
}
