using el.api.locathales.Application.Models;
using el.api.locathales.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace el.api.locathales.Application.Interfaces
{
    public interface IMarcaApplication
    {
        Task<Result<Marca>> Obter(int idFipe);
        Task<Result<IEnumerable<Marca>>> ListarTodos();
    }
}