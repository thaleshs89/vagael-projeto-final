using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Text;

namespace el.api.locathales.Domain.Enums
{
    public class Enumeradores
    {
        public enum StatusVeiculo
        {
            [Description("Disponível para Locação")]
            Disponivel = 1,
            [Description("Veiculo Reservado")]
            Reservado = 2,
            [Description("Retirado pelo Cliente")]
            Retirado = 3,
            [Description("Revolvido pelo Cliente")]
            Devolvido = 4,
            [Description("Veículo em Manutenção")]
            EmManutencao = 5
        }

        public enum StatusContrato
        {
            [Description("Simulação")]
            Simulacao = 0,
            [Description("Ativo")]
            Ativo = 1,
            [Description("Finalizado")]
            Finalizado = 3,
            [Description("Cancelado")]
            Cancelado = 4
        }
    }

    public static class Constantes
    {
        public struct TiposCategoria
        {
            public const string Basico = "B";
            public const string Intermediario = "I";
            public const string Luxo = "L";
        }

        private static List<string> listaTipoCategoria = new List<string>() { TiposCategoria.Basico, TiposCategoria.Intermediario, TiposCategoria.Luxo };
        public static IReadOnlyCollection<string> ListaTipoCategoria => listaTipoCategoria.AsReadOnly();
        
    }

}
