using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Customers
{
    public class SetDefaultLocation
    {

        /// <summary>
        /// Sets the default store for a specific customer
        /// </summary>
        public class Query : IRequest
        {
            public Guid CustomerId { get; set; }
            public Guid DefaultLocationId { get; set; }
        }

        public class Handler : IRequestHandler<Query>
        {
            private readonly DataContext _context;
            private readonly ILogger<List> _logger;

            public Handler(DataContext context, ILogger<List> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<Unit> Handle(Query request, CancellationToken cancellationToken)
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == request.CustomerId.ToString());
                customer.DefaultLocationId = request.DefaultLocationId;


                return Unit.Value;
            }
        }
    }
}
