using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Commands;
using API.Models;
using API.Queries;
using BLL;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
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
        public async Task<ActionResult<UserViewModel>> Login([FromBody]Login.Query credentials)
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
