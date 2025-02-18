using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using app_wpf.Model;
using Microsoft.Win32;

namespace app_wpf
{
    public partial class EditarHabitacion : Window
    {
        private EditarHabitacionVM _viewModel = new EditarHabitacionVM();
        public Habitacion Habitacion;
        public EditarHabitacion(Habitacion habitacion)
        {
            InitializeComponent();
            Habitacion = habitacion;
            DataContext = _viewModel;
            setTipoHabitaciones();

        }
        public async void setTipoHabitaciones()
        {
            var lista = await ApiClient.GetTipoHabitaciones();
            TipoHabitacionesComboBox.ItemsSource = lista.Select(tipo => tipo.NombreTipoHabitacion);
            TipoHabitacionesComboBox.SelectedIndex = lista.FindIndex(tipo => tipo.NombreTipoHabitacion == Habitacion.TipoHabitacion.NombreTipoHabitacion);
            
            // Establecer valores iniciales
            _viewModel.habitaciones = await ApiClient.GetHabitaciones();
            _viewModel.ListaTipoHabitaciones = lista;
            _viewModel.NumeroOriginal = Habitacion.NumeroHabitacion;
            _viewModel.Numero = _viewModel.NumeroOriginal;
            _viewModel.Precio = Habitacion.Precio;
            _viewModel.CapacidadAdultos = Habitacion.TipoHabitacion.Capacidad.Adultos;
            _viewModel.CapacidadNinos =  Habitacion.TipoHabitacion.Capacidad.Menores;
            _viewModel.Descripcion = Habitacion.Descripcion;
            _viewModel.CamasDobles = Habitacion.Camas.Doble;
            _viewModel.CamasIndividuales = Habitacion.Camas.Individual;
            _viewModel.Piso = Habitacion.Piso;
            _viewModel.Dimensiones = Habitacion.Dimensiones;
            _viewModel.Foto = Habitacion.Fotos[0];
            Console.WriteLine(Habitacion.Fotos[0]);
        }

        private void ButtonSumarCamaDoble_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.CamasDobles++;
        }

        private void ButtonRestarCamaDoble_OnClick(object sender, RoutedEventArgs e)
        {
            if(_viewModel.CamasDobles > 0) _viewModel.CamasDobles--;
        }

        private void ButtonSumarCamaIndividual_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.CamasIndividuales++;
        }

        private void ButtonRestarCamaIndividual_OnClick(object sender, RoutedEventArgs e)
        {
            if (_viewModel.CamasIndividuales > 0) _viewModel.CamasIndividuales--;
        }

        private async void GuardarHabitacionButton_OnCLick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!_viewModel.HasErrors)
                {
                    await ApiClient.PutHabitacion(_viewModel.NumeroOriginal, _viewModel.GetHabitacion());
                    MessageBox.Show(
                        "Habitación editada",
                        "Éxito",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information
                    );
                    _viewModel.habitaciones = await ApiClient.GetHabitaciones();
                    Close(); // Se ejecuta solo cuando la tarea ha terminado
                    
                }
                else
                {
                    MessageBox.Show(
                        $"Corrige los errores antes de editar",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al editar la habitación: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}