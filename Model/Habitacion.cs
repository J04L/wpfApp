using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace app_wpf.Model
{
    public class Habitacion
    {
        [JsonProperty ("numeroHabitacion")]
        public int NumeroHabitacion { get; set; }
        
        [JsonProperty ("tipoHabitacion")]
        public TipoHabitacion TipoHabitacion { get; set; }
        
        [JsonProperty ("descripcion")]
        public string Descripcion { get; set; }
        
        [JsonProperty ("precio")]
        public double Precio { get; set; }
        
        [JsonProperty ("fotos")]
        public List<string> Fotos { get; set; }
        
        [JsonProperty ("camas")]
        public Camas Camas { get; set; }
        
        [JsonProperty ("dimesiones")]
        public double Dimensiones { get; set; }
        
        [JsonProperty ("disponible")]
        public bool Disponible { get; set; }
        
        [JsonProperty ("piso")]
        public int Piso { get; set; }

        public Habitacion(
            int numeroHabitacion,
            TipoHabitacion tipoHabitacion,
            Capacidad capacidad,
            string descripcion,
            double precio,
            List<string> fotos,
            Camas camas,
            double dimensiones,
            bool disponible,
            int piso)
        {
            NumeroHabitacion = numeroHabitacion;
            TipoHabitacion = tipoHabitacion;
            Descripcion = descripcion;
            Precio = precio;
            Fotos = fotos;
            Camas = camas;
            Dimensiones = dimensiones;
            Disponible = disponible;
            Piso = piso;
        }
    }

    public class HabitacionListView
    {
        public int NumeroHabitacion { get; set; }
        public string TipoHabitacion { get; set; }
        public WrapPanel Camas {get; set; }
        public string Capacidad { get; set; }
        public string Disponible { get; set; }
        public string Dimensiones { get; set; }
        public int Piso { get; set; }
        public double Precio { get; set; }

        public HabitacionListView(Habitacion habitacion)
        {
            NumeroHabitacion = habitacion.NumeroHabitacion;
            TipoHabitacion = habitacion.TipoHabitacion.NombreTipoHabitacion;
            Camas = GetCamasDataGrid(habitacion);
            Dimensiones = habitacion.Dimensiones + "m\u00b2";
            Capacidad = $"{habitacion.TipoHabitacion.Capacidad.Adultos} adultos, {habitacion.TipoHabitacion.Capacidad.Menores} menores";
            Disponible = habitacion.Disponible? "Disponible" : "No disponible";
            Piso = habitacion.Piso;
            Precio = habitacion.Precio;
        }
        public WrapPanel GetCamasDataGrid(Habitacion habitacion)
        {
            WrapPanel wrapPanel = new WrapPanel();
            int imgWidth = 30;
            
            for (int i = 0; i <habitacion.Camas.Individual;i++)
            {
                Image image = new Image();
                image.Width = imgWidth;
                image.Source = new BitmapImage(new Uri("pack://application:,,,/image/camaIndividualIcon.png"));
                wrapPanel.Children.Add(image);
            }
            for (int i = 0; i <habitacion.Camas.Doble; i++)
            {
                Image image = new Image();
                image.Width = imgWidth;
                image.Source = new BitmapImage(new Uri("pack://application:,,,/image/camaDobleIcon.png"));
                wrapPanel.Children.Add(image);
            }

            return wrapPanel;
        }
    }

    public class TipoHabitacion
    {
        [JsonProperty("nombreTipo")]
        public string NombreTipoHabitacion { get; set; }
        
        [JsonProperty("precioBase")]
        public double PrecioBase{ get; set; }
        
        [JsonProperty ("capacidadPersonas")]
        public Capacidad Capacidad { get; set; }
        
        [JsonProperty ("capacidadCamas")]
        public int CapacidadCamas { get; set; }
    }
    public class Capacidad
    {
        [JsonProperty("adultos")]
        public int Adultos { get; set; }
        
        [JsonProperty("menores")]
        public int Menores { get; set; }

        public Capacidad(int adultos, int menores)
        {
            Adultos = adultos;
            Menores = menores;
        }
    }
    public class Camas
    {
        [JsonProperty("individual")]
        public int Individual { get; set; }
        
        [JsonProperty("doble")]
        public int Doble { get; set; }

        public Camas(int individual, int doble)
        {
            Individual = individual;
            Doble = doble;
        }
    }
}
