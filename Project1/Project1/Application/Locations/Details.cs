using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Locations
{
    public class Details
    {
        /// <summary>
        /// Returns a location object with the specified ID
        /// </summary>
        public class Query : IRequest<Location>
        {
            public Guid? locationId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Location>
        {
            private readonly DataContext _context;

            public Handler(DataContext dataContext)
            {
                _context = dataContext;
            }

            public async Task<Location> Handle(Query request, CancellationToken cancellationToken)
            {
                var location = await _context.Locations.FindAsync(request.locationId);

                return location;
            }
        }
    }
}
