using API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public class CustomerController : BaseController
{

    public CustomerController(IMediator mediator, ILogger<CustomerController> logger) : base(mediator, logger) {}
    public IActionResult Index()
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