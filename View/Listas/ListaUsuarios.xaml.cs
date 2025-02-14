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
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace app_wpf
{
    /// <summary>
    /// Lógica de interacción para ListaUsuarios.xaml
    /// </summary>
    public partial class ListaUsuarios : Window
    {
        bool firstTime = true;
        public ICommand EliminarCommand { get; }

        public ListaUsuarios()
        {
            InitializeComponent();
	        FillDataGridUsuarios();
            EliminarCommand = new RelayCommand<UsuarioListView>(async (usuario) => await EliminarUsuario(usuario));
        }

        public async void FillDataGridUsuarios(bool applyFilter = false, Dictionary<string, object> filtro = null)
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

                    DataGridUsuarios.ItemsSource = usuarios;
                }
                else
                {
                    DataGridUsuarios.ItemsSource = null;
                    MessageBox.Show("No se encontraron usuarios.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
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
                string url = "http://localhost:3036/usuarios/users";
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
                string url = "http://localhost:3036/usuarios/getFilter";
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
            var filtro = new Dictionary<string, object>();

            // Agregar nombre al filtro si no está vacío
            if (!string.IsNullOrWhiteSpace(TbxNombre.Text))
            {
                filtro["nombre"] = TbxNombre.Text;
            }

            // Agregar apellido al filtro si no está vacío
            if (!string.IsNullOrWhiteSpace(TbxApellido.Text))
            {
                filtro["apellido"] = TbxApellido.Text;
            }

            // Agregar email al filtro si no está vacío
            if (!string.IsNullOrWhiteSpace(TbxEmail.Text))
            {
                filtro["email"] = TbxEmail.Text;
            }

            // Agregar rol si se seleccionó uno
            if (CbxRol.SelectedItem != null)
            {
                filtro["role"] = (CbxRol.SelectedItem as ComboBoxItem).Content.ToString();
            }

            // Agregar fecha de nacimiento si se seleccionó una
            if (DtpNacimiento.SelectedDate.HasValue)
            {
                filtro["birthday"] = DtpNacimiento.SelectedDate.Value.ToString("yyyy-MM-dd");
            }

            // Agregar ciudad si se seleccionó una
            if (CbxCiudad.SelectedItem != null)
            {
                filtro["ciudad"] = (CbxCiudad.SelectedItem as ComboBoxItem).Content.ToString();
            }

            // Agregar sexo si se seleccionó uno
            if (RBM.IsChecked == true)
            {
                filtro["sex"] = "M";
            }
            else if (RBF.IsChecked == true)
            {
                filtro["sex"] = "F";
            }
            else if (RBUndetermined.IsChecked == true)
            {
                filtro["sex"] = "Indeterminado";
            }

            // Agregar VIP si está activado
            if (TgglVip.IsChecked == true)
            {
                filtro["vip"] = true;
            }

            // Llamar a la API para obtener los usuarios filtrados
            Usuario[] usuariosFiltrados = await GetFilteredUsuariosTask(filtro);

            if (usuariosFiltrados != null && usuariosFiltrados.Length > 0)
            {
                ObservableCollection<UsuarioListView> usuarios = new ObservableCollection<UsuarioListView>();

                foreach (var usuario in usuariosFiltrados)
                {
                    usuarios.Add(new UsuarioListView(usuario));
                }

                DataGridUsuarios.ItemsSource = usuarios;
            }
            else
            {
                DataGridUsuarios.ItemsSource = null;
                MessageBox.Show("No se encontraron usuarios con el filtro aplicado.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async Task EliminarUsuario(UsuarioListView usuario)
        {
            if (usuario == null) return;

            var resultado = MessageBox.Show($"¿Está seguro de que desea eliminar a {usuario.Nombre}?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        string url = $"http://localhost:3036/usuarios/deleteUser/";
                        HttpResponseMessage response = await client.DeleteAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                            // Refresca el DataGrid después de eliminar el usuario
                            FillDataGridUsuarios();
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar el usuario. Inténtelo de nuevo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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

        private void BtnBuscarFiltro(object sender, RoutedEventArgs e)
        {
            FiltrarUsuarios();
        }
    }
}
