using AutoMapper;
using el.api.locathales.Domain.Commun;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.Enums;
using el.api.locathales.Domain.Repositories;
using el.api.locathales.Infrastructure.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace el.api.locathales.Infrastructure.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly IMapper _mapper;
        private readonly LocaThalesContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public VeiculoRepository(IMapper mapper, LocaThalesContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Incluir(Veiculo veiculo)
        {
            await _context.Veiculos
               .AddAsync(veiculo)
               .ConfigureAwait(false);
        }

        public async Task<Veiculo> Obter(string placa)
        {
            return await _context.Veiculos
                .AsNoTracking()
                .FirstOrDefaultAsync(veiculo => veiculo.Placa == placa);
        }

        public async Task<IEnumerable<Veiculo>> ListarPorCategoria(string categoria)
        {
            return await _context.Veiculos
                .AsNoTracking()
                .Where(veiculo => veiculo.Categoria == categoria)
                .ToListAsync();
        }

        public async Task<Veiculo> ObterPorStatus(Enumeradores.StatusVeiculo status)
        {
            return await _context.Veiculos
                .AsNoTracking()
                .FirstOrDefaultAsync(veiculo => veiculo.Status == (int)status);
        }

        public void Alterar(Veiculo veiculo)
        {
            _context.Entry(veiculo).State = EntityState.Modified;
            _context.Veiculos.Update(veiculo);
        }
    }
}