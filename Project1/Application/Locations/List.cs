using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Locations
{
    public class List
    {
        public class Query : IRequest<List<Location>> { }

        public class Handler : IRequestHandler<Query, List<Location>>
        {
            private readonly DataContext _context;
            private readonly ILogger<List> _logger;

            public Handler(DataContext context, ILogger<List> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<List<Location>> Handle(Query request, CancellationToken ct)
            {
                List<Location> locationList = await _context.Locations.ToListAsync(ct);
                return locationList;
            }
        }
    }
}
