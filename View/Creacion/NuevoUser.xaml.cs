using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using app_wpf.Model;
using Newtonsoft.Json;

namespace app_wpf.image
{
    public partial class NuevoUser : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl = "http://localhost:3036/usuarios";
        private string _userId = null;  // Guardamos el ID del usuario para ediciones

        public NuevoUser(string id)
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(id))
            {
                _userId = id;
                _ = RellenarDatos(id);
                BtnCrearEditar.Content = "Editar";
                TblockTitulo.Text = "Editar usuario";
            }
        }

        public async Task RellenarDatos(string _id)
        {
            Usuario usuario = await GetOneUser(_id);
            if (usuario != null)
            {
                _userId = usuario.Id;
                TbxNombre.Text = usuario.Nombre;
                TbxApellido.Text = usuario.Apellido;

                // Seleccionar el rol en el ComboBox
                foreach (var item in CbxRol.Items)
                {
                    if (item is ComboBoxItem comboItem && comboItem.Content != null)
                    {
                        if (string.Equals(comboItem.Content.ToString(), usuario.Role?.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            CbxRol.SelectedItem = comboItem;
                            break;
                        }
                    }
                }

                TbxEmail.Text = usuario.Email;
                TbxDni.Text = usuario.Dni;
                DatPickNacimiento.Text = usuario.Birthday?.ToString("yyyy-MM-dd");

                // Rellenar los PasswordBox
                PwdPass.Password = usuario.Password;
                PwdConfirmPass.Password = usuario.Password;

                // Seleccionar la ciudad en el ComboBox
                foreach (var item in CbxCiudad.Items)
                {
                    if (item is ComboBoxItem comboItem && comboItem.Content != null)
                    {
                        if (string.Equals(comboItem.Content.ToString(), usuario.Ciudad, StringComparison.OrdinalIgnoreCase))
                        {
                            CbxCiudad.SelectedItem = comboItem;
                            break;
                        }
                    }
                }

                // Sexo
                if (!string.IsNullOrEmpty(usuario.Sex))
                {
                    if (string.Equals(usuario.Sex, "M", StringComparison.OrdinalIgnoreCase))
                        RBM.IsChecked = true;
                    else if (string.Equals(usuario.Sex, "F", StringComparison.OrdinalIgnoreCase))
                        RBF.IsChecked = true;
                    else
                        RBUndetermined.IsChecked = true;
                }

                // Toggle VIP
                ToggleButton.IsChecked = usuario.Vip == true;
            }
        }




        public async Task<Usuario> GetOneUser(string id)
        {
            try
            {
                string url = $"{_baseUrl}/getById";
                var requestData = new { id };
                var jsonContent = JsonConvert.SerializeObject(requestData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Usuario>(json);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Usuario no encontrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show($"Error en la solicitud: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Excepción", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return null;
        }

        public async Task<bool> UpdateUsuario(string idUsuario, Dictionary<string, object> datosUsuario)
        {
            try
            {
                string url = $"{_baseUrl}/update/{idUsuario}";
                var jsonDatos = JsonConvert.SerializeObject(datosUsuario);
                var content = new StringContent(jsonDatos, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Usuario actualizado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show($"Error al actualizar el usuario: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public async Task<bool> CrearUsuario(Dictionary<string, object> datosUsuario)
        {
            try
            {
                string url = $"{_baseUrl}/newUser";
                var jsonDatos = JsonConvert.SerializeObject(datosUsuario);
                var content = new StringContent(jsonDatos, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Usuario creado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show($"Error al crear el usuario: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private async void crearEditar(object sender, RoutedEventArgs e)
        {
            Dictionary<string, object> datosUsuario = new Dictionary<string, object>
            {
                ["nombre"] = TbxNombre.Text,
                ["apellido"] = TbxApellido.Text,
                ["email"] = TbxEmail.Text,
                ["dni"] = TbxDni.Text,
                ["birthday"] = DatPickNacimiento.SelectedDate?.ToString("yyyy-MM-dd"),
                ["password"] = PwdPass.Password,
                ["ciudad"] = CbxCiudad.Text,
                ["role"] = CbxRol.Text,
                ["sex"] = RBM.IsChecked == true ? "M" : RBF.IsChecked == true ? "F" : "Sin determinar",
                ["vip"] = ToggleButton.IsChecked == true
            };

            if (BtnCrearEditar.Content.Equals("Editar") && !string.IsNullOrEmpty(_userId))
            {
                MessageBox.Show(_userId);
                bool actualizado = await UpdateUsuario(_userId, datosUsuario);
                if (actualizado) this.Close();
            }
            else
            {
                bool creado = await CrearUsuario(datosUsuario);
                if (creado) this.Close();
            }
        }
    }
}
