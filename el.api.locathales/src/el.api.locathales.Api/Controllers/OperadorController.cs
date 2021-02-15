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
    [Route("operadores")]
    public class OperadorController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IOperadorApplication _operadorApplication;
        public OperadorController(IMapper mapper, IOperadorApplication operadorApplication)
        {
            _mapper = mapper;
            _operadorApplication = operadorApplication;
        }
        [HttpGet]
        [Route("{matricula}")]
        [ProducesResponseType(typeof(OperadorModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string matricula)
        {
            var operador = await _operadorApplication.Obter(matricula);

            if (operador?.Object != null)
            {
                return Ok(_mapper.Map<Operador, Operador>(operador.Object));
            }
            return NotFound("Operador não encontrado");

        }

        [HttpPost]
        [ProducesResponseType(typeof(OperadorModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(OperadorModel operardorModel)
        {
            var result = await _operadorApplication.Salvar(operardorModel);

            if (result.Success)
                return Created($"/operadores/{result.Object.Matricula.Numero}", _mapper.Map<Operador, OperadorModel>(result.Object));

            return BadRequest(result.Notifications);
        }
    }
}
