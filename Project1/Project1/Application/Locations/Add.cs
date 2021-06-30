using Domain;
using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using Persistence;

namespace Application.Locations
{
    public class Add
    {   
        /// <summary>
        /// Adds a new location to the database
        /// </summary>
        public class Request : IRequest<Location> 
        {
            public Location location { get; set; }
        }

        public class Handler : IRequestHandler<Request, Location>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Location> Handle(Request request, CancellationToken cancellationToken)
            {
                if(_context.Locations.Where(x => x.Name == request.location.Name).FirstOrDefault() != default(Location))
                {
                    return null;
                }
                _context.Add(request.location);
                bool success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success == false)
                    throw new Exception("Problem saving changes.");

                return request.location;
            }
        }
    }
}
