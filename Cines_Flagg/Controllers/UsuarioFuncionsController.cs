using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cines_Flagg.Models;

namespace Cines_Flagg.Controllers
{
    public class UsuarioFuncionsController : Controller
    {
        private readonly MyContext _context;

        public UsuarioFuncionsController(MyContext context)
        {
            _context = context;
        }

        // GET: UsuarioFuncions
        public async Task<IActionResult> Index()
        {
            var myContext = _context.UF.Include(u => u.MiFuncion).Include(u => u.MiUsuario);
            return View(await myContext.ToListAsync());
        }

        // GET: UsuarioFuncions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UF == null)
            {
                return NotFound();
            }

            var usuarioFuncion = await _context.UF
                .Include(u => u.MiFuncion)
                .Include(u => u.MiUsuario)
                .FirstOrDefaultAsync(m => m.idUsuario == id);
            if (usuarioFuncion == null)
            {
                return NotFound();
            }

            return View(usuarioFuncion);
        }

        // GET: UsuarioFuncions/Create
        public IActionResult Create()
        {
            ViewData["idFuncion"] = new SelectList(_context.funciones, "ID", "ID");
            ViewData["idUsuario"] = new SelectList(_context.usuarios, "ID", "ID");
            return View();
        }

        // POST: UsuarioFuncions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idUsuario,idFuncion,cantidadCompra")] UsuarioFuncion usuarioFuncion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioFuncion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idFuncion"] = new SelectList(_context.funciones, "ID", "ID", usuarioFuncion.idFuncion);
            ViewData["idUsuario"] = new SelectList(_context.usuarios, "ID", "ID", usuarioFuncion.idUsuario);
            return View(usuarioFuncion);
        }

        // GET: UsuarioFuncions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UF == null)
            {
                return NotFound();
            }

            var usuarioFuncion = await _context.UF.FindAsync(id);
            if (usuarioFuncion == null)
            {
                return NotFound();
            }
            ViewData["idFuncion"] = new SelectList(_context.funciones, "ID", "ID", usuarioFuncion.idFuncion);
            ViewData["idUsuario"] = new SelectList(_context.usuarios, "ID", "ID", usuarioFuncion.idUsuario);
            return View(usuarioFuncion);
        }

        // POST: UsuarioFuncions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idUsuario,idFuncion,cantidadCompra")] UsuarioFuncion usuarioFuncion)
        {
            if (id != usuarioFuncion.idUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioFuncion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioFuncionExists(usuarioFuncion.idUsuario))
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
            ViewData["idFuncion"] = new SelectList(_context.funciones, "ID", "ID", usuarioFuncion.idFuncion);
            ViewData["idUsuario"] = new SelectList(_context.usuarios, "ID", "ID", usuarioFuncion.idUsuario);
            return View(usuarioFuncion);
        }

        // GET: UsuarioFuncions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UF == null)
            {
                return NotFound();
            }

            var usuarioFuncion = await _context.UF
                .Include(u => u.MiFuncion)
                .Include(u => u.MiUsuario)
                .FirstOrDefaultAsync(m => m.idUsuario == id);
            if (usuarioFuncion == null)
            {
                return NotFound();
            }

            return View(usuarioFuncion);
        }

        // POST: UsuarioFuncions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UF == null)
            {
                return Problem("Entity set 'MyContext.UF'  is null.");
            }
            var usuarioFuncion = await _context.UF.FindAsync(id);
            if (usuarioFuncion != null)
            {
                _context.UF.Remove(usuarioFuncion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioFuncionExists(int id)
        {
          return (_context.UF?.Any(e => e.idUsuario == id)).GetValueOrDefault();
        }
    }
}
