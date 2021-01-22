using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Models;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace API.Queries
{
    public class Login
    {
        // todo add fluent validation
        public class Query : IRequest<UserViewModel>
        {
            public string Email { get; set; }

            public string Password { get; set; }
        }

        public class Handler : IRequestHandler<Query, UserViewModel>
        {
            private readonly IAuthService authService;

            public Handler(IAuthService authService)
            {
                this.authService = authService;
            }

            public async Task<UserViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await this.authService.LoginAsync(request.Email, request.Password);
               
                //todo mapper
                return new UserViewModel()
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    Token = this.authService.CreateToken(user)
                };
            }
        }
    }
}
