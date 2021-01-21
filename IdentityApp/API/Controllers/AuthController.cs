using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Domain.Entities;
using MediatR;

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
        public async Task<ActionResult<User>> Login([FromBody]Login.Query credentials)
        {
            // todo add try catch block

           return await this.mediator.Send(new Login.Query(){Email = credentials.Email,Password = credentials.Password});

        }
    }
}
