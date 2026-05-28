using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace NotasMax.ViewModels
{
    public class DesempenhoAluno
    {
        [JsonPropertyName("materias")]
        public List<Materia> Materias { get; set; } = new();
        [JsonPropertyName("simulados")]
        public List<Simulado> Simulados { get; set; } = new();


        public class Materia
        {
            [JsonPropertyName("nome")]
            public string Nome { get; set; }
            [JsonPropertyName("media")]
            public double Media { get; set; }
        }
        public class Simulado
        {
            [JsonPropertyName("numero")]
            public int Numero { get; set; }
            [JsonPropertyName("media")]
            public double Media { get; set; }
            [JsonPropertyName("data")]
            public DateTime Data { get; set; }
        }
    }
}
