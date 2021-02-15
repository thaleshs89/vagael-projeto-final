using AutoMapper;
using el.api.locathales.Application.Interfaces;
using el.api.locathales.Application.Models;
using el.api.locathales.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace el.api.locathales.Api.Controllers
{
    [ApiController]
    [Route("veiculos")]
    public class VeiculoController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IVeiculoApplication _veiculoApplication;
        public VeiculoController(IMapper mapper, IVeiculoApplication veiculoApplication)
        {
            _mapper = mapper;
            _veiculoApplication = veiculoApplication;
        }
        [HttpGet]
        [Route("{placa}")]
        [ProducesResponseType(typeof(VeiculoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string placa)
        {
            var veiculo = await _veiculoApplication.Obter(placa);

            if (veiculo?.Object == null)
                return NotFound("Veiculo não encontrado");

            return Ok(_mapper.Map<Veiculo, VeiculoModel>(veiculo.Object));
        }

        [HttpGet]
        [Route("categoria/{categoria}")]
        [ProducesResponseType(typeof(IEnumerable<VeiculoModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarPorCategoria(string categoria)
        {
            var listaVeiculo = await _veiculoApplication.ListarPorCategoria(categoria);

            if (listaVeiculo?.Object != null)
            {
                return Ok(_mapper.Map<IEnumerable<Veiculo>, IEnumerable<VeiculoModel>>(listaVeiculo.Object));
            }

            return NotFound("Veiculo não encontrado");
        }

        [HttpPost]
        [ProducesResponseType(typeof(VeiculoModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(VeiculoModel veiculoModel)
        {
            var result = await _veiculoApplication.Salvar(veiculoModel);

            if (result.Success)
                return Created($"/veiculos/{result.Object.Placa}", _mapper.Map<Veiculo, VeiculoModel>(result.Object));

            return BadRequest(result.Notifications);
        }
    }
}
