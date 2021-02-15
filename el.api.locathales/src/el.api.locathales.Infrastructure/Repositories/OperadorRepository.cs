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
    public class OperadorRepository : IOperadorRepository
    {
        private readonly IMapper _mapper;
        private readonly LocaThalesContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public OperadorRepository(IMapper mapper, LocaThalesContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Incluir(Operador operador)
        {
            await _context.Operadores
                  .AddAsync(operador)
                  .ConfigureAwait(false);
        }

        public async Task<Operador> Obter(string matricula)
        {
            return await _context.Operadores
                .AsNoTracking()
                .FirstOrDefaultAsync(operador => operador.Matricula.Numero == matricula);
        }
    }    
}