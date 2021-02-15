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
    [Route("marcas")]
    public class MarcaController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IMarcaApplication _marcaApplication;
        public MarcaController(IMapper mapper, IMarcaApplication marcaApplication)
        {
            _mapper = mapper;
            _marcaApplication = marcaApplication;
        }
        [HttpGet]
        [Route("{idFipe}")]
        [ProducesResponseType(typeof(MarcaModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int idFipe)
        {
            var marca = await _marcaApplication.Obter(idFipe);

            if (marca == null || marca.Object == null)
                return NotFound("Marca não encontrado");

            return Ok(_mapper.Map<Marca, MarcaModel>(marca.Object));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerator<MarcaModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarTodos()
        {
            var listaMarcas = await _marcaApplication.ListarTodos();

            if (listaMarcas == null || listaMarcas.Object == null)
                return NotFound("Marcas não encontradas");

            return Ok(_mapper.Map<IEnumerable<Marca>, IEnumerable<MarcaModel>>(listaMarcas.Object.ToList()));
        }
    }
}
