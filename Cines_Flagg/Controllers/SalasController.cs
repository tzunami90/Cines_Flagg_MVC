using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cines_Flagg.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace Cines_Flagg.Controllers
{
    public class SalasController : Controller
    {
        private readonly MyContext _context;

        public SalasController(MyContext context)
        {
            _context = context;
        }

        // GET: Salas
        public async Task<IActionResult> Index()
        {
            string esAdminValue = HttpContext.Session.GetString("EsAdmin");

            if (esAdminValue == "Y")
            {
                // Si es administrador, permitir el acceso a la acción
                return _context.salas != null ?
                         View(await _context.salas.ToListAsync()) :
                         Problem("Entity set 'MyContext.salas'  is null.");
            }
            else
            {
                // Si no es administrador, redirigir a AccessDenied
                return RedirectToAction("AccessDenied", "Home");
            }
           
        }

        // GET: Salas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string esAdminValue = HttpContext.Session.GetString("EsAdmin");

            if (esAdminValue == "Y")
            {
                if (id == null || _context.salas == null)
                {
                    return NotFound();
                }

                var sala = await _context.salas
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (sala == null)
                {
                    return NotFound();
                }

                return View(sala);
            }
            else
            {
                // Si no es administrador, redirigir a AccessDenied
                return RedirectToAction("AccessDenied", "Home");
            }
            
        }

        // GET: Salas/Create
        public IActionResult Create()
        {
            string esAdminValue = HttpContext.Session.GetString("EsAdmin");

            if (esAdminValue == "Y")
            {
                return View();
            }
            else
            {
                // Si no es administrador, redirigir a AccessDenied
                return RedirectToAction("AccessDenied", "Home");
            }            
        }

        // POST: Salas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Ubicacion,Capacidad")] Sala sala)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sala);         
        }

        // GET: Salas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string esAdminValue = HttpContext.Session.GetString("EsAdmin");

            if (esAdminValue == "Y")
            {
                if (id == null || _context.salas == null)
                {
                    return NotFound();
                }

                var sala = await _context.salas.FindAsync(id);
                if (sala == null)
                {
                    return NotFound();
                }
                return View(sala);
            }
            else
            {
                // Si no es administrador, redirigir a AccessDenied
                return RedirectToAction("AccessDenied", "Home");
            }
            
        }

        // POST: Salas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Ubicacion,Capacidad")] Sala sala)
        {
            if (id != sala.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaExists(sala.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sala);
        }

        // GET: Salas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string esAdminValue = HttpContext.Session.GetString("EsAdmin");

            if (esAdminValue == "Y")
            {
                if (id == null || _context.salas == null)
                {
                    return NotFound();
                }

                var sala = await _context.salas
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (sala == null)
                {
                    return NotFound();
                }

                return View(sala);
            }
            else
            {
                // Si no es administrador, redirigir a AccessDenied
                return RedirectToAction("AccessDenied", "Home");
            }
            
        }

        // POST: Salas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.salas == null)
            {
                return Problem("Entity set 'MyContext.salas'  is null.");
            }
            var sala = await _context.salas.FindAsync(id);
            if (sala != null)
            {
                IEnumerable<Funcion> misFunciones = sala.MisFunciones.Where(funcion => funcion.Fecha >= DateTime.Now);

                if (misFunciones == null) //VALIDA SI NO TIENE FUNCIONES POSTERIORES A HOY AL ELIMINARLA
                {
                    _context.salas.Remove(sala);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else //SI TIENE FUNCIONES NO ELIMINA
                {
                    ModelState.AddModelError("Salas", "No es posible eliminar la sala ya que tiene funciones asociadas");
                    return RedirectToAction(nameof(Index));
                }
                
            }
            return RedirectToAction(nameof(Index));

        }

        private bool SalaExists(int id)
        {
          return (_context.salas?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
