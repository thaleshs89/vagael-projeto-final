using el.api.locathales.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace el.api.locathales.Application.Interfaces
{
    public interface IModeloApplication
    {
        Task<Result<IEnumerable<Modelo>>> ListarPorMarca(string idFipeMarca);
        Task<Result<ModeloDetalhes>> ListarDetalhes(string idMarca, string idModeloFipe);
    }
}