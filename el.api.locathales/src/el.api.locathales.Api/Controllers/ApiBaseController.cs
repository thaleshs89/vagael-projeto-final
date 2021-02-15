using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using el.api.locathales.Application.Models;
using System.Collections.Generic;

namespace el.api.locathales.Api.Controllers
{
    public abstract class ApiBaseController : ControllerBase
    {
        protected BadRequestObjectResult BadRequest(IReadOnlyCollection<Notification> notifications)
        {
            return new BadRequestObjectResult(new ErrorModel(notifications));
        }

        protected NotFoundObjectResult NotFound(string message)
        {
            return new NotFoundObjectResult(new ErrorModel(message));
        }
    }
}