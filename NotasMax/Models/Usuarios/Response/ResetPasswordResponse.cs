using System.Text.Json.Serialization;

namespace NotasMax.Models.Usuarios.Response
{
    public class ResetPasswordResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
