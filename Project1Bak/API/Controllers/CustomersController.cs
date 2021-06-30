using API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Application.Customers;
using System.Threading.Tasks;

public class CustomersController : BaseController
{

    public CustomersController(IMediator mediator, ILogger<CustomersController> logger) : base(mediator, logger) {}
    public async Task<IActionResult> Index()
    {
        return View(await _mediator.Send(new List.Query()));
    }

    public ActionResult Register()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }
    public IActionResult Contact()
    {
        return View();
    }
    public IActionResult Error()
    {
        return View();
    }
}