using el.api.locathales.Domain.Commun;
using el.api.locathales.Domain.Entities;
using el.api.locathales.Domain.Repositories;
using el.api.locathales.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace el.api.locathales.Tests.Mocks
{
    public class ClienteRepositoryMock : IClienteRepository
    {
        public async Task Incluir(Cliente cliente)
        {
        }

        public async Task<Cliente> Obter(string cpf)
        {
            return Clientes.FirstOrDefault(c => c.Cpf.ToString() == cpf);
        }

        private static IEnumerable<Cliente> Clientes
        {
            get
            {
                var cliente1 = new Cliente(
                        "João",
                        new CPF("12345678910"),
                        new Email("joaosilva@gmail.com"),
                        new DateTime(1990,10,10), "Teste12");

                var cliente2 = new Cliente(
                        "Silveira",
                        new CPF("01987654321"),
                        new Email("mariasilva@gmail.com"),
                        new DateTime(1970, 2, 3), "teste01");

                typeof(Operador).GetProperty("Id").SetValue(cliente1, Guid.Parse("f8a0db6b-dabf-4f97-9b1c-8cf08b930466"));
                typeof(Operador).GetProperty("Id").SetValue(cliente2, Guid.Parse("6fdc66ad-649f-4f3c-9806-6409e8ca4e47"));

                return new Cliente[]
                {
                        cliente1, cliente2
                };
            }
        }

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();
    }
}
