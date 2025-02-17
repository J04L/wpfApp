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
using app_wpf.View.Edicion;
using app_wpf.View.Creacion;


namespace app_wpf.View.Listas
{
    public partial class ListaReservas : Window
    {
        private const string EndpointReservas = "http://localhost:3036/api/reservas/reservas";
        public List<ReservasModel> ListaDeReservas { get; set; } = new();

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
                                // Limpiar la lista de reservas
                                ListaDeReservas.Clear();

                                // Añadir las reservas recibidas
                                foreach (var reserva in reservas)
                                {
                                    ListaDeReservas.Add(reserva);
                                }

                                // Reasignar el ItemsSource para reflejar los cambios
                                DataGridReservas.ItemsSource = null;
                                DataGridReservas.ItemsSource = ListaDeReservas;
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

        private async void EliminarReservaButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Verificar si hay una reserva seleccionada en el DataGrid
            var reservaSeleccionada = DataGridReservas.SelectedItem as ReservasModel;
            if (reservaSeleccionada != null)
            {
                // Mostrar mensaje de confirmación
                var resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar esta reserva?",
                                                 "Confirmación",
                                                 MessageBoxButton.YesNo,
                                                 MessageBoxImage.Question);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        using HttpClient client = new HttpClient();
                        client.DefaultRequestHeaders.Add("Accept", "application/json"); // Para asegurar que la respuesta sea en formato JSON

                        // Crear el objeto con el _id de la reserva a eliminar
                        var datos = new { _id = reservaSeleccionada._id };

                        // Convertir el objeto a JSON
                        var jsonContent = new StringContent(JsonSerializer.Serialize(datos), Encoding.UTF8, "application/json");

                        // Enviar la solicitud DELETE con el _id en el cuerpo
                        HttpResponseMessage response = await client.SendAsync(new HttpRequestMessage
                        {
                            Method = HttpMethod.Delete,
                            RequestUri = new Uri("http://localhost:3036/api/reservas/delete"),
                            Content = jsonContent
                        });

                        if (response.IsSuccessStatusCode)
                        {
                            // Eliminar la reserva de la lista
                            ListaDeReservas.Remove(reservaSeleccionada);

                            // Actualizar el DataGrid
                            DataGridReservas.ItemsSource = null;
                            DataGridReservas.ItemsSource = ListaDeReservas;

                            MessageBox.Show("Reserva eliminada exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        else
                        {
                            string mensajeError = await response.Content.ReadAsStringAsync();
                            MessageBox.Show($"Error: {mensajeError}", "Error al eliminar", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}\nStackTrace: {ex.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una reserva para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void EditarReservaButton_OnClick(object sender, RoutedEventArgs e)
        {
            var reservaSeleccionada = DataGridReservas.SelectedItem as ReservasModel;
            if (reservaSeleccionada != null)
            {
                // Crear la ventana de edición y pasar la reserva seleccionada
                var ventanaEdicion = new EditarReserva(reservaSeleccionada);
                ventanaEdicion.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una reserva para editar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Reservar_click(object sender, RoutedEventArgs e)
        {
            var reservar = new Reservas();
            reservar.Show();
            Close();
        }

        private void Listausuarios_click(object sender, RoutedEventArgs e)
        {
            var listaUsuarios = new ListaUsuarios();
            listaUsuarios.Show();
            Close();
        }

        private void ListaReservas_click(object sender, RoutedEventArgs e)
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
