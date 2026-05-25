using System.Text.Json.Serialization;

namespace NotasMax.Models.Simulados
{

    public class SimuladoListResponse
    {
        [JsonPropertyName("simulados")]
        public List<Simulado> Simulados { get; set; } = new List<Simulado>();
    }
}
