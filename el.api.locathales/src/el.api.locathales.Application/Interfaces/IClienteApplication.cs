using el.api.locathales.Application.Models;
using el.api.locathales.Domain.Entities;
using System.Threading.Tasks;

namespace el.api.locathales.Application.Interfaces
{
    public interface IClienteApplication
    {
        Task<Result<Cliente>> Salvar(ClienteModel clienteModel);
        Task<Result<Cliente>> Obter(string cpf);
    }
}