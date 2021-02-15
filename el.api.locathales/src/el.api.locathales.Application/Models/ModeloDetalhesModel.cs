using System.Collections.Generic;

namespace el.api.locathales.Application.Models
{
    public class ModeloDetalhesModel
    {
        public string IdFipe { get; set; }
        public IEnumerable<KeyValuePair<int, decimal>> AnosModeloPreco { get; set; }
        public string Combustivel { get; set; }
        public string Categoria { get; set; }

    }
}