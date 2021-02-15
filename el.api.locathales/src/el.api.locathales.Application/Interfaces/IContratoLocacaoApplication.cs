using el.api.locathales.Application.Models;
using el.api.locathales.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace el.api.locathales.Application.Interfaces
{
    public interface IContratoLocacaoApplication
    {
        Task<Result<ContratoLocacao>> Obter(Int64 numeroContrato);
        Task<Result<ContratoLocacao>> Salvar(ContratoLocacaoModel contratoLocacaoModel);
        Task<Result>AtualizarStatus(long numeroContrato, int status);
    }
}