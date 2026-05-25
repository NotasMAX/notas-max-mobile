using NotasMax.Models.Usuarios;
using System.Text.Json.Serialization;

namespace NotasMax.Models
{
    public class ResultadoSimulado
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        [JsonPropertyName("aluno_id")]
        public string AlunoId { get; set; }

        public Usuario Aluno { get; set; } = new Usuario();

        [JsonPropertyName("nota")]
        public double Nota { get; set; }

        [JsonPropertyName("acertos")]
        public int Acertos { get; set; }

        [JsonPropertyName("notificacao_enviada")]
        public string NotificacaoEnviada { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
