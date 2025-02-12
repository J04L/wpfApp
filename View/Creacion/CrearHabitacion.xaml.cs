using System.Windows;
using System.Windows.Controls;
using app_wpf.Model;

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
            TipoHabitacionesComboBox.ItemsSource = lista.Select(tipo => tipo.NombreTipoHabitacion);
            TipoHabitacionesComboBox.SelectedIndex = 0;
            
            // Establecer valores iniciales
            _viewModel.ListaTipoHabitaciones = lista;
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
    }
}
