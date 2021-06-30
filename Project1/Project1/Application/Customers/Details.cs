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
    public class Details
    {
        /// <summary>
        /// Returns a Customer with an Id matched in specified Guid
        /// </summary>
        public class Query : IRequest<Customer> 
        { 
            public Guid? CustomerId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Customer>
        {
            private readonly DataContext _context;
            private readonly ILogger<List> _logger;

            public Handler(DataContext context, ILogger<List> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<Customer> Handle(Query request, CancellationToken ct)
            {

                return await _context.Customers.FirstOrDefaultAsync(m => m.Id.ToUpper() == request.CustomerId.ToString().ToUpper());

            }
        }
    }
}
