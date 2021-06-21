using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Persistence;

namespace Application.Handler
{
    internal class LocationHandler : BaseHandler, IHandler
    {

        public LocationHandler(DataContext context)
            : base(context)
        {
        }

        public bool Create(Location newLocation)
        {
            return Create<Location>(newLocation);
        }

        public Location Read(Guid id)
        {
            return Read<Location>(id);
        }

        public bool Update(Location newLocation)
        {
            return Update<Location>(newLocation, newLocation.Id);
        }

        public bool Delete(Guid id)
        {
            return Delete<Location>(id);
        }

        public List<Location> List()
        {
            List<Location> locations = _context.Locations.ToList();
            return locations;
        }

        public List<LocationProductInfo> ListLocationInventory(Guid locationId)
        {
            List<LocationProductInfo> LocationProductInfos = _context.LocationProductInfoes.ToList();

            IEnumerable<LocationProductInfo> locationInventory =
                from inventory in LocationProductInfos
                where inventory.LocationId == locationId
                select inventory;

            return locationInventory.ToList();
        }
    }
}