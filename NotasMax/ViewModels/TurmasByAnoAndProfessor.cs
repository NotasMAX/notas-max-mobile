using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NotasMax.ViewModels
{
    public class TurmasByAnoAndProfessor
    {

        [JsonPropertyName("turmas")]
        public List<Turma> Turmas { get; set; } = new();
        [JsonPropertyName("total_alunos")]
        public int TotalAlunos { get; set; }
        [JsonPropertyName("total_turmas")]
        public int TotalTurmas { get; set; }

        public class Disciplina
        {
            [JsonPropertyName("disciplinaId")]
            public string Id { get; set; }
            [JsonPropertyName("materiaId")]
            public string MateriaId { get; set; }
            [JsonPropertyName("materiaNome")]
            public string MateriaNome { get; set; } = string.Empty;
        }

        public class Turma
        {
            [JsonPropertyName("turmaId")]
            public string Id { get; set; }
            [JsonPropertyName("serie")]
            public int Serie { get; set; }
            [JsonPropertyName("disciplinas")]
            public List<Disciplina> Disciplinas { get; set; } = new();
            [JsonPropertyName("quantidade_alunos")]
            public int QuantidadeAlunos { get; set; }
            [JsonPropertyName("media")]
            public double Media { get; set; }

            [JsonIgnore]
            public string SerieFormatada => $"{Serie}º EM";
        }
    }

}