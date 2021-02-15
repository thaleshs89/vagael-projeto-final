using el.api.locathales.Domain.Core.Entities;
using Flunt.Validations;
using System;

namespace el.api.locathales.Domain.Entities
{
    public class ContratoLocacao : Entity, IAggregateRoot
    {
        public ContratoLocacao(string placa, DateTime dataInicial, DateTime dataFinal, string cpfCliente)
        {
            Placa = placa;
            DataInicial = dataInicial;
            DataFinal = dataFinal;
            CpfCliente = cpfCliente;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(CpfCliente, nameof(CpfCliente), "O campo CPF Cliente é obrigatório.")
                .IsTrue(DataInicial != DateTime.MinValue, nameof(DataInicial), "O campo Data Inicial obrigatário.")
                .IsTrue(DataFinal != DateTime.MinValue, nameof(DataFinal), "O campo Data Final obrigatário.")
                .IsNotNullOrWhiteSpace(Placa, nameof(Placa), "O campo Placa é obrigatório."));
        }

        public string Placa { get; private set; }
        public DateTime DataInicial { get; private set; }
        public DateTime DataFinal { get; private set; }
        public Decimal ValorTotalContrato { get; private set; }
        public Decimal ValorDia { get; private set; }
        public string CpfCliente { get; private set; }
        public int StatusContrato { get; private set; }
        public DateTime DataCriação { get; private set; }
        public DateTime DataModificação { get; private set; }
        public DateTime DataAtivacao { get; private set; }
        public Int64 NumeroContrato { get; private set; }
        public void CriarContrato()
        {
            StatusContrato = 1;
            DataCriação = DateTime.Now;
        }

        public void ValidarDatas()
        {
            var hoje = DateTime.Now.Date;
            AddNotifications(new Contract()
               .Requires()
               .IsTrue(DataInicial >= hoje, nameof(DataInicial), $"O campo Data Inicial deve ser maior que {hoje}")
               .IsTrue(DataFinal >= hoje.AddDays(1), nameof(DataFinal), $"O campo Data Final deve ser maior que {hoje.AddDays(1)}")
               .IsTrue(DataInicial.Date < DataFinal.Date, nameof(DataFinal), $"O campo Data Final deve ser maior que {DataInicial.Date}")
               .IsTrue(DataFinal.Date.Subtract(DataInicial.Date).Days < Parametros.LimiteData, nameof(DataFinal), $"Diferença entre as datas deve ser menor que {Parametros.LimiteData} dias."));
        }

        public void CalcularValorContrato(decimal valorDiaria)
        {
            var diasContrato = DataFinal.Subtract(DataInicial).Days;
            ValorDia = valorDiaria;
            ValorTotalContrato = diasContrato * ValorDia;
        }

        public void AlterarStatusContrato(int statusContrato)
        {
            StatusContrato = statusContrato;
        }

        public bool ValidarAlteracaoStatus(int status, ref int novoStatusVeiculo)
        {
            var permiteAlterar = false;
            switch (status)
            {
                case (int)Enums.Enumeradores.StatusContrato.Ativo:
                    if (StatusContrato == (int)Enums.Enumeradores.StatusContrato.Simulacao)
                    {
                        permiteAlterar = true;
                        novoStatusVeiculo = (int)Enums.Enumeradores.StatusVeiculo.Reservado;
                    }
                    break;
                case (int)Enums.Enumeradores.StatusContrato.Finalizado:
                    if (StatusContrato == (int)Enums.Enumeradores.StatusContrato.Ativo)
                    {
                        permiteAlterar = true;
                        novoStatusVeiculo = (int)Enums.Enumeradores.StatusVeiculo.Devolvido;
                    }
                    break;
                case (int)Enums.Enumeradores.StatusContrato.Cancelado:
                    if (StatusContrato == (int)Enums.Enumeradores.StatusContrato.Ativo)
                    {
                        permiteAlterar = true;
                        novoStatusVeiculo = (int)Enums.Enumeradores.StatusVeiculo.Disponivel;
                    }
                    break;
            }
            if(permiteAlterar)
            {
                StatusContrato = status;
            }
            return permiteAlterar; 
        }
    }
}