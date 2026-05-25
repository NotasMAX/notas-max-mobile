using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace NotasMax.ViewModels
{
    public class CalendarioByAluno
    {
        [JsonPropertyName("simulados")]
        public List<CalendarioSimulado> Simulados { get; set; } = new();
        public class CalendarioSimulado
        {
            [JsonPropertyName("numero")]
            public int Numero { get; set; }

            [JsonPropertyName("tipo")]
            public string Tipo { get; set; } = string.Empty;

            [JsonPropertyName("data_realizacao")]
            public DateTime DataRealizacao { get; set; }
        }
    }
}