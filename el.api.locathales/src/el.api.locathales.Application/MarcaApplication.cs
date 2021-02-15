using AutoMapper;
using el.api.locathales.Application.Interfaces;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace el.api.locathales.Application
{
    public class MarcaApplication : IMarcaApplication
    {
        private readonly IMapper _mapper;
        private readonly IMarcaRepository _marcaRepository;

        public MarcaApplication(IMapper mapper, IMarcaRepository marcaRepository)
        {
            _mapper = mapper;
            _marcaRepository = marcaRepository;
        }

        public async Task<Result<IEnumerable<Marca>>> ListarTodos()
        {
            var listaMarcas = await _marcaRepository.ListarTodos();
            if(listaMarcas != null && listaMarcas.Any())
            {
                return Result<IEnumerable<Marca>>.Ok(listaMarcas);
            }
            else
            {
                return Result<IEnumerable<Marca>>.Ok(null);
            }
        }

        public async Task<Result<Marca>> Obter(int idFipe)
        {
            var marca = await _marcaRepository.Obter(idFipe);
            return Result<Marca>.Ok(marca);
        }
    }
}
