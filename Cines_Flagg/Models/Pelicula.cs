using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cines_Flagg.Models
{
    public class Pelicula
    {

        public int ID { set; get; } = 0;
        public string Nombre { set; get; } = "";
        public string Sinopsis { set; get; } = "";
        public List<Funcion> MisFunciones { set; get; } = new List<Funcion>();
        public int Duracion { set; get; } = 0;
        //private static int idPelicula;
        public string Poster { set; get; } = "";

        public Pelicula() { }

        public Pelicula(int id, string nombre, string sinopsis, int duracion, string poster)
        {
            ID = id;
            Nombre = nombre;
            Sinopsis = sinopsis;
            Duracion = duracion;
            Poster = poster;
            
        }

        public string[] ToString()
        {

            return new string[]
            {
                ID.ToString(),
                Nombre.ToString(),
                Sinopsis.ToString(),
                Duracion.ToString(),
                Poster.ToString()
            };


        }
    }
}
