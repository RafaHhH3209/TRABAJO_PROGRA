using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX_AUTO_AP
{
    public abstract class Persona
    {
        //ENCAPSULAMIENTO
        //propiedades de la clase Persona, que permiten almacenar información sobre la persona
        private string NombreCompleto { get; set; }
        private DateTime FechaNacimiento { get; set; }
        private string Sexo { get; set; }
        private string EstadoCivil { get; set; }
        private string Telefono { get; set; }
        private string Celular { get; set; }
        private string Email { get; set; }
        private string Direccion { get; set; }
        private string Nacionalidad { get; set; }
        private string CelIdentidad { get; set; }

        //CONSTRUCTOR
        //Asignar valores a sus propiedades cuando se crea un nuevo objeto.
        public Persona(string nombreCompleto, DateTime fechaNacimiento, string sexo, string estadoCivil, string telefono, string celular, string email, string direccion, string nacionalidad, string celIdentidad)
        {
            NombreCompleto = nombreCompleto;
            FechaNacimiento = fechaNacimiento;
            Sexo = sexo;
            EstadoCivil = estadoCivil;
            Telefono = telefono;
            Celular = celular;
            Email = email;
            Direccion = direccion;
            Nacionalidad = nacionalidad;
            CelIdentidad = celIdentidad;
            CelIdentidad = celIdentidad;
        }
        //Este método devuelve una cadena de texto que contiene información sobre la persona, la información se devolverá en la lista 
        public abstract string ObtenerInformacion();

    }
}

