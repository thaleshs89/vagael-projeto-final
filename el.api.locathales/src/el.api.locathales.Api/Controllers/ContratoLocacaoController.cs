using AutoMapper;
using el.api.locathales.Application.Interfaces;
using el.api.locathales.Application.Models;
using el.api.locathales.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace el.api.locathales.Api.Controllers
{
    [ApiController]
    [Route("contratosLocacao")]
    public class ContratoLocacaoController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IContratoLocacaoApplication _contratoLocacaoApplication;
        public ContratoLocacaoController(IMapper mapper, IContratoLocacaoApplication contratoLocacaoApplication)
        {
            _mapper = mapper;
            _contratoLocacaoApplication = contratoLocacaoApplication;
        }
        [HttpGet]
        [Route("{numeroContrato}")]
        [ProducesResponseType(typeof(ContratoLocacaoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(Int64 numeroContrato)
        {
            var contratoLocacao = await _contratoLocacaoApplication.Obter(numeroContrato);

            if (contratoLocacao?.Object == null)
                return NotFound("ContratoLocacao não encontrado");

            return Ok(_mapper.Map<ContratoLocacao, ContratoLocacaoModel>(contratoLocacao.Object));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ContratoLocacaoModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(ContratoLocacaoModel contratoLocacaoModel)
        {
            var result = await _contratoLocacaoApplication.Salvar(contratoLocacaoModel);

            if (result.Success)
                return Created($"/contratoLocacaos/{result.Object.NumeroContrato}", _mapper.Map<ContratoLocacao, ContratoLocacaoModel>(result.Object));

            return BadRequest(result.Notifications);
        }

        [HttpPatch]
        [Route("{numeroContrato}/status/{status}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarStatus(Int64 numeroContrato, int status)
        {
            var result = await _contratoLocacaoApplication.AtualizarStatus(numeroContrato, status);
            if(result.Invalid)
            {
                return BadRequest(result.Notifications);
            }
            return Ok();
        }

    }
}
