using System.Text.Json.Serialization;

namespace NotasMax.Models.Usuarios
{
    public class Usuario
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("tipo_usuario")]
        public string TipoUsuario { get; set; }
        public string TelefoneResponsavel { get; set; }
        public string NomeResponsavel { get; set; }

        //public string ResetToken { get; set; }
        //public DateTime ResetTokenExpiry { get; set; }

    }
}
