using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace el.api.locathales.Domain.Entities
{
    public class VeiculoAnoFipe
    {
        protected VeiculoAnoFipe() { }

        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("ano_modelo")]
        public string AnoModelo { get; set; }
        [JsonPropertyName("preco")]
        public string Preco { get; set; }
        [JsonPropertyName("combustivel")]
        public string Combustivel { get; set; }
        [JsonPropertyName("fipe_codigo")]
        public string FipeCodigo { get; set; }
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}
