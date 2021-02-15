using el.api.locathales.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace el.api.locathales.Domain.Repositories
{
    public interface IMarcaRepository
    {
        Task<IEnumerable<Marca>> ListarTodos();
        Task<Marca> Obter(int idFipe);
    }
}