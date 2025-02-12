using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace app_wpf.Model
{
    public class Usuario
    {
        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("dni")]
        public string Dni { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("sex")]
        public string Sex { get; set; }

        [JsonProperty("ciudad")]
        public string Ciudad { get; set; }

        [JsonProperty("vip")]
        public bool Vip { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        public Usuario(
            string nombre,
            string email,
            string dni,
            string username,
            string password,
            string birthday,
            string sex,
            string ciudad,
            bool vip,
            string role = "Cliente",
            string avatar = "")
        {
            Nombre = nombre;
            Email = email;
            Dni = dni;
            Username = username;
            Password = password;
            Birthday = birthday;
            Sex = sex;
            Ciudad = ciudad;
            Vip = vip;
            Role = role;
            Avatar = avatar;
        }
    }
    public class UsuarioListView
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Vip { get; set; }
        public string Ciudad { get; set; }
        public string Avatar { get; set; }

        public UsuarioListView(Usuario usuario)
        {
            Nombre = usuario.Nombre;
            Email = usuario.Email;
            Username = usuario.Username;
            Role = usuario.Role;
            Vip = usuario.Vip ? "Sí" : "No";
            Ciudad = usuario.Ciudad;
            Avatar = usuario.Avatar;
        }
    }
}
