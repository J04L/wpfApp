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

namespace app_wpf
{
    public partial class CrearHabitacion : Window
    {
        public static Dictionary<string, double> tipoHabitacionesDictionary = new Dictionary<string, double>();
        public CrearHabitacion ()
        {
            InitializeComponent();
            setTipoHabitacionesDiccionari();

        }
        public async Task<bool> setTipoHabitacionesDiccionari()
        {
            string url = "http://localhost:3036/api/tipoHabitaciones";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<TipoHabitacion> listaTiposHabitacoines = JsonConvert.DeserializeObject<List<TipoHabitacion>>(json);

                    Dictionary<string, double> dictionary = new Dictionary<string, double>();
                    foreach (var tipoHabitacion in listaTiposHabitacoines)
                    {
                        dictionary[tipoHabitacion.nombreTipo] = tipoHabitacion.precioBase;
                    }

                    tipoHabitacionesDictionary = dictionary;
                    TipoHabitacionesComboBox.ItemsSource = getNombreTipoHabitaciones();
                    return true;
                }
            }

            return false;
        }

        public List<string> getNombreTipoHabitaciones()
        {
            List<string> list = new List<string>();

            foreach (var tipoHabitacion in tipoHabitacionesDictionary)
            {
                list.Add(tipoHabitacion.Key);
            }

            return list;
        }
        public class TipoHabitacion
        {
            public string nombreTipo { get; set; }
            public double precioBase { get; set; }

            public TipoHabitacion(string nombreTipo, double precioBase)
            {
                this.nombreTipo = nombreTipo;
                this.precioBase = precioBase;
            }
        }
    }
}
