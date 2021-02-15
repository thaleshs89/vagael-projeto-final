using Flunt.Validations;
using el.api.locathales.Domain.Core.ValueObjects;

namespace el.api.locathales.Domain.ValueObjects
{
    public class Matricula : ValueObject
    {
        public Matricula(string numero)
        {
            Numero = numero;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Numero, nameof(Matricula.Numero), "Matricula não pode ser nulo ou branco")
                .HasLen(Numero, 7, nameof(Matricula.Numero), "Matricula deve conter 7 dígitos")
                .IsDigit(Numero, nameof(Matricula.Numero), "Matricula deve conter apenas números"));
        }

        public string Numero { get; private set; }

        public override string ToString()
        {
            return Numero;
        }
    }
}
