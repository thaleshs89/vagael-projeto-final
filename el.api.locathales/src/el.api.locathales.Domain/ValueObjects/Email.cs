using Flunt.Validations;
using el.api.locathales.Domain.Core.ValueObjects;

namespace el.api.locathales.Domain.ValueObjects
{
    public class Email : ValueObject
    {        
        public Email(string endereco)
        {
            Endereco  = endereco;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Endereco, nameof(Email.Endereco), "Email não pode ser nulo ou branco")
                .IsEmail(Endereco, nameof(Email.Endereco), "Email inválido"));
        }

        public string Endereco { get; private set; }

        public override string ToString()
        {
            return Endereco;
        }
    }
}