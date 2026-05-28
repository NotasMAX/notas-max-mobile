using System.Text.Json.Serialization;

namespace NotasMax.Models.Simulados
{

    public class SimuladoAlunoResponse
    {
        [JsonPropertyName("aluno_id")]
        public string AlunoId { get; set; }

        [JsonPropertyName("media_geral")]
        public double MediaGeral { get; set; }

        [JsonPropertyName("media_por_materia")]
        public List<MediaMateriaSimulado> MediaPorMateria { get; set; } = new();

        [JsonPropertyName("simulados")]
        public List<SimuladoAluno> Simulados { get; set; } = new();
    }

    public class MediaMateriaSimulado
    {
        [JsonPropertyName("materia_id")]
        public string MateriaId { get; set; }

        [JsonPropertyName("materia")]
        public string Materia { get; set; }

        [JsonPropertyName("professor")]
        public string Professor { get; set; }

        [JsonPropertyName("media")]
        public double Media { get; set; }

        [JsonPropertyName("simulados_realizados")]
        public int SimuladosRealizados { get; set; }
    }

    public class SimuladoAluno
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

        [JsonPropertyName("media_simulado")]
        public double MediaSimulado { get; set; }

        [JsonPropertyName("materias")]
        public List<MateriaSimuladoAluno> Materias { get; set; } = new();
    }

    public class MateriaSimuladoAluno
    {
        [JsonPropertyName("materia_id")]
        public string MateriaId { get; set; }

        [JsonPropertyName("materia")]
        public string Materia { get; set; }

        [JsonPropertyName("professor")]
        public string Professor { get; set; }

        [JsonPropertyName("quantidade_questoes")]
        public int QuantidadeQuestoes { get; set; }

        [JsonPropertyName("acertos")]
        public int Acertos { get; set; }

        [JsonPropertyName("nota")]
        public double Nota { get; set; }
    }

}
