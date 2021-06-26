using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using Persistence;
using Microsoft.Extensions.Logging;

namespace Application.Locations
{
    public class Add
    {   
        public class Request : IRequest<Location> 
        {
            public Location location { get; set; }
        }

        public class Handler : IRequestHandler<Request, Location>
        {
            private readonly DataContext _context;
            private readonly ILogger<Add> _logger;

            public Handler(DataContext context, ILogger<Add> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<Location> Handle(Request request, CancellationToken cancellationToken)
            {
                _context.Add(request.location);
                bool success = await _context.SaveChangesAsync(cancellationToken) > 0;

                if (success == false)
                    throw new Exception("Problem saving changes.");

                return request.location;
            }
        }
    }
}
