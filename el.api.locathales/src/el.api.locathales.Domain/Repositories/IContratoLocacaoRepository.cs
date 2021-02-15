using el.api.locathales.Domain.Commun;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace el.api.locathales.Domain.Repositories
{
    public interface IContratoLocacaoRepository : IAggregateRoot
    {
        Task<ContratoLocacao> Obter(Int64 numeroContrato);
        Task Incluir(ContratoLocacao contratoLocacao);
        void Alterar(ContratoLocacao contratoLocacao);
    }
}