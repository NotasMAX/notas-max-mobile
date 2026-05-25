using System.Text.Json.Serialization;

namespace NotasMax.Models
{
    public class ConteudoSimulado
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        [JsonPropertyName("turma_disciplina_id")]
        public string TurmaDisciplinaId { get; set; }

        public TurmaDisciplina TurmaDisciplina { get; set; } = new TurmaDisciplina();


        [JsonPropertyName("quantidade_questoes")]
        public int QuantidadeQuestoes { get; set; }

        [JsonPropertyName("peso")]
        public int Peso { get; set; }

        [JsonPropertyName("resultados")]
        public ResultadoSimulado Resultados { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
