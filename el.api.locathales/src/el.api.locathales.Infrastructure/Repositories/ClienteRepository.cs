using AutoMapper;
using el.api.locathales.Domain.Commun;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.Repositories;
using el.api.locathales.Infrastructure.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace el.api.locathales.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IMapper _mapper;
        private readonly LocaThalesContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ClienteRepository(IMapper mapper, LocaThalesContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Incluir(Cliente cliente)
        {
            await _context.Clientes
                .AddAsync(cliente)
                .ConfigureAwait(false);
        }

        public async Task<Cliente> Obter(string cpf)
        {

            return await _context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(cliente => cliente.Cpf.Numero == cpf);
        }
    }
}