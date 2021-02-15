using el.api.locathales.Domain.Core.Entities;
using System.Text.Json.Serialization;

namespace el.api.locathales.Domain.Entities
{
    public class Modelo
    {
        [JsonPropertyName("fipe_marca")]
        public string FipeMarca { get; set; }

        [JsonPropertyName("name")]
        public string Nome { get; set; }

        [JsonPropertyName("marca")]
        public string Marca { get; set; }

        [JsonPropertyName("key")]
        public string Codigo { get; set; }

        [JsonPropertyName("id")]
        public string IdFipe { get; set; }

        [JsonPropertyName("fipe_name")]
        public string FipeNome { get; set; }

        [JsonPropertyName("fipe_codigo")]
        public string FipeCodigo { get; set; }

        [JsonPropertyName("veiculo")]
        public string DecricaoModelo { get; set; }

    }
}