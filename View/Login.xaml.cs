using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using app_wpf.Model;
using app_wpf.View.Creacion;
using app_wpf.View.Listas;
using Newtonsoft.Json;

namespace app_wpf
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly HttpClient _httpClient;  // Ahora es un HttpClient real

        public Login()
        {
            InitializeComponent();
            _httpClient = new HttpClient(); // Inicializamos el HttpClient
        }

        private async void Login_click(object sender, RoutedEventArgs e)
        {
            string email = TbxMail.Text.Trim();
            string password = TbxPasswd.Password.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("⚠️ Debes ingresar un email y una contraseña.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Preparamos el JSON con los datos del login
                var requestBody = new { email, password };
                string json = JsonConvert.SerializeObject(requestBody);
                Console.WriteLine($"📤 Enviando JSON: {json}");

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:3036/usuarios/logincorporate", content);
                string responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"📥 Respuesta API: {responseString}");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("✅ Inicio de sesión exitoso.", "Bienvenido", MessageBoxButton.OK, MessageBoxImage.Information);
                    Reservas Reservas = new Reservas();
                    Reservas.Show();
                    Close();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    MessageBox.Show("❌ Acceso denegado: Solo los administradores pueden iniciar sesión.",
                        "Error de autenticación", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("❌ Error al iniciar sesión.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en Login_click: {ex.Message}");
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
