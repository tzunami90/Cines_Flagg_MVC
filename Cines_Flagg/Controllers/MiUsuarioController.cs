        using Cines_Flagg.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
            //aca va usuarioActual
            Usuario usuario = _context.usuarios.Where(usuario => usuario.ID == 1).FirstOrDefault();
            return View(usuario);
        }

        public IActionResult MisFunciones() 
        {
            _context.usuarios.Include(u => u.MisFunciones).Load();
            _context.funciones.Include(f => f.MiPelicula).Load();
            _context.funciones.Include(f => f.MiSala).Load();
            _context.usuarios.Include(u => u.UsuarioFuncion).Load();

            Usuario usuario = _context.usuarios.Where(usuario => usuario.ID == 1).FirstOrDefault();

            return View(usuario.UsuarioFuncion);

        }
        public IActionResult CargarCredito(int ID, double monto)
        {
            Debug.WriteLine("Cargar Crédito --> ID Usuario : " + ID + " Monto a cargar : " + monto);
            Usuario usuario = _context.usuarios.Where(usuario => usuario.ID == ID).FirstOrDefault();

            return RedirectToAction("Index");
        }
    }
}
