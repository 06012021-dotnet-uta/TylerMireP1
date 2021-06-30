using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Locations
{
    public class Inventory
    {
        /// <summary>
        /// Returns a list of LocationProductInfo objects that match on the specified location ID
        /// </summary>
        public class Query : IRequest<List<LocationProductInfo>>
        {
            public Guid? locationId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<LocationProductInfo>>
        {
            private readonly DataContext _context;

            public Handler(DataContext dataContext)
            {
                _context = dataContext;
            }

            public async Task<List<LocationProductInfo>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<LocationProductInfo> productInfos;
                IEnumerable<LocationProductInfo> locationProductInfos;

                if(request.locationId == null)
                {
                    productInfos = await _context.LocationProductInfoes.ToListAsync();
                    locationProductInfos = productInfos;
                }
                else
                {
                    productInfos = await _context.LocationProductInfoes.ToListAsync();
                    locationProductInfos = from x in productInfos where x.LocationId == request.locationId select x;
                }
                

                return locationProductInfos.ToList();
            }
        }


    }
}
