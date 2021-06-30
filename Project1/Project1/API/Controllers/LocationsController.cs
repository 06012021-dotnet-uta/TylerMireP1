using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Locations;

namespace API.Controllers
{
    public class LocationsController : BaseController
    {
        public LocationsController(IMediator mediator, ILogger<LocationsController> logger)
            : base(mediator, logger)
        {
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
            return View(await _mediator.Send(new List.Query()));
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            var locationDetails = await _mediator.Send(new Inventory.Query() { locationId = id});
            var location = await _mediator.Send(new Details.Query() { locationId = id });
            var productInfoDict = await _mediator.Send(new Application.Products.DetailsDict.Request());

            ViewBag.Location = location;
            ViewBag.Inventory = locationDetails;
            ViewBag.ProductInfoDict = productInfoDict;

            return View();
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            return View();
        }

        

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AddressStreet,AddressCity,AddressState,PhoneNumber,ZipCode")] Location newLocation)
        {
            Location l;
            if (ModelState.IsValid)
            {
                l = await _mediator.Send(new Add.Request() { location = newLocation});
            }
            return View();
        }
        /*

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,AddressStreet,AddressCity,AddressState,PhoneNumber,ZipCode")] Location location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.Id))
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
            return View(location);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var location = await _context.Locations.FindAsync(id);
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(Guid id)
        {
            return _context.Locations.Any(e => e.Id == id);
        }
        */
    }
}
