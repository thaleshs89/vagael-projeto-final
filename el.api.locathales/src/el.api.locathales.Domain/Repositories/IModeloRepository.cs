using el.api.locathales.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace el.api.locathales.Domain.Repositories
{
    public interface IModeloRepository
    {
        Task<IEnumerable<Modelo>> ListarPorMarca(string idFipeMarca);
        Task<ModeloDetalhes> ObterDetalhesModelo(string idMarca,string idModeloFipe);
    }
}