using Application.Errors;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customers
{
    public class Login
    {
        public class Query : IRequest<Customer>
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class Handler : IRequestHandler<Query, Customer>
        {
            private readonly UserManager<Customer> _userManager;
            private readonly SignInManager<Customer> _signInManager;

            public Handler(UserManager<Customer> userManager, SignInManager<Customer> signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }

            public async Task<Customer> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.UserName);

                if (user == null) throw new RestException(HttpStatusCode.Unauthorized);

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    return user;
                }

                throw new RestException(HttpStatusCode.Unauthorized);
            }
        }
    }
}
