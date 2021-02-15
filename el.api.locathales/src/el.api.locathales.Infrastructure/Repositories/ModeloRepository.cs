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
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace el.api.locathales.Infrastructure.Repositories
{
    public class ModeloRepository : IModeloRepository
    {
        private readonly IMapper _mapper;
        private readonly LocaThalesContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICacheMemoriaRepository _cacheMemoria;
        private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        public IUnitOfWork UnitOfWork => _context;

        public static readonly int TamanhoCodigoVeiculoZero = 7;

        public ModeloRepository(IMapper mapper, LocaThalesContext context, IHttpClientFactory httpClientFactory, ICacheMemoriaRepository cacheMemoria)
        {
            _mapper = mapper;
            _context = context;
            _httpClientFactory = httpClientFactory;
            _cacheMemoria = cacheMemoria;
        }

        public async Task<IEnumerable<Modelo>> ListarPorMarca(string idFipeMarca)
        {
            return await _cacheMemoria.ObterAsync($"{Parametros.Cache.Modelos}{idFipeMarca}", async () => await ListarPorMarcaApiAsync(idFipeMarca));
        }

        public async Task<IEnumerable<Modelo>> ListarPorMarcaApiAsync(string idFipeMarca)
        {
            var cliente = _httpClientFactory.CreateClient(Parametros.Fipe.Nome);
            var response = await cliente.GetAsync($"{Parametros.Fipe.CodigoTipoVeiculo}/carros/veiculos/{idFipeMarca}.json");

            if (response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                return JsonSerializer.Deserialize<IEnumerable<Modelo>>(await response.Content.ReadAsStringAsync(), jsonSerializerOptions);
            }
            else
            {
                var conteudo = await response.Content.ReadAsStringAsync();
            }

            return default;
        }

        public async Task<ModeloDetalhes> ObterDetalhesModelo(string idMarca, string idModeloFipe)
        {
            
            var listaModelosAnos = await _cacheMemoria.ObterAsync($"{Parametros.Cache.DetalhesModelo}-{idMarca}-{idModeloFipe}", async () => await ObterModelosAnoPOrMarcaModelo(idMarca, idModeloFipe));
            if(listaModelosAnos != null && listaModelosAnos.Any())
            {
                var detalheModelo = new ModeloDetalhes() { IdFipe = idModeloFipe };
                var listaAnoModelo = new List<KeyValuePair<int, decimal>>();
                foreach (var item in listaModelosAnos)
                {
                    if(item.FipeCodigo.Length == TamanhoCodigoVeiculoZero)
                    {
                        continue;
                    }
                    var detalheVeiculo = await ObterDetalhesPorAnoModelo(idMarca, idModeloFipe, item.FipeCodigo);
                    var chaveValor = new KeyValuePair<int, decimal>(Convert.ToInt32(detalheVeiculo.AnoModelo), Convert.ToDecimal(detalheVeiculo.Preco.Replace("R$","")));
                    listaAnoModelo.Add(chaveValor);
                    if (string.IsNullOrWhiteSpace(detalheModelo.Combustivel))
                    {
                        detalheModelo.Combustivel = detalheVeiculo.Combustivel;
                    }
                }
                detalheModelo.AnosModeloPreco = listaAnoModelo;
                return detalheModelo;
            }
            return default; 
        }

        private async Task<VeiculoAnoFipe> ObterDetalhesPorAnoModelo(string idMarca, string idModeloFipe, string codigoAno)
        {
            return await _cacheMemoria.ObterAsync($"{Parametros.Cache.DetalhesModelo}-{idModeloFipe}-{codigoAno}", async () => await ObbterDetalhesPorAnoModeloApiAsync(idMarca, idModeloFipe, codigoAno));
        }

        private async Task<VeiculoAnoFipe> ObbterDetalhesPorAnoModeloApiAsync(string idMarca, string idModeloFipe, string codigoAno)
        {
            var cliente = _httpClientFactory.CreateClient(Parametros.Fipe.Nome);

            var response = await cliente.GetAsync($"{Parametros.Fipe.CodigoTipoVeiculo}/carros/veiculo/{idMarca}/{idModeloFipe}/{codigoAno}.json");

            if (response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                return JsonSerializer.Deserialize<VeiculoAnoFipe>(await response.Content.ReadAsStringAsync(), jsonSerializerOptions);
            }
            else
            {
                var conteudo = await response.Content.ReadAsStringAsync();
            }

            return default;
        }

        private async Task<IEnumerable<Modelo>> ObterModelosAnoPOrMarcaModelo(string idMarca, string idModeloFipe)
        {
            var cliente = _httpClientFactory.CreateClient(Parametros.Fipe.Nome);

            var response = await cliente.GetAsync($"{Parametros.Fipe.CodigoTipoVeiculo}/carros/veiculo/{idMarca}/{idModeloFipe}.json");

            if (response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                return JsonSerializer.Deserialize<IEnumerable<Modelo>>(await response.Content.ReadAsStringAsync(), jsonSerializerOptions);
            }
            else
            {
                var conteudo = await response.Content.ReadAsStringAsync();
            }

            return default;
        }
    }
}