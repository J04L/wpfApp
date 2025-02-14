using System.Windows;
using System.Windows.Controls;
using app_wpf.Model;
using Microsoft.Win32;

namespace app_wpf
{
    public partial class CrearHabitacion : Window
    {
        private CrearHabitacionVM _viewModel = new CrearHabitacionVM();

        public CrearHabitacion()
        {
            InitializeComponent();
            DataContext = _viewModel;
            setTipoHabitaciones();

        }

        public async void setTipoHabitaciones()
        {
            var lista = await ApiClient.GetTipoHabitaciones();
            _viewModel.ListaTipoHabitaciones = lista;
            TipoHabitacionesComboBox.ItemsSource = lista.Select(tipo => tipo.NombreTipoHabitacion);
            TipoHabitacionesComboBox.SelectedIndex = 0;

            // Establecer valores iniciales
            _viewModel.TipoHabitacion = lista[0].NombreTipoHabitacion;
            _viewModel.Numero = 6;
            _viewModel.CapacidadAdultos = 2;
            _viewModel.CapacidadNinos = 1;
            _viewModel.Descripcion = "Nueva habitación";
            _viewModel.CamasDobles = 1;
            _viewModel.CamasIndividuales = 1;
            _viewModel.Piso = 1;
            _viewModel.Dimensiones = 30;
        }

        private void ButtonSumarCamaDoble_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.CamasDobles++;
        }

        private void ButtonRestarCamaDoble_OnClick(object sender, RoutedEventArgs e)
        {
            if (_viewModel.CamasDobles > 0) _viewModel.CamasDobles--;
        }

        private void ButtonSumarCamaIndividual_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.CamasIndividuales++;
        }

        private void ButtonRestarCamaIndividual_OnClick(object sender, RoutedEventArgs e)
        {
            if (_viewModel.CamasIndividuales > 0) _viewModel.CamasIndividuales--;
        }

        private void SeleccionarImagenButton_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Filter = "\"Imágenes|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Todos los archivos|*.",
                Title = "Selecciona una imagen"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string rutaImagen = openFileDialog.FileName;
                Console.Write(openFileDialog.FileName);
                _viewModel.Foto = rutaImagen;

            }
        }

        private async void GuardarHabitacionButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                await ApiClient.PostHabitacion(_viewModel.Foto, _viewModel.GetHabitacion());
                MessageBox.Show(
                    "Habitación creada",
                    "Éxito",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
                Close(); // Se ejecuta solo cuando la tarea ha terminado
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al guardar la habitación: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }

    }
}
