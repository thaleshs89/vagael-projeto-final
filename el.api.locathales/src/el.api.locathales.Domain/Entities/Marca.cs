using el.api.locathales.Domain.Core.Entities;
using System.Text.Json.Serialization;

namespace el.api.locathales.Domain.Entities
{
    public class Marca
    {
        [JsonPropertyName("name")]
        public string Nome { get; set; }

        [JsonPropertyName("fipe_name")]
        public string FipeNome { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("id")]
        public int IdFipe { get; set; }
    }
}