using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Orders
{
    public class OrderList
    {
        /// <summary>
        /// Retrieves the list of orders for a specified user
        /// </summary>
        public class Request : IRequest<List<OrderItem>>
        {
            public IIdentity user { get; set; }
        }

        public class Handler : IRequestHandler<Request, List<OrderItem>>
        {
            private readonly UserManager<Customer> _userManager;
            private readonly DataContext _context;

            public Handler(DataContext context, UserManager<Customer> userManager)
            {
                _userManager = userManager;
                _context = context;
            }

            public async Task<List<OrderItem>> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.user.Name);
                var userId = new Guid(user.Id);
                var orders = await _context.Orders.Where(o => o.CustomerId == userId).ToListAsync();
                List<OrderItem> orderItems = new List<OrderItem>();
                foreach(var o in orders)
                {
                    orderItems.AddRange(_context.OrderItems.Where(x => x.OrderId == o.Id).ToList());
                }
                return orderItems;
            }
        }

    }
}
