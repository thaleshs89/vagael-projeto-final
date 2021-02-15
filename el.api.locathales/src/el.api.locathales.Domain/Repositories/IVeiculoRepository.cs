using el.api.locathales.Domain.Commun;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace el.api.locathales.Domain.Repositories
{
    public interface IVeiculoRepository : IAggregateRoot
    {
        Task Incluir(Veiculo veiculo);
        Task<Veiculo> Obter(string placa);
        Task<IEnumerable<Veiculo>> ListarPorCategoria(string categoria);
        Task<Veiculo> ObterPorStatus(Enumeradores.StatusVeiculo status);
        void Alterar(Veiculo veiculo);
    }
}