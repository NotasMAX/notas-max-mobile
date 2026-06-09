using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NotasMax.ViewModels
{
    public class GestaoNotasViewModel
    {
        [JsonPropertyName("turmas")]
        public List<Turma> Turmas { get; set; } = new();

        public class Turma
        {
            [JsonPropertyName("id")]
            public string Id { get; set; } = string.Empty;
            [JsonPropertyName("serie")]
            public int Serie { get; set; }
            [JsonPropertyName("disciplinas")]
            public List<Disciplina> Disciplinas { get; set; } = new();

            [JsonIgnore]
            public string SerieFormatada => $"{Serie}º EM";
        }

        public class Disciplina
        {
            [JsonPropertyName("id")]
            public string Id { get; set; } = string.Empty;
            [JsonPropertyName("materia_nome")]
            public string MateriaNome { get; set; } = string.Empty;
            [JsonPropertyName("simulados")]
            public List<Simulado> Simulados { get; set; } = new();
        }

        public class Simulado
        {
            [JsonPropertyName("id")]
            public string Id { get; set; } = string.Empty;
            [JsonPropertyName("numero")]
            public int Numero { get; set; }
            [JsonIgnore]
            public string NumeroFormatado => $"Sim. {Numero}";
        }
    }
}
