using System.Text.Json.Serialization;

namespace NotasMax.Models
{
    public class Simulado
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("numero")]
        public int Numero { get; set; }

        [JsonPropertyName("tipo")]
        public string Tipo { get; set; }

        [JsonPropertyName("bimestre")]
        public int Bimestre { get; set; }

        [JsonPropertyName("data_realizacao")]
        public DateTime DataRealizacao { get; set; }

        [JsonPropertyName("turma_id")]
        public string? TurmaId { get; set; }
        public Turma Turma { get; set; } = new Turma();

        [JsonPropertyName("conteudos")]
        public List<ConteudoSimulado> Conteudos { get; set; } = new List<ConteudoSimulado>();

    }
}
