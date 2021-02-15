using AutoMapper;
using el.api.locathales.Application.Interfaces;
using el.api.locathales.Application.Models;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.Repositories;
using el.api.locathales.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace el.api.locathales.Application
{
    public class VeiculoApplication : IVeiculoApplication
    {
        private readonly IMapper _mapper;
        private readonly IVeiculoRepository _veiculoRepository;

        public VeiculoApplication(IMapper mapper, IVeiculoRepository veiculoRepository)
        {
            _mapper = mapper;
            _veiculoRepository = veiculoRepository;
        }

        async Task<Result<Veiculo>> IVeiculoApplication.Salvar(VeiculoModel veiculoModel)
        {
            var veiculo = _mapper.Map<VeiculoModel, Veiculo>(veiculoModel);

            if (veiculo.Valid)
            {
                var veiculoCriado = await _veiculoRepository.Obter(veiculo.Placa);
                if (veiculoCriado != null)
                {
                    return Result<Veiculo>.Error("Placa já cadastrada", nameof(veiculo.Placa));
                }
                veiculo.CriarVeiculo();
                await _veiculoRepository.Incluir(veiculo);
                await _veiculoRepository.UnitOfWork.PersistirNoBanco();
                return Result<Veiculo>.Ok(veiculo);
            }

            return Result<Veiculo>.Error(veiculo.Notifications);
        }

        async Task<Result<Veiculo>> IVeiculoApplication.Obter(string placa)
        {
            return Result<Veiculo>.Ok(await _veiculoRepository.Obter(placa));
        }

        public async Task<Result<IEnumerable<Veiculo>>> ListarPorCategoria(string categoria)
        {
            var listaVeiculo = await _veiculoRepository.ListarPorCategoria(categoria);
            if (listaVeiculo != null && listaVeiculo.Any())
            {
                return Result<IEnumerable<Veiculo>>.Ok(listaVeiculo);
            }
            else
            {
                return Result<IEnumerable<Veiculo>>.Ok(null);
            }
        }
    }
}
