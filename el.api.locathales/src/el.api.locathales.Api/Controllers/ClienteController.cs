using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using el.api.locathales.Application.Interfaces;
using el.api.locathales.Application.Models;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace el.api.locathales.Api.Controllers
{
    [ApiController]
    [Route("clientes")]
    public class ClienteController : ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IClienteApplication _clienteApplication;

        public ClienteController(IMapper mapper, IClienteApplication clienteApplication)
        {
            _mapper = mapper;
            _clienteApplication = clienteApplication;
        }

        [HttpGet]
        [Route("{cpf}")]
        [ProducesResponseType(typeof(ClienteModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string cpf)
        {
            var cliente = await _clienteApplication.Obter(cpf);

            if (cliente?.Object != null)
            {
                return Ok(_mapper.Map<Cliente, ClienteModel>(cliente.Object));
            }
            if(cliente != null && cliente.Invalid)
            {
                return BadRequest(cliente.Notifications);
            }
            return NotFound("Cliente não encontrado");
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClienteModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(ClienteModel clienteModel)
        {
            var result = await _clienteApplication.Salvar(clienteModel);

            if (result.Success)
                return Created($"/clientes/{result.Object.Cpf.ToString()}", _mapper.Map<Cliente, ClienteModel>(result.Object));

            return BadRequest(result.Notifications);
        }
    }
}
