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
    public class ContratoLocacaoRepository : IContratoLocacaoRepository
    {
        private readonly IMapper _mapper;
        private readonly LocaThalesContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ContratoLocacaoRepository(IMapper mapper, LocaThalesContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ContratoLocacao> Obter(long numeroContrato)
        {
            return await _context.Contratos
                 .AsNoTracking()
                 .FirstOrDefaultAsync(contrato => contrato.NumeroContrato == numeroContrato);
        }

        public async Task Incluir(ContratoLocacao contratoLocacao)
        {
            await _context.Contratos
               .AddAsync(contratoLocacao)
               .ConfigureAwait(false);
        }

        public void Alterar(ContratoLocacao contratoLocacao)
        {
            _context.Entry(contratoLocacao).State = EntityState.Modified;
            _context.Contratos.Update(contratoLocacao);
        }
    }
}