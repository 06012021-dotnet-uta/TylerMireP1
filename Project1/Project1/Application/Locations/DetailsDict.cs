using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Locations
{
    public class DetailsDict
    {
        /// <summary>
        /// Retrieves a dictionary of all locations with their ID as the key
        /// </summary>
        public class Request : IRequest<Dictionary<Guid, Location>>
        { 
        }

        public class Handler : IRequestHandler<Request, Dictionary<Guid, Location>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Dictionary<Guid, Location>> Handle(Request request, CancellationToken cancellationToken)
            {
                return await _context.Locations.ToDictionaryAsync(x => x.Id, x => x);
            }
        }

    }
}
