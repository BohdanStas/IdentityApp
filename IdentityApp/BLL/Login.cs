using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BLL
{
    public class Login
    {
        // todo add fluent validation

        public class Query : IRequest<User>
        {
            public string Email { get; set; }

            public string Password { get; set; }

        }

        public class Handler : IRequestHandler<Query, User>
        {
            private readonly UserManager<User> userManager;

            private readonly SignInManager<User> signInManager;

            public Handler(UserManager<User> userManager, SignInManager<User> signInManager)
            {
                this.signInManager = signInManager;
                this.userManager = userManager;
            }

            public async Task<User> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await this.userManager.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    // todo implement error handle midleware
                    throw new Exception("User not found");
                }

                var result =await this.signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (!result.Succeeded)
                {
                   throw new Exception("Password is wrong");
                }
                 
                //todo generate jwt token
                    return user;
            }
        }
    }
}
