using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Customers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System;

namespace API.Controllers
{
    public class CustomersController : BaseController
    {
        private readonly SignInManager<Customer> _signInManager;

        public CustomersController(IMediator mediator, ILogger<CustomersController> logger, SignInManager<Customer> signInManager) 
            : base(mediator, logger) 
        {
            _signInManager = signInManager;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _mediator.Send(new List.Query()));
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterCustomerModel newCustomer)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new Register.Command()
                {
                    UserName = newCustomer.UserName,
                    Password = newCustomer.Password,
                    FirstName = newCustomer.FirstName,
                    LastName = newCustomer.LastName,
                    Email = newCustomer.Email,
                    AddressStreet = newCustomer.AddressStreet,
                    AddressCity = newCustomer.AddressCity,
                    AddressState = newCustomer.AddressState
                });
                return RedirectToAction("Index", "Home");
            }
            return View(newCustomer);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginCustomerModel loginCustomer)
        {
            if(!ModelState.IsValid)
            {
                return View(loginCustomer);
            }
            
            var c = await _mediator.Send(new Login.Query() 
            {
                UserName = loginCustomer.UserName,
                Password = loginCustomer.Password
            });

            if(c == null)
            {
                ModelState.AddModelError(string.Empty, "The username or password is incorrect.");
                return View(loginCustomer);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _mediator.Send(new Logout.Request());
            return RedirectToAction("Index", "Home");
        }

        
        // GET: Customers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _mediator.Send(new Details.Query() { CustomerId = id });
            ViewBag.LocationInfoDict = await _mediator.Send(new Application.Locations.DetailsDict.Request());
            ViewBag.OrderItems = await _mediator.Send(new Application.Orders.OrderList.Request() { user = HttpContext.User.Identity });

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDefaultLocation(Customer customer)
        {
            await _mediator.Send(new SetDefaultLocation.Query() { CustomerId = new Guid(customer.Id), DefaultLocationId = customer.DefaultLocationId });
            return RedirectToAction("Details", new { id = customer.Id});
        }

        /*
        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,PasswordHash,FirstName,LastName,Email,AddressStreet,AddressCity,AddressState,DefaultLocationId,AccountCreationDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Id = Guid.NewGuid();
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserName,PasswordHash,FirstName,LastName,Email,AddressStreet,AddressCity,AddressState,DefaultLocationId,AccountCreationDate")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(Guid id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
        */
    }
}
