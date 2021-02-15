using el.api.locathales.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace el.api.locathales.Domain.Repositories
{
    public interface ICacheMemoriaRepository
    {
        void Limpar();
        List<KeyValuePair<string, object>> Listar();
        TObjetoCache Obter<TObjetoCache>(string chave);
        Task<TObjetoCache> ObterAsync<TObjetoCache>(string chave, Func<Task<TObjetoCache>> funcaoPopularAsync, ExpiracaoCache periodoAtualizacaoCacheMemoria = ExpiracaoCache._2Minutos);
        void Salvar<TObjetoCache>(string chave, TObjetoCache objeto, ExpiracaoCache periodoAtualizacaoCacheMemoria = ExpiracaoCache._2Minutos);
    }
}