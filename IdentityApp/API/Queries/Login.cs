using System.Threading;
using System.Threading.Tasks;
using API.Models;
using AutoMapper;
using Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace API.Queries
{
    // ask about nested class. Maybe better to segregate them?
    public class Login
    {
        public class Query : IRequest<UserViewModel>
        {
            public string Email { get; set; }

            public string Password { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Email).EmailAddress();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Query, UserViewModel>
        {
            private readonly IAuthService authService;

            private readonly IMapper mapper;

            public Handler(IAuthService authService, IMapper mapper)
            {
                this.authService = authService;
                this.mapper = mapper;
            }

            public async Task<UserViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await this.authService.LoginAsync(request.Email, request.Password);

                var userViewModel = this.mapper.Map<UserViewModel>(user);
                userViewModel.Token = this.authService.CreateToken(user);

                return userViewModel;
            }
        }
    }
}
