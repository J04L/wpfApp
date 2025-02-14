using System.IO;
using System.Net.Http;
using System.Text;
using app_wpf.Model;
using Newtonsoft.Json;

namespace app_wpf;
public class ApiClient
{
    public static async Task<int> GetPrecioMaximo()
    {
        using (HttpClient client = new HttpClient())
        {
            string url = "http://localhost:3036/api/habitacionPrecioMax";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                String json = await response.Content.ReadAsStringAsync();
                return int.Parse(json);
            }
            return 0;
        }
    }
    public static async Task<int> GetPisoMaximo()
    {
        using (HttpClient client = new HttpClient())
        {
            string url = "http://localhost:3036/api/habitacionPisoMax";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                String json = await response.Content.ReadAsStringAsync();
                return int.Parse(json);
            }
            return 0;
        }
    }

    public static async Task PostHabitacion(String rutaImagen, Habitacion habitacion)
    {
        using (FileStream fs = new FileStream(rutaImagen, FileMode.Open, FileAccess.Read))
        using (StreamContent streamContent = new StreamContent(fs))
        using (HttpClient client = new HttpClient())
        using (MultipartFormDataContent form = new MultipartFormDataContent())
        {
            var extension = fs.Name.Substring(fs.Name.LastIndexOf('.') + 1);
            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/" + extension);
            form.Add(streamContent, "imagen", Path.GetFileName(rutaImagen));
            // Convertir objeto Habitacion a JSON
            string json = JsonConvert.SerializeObject(habitacion);
            StringContent jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            // Agregar la habitacion en JSON como otro campo del formulario
            form.Add(jsonContent, "habitacion");

            // Enviar la petición POST
            HttpResponseMessage response = await client.PostAsync(" http://localhost:3036/api/habitacion", form);
            string result = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Respuesta del servidor: " + result);
        }
    }

    public static async Task PutHabitacion(Habitacion habitacion)
    {
        using (HttpClient client = new HttpClient())
        {
            // Convertir objeto Habitacion a JSON
            string json = JsonConvert.SerializeObject(habitacion);
            StringContent jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

            // Enviar la petición POST
            HttpResponseMessage response = await client.PutAsync($" http://localhost:3036/api/habitacion/{habitacion.NumeroHabitacion}", jsonContent);
            string result = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Respuesta del servidor: " + result);
        }
    }
    public static async Task DeleteHabitacion(Habitacion habitacion)
    {
        using (HttpClient client = new HttpClient())
        {
            // Enviar la petición POST
            HttpResponseMessage response = await client.DeleteAsync($" http://localhost:3036/api/habitacion/{habitacion.NumeroHabitacion}");
            string result = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Respuesta del servidor: " + result);
        }
    }

    public static async Task<List<Habitacion>> GetHabitacionesFiltradasAsync(
        string nombreTipo = null,
        double? precioMin = null,
        double? precioMax = null,
        int? cantHuespedes = null,
        bool? disponible = null,
        int? pisoMin = null,
        int? pisoMax = null
    )
    {
        using (HttpClient client = new HttpClient())
        {
            string url = "http://localhost:3036/api/habitacion?";
            List<string> filtros = new List<string>();

            if (!string.IsNullOrEmpty(nombreTipo))
                filtros.Add($"nombreTipo={nombreTipo}");
            if (precioMin.HasValue)
                filtros.Add($"precioMin={precioMin.Value}");
            if (precioMax.HasValue)
                filtros.Add($"precioMax={precioMax.Value}");
            if (cantHuespedes.HasValue)
                filtros.Add($"cantHuespedes={cantHuespedes.Value}");
            if (disponible.HasValue)
                filtros.Add($"disponible={disponible.Value.ToString().ToLower()}");
            if (pisoMin.HasValue)
                filtros.Add($"pisoMin={pisoMin.Value}");
            if (pisoMax.HasValue)
                filtros.Add($"pisoMax={pisoMax.Value}");
                

            // Concatenamos los filtros en la URL
            if (filtros.Count > 0)
                url += string.Join("&", filtros);

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<Habitacion> habitaciones = JsonConvert.DeserializeObject<List<Habitacion>>(json);
                return habitaciones;
            } 
            return null;
        }
    }
    public static async Task<List<TipoHabitacion>> GetTipoHabitaciones()
    {
        using (HttpClient client = new HttpClient())
        {
            string url = "http://localhost:3036/api/tipoHabitaciones";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                List<TipoHabitacion> tipoHabitaciones = JsonConvert.DeserializeObject<List<TipoHabitacion>>(json);
                return tipoHabitaciones;
            }

            return null;
        }
    }
}