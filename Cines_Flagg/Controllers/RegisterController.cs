using Microsoft.AspNetCore.Mvc;
using Cines_Flagg.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Cines_Flagg.Controllers
{
    public class RegisterController : Controller
    {
        private readonly MyContext _context;

        public RegisterController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Registro de usuario nuevo en la BD 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Nombre,Apellido,DNI,FechaNacimiento,Mail,Password")] Usuario model)
        {
            if (ModelState.IsValid)
            {
                bool isValidUser = ValidateUser(model.Mail, model.DNI);

                if (isValidUser)
                {
                    _context.Add(model);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Registro Exitoso";
                    return RedirectToAction("Index", "Login");   
                }
                else
                {
                    //ModelState.AddModelError("", "Ya existe un usuario con este Mail o DNI");
                    TempData["MensajeError"] = "Ya existe un usuario con este Mail o DNI";
                    ViewBag.ShowModal = true;
                    return RedirectToAction("Index", "Register");
                }
            }
            return View(model);
        }

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
                else{
                    return true;
                }               
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }       
    }
}
