using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EX_AUTO_AP
{
    public class Contactos : Persona
    {
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

        public Contactos(string nombreCompleto, DateTime fechaNacimineto, string sexo, string estadoCivil, string telefono, string celular, string email, string direccion, string nacionalidad, string celIdentidad)
            : base(nombreCompleto, fechaNacimineto, sexo, estadoCivil, telefono, celular, email, direccion, nacionalidad,celIdentidad)
        {
            NombreCompleto = nombreCompleto;
            FechaNacimiento = fechaNacimineto;
            Sexo = sexo;
            EstadoCivil = estadoCivil;
            Telefono = telefono;
            Celular = celular;  
            Email = email;
            Direccion = direccion;
            Nacionalidad = nacionalidad; 
            CelIdentidad = celIdentidad;
        }

        public string ObtenerID() { return CelIdentidad; }

        public override string ObtenerInformacion()
        {
            return $"Nombre Completo: {NombreCompleto}, fecha de Nacimiento: {FechaNacimiento:dd/MM/yyyy}, Sexo: {Sexo}, Estado civil: {EstadoCivil}, Tel: {Telefono}, Cel: {Celular}, Email: {Email}, Dirección: {Direccion}, Nacionalidad: {Nacionalidad}, CI: {CelIdentidad}";
        }
    }
}
