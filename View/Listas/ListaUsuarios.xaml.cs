using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using app_wpf.image;
using app_wpf.Model;
using Newtonsoft.Json;

namespace app_wpf
{
    /// <summary>
    /// Lógica de interacción para ListaUsuarios.xaml
    /// </summary>
    public partial class ListaUsuarios : Window
    {
        public ListaUsuarios()
        {
            InitializeComponent();
	    FillDataGridUsuarios();
        }

        public async void FillDataGridUsuarios()
        {
            try
            {
                Usuario[] usuariosEnBDD = await GetAllUsuariosTask();

                if (usuariosEnBDD != null)
                {
                    ObservableCollection<UsuarioListView> usuarios = new ObservableCollection<UsuarioListView>();

                    foreach (var usuario in usuariosEnBDD)
                    {
                        usuarios.Add(new UsuarioListView(usuario));
                    }

                    DataGridUsuarios.ItemsSource = usuarios;  // Enlaza los datos al DataGrid
                }
                else
                {
                    DataGridUsuarios.ItemsSource = null;  // Vacía el DataGrid si no hay datos
                    MessageBox.Show("No se encontraron usuarios en la base de datos.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task<Usuario[]> GetAllUsuariosTask()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "http://localhost:3036/api/users";
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Usuario[] usuarios = JsonConvert.DeserializeObject<Usuario[]>(json);
                    return usuarios;
                }
                else return null;
            }
        }

        public async Task<Usuario[]> GetFilteredUsuariosTask(Dictionary<string, object> filtro)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "http://localhost:3036/api/getFilter";
                var jsonFiltro = JsonConvert.SerializeObject(filtro);
                var content = new StringContent(jsonFiltro, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Usuario[] usuarios = JsonConvert.DeserializeObject<Usuario[]>(json);
                    return usuarios;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task FiltrarUsuarios()
        {
            var filtro = new Dictionary<string, object>
    {
        { "nombre", "Juan" },
        { "email", "juan@example.com" }
    };

            Usuario[] usuariosFiltrados = await GetFilteredUsuariosTask(filtro);

            if (usuariosFiltrados != null)
            {
                foreach (var usuario in usuariosFiltrados)
                {
                    Console.WriteLine($"Nombre: {usuario.Nombre}, Email: {usuario.Email}");
                }
            }
            else
            {
                Console.WriteLine("No se encontraron usuarios con el filtro aplicado.");
            }
        }

    

        public async Task<Usuario> GetUsuarioByEmail(string email)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"http://localhost:3036/api/getFilter";
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

        private void ListaHabitaciones_click(object sender, RoutedEventArgs e)
        {
            MainWindow listaHabitaciones = new MainWindow();
            listaHabitaciones.Show();
            Close();
        }

        private void NuevoUser_click(object sender, RoutedEventArgs e)
        {
            NuevoUser newUser = new NuevoUser();
            newUser.Show();
            Close();
        }

    }
}
