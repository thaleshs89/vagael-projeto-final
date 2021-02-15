using el.api.locathales.Domain.Commun;
using el.api.locathales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace el.api.locathales.Domain.Repositories
{
    public interface IOperadorRepository : IAggregateRoot
    {
        Task Incluir(Operador operador);
        Task<Operador> Obter(string matricula);
    }
}
