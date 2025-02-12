using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Net.Http;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using Accessibility;
using app_wpf.Model;
using Newtonsoft.Json;


namespace app_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeFiltro();

        }

        public async void InitializeFiltro()
        {
            //establece el contenido del combobox del filtro (Individual, Doble, Familiar, Suite, Triple)
            var tipoHabitaciones = await ApiClient.GetTipoHabitaciones();
            var lista = tipoHabitaciones.Select(p => p.NombreTipoHabitacion).Append("Cualquiera").ToList();
            tipoHabitacionesFiltroCombobox.ItemsSource = lista;
            tipoHabitacionesFiltroCombobox.SelectedIndex = lista.FindIndex(nombre => nombre == "Cualquiera");

            //precio filtro
            precioMinFiltro.Text = "0";
            precioMaxFiltro.Text = (await  ApiClient.GetPrecioMaximo()).ToString();

            //piso filtro
            pisoMinFiltro.Text = "0";
            pisoMaxFiltro.Text = (await ApiClient.GetPisoMaximo()).ToString();

            //disponibilidad filtro
            disponibleFiltro.IsChecked = null;

            //huespedes
            huespedesSliderFiltro.Value = 1;

            fillLisViewHabitaciones();

        }

        public async void fillLisViewHabitaciones()
        {
            Habitacion[] HabitacionesEnBDD = await ApiClient.GetHabitacionesFiltradasAsync(
                (tipoHabitacionesFiltroCombobox.SelectedItem.ToString() == "Cualquiera"
                    ? null
                    : tipoHabitacionesFiltroCombobox.SelectedItem.ToString()),
                double.Parse(precioMinFiltro.Text),
                double.Parse(precioMaxFiltro.Text),
                (int)huespedesSliderFiltro.Value,
                disponibleFiltro.IsChecked,
                int.Parse(pisoMinFiltro.Text),
                int.Parse(pisoMaxFiltro.Text)
            );
            List<HabitacionListView> habitaciones = new List<HabitacionListView>();

            foreach (var habitacion in HabitacionesEnBDD)
            {
                habitaciones.Add(new HabitacionListView(habitacion));
            }

            DataGridHabitaciones.ItemsSource = habitaciones;
        }

        public void RangeBase_OnValueChanged(Object sender, EventArgs e)
        {
            if (TextBlockSlider != null) TextBlockSlider.Text = huespedesSliderFiltro.Value.ToString();
        }

        private void ButtonFiltro_OnClick(object sender, RoutedEventArgs e)
        {
           
        }
    }

}