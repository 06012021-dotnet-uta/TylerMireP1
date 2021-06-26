using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customers
{
    public class Logout
    {
        public class Request : IRequest
        {
            public Customer user { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly UserManager<Customer> _userManager;
            private readonly SignInManager<Customer> _signInManager;

            public Handler(UserManager<Customer> userManager, SignInManager<Customer> signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                await _signInManager.SignOutAsync();
                return Unit.Value;
            }
        }
    }
}
