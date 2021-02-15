using el.api.locathales.Domain.Core.Entities;
using el.api.locathales.Domain.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace el.api.locathales.Domain.Entities
{
    public abstract class Usuario : Entity, IAggregateRoot
    {
        public string Nome { get; protected set; }
        [Column(TypeName = "datetime")]
        public DateTime DataCriacao { get; protected set; }
        [Column(TypeName = "datetime")]
        public DateTime DataModificacao { get; protected set; }
        public Status Status { get; protected set; }
        public Email Email { get; protected set; }
        public string Senha { get; protected set; }

        public void Criar()
        {
            DataCriacao = DateTime.Now;
            DataModificacao = DateTime.Now;
            Status = new Status(1, "Ativo");
        }
    }
}
