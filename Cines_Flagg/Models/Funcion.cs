using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;


namespace Cines_Flagg.Models
{
    public class Funcion
    {
        public int ID { set; get; } = 0;
        public int idSala { set; get; } = 0;
        public Sala MiSala { set; get; } = new Sala();
        public int idPelicula { set; get; } = 0;
        public Pelicula MiPelicula { set; get; } = new Pelicula();
        public List<Usuario> Clientes { set; get; } = new List<Usuario>();
        public DateTime Fecha { set; get; } = new DateTime();
        public int CantClientes { set; get; } = 0;
        public double Costo { set; get; } = 0.0;
        public List<UsuarioFuncion> UsuarioFuncion { get; set; }
        //private static int idFuncion { set; get; }

        public Funcion() { }


        public Funcion(DateTime Fecha, double Costo)
        {      
            this.Fecha = Fecha;
            this.Costo = Costo;
        }

       public Funcion(int ID, Sala MiSala, Pelicula MiPelicula, DateTime Fecha, double Costo) 
        {
            this.ID = ID;
            this.idSala = MiSala.ID;
            this.MiSala = MiSala;
            this.idPelicula = MiPelicula.ID;
            this.MiPelicula = MiPelicula;  
            this.Fecha = Fecha;            
            this.Costo = Costo;

        }

        public Funcion(int ID, int idSala, int idPelicula, DateTime fecha, double costo)
        {
            this.ID = ID;
            this.idSala = idSala;
            this.idPelicula = idPelicula;
            this.Fecha = fecha;
            this.Costo = costo;
        }


        public string[] ToString()
        {

            return new string[]
            {
                ID.ToString(),MiSala.Ubicacion.ToString(),MiSala.ID.ToString(),MiPelicula.Nombre.ToString(),MiPelicula.ID.ToString(), Fecha.ToString(), Costo.ToString() //CantClientes.ToString() // Muestra cantidad de clientes que ya compraron la funcion
            };
        }

        public string[] ToStringFunciones()
        {   

            return new string[]
            {
                ID.ToString(),MiPelicula.Nombre.ToString(),MiSala.Ubicacion.ToString(),Fecha.ToString(), Costo.ToString() //CantClientes.ToString() // Muestra cantidad de clientes que ya compraron la funcion
            };
        }



    }

}