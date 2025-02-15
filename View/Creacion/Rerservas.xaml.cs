using app_wpf.Model;
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

                // Obtener el tipo de habitación
                string tipo = TipoHabitacionNormal.IsChecked == true ? "Normal" :
                              TipoHabitacionSuit.IsChecked == true ? "Suit" :
                              TipoHabitacionPresidencial.IsChecked == true ? "Presidencial" : null;

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
                    tipo,
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
                    MessageBox.Show("Respuesta JSON: " + responseBody);
                    //var habitaciones = JsonSerializer.Deserialize<List<Habitacion>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    List<Habitacion> habitaciones = JsonConvert.DeserializeObject<List<Habitacion>>(responseBody);

                    const string BASE_URL = "http://localhost:3036/img/";

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

        private void ReservarHabitacion_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Habitación reservada.");
        }
    }
}
