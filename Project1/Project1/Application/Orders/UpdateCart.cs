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
    public class UpdateCart
    {
        /// <summary>
        /// Updates the cart using the specified updated cart items for a user
        /// </summary>
        public class Request : IRequest
        {
            public List<CartItem> updatedCartItems { get; set; }
            public IIdentity user { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly DataContext _context;
            private readonly UserManager<Customer> _userManager;

            public Handler(DataContext context, UserManager<Customer> userManager)
            {
                _context = context;
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                Customer user = await _userManager.FindByNameAsync(request.user.Name);
                Cart userCart = await _context.Carts.Where(x => x.Customer.UserName == request.user.Name).FirstOrDefaultAsync();
                var locationProductInfoes = await _context.LocationProductInfoes.ToListAsync();
                var currentCartItems = await _context.CartItems.Where(x => x.CartId == userCart.Id).ToListAsync();
                var updatedCartItems = request.updatedCartItems;

                //Check for any 0 totalItems and update database item totals
                for(int i = 0; i < updatedCartItems.Count(); i++)
                {
                    updatedCartItems[i].CartId = userCart.Id;

                    foreach(var l in locationProductInfoes)
                    {
                        if (l.LocationId == updatedCartItems[i].LocationId && l.ProductId == updatedCartItems[i].ProductId)
                        {
                            l.TotalItems += currentCartItems[i].TotalItems;
                            l.TotalItems -= updatedCartItems[i].TotalItems;
                        }
                    }

                    if (updatedCartItems[i].TotalItems == 0)
                    {
                        request.updatedCartItems.Remove(updatedCartItems[i]);
                        i -= 1;
                    }
                }

                //Replace existing cart items with request items
                
                _context.CartItems.RemoveRange(currentCartItems);
                await _context.CartItems.AddRangeAsync(request.updatedCartItems);
                

                bool success = await _context.SaveChangesAsync() > 0;

                if (success)
                    return Unit.Value;

                throw new Exception("Unable to save changes to database");

            }
        }

    }
}
