using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders
{
    public class AddToCart
    {
        /// <summary>
        /// Adds the orders contained in an OrderFormModel to the database for a specified user
        /// </summary>
        public class Request : IRequest<bool>
        {
            public OrderFormModel orders {get; set;}
            public IIdentity user { get; set; }
        }

        public class Handler : IRequestHandler<Request, bool>
        {
            private readonly DataContext _context;
            private readonly UserManager<Customer> _userManager;

            public Handler(DataContext context, UserManager<Customer> userManager)
            {
                _context = context;
                _userManager = userManager;
            }

            public async Task<bool> Handle(Request request, CancellationToken cancellationToken)
            {
                var carts = await _context.Carts.ToListAsync();
                Cart userCart = await _context.Carts.Where(x => x.Customer.UserName == request.user.Name).FirstOrDefaultAsync();
                Customer user = await _userManager.FindByNameAsync(request.user.Name);
                List<LocationProductInfo> locationProducts = await _context.LocationProductInfoes.Where(x => x.LocationId == request.orders.LocationId).ToListAsync();

                if (userCart == default(Cart))
                {
                    throw new Exception($"No cart found for user {user.UserName}");
                }

                for(int i = 0; i < request.orders.ProductId.Count; i++)
                {
                    if (request.orders.ItemsAdded[i] > 0)
                    {
                        LocationProductInfo l = locationProducts.Where(x => x.ProductId == request.orders.ProductId[i]).FirstOrDefault();

                        l.TotalItems -= request.orders.ItemsAdded[i];

                        //Check to see if there is already item in cart and increase count
                        var itemMatch = await _context.CartItems.Where(x => x.ProductId == request.orders.ProductId[i]).FirstOrDefaultAsync();

                        if (itemMatch == default(CartItem))
                        {
                            _context.CartItems.Add(new CartItem
                            {
                                ProductId = request.orders.ProductId[i],
                                LocationId = request.orders.LocationId,
                                TotalItems = request.orders.ItemsAdded[i],
                                CartId = userCart.Id
                            });
                        }
                        else
                        {
                            itemMatch.TotalItems += request.orders.ItemsAdded[i];
                        }
                    }
                }

                bool success = await _context.SaveChangesAsync() > 0;

                if (success)
                    return true;
                
                throw new Exception("Unable to save changes to database");
            }
        }
    }
}
