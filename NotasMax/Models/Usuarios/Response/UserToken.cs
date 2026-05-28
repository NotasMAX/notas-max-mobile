using NotasMax.Models.Usuarios;
using System.Text.Json.Serialization;

namespace NotasMax.Models.Usuarios.Response
{
    // Este modelo representa a resposta da API apos realizar o login.
    public class UserToken
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("usuario")]
        public Usuario Usuario { get; set; }
    }

}