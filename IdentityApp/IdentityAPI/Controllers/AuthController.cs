using System.Threading.Tasks;
using IdentityAPI.Commands;
using IdentityAPI.Models;
using IdentityAPI.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace IdentityAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserViewModel>> Login([FromBody] Login.Query credentials)
        {
            // todo add try catch block
            return await this.mediator.Send(credentials);
        }

        [HttpGet]
        public ActionResult<string> GetSecuredString()
        {
            return "My string with authorize attr";
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserViewModel>> Register([FromBody] Register.Command command)
        {
            return await this.mediator.Send(command);
        }
    }
}
