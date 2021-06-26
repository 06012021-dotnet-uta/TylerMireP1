using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Customers
{
    public class Register
    {
        public class Command : IRequest<Customer>
        {
            public string Username {get; set;}
            public string Password {get; set;}
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string AddressStreet { get; set; }
            public string AddressCity { get; set; }
            public string AddressState { get; set; }
        }

        public class Handler : IRequestHandler<Command, Customer>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Customer> Handle(Command request, CancellationToken cancellationToken)
            {
                if(await _context.Customers.AnyAsync(x => x.Username == request.Username))
                {
                    throw new Exception("User already present");
                }


                Customer customer = new Customer
                {
                    Username = request.Username,
                    PasswordHash = Encoding.ASCII.GetBytes(request.Password),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    AddressStreet = request.AddressStreet,
                    AddressCity = request.AddressCity,
                    AddressState = request.AddressState
                };

                await _context.Customers.AddAsync(customer);
                bool success = await _context.SaveChangesAsync() > 0;
                
                if(success)
                {
                    return customer;
                }
                
                throw new Exception("Problem saving customer.");
            }
        }

    }
}