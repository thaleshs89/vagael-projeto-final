using Flunt.Validations;
using el.api.locathales.Domain.Core.Entities;
using el.api.locathales.Domain.ValueObjects;
using System;

namespace el.api.locathales.Domain.Entities
{
    public class Operador : Usuario
    {
        protected Operador() { }

        public Operador(string nome, Matricula matricula, Email email)
        {
            Nome = nome;
            Matricula = matricula;
            Email = email;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Nome, nameof(Nome), "Campo Nome não pode ser vazio"));

            if (Matricula != null)
            {
                AddNotifications(this.Matricula);
            }
        }

        public Matricula Matricula { get; private set; }

    }
}
