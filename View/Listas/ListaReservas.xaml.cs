using app_wpf.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http.Json;


namespace app_wpf.View.Listas
{
    public partial class ListaReservas : Window
    {
        private const string EndpointReservas = "http://localhost:3036/api/reservas/reservas";
        public ObservableCollection<ReservasModel> ListaDeReservas { get; set; } = new();

        public ListaReservas()
        {
            InitializeComponent();
            DataGridReservas.ItemsSource = ListaDeReservas;
            BuscarReservasButton.Click += BuscarReservasButton_Click;
        }

        private async void BuscarReservasButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var filtros = new
                {
                    n_habitacion = HabitacionTextBox.Text,
                    f_Inicio = InicioDatePicker.SelectedDate?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    //f_Inicio = "01-02-2024",
                    f_Final = FinalDatePicker.SelectedDate?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    numeroHuespedes = int.TryParse(HuespedesTextBox.Text, out int hues) ? hues : (int?)null,
                    precioMin = decimal.TryParse(PrecioTotalMin.Text, out decimal minTotal) ? minTotal : (decimal?)null,
                    precioMax = decimal.TryParse(PrecioTotalMax.Text, out decimal maxTotal) ? maxTotal : (decimal?)null,
                    precioNocheMin = decimal.TryParse(PrecioDiaMin.Text, out decimal minDia) ? minDia : (decimal?)null,
                    precioNocheMax = decimal.TryParse(PrecioDiaMax.Text, out decimal maxDia) ? maxDia : (decimal?)null,
                };

                using HttpClient client = new();
                HttpResponseMessage response = await client.PostAsJsonAsync(EndpointReservas, filtros);
                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var reservas = JsonSerializer.Deserialize<List<ReservasModel>>(responseContent);
                        if (reservas != null)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                ListaDeReservas.Clear();
                                foreach (var reserva in reservas)
                                {
                                    ListaDeReservas.Add(reserva);
                                }
                            });
                        }
                        else
                        {
                            MessageBox.Show("No se pudo deserializar la respuesta correctamente.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (JsonException jsonEx)
                    {
                        MessageBox.Show($"Error de deserialización: {jsonEx.Message}", "Error de Deserialización", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error al obtener las reservas.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\nStackTrace: {ex.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DataGridReservas_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
