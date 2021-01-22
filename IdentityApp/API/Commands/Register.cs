using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
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

            public string UserName { get; set; }

            public string Password { get; set; }
        }

        // или же лучше вынести?
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Email).EmailAddress().NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, UserViewModel>
        {
            private readonly UserManager<User> userManager;

            private readonly IAuthService authService;

            private readonly IMapper mapper;

            public Handler(UserManager<User> userManager, IAuthService authService, IMapper mapper)
            {
                this.userManager = userManager;
                this.authService = authService;
                this.mapper = mapper;
            }

            public async Task<UserViewModel> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await this.userManager.FindByEmailAsync(request.Email) != null)
                {
                    throw new Exception("User with this email is exist");
                }

                if (await this.userManager.Users.AnyAsync(u => u.UserName == request.UserName))
                {
                    throw new Exception("User with this username is exist");
                }

                var user = this.mapper.Map<User>(request);

                var result = await this.userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    throw new Exception("something went wrong");
                }

                var userViewModel = this.mapper.Map<UserViewModel>(user);
                userViewModel.Token = this.authService.CreateToken(user);

                return userViewModel;
            }
        }
    }
}
