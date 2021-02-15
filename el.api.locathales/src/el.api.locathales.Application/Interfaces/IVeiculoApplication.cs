using el.api.locathales.Application.Models;
using el.api.locathales.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace el.api.locathales.Application.Interfaces
{
    public interface IVeiculoApplication
    {
        Task<Result<Veiculo>> Salvar(VeiculoModel veiculoModel);
        Task<Result<Veiculo>> Obter(string placa);
        Task<Result<IEnumerable<Veiculo>>> ListarPorCategoria(string categoria);
    }
}