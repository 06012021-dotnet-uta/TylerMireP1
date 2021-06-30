using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using MediatR;

namespace Project1.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IMediator mediator, ILogger<HomeController> logger) : base(mediator, logger) {}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
