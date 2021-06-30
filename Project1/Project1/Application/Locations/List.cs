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
        /// <summary>
        /// Returns a list of all the locations in the database
        /// </summary>
        public class Query : IRequest<List<Location>> { }

        public class Handler : IRequestHandler<Query, List<Location>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Location>> Handle(Query request, CancellationToken ct)
            {
                return await _context.Locations.ToListAsync(ct);
            }
        }
    }
}
