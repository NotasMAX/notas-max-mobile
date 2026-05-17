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

            [JsonIgnore]
            public string SerieFormatada => $"{Serie}º EM";
        }
    }

}