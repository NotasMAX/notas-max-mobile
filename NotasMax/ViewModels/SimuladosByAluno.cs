using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace NotasMax.ViewModels
{
    public class SimuladosByAluno
    {
        [JsonPropertyName("simulados")]
        public List<Simulado> Simulados { get; set; } = new();
        public class Simulado
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }
            [JsonPropertyName("numero")]
            public int Numero { get; set; }
            [JsonPropertyName("data_realizacao")]
            public DateTime DataRealizacao { get; set; }
            [JsonPropertyName("media")]
            public double Media { get; set; }
        }
    }
}

