using System;
using System.Text;
using Newtonsoft.Json;

namespace app_wpf.Model
{
    public class Usuario
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("apellido")]
        public string Apellido { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("dni")]
        public string Dni { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("birthday")]
        public DateTime? Birthday { get; set; }

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
    }

    public class UsuarioListView
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Dni { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Birthday { get; set; }
        public string Sex { get; set; }
        public string Ciudad { get; set; }
        public string Vip { get; set; }
        public string Role { get; set; }
        public string Avatar { get; set; }

        public UsuarioListView(Usuario usuario)
        {
            Id = usuario.Id;
            Nombre = usuario.Nombre;
            Apellido = usuario.Apellido;
            Email = usuario.Email;
            Dni = usuario.Dni;
            Username = usuario.Username;
            Password = usuario.Password;
            Birthday = usuario.Birthday?.ToString("yyyy-MM-dd") ?? "N/A";
            Sex = usuario.Sex;
            Ciudad = usuario.Ciudad;
            Vip = usuario.Vip ? "Sí" : "No";
            Role = usuario.Role;
            Avatar = usuario.Avatar;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Nombre: {Nombre} {Apellido}, Email: {Email}, DNI: {Dni}, Username: {Username}, " +
                   $"Fecha de Nacimiento: {Birthday}, Sexo: {Sex}, Ciudad: {Ciudad}, VIP: {Vip}, Rol: {Role}, Avatar: {Avatar}";
        }
    }
}
