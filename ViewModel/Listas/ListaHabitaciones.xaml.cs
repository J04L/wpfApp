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
            fillLisViewHabitaciones();
        }

        public async void fillLisViewHabitaciones()
        {
            Habitacion[] HabitacionesEnBDD =  await GetAllHabitacionesTask();
            List<HabitacionListView> habitaciones = new List<HabitacionListView>();
            
            foreach (var habitacion in HabitacionesEnBDD)
            {
                habitaciones.Add(new HabitacionListView(habitacion));
            }
            DataGridHabitaciones.ItemsSource = habitaciones;
        }
        
        public void RangeBase_OnValueChanged(Object sender, EventArgs e)
        {
            if (TextBlockSlider != null) TextBlockSlider.Text = HuespedesSlider.Value.ToString();
        }

        public async Task<Habitacion[]> GetAllHabitacionesTask()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "http://localhost:3036/api/habitacion";
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Habitacion[] habitacion = JsonConvert.DeserializeObject<Habitacion[]>(json);
                    return habitacion;
                }
                else return null;
            }
        }

        public async Task<Habitacion> GetHabitacionById(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"http://localhost:3036/api/habitacion/{id}";
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Habitacion habitacion = JsonConvert.DeserializeObject<Habitacion>(json);
                    return habitacion;
                }
                else return null;
            }
        }
    }

}