using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Orders
{
    public class CartDetails
    {
        /// <summary>
        /// Retrieves a list of CartItems for the specified user
        /// </summary>
        public class Request : IRequest<List<CartItem>>
        {
            public IIdentity user { get; set; }
        }

        public class Handler : IRequestHandler<Request, List<CartItem>>
        {
            private readonly UserManager<Customer> _userManager;
            private readonly DataContext _context;

            public Handler(DataContext context, UserManager<Customer> userManager)
            {
                _userManager = userManager;
                _context = context;
            }

            public async Task<List<CartItem>> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.user.Name);
                var userId = new Guid(user.Id);
                var cart = await _context.Carts.Where(cart => cart.CustomerId == userId).FirstOrDefaultAsync();
                var returnVal = await _context.CartItems.Where(x => x.CartId == cart.Id).ToListAsync();
                return returnVal;
            }
        }

    }
}
