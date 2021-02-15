using AutoMapper;
using el.api.locathales.Application.Interfaces;
using el.api.locathales.Application.Models;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.Repositories;
using el.api.locathales.Domain.ValueObjects;
using System.Threading.Tasks;

namespace el.api.locathales.Application
{
    public class OperadorApplication : IOperadorApplication
    {
        private readonly IMapper _mapper;
        private readonly IOperadorRepository _operadorRepository;

        public OperadorApplication(IMapper mapper, IOperadorRepository operadorRepository)
        {
            _mapper = mapper;
            _operadorRepository = operadorRepository;
        }
        public async Task<Result<Operador>> Salvar(OperadorModel operadorModel)
        {
            var operador = _mapper.Map<OperadorModel, Operador>(operadorModel);

            if (operador.Valid)
            {
                var operadorExiste = await _operadorRepository.Obter(operador.Matricula.Numero);
                if (operadorExiste != null)
                {
                    return Result<Operador>.Error("Operador Já cadastrado", nameof(operador));
                }
                operador.Criar();
                await _operadorRepository.Incluir(operador);
                await _operadorRepository.UnitOfWork.PersistirNoBanco();
                return Result<Operador>.Ok(operador);
            }

            return Result<Operador>.Error(operador.Notifications);
        }

        public async Task<Result<Operador>> Obter(string matricula)
        {
            var matriculaObj = new Matricula(matricula);
            if(matriculaObj.Valid)
            {
                return Result<Operador>.Ok(await _operadorRepository.Obter(matricula));
            }
            return Result<Operador>.Error(matriculaObj.Notifications);
        }
    }
}
