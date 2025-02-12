using System.Windows;
using System.Windows.Controls;
using app_wpf.Model;

namespace app_wpf.View.Edicion
{
    public partial class EditarHabitacion : Window
    {
        private EditarHabitacionVM _viewModel = new EditarHabitacionVM();
        public Habitacion Habitacion;
        public EditarHabitacion()
        {
            InitializeComponent();
            Habitacion = HabitacionPrueba();
            DataContext = _viewModel;
            setTipoHabitaciones();

        }
        public async void setTipoHabitaciones()
        {
            var lista = await ApiClient.GetTipoHabitaciones();
            TipoHabitacionesComboBox.ItemsSource = lista.Select(tipo => tipo.NombreTipoHabitacion);
            TipoHabitacionesComboBox.SelectedIndex = lista.FindIndex(tipo => tipo.NombreTipoHabitacion == Habitacion.TipoHabitacion.NombreTipoHabitacion);
            
            // Establecer valores iniciales
            _viewModel.ListaTipoHabitaciones = lista;
            _viewModel.Numero = Habitacion.NumeroHabitacion;
            _viewModel.Precio = Habitacion.Precio;
            _viewModel.CapacidadAdultos = Habitacion.TipoHabitacion.Capacidad.Adultos;
            _viewModel.CapacidadNinos =  Habitacion.TipoHabitacion.Capacidad.Adultos;
            _viewModel.Descripcion = Habitacion.Descripcion;
            _viewModel.CamasDobles = Habitacion.Camas.Doble;
            _viewModel.CamasIndividuales = Habitacion.Camas.Individual;
            _viewModel.Piso = Habitacion.Piso;
            _viewModel.Dimensiones = Habitacion.Dimensiones;
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

        public Habitacion HabitacionPrueba()
        {
            // Crear la capacidad de la habitación (2 adultos, 1 menor)
            Capacidad capacidad = new Capacidad(2, 1);

            // Crear el tipo de habitación
            TipoHabitacion tipoHabitacion = new TipoHabitacion
            {
                NombreTipoHabitacion = "Suite",
                PrecioBase = 200.50,
                Capacidad = capacidad,
                CapacidadCamas = 3
            };

            // Crear las camas (1 individual, 1 doble)
            Camas camas = new Camas(4, 1);

            // Crear la habitación con los valores de prueba
            Habitacion habitacion = new Habitacion(
                numeroHabitacion: 101,
                tipoHabitacion: tipoHabitacion,
                capacidad: capacidad, 
                descripcion: "Una suite ejecutiva con vista al mar.",
                precio: 300.75,
                fotos: new List<string> { "foto1.jpg", "foto2.jpg" },
                camas: camas,
                dimensiones: 45.5,
                disponible: true,
                piso: 5
            );
            return habitacion;
        }
    }
}