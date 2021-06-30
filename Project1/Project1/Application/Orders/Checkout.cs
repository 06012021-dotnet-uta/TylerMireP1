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
    public class Checkout
    {
        /// <summary>
        /// Checks out the cart for the list of CartItems for the specified user
        /// </summary>
        public class Request : IRequest
        {
            public List<CartItem> cartItems { get; set; }
            public IIdentity user { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly DataContext _context;
 

            public Handler(DataContext context, UserManager<Customer> userManager)
            {
                _context = context;
            }

            public Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                //Convert cartItems to orderItems
                //Add orderItems to context.orders
                //clear cart
                Dictionary<Guid, Location> locationDetailsDict = _context.Locations.ToDictionary(x => x.Id, x => x);
                Customer customer = _context.Customers.Where(x => x.UserName == request.user.Name).FirstOrDefault();
                request.cartItems.OrderBy(x => x.LocationId);
                List<List<CartItem>> itemsByLocation = new List<List<CartItem>>();
                foreach (var key in locationDetailsDict.Keys)
                {
                    itemsByLocation.Add(request.cartItems.Where(x => x.LocationId == key).ToList());
                }

                foreach(var location in itemsByLocation)
                {
                    if (location.Count > 0)
                    {
                        Order order = new Order()
                        {
                            Id = Guid.NewGuid(),
                            LocationId = location[0].LocationId,
                            CustomerId = new Guid(customer.Id),
                            OrderCreationDate = DateTime.Now
                        };

                        List<OrderItem> orderItems = new List<OrderItem>();


                        foreach (var item in request.cartItems)
                        {
                            orderItems.Add(new OrderItem()
                            {
                                TotalItems = item.TotalItems,
                                ProductId = item.ProductId,
                                OrderId = order.Id
                            });
                        }
                        _context.Orders.Add(order);
                        _context.OrderItems.AddRange(orderItems);
                    }
                    
                }

                var contextCartItems = _context.CartItems.Where(x => x.CartId == request.cartItems[0].CartId).ToList();
                _context.CartItems.RemoveRange(contextCartItems);

                bool success =  _context.SaveChanges() > 0;

                



                if (success)
                {
                    return Task.FromResult(Unit.Value);
                }
                    

                throw new Exception("Problem saving changes to database");
                
            }
        }

    }
}
