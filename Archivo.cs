using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
using System.Windows;
using EX_AUTO_AP;

namespace EX_AUTO_AP
{
    internal class Archivo
    {
        // Guarda una nueva persona en el archivo de texto al momento de registrar
        #region GuardamosTexto
        public static void GuardarTexto(Tienda tiendita, string rutaArchivo, Contactos nuevaPersona)
        {
            //intentar abrir el archivo y escribir en el 
            try
            {
                using (StreamWriter writer = new StreamWriter(rutaArchivo, append: true)) // StreamWriter: Abre el archivo en la ruta especificada para escritura. Append evita sobreescritura
                {
                    writer.WriteLine(nuevaPersona.ObtenerInformacion()); //Usa el objeto StreamWriter (aquí llamado writer) para escribir una línea en el archivo de texto
                }
                Console.WriteLine("Archivo guardado exitosamente.");
            }
            ///caso contrario error
            catch (IOException)
            {
                throw new IOException("Error al escribir en el archivo de texto. Verifique que la ruta del archivo es correcta y el archivo no está siendo utilizado por otro programa.");
            }
        }
        #endregion

        // Método para buscar persona en el archivo de texto
        public static string BuscarPersona(string ciBuscar, string rutaArchivo)
        {
            // Validar si el CI es nulo o vacío
            if (string.IsNullOrEmpty(ciBuscar))
            {
                return "Ingrese la cédula de identidad para realizar la búsqueda.";
            }

            // Intentar convertir el CI a un número, ignorando el valor de salida ( no almacena ese ci)
            if (!int.TryParse(ciBuscar, out _))
            {
                return "La cédula de identidad no es válida.";
            }

            try
            {
                // Leer todas las líneas del archivo
                string[] lineas = File.ReadAllLines(rutaArchivo);

                // Buscar la línea que contiene la cédula de identidad
                foreach (var linea in lineas)
                {
                    if (linea.Contains($"CI: {ciBuscar}"))
                    {
                        // Dividir y desglosar los campos
                        string[] datos = linea.Split(',');

                        //([0]) contiene el nombre de la propiedad y [1]) contiene el valor: Ejemplo (Nombre Completo: Juan Perez)
                        //Se usa Trim() para eliminar posibles espacios en blanco alrededor del valor.
                        string nombreCompleto = datos[0].Split(':')[1].Trim();
                        string fechaNacimiento = datos[1].Split(':')[1].Trim();
                        string sexo = datos[2].Split(':')[1].Trim();
                        string estadoCivil = datos[3].Split(':')[1].Trim();
                        string telefono = datos[4].Split(':')[1].Trim();
                        string celular = datos[5].Split(':')[1].Trim();
                        string email = datos[6].Split(':')[1].Trim();
                        string direccion = datos[7].Split(':')[1].Trim();
                        string nacionalidad = datos[8].Split(':')[1].Trim();

                        // Crear un mensaje con la información
                        string mensaje = $"Nombre: {nombreCompleto}\n" +
                                         $"Cédula de Identidad: {ciBuscar}\n" +
                                         $"Fecha de Nacimiento: {fechaNacimiento}\n" +
                                         $"Sexo: {sexo}\n" +
                                         $"Estado Civil: {estadoCivil}\n" +
                                         $"Teléfono: {telefono}\n" +
                                         $"Celular: {celular}\n" +
                                         $"Email: {email}\n" +
                                         $"Dirección: {direccion}\n" +
                                         $"Nacionalidad: {nacionalidad}";

                        //retorna el mensaje que luego será utilizado en el Button_B_Click
                        return mensaje;
                    }
                }
                // caso de error en el messageBox de Button_B_Click mostrara ese error
                return "Persona no encontrada.";
            }
            //caprurando la excepción
            catch (IOException ex)
            {
                return $"Error al leer el archivo: {ex.Message}";
            }
        }

    }
}
