using el.api.locathales.Domain.Core.Entities;
using Flunt.Validations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace el.api.locathales.Domain.Entities
{
    public class Veiculo : Entity, IAggregateRoot
    {
        protected Veiculo() { }

        public Veiculo(string placa, int codigoMarca, int codigoModelo, int ano, int valorDia, string combustivel, int limitePortaMalas, string categoria, string nomeModelo)
        {

            Placa = placa;
            CodigoMarca = codigoMarca;
            CodigoModelo = codigoModelo;
            Ano = ano;
            ValorDia = valorDia;
            Combustivel = combustivel;
            LimitePortaMalas = limitePortaMalas;
            Categoria = categoria;
            NomeModelo = nomeModelo;

            AddNotifications(new Contract()
               .Requires()
               .IsNotNullOrWhiteSpace(Placa, nameof(Placa), "Placa não pode ser nulo ou branco")
               .IsNotNullOrWhiteSpace(Combustivel, nameof(Combustivel), "Combustível não pode ser nulo ou branco")
               .IsNotNullOrWhiteSpace(Categoria, nameof(Categoria), "Categoria não pode ser nulo ou branco")
               .IsNotNullOrWhiteSpace(NomeModelo, nameof(NomeModelo), "Nome do Modelo não pode ser nulo ou branco")

               .Matchs(placa, "^[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}$|^[a-zA-Z]{3}[0-9]{4}$", nameof(placa), "Placa Inválida"));
        }

        public string Placa { get; private set; }
        public int CodigoMarca { get; private set; }
        public int CodigoModelo { get; private set; }
        public int Ano { get; private set; }
        public int ValorDia { get; private set; }
        public string Combustivel { get; private set; }
        public int LimitePortaMalas { get; private set; }
        public string Categoria { get; private set; }
        public string NomeModelo { get; private set; }
        public int Status { get; private set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCriacao { get; private set; }

        public void CriarVeiculo()
        {
            DataCriacao = DateTime.Now;
            Status = (int)Enums.Enumeradores.StatusVeiculo.Disponivel;
        }

        public void AlterarStatusVeiculo(Enums.Enumeradores.StatusVeiculo statusVeiculo)
        {
            Status = (int)statusVeiculo;
        }
    }
}