using AutoMapper;
using el.api.locathales.Application.Interfaces;
using el.api.locathales.Application.Models;
using el.api.locathales.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace el.api.locathales.Api.Controllers
{
    [ApiController]
    [Route("modelos")]
    public class ModeloController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IModeloApplication _modeloApplication;
        public ModeloController(IMapper mapper, IModeloApplication modeloApplication)
        {
            _mapper = mapper;
            _modeloApplication = modeloApplication;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerator<ModeloModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarPorMarca(string idFipeMarca)
        {
            var listaModelos = await _modeloApplication.ListarPorMarca(idFipeMarca);

            if (listaModelos == null || listaModelos.Object == null)
                return NotFound("Modelos não encontradas");

            return Ok(_mapper.Map<IEnumerable<Modelo>, IEnumerable<ModeloModel>>(listaModelos.Object.ToList()));
        }

        [HttpGet]
        [Route("detalhes")]
        [ProducesResponseType(typeof(ModeloDetalhesModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterDetalhesModelo(string idMarca, string idModeloFipe)
        {
            var retornoModelo = await _modeloApplication.ListarDetalhes(idMarca, idModeloFipe);

            if (retornoModelo?.Object != null)
            {
                return Ok(_mapper.Map<ModeloDetalhes, ModeloDetalhesModel>(retornoModelo.Object));
            }
            return NotFound("Dados do Modelo Não encontrado");
        }
    }
}
