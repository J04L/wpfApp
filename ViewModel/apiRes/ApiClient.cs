using System.Net.Http;
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
    public static async Task<Habitacion[]> GetHabitacionesFiltradasAsync(
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
                Habitacion[] habitaciones = JsonConvert.DeserializeObject<Habitacion[]>(json);
                return habitaciones;
            }
            else return null;
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