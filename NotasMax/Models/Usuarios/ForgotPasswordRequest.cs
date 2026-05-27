using System.Text.Json.Serialization;

namespace NotasMax.Models.Usuarios
{
    public class ForgotPasswordRequest
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
