using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Threading.Tasks;
using Application.Locations;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class LocationsController : BaseController
    {

        public LocationsController(IMediator mediator, ILogger<LocationsController> logger) : base(mediator, logger)
        {
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
            return View(await _mediator.Send(new List.Query()));
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Locations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Locations/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
