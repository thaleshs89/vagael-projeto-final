using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace el.api.locathales.Domain
{
    /// <summary>
    /// Abstração dos parâmetros do sistema
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Parametros
    {
        public static int LimiteData => Convert.ToInt32(Environment.GetEnvironmentVariable("LIMITE_ENTRE_DATAS"));

        public static class Fipe
        {
            public static Uri UriFipe => new Uri(Environment.GetEnvironmentVariable("API_URL_Fipe"));
            public static string Nome => nameof(Fipe);
            public static string CodigoTipoVeiculo => "1";
        }
        public static class Cache
        {
            public const string Prefixo = "el.api.locathales";
            public const string Marcas = "1";
            public const string Modelos = "2";
            public const string DetalhesModelo = "3";
        }
    }
}
