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
using app_wpf.Model;
using Newtonsoft.Json;

namespace app_wpf
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        public async Task<Usuario> GetUsuarioByEmail(string email)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"http://localhost:3036/usuarios/getOne";
                var requestBody = new StringContent(JsonConvert.SerializeObject(new { email }), System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, requestBody);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Usuario usuario = JsonConvert.DeserializeObject<Usuario>(json);
                    return usuario;
                }
                else return null;
            }
        }
    }
}
