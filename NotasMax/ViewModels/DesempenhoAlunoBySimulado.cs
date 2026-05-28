using System.Text.Json.Serialization;

namespace NotasMax.ViewModels
{
    public class DesempenhoAlunoBySimulado
    {
        [JsonPropertyName("numero")]
        public int Numero { get; set; }

        [JsonPropertyName("data_realizacao")]
        public DateTime DataRealizacao { get; set; }
        [JsonPropertyName("media")]
        public double Media { get; set; }

        [JsonPropertyName("disciplinas")]
        public List<Disciplina> Disciplinas { get; set; } = new();

        public class Disciplina
        {
            [JsonPropertyName("materia_nome")]
            public string Nome { get; set; }
            [JsonPropertyName("nota")]
            public double Nota { get; set; }
        }
    }
}
