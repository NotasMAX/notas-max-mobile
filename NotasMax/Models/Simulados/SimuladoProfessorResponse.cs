using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace NotasMax.Models.Simulados
{
    public class SimuladoProfessorResponse
    {
        [JsonPropertyName("professor_id")]
        public string ProfessorId { get; set; } = string.Empty;

        [JsonPropertyName("total_turmas")]
        public int TotalTurmas { get; set; }

        [JsonPropertyName("total_simulados_aplicados")]
        public int TotalSimuladosAplicados { get; set; }

        [JsonPropertyName("media_geral_todas_turmas")]
        public double? MediaGeralTodasTurmas { get; set; }

        [JsonPropertyName("turmas")]
        public List<TurmaResumo> Turmas { get; set; } = [];
    }
    public class TurmaResumo
    {
        [JsonPropertyName("turma_id")]
        public string TurmaId { get; set; } = string.Empty;

        [JsonPropertyName("serie")]
        public int Serie { get; set; }

        [JsonPropertyName("ano")]
        public int Ano { get; set; }

        [JsonPropertyName("total_alunos")]
        public int TotalAlunos { get; set; }

        [JsonPropertyName("media_geral_turma")]
        public double MediaGeralTurma { get; set; }

        [JsonPropertyName("total_simulados_na_turma")]
        public int TotalSimuladosNaTurma { get; set; }

        // Propriedades calculadas para exibição no XAML
        public string SerieAno => $"{Serie}º EM";
        public string TotalAlunosFormatado => $"{TotalAlunos} alunos";
    }
}
