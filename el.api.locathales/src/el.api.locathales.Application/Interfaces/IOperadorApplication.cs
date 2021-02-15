using el.api.locathales.Application.Models;
using el.api.locathales.Domain.Entities;
using System.Threading.Tasks;

namespace el.api.locathales.Application.Interfaces
{
    public interface IOperadorApplication
    {
        Task<Result<Operador>> Salvar(OperadorModel operadorModel);
        Task<Result<Operador>> Obter(string matricula);
    }
}