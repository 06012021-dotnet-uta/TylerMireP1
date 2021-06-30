using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
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
