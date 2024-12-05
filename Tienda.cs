using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace EX_AUTO_AP
{
    public class Tienda 
    {
        // personita es una lista que almacenará concactos de Persona
        // Esto permite tener un registro de todas las personas gestionadas por la tienda
        public List<Contactos> personita = new List<Contactos>(); //COMPOSICION
        //public Carrera CarreraEstudio { get; set; } // Agregación


        //Método recibe un objeto de tipo Persona y lo agrega a la lista personita
        //A traves de la persona dato que recibe por medio del parametro
        #region AgregarPersona
        public void AgregarPersona(Contactos contactos)
        {
            //Buscar si ya existe una persona con la misma cel identidad 
            Persona existe = personita.FirstOrDefault(p => p.ObtenerID() == contactos.ObtenerID());
            //agregar persona
            if (existe == null)
            {
                personita.Add(contactos);
            }
            //en caso contrario mostrar error
            else
            {
                throw new Exception($"la persona con este ci {contactos.ObtenerID()} ya fue registrada");
            }
        }
        #endregion


        //Este método devuelve la lista completa de personas almacenadas en personita
        //Permite acceder a todos los registros de una vez. Utilizado en actualizar persona
        #region ObtenerPersona
        public List<Contactos> ObtenerPersonas()
        {
            return personita;
        }
        #endregion


        //Este método busca una persona en la lista utilizando su número de cédula de identidad (CelIdentidad). Utilizado en el Button_B
        //Si encuentra una coincidencia, devuelve el objeto Persona, o null si no hay coincidencias.
        #region BuscarPersona
        public Persona BuscarPersona(string ci)
        {
            return personita.FirstOrDefault(p => p.ObtenerID() == ci); // Busca por la primera cédula de identidad
        }
        #endregion

    }

}