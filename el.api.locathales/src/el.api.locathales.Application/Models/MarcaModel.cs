using System.Text.Json.Serialization;

namespace el.api.locathales.Application.Models
{
    public class MarcaModel
    {
        public string Nome { get; set; }
        public string FipeNome { get; set; }
        public int Order { get; set; }
        public string Key { get; set; }
        public int IdFipe { get; set; }
    }
}