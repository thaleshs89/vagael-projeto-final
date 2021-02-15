using System;
using System.Collections.Generic;
using System.Linq;

namespace el.api.locathales.Domain.Entities
{
    public class ModeloDetalhes
    {
        public string IdFipe { get; set; }
        public IEnumerable<KeyValuePair<int, decimal>> AnosModeloPreco { get; set; }
        public string Combustivel { get; set; }
        public string Categoria { get; set; }

        public void CalcularCategoria()
        {
            var mediaPreco = AnosModeloPreco.Average(pre => pre.Value);
            if(mediaPreco < 30)
            {
                Categoria = Enums.Constantes.TiposCategoria.Basico;
            }
            else if(mediaPreco < 70)
            {
                Categoria = Enums.Constantes.TiposCategoria.Intermediario;
            }
            else
            {
                Categoria = Enums.Constantes.TiposCategoria.Luxo;
            }

        }
    }
}