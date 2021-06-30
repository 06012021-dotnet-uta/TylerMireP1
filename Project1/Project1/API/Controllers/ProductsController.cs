using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
            :base(mediator, logger)
        {
        }
    }
}
