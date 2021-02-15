using el.api.locathales.Domain.Commun;
using el.api.locathales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace el.api.locathales.Domain.Repositories
{
    public interface IClienteRepository : IAggregateRoot
    {
        Task Incluir(Cliente cliente);
        Task<Cliente> Obter(string cpf);
    }
}
