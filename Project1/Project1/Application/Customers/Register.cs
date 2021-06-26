using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Customers
{
    public class Register
    {
        public class Command : IRequest<Customer>
        {
            
            public string UserName {get; set;}
            public string Password {get; set;}
            public string PasswordConfirm { get; set; }
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
            private readonly UserManager<Customer> _userManager;
            private readonly SignInManager<Customer> _signInManager;

            public Handler(DataContext context, UserManager<Customer> userManager, SignInManager<Customer> signInManager)
            {
                _context = context;
                _userManager = userManager;
                _signInManager = signInManager;
            }
            public async Task<Customer> Handle(Command request, CancellationToken cancellationToken)
            {
                if(await _context.Customers.AnyAsync(x => x.UserName == request.UserName))
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { UserName = "Username already exists." });
                }

                Customer customer = new Customer
                {
                    UserName = request.UserName,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    AddressStreet = request.AddressStreet,
                    AddressCity = request.AddressCity,
                    AddressState = request.AddressState,
                    AccountCreationDate = DateTime.Now
                };

                var result = await _userManager.CreateAsync(customer, request.Password);
                
                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(customer, false);
                    return await _userManager.FindByNameAsync(customer.UserName);
                }

                throw new Exception("Problem creating user");

                
            }
        }

    }
}