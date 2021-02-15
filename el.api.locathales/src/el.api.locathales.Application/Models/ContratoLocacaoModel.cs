using System;

namespace el.api.locathales.Application.Models
{
    public class ContratoLocacaoModel
    {
        public string Placa { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public Decimal ValorTotalContrato { get; set; }
        public Decimal ValorDia { get; set; }
        public string CpfCliente { get; set; }
        public int StatusContrato { get; set; }
        public Int64 NumeroContrato { get; set; }
    }
}