using System;

namespace el.api.locathales.Application.Models
{
    public class VeiculoModel
    {
        public Guid Id { get; set; }
        public string Placa { get; set; }
        public int CodigoMarca { get; set; }
        public int CodigoModelo { get; set; }
        public int Ano { get; set; }
        public int ValorHora { get; set; }
        public string Combustivel { get; set; }
        public int LimitePortaMalas { get; set; }
        public string Categoria { get; set; }
        public int StatusVeiculo { get; set; }
        public string NomeModelo { get; set; }
    }
}