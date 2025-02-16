using app_wpf.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace app_wpf.View.Creacion
{
    /// <summary>
    /// Lógica de interacción para CrearReserva.xaml
    /// </summary>
    public partial class CrearReserva : Window
    {
        public Habitacion habitacionSeleccionada;
        private DateTime fechaEntrada;
        private DateTime fechaSalida;
        private int numeroHuespedes;
        private double precioTotal;

        public CrearReserva(Habitacion habitacion, DateTime fechaEntrada, DateTime fechaSalida, int numHuespedes, double precioTotal, string tipoHabitacion, string fotoHabitacion, int numeroHabitacion)
        {
            InitializeComponent();
            this.habitacionSeleccionada = habitacion;
            this.fechaEntrada = fechaEntrada;
            this.fechaSalida = fechaSalida;
            this.numeroHuespedes=numHuespedes;
            this.precioTotal=precioTotal;

            // Asignar los valores a los controles correspondientes en la UI
            NumeroHabitacionLabel.Content = $"{numeroHabitacion}";  // Mostrar el número de habitación
            TipoHabitacionLabel.Content = tipoHabitacion;  // Label que muestra el tipo de habitación
            FotoHabitacionImage.Source = new BitmapImage(new Uri(fotoHabitacion, UriKind.RelativeOrAbsolute)); // Imagen de la habitación

            FechaEntradaLabel.Content = fechaEntrada.ToShortDateString(); // Mostrar solo la fecha de entrada
            FechaSalidaLabel.Content = fechaSalida.ToShortDateString(); // Mostrar solo la fecha de salida
            HuespedesLabel.Content = numHuespedes; // Mostrar número de huéspedes
            PrecioTotalLabel.Content = $"{precioTotal} €"; // Mostrar precio total

            
        }

        private void MostrarDatos()
        {
            // Mostrar la imagen de la habitación
            if (habitacionSeleccionada.Fotos.Count > 0)
            {
                string rutaImagen = "/RutaBase/" + habitacionSeleccionada.Fotos[0]; // Ajusta la ruta según corresponda
                FotoHabitacionImage.Source = new BitmapImage(new Uri(rutaImagen, UriKind.RelativeOrAbsolute));
            }

            // Mostrar la información de la habitación
            NumeroHabitacionLabel.Content = $"{habitacionSeleccionada.NumeroHabitacion}";
            FechaEntradaLabel.Content = $"{fechaEntrada:dd/MM/yyyy}";
            FechaSalidaLabel.Content = $"{fechaSalida:dd/MM/yyyy}";
            HuespedesLabel.Content = $"{numeroHuespedes}";

            // Mostrar el precio total
            PrecioTotalLabel.Content = $"{precioTotal:C}";
        }

        private async void CrearReservaButton_Click(object sender, RoutedEventArgs e)
        {
            // Comprobaciones para asegurarse de que todos los campos obligatorios están completos
            string huespedNombre = NombreTextBox.Text;
            string huespedApellidos = ApellidosTextBox.Text;
            string huespedEmail = EmailTextBox.Text;
            string huespedDni = DniTextBox.Text;

            if (string.IsNullOrEmpty(huespedNombre) || string.IsNullOrEmpty(huespedApellidos) || string.IsNullOrEmpty(huespedEmail) || string.IsNullOrEmpty(huespedDni))
            {
                MessageBox.Show("Por favor, rellena todos los campos.");
                return;
            }

            if (!Regex.IsMatch(huespedEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("El formato del correo electrónico no es válido.");
                return;
            }

            // Validar formato del DNI (Ejemplo para España: 8 dígitos seguidos de una letra)
            if (!Regex.IsMatch(huespedDni, @"^\d{8}[A-Z]$"))
            {
                MessageBox.Show("El formato del DNI no es válido. Debe contener 8 números y una letra (Ej: 12345678A).");
                return;
            }


            // Verificar que habitacionSeleccionada no es null
            if (habitacionSeleccionada == null)
            {
                MessageBox.Show("Por favor, selecciona una habitación.");
                return;
            }

            // Verificar que el precio de la habitación es válido
            if (habitacionSeleccionada.Precio <= 0)
            {
                MessageBox.Show("El precio de la habitación no es válido.");
                return;
            }

            // Verificar que la capacidad del ComboBox de huéspedes es válida
            if (numeroHuespedes <= 0)
            {
                MessageBox.Show("El número de huéspedes debe ser mayor que 0.");
                return;
            }

            // Crear el objeto de reserva con los datos que el usuario ha proporcionado
            ReservasModel reserva = new ReservasModel
            {
                n_habitacion = habitacionSeleccionada.NumeroHabitacion.ToString(),  // Número de la habitación
                tipo_habitacion = habitacionSeleccionada.TipoHabitacion?.NombreTipoHabitacion,  // Tipo de habitación
                f_Inicio = fechaEntrada,
                f_Final = fechaSalida,
                totalDias = (int)(fechaSalida - fechaEntrada).TotalDays,  // Calcular los días de estancia
                huespedEmail = huespedEmail,
                huespedNombre = huespedNombre,
                huespedApellidos = huespedApellidos,
                huespedDni = huespedDni,
                trabajadorEmail = "trabajador@example.com",  // Este valor puede ser extraído de la sesión o un campo de la UI
                numeroHuespedes = numeroHuespedes,
                precio_noche = (decimal)habitacionSeleccionada.Precio,  // Precio de la noche
                precio_total = (decimal)this.precioTotal,  // Precio total de la reserva
                cuna = false,  // Si tienes un CheckBox para cuna
                camaExtra = false,  // Si tienes un CheckBox para cama extra
                notificar = true  // Si tienes un CheckBox para notificación
            };


            // Llamar a la API para guardar la reserva
            bool resultado = await CrearReservaEnApiAsync(reserva);

            // Si la reserva se crea correctamente, cerrar la ventana
            if (resultado)
            {
                MessageBox.Show("Reserva creada exitosamente.");
                this.Close(); // Cerrar la ventana
            }
            else
            {
                MessageBox.Show("Hubo un error al crear la reserva. Por favor, intente nuevamente.");
            }
        }

        // Método para enviar los datos de la reserva a la API
        private async Task<bool> CrearReservaEnApiAsync(ReservasModel reserva)
        {
            try
            {
                var client = new HttpClient();
                var url = "http://localhost:3036/api/reservas/new"; 

                var json = JsonConvert.SerializeObject(reserva);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Error al crear la reserva: " + errorResponse);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al intentar conectar con el servidor: " + ex.Message);
                return false;
            }
        }
    }

}
