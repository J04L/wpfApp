using app_wpf.Model;
using app_wpf.View.Creacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



namespace app_wpf.View.Edicion
{
    /// <summary>
    /// Lógica de interacción para EditarReserva.xaml
    /// </summary>
    public partial class EditarReserva : Window
    {
        // Propiedad que representará los datos de la reserva
        public ReservasModel Reserva { get; set; }
        private readonly HttpClient _httpClient = new HttpClient();




        // Constructor de la ventana de edición
        public EditarReserva(ReservasModel reserva)
        {
            InitializeComponent();

            // Asignar la reserva a la propiedad
            Reserva = reserva;

            // Establecer el contexto de datos (DataContext) de la ventana para enlazar los campos
            this.DataContext = Reserva;
        }

        private async void GuardarCambiosButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Crear el objeto con los datos que se van a enviar al servidor
                var reservaData = new
                {
                   
                    n_habitacion = NumeroHabitacionLabel.Text,
                    huespedEmail = EmailTextBox.Text,
                    huespedDni = DniTextBox.Text,
                    f_Inicio = FechaEntradaDatePicker.SelectedDate?.ToString("yyyy-MM-dd"),
                    f_Final = FechaSalidaDatePicker.SelectedDate?.ToString("yyyy-MM-dd"),
                    huespedNombre = NombreTextBox.Text,
                    huespedApellidos = ApellidosTextBox.Text,
                    precio_total = PrecioTotalLabel.Text,
                    tipo_habitacion = TipoHabitacionLabel.Text
                };

                // Realizar la petición PATCH al endpoint
                var client = new HttpClient();
                var response = await client.PatchAsJsonAsync("http://localhost:3036/api/reservas/update", reservaData);

                if (response.IsSuccessStatusCode)
                {
                    // Si la actualización fue exitosa, mostrar mensaje de éxito
                    MessageBox.Show("Reserva actualizada exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Cerrar la ventana después de guardar
                    this.Close();
                }
                else
                {
                    // Si ocurrió un error, mostrar el mensaje de error
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al actualizar la reserva: {errorMessage}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Capturar cualquier error de la solicitud o excepciones y mostrar el mensaje
                MessageBox.Show($"Error al realizar la solicitud: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


       /* private async void ComprobarCambiosButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtener los datos de la interfaz
                var fechaInicio = FechaEntradaDatePicker.SelectedDate?.ToString("yyyy-MM-dd");
                var fechaFin = FechaSalidaDatePicker.SelectedDate?.ToString("yyyy-MM-dd");
                var capacidad = int.TryParse(HuespedesTextBox.Text, out var cap) ? cap : (int?)null;
                var tipo = TipoHabitacionLabel.Content?.ToString();
                var vip = false; // Aquí puedes agregar lógica para obtener el valor VIP si lo tienes en la interfaz
                var oferta = false; // Aquí puedes agregar lógica para obtener el valor de oferta si lo tienes en la interfaz
                var extras = new
                {
                    cuna = false, // Aquí puedes agregar lógica para obtener el valor de cuna si lo tienes en la interfaz
                    camaExtra = false // Aquí puedes agregar lógica para obtener el valor de cama extra si lo tienes en la interfaz
                };

                // Crear el objeto con los datos para enviar al endpoint
                var datosBusqueda = new
                {
                    capacidad,
                    fecha_inicio = fechaInicio,
                    fecha_fin = fechaFin,
                    tipo,
                    vip,
                    oferta,
                    extras
                };

                // Enviar la solicitud al endpoint
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:3036/api/reservas/"); // Cambia esto por la URL de tu API
                    var response = await client.PostAsJsonAsync("habitaciones/libres", datosBusqueda);

                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta del servidor
                        var habitacionesDisponibles = await response.Content.ReadFromJsonAsync<List<Habitacion>>();

                        if (habitacionesDisponibles != null && habitacionesDisponibles.Any())
                        {
                            // Obtener la primera habitación disponible
                            var primeraHabitacion = habitacionesDisponibles.First();

                            // Actualizar la interfaz con los datos de la habitación
                            NumeroHabitacionLabel.Text = primeraHabitacion.NumeroHabitacion.ToString();
                            TipoHabitacionLabel.Content = primeraHabitacion.TipoHabitacion.NombreTipoHabitacion;
                            PrecioTotalLabel.Content = primeraHabitacion.Precio.ToString("C"); // Formato de moneda
                                                                                               // Aquí puedes actualizar otros campos si es necesario
                        }
                        else
                        {
                            MessageBox.Show("No hay habitaciones disponibles para los criterios seleccionados.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al obtener las habitaciones disponibles.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }*/
    }
}