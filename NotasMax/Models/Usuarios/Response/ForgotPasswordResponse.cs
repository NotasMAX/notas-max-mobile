using System.Text.Json.Serialization;

namespace NotasMax.Models.Usuarios.Response
{
    public class ForgotPasswordResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
