using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Cines_Flagg.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public int DNI { get; set; } = 0;
        public string Mail { get; set; } = "";
        public string Password { get; set; } = "";
        public int? IntentosFallidos { get; set; } = 0;
        public bool? Bloqueado { get; set; } = false;
        public List<Funcion> MisFunciones { get; set; } = new List<Funcion>();
        public double? Credito { get; set; }
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; } = new DateTime();
        public bool? EsAdmin { get; set; } = false;
        public List<UsuarioFuncion> UsuarioFuncion { get; set; }


        public Usuario() { }

        public Usuario(string mail, string password)
        {
            Mail = mail;
            Password = password;
        }

        public Usuario (int dni, string nombre, string apellido, string mail, string password, DateTime fechaNacimiento, bool esAdmin, bool bloqueado, double credito)
        {
            Nombre = nombre;
            Apellido = apellido;
            DNI = dni;
            Mail = mail;
            Password = password;
            IntentosFallidos = 0;
            Bloqueado = bloqueado;
            EsAdmin = esAdmin;
            Credito = credito;
            FechaNacimiento = fechaNacimiento;
        }

        public string[] ToString()
        {
            return new string[]
            {
                ID.ToString(), Nombre, Apellido, DNI.ToString(), Mail, Password, FechaNacimiento.ToString(), EsAdmin.ToString(), Bloqueado.ToString(), Credito.ToString() };

        }

    }
}