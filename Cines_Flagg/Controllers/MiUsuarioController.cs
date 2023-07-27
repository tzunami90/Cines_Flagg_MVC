using Cines_Flagg.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Newtonsoft.Json;


namespace Cines_Flagg.Controllers
{
    public class MiUsuarioController : Controller
    {
        private readonly ILogger<MiUsuarioController> _logger;
        private readonly MyContext _context;
        private Usuario usuarioActual;

        //[Authorize]
        public MiUsuarioController(ILogger<MiUsuarioController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //aca va usuarioActual
            usuarioActual = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("objetoUsuario"));
            // Usuario usuario = _context.usuarios.Where(usuario => usuario.ID == 1).FirstOrDefault();
            return View(usuarioActual);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CargarCredito(int ID, double monto)
        {
            Usuario usuario = _context.usuarios.Where(usuario => usuario.ID == ID).FirstOrDefault();
            double? creditoNuevo = usuario.Credito + monto;
            usuario.Credito = creditoNuevo;

            _context.usuarios.Update(usuario);
            _context.SaveChanges();

            HttpContext.Session.SetString("objetoUsuario", JsonConvert.SerializeObject(usuario));

            TempData["PlayCreditSound"] = true; // Almacena la bandera para reproducir el sonido

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,Apellido,Password,FechaNacimiento")] Usuario usuario)
        {
            if (id != usuario.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    usuarioActual = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("objetoUsuario"));
                    usuario.Mail = usuarioActual.Mail;
                    usuario.DNI = usuarioActual.DNI;
                    usuario.EsAdmin = usuarioActual.EsAdmin;
                    usuario.Bloqueado =usuarioActual.Bloqueado;
                    usuario.Credito = usuarioActual.Credito;
                    usuario.IntentosFallidos = usuarioActual.IntentosFallidos;

                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                HttpContext.Session.SetString("objetoUsuario", JsonConvert.SerializeObject(usuario));
                return RedirectToAction(nameof(Index));
            }
            
            return View(usuario);
        }

        private bool UsuarioExists(int id)
        {
            return (_context.usuarios?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
