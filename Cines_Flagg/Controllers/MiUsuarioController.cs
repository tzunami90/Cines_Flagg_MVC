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

            int? idUsuario = HttpContext.Session.GetInt32("idUsuarioActual");
            Usuario usuario = _context.usuarios.Where(usuario => usuario.ID == idUsuario).FirstOrDefault();

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
                    //usuario.FechaNacimiento = usuarioActual.FechaNacimiento;

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

        //DEVOLUCION DE ENTRADAS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult devolverEntrada(int idFuncion, int cantidad) 
        {

            int? idUsuario = HttpContext.Session.GetInt32("idUsuarioActual");

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            try
            {
                Usuario usuario = _context.usuarios.Where(u => u.ID == idUsuario).FirstOrDefault();
                Funcion funcion = _context.funciones.Where(f => f.ID == idFuncion).FirstOrDefault();

                if (usuario != null && funcion != null)
                {
                    UsuarioFuncion UF = _context.UF.Where(uf => uf.idUsuario == idUsuario && uf.idFuncion == idFuncion).FirstOrDefault();
                    double monto = funcion.Costo * cantidad; //El monto corresponde al costo de la funcion X la cantidad a devolver

                    if (UF != null)
                    {

                        if (DateTime.Compare(funcion.Fecha.Date, DateTime.UtcNow) > 0) //Revisamos si devuelve entradas con fecha superior a la del dia de hoy o no
                        {
                            if (UF.cantidadCompra == cantidad) //devuelve todas las entradas que compro (cantidad = a la cantidad que compro)
                            {
                                usuario.Credito += monto; //Le suma el monto (cantidad devuelta x costo) al usuario
                                funcion.CantClientes -= cantidad; //Resta la cantidad de clientes en al funcion
                                usuario.MisFunciones.Remove(funcion); //Elimina la funcion de la lista de funciones del usuario
                                funcion.Clientes.Remove(usuario);  //Elimina al usuario de la lista de clientes de la funcion

                                _context.usuarios.Update(usuario); //Actualiza el contexto de usaurio porque cambio el credito
                                _context.funciones.Update(funcion); //Actualiza el contexto de funcion porque cambio la disponibilidad
                                _context.UF.Remove(UF); //Elimina la usuario-funcion (ya que se devolvio todo)
                                _context.SaveChanges();

                                TempData["MensajeDevolver"] = "Se devolvio la totalidad de entradas";
                                TempData["playDevol"] = true; // Almacena la bandera para reproducir el sonido

                                HttpContext.Session.SetString("objetoUsuario", JsonConvert.SerializeObject(usuario));
                                return RedirectToAction(nameof(MisFunciones));
                            }
                            else if (UF.cantidadCompra > cantidad)//devuelve algunas (cantidad menor a la que habia comprado)
                            {
                                UF.cantidadCompra -= cantidad; //Resta en la relacion la cantidad de entradas entre usuario y funciones
                                usuario.Credito += monto; //Le suma el monto (cantidad devuelta x costo) al usuario
                                funcion.CantClientes -= cantidad; // Resta la cantidad de clientes en la funcion

                                _context.usuarios.Update(usuario);
                                _context.funciones.Update(funcion);
                                _context.UF.Update(UF);
                                _context.SaveChanges();

                                TempData["MensajeDevolver"] = "Se devolvio la cantidad de entradas indicadas";
                                TempData["playDevol"] = true; // Almacena la bandera para reproducir el sonido

                                HttpContext.Session.SetString("objetoUsuario", JsonConvert.SerializeObject(usuario));

                                return RedirectToAction(nameof(MisFunciones));

                            }
                            else //Quiere devolver de mas (cantidad mayor a la cantidad que compro)
                            {
                                TempData["MensajeDevolver"] = "No puede devolver mas entradas de las que tiene compradas";
                                return RedirectToAction(nameof(MisFunciones));
                            }
                        }
                        else
                        {
                            TempData["MensajeDevolver"] = "La funcion de las entradas a devolver ya expiro";            
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {               
                TempData["MensajeDevolver"] = "OBJETO o ID no encontrado";
            }

            return RedirectToAction(nameof(MisFunciones));
        }

        private bool UsuarioExists(int id)
        {
            return (_context.usuarios?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
