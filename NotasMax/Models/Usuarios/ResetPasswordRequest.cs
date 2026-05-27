using System.Text.Json.Serialization;

namespace NotasMax.Models.Usuarios
{
    public class ResetPasswordRequest
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("novaSenha")]
        public string NovaSenha { get; set; }
    }
}
