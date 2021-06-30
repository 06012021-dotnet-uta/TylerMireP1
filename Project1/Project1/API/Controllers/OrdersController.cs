using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.Extensions.Logging;
using Domain;
using Application.Orders;
using System.Collections.Generic;

namespace API.Controllers
{
    public class OrdersController : BaseController
    {

        public OrdersController(IMediator mediator, ILogger<OrdersController> logger)
            : base(mediator, logger)
        {
        }

        public async Task<IActionResult> AddToCart(OrderFormModel formOrders)
        {
            await _mediator.Send(new AddToCart.Request()
            {
                orders = formOrders,
                user = HttpContext.User.Identity
            });
            return RedirectToAction("Index", "Locations");
        }

        public async Task<IActionResult> Cart()
        {
            var cartDetails = await _mediator.Send(new CartDetails.Request() { user = HttpContext.User.Identity });
            ViewBag.Inventory = await _mediator.Send(new Application.Locations.Inventory.Query());
            ViewBag.ProductDetailsDict = await _mediator.Send(new Application.Products.DetailsDict.Request());
            ViewBag.LocationDetailsDict = await _mediator.Send(new Application.Locations.DetailsDict.Request());

            return View(cartDetails);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart(List<CartItem> cartDetails)
        {
            await _mediator.Send(new UpdateCart.Request()
            {
                updatedCartItems = cartDetails,
                user = HttpContext.User.Identity
            });

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Checkout()
        {
            var cartDetails = await _mediator.Send(new CartDetails.Request() { user = HttpContext.User.Identity });
            ViewBag.Inventory = await _mediator.Send(new Application.Locations.Inventory.Query());
            ViewBag.ProductDetailsDict = await _mediator.Send(new Application.Products.DetailsDict.Request());
            ViewBag.LocationDetailsDict = await _mediator.Send(new Application.Locations.DetailsDict.Request());

            return View(cartDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(List<CartItem> cartDetails)
        {
            await _mediator.Send(new Checkout.Request() { cartItems = cartDetails, user = HttpContext.User.Identity });

            return RedirectToAction("Index", "Home");
        }


        /*

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LocationId,CustomerId,ProductId,OrderCreationDate,LastOrderDate,TotalItems,Total")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Id = Guid.NewGuid();
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,LocationId,CustomerId,ProductId,OrderCreationDate,LastOrderDate,TotalItems,Total")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
        */
    }
}
