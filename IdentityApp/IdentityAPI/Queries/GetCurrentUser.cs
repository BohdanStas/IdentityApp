using IdentityAPI.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IdentityAPI.Queries
{
    public class GetCurrentUser
    {
        public class Query : IRequest<UserViewModel> { }

        public class Handler : IRequestHandler<Query, UserViewModel>
        {
            private readonly IAuthService authService;

            private readonly IMapper mapper;

            private readonly UserManager<User> userManager;

            public Handler(IAuthService authService, IMapper mapper, UserManager<User> userManager)
            {
                this.authService = authService;
                this.mapper = mapper;
                this.userManager = userManager;
            }

            public async Task<UserViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var userName = this.authService.GetCurrentUserName();
                var user = await this.userManager.FindByNameAsync(userName);

                var userViewModel = this.mapper.Map<UserViewModel>(user);
                userViewModel.Token = this.authService.CreateToken(user);

                return userViewModel;
            }
        }

    }
}
