using Microsoft.AspNetCore.Mvc;
using el.api.locathales.Api.Controllers;
using el.api.locathales.Application;
using el.api.locathales.Tests.Fixtures;
using el.api.locathales.Tests.Mocks;
using Xunit;

namespace el.api.locathales.Tests.Controllers
{
    [Collection("Mapper")]
    public class ClienteControllerTest
    {
        private readonly MapperFixture _mapperFixture;

        public ClienteControllerTest(MapperFixture mapperFixture)
        {
            _mapperFixture = mapperFixture;
        }


        private ClienteController CreateClienteController()
        {
            var clienteRepository = new ClienteRepositoryMock();
            var clienteApplication = new ClienteApplication(_mapperFixture.Mapper, clienteRepository);

            return new ClienteController(_mapperFixture.Mapper, clienteApplication);
        }

        private T GetOkObject<T>(IActionResult result)
        {
            var okObjectResult = (OkObjectResult)result;
            return (T)okObjectResult.Value;
        }
    }
}
