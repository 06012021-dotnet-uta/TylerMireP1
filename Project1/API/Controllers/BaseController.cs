using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IMediator _mediator;
        protected readonly ILogger _logger;

        public BaseController(IMediator mediator, ILogger logger)
        {
           _mediator = mediator;
           _logger = logger;
        }
    }
}