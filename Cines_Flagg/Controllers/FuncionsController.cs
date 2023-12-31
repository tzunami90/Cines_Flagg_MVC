﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cines_Flagg.Models;

namespace Cines_Flagg.Controllers
{
    public class FuncionsController : Controller
    {
        private readonly MyContext _context;

        public FuncionsController(MyContext context)
        {
            _context = context;
        }

        // GET: Funcions
        public async Task<IActionResult> Index()
        {
            string esAdminValue = HttpContext.Session.GetString("EsAdmin");

            if (esAdminValue == "Y")
            {
                var myContext = _context.funciones.Include(f => f.MiPelicula).Include(f => f.MiSala);
                return View(await myContext.ToListAsync());
            }
            else
            {
                // Si no es administrador, redirigir a AccessDenied
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        // GET: Funcions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string esAdminValue = HttpContext.Session.GetString("EsAdmin");
            if (esAdminValue == "Y")
            {
                    if (id == null || _context.funciones == null)
                {
                    return NotFound();
                }

                var funcion = await _context.funciones
                    .Include(f => f.MiPelicula)
                    .Include(f => f.MiSala)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (funcion == null)
                {
                    return NotFound();
                }

                return View(funcion);
            }
            else
            {
                // Si no es administrador, redirigir a AccessDenied
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        // GET: Funcions/Create
        public IActionResult Create()
        {
            string esAdminValue = HttpContext.Session.GetString("EsAdmin");
            if (esAdminValue == "Y")
            {
                //Si cambias "ID" por "Nombre" te muestra los nombres de las peliculas ;)
                ViewData["idPelicula"] = new SelectList(_context.peliculas, "ID", "ID");
                ViewData["idSala"] = new SelectList(_context.salas, "ID", "ID");
                return View();
            }
            else
            {
                // Si no es administrador, redirigir a AccessDenied
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        // POST: Funcions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="funcion"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,idSala,idPelicula,Fecha,CantClientes,Costo")] Funcion funcion)
        {
            if (ModelState.IsValid)
            {
                _context.funciones.Include(funcion => funcion.MiSala).Include(funcion => funcion.MiPelicula);

                funcion.MiSala = _context.salas.Where(sala => sala.ID == funcion.MiSala.ID).FirstOrDefault();
                funcion.MiPelicula = _context.peliculas.Where(pelicula => pelicula.ID == funcion.MiPelicula.ID).FirstOrDefault();

                _context.Add(funcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idPelicula"] = new SelectList(_context.peliculas, "ID", "ID", funcion.idPelicula);
            ViewData["idSala"] = new SelectList(_context.salas, "ID", "ID", funcion.idSala);
            return View(funcion);
        }

        // GET: Funcions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string esAdminValue = HttpContext.Session.GetString("EsAdmin");
            if (esAdminValue == "Y")
            {
                if (id == null || _context.funciones == null)
                {
                    return NotFound();
                }

                var funcion = await _context.funciones.FindAsync(id);
                if (funcion == null)
                {
                    return NotFound();
                }
                ViewData["idPelicula"] = new SelectList(_context.peliculas, "ID", "ID", funcion.idPelicula);
                ViewData["idSala"] = new SelectList(_context.salas, "ID", "ID", funcion.idSala);
                return View(funcion);
            }
            else
            {
                // Si no es administrador, redirigir a AccessDenied
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        // POST: Funcions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,idSala,idPelicula,Fecha,CantClientes,Costo")] Funcion funcion)
        {
            if (id != funcion.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    funcion.MiSala = _context.salas.Where(sala => sala.ID == funcion.MiSala.ID).FirstOrDefault();
                    funcion.MiPelicula = _context.peliculas.Where(pelicula => pelicula.ID == funcion.MiPelicula.ID).FirstOrDefault();

                    _context.Update(funcion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionExists(funcion.ID))
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
            ViewData["idPelicula"] = new SelectList(_context.peliculas, "ID", "ID", funcion.idPelicula);
            ViewData["idSala"] = new SelectList(_context.salas, "ID", "ID", funcion.idSala);
            return View(funcion);
        }

        // GET: Funcions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string esAdminValue = HttpContext.Session.GetString("EsAdmin");
            if (esAdminValue == "Y")
            {
                if (id == null || _context.funciones == null)
                {
                    return NotFound();
                }

                var funcion = await _context.funciones
                    .Include(f => f.MiPelicula)
                    .Include(f => f.MiSala)
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (funcion == null)
                {
                    return NotFound();
                }

                return View(funcion);
            }
            else
            {
                // Si no es administrador, redirigir a AccessDenied
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        // POST: Funcions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.funciones == null)
            {
                return Problem("Entity set 'MyContext.funciones'  is null.");
            }
            var funcion = await _context.funciones.FindAsync(id);
            if (funcion != null)
            {
                _context.funciones.Remove(funcion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionExists(int id)
        {
          return (_context.funciones?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
