using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products
{
    public class DetailsDict
    {
        /// <summary>
        /// Returns a dictionary of all the products using their ID as a key
        /// </summary>
        public class Request : IRequest<Dictionary<Guid, Product>>
        { 
        }

        public class Handler : IRequestHandler<Request, Dictionary<Guid, Product>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Dictionary<Guid, Product>> Handle(Request request, CancellationToken cancellationToken)
            {
                return await _context.Products.ToDictionaryAsync(x => x.Id, x => x);
            }
        }

    }
}
