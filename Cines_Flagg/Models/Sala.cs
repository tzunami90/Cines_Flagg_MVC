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
    public class Sala
    {
        public int ID { get; set; } = 0;
        public string Ubicacion { get; set; } = "";
        public int Capacidad { get; set; } = 0;
        public List<Funcion> MisFunciones { get; set; } = new List<Funcion>();
        //private static int idSala { get; set; }


        public Sala() { }

        public Sala(int id, string Ubicacion, int Capacidad)
        {
            this.ID = id;
            this.Ubicacion = Ubicacion;
            this.Capacidad = Capacidad;
        }       
        

        public string[] ToString()
        {

            return new string[]
            {
                ID.ToString(),Ubicacion.ToString(),Capacidad.ToString()
            };
        }
    }    
}