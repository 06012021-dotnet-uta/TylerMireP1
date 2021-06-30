using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Customers
{
    public class Login
    {
        /// <summary>
        /// Makes a loggin attempt and returns a customer if login was successful
        /// </summary>
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

                if (user == null) return null;

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user);
                    return user;
                }

                return null;
            }
        }
    }
}
