using AutoMapper;
using el.api.locathales.Application.Interfaces;
using el.api.locathales.Application.Models;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.Repositories;
using el.api.locathales.Domain.ValueObjects;
using System.Threading.Tasks;

namespace el.api.locathales.Application
{
    public class ClienteApplication : IClienteApplication
    {
        private readonly IMapper _mapper;
        private readonly IClienteRepository _clienteRepository;

        public ClienteApplication(IMapper mapper, IClienteRepository clienteRepository)
        {
            _mapper = mapper;
            _clienteRepository = clienteRepository;
        }
        public async Task<Result<Cliente>> Salvar(ClienteModel clienteModel)
        {
            var cliente = _mapper.Map<ClienteModel, Cliente>(clienteModel);

            if (cliente.Valid)
            {
                var clienteExiste = await _clienteRepository.Obter(cliente.Cpf.Numero);
                if(clienteExiste != null)
                {
                    return Result<Cliente>.Error("Cliente Já cadastrado", nameof(cliente));
                }
                cliente.Criar();
                await _clienteRepository.Incluir(cliente);
                await _clienteRepository.UnitOfWork.PersistirNoBanco();
                return Result<Cliente>.Ok(cliente);
            }

            return Result<Cliente>.Error(cliente.Notifications);
        }

        public async Task<Result<Cliente>> Obter(string cpf)
        {
            var cpfObj = new CPF(cpf);
            if(cpfObj.Valid)
            {
                return Result<Cliente>.Ok(await _clienteRepository.Obter(cpf));
            }
            return Result<Cliente>.Error(cpfObj.Notifications);
        }
    }
}
