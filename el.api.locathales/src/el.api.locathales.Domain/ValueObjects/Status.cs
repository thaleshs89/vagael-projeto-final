using Flunt.Validations;
using el.api.locathales.Domain.Core.ValueObjects;

namespace el.api.locathales.Domain.ValueObjects
{
    public class Status : ValueObject
    {
        public Status(int value, string descricao)
        {
            Value = value;
            Descricao = descricao;
        }

        public int Value { get; private set; }
        public string Descricao { get; private set; }
        

        public override string ToString()
        {
            return Descricao;
        }
    }
}
