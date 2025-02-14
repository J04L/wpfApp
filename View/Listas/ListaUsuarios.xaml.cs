using app_wpf.image;
using app_wpf.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static app_wpf.Model.Usuario;

namespace app_wpf
{
    public partial class ListaUsuarios : Window
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public ListaUsuarios()
        {
            InitializeComponent();
            _ = FillDataGridUsuarios();
        }

        private async void BtnEditarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridUsuarios.SelectedItem is UsuarioListView usuario)
            {
                try
                {
                    var editUser = new NuevoUser(usuario.Id);  // Usamos el ID del usuario
                    MessageBox.Show(usuario.Id+"");
                    editUser.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al editar el usuario: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un usuario para editar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void BtnEliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridUsuarios.SelectedItem is UsuarioListView usuario)
            {
                var resultado = MessageBox.Show($"¿Está seguro de que desea eliminar a {usuario.Nombre}?",
                    "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        string url = $"http://localhost:3036/usuarios/deleteUserById/{usuario.Id}";  // Usamos el ID para eliminar
                        HttpResponseMessage response = await _httpClient.DeleteAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                            await FillDataGridUsuarios();
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar el usuario. Inténtelo de nuevo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un usuario para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public async Task FillDataGridUsuarios(bool applyFilter = false, Dictionary<string, object> filtro = null)
        {
            try
            {
                Usuario[] usuariosEnBDD = applyFilter && filtro != null
                    ? await GetFilteredUsuariosTask(filtro)
                    : await GetAllUsuariosTask();

                if (usuariosEnBDD?.Length > 0)
                {
                    var usuarios = new ObservableCollection<UsuarioListView>();
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
                MessageBox.Show($"Ocurrió un error al cargar los usuarios: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task<Usuario[]> GetAllUsuariosTask()
        {
            try
            {
                string url = "http://localhost:3036/usuarios/users";
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Usuario[]>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener todos los usuarios: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return Array.Empty<Usuario>();
            }
        }

        public async Task<Usuario[]> GetFilteredUsuariosTask(Dictionary<string, object> filtro)
        {
            try
            {
                string url = "http://localhost:3036/usuarios/getFilter";
                var content = new StringContent(JsonConvert.SerializeObject(filtro), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Usuario[]>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aplicar el filtro: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return Array.Empty<Usuario>();
            }
        }

        private async void BtnBuscarFiltro(object sender, RoutedEventArgs e)
        {
            await FiltrarUsuarios();
        }

        private async Task FiltrarUsuarios()
        {
            var filtro = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(TbxNombre.Text))
                filtro["nombre"] = TbxNombre.Text;

            if (!string.IsNullOrWhiteSpace(TbxApellido.Text))
                filtro["apellido"] = TbxApellido.Text;

            if (!string.IsNullOrWhiteSpace(TbxEmail.Text))
                filtro["email"] = TbxEmail.Text;

            if (CbxRol.SelectedItem is ComboBoxItem rolItem && !string.IsNullOrWhiteSpace(rolItem.Content.ToString()))
                filtro["role"] = rolItem.Content;

            if (DtpNacimiento.SelectedDate.HasValue)
                filtro["birthday"] = DtpNacimiento.SelectedDate.Value.ToString("yyyy-MM-dd");

            if (CbxCiudad.SelectedItem is ComboBoxItem ciudadItem && !string.IsNullOrWhiteSpace(ciudadItem.Content.ToString()))
                filtro["ciudad"] = ciudadItem.Content;

            if (RBM.IsChecked == true) filtro["sex"] = "M";
            else if (RBF.IsChecked == true) filtro["sex"] = "F";
            else if (RBUndetermined.IsChecked == true) filtro["sex"] = "Indeterminado";

            if (TgglVip.IsChecked == true) filtro["vip"] = true;

            await FillDataGridUsuarios(true, filtro);
        }


        private void ListaHabitaciones_click(object sender, RoutedEventArgs e)
        {
            var listaHabitaciones = new MainWindow();
            listaHabitaciones.Show();
            Close();
        }

        private void NuevoUser_click(object sender, RoutedEventArgs e)
        {
            var newUser = new NuevoUser(null);
            newUser.ShowDialog();
            _ = FillDataGridUsuarios();
        }
    }
}
