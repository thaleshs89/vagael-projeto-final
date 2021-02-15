using el.api.locathales.Domain.Enums;
using el.api.locathales.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace el.api.locathales.Infrastructure.Repositories
{
    public class CacheMemoriaRepository : ICacheMemoriaRepository
    {
        private readonly MemoryCache memoryCache;

        public CacheMemoriaRepository(MemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public void Salvar<TObjetoCache>(string chave, TObjetoCache objeto,
            ExpiracaoCache periodoAtualizacaoCacheMemoria = ExpiracaoCache._2Minutos)
        {
            memoryCache.Set(chave, objeto, DateTimeOffset.Now.AddMinutes(periodoAtualizacaoCacheMemoria.GetHashCode()));
        }
        public TObjetoCache Obter<TObjetoCache>(string chave)
        {
            return (TObjetoCache)memoryCache.Get(chave);
        }

        public async Task<TObjetoCache> ObterAsync<TObjetoCache>(string chave, Func<Task<TObjetoCache>> funcaoPopularAsync,
            ExpiracaoCache periodoAtualizacaoCacheMemoria = ExpiracaoCache._2Minutos)
        {
            if (memoryCache == null)
            {
                return await funcaoPopularAsync();
            }

            TObjetoCache resultado;

            try
            {
                var resultadoCache = memoryCache.Get(chave);

                if (resultadoCache == default)
                {
                    resultado = await funcaoPopularAsync();

                    if (!Equals(resultado, default(TObjetoCache)))
                    {
                        var politicaDeExpiracao = new CacheItemPolicy
                        {
                            AbsoluteExpiration = ObterDataExpiracao(periodoAtualizacaoCacheMemoria)
                        };

                        memoryCache.Set(chave, resultado, politicaDeExpiracao);
                    }
                }
                else
                {
                    resultado = (TObjetoCache)resultadoCache;
                }
            }
            catch (Exception exception)
            {
                resultado = await funcaoPopularAsync();
            }

            return resultado;
        }

        public List<KeyValuePair<string, object>> Listar()
        {
            return memoryCache.ToList();
        }

       public void Limpar()
        {
            var chavesCache = memoryCache.Select(itemCache => itemCache.Key);

            foreach (string chaveCache in chavesCache)
            {
                memoryCache.Remove(chaveCache);
            }
        }

        private static DateTime ObterDataExpiracao(ExpiracaoCache periodoAtualizacao)
        {
            var minutosAtualizacao = (int)periodoAtualizacao;
            var agora = DateTime.Now;
            var data = new DateTime(agora.Year, agora.Month, agora.Day, agora.Hour, agora.Minute, 0);
            data = data.AddMinutes(minutosAtualizacao);
            data = data.AddMinutes(-(data.Minute % minutosAtualizacao));

            while (data < DateTime.Now)
            {
                data = data.AddMinutes(minutosAtualizacao);
            }

            return data;
        }
    }
}
