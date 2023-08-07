using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cines_Flagg.Models;
using System.Diagnostics;

namespace Cines_Flagg.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly MyContext _context;

        public UsuariosController(MyContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            string esAdminValue = HttpContext.Session.GetString("EsAdmin");

            if (esAdminValue == "Y")
            {
                // Si es administrador, permitir el acceso a la acción
                return _context.usuarios != null ?
                          View(await _context.usuarios.ToListAsync()) :
                          Problem("Entity set 'MyContext.usuarios'  is null.");
            }
            else
            {
                // Si no es administrador, redirigir a AccessDenied
                return RedirectToAction("AccessDenied", "Home");
            }

        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            string esAdminValue = HttpContext.Session.GetString("EsAdmin");

            if (esAdminValue == "Y")
            {
                if (id == null || _context.usuarios == null)
                {
                    return NotFound();
                }

                var usuario = await _context.usuarios
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (usuario == null)
                {
                    return NotFound();
                }

                return View(usuario);
            }
            else
            {
                // Si no es administrador, redirigir a AccessDenied
                return RedirectToAction("AccessDenied", "Home");
            }

        }

        // GET: Usuarios/Create
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

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,Apellido,DNI,Mail,Password,IntentosFallidos,Bloqueado,Credito,FechaNacimiento,EsAdmin")] Usuario usuario)
        {
                if (ModelState.IsValid)
                {
                    bool isValidUser = ValidateUser(usuario.Mail, usuario.DNI);

                    if (isValidUser) //El usuario no existe entonces lo creo
                    {
                        _context.Add(usuario);
                        await _context.SaveChangesAsync();
                        ViewBag.SuccessMessage = "Registro Exitoso";
                        return RedirectToAction(nameof(Index));
                    }
                    else //El usuario ya existe con el mail o dni ingresados
                    {
                        ModelState.AddModelError("Usuario", "Ya existe un usuario con este Mail");
                        return RedirectToAction("Index", "Register");
                    }
                }
                return View(usuario);
        }
        //VALIDADOR MAIL O DNI EXISTENTE
        private bool ValidateUser(string Mail, int DNI)
        {
            try
            {
                _context.usuarios.Load();
                Usuario usr = _context.usuarios.Where(u => u.Mail == Mail || u.DNI == DNI).FirstOrDefault();
                if (usr != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string esAdminValue = HttpContext.Session.GetString("EsAdmin");

            if (esAdminValue == "Y")
            {
                if (id == null || _context.usuarios == null)
                {
                    return NotFound();
                }

                var usuario = await _context.usuarios.FindAsync(id);
                if (usuario == null)
                {
                    return NotFound();
                }
                return View(usuario);
            }
            else
            {
                // Si no es administrador, redirigir a AccessDenied
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,Apellido,DNI,Mail,Password,IntentosFallidos,Bloqueado,Credito,FechaNacimiento,EsAdmin")] Usuario usuario)
        {
            if (id != usuario.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string esAdminValue = HttpContext.Session.GetString("EsAdmin");

            if (esAdminValue == "Y")
            {
                if (id == null || _context.usuarios == null)
                {
                    return NotFound();
                }

                var usuario = await _context.usuarios
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (usuario == null)
                {
                    return NotFound();
                }

                return View(usuario);
            }
            else
            {
                // Si no es administrador, redirigir a AccessDenied
                return RedirectToAction("AccessDenied", "Home");
            }
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.usuarios == null)
            {
                return Problem("Entity set 'MyContext.usuarios'  is null.");
            }
            var usuario = await _context.usuarios.FindAsync(id);
            if (usuario != null)
            {//DEVOLVEMOS EL DINERO POR FUNCIONES POSTERIORES
                List<UsuarioFuncion> usuarioFuncion = _context.UF.Where(uf => uf.idUsuario == id).ToList();
                foreach (UsuarioFuncion uf in usuarioFuncion)
                {
                    if (uf.MiFuncion.Fecha >= DateTime.Now)
                    {
                        uf.MiUsuario.Credito += uf.cantidadCompra * uf.MiFuncion.Costo;
                        _context.usuarios.Update(uf.MiUsuario);
                        _context.SaveChanges();
                    }
                }
                _context.usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.usuarios?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
