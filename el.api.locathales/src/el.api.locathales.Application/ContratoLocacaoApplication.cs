using AutoMapper;
using el.api.locathales.Application.Interfaces;
using el.api.locathales.Application.Models;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.Repositories;
using el.api.locathales.Domain.ValueObjects;
using el.api.locathales.Domain.Enums;
using System.Threading.Tasks;
using System;
using el.api.locathales.Domain.Commun;

namespace el.api.locathales.Application
{
    public class ContratoLocacaoApplication : IContratoLocacaoApplication
    {
        private readonly IMapper _mapper;
        private readonly IContratoLocacaoRepository _contratoLocacaoRepository;
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IClienteRepository _clienteRepository;


        public ContratoLocacaoApplication(IMapper mapper, IContratoLocacaoRepository contratoLocacaoRepository, IClienteRepository clienteRepository, IVeiculoRepository veiculoRepository)
        {
            _mapper = mapper;
            _contratoLocacaoRepository = contratoLocacaoRepository;
            _clienteRepository = clienteRepository;
            _veiculoRepository = veiculoRepository;
        }
        public async Task<Result<ContratoLocacao>> Salvar(ContratoLocacaoModel contratoLocacaoModel)
        {
            var contratoLocacao = _mapper.Map<ContratoLocacaoModel, ContratoLocacao>(contratoLocacaoModel);

            if (contratoLocacao.Valid)
            {
                contratoLocacao.ValidarDatas();
                if (contratoLocacao.Invalid)
                {
                    return Result<ContratoLocacao>.Error(contratoLocacao.Notifications);
                }
                var cliente = await _clienteRepository.Obter(contratoLocacao.CpfCliente);
                if (cliente == null)
                {
                    return Result<ContratoLocacao>.Error("Cliente não encontrado.", nameof(contratoLocacao.CpfCliente));
                }
                var veiculo = await _veiculoRepository.Obter(contratoLocacao.Placa);
                if (veiculo == null || veiculo.Status != (int)Enumeradores.StatusVeiculo.Disponivel)
                {
                    return Result<ContratoLocacao>.Error($"Veiculo de Placa {contratoLocacao.Placa} indisponível para Locação.", nameof(contratoLocacao.CpfCliente));
                }
                contratoLocacao.CalcularValorContrato(veiculo.ValorDia);
                contratoLocacao.CriarContrato();
                veiculo.AlterarStatusVeiculo(Enumeradores.StatusVeiculo.Reservado);
                _veiculoRepository.Alterar(veiculo);
                await _contratoLocacaoRepository.Incluir(contratoLocacao);
                await _contratoLocacaoRepository.UnitOfWork.PersistirNoBanco();
                return Result<ContratoLocacao>.Ok(contratoLocacao);
            }

            return Result<ContratoLocacao>.Error(contratoLocacao.Notifications);
        }

        public async Task<Result<ContratoLocacao>> Obter(Int64 numeroContrato)
        {
            return Result<ContratoLocacao>.Ok(await _contratoLocacaoRepository.Obter(numeroContrato));
        }

        public async Task<Result> AtualizarStatus(long numeroContrato, int status)
        {
            if (!Comum.ValidateEnumValue(status, typeof(Enumeradores.StatusContrato)))
            {
                return Result.Error("Status inválido", nameof(status));
            }
            var contrato = await _contratoLocacaoRepository.Obter(numeroContrato);
            if (contrato == null)
            {
                return Result.Error("contrato não encontrado", nameof(numeroContrato));
            }
            int novoStatusVeiculo = 0;
            if (contrato.ValidarAlteracaoStatus(status, ref novoStatusVeiculo))
            {
                if (novoStatusVeiculo != 0)
                {
                    var veiculo = await _veiculoRepository.Obter(contrato.Placa);
                    veiculo.AlterarStatusVeiculo(Enumeradores.StatusVeiculo.Reservado);
                    _veiculoRepository.Alterar(veiculo);
                }
                contrato.AlterarStatusContrato(status);
                _contratoLocacaoRepository.Alterar(contrato);
                await _contratoLocacaoRepository.UnitOfWork.PersistirNoBanco();
                return Result.Ok();
            }
            return Result.Error("Não é permitido alterar status", nameof(status));

        }
    }
}
