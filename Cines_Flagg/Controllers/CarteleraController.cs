using Cines_Flagg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Web;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Cines_Flagg.Controllers
{
    [AllowAnonymous]
    public class CarteleraController : Controller
    {

        private readonly ILogger<CarteleraController> _logger;

        private readonly MyContext _context;
        private Usuario usuarioActual;


        public CarteleraController(ILogger<CarteleraController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;

        }

    public ActionResult CerrarSesion()
        {
            // Eliminar las variables de sesión relacionadas con el usuario
            HttpContext.Session.Clear(); // O puedes utilizar Session.Remove("nombreVariable") para eliminar variables específicas.
            HttpContext.Session.Remove("EsAdmin"); //Limpio el check de administrador o no
            usuarioActual = null;   
            // Redirigir al usuario a la página de inicio de sesión (o a otra página de tu elección)
            return RedirectToAction("Index", "Cartelera"); // Redirige a la acción "Index" del controlador "Login".
        }

        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("logueado") != null)
            {
                // Tu código actual para obtener el usuario actual
                //HttpContext.Session.SetString("logueado", "si");

                this.usuarioActual = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("objetoUsuario"));
                //usuarioActual = _context.usuarios.FirstOrDefault(usuario => usuario.ID == HttpContext.Session.GetInt32("idUsuarioActual"));

                if (usuarioActual != null)
                {
                    ViewBag.UsuarioActual = usuarioActual;
                }
            }

            ViewBag.usAct = usuarioActual;
            var peliculas = _context.peliculas.ToList();

            return View(peliculas);
        }


        public ActionResult Logout()
        {
            // Código para cerrar la sesión del usuario, por ejemplo:
             HttpContext.Session.Clear();
            // Aquí también puedes agregar la lógica adicional, como limpiar las cookies o datos de sesión si es necesario.

            return RedirectToAction("Index", "Cartelera"); // Redirige a la página de inicio después de cerrar sesión.
        }

        private List<Pelicula> ObtenerPeliculas()
        {

            using (var context = new MyContext())
            {
                return context.peliculas.Select(p => new Pelicula
                {
                    Nombre = p.Nombre,
                    Poster = p.Poster
                }).ToList();

            }

        }

        public IActionResult Compra(string nombrePelicula)
        {

            if (HttpContext.Session.GetString("logueado") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.NombrePelicula = nombrePelicula;

                var funciones = _context.funciones
                      .Include(f => f.MiPelicula)
                      .Include(f => f.MiSala)
                      .Where(f => f.MiPelicula.Nombre == nombrePelicula && f.Fecha.Date >= DateTime.Now.Date)
                      .OrderBy(f => f.Fecha)
                      .ToList();

                ViewBag.Funciones = funciones;  

                return View(funciones);
            }
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult ComprarEntrada(int idFuncion, int cant)
        {
            int? idUsuario = HttpContext.Session.GetInt32("idUsuarioActual");

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            try
            {
                Usuario usuario = _context.usuarios.Where(u => u.ID == idUsuario).FirstOrDefault();
                Funcion funcion = _context.funciones.Where(f => f.ID == idFuncion).Include(f=>f.MiSala).Include(f=>f.MiPelicula).FirstOrDefault();

                double importe;
                int entradasDispoibles;

                if (usuario != null && funcion != null)
                {
                    // Se busca en tabla intermedia para ver si existe la relacion usuario / funcion (si existe el clietne tiene entradas)
                    UsuarioFuncion usuarioFuncion = _context.UF.Where(uf => uf.idUsuario == idUsuario && uf.idFuncion == idFuncion).FirstOrDefault();

                    importe = funcion.Costo * cant;
                    entradasDispoibles = funcion.MiSala.Capacidad - funcion.CantClientes;

                    if (importe > usuario.Credito)
                    {
                        TempData["MensajeCompra"] = "No tiene crédito para comprar la cantidad de entradas indicada.";

                        ViewBag.NombrePelicula = funcion.MiPelicula.Nombre;
                        return RedirectToAction("Compra", new { nombrePelicula = funcion.MiPelicula.Nombre });
                    }
                    else if (entradasDispoibles < cant)
                    {
                        TempData["MensajeCompra"] = "No hay mas asientos en la sala disponibles para la cantidad de entradas que solicita";

                        ViewBag.NombrePelicula = funcion.MiPelicula.Nombre;
                        // return RedirectToAction(nameof(Compra));
                        return RedirectToAction("Compra", new { nombrePelicula = funcion.MiPelicula.Nombre});
                    }
                    else
                    {
                        if (usuarioFuncion != null)// Compra si el usuario tiene funciones compradas
                        {
                            funcion.CantClientes += cant; //se suman clientes a al objeto funcion
                            usuario.Credito -= importe; //se resta dinero al objeto usuario
                            usuarioFuncion.cantidadCompra += cant;//se agrega la cantidad en el objeto para la tabla intermedia 

                            _context.UF.Update(usuarioFuncion); //Se actualiza usuario funcion, ya que se agregaron mas cantidades de entradas compradas
                            _context.funciones.Update(funcion);//Se actualiza funcion para restar la cantidad de entradas disponibles para una funcion
                            _context.usuarios.Update(usuario); //se actualiza usuario para restar crédito
                            _context.SaveChanges();

                            TempData["MensajeCompra"] = "Compra realizada con exito";
                            TempData["playCompraSound"] = true; // Almacena la bandera para reproducir el sonido

                            HttpContext.Session.SetString("objetoUsuario", JsonConvert.SerializeObject(usuario));

                            return View("Index", _context.peliculas.ToList());
                        }
                        else //Compra de 0
                        {
                            UsuarioFuncion UF = new UsuarioFuncion { MiUsuario = usuario, MiFuncion = funcion, cantidadCompra = cant };                            

                            usuario.MisFunciones.Add(funcion); //Al usuario se le agrega la funcion que compro a su lista de funciones
                            usuario.Credito -= importe; //Resto credito al usuario
                            funcion.Clientes.Add(usuario); //A la funcion se le agrega el usuario a la lista de CLientes
                            funcion.CantClientes += cant;// A la funcion se se le agrega la cantidad de cliente que compraron esa funcion                    
                            _context.UF.Add(UF); // Se agrega las relacion entre el usuario y las funciones
                            _context.usuarios.Update(usuario); //Se actualiza el usuario restandole credito
                            _context.funciones.Update(funcion); //Se actualiza la funcion restando disponibilidad de la sala
                            _context.SaveChanges();

                            TempData["MensajeCompra"] = "Compra realizada con exito";
                            TempData["playCompraSound"] = true; // Almacena la bandera para reproducir el sonido

                            HttpContext.Session.SetString("objetoUsuario", JsonConvert.SerializeObject(usuario));

                            return View("Index", _context.peliculas.ToList());
                        }
                    }
                }
                else
                {
                    TempData["MensajeCompra"] = "No se a podido ingresar la compra";
                    ViewBag.NombrePelicula = funcion.MiPelicula.Nombre;
                    return RedirectToAction("Compra", new { nombrePelicula = funcion.MiPelicula.Nombre });
                }
            }
            catch (Exception ex)
            {
                TempData["MensajeCompra"] = "Objeto o ID no encontrado";
                Console.WriteLine(ex.ToString());

                return RedirectToAction(nameof(Compra));
            }
        }


        public ActionResult IndexSala()
        {

            return View("IndexSala", _context.salas.ToList());
        }

        public IActionResult CompraSala(string nombreUbicacion)
        {

            if (HttpContext.Session.GetString("logueado") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Ubicacion = nombreUbicacion;

                var funciones = _context.funciones
                      .Include(f => f.MiPelicula)
                      .Include(f => f.MiSala)
                      .Where(f => f.MiSala.Ubicacion == nombreUbicacion && f.Fecha.Date >= DateTime.Now.Date)
                      .ToList();

                ViewBag.Funciones = funciones;

                return View(funciones);
            }
        }
    }
}
