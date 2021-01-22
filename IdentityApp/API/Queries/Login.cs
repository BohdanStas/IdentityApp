using System.Threading;
using System.Threading.Tasks;
using API.Models;
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
