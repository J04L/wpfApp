using app_wpf.Model;
using app_wpf.View.Listas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace app_wpf.View.Creacion
{
    public partial class Reservas : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public Reservas()
        {
            InitializeComponent();
        }

        private async void BuscarHabitaciones_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Manejo seguro de fechas
                string fechaInicio = FechaEntrada.SelectedDate?.ToString("yyyy-MM-dd") ?? "";
                string fechaFin = FechaSalida.SelectedDate?.ToString("yyyy-MM-dd") ?? "";

                // Manejo seguro de capacidad
                int capacidad = 1;
                if (CapacidadComboBox.SelectedItem is ComboBoxItem selectedItem &&
                    int.TryParse(selectedItem.Content.ToString(), out int parsedCapacidad))
                {
                    capacidad = parsedCapacidad;
                }

                

                bool vip = VipCheckBox.IsChecked == true;
                bool oferta = OfertaCheckBox.IsChecked == true;

                // Extras
                bool cuna = CheckCuna.IsChecked == true;
                bool camaExtra = CheckCamaExtra.IsChecked == true;

                var requestData = new
                {
                    capacidad,
                    fecha_inicio = fechaInicio,
                    fecha_fin = fechaFin,
                    
                    vip,
                    oferta,
                    extras = new { cuna, camaExtra }
                };

                string json = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:3036/api/reservas/habitaciones/libres", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    //var habitaciones = JsonSerializer.Deserialize<List<Habitacion>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    List<Habitacion> habitaciones = JsonConvert.DeserializeObject<List<Habitacion>>(responseBody);

                    const string BASE_URL = "http://localhost:3036/";

                    foreach (var habitacion in habitaciones)
                    {
                        if (habitacion.Fotos != null && habitacion.Fotos.Count > 0)
                        {
                            habitacion.Fotos[0] = BASE_URL + habitacion.Fotos[0];
                        }
                    }
                    HabitacionesItemsControl.ItemsSource = habitaciones;

                }
                else
                {
                    MessageBox.Show($"Error al obtener habitaciones. Código: {response.StatusCode}");
                    Console.WriteLine($"Error al obtener habitaciones. Código: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
        }

        private void Reservar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Habitacion habitacion)
            {
                // Obtener solo las fechas seleccionadas
                DateTime fechaEntrada = FechaEntrada.SelectedDate ?? DateTime.Now;
                DateTime fechaSalida = FechaSalida.SelectedDate ?? DateTime.Now.AddDays(1);

                // Obtener solo el número de huéspedes del ComboBox
                int numHuespedes = 1; // Valor por defecto
                if (CapacidadComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    if (int.TryParse(selectedItem.Content.ToString(), out int capacidad))
                    {
                        numHuespedes = capacidad;
                    }
                }

                // Calcular el precio total (Precio * Días de estancia)
                int diasTotales = (int)(fechaSalida - fechaEntrada).TotalDays;
                if (diasTotales <= 0) diasTotales = 1; // Asegurar al menos 1 día de estancia
                double precioTotal = diasTotales * habitacion.Precio;

                // Obtener el tipo de habitación y la primera foto
                string tipoHabitacion = habitacion.TipoHabitacion.NombreTipoHabitacion;
                int numeroHabitacion = habitacion.NumeroHabitacion; // Número de habitación
                string fotoHabitacion = habitacion.Fotos?.FirstOrDefault(); // Obtener la primera foto

                // Pasar los datos a la ventana CrearReserva (sin indicadores)
                CrearReserva ventanaReserva = new CrearReserva(habitacion, fechaEntrada, fechaSalida, numHuespedes, precioTotal, tipoHabitacion, fotoHabitacion, numeroHabitacion);
                ventanaReserva.Owner = Application.Current.MainWindow;
                ventanaReserva.ShowDialog();
                // Reiniciar la lista de habitaciones después de la reserva
                HabitacionesItemsControl.ItemsSource = null;  // Limpiar el ItemsSource
            }
        }

        private void ListaReservas_click(object sender, RoutedEventArgs e)
        {
            var listaReservas = new ListaReservas();
            listaReservas.Show();
            Close();
        }

        private void Listausuarios_click(object sender, RoutedEventArgs e)
        {
            var listaUsuarios = new ListaUsuarios();
            listaUsuarios.Show();
            Close();
            }

        private void ReservarView_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Ya estas en esta ventana", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

        }

        private void ListaHabitaciones_click(object sender, RoutedEventArgs e)
        {
            var listaHabitaciones = new MainWindow();
            listaHabitaciones.Show();
            Close();
        }


    }
}
