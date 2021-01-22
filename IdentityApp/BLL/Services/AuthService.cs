using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;

        private readonly SignInManager<User> signInManager;

        private readonly IConfiguration configuration;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            // generate signing credentials 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            var user = await this.userManager.FindByEmailAsync(email);

            if (user == null)
            {
                //todo custom ex with midleware
                throw new Exception("User not found");
            }

            var result = await this.signInManager.CheckPasswordSignInAsync(user, password, false);

            if (!result.Succeeded)
            {
                throw new Exception("Password is wrong");
            }

            //todo mapper
            return user;
        }
    }
}
