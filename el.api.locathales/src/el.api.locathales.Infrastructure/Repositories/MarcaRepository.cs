using AutoMapper;
using el.api.locathales.Domain;
using el.api.locathales.Domain.Commun;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.Repositories;
using el.api.locathales.Infrastructure.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace el.api.locathales.Infrastructure.Repositories
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly IMapper _mapper;
        private readonly LocaThalesContext _context;
        public IUnitOfWork UnitOfWork => _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICacheMemoriaRepository _cacheMemoria;
        private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        public MarcaRepository(IMapper mapper, LocaThalesContext context, IHttpClientFactory httpClientFactory, ICacheMemoriaRepository cacheMemoria)
        {
            _mapper = mapper;
            _context = context;
            _httpClientFactory = httpClientFactory;
            _cacheMemoria = cacheMemoria;
        }

        public async Task<IEnumerable<Marca>> ListarTodos()
        {
            return await _cacheMemoria.ObterAsync($"{Parametros.Cache.Marcas}", async () => await ListarTodasApiAsync());
        }

        public async Task<Marca> Obter(int idFipe)
        {
            var listaMarcas = await ListarTodos();
            return listaMarcas.FirstOrDefault(ma => ma.IdFipe == idFipe);
        }

        private async Task<IEnumerable<Marca>> ListarTodasApiAsync()
        {
            var cliente = _httpClientFactory.CreateClient(Parametros.Fipe.Nome);

            var response = await cliente.GetAsync($"{Parametros.Fipe.CodigoTipoVeiculo}/carros/marcas.json");

            if (response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                var retorno = response.Content.ReadAsStringAsync();
                var listaMarcas = JsonSerializer.Deserialize<IEnumerable<Marca>>(await retorno, jsonSerializerOptions);
                return listaMarcas ;
            }
            else
            {
                var conteudo = await response.Content.ReadAsStringAsync();
            }

            return default;
        }
    }    
}