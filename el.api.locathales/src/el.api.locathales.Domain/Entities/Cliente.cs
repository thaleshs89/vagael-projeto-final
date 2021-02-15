using Flunt.Validations;
using el.api.locathales.Domain.Core.Entities;
using el.api.locathales.Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace el.api.locathales.Domain.Entities
{
    public class Cliente : Usuario
    {
        protected Cliente() { }
        public Cliente(string nome, CPF cpf, Email email, DateTime dataNascimento, string senha)
        {
            Nome = nome;
            Cpf = cpf;
            Email = email;
            DataNascimento = dataNascimento;
            Senha = senha;
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Nome, nameof(Nome), "Campo Nome não pode ser vazio"));

            AddNotifications(Cpf);
        }

        public CPF Cpf { get; private set; }
        [Column(TypeName = "datetime")]
        public DateTime DataNascimento { get; private set; }

    }
}
