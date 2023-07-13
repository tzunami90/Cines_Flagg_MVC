using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cines_Flagg.Models
{
    public class UsuarioFuncion
    {
        //Esta clase se crea ya que la relacion Usaurio-Funcion es MANY to MANY y se utiliza tabla intermedia
        
        public Usuario MiUsuario { get; set; } = new Usuario();
        public Funcion MiFuncion { get; set; } = new Funcion();

        public int idUsuario { get; set; }
        public int idFuncion { get; set; }
        public int cantidadCompra { get; set; }

        public UsuarioFuncion() { }


        public UsuarioFuncion(Usuario MiUsuario, Funcion MiFuncion, int cantidadCompra)
        { 
            this.MiUsuario = MiUsuario;
            this.MiFuncion = MiFuncion;
            this.cantidadCompra = cantidadCompra;
            idUsuario = MiUsuario.ID;
            idFuncion = MiFuncion.ID;
        }

        public UsuarioFuncion(int idUsuario, int idFuncion, int cantidad)
        {
            this.idUsuario = idUsuario;
            this.idFuncion = idFuncion;
            this.cantidadCompra = cantidad;
        }

        public string[] ToString()
        {
            return new string[]
            {
                MiUsuario.ID.ToString(), MiFuncion.ID.ToString(), cantidadCompra.ToString() 
            };        
        }
    }
}
