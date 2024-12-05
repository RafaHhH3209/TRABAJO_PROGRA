using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System;
using Microsoft.Win32;

namespace EX_AUTO_AP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string rutaArchivo = "C:\\Users\\HP\\Videos\\ex\\ARCHIVOS_TXT\\Agenda.txt";

        //Instancia de la clase Tienda, que se usa para gestionar la lista de personas
        private Tienda tiendita = new Tienda();
        // información de una persona temporalmente
        Contactos contactos;
        public MainWindow()
        {
            InitializeComponent();
        }

        //Este método se ejecuta al hacer clic en el botón para registrar una persona
        #region Botton_Registro_Personas
        private void Button_R_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //Inicializamos las variables en sus respectivos lugares
                string celIdentidad = TextBox_CI.Text;
                string nombreCompleto = TextBoc_NA.Text;
                string sexo = ComboBox_Se.Text;
                string estadoCivil = ComboBox_EC.Text;
                string fechaNacimiento = TextBox_FN.Text;
                string telefono = TextBox_Tel.Text;
                string celular = TextBox_Celula.Text;
                string email = TextBox_Email.Text;
                string direccion = TextBox_Di.Text;
                string nacionalidad = ComboBox_Nacionalidad.Text;

                //VALIDACION DE CAMPOS OBLIGATORIOS
                if (string.IsNullOrEmpty(celIdentidad)) { throw new Exception("El campo cedula de identidad no puede estar vacio"); }
                if (string.IsNullOrEmpty(nombreCompleto)) { throw new Exception("El campo nombre completo no puede estar vacio"); }
                if (string.IsNullOrEmpty(sexo)) { throw new Exception("El campo sexo no puede estar vacio"); }
                if (string.IsNullOrEmpty(estadoCivil)) { throw new Exception("El campo estado  no puede estar vacio"); }
                if (string.IsNullOrEmpty(telefono)) { throw new Exception("El campo telefono no puede estar vacio"); }
                if (string.IsNullOrEmpty(celular)) { throw new Exception("El campo celular no puede estar vacio"); }

                ValidarCi(celIdentidad);
                ValidarNombres(nombreCompleto);
                ValidarTelefono(telefono);
                ValidarCelular(celular);

                //VALIDACION DE CAMPOS OPCIONALES
                if (!string.IsNullOrEmpty(email)) ValidarEmail(email);
                if (!string.IsNullOrEmpty(direccion)) ValidarDireccion(direccion);
                if (!DateTime.TryParse(fechaNacimiento, out DateTime fechaN)) ; //LO CONVIERTE A FECHAN, por que se autorellena?


                // Crear nueva persona y agregar a la tienda
                //Necesario para obtenerInformación de GuardarTexto 
                Contactos contactos = new Contactos(nombreCompleto, fechaN, sexo, estadoCivil, telefono, celular, email, direccion, nacionalidad, celIdentidad);

                // Agregar la persona a la tienda
                tiendita.AgregarPersona(contactos);

                // Guardar la persona en el archivo, sin sobreescritura 
                Archivo.GuardarTexto(tiendita, rutaArchivo, contactos);

                ActualizarPersona();
            }
            //En caso de no cumplir con las verificaciones de los campos lanza una excepcion
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error de Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion


        //Este método actualiza la lista de personas en la interfaz.
        //Limpia el listado actual y añade la información de todas las personas registradas en tiendita. Utilizado en el registro Persona
        #region ActualizarPersona
        private void ActualizarPersona()
        {
            PersonasLista.Items.Clear(); //Limpia los items en la persona
            foreach (var contactos in tiendita.ObtenerPersonas()) //para cada persona obtenemos su información
            {
                PersonasLista.Items.Add(contactos.ObtenerInformacion()); //anadimos la información en persona
            }
        }
        #endregion


        //Este método se llama al hacer clic en el botón para limpiar todos los campos de entrada en el formulario
        #region Botton_Limpiar
        private void Button_L_Click(object sender, RoutedEventArgs e)
        {
            TextBox_CI.Clear();
            TextBoc_NA.Clear();
            TextBox_FN.Clear();
            TextBox_Tel.Clear();
            TextBox_Celula.Clear();
            TextBox_Email.Clear();
            TextBox_Di.Clear();

            ComboBox_Se.SelectedIndex = -1;
            ComboBox_EC.SelectedIndex = -1;
            ComboBox_Nacionalidad.SelectedIndex = -1;
        }
        #endregion

        //Este método permite buscar a una persona utilizando su cédula de identidad.
        //Si se encuentra, se rellenan todos los campos del formulario con la información de la persona con un MessageBox.
        #region BucarPersona
        private void Button_B_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el CI ingresado en el TextBox
            string ciBuscar = TextBox_CI.Text;

            // Llamar al método de búsqueda en la clase Archivo
            string resultadoBusqueda = Archivo.BuscarPersona(ciBuscar, rutaArchivo);

            // Mostrar el resultado en un MessageBox, en el primer caso si ingresa todos los datos correctamente solo aparecerá Resulado Busqueda, caso contrario error
            MessageBox.Show(resultadoBusqueda, "Resultado de Búsqueda", MessageBoxButton.OK, MessageBoxImage.Information);
        }



        #endregion

        //Este método muestra el contenido del archivo de texto y actualiza la lista de personas en la interfaz
        #region MostrarPersona
        private void Button_M_Click(object sender, RoutedEventArgs e)
        {
            // Crear un cuadro de diálogo para abrir archivo
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                Title = "Selecciona el archivo de agenda"
            };

            // Verificar si el usuario selecciona un archivo
            if (openFileDialog.ShowDialog() == true)
            {
                string rutaArchivo = openFileDialog.FileName; //openFileDialog devuelve la ruta completa del archivo que el usuario selecciona mediante el diálogo de apertura de archivos

                try
                {
                    PersonasLista.Items.Clear(); // Limpiar el ListBox antes de cargar nuevos datos

                    string[] lineas = File.ReadAllLines(rutaArchivo); // Leer todas las líneas del archivo

                    // Agregar cada línea como un nuevo elemento en el ListBox
                    foreach (string linea in lineas)
                    {
                        PersonasLista.Items.Add(linea);
                    }
                }
                catch (IOException ex)
                {
                    // Manejo de errores de lectura
                    throw new Exception($"Error al leer el archivo: {ex.Message}");
                }
            }
            else
            {
                // Manejo en caso de que no se seleccione ningún archivo
                throw new Exception("No se seleccionó ningún archivo.");
            }
        }
        #endregion


        //Este método cierra la ventana actual de la aplicación
        private void Button_S_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Cierra la ventana actual, que también termina la aplicación si es la única ventana abierta.
        }

        //VALIDACION DE LOS DATOS
        #region Validacion
        static public string ValidarCi(string celIdentidad) //validacion de la cedula de identidad
        {
            if (celIdentidad.Length == 8) //si la celula de identidad tiene caracteres iguales a 8 
            {
                for (int i = 0; i < celIdentidad.Length; i++)
                {
                    if (celIdentidad[i] < 48 || celIdentidad[i] > 57)
                    {
                        throw new Exception("La cédula de identidad debe ser un valor numérico entero"); //mandamos un mensaje de error si la celula no es un numero
                    }
                }
                return celIdentidad; //retornamos si todo salio bien
            }
            else
            {
                throw new ArgumentException("La cédula de identidad debe ser un valor numérico entero de exactamente 8 dígitos."); //mensaje de error si no es exactamente de 8 digitos la cedula de identidad
            }
        }

        static public string ValidarNombres(string nombreCompleto) //validacion del nombre completo
        {
            // Verifica si la primera letra es mayúscula
            if (nombreCompleto[0] < 65 || nombreCompleto[0] > 90)
            {
                throw new Exception("El nombre debe comenzar con una letra mayúscula.");

            }
            // Verifica cada carácter del nombre
            for (int i = 0; i < nombreCompleto.Length; i++)
            {
                if ((nombreCompleto[i] < 65 || nombreCompleto[i] > 90) && (nombreCompleto[i] < 97 || nombreCompleto[i] > 122) && (nombreCompleto[i] != 32))
                {
                    throw new Exception("El nombre solo debe contener letras y espacios.");
                }
            }
            return nombreCompleto; //retornamos si todo salio bien
        }

        static void ValidarFEchaNacimiento(DateTime fechaNacimiento)
        {
            // Calcula la edad
            int edad = DateTime.Now.Year - fechaNacimiento.Year;
            // Comprobamos si la persona aún no ha cumplido la edad este año
            if (fechaNacimiento > DateTime.Now.AddYears(-edad))
            {
                edad--;  // Si la persona aún no ha cumplido, le restamos 1 a la edad
            }

            // Validar que la edad esté en el rango deseado
            if (edad < 1 || edad >= 100)
            {
                throw new ArgumentException("La edad de la fecha de nacimiento debe estar entre 1 y 99.");
            }
        }

        static public string ValidarTelefono(string telefono)
        {
            if (telefono.Length == 7) //verificamos si el telefono tiene 7 caracteres
            {
                for (int i = 0; i < telefono.Length; i++)
                {
                    if (telefono[i] < 48 || telefono[i] > 57) //si son numeros
                    {
                        throw new Exception("El telefono solo debe tener numeros"); //mensaje de error si no lo son
                    }
                }
                return telefono; //retornamos si todo salio bien
            }
            else
            {
                throw new ArgumentException("El teléfono debe ser un valor numérico entero de exactamente 7 dígitos."); //mensaje de error si no tiene exactamente 7 digitos
            }
        }

        static public string ValidarCelular(string celular)
        {
            if (celular.Length == 8) //verificamos si el celular tiene 8 digitos
            {
                for (int i = 0; i < celular.Length; i++)
                {
                    if (celular[i] < 48 || celular[i] > 57)
                    {
                        throw new Exception("El celular solo debe tener numeros"); //mensaje de error si no tiene numeros
                    }
                }
                return celular; //retornamos si todo salio bien
            }
            else
            {
                throw new ArgumentException("El celular debe ser un valor numérico entero de exactamente 8 dígitos."); //mensaje de error si no tiene exactamente 8 digitos
            }
        }

        static public string ValidarEmail(string email)
        {
            if (!(email.Contains("@")) || !(email.Contains("."))) //verificamos si el email contiene arroba y punto
            {
                throw new ArgumentException("el correo no es valido");
            }
            else
            {
                return email;//retornamos si todo salio bien
            }
        }

        static public string ValidarDireccion(string direccion)
        {
            return direccion;//retornamos si todo salio bien
        }
        #endregion
    }
}