using AutoMapper;
using el.api.locathales.Application.Interfaces;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace el.api.locathales.Application
{
    public class ModeloApplication : IModeloApplication
    {
        private readonly IMapper _mapper;
        private readonly IModeloRepository _modeloRepository;

        public ModeloApplication(IMapper mapper, IModeloRepository modeloRepository)
        {
            _mapper = mapper;
            _modeloRepository = modeloRepository;
        }

        public async Task<Result<ModeloDetalhes>> ListarDetalhes(string idMarca, string idModeloFipe)
        {
            var detalhesModelo = await _modeloRepository.ObterDetalhesModelo(idMarca, idModeloFipe);
            if(detalhesModelo != null)
            {
                detalhesModelo.CalcularCategoria();
            }

            return Result<ModeloDetalhes>.Ok(detalhesModelo);


        }

        public async Task<Result<IEnumerable<Modelo>>> ListarPorMarca(string idFipeMarca)
        {
            var listaModelos = await _modeloRepository.ListarPorMarca(idFipeMarca);
            if (listaModelos != null && listaModelos.Any())
            {
                return Result<IEnumerable<Modelo>>.Ok(listaModelos);
            }
            else
            {
                return Result<IEnumerable<Modelo>>.Ok(null);
            }
        }
    }
}
