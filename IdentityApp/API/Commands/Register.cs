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
using Microsoft.EntityFrameworkCore;

namespace API.Commands
{
    public class Register
    {
        public class Command : IRequest<UserViewModel>
        {
            public string Email { get; set; }

            public string Username { get; set; }

            public string Password { get; set; }
        }

        public class Handler : IRequestHandler<Command, UserViewModel>
        {
            private readonly UserManager<User> userManager;

            private readonly IAuthService authService;

            public Handler(UserManager<User> userManager, IAuthService authService)
            {
                this.userManager = userManager;
                this.authService = authService;
            }

            public async Task<UserViewModel> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await this.userManager.FindByEmailAsync(request.Email) != null)
                {
                    throw new Exception("User with this email is exist");
                }

                if (await this.userManager.Users.AnyAsync(u => u.UserName == request.Username))
                {
                    throw new Exception("User with this username is exist");
                }

                var user = new User()
                {
                    Email = request.Email,
                    UserName = request.Username
                };

                var result = await this.userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    throw new Exception("something went wrong");
                }

                return new UserViewModel()
                {
                    Email = request.Email,
                    UserName = request.Username,
                    Token = this.authService.CreateToken(user)
                };

            }
        }
    }
}
