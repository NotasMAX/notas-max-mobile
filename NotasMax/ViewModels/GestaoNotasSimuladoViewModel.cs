using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace NotasMax.ViewModels
{
    public class GestaoNotasSimuladoViewModel
    {
        [JsonPropertyName("simulado_id")]
        public string SimuladoId { get; set; } = string.Empty;
        [JsonPropertyName("turma_disciplina_id")]
        public string DisciplinaId { get; set; } = string.Empty;
        [JsonPropertyName("alunos")]
        public List<Aluno> Alunos { get; set; } = new();

        public class Aluno
        {
            [JsonPropertyName("id")]
            public string Id { get; set; } = string.Empty;
            [JsonPropertyName("nome")]
            public string Nome { get; set; } = string.Empty;
            [JsonPropertyName("acertos")]
            public int Acertos { get; set; }

            [JsonPropertyName("nota")]
            public double Nota { get; set; }

            [JsonPropertyName("quantidade_questoes")]
            public int QuantidadeQuestoes { get; set; }

        }
    }
}