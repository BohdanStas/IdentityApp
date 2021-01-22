using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAuthService
    {
        Task<User> LoginAsync(string email, string password);

        string CreateToken(User user);
    }
}
